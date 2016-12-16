using com.wer.sc.data;
using com.wer.sc.data.provider;
using com.wer.sc.data.utils;
using com.wer.sc.plugin.historydata.csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.mockdata
{
    public class MockData_Tick
    {
        private static string tickPath = @"E:\FUTURES\CSV\TICKADJUSTED";

        private static DataProvider_TickData dataProvider = new DataProvider_TickData(tickPath);

        public static ITickData GetTickData(string code, int date)
        {
            return CsvUtils_TickData.Load(CsvHistoryDataPathUtils.GetTickDataPath(tickPath, code, date));
        }
    }
}
