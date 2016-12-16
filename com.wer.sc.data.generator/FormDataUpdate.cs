using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.generator
{
    public partial class FormDataUpdate : Form
    {
        private IPlugin_HistoryData plugin_HistoryData;

        public FormDataUpdate(IPlugin_HistoryData plugin_HistoryData)
        {
            InitializeComponent();
            this.plugin_HistoryData = plugin_HistoryData;

            this.tbDataCenter.Text = this.plugin_HistoryData.GetDataPath();
            this.controlDataProceed1.DataProceed = new DataProceed_DataGenerator(this.plugin_HistoryData, !rb_New.Checked);
        }
    }
}
