using com.wer.sc.data;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.ana.test.model
{
    public class KLineModel_Simple2 : KLineModel
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

        public override void ModelEnd()
        {
            base.ModelEnd();
            AddPolyLine(new comp.graphic.PolyLine(ma5, Color.Red));
            AddPolyLine(new comp.graphic.PolyLine(ma10, Color.Blue));
            AddPolyLine(new comp.graphic.PolyLine(ma20, Color.Green));
            AddPolyLine(new comp.graphic.PolyLine(ma40, Color.Gray));
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
