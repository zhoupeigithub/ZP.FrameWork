using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Swfit.Common.Log4net;

namespace Swfit.UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Applogs.Instance().Register();//注册日志管理器
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool flag = Swfit.Common.Validate.Validation.ProgramIsRunning();
            if (flag)
            {
                MessageBox.Show("程序已打开，请等待响应");
                return;
            }
            Application.Run(new Frm_Main());
        }
    }
}
