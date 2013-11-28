using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
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
                using (StreamReader streamReader = new StreamReader("config.ini"))
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
                    else if (entryValue[0] == "posColor")
                    {
                        if (!entryValue[1].Contains("NULL"))
                        {
                            string[] col = entryValue[1].Split(' ');
                            if(col.Length == 3)
                                m_highColor = Color.FromArgb(int.Parse(col[0]), int.Parse(col[1]), int.Parse(col[2]));
                        }
                    }
                    else if (entryValue[0] == "negColor")
                    {
                        if (!entryValue[1].Contains("NULL"))
                        {
                            string[] col = entryValue[1].Split(' ');
                            if (col.Length == 3)
                                m_lowColor = Color.FromArgb(int.Parse(col[0]), int.Parse(col[1]), int.Parse(col[2]));
                        }
                    }
                }
            }

        }

        public void SaveConfig()
        {
            string[] entryValues = new string[this.usernameTextBox.Items.Count + 3];

            // Save last entry.
            entryValues[0] = "posColor=" + m_highColor.R + " " + m_highColor.G + " " + m_highColor.B;
            entryValues[1] = "negColor=" + m_lowColor.R + " " + m_lowColor.G + " " + m_lowColor.B;
            entryValues[2] = "lastid=" + (this.usernameTextBox.Text.Length == 0 ? "NULL" : this.usernameTextBox.Text);
            for (int i = 3; i < entryValues.Length; i++)
            {
                entryValues[i] = "id=" + this.usernameTextBox.Items[i - 3].ToString();
            }
            System.IO.File.WriteAllLines("config.ini", entryValues);
        }
    }
}
