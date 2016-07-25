using com.wer.sc.data.cnfutures.Properties;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures
{
    public class DataProvider_CnFutures : DataProvider
    {
        public DataProvider_CnFutures()
        {

        }

        public DataProvider_CnFutures(String configPath, String providerDataPath, String dataPath)
        {
            providerConfig = new DataProviderConfig(configPath, providerDataPath, dataPath);
        }

        private DataProviderConfig providerConfig;

        public DataProviderConfig ProviderConfig
        {
            get
            {
                if (providerConfig == null)
                    providerConfig = new DataProviderConfig(Helper);
                return providerConfig;
            }
        }

        private DataProviderImpl_CodeInfo provider_CodeInfo;

        public DataProviderImpl_CodeInfo Provider_CodeInfo
        {
            get
            {
                if (provider_CodeInfo == null)
                    provider_CodeInfo = new DataProviderImpl_CodeInfo(Helper.ConfigPath);
                return provider_CodeInfo;
            }
        }

        private DataProviderImpl_OpenDate provider_OpenDate;

        public DataProviderImpl_OpenDate Provider_OpenDate
        {
            get
            {
                if (provider_OpenDate == null)
                    provider_OpenDate = new DataProviderImpl_OpenDate(ProviderConfig);
                return provider_OpenDate;
            }
        }

        private DataProviderImpl_OpenTime provider_OpenTime;

        public DataProviderImpl_OpenTime Provider_OpenTime
        {
            get
            {
                if (provider_OpenTime == null)
                    provider_OpenTime = new DataProviderImpl_OpenTime(this);
                return provider_OpenTime;
            }
        }

        private DataProviderImpl_TickData provider_TickData;
        public DataProviderImpl_TickData Provider_TickData
        {
            get
            {
                if (provider_TickData == null)
                    provider_TickData = new DataProviderImpl_TickData(this);
                return provider_TickData;
            }
        }

        public override string GetName()
        {
            return "中国期货市场";
        }

        public override string GetDescription()
        {
            return "";
        }

        override
        public string GetDataPath()
        {
            return ProviderConfig.DataPath;
        }

        override
        public List<CodeInfo> GetCodes()
        {
            return Provider_CodeInfo.GetAllCodes();
        }

        override
        public List<int> GetOpenDates()
        {
            return Provider_OpenDate.GetOpenDates();
        }

        override
        public List<double[]> GetOpenTime(string code, int date)
        {
            return Provider_OpenTime.GetOpenTime(code, date);
        }

        override
        public TickData GetTickData(string code, int date)
        {
            return Provider_TickData.GetTickData(code, date);
        }

        public String GetCodePath(String code, int date)
        {
            return ProviderConfig.ProviderDataPath + GetCodeMarket(code) + "\\" + date + "\\" + code + "_" + date + ".csv";
        }

        public String GetCodeMarket(String code)
        {
            return Provider_CodeInfo.GetBelongMarket(code);
        }

        public String GetVariety(String code)
        {
            return Provider_CodeInfo.GetVariety(code);
        }

    }
}