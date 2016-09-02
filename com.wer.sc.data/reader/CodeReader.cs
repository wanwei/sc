using com.wer.sc.data.store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.reader
{
    public class CodeReader : ICodeReader
    {
        private List<CodeInfo> codes = new List<CodeInfo>();

        private Dictionary<String, CodeInfo> dicCodes = new Dictionary<string, CodeInfo>();

        private List<String> catelogs = new List<string>();

        private Dictionary<String, List<String>> dicCatelogCode = new Dictionary<string, List<string>>();

        private String path;

        public CodeReader(String path)
        {
            this.path = path;
            init();
        }

        private void init()
        {
            CodeStore store = new CodeStore(path);
            List<CodeInfo> codeInfos = store.Load();
            for (int i = 0; i < codeInfos.Count; i++)
            {
                CodeInfo codeInfo = codeInfos[i];
                codes.Add(codeInfo);
                dicCodes.Add(codeInfo.code, codeInfo);
                String catelog = codeInfo.catelog;
                if (!catelogs.Contains(catelog))
                    catelogs.Add(catelog);
                String code = codeInfo.code;
                AddDicCodeCatelog(code, catelog);
            }
        }

        private void AddDicCodeCatelog(String code, String catelog)
        {
            List<String> codes;
            if (dicCatelogCode.ContainsKey(catelog))
            {
                codes = dicCatelogCode[catelog];
            }
            else
            {
                codes = new List<string>();
                dicCatelogCode.Add(catelog, codes);
            }
            codes.Add(code);
        }

        public List<CodeInfo> GetAllCodes()
        {
            return codes;
        }

        public bool Contain(String code)
        {
            return dicCodes.ContainsKey(code);
        }

        public CodeInfo GetCode(String code)
        {
            return dicCodes[code];
        }

        public List<String> GetAllCatelogs()
        {
            return catelogs;
        }

        public List<String> GetCodesByCatelog(String catelog)
        {
            return dicCatelogCode[catelog];
        }

        public void Refresh()
        {
            codes.Clear();
            dicCodes.Clear();
            catelogs.Clear();
            dicCatelogCode.Clear();
            init();
        }
    }
}