using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.data.generator
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormChoosePlugin formChoosePlugin = new FormChoosePlugin();
            formChoosePlugin.ShowDialog();
            
            if (formChoosePlugin.DialogResult == DialogResult.OK)
            {
                FormDataUpdate form = new FormDataUpdate(formChoosePlugin.ChoosedPlugin);
                Application.Run(form);
            }
        }
    }
}
