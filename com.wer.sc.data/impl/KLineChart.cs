﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class KLineChart : KLineChart_Abstract
    {
        private string code;

        private double time;

        private int date;

        private float start; //起始价        

        private float high; //最高价

        private float low; //最低价

        private float end; //收盘价

        private int mount;//成交量，单位是手

        private float money;

        private int hold;

        public KLineChart()
        {

        }

        public KLineChart(IKLineData data, int index)
        {
            this.code = data.Code;
            this.time = data.Arr_Time[index];
            this.start = data.Arr_Start[index];
            this.high = data.Arr_High[index];
            this.low = data.Arr_Low[index];
            this.end = data.Arr_End[index];
            this.mount = data.Arr_Mount[index];
            this.money = data.Arr_Money[index];
            this.hold = data.Arr_Hold[index];
        }

        public override string Code
        {
            get
            {
                return this.code;
            }
            set { this.code = value; }
        }

        public override double FullTime
        {
            get
            {
                return this.time;
            }
            set { this.time = value; }
        }


        //public override int Date
        //{
        //    get
        //    {
        //        return this.date;
        //    }

        //    set
        //    {
        //        this.date = value;
        //    }
        //}

        //public override double Time
        //{
        //    get
        //    {
        //        return FullTime - (int)FullTime;
        //    }
        //}

        public override float Start
        {
            get
            {
                return start;
            }
            set { this.start = value; }
        }

        public override float High
        {
            get
            {
                return high;
            }
            set { this.high = value; }
        }

        public override float Low
        {
            get
            {
                return low;
            }
            set { this.low = value; }
        }

        public override float End
        {
            get
            {
                return end;
            }
            set { this.end = value; }
        }

        public override int Mount
        {
            get
            {
                return mount;
            }
            set { this.mount = value; }
        }


        public override float Money
        {
            get
            {
                return money;
            }
            set { this.money = value; }
        }

        public override int Hold
        {
            get
            {
                return this.hold;
            }
            set { this.hold = value; }
        }
    }
}