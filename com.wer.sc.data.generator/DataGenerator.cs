using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.generator
{
    /// <summary>
    /// 数据生成：
    /// 1.codes
    /// 2.opendate
    /// 3.catelogs
    /// 4.tick数据
    /// 5.k线数据
    /// </summary>
    public class DataGenerator
    {

    }

    //public class GenerateInfo
    //{
    //    public List<GenerateInfo_Code> generates = new List<GenerateInfo_Code>();

    //    public int GetPeriodCount()
    //    {
    //        int max = 0;
    //        for (int i = 0; i < generates.Count; i++)
    //        {
    //            max += generates[i].GetCalcPeriodCount();
    //        }
    //        return max;
    //    }
    //}

    //public class GenerateInfo_Code
    //{
    //    public String code;

    //    public List<int> dates;

    //    public int GetCalcPeriodCount()
    //    {
    //        int progress = 10;
    //        if (dates.Count % progress == 0)
    //            return (dates.Count / progress);
    //        return (dates.Count / progress) + 1;
    //    }
    //}
}