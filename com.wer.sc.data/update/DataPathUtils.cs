using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class DataPathUtils
    {
        private String dataPath;
        public DataPathUtils(String dataPath)
        {
            this.dataPath = RealPath(dataPath);
        }

        public String GetCodePath()
        {
            return dataPath + "codes";
        }

        public String GetOpenDatePath()
        {
            return dataPath + "opendate";
        }

        public string GetTickPath(string code)
        {
            return dataPath + code + "\\tick\\";
        }

        public string GetTickPath(string code, int date)
        {
            String realPath = dataPath + code + "\\tick\\" + code + "_" + date + ".tick";
            return realPath;
        }

        public String GetKLineDataPath(String code, KLinePeriod period)
        {
            String realPath = dataPath + code + "\\" + code + "_" + period.Period + GetPeriodTypeName(period.PeriodType) + ".kline";
            return realPath;
        }

        private String GetPeriodTypeName(int type)
        {
            switch (type)
            {
                case KLinePeriod.TYPE_SECOND:
                    return "second";
                case KLinePeriod.TYPE_MINUTE:
                    return "minute";
                case KLinePeriod.TYPE_HOUR:
                    return "hour";
                case KLinePeriod.TYPE_DAY:
                    return "day";
                case KLinePeriod.TYPE_WEEK:
                    return "week";
            }
            return "";
        }

        private String RealPath(String path2)
        {
            String path = path2;
            if (!path.EndsWith("\\") || !path.EndsWith("/"))
                path += "\\";
            return path;
        }
    }
}
