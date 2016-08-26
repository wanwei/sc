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

        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }

            set
            {
                currentIndex = value;
            }
        }

        public void Switch(int index)
        {
            for (int i = 0; i < drawers.Count; i++)
            {
                if (i == index)
                    Drawers[i].IsEnable = true;
                else
                    Drawers[i].IsEnable = false;
            }
            this.CurrentIndex = index;            
        }

        public override void DrawGraph(Graphics graphic)
        {
            GraphicDrawer_Abstract drawer = drawers[CurrentIndex];
            Rectangle rect = control.DisplayRectangle;
            drawer.SetDrawRect(rect);
            drawer.DrawGraph(graphic);            
            //drawer.DrawGraph();
        }
    }
}