using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDrawer_Switch : GraphicDrawer_Abstract
    {
        private List<GraphicDrawer_Abstract> drawers = new List<GraphicDrawer_Abstract>();

        public List<GraphicDrawer_Abstract> Drawers
        {
            get
            {
                return drawers;
            }
        }

        private int currentIndex;

        public void Switch(int index)
        {
            this.currentIndex = index;
        }

        public override void DrawGraph(Graphics graphic)
        {
            GraphicDrawer_Abstract drawer = drawers[currentIndex];
            Rectangle rect = control.DisplayRectangle;
            drawer.SetDrawRect(rect);
            drawer.DrawGraph(graphic);
        }
    }
}