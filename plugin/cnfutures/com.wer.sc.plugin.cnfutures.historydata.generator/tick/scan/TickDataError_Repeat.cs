using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator.tick.clean
{
    /// <summary>
    /// Tick数据的错误
    /// 在一个时间上重复了多次
    /// </summary>
    public class TickDataError_Repeat
    {
        /// <summary>
        /// 重复开始位置index
        /// </summary>
        public int RepeatIndex;

        /// <summary>
        /// 重复时间次数
        /// </summary>
        public int RepeatTimes;
    }
}
