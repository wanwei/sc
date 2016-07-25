using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    public class PluginHelper
    {
        private String configPath;

        public string ConfigPath
        {
            get
            {
                return configPath;
            }

            set
            {
                configPath = value;
            }
        }        
    }
}
