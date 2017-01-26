﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.market.cnfutures
{
    public abstract class Plugin_XApi_Base
    {
        const string connPath = @"\plugin\Connections\CNFUTURES\";

        public List<ConnectionInfo> GetAllConnections()
        {            
            //Application.StartupPath
            string connFullPath = Environment.CurrentDirectory + connPath;
            string[] connFiles = Directory.GetFiles(connFullPath, "*.CONN");
            List<ConnectionInfo> conns = new List<ConnectionInfo>();
            for (int i = 0; i < connFiles.Length; i++)
            {
                string connFile = connFiles[i];
                string content = File.ReadAllText(connFile);
                ConnectionInfo connInfo = ConnectionInfo.LoadJson(content);
                conns.Add(connInfo);
            }
            return conns;
        }
    }
}
