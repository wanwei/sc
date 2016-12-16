using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.receiver
{
    public partial class FormReceiver : Form
    {
        private const string PATH = @"\RECEIVER\";

        private LogUtils logUtils = new LogUtils();

        private TickBarWriter tickBarWriter;// = new TickBarWriter();

        private IPluginMgr mgr;

        private IPlugin_Market currentMarket;

        //当前连接
        private ConnectionInfo currentConnection;

        private List<InstrumentInfo> instruments = new List<InstrumentInfo>();

        private LoginInfo marketDataLoginInfo;

        private LoginInfo marketTraderLoginInfo;

        public FormReceiver()
        {
            InitializeComponent();
            //this.tickBarWriter = new TickBarWriter(Environment.CurrentDirectory + PATH, 20161213);
            this.mgr = PluginMgrFactory.DefaultPluginMgr;
            InitMenu();
        }

        private void InitMenu()
        {
            List<PluginInfo> plugins = mgr.GetPlugins(typeof(IPlugin_Market));
            if (plugins != null)
            {
                for (int i = 0; i < plugins.Count; i++)
                {
                    PluginInfo pluginInfo = plugins[i];
                    ToolStripMenuItem item = (ToolStripMenuItem)menuItemServer.DropDownItems.Add(pluginInfo.PluginName);
                    AddMarket(item, pluginInfo);
                }
            }
            ToolStripItem loginOutItem = menuItemServer.DropDownItems.Add("登出");
            loginOutItem.Click += LoginOutItem_Click;
            ToolStripItem exitItem = menuItemServer.DropDownItems.Add("退出");
            exitItem.Click += ExitItem_Click;
        }

        private void AddMarket(ToolStripMenuItem item, PluginInfo pluginInfo)
        {
            IPlugin_Market plugin_Market = mgr.CreatePluginObject<IPlugin_Market>(pluginInfo);
            item.Tag = plugin_Market;

            IPlugin_MarketData plugin_MarketData = plugin_Market.MarketData;
            IPlugin_MarketTrader plugin_MarketTrader = plugin_Market.MarketTrader;

            plugin_MarketData.OnConnectionStatus = OnConnectionStatus_MarketData;
            plugin_MarketData.OnReturnMarketData = OnReturnMarketData;

            plugin_MarketTrader.OnConnectionStatus = OnConnectionStatus_MarketTrader;
            plugin_MarketTrader.OnReturnInstruments = OnReturnInstrument;

            List<ConnectionInfo> connectionInfos = plugin_MarketData.GetAllConnections();
            if (connectionInfos == null)
                return;
            for (int i = 0; i < connectionInfos.Count; i++)
            {
                ConnectionInfo connectionInfo = connectionInfos[i];
                ToolStripItem connItem = item.DropDownItems.Add(connectionInfo.Name);
                connItem.Tag = connectionInfo;
                connItem.Click += ConnectionItem_Click;
            }
        }

        public void OnConnectionStatus_MarketData(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            this.marketDataLoginInfo = userLogin;
            logUtils.WriteLog("登陆数据服务器：" + status);
            logUtils.WriteLog("用户：" + userLogin.AccountID + "|" + userLogin.InvestorName);
            if (status == ConnectionStatus.Logined)
            {
                //登陆成功Beep
                System.Media.SystemSounds.Beep.Play();

                Type type = currentMarket.GetType();
                string[] nameDescArr = PluginAssembly.GetPluginNameDesc(type);
                if (this.tickBarWriter == null)
                    this.tickBarWriter = new TickBarWriter(Environment.CurrentDirectory + PATH + nameDescArr[0] + "\\", marketDataLoginInfo.TradingDay);
            }
        }

        public void OnConnectionStatus_MarketTrader(object sender, ConnectionStatus status, ref LoginInfo userLogin)
        {
            this.marketTraderLoginInfo = userLogin;
            logUtils.WriteLog("登陆交易服务器：" + status);
            logUtils.WriteLog("用户：" + userLogin.AccountID + "|" + userLogin.InvestorName);

            //登陆后访问
            if (status == ConnectionStatus.Logined)
            {
                currentMarket.MarketTrader.QueryInstruments();
            }
        }

        public void OnReturnMarketData(object sender, ref ITickBar marketData)
        {
            tickBarWriter.Write(marketData);
        }

        public void OnReturnInstrument(object sender, ref List<InstrumentInfo> instruments)
        {
            logUtils.WriteLog("获得" + instruments.Count + "个品种");
            //this.instruments.AddRange(instruments);

            //TODO 写入instruments
            WriteInstruments(instruments);
            DoSubscribe(instruments);
        }

        private void WriteInstruments(List<InstrumentInfo> instruments)
        {
            instruments.Sort(delegate (InstrumentInfo a, InstrumentInfo b) { return a.Symbol.CompareTo(b.Symbol); });
            JsonUtils_Instrument.Save(GetPath() + "Instruments.json", instruments);
        }

        private string GetPath()
        {
            return Environment.CurrentDirectory + PATH;
        }

        private void DoSubscribe(List<InstrumentInfo> instruments)
        {
            int subCount = instruments.Count;
            string[] codes = new string[subCount];
            for (int i = 0; i < subCount; i++)
            {
                InstrumentInfo instrument = instruments[i];
                codes[i] = instrument.Symbol;
            }
            this.currentMarket.MarketData.Subscribe(codes);
            //string[] codes = new string[] { "rb1705" };
            //this.currentMarket.MarketData.Subscribe(codes);
        }

        private void ConnectionItem_Click(object sender, EventArgs e)
        {
            Disconnect();

            ToolStripItem connectionItem = (ToolStripItem)sender;
            ToolStripItem marketItem = connectionItem.OwnerItem;
            currentMarket = (IPlugin_Market)marketItem.Tag;
            currentConnection = (ConnectionInfo)connectionItem.Tag;
            currentMarket.MarketData.Connect(currentConnection);
            currentMarket.MarketTrader.Connect(currentConnection);
        }

        private void FormReceiver_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        private void LoginOutItem_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Disconnect()
        {
            if (currentMarket != null)
            {
                currentMarket.MarketData.DisConnect();
                currentMarket.MarketTrader.DisConnect();
                currentMarket = null;
                currentConnection = null;
            }
        }

        private void menuItemLog_Click(object sender, EventArgs e)
        {
            FormLog frmLog = new FormLog(logUtils);
            frmLog.Show();
        }

        private void menuItemDir_Click(object sender, EventArgs e)
        {

        }
    }
}
