using com.wer.sc.data.historydata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;

namespace com.wer.sc.data.cnfutures
{
    public class Plugin_HistoryData_CnFutures : Plugin_HistoryData_Abstract
    {
        private NeedsToUpdate needsToUpdate;

        public Plugin_HistoryData_CnFutures(string pluginSrcDataPath) : base(pluginSrcDataPath)
        {
            this.needsToUpdate = new NeedsToUpdate();
            needsToUpdate.IsTickUpdate = true;
            needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Hour);
            needsToUpdate.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
        }

        public override string GetDataPath()
        {
            return @"D:\SCDATA\CNFUTURES\";
        }

        public override string GetDescription()
        {
            return "提供中国市场的期货历史数据";
        }

        public override string GetName()
        {
            return "CNFUTURES";
        }

        public override NeedsToUpdate GetNeedsToUpdate()
        {
            return needsToUpdate;
        }
    }
}
