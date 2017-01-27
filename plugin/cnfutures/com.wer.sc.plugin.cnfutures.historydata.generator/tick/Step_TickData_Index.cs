using com.wer.sc.data.cnfutures.generator.tick.generator;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator.tick
{
    /// <summary>
    /// 期货指数的更新
    /// </summary>
    public class Step_TickData_Index : Step_TickData_Abstract
    {
        private DataGenerator_TickData_Index generator;

        public Step_TickData_Index(string code, int date, string pluginSrcDataPath, DataLoader dataLoader) : base(code, date, pluginSrcDataPath)
        {
            this.generator = new DataGenerator_TickData_Index(pluginSrcDataPath, dataLoader);
        }

        public override TickData GetTickData(string code, int date)
        {
            string variety = code.Substring(0, code.Length - 2);
            return generator.Generate(variety, date);
        }
    }
}
