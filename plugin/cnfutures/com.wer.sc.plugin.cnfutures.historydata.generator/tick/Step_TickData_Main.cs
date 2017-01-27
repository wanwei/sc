using com.wer.sc.data.cnfutures.generator.tick.generator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator.tick
{
    public class Step_TickData_Main : Step_TickData_Abstract
    {
        private DataGenerator_TickData_Main generator;

        public Step_TickData_Main(string code, int date, string pluginSrcDataPath, DataLoader dataLoader) : base(code, date, pluginSrcDataPath)
        {
            this.generator = new DataGenerator_TickData_Main(pluginSrcDataPath, dataLoader.DataLoader_CodeInfo, dataLoader.DataLoader_TickData);
        }

        public override TickData GetTickData(string code, int date)
        {
            string variety = code.Substring(0, code.Length - 2);
            return generator.Generate(variety, date);
        }
    }
}
