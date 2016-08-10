using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.ana;
using com.wer.sc.data;
using com.wer.sc.comp.graphic;

namespace com.wer.sc.comp.ana
{
    public partial class AnaComponent : UserControl
    {
        private String dataPath;

        private DataReaderFactory fac;

        private GraphicDataProvider_Default dataProvider;

        private AnaDrawer_KLine drawer;

        public AnaComponent()
        {
            InitializeComponent();
            //this.drawer = new AnaDrawer_KLine()            
            //drawer.Run("m13", 20100101, 20150101, new KLinePeriod(KLinePeriod.TYPE_DAY, 1), model);
        }

        public string DataPath
        {
            get
            {
                return dataPath;
            }

            set
            {
                if (dataPath != value)
                {
                    dataPath = value;

                    this.fac = new DataReaderFactory(DataPath);
                    this.dataProvider = new GraphicDataProvider_Default(fac);
                    if (this.drawer != null)
                        this.drawer.UnBind();
                    this.drawer = new AnaDrawer_KLine(fac, dataProvider);
                    this.drawer.Bind(this);
                }
            }
        }

        public AnaDrawer_KLine Drawer
        {
            get
            {
                return drawer;
            }
        }
    }
}