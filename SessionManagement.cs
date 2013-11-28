using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace PS2StatTracker
{
    public partial class GUIMain
    {
        void GetTotalKDR()
        {
            if (OkayToGetPage())
            {
                // Get total real deaths. Not fake deaths. Get good.
                HttpWebRequest req = WebRequest.Create("http://census.soe.com/get/ps2/characters_stat/?character_id=" + GetUserID(this.usernameTextBox.Text) +
                    "&stat_name=weapon_deaths&c:show=value_forever")
                                     as HttpWebRequest;
                string result = null;
                using (HttpWebResponse resp = req.GetResponse()
                                              as HttpWebResponse)
                {
                    StreamReader reader =
                        new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }

                char[] delimiterChars = { '[', '{', ':', '"', '}', ']' };
                string[] words = result.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] == "value_forever")
                    {
                        if (i < words.Length)
                            m_totalDeaths = Int32.Parse(words[i + 1]);
                        break;
                    }
                }

                // Get total kills.
                req = WebRequest.Create("http://census.soe.com/get/ps2/characters_stat_history?character_id=" + GetUserID(this.usernameTextBox.Text) +
                    "&stat_name=kills&c:show=all_time&c:limit=100")
                                     as HttpWebRequest;

                using (HttpWebResponse resp = req.GetResponse()
                                              as HttpWebResponse)
                {
                    StreamReader reader =
                        new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }

                // Kills first, then total deaths with revives.
                string[] words2 = result.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < words.Length; i++)
                {
                    if (words2[i] == "all_time")
                    {
                        if (i < words.Length)
                        {
                            if(m_totalKills == 0.0f)
                                m_totalKills = Int32.Parse(words2[i + 1]);
                            break;
                        }
                    }
                }

                // Get Total deaths with revives.
                req = WebRequest.Create("http://census.soe.com/get/ps2/characters_stat_history?character_id=" + GetUserID(this.usernameTextBox.Text) +
                    "&stat_name=deaths&c:show=all_time&c:limit=100")
                                     as HttpWebRequest;

                using (HttpWebResponse resp = req.GetResponse()
                                              as HttpWebResponse)
                {
                    StreamReader reader =
                        new StreamReader(resp.GetResponseStream());
                    result = reader.ReadToEnd();
                }

                // Kills first, then total deaths with revives.
                string[] words3 = result.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < words.Length; i++)
                {
                    if (words3[i] == "all_time")
                    {
                        if (i < words.Length)
                        {
                            m_totalDeathsWithRevives = Int32.Parse(words3[i + 1]);
                            break;
                        }
                    }
                }
            }
        }

        void GetEventStats()
        {
            if (OkayToGetPage())
            {
                m_timeout = 0;
                m_pageLoaded = false;
                m_updatingEvents = true;
                m_browser.Navigate("https://players.planetside2.com/#!/" + GetUserID(this.usernameTextBox.Text) + "/killboard");
                if (m_initialized)
                    m_browser.Reload();
                this.updatingLabel.Text = "Updating Events...";
                this.updatingLabel.Visible = true;
            }
        }

        void GetWeaponStats()
        {
            if (OkayToGetPage())
            {
                m_timeout = 0;
                m_pageLoaded = false;
                m_updatingWeapons = true;
                m_browser.Navigate("https://players.planetside2.com/#!/" + GetUserID(this.usernameTextBox.Text) + "/weapons");
                if (m_initialized)
                    m_browser.Reload();
                this.updatingLabel.Text = "Updating Weapons...";
                this.updatingLabel.Visible = true;
            }
        }

        void CancelOperation()
        {
            HandleUpdateChoice();
        }

        bool OkayToGetPage()
        {
            return !m_updatingEvents && !m_updatingWeapons && m_pageLoaded;
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
                m_totalDeaths = 0;
                // Make API calls.
                GetTotalKDR();
                // Update text relating to API.
                UpdateMiscFields();
                // Load event web page.
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
                    this.kdrGrowthLabel.Visible = true;
                    UpdateEventTextFields();
                    UpdateOverallStats(0.0f, 0.0f, 0.0f);
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
            m_timeout = 0;
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
            m_timeout = 0;
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
