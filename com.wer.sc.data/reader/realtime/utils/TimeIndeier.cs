using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader.realtime.utils
{
    /// <summary>
    /// 时间索引器
    /// </summary>
    public class TimeIndeier : ITimeIndeier
    {
        public TimeIndeier()
        {

        }

        /// <summary>
        /// 得到timeGetter里time对应的索引号
        /// </summary>
        /// <param name="timeGetter"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public int IndexOf(TimeGetter timeGetter, double time)
        {
            return IndexOf(timeGetter, time, true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeGetter"></param>
        /// <param name="time"></param>
        /// <param name="findBackward"></param>
        /// <returns></returns>
        public int IndexOf(TimeGetter timeGetter, double time, bool findBackward)
        {
            int startIndex = 0;
            int endIndex = timeGetter.Count - 1;
            double ctime = timeGetter.GetTime(startIndex);
            if (time < ctime)
                return -1;
            return IndexOf(timeGetter, time, 0, endIndex, findBackward, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeGetter"></param>
        /// <param name="time"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="findBackward"></param>
        /// <param name="indexIfRepeat"></param>
        /// <returns></returns>
        private int IndexOf(TimeGetter timeGetter, double time, int startIndex, int endIndex, bool findBackward, int indexIfRepeat)
        {
            if (endIndex - startIndex <= 1)
            {
                double startTime = timeGetter.GetTime(startIndex);
                if (startTime == time)
                    return startIndex;
                if (time < startTime)
                    return -1;
                double endTime = timeGetter.GetTime(endIndex);
                if (endTime == time)
                    return endIndex;
                if (time > endTime)
                    return -1;
                if (findBackward)
                    return startIndex;
                return endIndex;
            }
            int currentIndex = (endIndex + startIndex) / 2;
            double currentTime = timeGetter.GetTime(currentIndex);
            if (currentTime == time)
                return currentIndex;
            if (currentTime > time)
            {
                return IndexOf(timeGetter, time, startIndex, currentIndex, findBackward, indexIfRepeat);
            }
            else
            {
                return IndexOf(timeGetter, time, currentIndex, endIndex, findBackward, indexIfRepeat);
            }
        }
    }
}
