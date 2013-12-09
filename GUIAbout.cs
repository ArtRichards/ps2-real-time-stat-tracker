using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace PS2StatTracker
{
    public partial class GUIAbout : Form
    {
        public GUIAbout()
        {
            InitializeComponent();
        }

        public void SetTitle(string title)
        {
            this.titleLabel.Text = title;
        }

        public void SetVersion(string version)
        {
            this.versionLabel.Text = version;
        }

        private void closeButton_Click(object sender, EventArgs evt)
        {
            this.Close();
        }

        private void websiteLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs evt)
        {
            try {
                Process.Start("http://recursion.recursion.tk/");
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void changelogButton_Click(object sender, EventArgs e)
        {
            GUIChangelog changes = new GUIChangelog();
            changes.ShowDialog(this);
        }
    }
}
