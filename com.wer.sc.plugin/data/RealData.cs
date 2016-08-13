using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class RealData
    {
        public String code;

        public int date;

        /// <summary>
        /// 昨日收盘
        /// </summary>
        public float yesterdayEnd;

        /// <summary>
        /// 全日时间
        /// </summary>
        public double[] arr_time;

        /// <summary>
        /// 全日价格
        /// </summary>
        public float[] arr_price;

        ///// <summary>
        ///// 全日上涨价格
        ///// </summary>
        //public float[] arr_uprange;

        ///// <summary>
        ///// 全日涨幅
        ///// </summary>
        //public float[] arr_uppercent;

        /// <summary>
        /// 全日成交
        /// </summary>
        public int[] arr_mount;

        /// <summary>
        /// 全天持仓数据
        /// </summary>
        public int[] arr_hold;

        private int barPos;

        public RealData(int date, float yesterdayEnd, int length)
        {
            this.date = date;
            this.yesterdayEnd = yesterdayEnd;
            arr_time = new double[length];
            arr_price = new float[length];
            arr_mount = new int[length];
            arr_hold = new int[length];
        }

        public int BarPos
        {
            get
            {
                return barPos;
            }

            set
            {
                barPos = value;
            }
        }

        public double FullTime
        {
            get
            {
                return arr_time[barPos];
            }
        }

        public int Date
        {
            get { return (int)arr_time[barPos]; }
        }

        public double Time
        {
            get
            {
                return FullTime - Date;
            }
        }

        public float Price
        {
            get
            {
                return arr_price[barPos];
            }
        }

        public float UpRange
        {
            get
            {
                float p = Price;
                return (float)Math.Round(p - yesterdayEnd, 2);
            }
        }

        public float UpPerncet
        {
            get
            {
                float p = Price;
                return (float)Math.Round((p - yesterdayEnd) / p * 100, 2);
            }
        }

        public int Mount
        {
            get
            {
                return arr_mount[BarPos];
            }
        }

        public int Hold
        {
            get
            {
                return arr_hold[BarPos];
            }
        }

        public int Length
        {
            get
            {
                return arr_price.Length;
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(FullTime).Append(",");
            sb.Append(Price).Append(",");
            sb.Append(UpRange).Append(",");
            sb.Append(UpPerncet).Append(",");
            sb.Append(Mount).Append(",");
            sb.Append(Hold);
            return sb.ToString();
        }
    }
}
