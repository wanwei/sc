using com.wer.sc.data.historydata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.generator
{
    /// <summary>
    /// 更新信息获取器
    /// </summary>
    public class UpdateDataInfoGetter
    {
        /// <summary>
        /// 得到还需要更新的Tick数据
        /// 返回一个数据更新信息的队列，每个元素记录了一支股票或期货需要更新的数据
        /// </summary>
        /// <param name="isFillUp">是否将所有缺的数据全部补上，如果isFillUp为false，那么会从现在的历史数据中最新的数据开始更新，否则会将会补全所有数据</param>
        /// <returns></returns>
        public List<UpdateDataInfo> GetTickNewData(bool isFillUp)
        {
            //List<UpdateDataInfo> newDataList = new List<UpdateDataInfo>(codes.Count);
            //for (int i = 0; i < codes.Count; i++)
            //{
            //    UpdateDataInfo info = new UpdateDataInfo();
            //    info.code = codes[i].Code;
            //    if (isFillUp)
            //        info.dates = loader.GetWaitForUpdateOpenDates_TickData_FillUp(codes[i].Code);
            //    else
            //        info.dates = loader.GetWaitForUpdateOpenDates_TickData(codes[i].Code);
            //    newDataList.Add(info);
            //}
            //return newDataList;
            return null;
        }

        /// <summary>
        /// 得到还需要更新的K线数据
        /// 返回一个数据更新信息的队列，每个元素记录了一支股票或期货需要更新的数据
        /// </summary>
        /// <param name="period"></param>
        /// <param name="isFillUp"></param>
        /// <returns></returns>
        public List<UpdateDataInfo> GetKLineNewData(KLinePeriod period, bool isFillUp)
        {
            //List<UpdateDataInfo> newDataList = new List<UpdateDataInfo>(codes.Count);
            //for (int i = 0; i < codes.Count; i++)
            //{
            //    UpdateDataInfo info = new UpdateDataInfo();
            //    info.code = codes[i].Code;
            //    if (isFillUp)
            //        info.dates = loader.GetWaitForUpdateOpenDates_KLineData_FillUp(codes[i].Code, period);
            //    else
            //        info.dates = loader.GetWaitForUpdateOpenDates_KLineData(codes[i].Code, period);
            //    newDataList.Add(info);
            //}
            //return newDataList;
            return null;
        }
    }
}
