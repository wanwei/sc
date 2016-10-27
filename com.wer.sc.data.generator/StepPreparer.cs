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

        //private DataLoader dataLoader;

        //private bool updateFillUp;

        //public StepPreparer(string srcDataPath, string pluginSrcDataPath, bool updateFillUp)
        //{
        //    this.srcDataPath = srcDataPath;
        //    this.pluginSrcDataPath = pluginSrcDataPath;
        //    this.dataLoader = new DataLoader(srcDataPath, pluginSrcDataPath);
        //    this.updateFillUp = updateFillUp;
        //}

        //public List<IStep> GetAllSteps()
        //{
        //    DataLoader dataLoader = new DataLoader(srcDataPath, pluginSrcDataPath);

        //    List<IStep> steps = new List<IStep>();
        //    steps.Add(new Step_OpenDate(dataLoader));
        //    steps.Add(new Step_CodeInfo(pluginSrcDataPath));
        //    steps.Add(new Step_OpenTime(pluginSrcDataPath));
        //    this.preparer = new HistoryData_PrepareForUpdate(pluginSrcDataPath, dataLoader.DataLoader_CodeInfo.GetAllCodes(), dataLoader.DataLoader_OpenDate.GetOpenDates());

        //    GetDayStartTime(steps);
        //    GetTickSteps(steps);
        //    GetKLineDataSteps(steps);
        //    return steps;
        //}        
    }
}