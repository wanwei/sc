using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market.account
{
    /// <summary>
    /// 账号
    /// </summary>
    public interface IAccount
    {
        int CreateDate { get; }

        double InitMoney { get; }

        double Money { get; }

        double Asset { get; }

        void SendOrder(OrderInfo orderInfo);
    }
}
