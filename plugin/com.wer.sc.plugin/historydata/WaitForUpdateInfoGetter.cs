using com.wer.sc.data;
using com.wer.sc.data.opentime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.historydata
{
    /// <summary>
    /// 该类帮助获取需要更新的Csv数据信息，包括类型和日期
    /// </summary>
    public class WaitForUpdateInfoGetter
    {
        private List<CodeInfo> codes;
        private List<int> openDates;

        private WaitForUpdateDateGetter updateDateGetter;

        public WaitForUpdateInfoGetter(string srcDataPath, List<CodeInfo> codes, List<int> openDates, IOpenDateReader_HistoryData openDateReader_HistoryData)
        {
            this.codes = codes;
            this.openDates = openDates;
            //IOpenDateReader_HistoryData openDateReader_HistoryData = new OpenDateReader_HistoryData_CsvData(srcDataPath);
            this.updateDateGetter = new WaitForUpdateDateGetter(openDateReader_HistoryData, openDates);
        }

        /// <summary>
        /// 得到还需要更新的Tick数据
        /// 返回一个数据更新信息的队列，每个元素记录了一支股票或期货需要更新的数据
        /// </summary>
        /// <param name="isFillUp">是否将所有缺的数据全部补上，如果isFillUp为false，那么会从现在的历史数据中最新的数据开始更新，否则会将会补全所有数据</param>
        /// <returns></returns>
        public List<WaitForUpdateInfo> GetTickNewData(bool isFillUp)
        {
            List<WaitForUpdateInfo> newDataList = new List<WaitForUpdateInfo>(codes.Count);
            for (int i = 0; i < codes.Count; i++)
            {
                WaitForUpdateInfo info = new WaitForUpdateInfo();
                info.code = codes[i].Code;
                if (isFillUp)
                    info.dates = updateDateGetter.GetWaitForUpdateOpenDates_TickData_FillUp(codes[i].Code);
                else
                    info.dates = updateDateGetter.GetWaitForUpdateOpenDates_TickData(codes[i].Code);
                newDataList.Add(info);
            }
            return newDataList;
        }

        /// <summary>
        /// 得到还需要更新的K线数据
        /// 返回一个数据更新信息的队列，每个元素记录了一支股票或期货需要更新的数据
        /// </summary>
        /// <param name="period"></param>
        /// <param name="isFillUp"></param>
        /// <returns></returns>
        public List<WaitForUpdateInfo> GetKLineNewData(KLinePeriod period, bool isFillUp)
        {
            List<WaitForUpdateInfo> newDataList = new List<WaitForUpdateInfo>(codes.Count);
            for (int i = 0; i < codes.Count; i++)
            {
                WaitForUpdateInfo info = new WaitForUpdateInfo();
                info.code = codes[i].Code;
                if (isFillUp)
                    info.dates = updateDateGetter.GetWaitForUpdateOpenDates_KLineData_FillUp(codes[i].Code, period);
                else
                    info.dates = updateDateGetter.GetWaitForUpdateOpenDates_KLineData(codes[i].Code, period);
                newDataList.Add(info);
            }
            return newDataList;
        }
    }
}