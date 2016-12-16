using com.wer.sc.data.store;
using com.wer.sc.data.utils;
using com.wer.sc.plugin;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.generator
{
    public class Step_KLineData : IStep
    {
        private string code;

        private int startDate;

        private int endDate;

        private KLinePeriod period;

        private IPlugin_HistoryData historyData;

        private DataPathUtils dataPathUtils;

        public Step_KLineData(string code, int startDate, int endDate, KLinePeriod period, IPlugin_HistoryData historyData, DataPathUtils dataPathUtils)
        {
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
            this.period = period;
            this.historyData = historyData;
            this.dataPathUtils = dataPathUtils;
        }

        public int ProgressStep
        {
            get
            {
                if (period.PeriodType == KLinePeriod.TYPE_DAY)
                    return 3;

                return 3 * TimeUtils.Substract(endDate, startDate).Days;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新" + code + "的" + startDate + "-" + endDate + "的" + period + "K线数据";
            }
        }

        public string Proceed()
        {
            IKLineData klineData = historyData.GetKLineData(code, startDate, endDate, period);
            KLineDataStore store = new KLineDataStore(dataPathUtils.GetKLineDataPath(code, period));
            store.Append(klineData);
            return StepDesc + "完毕";
        }
    }
}