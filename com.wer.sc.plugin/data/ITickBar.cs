using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public interface ITickBar
    {
        string Code { get; }

        double Time { get; }

        // 交易价格
        float Price { get; }

        // 交易量
        int Mount { get; }

        // 到现在为止总成交量
        int TotalMount { get; }

        // 持仓增减
        int Add { get; }

        // 买价
        float BuyPrice { get; }

        // 买量
        int BuyMount { get; }

        // 卖价
        float SellPrice { get; }

        // 卖量
        int SellMount { get; }

        int Hold { get; }

        // 买OR卖
        Boolean IsBuy { get; }
    }
}
