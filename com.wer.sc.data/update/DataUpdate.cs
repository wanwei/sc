using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataUpdate
    {
        private DataUpdate_Code update_Code;

        private DataUpdate_KLine update_KLine;

        private DataUpdate_Tick update_Tick;

        private DataProviderWrap dataProviderWrap;

        public DataUpdate(DataProviderWrap dataProviderWrap)
        {
            this.dataProviderWrap = dataProviderWrap;
            DataProvider dataProvider = dataProviderWrap.GetProvider();
            update_Code = new DataUpdate_Code(dataProvider);
            update_KLine = new DataUpdate_KLine(dataProvider);
            update_Tick = new DataUpdate_Tick(dataProvider);
        }

        public void UpdateCodeInfos()
        {
            Update_Code.Update();
        }

        public void UpdateAllByCode(String code)
        {
            UpdateTick(code);
            UpdateKLine(code);
        }

        public void UpdateKLine(String code)
        {
            Update_KLine.UpdateCode(code, dataProviderWrap.GetFactory());
        }

        public void UpdateTick(String code)
        {
            Update_Tick.DoUpdate(code, dataProviderWrap.GetFactory());
        }

        public DataUpdate_Tick Update_Tick
        {
            get
            {
                return update_Tick;
            }
        }

        public DataUpdate_KLine Update_KLine
        {
            get
            {
                return update_KLine;
            }
        }

        public DataUpdate_Code Update_Code
        {
            get
            {
                return update_Code;
            }
        }
    }
}