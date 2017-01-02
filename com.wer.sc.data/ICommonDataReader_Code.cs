using System.Collections.Generic;

namespace com.wer.sc.data
{
    /// <summary>
    /// 股票信息读取器
    /// </summary>
    public interface ICommonDataReader_Code
    {
        /// <summary>
        /// 是否包含code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool Contain(string code);

        List<string> GetAllCatelogs();

        List<CodeInfo> GetAllCodes();

        CodeInfo GetCode(string code);

        List<string> GetCodesByCatelog(string catelog);

        void Refresh();
    }
}