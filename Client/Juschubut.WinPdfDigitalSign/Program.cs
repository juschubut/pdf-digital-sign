using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Juschubut.WinPdfDigitalSign
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

            App.Initialize();
            
            Form frm = null;
            
            if (App.Setup.IsModoIntegrado)
            {
                frm = new FrmModoIntegrado();

                frm.Visible = !App.Setup.ModoOculto;
            }
            else
            {
                frm = new FrmFirmador();                
            }

            Application.Run(frm);

            var p = System.Diagnostics.Process.GetCurrentProcess();

            p.CloseMainWindow();
            p.Kill();
        } 
    }
}
