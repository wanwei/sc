using com.wer.sc.comp.graphic.real;
using com.wer.sc.comp.graphic.utils;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDrawer_Main : GraphicDrawer_Switch
    {
        private GraphicDataProvider_Main dataProvider;

        private GraphicDrawer_Candle drawer_Candle;
        private GraphicDrawer_Real drawer_Real;

        public GraphicDataProvider_Main DataProvider
        {
            get
            {
                return dataProvider;
            }

            set
            {
                dataProvider = value;
                drawer_Candle.DataProvider = dataProvider.DataProvider_Candle;
                drawer_Real.DataProvider = dataProvider.DataProvider_Real;
            }
        }

        public GraphicDrawer_Main()
        {
            drawer_Candle = new GraphicDrawer_Candle();
            drawer_Real = new GraphicDrawer_Real();
      
            this.Drawers.Add(drawer_Candle);
            this.Drawers.Add(drawer_Real);
            this.Switch(0);
        }

        public override void BindControl(Control control)
        {
            base.BindControl(control);
            drawer_Candle.BindControl(control);
            drawer_Real.BindControl(control);
            control.KeyUp += Control_KeyUp;
        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                if (this.CurrentIndex == 0)
                {
                    CrossHairDrawer currentdrawer = this.drawer_Candle.CrossHairDrawer;
                    bool showCross = currentdrawer.ShowCrossHair;
                    if (showCross)
                    {
                        this.drawer_Real.CrossHairDrawer.ShowCrossHair = true;
                        this.drawer_Real.CrossHairDrawer.ChangeCrossPoint(currentdrawer.CrossPoint);
                    }
                    else
                    {
                        this.drawer_Real.CrossHairDrawer.ShowCrossHair = false;
                    }
                    this.Switch(1);
                }
                else
                {
                    CrossHairDrawer currentdrawer = this.drawer_Real.CrossHairDrawer;
                    bool showCross = currentdrawer.ShowCrossHair;
                    if (showCross)
                    {
                        this.drawer_Candle.CrossHairDrawer.ShowCrossHair = true;
                        this.drawer_Candle.CrossHairDrawer.ChangeCrossPoint(currentdrawer.CrossPoint);
                    }
                    else
                    {
                        this.drawer_Candle.CrossHairDrawer.ShowCrossHair = false;
                    }
                    this.Switch(0);
                }
                this.DrawGraph();
            }
        }

        public override void UnBindControl()
        {
            base.UnBindControl();
        }
    }
}
