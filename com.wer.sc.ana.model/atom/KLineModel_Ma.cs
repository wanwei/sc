using com.wer.sc.data;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana.model.atom
{
    [ZbRegisterAttribute("均线")]
    public class KLineModel_Ma : Plugin_KLineModel
    {
        [ModelLines("ma5", "#111111", 1)]
        public float[] ma5;

        [ModelLines("ma10", "#0000FF", 1)]
        public float[] ma10;

        [ModelLines("ma20", "#FF0000", 1)]
        public float[] ma20;

        [ModelLines("ma40", "#00FF00", 1)]
        public float[] ma40;

        public override void init(String code, IKLineData data)
        {
            base.init(code, data);
            ma5 = new float[data.Length];
            ma10 = new float[data.Length];
            ma20 = new float[data.Length];
            ma40 = new float[data.Length];
        }

        public override void Loop()
        {
            ma5[BarPos] = Ma(Arr_End, 5);
            ma10[BarPos] = Ma(Arr_End, 10);
            ma20[BarPos] = Ma(Arr_End, 20);
            ma40[BarPos] = Ma(Arr_End, 40);
        }
    }
}