using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime
{
    public class RealTimeDataNavigateFactory : IRealTimeDataNavigaterFactory
    {
        private DataReaderFactory dataReaderFactory;

        public RealTimeDataNavigateFactory(DataReaderFactory dataReaderFactory)
        {
            this.dataReaderFactory = dataReaderFactory;
        }

        public IRealTimeDataNavigater CreateNavigater(string code, double time, int startDate, int endDate)
        {
            return new RealTimeDataNavigater(dataReaderFactory, code, time, startDate, endDate);
        }

        public IRealTimeDataNavigater CreateNavigater(string code, double time)
        {
            return new RealTimeDataNavigater(dataReaderFactory, code, time);
        }
    }
}
