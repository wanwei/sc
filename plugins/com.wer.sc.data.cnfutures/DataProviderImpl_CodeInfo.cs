using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures
{
    public class DataProviderImpl_CodeInfo
    {
        private Dictionary<String, CodeInfo> dicCodes = new Dictionary<string, CodeInfo>();
        private List<CodeInfo> codes;
        private Dictionary<String, String> dicCatelogs = new Dictionary<string, string>();
        private List<String> catelogs = new List<string>();

        public DataProviderImpl_CodeInfo(String configPath)
        {
            initCodes(configPath);
            initCatelogs(configPath);
        }

        private void initCodes(string configPath)
        {
            String[] lines = File.ReadAllLines(configPath + "codes.csv");
            codes = new List<CodeInfo>();
            for (int i = 0; i < lines.Length; i++)
            {
                String[] strs = lines[i].Split(',');
                CodeInfo code = new CodeInfo(strs[0], strs[2], strs[1]);
                codes.Add(code);
                dicCodes.Add(code.code, code);
            }
        }

        private void initCatelogs(string configPath)
        {
            String[] lines = File.ReadAllLines(configPath + "catelogs.csv");
            for (int i = 0; i < lines.Length; i++)
            {
                String[] strs = lines[i].Split(',');
                dicCatelogs.Add(strs[0], strs[2]);
                catelogs.Add(strs[0]);
            }
        }

        public CodeInfo GetCode(String code)
        {
            return dicCodes[code];
        }

        public List<CodeInfo> GetAllCodes()
        {
            return codes;
        }

        public String GetBelongMarket(String code)
        {
            return dicCatelogs[dicCodes[code.ToUpper()].catelog];
        }

        public String GetVariety(String code)
        {
            return dicCodes[code.ToUpper()].catelog;
        }

        public List<CodeInfo> GetCodes(String variety)
        {
            List<CodeInfo> vcodes = new List<CodeInfo>();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo c = codes[i];
                if (c.catelog.Equals(variety))
                    vcodes.Add(c);
            }
            return vcodes;
        }

        public List<String> GetVarieties()
        {
            return catelogs;
        }
    }
}