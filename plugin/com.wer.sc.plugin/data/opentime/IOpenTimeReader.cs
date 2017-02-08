using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.opentime
{
    public interface IOpenTimeReader
    {
        /// <summary>
        /// 得到指定开盘时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<double[]> GetOpenTime(String code, int date);
    }
}
