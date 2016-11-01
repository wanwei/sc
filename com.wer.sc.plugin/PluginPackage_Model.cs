using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 模型插件包，里面包含多个模型
    /// </summary>
    public class PluginPackage_Model
    {
        public List<ModelInfo> Models;
    }

    public class ModelInfo
    {
        public string FullName;

        public IPlugin_Model Model;
    }
}
