using com.wer.sc.data.cnfutures;
using com.wer.sc.plugin.market.cnfutures.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market.cnfutures
{
    public class OpenTimeGetter
    {
        private OpenTimeUtil openTimeUtil;

        private Dictionary<string, string> dicMarket = new Dictionary<string, string>();

        public OpenTimeGetter()
        {
            this.openTimeUtil = new OpenTimeUtil();
            this.openTimeUtil.LoadFromString(Resources.opentime);

            string[] strs = Resources.catelogs.Split('\r');
            for (int i = 0; i < strs.Length; i++)
            {
                string str = strs[i];
                if (str.Trim() == "")
                    continue;
                string[] arr = str.Split(',');
                dicMarket.Add(arr[0].Trim().ToUpper(), arr[2].Trim());
            }
        }

        public List<double[]> GetMarketOpenTime(string code, int date)
        {
            string variety = code.Substring(0, code.Length - 2);
            string market = dicMarket[variety.ToUpper()];
            return openTimeUtil.GetOpenTime(market, variety, date);
        }
    }
}
