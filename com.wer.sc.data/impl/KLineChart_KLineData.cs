using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class KLineChart_KLineData : KLineChart_Abstract
    {
        private IKLineData data;

        private int index;

        public IKLineData Data
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

        public KLineChart_KLineData(IKLineData data, int index)
        {
            this.Data = data;
            this.Index = index;
        }

        public override string Code
        {
            get
            {
                return data.Code;
            }
            set { throw new NotImplementedException(); }
        }

        public override double Time
        {
            get
            {
                return Data.Arr_Time[Index];
            }
            set { throw new NotImplementedException(); }
        }

        public override float Start
        {
            get
            {
                return Data.Arr_Start[Index];
            }
            set { throw new NotImplementedException(); }
        }

        public override float High
        {
            get
            {
                return Data.Arr_High[Index];
            }
            set { throw new NotImplementedException(); }
        }

        public override float Low
        {
            get
            {
                return Data.Arr_Low[Index];
            }
            set { throw new NotImplementedException(); }
        }

        public override float End
        {
            get
            {
                return Data.Arr_End[Index];
            }
            set { throw new NotImplementedException(); }
        }

        public override int Mount
        {
            get
            {
                return Data.Arr_Mount[Index];
            }
            set { throw new NotImplementedException(); }
        }

        public override float Money
        {
            get
            {
                return data.Arr_Money[index];
            }
            set { throw new NotImplementedException(); }
        }

        public override int Hold
        {
            get
            {
                return data.Arr_Hold[index];
            }
            set { throw new NotImplementedException(); }
        }

        public Object clone()
        {
            return new KLineChart_KLineData(Data, Index);
        }
    }
}
