using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 市场交易接口
    /// </summary>
    public interface IPlugin_MarketTrader
    {

        /// <summary>
        /// 得到所有连接信息
        /// </summary>
        /// <returns></returns>
        List<ConnectionInfo> GetAllConnections();

        /// <summary>
        /// 连接市场服务器
        /// </summary>
        void Connect(ConnectionInfo connectionInfo);

        /// <summary>
        /// 
        /// </summary>
        DelegateOnConnectionStatus OnConnectionStatus { get; set; }

        /// <summary>
        /// 断开市场服务器
        /// </summary>
        void DisConnect();

        /// <summary>
        /// 查询品种
        /// </summary>
        /// <param name="instruments"></param>
        void QueryInstruments(string[] instruments = null);

        /// <summary>
        /// 返回合约信息
        /// </summary>
        DelegateOnReturnInstrument OnReturnInstruments { get; set; }
    }
}
