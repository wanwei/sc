using com.wer.sc.data;
using com.wer.sc.data.update;
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

namespace com.wer.sc.app.data
{
    public partial class FrmDataMgr : Form
    {
        private DataMgr mgr;

        private String currentProviderName;

        public FrmDataMgr()
        {
            InitializeComponent();
            this.mgr = new DataMgr();
            List<DataProviderWrap> providers = mgr.GetProviders();
            for (int i = 0; i < providers.Count; i++)
            {
                DataProviderWrap provider = providers[i];
                ToolStripItem item = this.menuItemDataCenter.DropDownItems.Add(provider.GetName());
                item.Click += Item_Click;
            }
            if (providers.Count > 0)
                LoadProvider(providers[0]);
        }

        private void Item_Click(object sender, EventArgs e)
        {
            String text = ((ToolStripItem)sender).Text;
            DataProviderWrap provider = mgr.GetProvider(text);
            LoadProvider(provider);
        }

        private void LoadProvider(DataProviderWrap provider)
        {
            if (provider == null)
            {
                this.lbDataPath.Text = "";
            }
            else
            {
                this.currentProviderName = provider.GetName();
                this.lbDataPath.Text = provider.GetDataPath();
                List<CodeInfo> currentCodes = provider.GetCurrentCodes();
                LoadCodes(currentCodes, listBoxCodes);
                List<CodeInfo> updateCodes = provider.GetUpdateCodes();
                LoadCodes(updateCodes, listBox2);
            }
        }

        private void LoadCodes(List<CodeInfo> currentCodes, ListBox box)
        {
            box.Items.Clear();
            for (int i = 0; i < currentCodes.Count; i++)
            {
                CodeInfo c = currentCodes[i];
                box.Items.Add(c);
            }
        }

        private void listBoxCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodeInfo code = (CodeInfo)((ListBox)sender).SelectedItem;
            LoadCodeUpdateInfo(code);
        }

        private void LoadCodeUpdateInfo(CodeInfo code)
        {
            DataReaderFactory fac = GetCurrentDataProvider().GetFactory();
            List<int> tickDates = GetCurrentDataUpdate().Update_Tick.GetUpdatedDates(code.Code, fac);
            if (tickDates.Count == 0)
            {
                lbTickStart.Text = "--------";
                lbTickEnd.Text = "--------";
            }
            else
            {
                lbTickStart.Text = tickDates[0].ToString();
                lbTickEnd.Text = tickDates[tickDates.Count - 1].ToString();
            }

            DataUpdate_KLine update_KLine = GetCurrentDataUpdate().Update_KLine;
            ShowDate(lbMinuteStart, update_KLine.GetUpdateFirstTime(code.Code, fac, new KLinePeriod(KLineTimeType.MINUTE, 1)));
            ShowDate(lbMinuteEnd, update_KLine.GetUpdateLastDate(code.Code, fac, new KLinePeriod(KLineTimeType.MINUTE, 1)));

            ShowDate(lb15MinuteStart, update_KLine.GetUpdateFirstTime(code.Code, fac, new KLinePeriod(KLineTimeType.MINUTE, 15)));
            ShowDate(lb15MinuteEnd, update_KLine.GetUpdateLastDate(code.Code, fac, new KLinePeriod(KLineTimeType.MINUTE, 15)));

            ShowDate(lbHourStart, update_KLine.GetUpdateFirstTime(code.Code, fac, new KLinePeriod(KLineTimeType.HOUR, 1)));
            ShowDate(lbHourEnd, update_KLine.GetUpdateLastDate(code.Code, fac, new KLinePeriod(KLineTimeType.HOUR, 1)));

            ShowDate(lbDayStart, update_KLine.GetUpdateFirstTime(code.Code, fac, new KLinePeriod(KLineTimeType.DAY, 1)));
            ShowDate(lbDayEnd, update_KLine.GetUpdateLastDate(code.Code, fac, new KLinePeriod(KLineTimeType.DAY, 1)));
        }

        private void ShowDate(Label lb, int date)
        {
            if (date < 0)
                lb.Text = "--------";
            else
                lb.Text = date.ToString();
        }

        private void btUpdateCode_Click(object sender, EventArgs e)
        {
            if (currentProviderName == null)
                return;
            GetCurrentDataUpdate().UpdateCodeInfos();
        }

        private DataUpdate_Old GetCurrentDataUpdate()
        {
            return mgr.GetDataUpdate(currentProviderName);
        }

        private DataProviderWrap GetCurrentDataProvider()
        {
            return mgr.GetProvider(currentProviderName);
        }

        private void menuItemUpdateByCodes_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection objs = listBoxCodes.SelectedItems;
            for (int i = 0; i < objs.Count; i++)
            {
                CodeInfo code = (CodeInfo)objs[i];
                DataUpdate_Old dataUpdater = GetCurrentDataUpdate();
                dataUpdater.UpdateTick(code.Code);
                dataUpdater.UpdateKLine(code.Code);
            }
        }

        private void MenuItemCheckData_Click(object sender, EventArgs e)
        {
            //FrmCheckData frmCheck = new FrmCheckData();
            //frmCheck.ShowDialog();
            FrmDataBrowser frm = new FrmDataBrowser();
            frm.ShowDialog();
        }        
    }
}
