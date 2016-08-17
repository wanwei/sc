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
        IKLineData CurrentKLineData { get; }

        int CurrentIndex { get; }

        double CurrentTime { get; }

        void Change(IKLineData data, double time);

        void Change(String code, double time, KLinePeriod period);

        void ChangeCode(String code);

        void ChangeTime(double time);

        void ChangePeriod(KLinePeriod period);
    }
}