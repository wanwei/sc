using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.data;

namespace com.wer.sc.plugin.mock.historydata
{
    [Plugin("MOCK.HISTORYDATA.SINA", "模拟历史数据，新浪数据", "模拟历史数据，模拟取新浪数据，测试专用", MarketType.CnStock)]
    public class Plugin_HistoryData_Mock_Sina : IPlugin_HistoryData
    {
        public List<CodeInfo> GetCodes()
        {
            throw new NotImplementedException();
        }

        public string GetDataPath()
        {
            throw new NotImplementedException();
        }

        public List<DayOpenTime> GetDayOpenTime(string code)
        {
            throw new NotImplementedException();
        }

        public IKLineData GetKLineData(string code, int startDate, int endDate, KLinePeriod klinePeriod)
        {
            throw new NotImplementedException();
        }

        public NeedsToUpdate GetNeedsToUpdate()
        {
            throw new NotImplementedException();
        }

        public List<int> GetOpenDates()
        {
            throw new NotImplementedException();
        }

        public ITickData GetTickData(string code, int date)
        {
            throw new NotImplementedException();
        }
    }
}
