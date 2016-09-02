using System.Collections.Generic;

namespace com.wer.sc.data
{
    public interface ICodeReader
    {
        bool Contain(string code);

        List<string> GetAllCatelogs();

        List<CodeInfo> GetAllCodes();

        CodeInfo GetCode(string code);

        List<string> GetCodesByCatelog(string catelog);

        void Refresh();
    }
}