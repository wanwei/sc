using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historydata
{
    /// <summary>
    /// 该类表示等待更新的数据
    /// </summary>
    public class WaitForUpdateInfo
    {
        public String code;

        public List<int> dates;
    }
}