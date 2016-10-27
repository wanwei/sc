using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public class CodeStore
    {
        private String path;
        public CodeStore(String path)
        {
            this.path = path;
        }

        public void Save(List<CodeInfo> codes)
        {
            CsvUtils_Code.Save(path, codes);
        }

        public List<CodeInfo> Load()
        {
            return CsvUtils_Code.Load(path);
        }
    }
}