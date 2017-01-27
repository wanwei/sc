using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.mockdata
{
    public class MockData_KLine
    {

        public IKLineData GetData(string code, int startDate, int endDate, KLinePeriod period)
        {
            return Plugin_HistoryData_CsvProvider_Mock.Plugin_History.GetKLineData(code, startDate, endDate, period);
        }
    }
}
