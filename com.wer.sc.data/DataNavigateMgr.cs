using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using com.wer.sc.data.navigate;

namespace com.wer.sc.data
{
    public class DataNavigateMgr
    {
        private DataReaderFactory dataReaderFactory;

        public DataNavigateMgr(DataReaderFactory dataReaderFactory)
        {
            this.dataReaderFactory = dataReaderFactory;
        }

        public IDataNavigate2 CreateNavigate(String code, double time)
        {
            return new DataNavigate2(dataReaderFactory, code, time);
        }

        public IDataNavigate2 CreateNavigate(String code, double time, int startDate, int endDate)
        {
            return new DataNavigate2(dataReaderFactory, code, time, startDate, endDate);
        }
    }
}
