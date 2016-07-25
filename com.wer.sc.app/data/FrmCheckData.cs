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

namespace com.wer.sc.app.data
{
    public partial class FrmCheckData : Form
    {
        DataReaderFactory fac;
        public FrmCheckData()
        {
            InitializeComponent();
            fac = new DataReaderFactory(@"D:\SCDATA\CNFUTURES");
        }

        private void btShowData_Click(object sender, EventArgs e)
        {
            TickData data = fac.TickDataReader.GetTickData(tbCode.Text.Trim(), int.Parse(tbDate.Text.Trim()));
            tbData.Clear();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < data.Length; i++)
            {
                data.BarPos = i;
                sb.Append(data.ToString() + "\r\n");
            }
            tbData.Text = sb.ToString();
        }
    }
}
