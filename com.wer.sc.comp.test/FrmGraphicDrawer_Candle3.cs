﻿using com.wer.sc.comp.graphic;
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
    public partial class FrmGraphicDrawer_Candle3 : Form
    {
        public FrmGraphicDrawer_Candle3()
        {
            InitializeComponent();
            GraphicDrawer_Candle drawer = new GraphicDrawer_Candle();            

            MockGraphicDataProvider dataProvider = new MockGraphicDataProvider();
            dataProvider.Code = "m05";
            dataProvider.Period = new KLinePeriod(KLinePeriod.TYPE_DAY, 1);
            dataProvider.EndIndex = 710;

            drawer.DataProvider = dataProvider;

            drawer.BindControl(this);
            //drawer.DrawGraph();
        }
    }
}
