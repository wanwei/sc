using com.wer.sc.plugin.historydata.csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;

namespace com.wer.sc.mockdata
{
    public class Plugin_HistoryData_CsvProvider_Mock : Plugin_HistoryData_CsvProvider
    {
        public override string GetDataPath()
        {
            throw new NotImplementedException();
        }

        public override NeedsToUpdate GetNeedsToUpdate()
        {
            throw new NotImplementedException();
        }

        public override string GetPluginSrcDataPath()
        {
            return @"E:\FUTURES\CSV\TICKADJUSTED";
        }

        private static IPlugin_HistoryData plugin = new Plugin_HistoryData_CsvProvider_Mock();

        public static IPlugin_HistoryData Plugin_History
        {
            get { return plugin; }
        }
    }
}
