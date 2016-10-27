using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator.tick
{
    /// <summary>
    /// 
    /// </summary>
    public class Step_TickDataBuilder
    {
        private DataLoader dataLoader;
        public Step_TickDataBuilder(DataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public Step_TickData_Abstract Build(string code, int date)
        {
            if (code.EndsWith("13"))
                return new Step_TickData_Index(code, date, dataLoader.PluginSrcDataPath, dataLoader);
            if (code.EndsWith("MI"))
                return new Step_TickData_Main(code, date, dataLoader.PluginSrcDataPath, dataLoader);
            return new Step_TickData_Normal(code, date, dataLoader.PluginSrcDataPath, dataLoader);
        }
    }
}
