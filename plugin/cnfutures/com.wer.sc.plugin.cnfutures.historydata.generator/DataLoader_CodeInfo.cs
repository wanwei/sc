using com.wer.sc.data.utils;
using com.wer.sc.plugin.cnfutures.historydata.generator.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.cnfutures.generator
{
    /// <summary>
    /// 股票或期货信息加载
    /// </summary>
    public class DataLoader_CodeInfo
    {
        private Dictionary<String, CodeInfo> dicCodes = new Dictionary<string, CodeInfo>();
        private List<CodeInfo> codes;
        private Dictionary<String, String> dicCatelogs = new Dictionary<string, string>();
        private List<String> catelogs = new List<string>();

        public DataLoader_CodeInfo()
        {
            initCodes();
            initCatelogs();
        }

        private void initCodes()
        {
            this.codes = CsvUtils_Code.LoadByContent(Resources.codes);
            for (int i = 0; i < codes.Count; i++)
            {
                dicCodes.Add(codes[i].Code, codes[i]);
            }
        }

        private void initCatelogs()
        {
            String[] lines = Resources.catelogs.Split('\r');
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.Equals(""))
                    continue;
                String[] strs = line.Split(',');
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
            return dicCatelogs[dicCodes[code.ToUpper()].Catelog];
        }

        public String GetVariety(String code)
        {
            return dicCodes[code.ToUpper()].Catelog;
        }

        public List<CodeInfo> GetCodes(String variety)
        {
            List<CodeInfo> vcodes = new List<CodeInfo>();
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo c = codes[i];
                if (c.Catelog.Equals(variety))
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
