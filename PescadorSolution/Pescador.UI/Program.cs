using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Pescador.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            var legalAcceptd = Registry.GetValue(@"HKEY_CURRENT_USER\Software\dotSeven\Pescador", "Legal", null);
            if (legalAcceptd == null)
            {
                Application.Run(new LegalesWindow());
                
                legalAcceptd = Registry.GetValue(@"HKEY_CURRENT_USER\Software\dotSeven\Pescador", "Legal", null);

                if (legalAcceptd is int && ((int) legalAcceptd) != 1)
                    return;
            }

            if (legalAcceptd is int && ((int)legalAcceptd) != 1)
                Application.Run(new LegalesWindow());

            legalAcceptd = Registry.GetValue(@"HKEY_CURRENT_USER\Software\dotSeven\Pescador", "Legal", null);
            if (legalAcceptd is int && ((int) legalAcceptd) == 1)
            {
                Application.Run(new MainWindow());
                return;
            }
        }
    }
}
