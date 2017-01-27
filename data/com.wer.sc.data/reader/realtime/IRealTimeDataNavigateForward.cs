using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime
{
    /// <summary>
    /// 数据前进导航器
    /// 该接口用于数据回测
    /// </summary>
    public interface IRealTimeDataNavigateForward : IRealTimeDataReader
    {
        bool NavigateForward(int len);
    }
}
