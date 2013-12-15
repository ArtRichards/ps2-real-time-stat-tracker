using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Web;

namespace PS2StatTracker 
{
    public class GlobalVariables {
        // Update this with new versions.
        public const string VERSION_NUM = "0.6.0.0";
        public const string PROGRAM_TITLE = "Real Time Stat Tracker";
    }

    public struct VersionInfo {
        public string version,
            md5,
            updateInfo;
        public bool isNew;
    }

    public class UpdateChecker {
        string fileAddress = "http://recursion.recursion.tk/PS2RTSTSetup.msi";
        string fileInfoAddress = "http://recursion.recursion.tk/rtstver.txt";
        string fileSave = "rtstupdate.msi";

        int attempts = 3;
        int versionField = 0;
        int md5Field = 1;
        public async Task<VersionInfo> CheckForNewVersion() {
            VersionInfo versionInfo = new VersionInfo();
            HttpClient httpClient = new HttpClient();
            using (HttpResponseMessage response = await httpClient.GetAsync(fileInfoAddress)) {
                if (response.IsSuccessStatusCode) {
                    // Read the text contents.
                    string fileInfo = await response.Content.ReadAsStringAsync();
                    string[] fields = fileInfo.Split('\n');
                    System.Version currentVersion = new System.Version(GlobalVariables.VERSION_NUM);
                    System.Version remoteVersion = new System.Version(fields[0]);
                    if (remoteVersion.CompareTo(currentVersion) > 0)
                        versionInfo.isNew = true;
                    versionInfo.version = fields[versionField];
                    versionInfo.md5 = fields[md5Field];
                    // Assemble the update info.
                    for(int i = 2; i < fields.Length; i++)
                    {
                        versionInfo.updateInfo += fields[i] + "\n";
                    }
                    // Retry.
                } else if (attempts >= 1)
                    await Task.Delay(1000);
            }

            return versionInfo;
        }
        public async Task DownloadFile(bool execute = true) {
            HttpClient httpClient = new HttpClient();
            using (HttpResponseMessage response = await httpClient.GetAsync(fileAddress)) {
                if (response.IsSuccessStatusCode) {
                    // Read the data to a byte array.
                    byte[] byteArray = await response.Content.ReadAsByteArrayAsync();
                    // Write the bytes to file.
                    System.IO.File.WriteAllBytes(fileSave, byteArray);
                    // Launch the update.
                    if(execute)
                        System.Diagnostics.Process.Start(fileSave);
                } else if (attempts < 1)
                    response.EnsureSuccessStatusCode(); // throws an exception when IsSuccessStatusCode is false
                else
                    await Task.Delay(1000);
            }
        }
    }

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
