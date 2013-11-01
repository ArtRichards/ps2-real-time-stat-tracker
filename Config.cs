using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace PS2StatTracker
{
    public partial class GUIMain
    {
        public void LoadConfig()
        {
            string text = "";
            try
            {
                using (StreamReader streamReader = new StreamReader("../config.ini"))
                {
                    text = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }// Ignore file not found.
            catch (FileNotFoundException) { }

            string[] entries = text.Split('\n', '\t');

            foreach (string entry in entries)
            {
                string[] entryValue = entry.Split('=');
                if (entryValue.Length >= 2)
                {
                    if (entryValue[0] == "id")
                    {
                        if (!entryValue[1].Contains("NULL"))
                            this.usernameTextBox.Items.Add(RemoveWhiteSurroundingSpace(entryValue[1]));
                    }
                    else if (entryValue[0] == "lastid")
                    {
                        if (!entryValue[1].Contains("NULL"))
                            this.usernameTextBox.Text = RemoveWhiteSurroundingSpace(entryValue[1]);
                    }
                }
            }

        }

        public void SaveConfig()
        {
            string[] entryValues = new string[this.usernameTextBox.Items.Count + 1];

            // Save last entry.
            entryValues[0] = "lastid=" + (this.usernameTextBox.Text.Length == 0 ? "NULL" : this.usernameTextBox.Text);
            for (int i = 1; i < entryValues.Length; i++)
            {
                entryValues[i] = "id=" + this.usernameTextBox.Items[i - 1].ToString();
            }
            System.IO.File.WriteAllLines("../config.ini", entryValues);
        }
    }
}
