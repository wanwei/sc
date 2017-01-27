using com.wer.sc.data.store;
using com.wer.sc.plugin;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.generator
{
    /// <summary>
    /// 更新所有的股票或期货信息
    /// 股票或期货信息是放在Resources里
    /// </summary>
    public class Step_CodeInfo : IStep
    {
        private CodeStore codeStore;

        private List<CodeInfo> codes;

        public Step_CodeInfo(IPlugin_HistoryData historyData, DataPathUtils utils)
        {
            this.codes = historyData.GetCodes();
            this.codeStore = new CodeStore(utils.GetCodePath());
        }

        public int ProgressStep
        {
            get
            {
                return 5;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新品种信息";
            }
        }

        public List<CodeInfo> Codes
        {
            get
            {
                return codes;
            }            
        }

        public string Proceed()
        {
            codeStore.Save(codes);
            return "期货信息更新完成";
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}
