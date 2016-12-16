using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.utils.ui.proceed
{
    public interface Proceed
    {
        List<Step> Prepare();

        bool IsCancel { get; set; }
    }

    public interface Step
    {
        int ProgressSteps { get; }

        String DoProceed();
    }
}