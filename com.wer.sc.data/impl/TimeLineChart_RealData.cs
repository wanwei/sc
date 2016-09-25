﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class TimeLineChart_RealData : TimeLineChart_Abstract
    {
        private ITimeLineData realData;
        private int index;

        public TimeLineChart_RealData(ITimeLineData realData, int index)
        {
            this.realData = realData;
            this.index = index;
        }

        public override string Code
        {
            get
            {
                return realData.Code;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Hold
        {
            get
            {
                return realData.Arr_Hold[index];
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override int Mount
        {
            get
            {
                return realData.Arr_Mount[index];
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override float Price
        {
            get
            {
                return realData.Arr_Price[index];
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override double Time
        {
            get
            {
                return realData.Arr_Time[index];
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override float UpPercent
        {
            get
            {
                return realData.Arr_UpPercent[index];
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override float UpRange
        {
            get
            {
                return realData.Arr_UpRange[index];
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ITimeLineData RealData
        {
            get
            {
                return realData;
            }

            set
            {
                realData = value;
            }
        }

        public int Index
        {
            get
            {
                return index;
            }

            set
            {
                index = value;
            }
        }
    }
}