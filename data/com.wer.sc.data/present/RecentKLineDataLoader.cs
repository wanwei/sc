﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.present
{
    /// <summary>
    /// 最近的K线数据装载
    /// </summary>
    public class RecentKLineDataLoader
    {
        private DataReaderFactory dataReaderFactory;

        public RecentKLineDataLoader(DataReaderFactory dataReaderFactory)
        {
            this.dataReaderFactory = dataReaderFactory;
        }

        public IKLineData GetRecentKLineData(string code, int lastOpenDate, KLinePeriod period)
        {
            return dataReaderFactory.KLineDataReader.GetData(code, GetFirstDate(dataReaderFactory, lastOpenDate, period), lastOpenDate, period);
        }

        /// <summary>
        /// 默认获取如下数据：
        /// 日线：获取500日
        /// 1小时：获取4月
        /// 15分钟：获取30日
        /// 5分钟：获取10日
        /// 1分钟：获取3日
        /// </summary>
        /// <param name="dataReaderFactory"></param>
        /// <param name="lastDate"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        private int GetFirstDate(DataReaderFactory dataReaderFactory, int lastDate, KLinePeriod period)
        {
            ICommonDataReader_OpenDate openDateReader = dataReaderFactory.OpenDateReader;
            //if (period.CompareTo(KLinePeriod.KLinePeriod_1Week) >= 0)
            //    return openDateReader.GetPrevOpenDate(lastDate, 500);
            if (period.CompareTo(KLinePeriod.KLinePeriod_1Day) >= 0)
                return openDateReader.GetPrevOpenDate(lastDate, 500 - 1);
            if (period.CompareTo(KLinePeriod.KLinePeriod_1Hour) >= 0)
                return openDateReader.GetPrevOpenDate(lastDate, 150 - 1);
            if (period.CompareTo(KLinePeriod.KLinePeriod_15Minute) >= 0)
                return openDateReader.GetPrevOpenDate(lastDate, 40 - 1);
            if (period.CompareTo(KLinePeriod.KLinePeriod_5Minute) >= 0)
                return openDateReader.GetPrevOpenDate(lastDate, 10 - 1);
            if (period.CompareTo(KLinePeriod.KLinePeriod_1Minute) >= 0)
                return openDateReader.GetPrevOpenDate(lastDate, 3 - 1);
            return openDateReader.GetPrevOpenDate(lastDate, 1 - 1); ;
        }
    }
}
