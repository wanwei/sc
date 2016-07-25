﻿using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class KLineDataWrap
    {
        private String code;

        private int start;

        private int end;

        private KLinePeriod period;

        private KLineData klineData;

        private KLineDataReader dataReader;

        public KLineDataWrap(KLineDataReader dataReader)
        {
            this.dataReader = dataReader;
        }

        public void RefreshData(String code, int start, int end, KLinePeriod period)
        {
            this.code = code;
            this.start = start;
            this.end = end;
            this.period = period;
            this.klineData = dataReader.GetData(code, start, end, period);
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public int Start
        {
            get
            {
                return start;
            }

        }

        public int End
        {
            get
            {
                return end;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return period;
            }
        }

        public KLineData KlineData
        {
            get
            {
                return klineData;
            }
        }
    }
}
