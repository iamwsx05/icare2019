using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DiagnoseClient
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
            //Form frm = frmLED.LEDForm("123456789");
            Application.Run(new frmDiagClientMain());
        }
    }
}