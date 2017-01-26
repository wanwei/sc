using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class CommonDataReaderMgr_OpenDate
    {
        private Dictionary<string, ICommonDataReader_OpenDate> dic = new Dictionary<string, ICommonDataReader_OpenDate>();

        private DataReaderFactory fac;

        public CommonDataReaderMgr_OpenDate(DataReaderFactory fac)
        {
            this.fac = fac;
        }

        public ICommonDataReader_OpenDate GetOpenDateReader(string code)
        {
            ICommonDataReader_OpenDate openDateReader;
            bool exist = dic.TryGetValue(code, out openDateReader);
            if (exist)
                return openDateReader;
            return GetOpenDateReaderInternal(code);
        }

        private Object lockObj = new object();

        private ICommonDataReader_OpenDate GetOpenDateReaderInternal(string code)
        {
            lock (lockObj)
            {
                if (dic.ContainsKey(code))
                    return dic[code];
                List<int> dates = fac.TickDataReader.GetTickDates(code);
                if (dates == null)
                    return null;
                OpenDateCache openDateReader = new OpenDateCache(dates);
                dic.Add(code, openDateReader);
                return openDateReader;
            }
        }

    }
}
