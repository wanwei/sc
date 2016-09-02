﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cache.impl
{
    public class DayDataCache
    {
        private DataReaderFactory dataReaderFac;

        private Dictionary<DayDataKey, IRealData> dicRealData = new Dictionary<DayDataKey, IRealData>();

        private List<DayDataKey> keies = new List<DayDataKey>();

        public DayDataCache(DataReaderFactory dataReaderFac)
        {
            this.dataReaderFac = dataReaderFac;
        }

        public TickData GetTickData(String code, int date)
        {
            return dataReaderFac.TickDataReader.GetTickData(code, date);
        }

        public IKLineData GetKLineData(String code, int date)
        {
            return dataReaderFac.KLineDataReader.GetData(code, date, date, new KLinePeriod(KLinePeriod.TYPE_MINUTE, 1));
        }

        public IRealData GetRealData(string code, int date)
        {
            DayDataKey key = new DayDataKey(code, date);
            if (dicRealData.ContainsKey(key))
                return dicRealData[key];

            IRealData realdata = dataReaderFac.RealDataReader.GetData(code, date);
            if (keies.Count > 10)
            {
                DayDataKey firstKey = keies[0];
                keies.RemoveAt(0);
                dicRealData.Remove(firstKey);
                keies.Add(key);
                dicRealData.Add(key, realdata);
            }
            return realdata;
        }
    }

    public class DayDataKey
    {
        public string Code;

        public int Date;

        public DayDataKey(string code, int date)
        {
            this.Code = code;
            this.Date = date;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
