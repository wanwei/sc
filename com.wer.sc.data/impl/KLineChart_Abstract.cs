﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public abstract class KLineChart_Abstract : IKLineChart
    {
        public abstract string Code { get; set; }

        public abstract double Time { get; set; }

        public abstract float Start { get; set; }

        public abstract float High { get; set; }

        public abstract float Low { get; set; }

        public abstract float End { get; set; }

        public abstract int Mount { get; set; }

        public abstract float Money { get; set; }

        public abstract int Hold { get; set; }

        public float Height
        {
            get
            {
                return High - Low;
            }
        }

        public float TopShadow
        {
            get
            {
                return High - BlockHigh;
            }
        }

        public float BottomShadow
        {
            get
            {
                return BlockLow - Low;
            }
        }

        /**
         * 得到当日中间价
         * @return
         */
        public float Middle
        {
            get
            {
                return (High + Low) / 2;
            }
        }

        /**
         * 得到开盘收盘的中间价
         * @return
         */
        public float BlockMiddle
        {
            get
            {
                return (BlockHigh + BlockLow) / 2;
            }
        }

        /**
         * 得到开盘收盘价格低的那个
         * @return
         */
        public float BlockLow
        {
            get
            {
                return Start < End ? Start : End;
            }
        }

        /**
         * 得到开盘收盘价格高的那个
         * @return
         */
        public float BlockHigh
        {
            get
            {
                return Start > End ? Start : End;
            }
        }

        /**
         * 得到开盘收盘价格差
         * @return
         */
        public float BlockHeight
        {
            get
            {
                return BlockHigh - BlockLow;
            }
        }

        public bool isRed()
        {
            return End >= Start;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Code).Append(",");
            sb.Append(Time).Append(",");
            sb.Append(Start).Append(",");
            sb.Append(High).Append(",");
            sb.Append(Low).Append(",");
            sb.Append(End).Append(",");
            sb.Append(Mount).Append(",");
            sb.Append(Money).Append(",");
            sb.Append(Hold);
            return sb.ToString();
        }
    }
}
