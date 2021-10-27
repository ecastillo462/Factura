using Factura2021_1400.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Factura2021_1400
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
            Application.Run(new LoginView());
        }
    }
}
