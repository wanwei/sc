using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 市场类别
    /// </summary>
    public enum MarketType : byte
    {
        /// <summary>
        /// 国内期货市场
        /// </summary>
        CnFutures = 0,

        /// <summary>
        /// 国内股票市场
        /// </summary>
        CnStock = 1
    }
}
