using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market
{
    /// <summary>
    /// 连接状态
    /// </summary>
    public enum ConnectionStatus : byte
    {
        /// <summary>
        /// 未初始化
        /// </summary>
        [Description("未初始化")]
        Uninitialized = 0,
        /// <summary>
        /// 已经初始化
        /// </summary>
        [Description("已经初始化")]
        Initialized,
        /// <summary>
        /// 连接已经断开
        /// </summary>
        [Description("连接已经断开")]
        Disconnected,
        /// <summary>
        /// 连接中...
        /// </summary>
        [Description("连接中...")]
        Connecting,
        /// <summary>
        /// 连接成功
        /// </summary>
        [Description("连接成功")]
        Connected,
        /// <summary>
        /// 授权中...
        /// </summary>
        [Description("授权中...")]
        Authorizing,
        /// <summary>
        /// 授权成功
        /// </summary>
        [Description("授权成功")]
        Authorized,
        /// <summary>
        /// 登录中...
        /// </summary>
        [Description("登录中...")]
        Logining,
        /// <summary>
        /// 登录成功
        /// </summary>
        [Description("登录成功")]
        Logined,
        /// <summary>
        /// 确认中...
        /// </summary>
        [Description("确认中...")]
        Confirming,
        /// <summary>
        /// 已经确认
        /// </summary>
        [Description("已经确认")]
        Confirmed,
        /// <summary>
        /// 已经确认
        /// </summary>
        [Description("操作中...")]
        Doing,
        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Done,
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        Unknown,
    }

    public enum InstLifePhaseType : byte
    {
        /// <summary>
        /// 未上市
        /// </summary>
        NotStart,

        /// <summary>
        /// 上市
        /// </summary>
        Started,

        /// <summary>
        /// 停牌
        /// </summary>
        Pause,

        /// <summary>
        /// 到期
        /// </summary>
        Expired,

        /// <summary>
        /// 发行,参考于XSpeed
        /// </summary>
        Issue,

        /// <summary>
        /// 首日上市,参考于XSpeed
        /// </summary>
        FirstList,

        /// <summary>
        /// 退市,参考于XSpeed
        /// </summary>
        UnList,
    };


    ///交易阶段类型
    public enum TradingPhaseType : byte
    {
        /// <summary>
        /// 开盘前
        /// </summary>
        BeforeTrading,

        /// <summary>
        /// 非交易
        /// </summary>
        NoTrading,

        /// <summary>
        /// 连续交易
        /// </summary>
        Continuous,

        /// <summary>
        /// 集合竞价报单
        /// </summary>
        AuctionOrdering,

        /// <summary>
        /// 集合竞价价格平衡
        /// </summary>
        AuctionBalance,

        /// <summary>
        /// 集合竞价撮合
        /// </summary>
        AuctionMatch,

        /// <summary>
        /// 收盘
        /// </summary>
        Closed,

        /// <summary>
        /// 停牌时段,参考于LTS
        /// </summary>
        Suspension,

        /// <summary>
        /// 熔断时段,参考于LTS
        /// </summary>
        Fuse,
    };

    public enum OpenCloseType : byte
    {
        Open,
        Close,
        CloseToday,
    };
    public enum OrderType : byte
    {
        Market,
        Stop,
        Limit,
        StopLimit,
        MarketOnClose,
        Pegged,
        TrailingStop,
        TrailingStopLimit,
    };

    public enum OrderStatus : byte
    {
        NotSent,
        PendingNew,
        New,
        Rejected,
        PartiallyFilled,
        Filled,
        PendingCancel,
        Cancelled,
        Expired,
        PendingReplace,
        Replaced,
    };

    /// <summary>
    /// 委托方向
    /// </summary>
    public enum OrderSide : byte
    {
        Buy,
        Sell,
        Unknown,
    };

    /// <summary>
    /// 持仓方向
    /// </summary>
    public enum PositionSide : byte
    {
        Long,
        Short,
    };

    public enum TimeInForce : byte
    {
        ATC,
        Day,
        GTC,
        IOC,
        OPG,
        OC,
        FOK,
        GTX,
        GTD,
        GFS,
        AUC,
    };

    public enum ExecType : byte
    {
        New,
        Stopped,
        Rejected,
        Expired,
        Trade,
        PendingCancel,
        Cancelled,
        CancelReject,
        PendingReplace,
        Replace,
        ReplaceReject,
    };
}
