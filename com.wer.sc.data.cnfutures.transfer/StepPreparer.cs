using com.wer.sc.data.historydata;
using com.wer.sc.data.provider;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    /// <summary>
    /// 更新国内期货市场历史数据的准备步骤
    /// </summary>
    public class StepPreparer
    {
        private const int DAYS_EVERYTICKSTEP = 10;

        private const int DAYS_EVERYKLINESTEP = 50;

        private string srcDataPath;

        private string pluginSrcDataPath;

        private HistoryData_PrepareForUpdate preparer;

        private DataLoader dataLoader;

        private bool updateFillUp;

        public StepPreparer(string srcDataPath, string pluginSrcDataPath, bool updateFillUp)
        {
            this.srcDataPath = srcDataPath;
            this.pluginSrcDataPath = pluginSrcDataPath;
            this.dataLoader = new DataLoader(srcDataPath, pluginSrcDataPath);
            this.updateFillUp = updateFillUp;
        }

        public List<IStep> GetAllSteps()
        {
            DataLoader dataLoader = new DataLoader(srcDataPath, pluginSrcDataPath);

            List<IStep> steps = new List<IStep>();
            steps.Add(new Step_OpenDate(dataLoader));
            steps.Add(new Step_CodeInfo(pluginSrcDataPath));
            steps.Add(new Step_OpenTime(pluginSrcDataPath));
            this.preparer = new HistoryData_PrepareForUpdate(pluginSrcDataPath, dataLoader.DataLoader_CodeInfo.GetAllCodes(), dataLoader.DataLoader_OpenDate.GetOpenDates());

            GetDayStartTime(steps);
            GetTickSteps(steps);
            GetKLineDataSteps(steps);
            return steps;
        }

        private void GetDayStartTime(List<IStep> steps)
        {
            List<CodeInfo> codes = dataLoader.DataLoader_CodeInfo.GetAllCodes();
            for (int i = 0; i < codes.Count; i++)
            {
                steps.Add(new Step_DayStartTime(codes[i].Code, dataLoader));
            }
        }

        private void GetTickSteps(List<IStep> steps)
        {
            List<UpdateDataInfo> dataInfoList = preparer.GetTickNewData(updateFillUp);
            for (int i = 0; i < dataInfoList.Count; i++)
            {
                GetTickSteps(steps, dataInfoList[i]);
            }
        }

        private void GetTickSteps(List<IStep> steps, UpdateDataInfo updateDataInfo)
        {
            int stepCount = updateDataInfo.dates.Count / DAYS_EVERYTICKSTEP;
            int lastStepUpdateCount = updateDataInfo.dates.Count % DAYS_EVERYTICKSTEP;
            if (lastStepUpdateCount != 0)
                stepCount++;
            else
                lastStepUpdateCount = DAYS_EVERYTICKSTEP;
            List<int> openDates = updateDataInfo.dates;
            for (int i = 0; i < stepCount; i++)
            {
                IStep step;
                if (i != stepCount - 1)
                    step = new Step_TickData(updateDataInfo.code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, DAYS_EVERYTICKSTEP), dataLoader);
                else
                    step = new Step_TickData(updateDataInfo.code, openDates.GetRange(i * DAYS_EVERYTICKSTEP, lastStepUpdateCount), dataLoader);
                steps.Add(step);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps)
        {
            List<UpdateDataInfo> dataInfoList = preparer.GetKLineNewData(KLinePeriod.KLinePeriod_1Minute, updateFillUp);
            for (int i = 0; i < dataInfoList.Count; i++)
            {
                GetKLineDataSteps(steps, dataInfoList[i]);
            }
        }

        private void GetKLineDataSteps(List<IStep> steps, UpdateDataInfo updateDataInfo)
        {
            int stepCount = updateDataInfo.dates.Count / DAYS_EVERYKLINESTEP;
            int lastStepUpdateCount = updateDataInfo.dates.Count % DAYS_EVERYKLINESTEP;
            if (lastStepUpdateCount != 0)
                stepCount++;
            else
                lastStepUpdateCount = DAYS_EVERYKLINESTEP;

            List<int> openDates = updateDataInfo.dates;
            string code = updateDataInfo.code;

            for (int i = 0; i < stepCount; i++)
            {
                Step_KLineData step;
                if (i != stepCount - 1)
                    step = new Step_KLineData(updateDataInfo.code, openDates.GetRange(i * DAYS_EVERYKLINESTEP, DAYS_EVERYKLINESTEP), dataLoader);
                else
                    step = new Step_KLineData(updateDataInfo.code, openDates.GetRange(i * DAYS_EVERYKLINESTEP, lastStepUpdateCount), dataLoader);
                steps.Add(step);
            }
        }
    }
}