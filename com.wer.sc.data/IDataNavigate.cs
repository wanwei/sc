using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 数据导航
    /// </summary>
    public interface IDataNavigate
    {
        String Code { get; }

        /// <summary>
        /// 得到当前K线数据
        /// </summary>
        IKLineData CurrentKLineData { get; }

        int CurrentKLineIndex { get; }

        /// <summary>
        /// 得到当前分时数据
        /// </summary>
        IRealData CurrentRealData { get; }        

        int CurrentRealIndex { get; }

        ITickData CurrentTickData { get; }

        int CurrentTickIndex { get; }

        double CurrentTime { get; }

        void Change(IKLineData data, double time);

        void Change(String code, double time, KLinePeriod period);

        void ChangeCode(String code);

        void ChangeTime(double time);

        void ChangeIndex(int index);

        void ChangePeriod(KLinePeriod period);
    }
}