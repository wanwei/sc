using com.wer.sc.data.update;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.generator
{
    public partial class FormGenerator : Form
    {
        private String providerName;

        private DataMgr providerMgr;

        private DataProviderWrap dataProvider;

        public FormGenerator()
        {
            InitializeComponent();
        }

        public void LoadDataCenter(String name, DataMgr mgr)
        {
            this.providerName = name;
            this.providerMgr = mgr;
            this.Text = "数据更新：" + name;
            this.dataProvider = providerMgr.GetProvider(name);
            this.tbPathDataCenter.Text = dataProvider.GetDataPath();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            DataUpdate generator = new DataUpdate(dataProvider);
            generator.AfterPrepared = new AfterPreparedHandler(AfterPrepared);
            generator.AfterGeneratedPeriod = new AfterGeneratedPeriodHandler(AfterGeneratedPeriod);
            generator.AfterGenerated = new AfterGeneratedHandler(AfterGenerated);
            generator.Update();
        }        

        private void GenerateInternal()
        {
            
        }

        private void AfterPrepared(UpdateInfo generateInfo)
        {
            int cnt = generateInfo.GetPeriodCount();
            UpdateProgressPrepare(cnt);
        }

        private void UpdateProgressPrepare(int max)
        {
            if (progressBar1.InvokeRequired)
            {
                PrepareInvokeCallback pi = new PrepareInvokeCallback(this.UpdateProgressPrepare);
                this.Invoke(pi, max);
            }
            else
            {
                progressBar1.Maximum = max;//设置最大长度值
                progressBar1.Value = 0;//设置当前值
                progressBar1.Step = 1;//设置没次增长多少
                toolStripStatusLabel1.Text = "准备完成，开始更新";
            }
        }

        private delegate void PrepareInvokeCallback(int max);

        public void AfterGeneratedPeriod(UpdatedPeriodArgs generateInfo)
        {
            if (this.progressBar1.InvokeRequired)
            {
                AfterGeneratedPeriodHandler md = new AfterGeneratedPeriodHandler(this.AfterGeneratedPeriod);
                this.Invoke(md, new object[] { generateInfo });
            }
            else
            {
                this.progressBar1.Value += progressBar1.Step;
                String txt = generateInfo.Description;
                txt = txt == "" ? "正在更新" + generateInfo.code + ":" + generateInfo.nextStartDate + "-" + generateInfo.nextEndDate : txt;
                this.toolStripStatusLabel1.Text = txt;
            }
        }

        public void AfterGenerated(UpdatedArgs args)
        {
            if (this.progressBar1.InvokeRequired)
            {
                AfterGeneratedHandler md = new AfterGeneratedHandler(this.AfterGenerated);
                this.Invoke(md, new object[] { args });
            }
            else
            {
                this.progressBar1.Value = this.progressBar1.Maximum;
                this.toolStripStatusLabel1.Text = "更新完成";
                //this.isAdjusted = false;
            }
        }

        private void btStop_Click(object sender, EventArgs e)
        {

        }
    }
}