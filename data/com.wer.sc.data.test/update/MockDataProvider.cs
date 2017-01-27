
using com.wer.sc.data.store;
using com.wer.sc.data.test.Properties;
using com.wer.sc.data.update;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class MockDataProvider : MockDataProvider_Abstract
    {
        public override string GetCodeResource()
        {
            return Resources.MockData_Code;
        }

        public override string GetOpenDateResource()
        {
            return Resources.MockData_OpenDate;
        }

        public override List<double[]> GetOpenTime(string code, int date)
        {
            return base.GetOpenTime(code, date);
        }
    }
}
