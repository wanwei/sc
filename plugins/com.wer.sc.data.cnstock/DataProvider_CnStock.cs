using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update.cnstock
{
    public class DataProvider_CnStock : DataProvider
    {
        public override string GetName()
        {
            return "中国股市数据提供";
        }

        public override string GetDescription()
        {
            return "中国股市数据提供";
        }


        public override List<CodeInfo> GetCodes()
        {
            return null;
        }

        public override string GetDataPath()
        {
            return ""; 
        }

        public override List<int> GetOpenDates()
        {
            return null;
        }

        public override List<double[]> GetOpenTime(string code, int date)
        {
            return null;
        }

        public override TickData GetTickData(string code, int date)
        {
            throw new NotImplementedException();
        }
    }
}
