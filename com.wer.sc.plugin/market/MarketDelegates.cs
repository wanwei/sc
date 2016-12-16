using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market
{
    /// <summary>
    /// 数据连接状态修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="status"></param>
    /// <param name="userLogin"></param>
    /// <param name="size1"></param>
    public delegate void DelegateOnConnectionStatus(object sender, ConnectionStatus status, ref LoginInfo userLogin);

    /// <summary>
    /// 返回市场数据修改
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="marketData"></param>
    public delegate void DelegateOnReturnMarketData(object sender, ref ITickBar marketData);

    /// <summary>
    /// 返回合约信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="instruments"></param>
    public delegate void DelegateOnReturnInstrument(object sender, ref List<InstrumentInfo> instruments);
}
