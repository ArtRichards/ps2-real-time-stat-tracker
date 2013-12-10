using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS2StatTracker
{
    public partial class GUIMain : Form {
        // Update this with new versions.
        string VERSION_NUM = "0.6.0";
        string PROGRAM_TITLE = "Real Time Stat Tracker";
        List<EventLog> m_eventLog;
        Dictionary<string,
            Weapon> m_sessionWeapons;
        Dictionary<string,
            string> m_itemCache;            // Cache of item IDs to their name.
        Dictionary<string, Player>
            m_playerCache;                  // Cache of player IDs to their struct. 
        EventLog m_currentEvent;
        GUIOverlay m_overlay;
        Player m_player;
        Player m_startPlayer;
        string m_userID;
        float m_sessionStartHSR;            // Start of session headshot ratio.
        float m_sessionStartKDR;
        int m_activeSeconds;
        int m_timeout;
        bool m_countEvents;
        bool m_lastEventFound;
        bool m_sessionStarted;
        bool m_initialized;
        Color m_highColor;
        Color m_lowColor;
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public GUIMain() {
            InitializeComponent();
            m_overlay = null;
            // Load version.
            this.versionLabel.Text = PROGRAM_TITLE + " V " + VERSION_NUM;
            m_highColor = Color.FromArgb(0, 192, 0);
            m_lowColor = Color.Red;
            m_eventLog = new List<EventLog>();
            m_sessionWeapons = new Dictionary<string, Weapon>();
            m_playerCache = new Dictionary<string, Player>();
            m_itemCache = new Dictionary<string, string>();
            m_currentEvent = new EventLog();
            m_player = new Player();
            m_startPlayer = new Player();
            m_userID = "";
            m_sessionStarted = false;
            m_lastEventFound = false;
            m_initialized = false;
            m_countEvents = false;
            m_activeSeconds = 0;
            m_timeout = 0;
            m_sessionStartHSR = 0.0f;
            m_sessionStartKDR = 0.0f;

            // Prevent X images showing up.
            ((DataGridViewImageColumn)this.eventLogGridView.Columns[2]).DefaultCellStyle.NullValue = null;

            // Handle mouse movement and resizing on borderless window.
            this.menuStrip1.MouseDown += OnMouseDown;

            m_currentEvent.Initialize();
        }

        private void ClearUsers() {
            usernameTextBox.Items.Clear();
        }

        //////////////////////////////////
        // Enables borderless dragging
        // For details: http://stackoverflow.com/questions/1592876
        //////////////////////////////////
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        private void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs evt) {
            try {
                if (evt.Button == MouseButtons.Left) {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        //////////////////////////////////
        // Enables borderless resizing
        // For details: http://stackoverflow.com/questions/1535826
        //////////////////////////////////
        protected override void WndProc(ref Message m) {
            const int wmNcHitTest = 0x84;
            const int htBottomLeft = 16;
            const int htBottomRight = 17;
            if (m.Msg == wmNcHitTest) {
                int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                Point pt = PointToClient(new Point(x, y));
                Size clientSize = ClientSize;
                if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16) {
                    m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }
            }
            base.WndProc(ref m);
        }

        //////////////////////////////////
        // Button clicks.
        //////////////////////////////////

        private void button1_Click(object sender, EventArgs evt) {
            try {
                ResumeSession();
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void startSessionButton_Click(object sender, EventArgs evt) {
            try {
                if (!m_sessionStarted)
                    Initialize();
                StartSession();
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void timer1_Tick(object sender, EventArgs evt) {
            try {
                m_activeSeconds += (timer1.Interval / 1000);

                GetEventStats();

                // Update weapons every 30 minutes. Currently hardcoded. May eventually add sliders under options.
                // Also may eventually add options.
                if (m_activeSeconds % 1800 == 0)
                    GetPlayerWeapons();
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        //////////////////////////////////
        // Tool strip button clicks.
        //////////////////////////////////

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            log.Info("Program quitting");
            Application.Exit();
        }

        private void updateEventsToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                if (m_sessionStarted)
                    GetEventStats();
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void updateWeaponsToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                if (m_sessionStarted)
                    GetPlayerWeapons();
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                GUIOptions config = new GUIOptions();
                config.ShowDialog(this);
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void clearUsersToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                ClearUsers();
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                using (GUIAbout about = new GUIAbout()) {
                    about.SetTitle(PROGRAM_TITLE);
                    about.SetVersion("Version " + VERSION_NUM);
                    about.ShowDialog(this);
                }
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void positiveColorsToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                this.colorDialog1.AllowFullOpen = true;
                if (this.colorDialog1.ShowDialog(this) == DialogResult.OK)
                    m_highColor = this.colorDialog1.Color;
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void negativeColorsToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                this.colorDialog1.AllowFullOpen = true;
                if (this.colorDialog1.ShowDialog(this) == DialogResult.OK)
                    m_lowColor = this.colorDialog1.Color;
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void cancelOperationToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                CancelOperation();
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void createSessionToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                using (GUISession session = new GUISession()) {
                    session.ShowDialog(this);
                    if (session.confirmed == true) {
                        m_countEvents = session.countStatsCheckBox.Checked;
                        Initialize((int)session.pastEventsNumber.Value);
                    }
                }
                ManageSessionButtons();
            } catch (Exception e) {
                Program.HandleException(e);
            }
        }

        private void startOverlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_overlay == null)
            {
                m_overlay = new GUIOverlay();
                m_overlay.FormClosed += new FormClosedEventHandler(overlay_FormClosed);
                m_overlay.Show(this);
            }
        }

        private void overlay_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_overlay.Dispose();
            m_overlay = null;
        }

        // Called from UpdateEvents and UpdateWeapons.
        private void UpdateOverlay()
        {
            if (m_overlay != null && m_player != null && m_sessionStarted)
            {
                m_overlay.SetStats(m_player, this.killsTextBox.Text, this.deathsTextBox.Text, this.kdrTextBox.Text,
                    this.hsTextBox.Text, this.eventLogGridView, m_sessionWeapons, m_eventLog);
            }
        }

        private void GUIMain_FormClosing(object sender, FormClosingEventArgs e) {
            SaveConfig();
        }
    }
}
