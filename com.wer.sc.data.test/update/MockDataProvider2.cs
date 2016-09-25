using com.wer.sc.data.test.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class MockDataProvider2 : MockDataProvider_Abstract
    {
        public override string GetCodeResource()
        {
            return Resources.MockData_Code2;
        }

        public override string GetOpenDateResource()
        {
            return Resources.MockData_OpenDate2;
        }
    }
}
