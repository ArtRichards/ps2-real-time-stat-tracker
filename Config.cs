using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using PS2StatTracker.Properties;

namespace PS2StatTracker {
    public partial class GUIMain {
        public void LoadConfig() {
            if (System.IO.File.Exists("config.ini")) {
                string text = "";
                using (StreamReader streamReader = new StreamReader("config.ini")) {
                    text = streamReader.ReadToEnd();
                    streamReader.Close();
                }

                string[] entries = text.Split('\n', '\t');

                foreach (string entry in entries) {
                    string[] entryValue = entry.Split('=');
                    if (entryValue.Length >= 2) {
                        if (entryValue[0] == "id") {
                            if (!entryValue[1].Contains("NULL"))
                                this.usernameTextBox.Items.Add(RemoveWhiteSurroundingSpace(entryValue[1]));
                        } else if (entryValue[0] == "lastid") {
                            if (!entryValue[1].Contains("NULL")) {
                                this.usernameTextBox.Text = RemoveWhiteSurroundingSpace(entryValue[1]);
                            }
                        } else if (entryValue[0] == "posColor") {
                            if (!entryValue[1].Contains("NULL")) {
                                string[] col = entryValue[1].Split(' ');
                                if (col.Length == 3)
                                    m_highColor = Color.FromArgb(int.Parse(col[0]), int.Parse(col[1]), int.Parse(col[2]));
                            }
                        } else if (entryValue[0] == "negColor") {
                            if (!entryValue[1].Contains("NULL")) {
                                string[] col = entryValue[1].Split(' ');
                                if (col.Length == 3)
                                    m_lowColor = Color.FromArgb(int.Parse(col[0]), int.Parse(col[1]), int.Parse(col[2]));
                            }
                        }
                    }
                }
                Settings.Default.PositiveColor = m_highColor;
                Settings.Default.NegativeColor = m_lowColor;
                Settings.Default.LastID = usernameTextBox.SelectedIndex;
                if (Settings.Default.IDs == null)
                    Settings.Default.IDs = new System.Collections.ArrayList();
                else
                    Settings.Default.IDs.Clear();
                foreach (string entry in usernameTextBox.Items) {
                    Settings.Default.IDs.Add(entry);
                }
                try {
                    System.IO.File.Delete("config.ini");
                } catch (Exception e) {
                    Program.HandleException(e, true);
                }
            } else {
                m_highColor = Settings.Default.PositiveColor;
                m_lowColor = Settings.Default.NegativeColor;
                if (Settings.Default.IDs == null)
                    Settings.Default.IDs = new System.Collections.ArrayList();
                else {
                    foreach (string entry in Settings.Default.IDs) {
                        this.usernameTextBox.Items.Add(entry);
                    }
                }
                if (Settings.Default.LastID > 0)
                    this.usernameTextBox.SelectedIndex = Settings.Default.LastID;
            }
        }

        public void SaveConfig() {
            Settings.Default.PositiveColor = m_highColor;
            Settings.Default.NegativeColor = m_lowColor;
            Settings.Default.LastID = usernameTextBox.SelectedIndex;
            if (Settings.Default.IDs == null)
                Settings.Default.IDs = new System.Collections.ArrayList();
            else
                Settings.Default.IDs.Clear();
            foreach (string entry in usernameTextBox.Items) {
                Settings.Default.IDs.Add(entry);
            }
            Settings.Default.Save();
        }
    }
}
