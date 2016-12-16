using com.wer.sc.data.provider;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.generator
{
    /// <summary>
    /// 数据更新的准备步骤，将所有步骤准备乘IStep的形式
    /// </summary>
    public class StepPreparer
    {
        private const int DAYS_EVERYTICKSTEP = 10;

        private const int DAYS_EVERYKLINESTEP = 50;

        private List<CodeInfo> codes;

        private List<int> openDates;

        private IPlugin_HistoryData historyData;

        private DataPathUtils dataPathUtils;

        private WaitForUpdateInfoGetter waitForUpdateInfoGetter;

        private bool isFillUp;

        public StepPreparer(IPlugin_HistoryData plugin_HistoryData, bool isFillUp)
        {
            this.historyData = plugin_HistoryData;
            this.dataPathUtils = new DataPathUtils(plugin_HistoryData.GetDataPath());
            this.isFillUp = isFillUp;
        }

        public List<IStep> GetAllSteps()
        {
            List<IStep> steps = new List<IStep>();

            Step_OpenDate step_OpenDate = new Step_OpenDate(historyData, dataPathUtils);
            steps.Add(step_OpenDate);
            Step_CodeInfo step_CodeInfo = new Step_CodeInfo(historyData, dataPathUtils);
            steps.Add(step_CodeInfo);

            this.openDates = step_OpenDate.OpenDates;
            this.codes = step_CodeInfo.Codes;

            this.waitForUpdateInfoGetter = new WaitForUpdateInfoGetter(historyData.GetDataPath(), codes, openDates, new OpenDateReader_DataCenter(historyData));

            GetDayStartTime(steps);
            GetTickSteps(steps);
            GetKLineDataSteps(steps);
            return steps;
        }

        private void GetDayStartTime(List<IStep> steps)
        {
            for (int i = 0; i < codes.Count; i++)
            {
                Step_DayStartTime step = new Step_DayStartTime(codes[i].Code, historyData, dataPathUtils);
                steps.Add(step);
            }
        }

        private void GetTickSteps(List<IStep> steps)
        {
            List<WaitForUpdateInfo> waitForUpdateInfos = waitForUpdateInfoGetter.GetTickNewData(isFillUp);
            for (int i = 0; i < waitForUpdateInfos.Count; i++)
            {
                GetTickSteps(steps, waitForUpdateInfos[i]);
            }
        }

        private void GetTickSteps(List<IStep> steps, WaitForUpdateInfo waitForUpdateInfo)
        {
            string code = waitForUpdateInfo.code;

            int stepCount = waitForUpdateInfo.dates.Count / DAYS_EVERYTICKSTEP;
            int lastStepUpdateCount = waitForUpdateInfo.dates.Count % DAYS_EVERYTICKSTEP;
            if (lastStepUpdateCount != 0)
                stepCount++;
            else
                lastStepUpdateCount = DAYS_EVERYTICKSTEP;
            List<int> openDates = waitForUpdateInfo.dates;
            for (int i = 0; i < stepCount; i++)
            {
                IStep step;
                if (i != stepCount - 1)
                    step = new Step_TickData(waitForUpdateInfo.code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, DAYS_EVERYTICKSTEP), historyData, dataPathUtils);
                else
                    step = new Step_TickData(waitForUpdateInfo.code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, lastStepUpdateCount), historyData, dataPathUtils);
                steps.Add(step);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps)
        {

            Dictionary<string, List<KLineWaitForUpdateInfo>> dic = GetAllWaitForUpdateInfo();
            for (int i = 0; i < codes.Count; i++)
            {
                string code = codes[i].Code;
                if (dic.ContainsKey(code))
                {
                    GetKLineDataSteps(steps, dic[code]);
                }
            }
        }

        private Dictionary<string, List<KLineWaitForUpdateInfo>> GetAllWaitForUpdateInfo()
        {
            List<KLinePeriod> periods = historyData.GetNeedsToUpdate().KlinePeriods;
            Dictionary<string, List<KLineWaitForUpdateInfo>> dic = new Dictionary<string, List<KLineWaitForUpdateInfo>>();
            for (int i = 0; i < periods.Count; i++)
            {
                List<WaitForUpdateInfo> dataInfoList = waitForUpdateInfoGetter.GetKLineNewData(periods[i], isFillUp);
                Add2WaitForUpdateInfoDic(dic, periods[i], dataInfoList);
            }

            return dic;
        }

        private void Add2WaitForUpdateInfoDic(Dictionary<string, List<KLineWaitForUpdateInfo>> dic, KLinePeriod klinePeriod, List<WaitForUpdateInfo> dataInfoList)
        {
            for (int i = 0; i < dataInfoList.Count; i++)
            {
                WaitForUpdateInfo updateInfo = dataInfoList[i];
                if (dic.ContainsKey(updateInfo.code))
                {
                    dic[updateInfo.code].Add(new KLineWaitForUpdateInfo(updateInfo, klinePeriod));
                }
                else
                {
                    List<KLineWaitForUpdateInfo> updateinfos = new List<KLineWaitForUpdateInfo>();
                    updateinfos.Add(new KLineWaitForUpdateInfo(updateInfo, klinePeriod));
                    dic.Add(updateInfo.code, updateinfos);
                }
            }
        }

        private void GetKLineDataSteps(List<IStep> steps, List<KLineWaitForUpdateInfo> updateDataInfos)
        {
            for (int i = 0; i < updateDataInfos.Count; i++)
            {
                KLineWaitForUpdateInfo klineUpdateDataInfo = updateDataInfos[i];
                WaitForUpdateInfo updateDataInfo = klineUpdateDataInfo.WaitForUpdateInfo;
                if (updateDataInfo.dates.Count == 0)
                    continue;
                string code = updateDataInfo.code;
                int startDate = updateDataInfo.dates[0];
                int endDate = updateDataInfo.dates[updateDataInfo.dates.Count - 1];
                Step_KLineData step = new Step_KLineData(code, startDate, endDate, klineUpdateDataInfo.KLinePeriod, historyData, dataPathUtils);
                steps.Add(step);
            }

        }
    }

    class KLineWaitForUpdateInfo
    {
        public WaitForUpdateInfo WaitForUpdateInfo;

        public KLinePeriod KLinePeriod;

        public KLineWaitForUpdateInfo(WaitForUpdateInfo WaitForUpdateInfo, KLinePeriod KLinePeriod)
        {
            this.WaitForUpdateInfo = WaitForUpdateInfo;
            this.KLinePeriod = KLinePeriod;
        }
    }
}