using com.wer.sc.plugin;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.generator
{
    public class Step_TickData : IStep
    {
        private string code;

        private List<int> openDates;

        private IPlugin_HistoryData historyData;

        private DataPathUtils dataPathUtils;

        public Step_TickData(string code, List<int> openDates, IPlugin_HistoryData historyData, DataPathUtils dataPathUtils)
        {
            this.code = code;
            this.openDates = openDates;
            this.historyData = historyData;
            this.dataPathUtils = dataPathUtils;
        }

        public int ProgressStep
        {
            get
            {
                return 5 * openDates.Count;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + code + "的" + openDates[0] + "-" + openDates[openDates.Count - 1] + "的Tick数据";
            }
        }

        public string Proceed()
        {
            for (int i = 0; i < openDates.Count; i++)
            {
                int openDate = openDates[i];
                Step_TickData_OneDay step = new Step_TickData_OneDay(code, openDate, historyData, dataPathUtils);
                step.Proceed();
            }
            return StepDesc + "完成";
        }
    }
}