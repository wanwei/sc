using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class TimeConvert
    {
        private static String GetZeroStr(int len)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                sb.Append("0");
            }
            return sb.ToString();
        }

        public static DateTime ConvertToDateTime(double time)
        {
            String timeStr = time.ToString();
            if (timeStr.Length < 15)
                timeStr = timeStr + GetZeroStr(15 - timeStr.Length);

            String timeFormat = timeStr.Substring(0, 4) + "-" + timeStr.Substring(4, 2) + "-" + timeStr.Substring(6, 2)
                + " " + timeStr.Substring(9, 2) + ":" + timeStr.Substring(11, 2) + ":" + timeStr.Substring(13, 2);

            return Convert.ToDateTime(timeFormat);
        }

        public static double ConvertToDoubleTime(DateTime dt)
        {
            return Double.Parse(string.Format("{0:yyyyMMdd.HHmmss}", dt));
        }

    }
}
