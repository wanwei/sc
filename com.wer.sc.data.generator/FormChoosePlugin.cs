using com.wer.sc.data.update;
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
    public partial class FormChoosePlugin : Form
    {
        private String providerName;

        private DataMgr providerDataMgr;

        public string ProviderName
        {
            get
            {
                return providerName;
            }         
        }

        public DataMgr ProviderDataMgr
        {
            get
            {
                return providerDataMgr;
            }            
        }


        public FormChoosePlugin()
        {
            InitializeComponent();
            this.providerDataMgr = new DataMgr();
            List<DataProviderWrap> providers = ProviderDataMgr.GetProviders();
            for (int i = 0; i < providers.Count; i++)
            {
                DataProviderWrap provider = providers[i];
                cbProvider.Items.Add(provider.GetName());
            }
            cbProvider.SelectedIndex = 0;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            this.providerName = cbProvider.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
