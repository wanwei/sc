﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace com.wer.sc.utils.ui.proceed
{
    public partial class ControlDataProceed : UserControl
    {
        private bool isProceeding = false;

        private IDataProceed dataProceed;        

        public ControlDataProceed()
        {
            InitializeComponent();
            Form form = this.ParentForm;
            if (form != null)
                form.FormClosing += ControlDataProceed_FormClosing;
        }

        private void ControlDataProceed_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isProceeding)
            {
                DialogResult result = MessageBox.Show("正在执行进程，关闭窗口将停止执行，是否关闭", "确认关闭", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
                else
                {
                    if (dataProceed != null)
                        dataProceed.IsCancel = true;
                }
            }
        }

        public IDataProceed DataProceed
        {
            get
            {
                return dataProceed;
            }

            set
            {
                dataProceed = value;
            }
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            if (isProceeding)
            {
                MessageBox.Show("正在执行进程");
                return;
            }
            isProceeding = true;
            if (BeforeProceedStart != null)
                BeforeProceedStart();

            progressBar1.Value = 0;
            this.toolStripStatusLabel1.Text = "正在准备执行进程";

            Thread thread1 = new Thread(new ThreadStart(ProceedInternal));
            thread1.Start();
        }

        private void btStop_Click(object sender, EventArgs e)
        {
            if (!isProceeding)
            {
                MessageBox.Show("没有任何进程在执行");
                return;
            }
            isProceeding = false;
            if (BeforeProceedStop != null)
                BeforeProceedStop();
            dataProceed.IsCancel = true;
            UpdateStatusLabel("正在取消进程");
        }

        #region 执行

        private void ProceedInternal()
        {
            if (dataProceed == null)
                return;
            int totalProgressStep = 0;            
            List<IStep> steps = dataProceed.Prepare();
            for (int i = 0; i < steps.Count; i++)
            {
                totalProgressStep += steps[i].ProgressStep;
            }

            UpdateMaxProgress(totalProgressStep);

            for (int i = 0; i < steps.Count; i++)
            {
                if (dataProceed.IsCancel)
                {
                    UpdateStatusLabel("进程已经被取消");
                    return;
                }
                IStep step = steps[i];
                UpdateStatusLabel(step.StepDesc);
                step.Proceed();
                if (!DataProceed.IsCancel)
                    UpdateProgressStep(step.ProgressStep);
            }
            UpdateStatusLabel("进程执行完成");
        }

        #endregion

        #region 更新进度条和状态栏

        private void UpdateMaxProgress(int max)
        {
            if (progressBar1.InvokeRequired)
            {
                UpdateProgressInvokeCallback pi = new UpdateProgressInvokeCallback(this.UpdateMaxProgress);
                this.Invoke(pi, max);
            }
            else
            {
                progressBar1.Maximum = max;//设置最大长度值
                progressBar1.Value = 0;//设置当前值
                progressBar1.Step = 1;//设置没次增长多少                
            }
        }

        private delegate void UpdateProgressInvokeCallback(int max);

        private void UpdateProgressStep(int step)
        {
            if (progressBar1.InvokeRequired)
            {
                UpdateProgressInvokeCallback pi = new UpdateProgressInvokeCallback(this.UpdateProgressStep);
                this.Invoke(pi, step);
            }
            else
            {
                progressBar1.Value += step;
            }
        }

        private void UpdateStatusLabel(string txt)
        {
            if (this.statusStrip1.InvokeRequired)
            {
                UpdateStatusInvokeCallback md = new UpdateStatusInvokeCallback(this.UpdateStatusLabel);
                this.Invoke(md, new object[] { txt });
            }
            else
            {
                this.toolStripStatusLabel1.Text = txt;
            }
        }

        private delegate void UpdateStatusInvokeCallback(String txt);

        #endregion

        public event BeforeProceedStart BeforeProceedStart;

        public event BeforeProceedStop BeforeProceedStop;
    }

    public delegate void BeforeProceedStart();

    public delegate void BeforeProceedStop();
}
