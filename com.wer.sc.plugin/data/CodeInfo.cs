using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 股票或期货信息的实现类
    /// </summary>
    public class CodeInfo : ICodeInfo
    {
        private string code;

        private String name;

        private string catelog;

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
            }
        }


        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Catelog
        {
            get
            {
                return catelog;
            }

            set
            {
                catelog = value;
            }
        }

        public CodeInfo()
        {

        }

        public CodeInfo(String code, String name, String catelog)
        {
            this.Code = code;
            this.Catelog = catelog;
            this.Name = name;
        }

        override
        public String ToString()
        {
            return Code + "," + Name + "," + Catelog;
        }

        public override bool Equals(object obj)
        {
            return this.ToString().Equals(obj.ToString());
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }
    }
}