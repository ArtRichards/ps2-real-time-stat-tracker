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
    public partial class GUIOptions : Form
    {
        public GUIOptions()
        {
            InitializeComponent();
            m_clearUsers = false;
        }

        uint eventUpdateTimeSec = 30;
        uint weaponUpdateTimeMin = 30;
        bool m_clearUsers;

        bool ShouldClearUsers()
        {
            return m_clearUsers;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
