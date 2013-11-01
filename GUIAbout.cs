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

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void websiteLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://recursion.recursion.tk/");
        }
    }
}
