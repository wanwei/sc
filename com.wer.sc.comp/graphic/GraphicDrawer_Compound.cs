using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDrawer_Compound : GraphicDrawer_Abstract
    {
        private List<GraphicDrawer_Abstract> drawers = new List<GraphicDrawer_Abstract>();

        private List<float> drawerPercent = new List<float>();

        public void AddGraph(GraphicDrawer_Abstract drawer, float percent)
        {
            drawers.Add(drawer);
            drawerPercent.Add(percent);
        }

        public override void DrawGraph(Graphics graphic)
        {
            float sum = 0;
            for (int i = 0; i < drawerPercent.Count; i++)
                sum += drawerPercent[i];

            int x = DisplayRect.X;
            int y = DisplayRect.Y;
            int width = DisplayRect.Width;
            for (int i = 0; i < drawers.Count; i++)
            {
                GraphicDrawer_Abstract drawer = drawers[i];
                float percent = drawerPercent[i];
                int height = (int)(percent / sum * DisplayRect.Height);
                drawer.SetDrawRect(new Rectangle(x, y, width, height));
                y += height;
                drawer.DrawGraph(graphic);
            }
        }
    }
}
