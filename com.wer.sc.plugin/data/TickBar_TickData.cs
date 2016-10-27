using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// ITickBar接口的实现类
    /// 该类是只读的，用来从TickData里获取数据
    /// </summary>
    public class TickBar_TickData : ITickBar
    {
        private ITickData data;

        private int index;

        public TickBar_TickData(ITickData data, int index)
        {
            this.data = data;
            this.index = index;
        }

        public string Code
        {
            get { return data.Code; }
        }

        public int Add
        {
            get
            {
                return data.Arr_Add[index];
            }
        }

        public int BuyMount
        {
            get
            {
                return data.Arr_BuyMount[index];
            }
        }

        public float BuyPrice
        {
            get
            {
                return data.Arr_BuyPrice[index];
            }
        }

        public int Hold
        {
            get
            {
                return data.Arr_Hold[index];
            }
        }

        public bool IsBuy
        {
            get
            {
                return data.Arr_IsBuy[index];
            }
        }

        public int Mount
        {
            get
            {
                return data.Arr_Mount[index];
            }
        }

        public float Price
        {
            get
            {
                return data.Arr_Price[index];
            }
        }

        public int SellMount
        {
            get
            {
                return data.Arr_SellMount[index];
            }
        }

        public float SellPrice
        {
            get
            {
                return data.Arr_SellPrice[index];
            }
        }

        public double Time
        {
            get
            {
                return data.Arr_Time[index];
            }
        }

        public int TotalMount
        {
            get
            {
                return data.Arr_TotalMount[index];
            }
        }
    }
}
