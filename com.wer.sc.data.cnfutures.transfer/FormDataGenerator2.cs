using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.cnfutures.generator
{
    public partial class FormDataGenerator2 : Form
    {
        public FormDataGenerator2()
        {
            InitializeComponent();

            this.tbSrcPath.Text = @"F:\FUTURES\CSV\TICK";
            this.tbAdjustedPath.Text = @"F:\FUTURES\CSV\TICKADJUSTED";
            this.tbVariety.Text = "M";
            this.rb_New.Checked = true;
            controlDataProceed1.BeforeProceedStart += ControlDataProceed1_BeforeProceedStart;
        }

        private void ControlDataProceed1_BeforeProceedStart()
        {
            string srcDataPath = tbSrcPath.Text;
            string pluginSrcDataPath = tbAdjustedPath.Text;
            //StepPreparer preparer = new StepPreparer(srcDataPath, pluginSrcDataPath);
            //List<IStep> steps = preparer.GetAllSteps();
            DataGeneratorProceed proceed = new DataGeneratorProceed(srcDataPath, pluginSrcDataPath, !this.rb_New.Checked);
            controlDataProceed1.DataProceed = proceed;
        }
    }  
}
