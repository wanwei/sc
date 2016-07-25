using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class KLineChart
    {
        private KLineData data;

        private int index;

        public KLineData Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
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

        public String toString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Time).Append(",");
            sb.Append(Start).Append(",");
            sb.Append(High).Append(",");
            sb.Append(Low).Append(",");
            sb.Append(End).Append(",");
            sb.Append(Mount);
            //sb.Append(mount()).Append(",");
            //sb.Append(hold());
            return sb.ToString();
        }

        public KLineChart(KLineData data, int index)
        {
            this.Data = data;
            this.Index = index;
        }

        public float Start
        {
            get
            {
                return Data.arr_start[Index];
            }
        }

        public float High
        {
            get
            {
                return Data.arr_high[Index];
            }
        }

        public float Low
        {
            get
            {
                return Data.arr_low[Index];
            }
        }

        public float End
        {
            get
            {
                return Data.arr_end[Index];
            }
        }

        public float Mount
        {
            get
            {
                return Data.arr_mount[Index];
            }
        }

        public double Time
        {
            get
            {
                return Data.arr_time[Index];
            }
        }

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

        public Object clone()
        {
            return new KLineChart(Data, Index);
        }
    }
}
