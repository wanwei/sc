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

namespace com.wer.sc.data.cnfutures.generator
{
    public partial class FormDataGenerator : Form
    {
        private bool isAdjusted = false;
        private DataGenerator_TickData generator;
        public FormDataGenerator()
        {
            InitializeComponent();
            this.tbSrcPath.Text = @"F:\FUTURES\CSV\TICK";
            this.tbAdjustedPath.Text = @"F:\FUTURES\CSV\TICKADJUSTED";
            this.tbVariety.Text = "M";
        }

        private void btAdjust_Click(object sender, EventArgs e)
        {
            this.isAdjusted = true;
            String srcPath = this.tbSrcPath.Text.Trim();
            String adjustedPath = this.tbAdjustedPath.Text.Trim();
            String strVarieties = this.tbVariety.Text.Trim();
            String[] varieties = strVarieties == "" ? null : strVarieties.Split(',');

            progressBar1.Value = 0;
            this.toolStripStatusLabel1.Text = "正在准备更新数据";
            generator = new DataGenerator_TickData(srcPath, adjustedPath, varieties);
            generator.AfterPrepared = new AfterPreparedHandler(AfterPrepared);
            generator.AfterGeneratedPeriod = new AfterGeneratedPeriodHandler(AfterGeneratedPeriod);
            generator.AfterGenerated = new AfterGeneratedHandler(AfterGenerated);
            generator.Generate();
        }

        public void AfterPrepared(GenerateInfo generateInfo)
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

        public void AfterGeneratedPeriod(GeneratedPeriodArgs generateInfo)
        {
            if (this.progressBar1.InvokeRequired)
            {
                AfterGeneratedPeriodHandler md = new AfterGeneratedPeriodHandler(this.AfterGeneratedPeriod);
                this.Invoke(md, new object[] { generateInfo });
            }
            else
            {
                this.progressBar1.Value += progressBar1.Step;
                this.toolStripStatusLabel1.Text = "正在更新" + generateInfo.variety + ":" + generateInfo.nextStartDate + "-" + generateInfo.nextEndDate;
            }
        }

        public void AfterGenerated(GeneratedArgs args)
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
                this.isAdjusted = false;
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            generator.IsCancel = true;
            this.isAdjusted = false;
        }

        private void FormAdjust_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isAdjusted)
            {
                DialogResult result = MessageBox.Show("正在更新数据，关闭窗口将停止更新，是否关闭", "确认关闭", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    generator.IsCancel = true;
                }
            }
        }
    }
}