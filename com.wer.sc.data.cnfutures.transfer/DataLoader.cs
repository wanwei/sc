using com.wer.sc.data.cnfutures.generator.Properties;
using com.wer.sc.data.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    /// <summary>
    /// 数据装载器
    /// 该类用于数据更新时装载最原始的数据
    /// 该类装载的数据：
    /// 1.opendate，从原始数据装载
    /// 2.codeinfo，从项目的资源文件装载
    /// 3.opentime，从项目的资源文件装载
    /// 4.tickdata，从原始数据装载
    /// </summary>
    public class DataLoader
    {
        private DataLoader_OpenDate dataLoader_OpenDate;

        private DataLoader_CodeInfo dataLoader_CodeInfo;

        private DataLoader_OpenTime dataLoader_OpenTime;

        private DataLoader_TickData dataLoader_TickData;

        private Plugin_HistoryData_CnFutures historyData;

        private String srcDataPath;

        private string pluginSrcDataPath;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="srcDataPath">提供最原始数据的路径</param>
        /// <param name="pluginSrcDataPath">提供给</param>
        public DataLoader(String srcDataPath, string pluginSrcDataPath)
        {
            this.srcDataPath = srcDataPath;
            this.pluginSrcDataPath = pluginSrcDataPath;
            this.dataLoader_OpenDate = new DataLoader_OpenDate(srcDataPath);
            this.dataLoader_CodeInfo = new DataLoader_CodeInfo();
            this.dataLoader_OpenTime = new DataLoader_OpenTime(this.dataLoader_CodeInfo);
            this.dataLoader_TickData = new DataLoader_TickData(srcDataPath, dataLoader_CodeInfo);
            this.historyData = new Plugin_HistoryData_CnFutures();
        }

        public string SrcDataPath
        {
            get
            {
                return srcDataPath;
            }
        }

        public string PluginSrcDataPath
        {
            get
            {
                return pluginSrcDataPath;
            }
        }

        public DataLoader_OpenDate DataLoader_OpenDate
        {
            get
            {
                return dataLoader_OpenDate;
            }
        }

        public DataLoader_CodeInfo DataLoader_CodeInfo
        {
            get
            {
                return dataLoader_CodeInfo;
            }
        }

        public DataLoader_OpenTime DataLoader_OpenTime
        {
            get
            {
                return dataLoader_OpenTime;
            }
        }

        public DataLoader_TickData DataLoader_TickData
        {
            get
            {
                return dataLoader_TickData;
            }
        }

        public Plugin_HistoryData_CnFutures Plugin_HistoryData
        {
            get
            {
                return historyData;
            }
        }
    }
}
