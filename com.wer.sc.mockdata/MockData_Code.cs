using com.wer.sc.data;
using com.wer.sc.data.utils;
using com.wer.sc.mockdata.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.mockdata
{
    public class MockData_Code
    {
        public static List<CodeInfo> GetAllCodes()
        {
            return Plugin_HistoryData_CsvProvider_Mock.Plugin_History.GetCodes();
        }
    }
}