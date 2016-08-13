using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class KLineChart_KLineData : KLineChart_Abstract
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

        public KLineChart_KLineData(KLineData data, int index)
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
                return Data.arr_start[Index];
            }
        }

        public override float High
        {
            get
            {
                return Data.arr_high[Index];
            }
        }

        public override float Low
        {
            get
            {
                return Data.arr_low[Index];
            }
        }

        public override float End
        {
            get
            {
                return Data.arr_end[Index];
            }
        }

        public override int Mount
        {
            get
            {
                return Data.arr_mount[Index];
            }
        }

        public override double Time
        {
            get
            {
                return Data.arr_time[Index];
            }
        }


        public Object clone()
        {
            return new KLineChart_KLineData(Data, Index);
        }
    }
}
