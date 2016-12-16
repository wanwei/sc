using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 向K线提供数据
    /// </summary>
    public interface IModelData
    {
        IKLineData KLineData { get; }
    }
}
