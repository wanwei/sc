using com.wer.sc.data.opentime;
using com.wer.sc.data.transfer;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.historydata.csv;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator.kline
{
    /// <summary>
    /// 一天的K线数据生成器
    /// </summary>
    public class Step_KLineData_OneDay : IStep
    {
        private string code;

        private int date;

        private KLinePeriod klinePeriod;

        private float lastEndPrice;

        private int lastEndHold;

        private DataLoader dataLoader;

        private IKLineData klineData;

        public Step_KLineData_OneDay(string code, int date, KLinePeriod klinePeriod, DataLoader dataLoader, float lastEndPrice, int lastEndHold)
        {
            this.code = code;
            this.date = date;
            this.klinePeriod = klinePeriod;
            this.dataLoader = dataLoader;
            this.lastEndPrice = lastEndPrice;
            this.lastEndHold = lastEndHold;
        }

        public int ProgressStep
        {
            get
            {
                return 3;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新";
            }
        }

        public IKLineData KlineData
        {
            get
            {
                return klineData;
            }
        }

        public string Proceed()
        {
            TickData tickData = (TickData)dataLoader.Plugin_HistoryData.GetTickData(code, date);
            /*
             * 此处不处理tickData为空的情况
             * 在DataTransfer_Tick2KLine.Transfer里处理tickData为空的情况
             */
            IOpenDateReader openDateReader = dataLoader.DataLoader_OpenDate.GetOpenDateReader();
            IOpenTimeReader openTimeReader = dataLoader.DataLoader_OpenTime;
            IKLineTimeListGetter timeListGetter = new KLineTimeListGetter(openDateReader, openTimeReader);
            List<double> klineTimes = timeListGetter.GetKLineTimes(code, date, klinePeriod);
            this.klineData = DataTransfer_Tick2KLine.Transfer(tickData, klineTimes, lastEndPrice, lastEndHold);
            string path = CsvHistoryDataPathUtils.GetKLineDataPath(dataLoader.PluginSrcDataPath, code, date, klinePeriod);
            CsvUtils_KLineData.Save(path, klineData);
            return "更新" + code + "-" + date + "的" + klinePeriod + "K线完成";
        }
    }
}
