using com.wer.sc.plugin;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.generator
{
    public class DataProceed_DataGenerator : IDataProceed
    {
        private bool isCancel;
        public bool IsCancel
        {
            get
            {
                return isCancel;
            }

            set
            {
                this.isCancel = value;
            }
        }
        private StepPreparer preparer;
        public DataProceed_DataGenerator(IPlugin_HistoryData plugin_HistoryData, bool isFillUp)
        {
            this.preparer = new StepPreparer(plugin_HistoryData, isFillUp);
        }

        public List<IStep> Prepare()
        {
            return preparer.GetAllSteps();
        }
    }
}