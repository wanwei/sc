using com.wer.sc.plugin.cnfutures.historydata.generator.Properties;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    /// <summary>
    /// 更新所有的股票或期货信息
    /// 股票或期货信息是放在Resources里
    /// </summary>
    public class Step_CodeInfo : IStep
    {
        private string pluginSrcDataPath;

        public Step_CodeInfo(string pluginSrcDataPath)
        {
            this.pluginSrcDataPath = pluginSrcDataPath;
        }

        public int ProgressStep
        {
            get
            {
                return 1;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新期货信息";
            }
        }

        public string Proceed()
        {
            File.WriteAllText(pluginSrcDataPath + "\\codes.csv", Resources.codes);
            File.WriteAllText(pluginSrcDataPath + "\\catelogs.csv", Resources.catelogs);
            return "期货信息更新完成";
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}
