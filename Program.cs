using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;


namespace PS2StatTracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Required for Gecko to work.
            Skybound.Gecko.Xpcom.Initialize("../xulrunner");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GUIMain form = new GUIMain();
            form.LoadConfig();
            Application.Run(form);
        }
    }
}
