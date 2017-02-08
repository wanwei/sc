using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    /// <summary>
    /// 市场连接信息
    /// </summary>
    public class ConnectionInfo
    {
        private Dictionary<string, string> data = new Dictionary<string, string>();

        /// <summary>
        /// 连接ID
        /// </summary>
        public string Id
        {
            get
            {
                string id = null;
                data.TryGetValue("ID", out id);
                return id;
            }
        }

        /// <summary>
        /// 设置或获取连接名称
        /// </summary>
        public string Name
        {
            get
            {
                string name = null;
                data.TryGetValue("NAME", out name);
                return name;
            }
        }

        /// <summary>
        /// 设置或获取连接描述
        /// </summary>
        public string Description
        {
            get
            {
                string desc = null;
                data.TryGetValue("DESC", out desc);
                return desc;
            }
        }

        //public Dictionary<string, string> Data
        //{
        //    get
        //    {
        //        return data;
        //    }
        //}

        public void AddValue(string key, string value)
        {
            this.data.Add(key.ToUpper(), value);
        }

        public string GetValue(string key)
        {
            return this.data[key.ToUpper()];
        }

        public bool ContainsKey(string key)
        {
            return data.ContainsKey(key);
        }

        public static ConnectionInfo LoadJson(string txt)
        {
            return JsonUtils.FromJsonTo<ConnectionInfo>(txt);
        }
    }
}
