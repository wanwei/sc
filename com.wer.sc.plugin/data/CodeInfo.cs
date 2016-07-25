using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    public class CodeInfo
    {
        public string code;

        public string catelog;

        public String name;

        public CodeInfo()
        {

        }

        public CodeInfo(String code, String name, String variety)
        {
            this.code = code;
            this.catelog = variety;
            this.name = name;
        }

        override
        public String ToString()
        {
            return code + "," + name + "," + catelog;
        }
    }
}