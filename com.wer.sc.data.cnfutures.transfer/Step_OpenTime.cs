using com.wer.sc.data.cnfutures.generator.Properties;
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
    /// 开盘时间
    /// </summary>
    public class Step_OpenTime : IStep
    {
        private string configPath;

        public Step_OpenTime(string configPath)
        {
            this.configPath = configPath;
        }

        public int ProgressStep
        {
            get
            {
                return 3;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新开盘时间";
            }
        }

        public string Proceed()
        {
            File.WriteAllText(configPath + "\\opentime.config", Resources.opentime);
            return "期货信息更新完成";
        }
        public override string ToString()
        {
            return StepDesc;
        }
    }
}
