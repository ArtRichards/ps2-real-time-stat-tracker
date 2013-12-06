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
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GUIMain form = new GUIMain();
            form.LoadConfig();
            Application.Run(form);
        }
    }
}
