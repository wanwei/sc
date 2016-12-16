using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 检索
    /// </summary>
    public class PluginAssemblyScan
    {
        private static string[] ignoreDll = GetIgnoreDll();

        private static string[] GetIgnoreDll()
        {
            string[] strs = new string[] {
                "com.wer.sc.plugin.dll",
                "com.wer.sc.utils.dll",
                "log4net.dll",
                "XAPI_CSharp.dll"};
            return strs;
        }

        public List<PluginAssembly> Scan(String path)
        {
            if (!Directory.Exists(path))
                return new List<PluginAssembly>();
            String[] files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            List<PluginAssembly> plugins = new List<PluginAssembly>();
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                if (IsIgnoreDll(file))
                    continue;
                PluginAssembly plugin = PluginAssembly.Create(file);
                if (plugin != null)
                    plugins.Add(plugin);
            }
            return plugins;
        }

        private static bool IsIgnoreDll(string file)
        {
            for (int i = 0; i < ignoreDll.Length; i++)
            {
                if (file.EndsWith(ignoreDll[i]))
                    return true;
            }
            return false;
        }
    }
}
