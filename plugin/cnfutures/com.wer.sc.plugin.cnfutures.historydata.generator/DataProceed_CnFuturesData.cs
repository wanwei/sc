using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    /// <summary>
    /// 中国期货数据的执行过程
    /// </summary>
    public class DataProceed_CnFuturesData : IDataProceed
    {
        private Boolean isCancel;
        public bool IsCancel
        {
            get
            {
                return this.isCancel;
            }

            set
            {
                this.isCancel = value;
            }
        }

        private StepPreparer stepPreparer;

        public DataProceed_CnFuturesData(string srcDataPath, string pluginSrcDataPath, bool updateFillUp)
        {
            this.stepPreparer = new StepPreparer(srcDataPath, pluginSrcDataPath, updateFillUp);
        }

        public List<IStep> Prepare()
        {
            return this.stepPreparer.GetAllSteps();
        }
    }
}