using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.provider
{
    public class DataProviderConfig
    {
        private String configPath;

        private String providerDataPath;

        private String dataPath;

        public DataProviderConfig(PluginHelper helper)
        {
            this.configPath = helper.ConfigPath;

            String dataProviderConfigPath = helper.ConfigPath + "\\dataprovider.config";
            XmlDocument doc = new XmlDocument();
            doc.Load(dataProviderConfigPath);

            XmlElement root = doc.DocumentElement;
            providerDataPath = root.GetAttribute("providerDataPath");
            dataPath = root.GetAttribute("dataPath");
        }

        public DataProviderConfig(String configPath, String providerDataPath, String dataPath)
        {
            this.configPath = configPath;
            this.providerDataPath = providerDataPath;
            this.dataPath = dataPath;
        }

        public string ConfigPath
        {
            get
            {
                return configPath;
            }
        }

        public String ProviderDataPath
        {
            get
            {
                return providerDataPath;
            }
        }

        public String DataPath
        {
            get
            {
                return dataPath;
            }
        }


        public String GetMarketPath(String market)
        {
            return ProviderDataPath + market + "\\";
        }
    }
}
