﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market
{
    /// <summary>
    /// 登录返回信息
    /// </summary>
    [ComVisible(true)]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct LoginInfo
    {
        /// <summary>
        /// 交易日
        /// </summary>
        public int TradingDay;

        /// <summary>
        /// 时间
        /// </summary>
        public int LoginTime;

        /// <summary>
        /// 该字串
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string SessionID;

        /// <summary>
        /// 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string UserID;

        /// <summary>
        /// 
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string AccountID;

        /// <summary>
        /// 投资者名称
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 81)]
        public string InvestorName;

        /// <summary>
        /// 错误代码
        /// </summary>
        public int XErrorID;

        /// <summary>
        /// 错误信息
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public string Text;
    }

    /// <summary>
    /// 品种信息
    /// </summary>
    public class InstrumentInfo
    {
        /// <summary>
        /// 品种名称
        /// </summary>
        public string InstrumentName;

        /// <summary>
        /// 品种ID
        /// </summary>
        public string Symbol;

        /// <summary>
        /// 该品种在数据中心保存时的ID
        /// 这个是为期货准备的，因为期货请求ID带年，如rb1705
        /// 但是保存时是不带年的，如rb05
        /// </summary>
        public string SaveID;

        /// <summary>
        /// 交易所ID
        /// </summary>
        public string ExchangeID;

        /// <summary>
        /// 合约数量乘数
        /// </summary>
        public int VolumeMultiple;

        /// <summary>
        /// 最小变动价位
        /// </summary>
        public double PriceTick;

        /// <summary>
        /// 到期日
        /// </summary>
        public int ExpireDate;

        /// <summary>
        /// 执行价
        /// </summary>
        public double StrikePrice;

        /// <summary>
        /// 产品代码
        /// </summary>        
        public string ProductID;

        /// <summary>
        /// 基础商品代码
        /// </summary>
        public string UnderlyingInstrID;

        ///合约生命周期状态
        public InstLifePhaseType InstLifePhase;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(InstrumentName).Append(",");
            sb.Append(Symbol).Append(",");
            sb.Append(ExchangeID).Append(",");
            sb.Append(VolumeMultiple).Append(",");
            sb.Append(PriceTick).Append(",");
            sb.Append(ExpireDate).Append(",");
            sb.Append(StrikePrice).Append(",");
            sb.Append(ProductID).Append(",");
            sb.Append(UnderlyingInstrID).Append(",");
            sb.Append(InstLifePhase);
            return sb.ToString();
        }
    }

    /// <summary>
    /// 市场数据
    /// </summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public class MarketData
    {
        /// <summary>
        /// 得到该Tick发生的时间
        /// </summary>
        public double Time;

        /// <summary>
        /// 得到该Tick的交易价格
        /// </summary>
        public float Price;

        /// <summary>
        /// 得到该Tick的交易量
        /// </summary>
        public int Mount;

        /// <summary>
        /// 得到今日从开盘到现在为止的总成交量
        /// </summary>
        public int TotalMount;

        /// <summary>
        /// 得到该Tick后持仓增加或是减少的量
        /// </summary>
        public int Add;

        /// <summary>
        /// 得到该Tick的买一价格
        /// </summary>
        public float BuyPrice;

        /// <summary>
        /// 得到该Tick的买一量
        /// </summary>
        public int BuyMount;

        /// <summary>
        /// 得到该Tick的卖一价格
        /// </summary>
        public float SellPrice;

        /// <summary>
        /// 得到该Tick的卖一量
        /// </summary>
        public int SellMount;

        /// <summary>
        /// 得到该Tick时的持仓
        /// </summary>
        public int Hold;

        /// <summary>
        /// 得到该Tick是买OR卖
        /// </summary>
        public Boolean IsBuy;
    }

    /// <summary>
    /// 深度市场信息
    /// 附加了
    /// </summary>
    public class DepthMarketData : MarketData
    {
        /// <summary>
        /// 合约代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 31)]
        public string InstrumentID;

        /// <summary>
        /// 交易所代码
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string ExchangeID;

        /// <summary>
        /// 当日均价
        /// </summary>
        public double AveragePrice;

        /// <summary>
        /// 今开盘
        /// </summary>
        public double OpenPrice;

        /// <summary>
        /// 最高价
        /// </summary>
        public double HighestPrice;

        /// <summary>
        /// 最低价
        /// </summary>
        public double LowestPrice;

        /// <summary>
        /// 今收盘
        /// </summary>
        public double ClosePrice;

        /// <summary>
        /// 本次结算价
        /// </summary>
        public double SettlementPrice;

        /// <summary>
        /// 涨停板价
        /// </summary>
        public double UpperLimitPrice;

        /// <summary>
        /// 跌停板价
        /// </summary>
        public double LowerLimitPrice;

        /// <summary>
        /// 昨收盘
        /// </summary>
        public double PreClosePrice;

        /// <summary>
        /// 上次结算价
        /// </summary>
        public double PreSettlementPrice;

        /// <summary>
        /// 昨持仓量
        /// </summary>
        public double PreOpenInterest;

        ///交易阶段类型
        public TradingPhaseType TradingPhase;

        ///买档
        public DepthField[] Bids;

        /// <summary>
        /// 卖档
        /// </summary>
        public DepthField[] Asks;
    }

    /// <summary>
    /// DepthField行情
    /// </summary>
    [ComVisible(false)]
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct DepthField
    {
        public double Price;
        public int Size;
        public int Count;
    }

    /// <summary>
    /// 账号信息
    /// </summary>
    public struct AccountInfo
    {     
        /// <summary>
        /// 账户ID
        /// </summary>
        public string AccountID;          
        /// <summary>
        /// 上次结算准备金
        /// </summary>
        public double PreBalance;
        /// <summary>
        /// 当前保证金总额
        /// </summary>
        public double CurrMargin;
        /// <summary>
        /// 平仓盈亏
        /// </summary>
        public double CloseProfit;
        /// <summary>
        /// 持仓盈亏
        /// </summary>
        public double PositionProfit;
        /// <summary>
        /// 期货结算准备金
        /// </summary>
        public double Balance;
        /// <summary>
        /// 可用资金
        /// </summary>
        public double Available;
        /// <summary>
        /// 入金金额
        /// </summary>
        public double Deposit;
        /// <summary>
        /// 出金金额
        /// </summary>
        public double Withdraw;
        /// <summary>
        /// 可取资金
        /// </summary>
        public double WithdrawQuota;
        /// <summary>
        /// 冻结的过户费
        /// </summary>
        public double FrozenTransferFee;
        /// <summary>
        /// 冻结的印花税
        /// </summary>
        public double FrozenStampTax;
        /// <summary>
        /// 冻结的手续费
        /// </summary>
        public double FrozenCommission;
        /// <summary>
        /// 冻结的资金
        /// </summary>
        public double FrozenCash;
        /// <summary>
        /// 过户费
        /// </summary>
        public double TransferFee;
        /// <summary>
        /// 印花税
        /// </summary>
        public double StampTax;
        /// <summary>
        /// 手续费
        /// </summary>
        public double Commission;
        /// <summary>
        /// 资金差额
        /// </summary>
        public double CashIn;
    }

    /// <summary>
    /// 持仓信息
    /// </summary>
    public struct PositionInfo
    {
        public string InstrumentName;

        public string Symbol;

        public string InstrumentID;

        public string ExchangeID;

        public string ClientID;

        public string AccountID;

        public PositionSide Side;
        /// <summary>
        /// 日期
        /// </summary>
        public int Date;
        /// <summary>
        /// 持仓成本
        /// </summary>
        public double PositionCost;

        /// <summary>
        /// 总持仓
        /// </summary>
        public double Position;
        /// <summary>
        /// 今日持仓
        /// </summary>
        public double TodayPosition;
        /// <summary>
        /// 历史持仓
        /// </summary>
        public double HistoryPosition;
        /// <summary>
        /// 历史冻结持仓
        /// </summary>
        public double HistoryFrozen;

        /// <summary>
        /// 今日买卖持仓
        /// </summary>
        public double TodayBSPosition;
        /// <summary>
        /// 今日买卖持仓冻结
        /// </summary>
        public double TodayBSFrozen;
        /// <summary>
        /// 今日申赎持仓
        /// </summary>
        public double TodayPRPosition;
        /// <summary>
        /// 今日申赎持仓冻结
        /// </summary>
        public double TodayPRFrozen;
    }
}