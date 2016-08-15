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
        }

        public override float Start
        {
            get
            {
                return Data.Arr_Start[Index];
            }
        }

        public override float High
        {
            get
            {
                return Data.Arr_High[Index];
            }
        }

        public override float Low
        {
            get
            {
                return Data.Arr_Low[Index];
            }
        }

        public override float End
        {
            get
            {
                return Data.Arr_End[Index];
            }
        }

        public override int Mount
        {
            get
            {
                return Data.Arr_Mount[Index];
            }
        }

        public override double Time
        {
            get
            {
                return Data.Arr_Time[Index];
            }
        }


        public Object clone()
        {
            return new KLineChart_KLineData(Data, Index);
        }
    }
}
