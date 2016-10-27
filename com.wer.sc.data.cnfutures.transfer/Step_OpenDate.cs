using com.wer.sc.utils.ui.proceed;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    /// <summary>
    /// 数据更新，更新开盘日期
    /// </summary>
    public class Step_OpenDate : IStep
    {
        private DataLoader stepDataLoader;

        public Step_OpenDate(DataLoader stepDataLoader)
        {
            this.stepDataLoader = stepDataLoader;
        }

        public int ProgressStep
        {
            get
            {
                return 5;
            }
        }

        public string StepDesc
        {
            get
            {
                return "更新开盘日数据";
            }
        }

        public string Proceed()
        {
            List<int> openDates = stepDataLoader.DataLoader_OpenDate.GetOpenDates();
            String[] openDateStr = new String[openDates.Count];
            for (int i = 0; i < openDates.Count; i++)
            {
                openDateStr[i] = openDates[i].ToString(); ;
            }
            File.WriteAllLines(stepDataLoader.PluginSrcDataPath + "\\opendates.csv", openDateStr);
            return "开盘日数据更新完成";
        }
        public override string ToString()
        {
            return StepDesc;
        }
    }
}
