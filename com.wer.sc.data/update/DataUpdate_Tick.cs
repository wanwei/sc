using com.wer.sc.data.store;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update
{
    public class DataUpdate_Tick
    {
        private DataPathUtils utils;
        private DataProvider dataProvider;

        public DataUpdate_Tick(DataProvider dataProvider)
        {
            this.utils = new DataPathUtils(dataProvider.GetDataPath());
            this.dataProvider = dataProvider;
        }

        public void Update()
        {
            DataReaderFactory tmpFac = new DataReaderFactory(dataProvider.GetDataPath());
            List<CodeInfo> codes = tmpFac.CodeReader.GetAllCodes();
            for (int i = 0; i < codes.Count; i++)
            {
                String code = codes[i].code;
                DoUpdate(code, tmpFac);
            }
        }

        public List<int> GetUpdatedDates(string code, DataReaderFactory tmpFac)
        {
            return tmpFac.TickDataReader.GetTickDates(code);
        }

        public List<int> GetUpdateDates(string code, DataReaderFactory tmpFac)
        {
            List<int> updateDates = new List<int>();
            List<int> openDates = dataProvider.GetOpenDates();
            List<int> currentDates = tmpFac.TickDataReader.GetTickDates(code);
            if (currentDates != null && openDates.Count == currentDates.Count)
                return updateDates;
            if (currentDates == null || currentDates.Count == 0)
            {
                updateDates.AddRange(openDates);
            }
            else
            {
                int lastDate = currentDates[currentDates.Count - 1];
                int index = openDates.IndexOf(lastDate);
                if (index < 0)
                    return updateDates;
                updateDates.AddRange(openDates.GetRange(index + 1, openDates.Count - index - 1));
            }

            return updateDates;
        }

        public void DoUpdate(string code, DataReaderFactory dataReaderFactory)
        {
            List<int> updateDates = GetUpdateDates(code, dataReaderFactory);
            if (updateDates == null)
                return;
            for (int i = 0; i < updateDates.Count; i++)
            {
                int date = updateDates[i];
                DoUpdate(code, date);
            }
        }
        public void DoUpdate(string code, int date)
        {
            TickData tickData = dataProvider.GetTickData(code, date);
            if (tickData == null)
                return;
            String path = utils.GetTickPath(code, date);
            TickDataStore store = new TickDataStore(path);
            store.save(tickData);
        }
    }
}
