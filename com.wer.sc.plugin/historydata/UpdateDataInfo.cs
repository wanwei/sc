using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.historydata
{
    /// <summary>
    /// 更新数据信息类
    /// 该类可以表示已经更新的数据，也可以表示未更新的数据
    /// </summary>
    public class UpdateDataInfo
    {
        public String code;

        public List<int> dates;
    }
}