using com.wer.sc.data;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.mockdata
{
    public class MockData_DayStartTime
    {
        public static List<DayOpenTime> GetDayStartTime(string code)
        {
            return Plugin_HistoryData_CsvProvider_Mock.Plugin_History.GetDayOpenTime(code);
        }
    }
}
