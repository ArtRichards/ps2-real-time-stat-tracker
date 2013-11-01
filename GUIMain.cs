using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HtmlAgilityPack;
using Skybound.Gecko;

namespace PS2StatTracker
{
    public struct Weapon
    {
        public static bool operator ==(Weapon e1, Weapon e2)
        {
            return e1.name == e2.name;
        }
        public static bool operator !=(Weapon e1, Weapon e2)
        {
            return e1.name != e2.name;
        }
        public void Initialize()
        {
            name = ""; kills = fireCount = headShots = hitsCount = lastFireCount = 0;
        }
        public
        string name;
        public
            float kills,
            fireCount,
            headShots, 
            hitsCount,
            lastFireCount;

    }
    public struct EventLog
    {
        public static bool operator == (EventLog e1, EventLog e2)
        {
            return (e1.headshot == e2.headshot && e1.location == e2.location && e1.method == e2.method &&
                e1.suicide == e2.suicide && e1.timeStamp == e2.timeStamp && e1.userName == e2.userName);
        }
        public static bool operator !=(EventLog e1, EventLog e2)
        {
            return !(e1.headshot == e2.headshot && e1.location == e2.location && e1.method == e2.method &&
                e1.suicide == e2.suicide && e1.timeStamp == e2.timeStamp && e1.userName == e2.userName);
        }

        public bool IsKill()
        {
            return !death;
        }

        public void Initialize()
        {
            timeStamp   = "";
            userName    = "";
            method      = "";
            location    = "";
            faction     = "";
            headshot    = false;
            death       = false;
            suicide     = false;
        }

        public
        string timeStamp,
            userName,
            method,
            location,
            faction;

        public
        bool headshot,
            death,
            suicide;
    };

    public partial class GUIMain : Form
    {
        // Update this with new versions.
        string VERSION_NUM = "0.4.0";
        string PROGRAM_TITLE = "Real Time Stat Tracker";
        GeckoWebBrowser m_browser;
        List<EventLog> m_eventLog;
        Dictionary<string,
            Weapon> m_weapons;
        Dictionary<string,
            Weapon> m_sessionWeapons;
        Dictionary<string,
            Weapon> m_sesStartWeapons;
        EventLog m_currentEvent;
        string m_username;
        string m_userFaction;
        string m_userID;
        float m_totalKills;
        float m_totalHS;
        float m_sessionStartHSR;          // Start of session headshot ratio.
        int m_activeSeconds;
        bool m_lastEventFound;
        bool m_sessionStarted;
        bool m_killsUpdated;             // Set to true everytime the kills are updated. False when kill updates are asked.
        bool m_weaponsUpdated;           // Set to true when weapons are first updated. False on disconnect.
        bool m_initialized;
        bool m_pageLoaded;
        bool m_dragging;
        bool m_resizing;
        Point m_dragCursorPoint;
        Point m_dragFormPoint;
        public GUIMain()
        {
            InitializeComponent();
            // Load version.
            this.versionLabel.Text = PROGRAM_TITLE + " V " + VERSION_NUM;

            m_eventLog = new List<EventLog>();
            m_weapons = new Dictionary<string, Weapon>();
            m_sessionWeapons = new Dictionary<string, Weapon>();
            m_sesStartWeapons = new Dictionary<string, Weapon>();
            m_browser = new GeckoWebBrowser();
            m_currentEvent = new EventLog();
            m_sessionStarted = false;
            m_killsUpdated = false;
            m_lastEventFound = false;
            m_initialized = false;
            m_pageLoaded = false;
            m_weaponsUpdated = false;
            m_dragging = false;
            m_resizing = false;
            m_totalHS = 0;
            m_totalKills = 0;
            m_activeSeconds = 0;
            m_sessionStartHSR = 0.0f;
            m_username = "";
            m_userID = "";
            m_userFaction = "";

            // Prevent X images showing up.
            ((DataGridViewImageColumn)this.eventLogGridView.Columns[2]).DefaultCellStyle.NullValue = null;

            // Handle mouse movement and resizing on borderless window.
            this.resizePanelLR.MouseDown += OnMouseDownResize;
            this.resizePanelLR.MouseMove += OnMouseMove;
            this.resizePanelLR.MouseUp += OnMouseUp;

            MouseMove += OnMouseMove;
            MouseDown += OnMouseDown;
            MouseUp += OnMouseUp;
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    ctrl.MouseMove += OnMouseMove;
                    ctrl.MouseDown += OnMouseDown;
                    ctrl.MouseUp += OnMouseUp;
                }
            }

            this.menuStrip1.MouseMove += OnMouseMove;
            this.menuStrip1.MouseDown += OnMouseDown;
            this.menuStrip1.MouseUp += OnMouseUp;

            this.panel1.MouseMove += OnMouseMove;
            this.panel1.MouseDown += OnMouseDown;
            this.panel1.MouseUp += OnMouseUp;

            foreach (Control ctrl in this.panel1.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    ctrl.MouseMove += OnMouseMove;
                    ctrl.MouseDown += OnMouseDown;
                    ctrl.MouseUp += OnMouseUp;
                }
            }
            this.panel2.MouseMove += OnMouseMove;
            this.panel2.MouseDown += OnMouseDown;
            this.panel2.MouseUp += OnMouseUp;

            foreach (Control ctrl in this.panel2.Controls)
            {
                if (ctrl.GetType() == typeof(Label))
                {
                    ctrl.MouseMove += OnMouseMove;
                    ctrl.MouseDown += OnMouseDown;
                    ctrl.MouseUp += OnMouseUp;
                }
            }

            m_currentEvent.Initialize();
            this.Controls.Add(m_browser);
            // Handler for when page load completes.
            m_browser.DocumentCompleted += new EventHandler(browser_DocumentCompleted);
        }

        void browser_DocumentCompleted(object sender, EventArgs e)
        {
            HandleUpdateChoice();
        }

        void HandleUpdateChoice()
        {
            m_pageLoaded = true;

            // Prevent unecessary calculations.
            if (m_browser.Url.AbsolutePath == "blank")
            {
                return;
            }
            // Update eventlog first.
            if (!m_killsUpdated)
            {
                m_killsUpdated = true;
                UpdateEventLog(m_username.Length == 0);
            }
            else
            {
                UpdateWeapons();
            }

            if (!m_weaponsUpdated)
            {
                GetWeaponStats();
                return;
            }

            this.updatingLabel.Visible = false;

            if (!this.startSessionButton.Enabled && m_lastEventFound && m_weaponsUpdated)
                this.startSessionButton.Enabled = true;
        }

        private void ClearUsers()
        {
            usernameTextBox.Items.Clear();
        }

        //////////////////////////////////
        // Button clicks.
        //////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            Initialize();
        }

        private void startSessionButton_Click(object sender, EventArgs e)
        {
            StartSession();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            m_activeSeconds += (timer1.Interval / 1000);
            // Update weapons every 30 minutes. Currently hardcoded. May eventually add sliders under options.
            // Also may eventually add options.
            if (m_activeSeconds % 1800 == 0)
                GetWeaponStats();
            else
                GetEventStats();
        }

        //////////////////////////////////
        // Mouse handlers
        //////////////////////////////////
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            m_dragging = true;
            m_resizing = false;
            m_dragCursorPoint = Cursor.Position;
            m_dragFormPoint = this.Location;
        }

        private void OnMouseDownResize(object sender, MouseEventArgs e)
        {
            m_dragging = false;
            m_resizing = true;
            m_dragCursorPoint = Cursor.Position;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(m_dragCursorPoint));
                this.Location = Point.Add(m_dragFormPoint, new Size(dif));
            }

            if (m_resizing)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(m_dragCursorPoint));
                this.Size += new Size(dif);
                m_dragCursorPoint = Cursor.Position;
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            m_dragging = false;
            m_resizing = false;
        }

        //////////////////////////////////
        // Tool strip button clicks.
        //////////////////////////////////

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void updateEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(m_sessionStarted)
                GetEventStats();
        }

        private void updateWeaponsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_sessionStarted)
                GetWeaponStats();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIOptions config = new GUIOptions();
            config.ShowDialog(this);
        }

        private void clearUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearUsers();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUIAbout about = new GUIAbout();
            about.SetTitle(PROGRAM_TITLE);
            about.SetVersion("Version " + VERSION_NUM);
            about.ShowDialog(this);
        }
    }
}
