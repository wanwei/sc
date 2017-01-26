using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;
using com.wer.sc.plugin.historydata.csv;

namespace com.wer.sc.plugin.mockmarket
{
    [Plugin("MOCKHISTORYDATA", "历史数据", "历史数据")]
    public class MockPlugin_HistoryData : Plugin_HistoryData_CsvProvider
    {
        public MockPlugin_HistoryData()
        {
        }

        public override string GetPluginSrcDataPath()
        {
            return @"E:\Futures\Csv\";
        }

        public override string GetDataPath()
        {
            return @"D:\SCTEST\MOCKDATA\";
        }

        public override string GetDescription()
        {
            return "MOCK出来的数据";
        }

        public override string GetName()
        {
            return "MockData";
        }

        public override NeedsToUpdate GetNeedsToUpdate()
        {
            NeedsToUpdate ntu = new NeedsToUpdate();
            ntu.IsTickUpdate = true;
            ntu.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Minute);
            ntu.KlinePeriods.Add(KLinePeriod.KLinePeriod_15Minute);
            ntu.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Hour);
            ntu.KlinePeriods.Add(KLinePeriod.KLinePeriod_1Day);
            return ntu;
        }

    }
}
