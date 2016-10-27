using com.wer.sc.data;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.mockdata
{
    public class MockData_DayStartTime
    {
        public static List<DayStartTime> GetDayStartTime(string code)
        {
            List<int> openDates = MockData_OpenDate.GetAllOpenDates();
            List<DayStartTime> startTimes = new List<DayStartTime>(openDates.Count);
            for (int i = 0; i < openDates.Count; i++)
            {
                int openDate = openDates[i];
                double startTime = MockData_OpenTime.GetOpenTime(code, openDate)[0][0];
                if (startTime < 0.18)
                    startTimes.Add(new DayStartTime(openDate, openDate + startTime));
                else
                {
                    startTimes.Add(new DayStartTime(openDate, openDates[i - 1] + startTime));
                }
            }
            return startTimes;
        }
    }
}
