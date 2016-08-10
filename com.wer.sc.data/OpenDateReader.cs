using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class OpenDateReader
    {
        private List<int> openDates;

        public OpenDateReader(String path)
        {
            OpenDateStore store = new OpenDateStore(path);
            this.openDates = store.Load();
        }

        public List<int> GetOpenDates()
        {
            return openDates;
        }
    }
}