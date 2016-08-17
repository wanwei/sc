using System;
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

        private float start; //起始价        

        private float high; //最高价

        private float low; //最低价

        private float end; //收盘价

        private int mount;//成交量，单位是手

        private float money;

        private int hold;

        public override string Code
        {
            get
            {
                return this.code;
            }
        }

        public void SetCode(String code)
        {
            this.code = code;
        }

        public override double Time
        {
            get
            {
                return this.time;
            }
        }

        public void SetTime(double time)
        {
            this.time = time;
        }

        public override float Start
        {
            get
            {
                return start;
            }
        }

        public void SetStart(float start)
        {
            this.start = start;
        }

        public override float High
        {
            get
            {
                return high;
            }
        }

        public void SetHigh(float high)
        {
            this.high = high;
        }

        public override float Low
        {
            get
            {
                return low;
            }
        }

        public void SetLow(float low)
        {
            this.low = low;
        }

        public override float End
        {
            get
            {
                return end;
            }
        }

        public void SetEnd(float end)
        {
            this.end = end;
        }

        public override int Mount
        {
            get
            {
                return mount;
            }
        }

        public void SetMount(int mount)
        {
            this.mount = mount;
        }


        public override float Money
        {
            get
            {
                return money;
            }
        }

        public void SetMoney(float money)
        {
            this.money = money;
        }

        public override int Hold
        {
            get
            {
                return this.hold;
            }
        }

        public void SetHold(int hold)
        {
            this.hold = hold;
        }
    }
}