using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public class GraphicDataProviderMgr
    {
        private DataReaderFactory dataReaderFac;

        private Dictionary<KLinePeriod, GraphicDataProvider> dicProvider = new Dictionary<KLinePeriod, GraphicDataProvider>();

        private KLinePeriod currentPeriod;

        private GraphicDataProvider currentProvider;

        private String code;

        private int startDate;

        private int endDate;

        private double currentTime;

        public GraphicDataProviderMgr(DataReaderFactory dataReaderFac)
        {
            this.dataReaderFac = dataReaderFac;
        }

        public void ChangeTime(double time)
        {
            this.currentTime = time;
        }

        #region 修改数据

        public void ChangeData(String code, int startDate, int endDate, KLinePeriod period)
        {
            this.code = code;
            this.startDate = startDate;
            this.endDate = endDate;
            this.currentPeriod = period;

            this.dicProvider.Clear();
            GraphicDataProvider dataProvider = new GraphicDataProvider_Default(dataReaderFac);
            dataProvider.ChangeData(code, startDate, endDate, period);
            this.dicProvider.Add(period, dataProvider);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="code"></param>
        public void ChangeData(String code)
        {
            this.code = code;
        }

        public void ChangeData(KLinePeriod period)
        {
            this.currentPeriod = period;
        }

        public void ExpandDataForward(int days)
        {

        }

        public void ExpandDataBackward(int days)
        {

        }

        #endregion

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="period"></param>
        public void ChangeKLinePeriod(KLinePeriod period)
        {

        }

        /// <summary>
        /// 得到当前的provider
        /// </summary>
        public GraphicDataProvider CurrentProvider
        {
            get { return currentProvider; }
        }

        public double CurrentTime
        {
            get
            {
                return currentTime;
            }            
        }
    }

    public class TimeTravel
    {
        public KLinePeriod CurrentPeriod;

        public void Next()
        {

        }
    }
}
