using com.wer.sc.data.provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wer.sc.plugin;

namespace com.wer.sc.data.cnfutures
{
    public class DataProvider_CnFutures : DataProvider_AbstractImpl
    {
        public DataProvider_CnFutures(PluginHelper helper) : base(helper)
        {
        }

        public override string GetDescription()
        {
            return "";
        }

        public override string GetName()
        {
            return "中国期货市场";
        }
    }
}
