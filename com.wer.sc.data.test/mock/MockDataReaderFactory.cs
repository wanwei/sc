using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.test.mock
{
    public class MockDataReaderFactory
    {
        public static DataReaderFactory GetDataReaderFactory()
        {
            return new DataReaderFactory(@"D:\SCMOCK\CNFUTURES\");
        }
    }
}
