using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.receiver
{
    public class DataReceiver
    {
        private IPluginMgr mgr;

        private IPlugin_Market currentMarket;

        //当前连接
        private ConnectionInfo currentConnection;

        private List<InstrumentInfo> instruments = new List<InstrumentInfo>();

        
    }
}
