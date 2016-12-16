﻿using com.wer.sc.plugin;
using com.wer.sc.plugin.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XAPI.Callback;

namespace com.wer.sc.plugin.xapi
{
    //[Plugin("中国期货市场交易", "中国期货市场交易")]
    public class Plugin_MarketTrader_XApi : Plugin_XApi_Base, IPlugin_MarketTrader
    {
        const string tradePath = @"plugin\CTP\CTP_Trade_x86.dll";

        private XApi api_Trade;

        private DelegateOnConnectionStatus onConnectionStatus;

        private DelegateOnReturnInstrument onReturnInstrument;

        public DelegateOnConnectionStatus OnConnectionStatus
        {
            get
            {
                return onConnectionStatus;
            }

            set
            {
                onConnectionStatus = value;
            }
        }

        public DelegateOnReturnInstrument OnReturnInstrument
        {
            get
            {
                return onReturnInstrument;
            }

            set
            {
                onReturnInstrument = value;
            }
        }

        public Plugin_MarketTrader_XApi()
        {
            api_Trade = new XApi(tradePath);
        }

        /// <summary>
        /// 连接市场服务器
        /// </summary>
        public void Connect(ConnectionInfo connectionInfo)
        {
            api_Trade.Server.BrokerID = connectionInfo.Data["BrokerId"];
            api_Trade.Server.Address = connectionInfo.Data["TradeServer"];
            api_Trade.User.UserID = connectionInfo.Data["UserID"];
            api_Trade.User.Password = connectionInfo.Data["Passwd"];

            api_Trade.OnConnectionStatus = XApi_OnConnectionStatus;
            api_Trade.OnRspQryInstrument = XApi_OnRspQryInstrument;
            api_Trade.Connect();
        }

        private void XApi_OnConnectionStatus(object sender, XAPI.ConnectionStatus status, ref XAPI.RspUserLoginField userLogin, int size1)
        {
            if (onConnectionStatus == null)
                return;

            LoginInfo loginInfo = StructTransfer.TransferUserLogin(userLogin);
            onConnectionStatus(sender, EnumTransfer.TransferConnectionStatus(status), ref loginInfo);
        }

        private List<InstrumentInfo> instruments = new List<InstrumentInfo>();

        private void XApi_OnRspQryInstrument(object sender, ref XAPI.InstrumentField instrument, int size1, bool bIsLast)
        {
            if (onReturnInstrument == null)
                return;

            //TODO 这里连续查询两次会有问题，暂不处理
            instruments.Add(StructTransfer.TransferInstrumentInfo(instrument));
            if (bIsLast)// || instruments.Count == size1)
            {
                onReturnInstrument(sender, ref instruments);
                instruments.Clear();
            }
        }

        /// <summary>
        /// 断开市场服务器
        /// </summary>
        public void DisConnect()
        {
            api_Trade.Disconnect();
        }

        public void QueryInstruments(string[] instruments)
        {
            XAPI.ReqQueryField field = new XAPI.ReqQueryField();
            api_Trade.ReqQuery(XAPI.QueryType.ReqQryInstrument, ref field);
        }

        public DelegateOnReturnInstrument OnReturnInstruments
        {
            get
            {
                return onReturnInstrument;
            }

            set
            {
                this.onReturnInstrument = value;
            }
        }
    }
}