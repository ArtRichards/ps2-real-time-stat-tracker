using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS2StatTracker
{
    public partial class GUISession : Form
    {
        public bool confirmed = false;

        public GUISession()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            confirmed = false;
            this.Hide();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            confirmed = true;
            this.Hide();
        }
    }
}
