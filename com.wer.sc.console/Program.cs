﻿using com.wer.sc.data;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.console
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginMgr2 mgr = PluginMgr2.Instance;
            List<PluginInfo2> plugins = mgr.Load();
            for (int i = 0; i < plugins.Count; i++)
            {
                Console.WriteLine(plugins[i]);
                PluginInfo2 plugin = plugins[i];
                Plugin_DataProvider provider = plugin.CreateDataProvider(plugin.DataProviders[0]);
                List<CodeInfo> codes = provider.GetCodes();
                for(int j = 0; j < codes.Count; j++)
                {
                    Console.WriteLine(codes[j]);
                }
            }
            Console.ReadLine();
        }
    }
}
