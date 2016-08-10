using com.wer.sc.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 数据提供插件
    /// 该插件的作用是给系统灌数据
    /// 
    /// 持久化以下几类数据：
    /// 1.tick数据
    /// 2.k线数据，5秒、1分钟、15分钟、日线
    /// 3.code及其分类
    /// </summary>
    public abstract class DataProvider
    {
        private PluginHelper pluginHelper;

        public PluginHelper Helper
        {
            get
            {
                return pluginHelper;
            }
        }
        
        public DataProvider(PluginHelper helper)
        {
            this.pluginHelper = helper;
        }

        public abstract String GetName();

        public abstract String GetDescription();

        /// <summary>
        /// 该方法返回数据最后的保存路径
        /// </summary>
        /// <returns></returns>
        public abstract String GetDataPath();

        /// <summary>
        /// 该插件提供的所有股票或期货信息
        /// </summary>
        /// <returns></returns>
        public abstract List<CodeInfo> GetCodes();

        /// <summary>
        /// 得到所有开盘日
        /// </summary>
        /// <returns></returns>
        public abstract List<int> GetOpenDates();

        /// <summary>
        /// 得到单一股票的所有开盘日
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public abstract List<int> GetOpenDates(String code);

        /// <summary>
        /// 得到某股票或期货当日开盘时间
        /// </summary>
        /// <param name="code"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public abstract List<double[]> GetOpenTime(String code, int date);

        public abstract TickData GetTickData(String code, int date);
    }
}