using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public class CodeStore
    {
        private String path;
        public CodeStore(String path)
        {
            this.path = path;
        }

        public void Save(List<CodeInfo> codes)
        {
            String[] strArr = new string[codes.Count];
            for (int i = 0; i < codes.Count; i++)
            {
                CodeInfo arr = codes[i];
                strArr[i] = arr.ToString();                
            }

            DirectoryInfo dir = Directory.GetParent(path);
            if (!dir.Exists)
                dir.Create();

            StreamWriter writer = File.CreateText(path);
            try
            {
                for (int i = 0; i < codes.Count; i++)
                {
                    writer.WriteLine(strArr[i]);
                }
            }
            finally
            {
                writer.Close();
            }
        }

        public List<CodeInfo> Load()
        {
            if (!File.Exists(path))
                return new List<CodeInfo>();
            String[] strs = File.ReadAllLines(path);
            List<CodeInfo> codes = new List<CodeInfo>();
            for (int i = 0; i < strs.Length; i++)
            {
                String line = strs[i];
                String[] arr = line.Split(',');
                codes.Add(new CodeInfo(arr[0], arr[1], arr[2]));
            }
            return codes;
        }
    }
}