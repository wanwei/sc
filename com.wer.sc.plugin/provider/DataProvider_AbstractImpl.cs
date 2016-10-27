using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.provider
{
    public abstract class Plugin_HistoryData_Abstract : Plugin_DataProvider
    {
        private DataProviderConfig providerConfig;

        private DataProvider_CodeInfo provider_CodeInfo;

        private DataProvider_OpenTime provider_OpenTime;

        private DataProvider_OpenDate provider_OpenDate;

        private DataProvider_TickData provider_TickData;

        public Plugin_HistoryData_Abstract(PluginHelper helper) : base(helper)
        {
            this.providerConfig = new DataProviderConfig(helper);
            this.provider_CodeInfo = new DataProvider_CodeInfo(providerConfig.ConfigPath);
            this.provider_OpenTime = new DataProvider_OpenTime(providerConfig.ConfigPath);
            this.provider_OpenDate = new DataProvider_OpenDate(providerConfig.ConfigPath);
            this.provider_TickData = new DataProvider_TickData(providerConfig.ProviderDataPath);
        }

        public override List<CodeInfo> GetCodes()
        {
            return provider_CodeInfo.GetAllCodes();
        }

        public override string GetDataPath()
        {
            return providerConfig.DataPath;
        }

        public override List<int> GetOpenDates()
        {
            return provider_OpenDate.GetOpenDates();
        }

        public override List<int> GetOpenDates(string code)
        {
            return provider_TickData.GetOpenDates(code);
        }

        public override List<double[]> GetOpenTime(string code, int date)
        {
            return provider_OpenTime.GetOpenTime(code, date);
        }

        public override TickData GetTickData(string code, int date)
        {
            return provider_TickData.GetTickData(code, date);
        }
    }
}
