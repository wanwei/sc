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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btCandle_Click(object sender, EventArgs e)
        {
            FrmGraphicDrawer_Candle frm = new FrmGraphicDrawer_Candle();
            frm.ShowDialog();
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            FrmTest frm = new FrmTest();
            frm.ShowDialog();
        }

        private void btCandle2_Click(object sender, EventArgs e)
        {
            FrmGraphicDrawer_Candle2 frm = new FrmGraphicDrawer_Candle2();
            frm.ShowDialog();
        }

        private void btAna_Click(object sender, EventArgs e)
        {
            FrmGraphicDrawer_Ana frm = new FrmGraphicDrawer_Ana();
            frm.ShowDialog();
        }

        private void btReal_Click(object sender, EventArgs e)
        {
            
        }
    }
}
