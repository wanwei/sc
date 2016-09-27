using com.wer.sc.data;
using com.wer.sc.data.store;
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
    public partial class FrmDataBrowser : Form
    {
        public FrmDataBrowser()
        {
            InitializeComponent();
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                tbData.Clear();
                tbPath.Clear();

                String fileName = fileDialog.FileName;
                tbPath.Text = fileName;                
                if (fileName.EndsWith("kline"))
                {
                    KLineDataStore store = new KLineDataStore(fileName);
                    KLineData data = store.LoadAll();
                    int showLen = data.Length;
                    showLen = showLen > 5000 ? 5000 : showLen;
                    StringBuilder sb = new StringBuilder();
                    for(int i = 0; i < showLen; i++)
                    {
                        data.BarPos = i;
                        sb.Append(data.ToString()).Append("\r\n");
                    }
                    tbData.Text = sb.ToString();
                }
                else if (fileName.EndsWith("tick"))
                {
                    TickDataStore store = new TickDataStore(fileName);
                    TickData data = store.Load();
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < data.Length; i++)
                    {
                        data.BarPos = i;
                        sb.Append(data.ToString()).Append("\r\n");
                    }
                    tbData.Text = sb.ToString();
                }
            }
        }
    }
}
