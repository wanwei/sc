using com.wer.sc.data.utils;
using com.wer.sc.plugin.historydata.csv;
using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator.tick
{
    public abstract class Step_TickData_Abstract : IStep
    {
        private string code;
        private int date;
        private string pluginSrcDataPath;

        public Step_TickData_Abstract(string code, int date, string pluginSrcDataPath)
        {
            this.code = code;
            this.date = date;
            this.pluginSrcDataPath = pluginSrcDataPath;
        }

        public abstract TickData GetTickData(string code, int date);

        public virtual int ProgressStep
        {
            get
            {
                return 10;
            }
        }

        public virtual string StepDesc
        {
            get
            {
                return "更新" + code + "在" + date + "的Tick数据";
            }
        }

        public string PluginSrcDataPath
        {
            get
            {
                return pluginSrcDataPath;
            }
        }

        public string Proceed()
        {
            TickData tickData = GetTickData(code, date);
            if (tickData == null)
                return code + "-" + date + "没有数据";
            string path = CsvHistoryDataPathUtils.GetTickDataPath(pluginSrcDataPath, code, date);
            CsvUtils_TickData.Save(path, tickData);
            return code + "-" + date + "的Tick数据更新完成";
        }
    }
}