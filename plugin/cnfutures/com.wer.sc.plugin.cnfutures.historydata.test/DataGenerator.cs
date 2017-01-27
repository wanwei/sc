using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.update.cnfutures.test
{
    [TestClass]
    public class DataGenerator
    {
        //[TestMethod]
        public void GenerateCodes()
        {
            IEnumerable<String> lines = File.ReadLines(@"d:\code.csv", Encoding.UTF8);
            List<CodeInfo> codeList = new List<CodeInfo>();
            foreach (String str in lines)
            {
                string[] strs = str.Split(',');
                CodeInfo code = new CodeInfo(strs[0], strs[1], GetBelong(strs[0]));
                codeList.Add(code);
            }

            String[] output = new string[codeList.Count];
            for (int i = 0; i < codeList.Count; i++)
            {
                output[i] = codeList[i].ToString();
            }
            File.WriteAllLines("d:\\codes.csv", output);
        }

        //[TestMethod]
        public void GenerateCatelogs()
        {
            IEnumerable<String> lines = File.ReadLines(@"d:\code.csv", Encoding.UTF8);

            List<String> catelogs = new List<string>();
            String[] catelogNames = new string[100];
            foreach (String str in lines)
            {
                string[] strs = str.Split(',');
                String code = strs[0];
                string catelog = GetBelong(code);
                if (!catelogs.Contains(catelog))
                    catelogs.Add(catelog);
                if (code.Contains("MI"))
                {
                    int index = catelogs.IndexOf(catelog);
                    String name = strs[1];
                    catelogNames[index] = name.Substring(0, name.Length - 2);
                    //catelogNames.Add(name.Substring(0, name.Length - 2));
                }
            }

            String[] output = new string[catelogs.Count];
            for (int i = 0; i < catelogs.Count; i++)
            {
                output[i] = catelogs[i] + "," + catelogNames[i];
            }

            File.WriteAllLines("d:\\catelogs.csv", output);
        }

        private static String GetBelong(String code)
        {
            int miIndex = code.IndexOf("MI");
            if (miIndex > 0)
                return code.Substring(0, miIndex);
            int endIndex = code.Length - 1;
            for (int i = endIndex; i >= 0; i--)
            {
                int m;
                bool b = int.TryParse(code[i].ToString(), out m);
                if (!b)
                {
                    endIndex = i;
                    break;
                }
            }
            if (endIndex != 0)
            {
                char c = code[endIndex];
                if (c == 'X' || c == 'Y')
                    endIndex--;
            }

            return code.Substring(0, endIndex + 1);
        }
    }
}
