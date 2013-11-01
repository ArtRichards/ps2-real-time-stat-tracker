using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS2StatTracker
{
    public partial class GUIMain
    {
    
        void GetEventStats()
        {
            if (m_pageLoaded)
            {
                m_pageLoaded = false;
                m_killsUpdated = false;
                m_browser.Navigate("https://players.planetside2.com/#!/" + GetUserID(this.usernameTextBox.Text) + "/killboard");
                if (m_initialized)
                    m_browser.Reload();
                this.updatingLabel.Text = "Updating Events...";
                this.updatingLabel.Visible = true;
            }
        }

        void GetWeaponStats()
        {
            if (m_pageLoaded)
            {
                m_pageLoaded = false;
                m_killsUpdated = true;
                m_browser.Navigate("https://players.planetside2.com/#!/" + GetUserID(this.usernameTextBox.Text) + "/weapons");
                if (m_initialized)
                    m_browser.Reload();
                this.updatingLabel.Text = "Updating Weapons...";
                this.updatingLabel.Visible = true;
            }
        }

        void Initialize()
        {
            if (this.usernameTextBox.Text.Length > 0)
            {
                Disconnect();
                m_lastEventFound = false;
                m_username = "";
                m_userID = "";
                m_totalKills = m_totalHS = 0;
                GetEventStats();
                m_initialized = true;
            }
        }

        private void StartSession()
        {
            if (m_sessionStarted == false)
            {
                if (m_lastEventFound)
                {
                    ResetSession();
                    this.startSessionButton.Text = "End Session";
                    this.hsrGrowthLabel.Visible = true;
                    UpdateEventTextFields();
                    UpdateOverallStats(0.0f, 0.0f);
                }
            }
            else
            {
                EndSession();
            }
        }

        void ResetSession()
        {
            timer1.Stop();
            m_activeSeconds = 0;
            m_eventLog.Clear();
            m_sessionWeapons.Clear();
            this.eventLogGridView.Rows.Clear();
            this.sessionWeaponsGridView.Columns.Clear();
            m_sessionStarted = true;
            GetEventStats();
            timer1.Start();
        }

        private void EndSession()
        {
            m_sessionStarted = false;
            m_activeSeconds = 0;
            timer1.Stop();
            this.startSessionButton.Text = "Start Session";
        }

        private void Disconnect()
        {
            EndSession();
            m_eventLog.Clear();
            m_sessionWeapons.Clear();
            m_sesStartWeapons.Clear();
            m_weapons.Clear();
            m_currentEvent.Initialize();
            m_weaponsUpdated = false;
            this.eventLogGridView.Rows.Clear();
            this.sessionWeaponsGridView.Columns.Clear();
            this.startSessionButton.Enabled = false;
            this.playerNameLabel.Visible = false;
        }
    }
}
