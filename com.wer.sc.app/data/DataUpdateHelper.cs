using com.wer.sc.data;
using com.wer.sc.data.update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.wer.sc.app.data
{
    public class DataUpdateHelper
    {
        public bool Pause;

        public bool Cancel;

        private DataMgr dataMgr;

        private DataReaderFactory dataReaderFactory;

        private String providerName;

        public List<UpdateInfo> GetUpdateInfo(List<String> codes)
        {
            return null;
        }

        public void Update(List<String> codes)
        {

        }

        public void Update(String code)
        {
            DataUpdate_Old update = dataMgr.GetDataUpdate(providerName);

            updateTick(code, update);

            update.UpdateKLine(code);
        }

        private void updateTick(string code, DataUpdate_Old update)
        {
            List<int> updateDates = update.Update_Tick.GetUpdateDates(code, dataReaderFactory);
            for (int i = 0; i < updateDates.Count; i++)
            {
                int date = updateDates[i];
                update.Update_Tick.DoUpdate(code, date);
                if (updateTickNotifier != null)
                    updateTickNotifier(code, date);
            }
        }

        private void updateKLine(String code, DataUpdate_Old update)
        {
            //update.Update_KLine.
        }

        public UpdateTickNotifier updateTickNotifier;

        public UpdateKLineNotifier updateKLineNotifier;

        public UpdateCompleteNotifier updateCompleteNotifier;

        public delegate void UpdateTickNotifier(String code, int date);

        public delegate void UpdateKLineNotifier(String code, KLinePeriod period);

        public delegate void UpdateCompleteNotifier();
    }

    public class UpdateInfo
    {
        public String Code;

        public List<int> tickDates;

        public int date_Minute;
        public int date_15Minute;
        public int date_Hour;
        public int date_Day;
    }
}
