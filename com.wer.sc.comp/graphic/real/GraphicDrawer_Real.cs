using com.wer.sc.comp.graphic.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic.real
{
    public class GraphicDrawer_Real : GraphicDrawer_Abstract
    {
        private PriceGraphicMapping priceMapping = new PriceGraphicMapping();

        public PriceGraphicMapping PriceMapping
        {
            get
            {
                priceMapping.DrawRect = DisplayRect;
                //priceMapping.PriceRect = GetPriceRectangle();
                return priceMapping;
            }
        }

        public override void DrawGraph(Graphics graphic)
        {

        }
    }
}
