using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// 开盘日数据存储，存储成CSV格式
    /// </summary>
    public class OpenDateStore
    {
        private String path;
        public OpenDateStore(String path)
        {
            this.path = path;
        }

        public void Save(List<int> openDates)
        {
            CsvUtils_OpenDate.Save(path, openDates);
        }

        public List<int> Load()
        {
            return CsvUtils_OpenDate.Load(path);
        }
    }
}
