using com.wer.sc.data.store;
using com.wer.sc.plugin;
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
        private List<int> openDates;

        private OpenDateStore openDateStore;

        public Step_OpenDate(IPlugin_HistoryData historyData, DataPathUtils utils)
        {                        
            OpenDateStore openDateStore = new OpenDateStore(utils.GetOpenDatePath());
            this.openDates = historyData.GetOpenDates();
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
            openDateStore.Save(openDates);
            return "开盘日数据更新完成";
        }

        public override string ToString()
        {
            return StepDesc;
        }
    }
}