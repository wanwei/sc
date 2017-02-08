using com.wer.sc.data.opentime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.transfer
{
    public class KLineTimeListGetter : IKLineTimeListGetter
    {
        private IOpenDateReader openDateReader;
        private IOpenTimeReader openTimeReader;

        public KLineTimeListGetter(IOpenDateReader openDateReader, IOpenTimeReader openTimeReader)
        {
            this.openDateReader = openDateReader;
            this.openTimeReader = openTimeReader;
        }

        public List<double> GetKLineTimes(string code, int date, KLinePeriod period)
        {
            List<double[]> openTimes = openTimeReader.GetOpenTime(code, date);
            return OpenTimePeriodUtils.GetKLineTimeList(date, openDateReader, openTimes, period);
        }
    }
}
