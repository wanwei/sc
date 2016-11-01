using com.wer.sc.data.store;
using com.wer.sc.plugin;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.generator
{
    public class Step_TickData_OneDay : IStep
    {
        private string code;

        private int date;

        private IPlugin_HistoryData historyData;

        private TickDataStore store;

        public Step_TickData_OneDay(string code, int date, IPlugin_HistoryData historyData, DataPathUtils dataPathUtils)
        {
            this.historyData = historyData;
            this.code = code;
            this.date = date;
            this.store = new TickDataStore(dataPathUtils.GetTickPath(code, date));
        }

        public int ProgressStep
        {
            get
            {
                return 5;
            }
        }

        public string StepDesc
        {
            get
            {
                return "Tick数据更新" + code + "-" + date;
            }
        }

        public string Proceed()
        {
            TickData tickData = (TickData)historyData.GetTickData(code, date);
            store.Save(tickData);
            return StepDesc + "完成";
        }
    }
}