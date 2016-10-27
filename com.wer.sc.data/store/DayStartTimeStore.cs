using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public class DayStartTimeStore
    {
        private String path;
        public DayStartTimeStore(String path)
        {
            this.path = path;
        }

        public void Save(List<DayStartTime> codes)
        {
            CsvUtils_DayStartTime.Save(path, codes);
        }

        public List<CodeInfo> Load()
        {
            return CsvUtils_Code.Load(path);
        }
    }
}
