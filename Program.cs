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
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [STAThread]
        static void Main()
        {
            System.IO.FileInfo log4netconfig = new System.IO.FileInfo("log4net.config");
            if (!log4netconfig.Exists)
                System.IO.File.WriteAllText(log4netconfig.FullName, Properties.Resources.defaultlog4netconfig);

            log4net.Config.XmlConfigurator.ConfigureAndWatch(log4netconfig);
            log.Info("Program starting");

            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                using (GUIMain form = new GUIMain()) {
                    form.LoadConfig();
                    Application.Run(form);
                }
            } catch (Exception e) {
                HandleException(e);
            }
        }

        internal static void HandleException(Exception e) {
            HandleException(e, false);
        }

        internal static void HandleException(Exception e, bool suppressPopup) {
            log.Error(e.Message, e);
            if (!suppressPopup) {
#if DEBUG
                MessageBox.Show("An error has occurred: " + e.Message + "\n\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
#else
                MessageBox.Show("An error has occurred: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
#endif
            }
        }
    }
}
