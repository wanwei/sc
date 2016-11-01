using com.wer.sc.data.historydata;
using com.wer.sc.data.provider;
using com.wer.sc.plugin;
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
    /// 更新国内期货市场历史数据的准备步骤
    /// </summary>
    public class StepPreparer
    {
        private const int DAYS_EVERYTICKSTEP = 10;

        private const int DAYS_EVERYKLINESTEP = 50;

        private List<CodeInfo> codes;

        private List<int> openDates;

        private IPlugin_HistoryData historyData;

        private DataPathUtils dataPathUtils;

        public StepPreparer(IPlugin_HistoryData plugin_HistoryData)
        {
            this.historyData = plugin_HistoryData;
            this.dataPathUtils = new DataPathUtils(plugin_HistoryData.GetDataPath());
        }

        public List<IStep> GetAllSteps()
        {
            List<IStep> steps = new List<IStep>();
            steps.Add(new Step_OpenDate(historyData, dataPathUtils));
            steps.Add(new Step_CodeInfo(historyData, dataPathUtils));

            this.codes = historyData.GetCodes();
            this.openDates = historyData.GetOpenDates();

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
            for (int i = 0; i < codes.Count; i++)
            {
                GetTickSteps(steps, codes[i].Code);
            }
        }

        private void GetTickSteps(List<IStep> steps, string code)
        {

        }

        //private void GetTickSteps(List<IStep> steps, UpdateDataInfo updateDataInfo)
        //{
        //    int stepCount = updateDataInfo.dates.Count / DAYS_EVERYTICKSTEP;
        //    int lastStepUpdateCount = updateDataInfo.dates.Count % DAYS_EVERYTICKSTEP;
        //    if (lastStepUpdateCount != 0)
        //        stepCount++;
        //    else
        //        lastStepUpdateCount = DAYS_EVERYTICKSTEP;
        //    List<int> openDates = updateDataInfo.dates;
        //    for (int i = 0; i < stepCount; i++)
        //    {
        //        IStep step;
        //        if (i != stepCount - 1)
        //            step = new Step_TickData(updateDataInfo.code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, DAYS_EVERYTICKSTEP), dataLoader);
        //        else
        //            step = new Step_TickData(updateDataInfo.code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, lastStepUpdateCount), dataLoader);
        //        steps.Add(step);
        //    }
        //}


        private void GetKLineDataSteps(List<IStep> steps)
        {

        }
    }

    /// <summary>
    /// 得到
    /// </summary>
    public class UpdateInfoGetter
    {
        public void GetUpdateInfo()
        {

        }
    }
}