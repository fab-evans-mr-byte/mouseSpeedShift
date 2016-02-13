using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace mouseSpeedShift
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(
            HandleRef hWnd,
            int nCmdShow);

        [STAThread]
        static void Main()
        {
            if (isFirstInstance())
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }

        private static bool isFirstInstance()
        {
            Process current = Process.GetCurrentProcess();
            string[] param = Environment.GetCommandLineArgs();

            if (param.Count() == 1)
            {
                foreach (Process process in Process.GetProcessesByName(current.ProcessName))
                {
                    if (process.Id != current.Id)
                    {
                        return false;
                    }
                }
            }

            int c = 0;

            foreach (Process process in Process.GetProcessesByName(current.ProcessName))
            {
                c++;
                if (c > 3)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
