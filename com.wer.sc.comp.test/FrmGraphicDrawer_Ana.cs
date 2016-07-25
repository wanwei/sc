using com.wer.sc.ana;
using com.wer.sc.ana.test.model;
using com.wer.sc.comp.graphic;
using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.test
{
    public partial class FrmGraphicDrawer_Ana : Form
    {
        public FrmGraphicDrawer_Ana()
        {
            InitializeComponent();            

            //MockGraphicDataProvider dataProvider = new MockGraphicDataProvider();
            //dataProvider.Code = "m05";
            //dataProvider.Period = new KLinePeriod(KLinePeriod.TYPE_DAY, 1);
            //dataProvider.EndIndex = 710;
            //drawer.DataProvider = dataProvider;

            KLineModelRunner runner = new KLineModelRunner(@"D:\SCDATA\CNFUTURES");
            runner.Code = "m05";
            //runner.Start = 20100725;
            //runner.End = 20141125;
            runner.Start = 20100725;
            runner.End = 20111125;
            runner.Period = new data.KLinePeriod(KLineTimeType.TYPE_DAY, 1);

            //KLineModel_Simple2 model = new KLineModel_Simple2();            
            KLineModel_Simple3 model = new KLineModel_Simple3();
            model.HLLen = 7;
            runner.Model = model;
            runner.run();

            KLineData data = runner.Data;
            MockGraphicDataProvider dataProvider = new MockGraphicDataProvider();
            dataProvider.Init(data);
            dataProvider.EndIndex = 200;

            GraphicDrawer_Candle drawer = new GraphicDrawer_Candle();
            drawer.DataProvider = dataProvider;
            drawer.drawer_chart.AddPolyLines(model.polyLines);
            drawer.drawer_chart.AddPoints(model.points);
            drawer.drawer_chart.AddPoints(model.pointLists);
            drawer.BindControl(this);

            //drawer.drawer_chart.AddPolyLines(model.polyLines);
            //drawer.DrawGraph();
        }
    }
}