using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace PS2StatTracker {
    static class Program {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        [STAThread]
        static void Main() {
            System.IO.FileInfo log4netconfig = new System.IO.FileInfo("log4net.config");
            if (!log4netconfig.Exists)
                System.IO.File.WriteAllText(log4netconfig.FullName, Properties.Resources.defaultlog4netconfig);

            log4net.Config.XmlConfigurator.ConfigureAndWatch(log4netconfig);
            log.Info("Program starting");

            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                StatTracker statTracker = new StatTracker();

                using (GUIMain form = new GUIMain(statTracker)) {
                    form.LoadConfig();
                    Application.Run(form);
                }
            } catch (Exception e) {
                HandleException(e);
            }
        }

        internal static void HandleException(Exception e) {
            HandleException(null, e, e.InnerException.GetType() == typeof(OperationCanceledException));
        }

        internal static void HandleException(IWin32Window parent, Exception e) {
            HandleException(parent, e, e.InnerException.GetType() == typeof(OperationCanceledException));
        }

        internal static void HandleException(IWin32Window parent, Exception e, bool suppressPopup) {
            log.Error(e.Message, e);
            if (!suppressPopup) {
#if DEBUG
                MessageBox.Show(parent, "An error has occurred: " + e.Message + "\n\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
#else
                MessageBox.Show(parent, "An error has occurred: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
#endif
            }
        }

        internal static async Task Retry(Task task, string actionDescription, int retryCount, bool ask) {
            try {
                await task;
                return;
            } catch (Exception e) {
                Program.HandleException(null, e, true);
                if (retryCount < 1) {
                    if (!ask)
                        throw;
                    else if (DialogResult.Retry != MessageBox.Show("An error occurred while performing the following action:\n\t- " + actionDescription + "\n\nWould you like to retry?", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1))
                        throw;
                }
            }
            await Retry(task, actionDescription, --retryCount, ask);
        }

        internal static async Task<T> Retry<T>(Task<T> task, string actionDescription, int retryCount, bool ask) {
            try {
                return await task;
            } catch (Exception e) {
                Program.HandleException(null, e, true);
                if (retryCount == 0) {
                    if (!ask)
                        throw;
                    else if (DialogResult.Retry != MessageBox.Show("An error occurred while performing the following action:\n\t- " + actionDescription + "\n\nWould you like to retry?", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1))
                        throw;
                }
            }
            return await Retry(task, actionDescription, --retryCount, ask);
        }
    }
}
