using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;
using com.wer.sc.plugin.historydata.csv;

namespace com.wer.sc.data.cnfutures
{
    [Plugin("HistoryData.CnFutures", "中国期货市场", "提供中国期货市场的各种数据，包括大连、上期、郑州、中金四个期货交易所", MarketType.CnFutures)]
    public class Plugin_HistoryData_CnFutures : Plugin_HistoryData_CsvProvider
    {
        private NeedsToUpdate needsToUpdate;

        public Plugin_HistoryData_CnFutures()
        {
            this.needsToUpdate = new NeedsToUpdate();
            needsToUpdate.IsTickUpdate = true;
            needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Hour);
            needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
        }

        public override string GetPluginSrcDataPath()
        {
            return @"E:\FUTURES\CSV\TICKADJUSTED\";
        }

        public override string GetDataPath()
        {
            return @"D:\SCDATA\CNFUTURES\";
        }        

        public override NeedsToUpdate GetNeedsToUpdate()
        {
            return needsToUpdate;
        }
    }
}
