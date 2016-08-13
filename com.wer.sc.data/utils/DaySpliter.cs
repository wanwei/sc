using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.utils
{
    public class DaySpliter
    {
        public List<int[]> Split(TimeGetter timeGetter)
        {
            double lastTime = timeGetter.GetTime(0);
            double time = timeGetter.GetTime(1);

            //算法                
            List<int[]> indeies = new List<int[]>(500);
            int len = timeGetter.Count;
            int currentIndex = 0;
            bool hasNight = false;
            for (int index = 1; index < len; index++)
            {
                time = timeGetter.GetTime(index);

                int date = (int)time;
                int lastDate = (int)lastTime;

                //夜盘开始，则一定是新的一天开始
                if (IsNightStart(time, lastTime))
                {
                    indeies.Add(new int[] { ((int)lastTime), currentIndex });
                    currentIndex = index;
                    hasNight = true;
                }
                else if (hasNight)
                {
                    //对于夜盘来说，如果到了第二天，则说明夜盘结束了,此时不算新的一天开始
                    if (date != lastDate)
                        hasNight = false;
                }
                //只要过了夜都算第二天的
                else if (date != lastDate)
                {
                    indeies.Add(new int[] { ((int)lastTime), currentIndex });
                    currentIndex = index;
                }

                lastTime = time;
            }
            return indeies;
        }

        public static bool IsNightStart(double time, double lastTime)
        {
            //time在晚上6点之后，lasttime在晚上6点之前
            //且前后时间相隔超过100分钟，说明time是夜盘开始
            double t1 = time - (int)time;
            if (t1 < 0.18)
                return false;

            double lastt1 = lastTime - (int)lastTime;
            if (lastt1 >= 0.18)
                return false;

            TimeSpan span = TimeUtils.Substract(time, lastTime);
            if (span.Hours * 60 + span.Minutes > 100)
            {
                return true;
            }

            return false;
        }

        public static int GetTimeDate(float time, OpenDateReader openDateReader)
        {
            int date = (int)time;
            float t = time - date;
            if (t < 0.18)
                return date;
            //openDateReader.GetOpenDates()
            return (int)time;
        }
    }



    public interface TimeGetter
    {
        double GetTime(int index);

        int Count
        {
            get;
        }
    }
}