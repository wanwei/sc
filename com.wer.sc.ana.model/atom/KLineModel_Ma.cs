using com.wer.sc.data;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana.model.atom
{
    public class KLineModel_Ma : KLineModel
    {
        public float[] ma5;

        public float[] ma10;

        public float[] ma20;

        public float[] ma40;

        public override void init(String code, KLineData data)
        {
            base.init(code, data);
            ma5 = new float[data.Length];
            ma10 = new float[data.Length];
            ma20 = new float[data.Length];
            ma40 = new float[data.Length];
        }

        public override void Loop()
        {
            ma5[BarPos] = ma(Arr_End, 5);
            ma10[BarPos] = ma(Arr_End, 10);
            ma20[BarPos] = ma(Arr_End, 20);
            ma40[BarPos] = ma(Arr_End, 40);
        }
    }
}