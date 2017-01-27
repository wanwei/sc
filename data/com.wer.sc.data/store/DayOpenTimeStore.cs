using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    /// <summary>
    /// 日开盘时间保存，现在保存成CSV格式
    /// </summary>
    public class DayOpenTimeStore
    {
        private String path;
        public DayOpenTimeStore(String path)
        {
            this.path = path;
        }

        public void Save(List<DayOpenTime> codes)
        {
            CsvUtils_DayStartTime.Save(path, codes);
        }
    }
}
