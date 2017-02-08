using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.index
{
    public interface ITimeIndeier
    {
        /// <summary>
        /// 得到时间所在位置，如果time不在队列里，默认返回time之前的时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        int IndexOf(TimeGetter timeGetter, double time);

        /// <summary>
        /// 得到时间所在位置，如果time不在队列里，可以通过findForward参数确定找之前还是之后的时间
        /// </summary>
        /// <param name="timeGetter"></param>
        /// <param name="time"></param>
        /// <param name="findForward"></param>
        /// <returns></returns>
        int IndexOf(TimeGetter timeGetter, double time, bool findForward);
    }

    public interface TimeGetter
    {
        double GetTime(int index);

        int Count { get; }
    }
}
