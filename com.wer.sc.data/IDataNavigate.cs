using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public interface IDataNavigate
    {
        void Change(String code, double time, KLinePeriod period);

        void ChangeCode(String code);

        void ChangeTime(double time);

        void ChangePeriod(KLinePeriod period);

        IKLineData CurrentData { get; }

        int CurrentIndex { get; }
    }
}