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
    public partial class FormGenerator : Form
    {
        public FormGenerator()
        {
            InitializeComponent();
        }

        public void LoadDataCenter(String name)
        {

        }

        private void Load()
        {
            DataMgr mgr = new DataMgr();
            List<DataProviderWrap> providers = mgr.GetProviders();
            for (int i = 0; i < providers.Count; i++)
            {
                DataProviderWrap provider = providers[i];
                //ToolStripItem item = this.menuItemDataCenter.DropDownItems.Add(provider.GetName());
                //item.Click += Item_Click;
            }
            //if (providers.Count > 0)
            //    LoadProvider(providers[0]);
        }

        private void LoadProvider(DataProviderWrap provider)
        {
            //if (provider == null)
            //{
            //    this.lbDataPath.Text = "";
            //}
            //else
            //{
            //    this.currentProviderName = provider.GetName();
            //    this.lbDataPath.Text = provider.GetDataPath();
            //    List<CodeInfo> currentCodes = provider.GetCurrentCodes();
            //    LoadCodes(currentCodes, listBoxCodes);
            //    List<CodeInfo> updateCodes = provider.GetUpdateCodes();
            //    LoadCodes(updateCodes, listBox2);
            //}
        }
    }
}
