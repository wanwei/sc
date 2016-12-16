using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PluginAttribute : Attribute
    {
        private string id;

        private String name;

        private string desc;
        public PluginAttribute( String name, string desc)
        {
            this.name = name;
            this.desc = desc;
        }

        public PluginAttribute(string id, String name, string desc)
        {
            this.id = id;
            this.name = name;
            this.desc = desc;
        }

        public string Id
        {
            get
            {
                return id;
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
        }

        public string Desc
        {
            get
            {
                return desc;
            }
        }

    }
}
