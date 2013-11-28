using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using HtmlAgilityPack;
using Skybound.Gecko;

namespace PS2StatTracker
{
    public partial class GUIMain
    {
        void UpdateWeapons()
        {
            GeckoElement collection = (GeckoElement)m_browser.Document.GetElementById("weaponsInfo");

            if (collection == null) return;

            // Load formatted html with javascript results into reader stream.
            System.IO.TextReader reader = new System.IO.StringReader(collection.InnerHtml);

            // Parse HTML by loading the text stream.
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.Load(reader);

            float totalKills = 0;
            float totalHS = 0;

            // Rows
            HtmlNodeCollection rows = htmlDoc.DocumentNode.SelectNodes("//div[@class='wepRow']");
            for (int i = 0; i < rows.Count; i++)
            {
                // Initialize new event.
                Weapon weapon = new Weapon();

                weapon.Initialize();

                // Columns
                // Name, kills, kills/min, accuracy, fire count, headshots, hitscount, score.
                // Only bring in absolute numbers. Accuracy and HS % will be calculated by this program.
                weapon.name = rows[i].SelectSingleNode(".//div[@class='wpName']").InnerText;
                weapon.kills = float.Parse(rows[i].SelectSingleNode(".//div[@class='kills sorted']").InnerText);
                weapon.fireCount = float.Parse(rows[i].SelectSingleNode(".//div[@class='fireCount']").InnerText);
                weapon.headShots = float.Parse(rows[i].SelectSingleNode(".//div[@class='headshots']").InnerText);
                weapon.hitsCount = float.Parse(rows[i].SelectSingleNode(".//div[@class='hitCount']").InnerText);

                // Update total stats.
                totalKills += weapon.kills;
                totalHS += weapon.headShots;

                bool skipWeapon = false;

                // Weapon already exists. Check to see if it changed.
                if (m_weapons.ContainsKey(weapon.name))
                {
                    Weapon lastWeapon = m_weapons[weapon.name];
                    // The weapon has been fired.
                    if (lastWeapon.lastFireCount < weapon.fireCount)
                    {
                        // Check if the weapon is a MAX arm. Ignore in this case-- there is no way to distinguish left/right currently.
                        for (int j = 0; j < rows.Count; j++)
                        {
                            if (i != j)
                            {
                                if (rows[j].SelectSingleNode(".//div[@class='wpName']").InnerText == weapon.name)
                                {
                                    skipWeapon = true;
                                    break;
                                }
                            }
                        }
                        if (!skipWeapon)
                            AddSessionWeapon(weapon, lastWeapon, true);

                    }
                }
                if (!skipWeapon)
                {
                    weapon.lastFireCount = weapon.fireCount;
                    // Add the weapon to the global list.
                    m_weapons[weapon.name] = weapon;
                    // Load session start weapons.
                    if (!m_weaponsUpdated)
                        m_sesStartWeapons[weapon.name] = weapon;
                }
            }

            UpdateWeaponTextFields(m_weapons, this.weaponsGridView);
            UpdateWeaponTextFields(m_sessionWeapons, this.sessionWeaponsGridView);

            // Update overall hs stats.
            if (!m_weaponsUpdated)
            {
                // Reset overall hsr so it can be updated again.
                m_sessionStartHSR = 0.0f;
                m_sessionStartKDR = 0.0f;
                UpdateOverallStats(0, totalHS, 0);
            }

            m_weaponsUpdated = true;
            m_updatingWeapons = false;
        }

        void UpdateEventLog(bool getName = false)
        {
            // Update the username displayed.
            if (getName)
            {
                GeckoElement title = (GeckoElement)m_browser.Document.GetElementById("title");
                System.IO.TextReader nameReader = new System.IO.StringReader(title.InnerHtml);
                // Parse HTML by loading the text stream.
                HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                html.Load(nameReader);
                HtmlNode node = html.DocumentNode.SelectSingleNode(".//a");
                this.playerNameLabel.Text = node.InnerText;
                this.playerNameLabel.Visible = true;
                m_username = node.InnerText;

                // Determine faction. Faction is stored as class name under logo.
                GeckoElement faction = (GeckoElement)m_browser.Document.GetElementById("factionLogo");
                if (faction.ClassName == "faction_nc")
                    m_userFaction = "NC";
                else if (faction.ClassName == "faction_tr")
                    m_userFaction = "TR";
                else if (faction.ClassName == "faction_vs")
                    m_userFaction = "VS";
                else
                    m_userFaction = "Unknown";

                SaveUserName();
            }

            // Get killboard table.
            GeckoElement collection = (GeckoElement)m_browser.Document.GetElementById("timelineTable");

            if (collection == null) return;

            // Load formatted html with javascript results into reader stream.
            System.IO.TextReader reader = new System.IO.StringReader(collection.InnerHtml);

            // Parse HTML by loading the text stream.
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.Load(reader);

            // Rows
            HtmlNodeCollection rows = htmlDoc.DocumentNode.SelectNodes(".//tr");
            for (int i = 0; i < rows.Count; i++)
            {
                // Initialize new event.
                EventLog newEvent = new EventLog();

                newEvent.Initialize();

                // Columns
                HtmlNodeCollection cols = rows[i].SelectNodes(".//td");

                // Timestamp, event, location, method.
                newEvent.timeStamp = cols[0].InnerText;
                newEvent.userName = cols[1].InnerText;
                newEvent.location = cols[2].InnerText;
                newEvent.method = cols[3].InnerText;

                // Determine faction from cols[1] inner html.
                if (cols[1].InnerHtml.Contains("faction nc"))
                    newEvent.faction = "NC";
                else if (cols[1].InnerHtml.Contains("faction tr"))
                    newEvent.faction = "TR";
                else if (cols[1].InnerHtml.Contains("faction vs"))
                    newEvent.faction = "VS";
                else
                    newEvent.faction = "Unknown";

                // Extract additional information.

                // Check for death.
                int indexStart = newEvent.userName.IndexOf("KILLED BY");
                if (indexStart != -1)
                {
                    // Remove KILLED BY and all white space.
                    newEvent.userName = newEvent.userName.Remove(indexStart);
                    newEvent.death = true;
                }
                newEvent.userName = RemoveWhiteSurroundingSpace(newEvent.userName);

                // Check for headshot.
                indexStart = newEvent.method.IndexOf("Headshot");
                if (indexStart != -1)
                {
                    // Remove KILLED BY and all white space.
                    newEvent.method = newEvent.method.Remove(indexStart);
                    newEvent.headshot = true;
                }
                newEvent.method = RemoveWhiteSurroundingSpace(newEvent.method);

                // Check for suicide.
                if (newEvent.userName == m_username || newEvent.method == "Suicide")
                {
                    newEvent.death = true;
                    newEvent.suicide = true;
                }

                // Check if the new event being added is the latest event. A full check needs to be done
                // if the current event doesn't match. Rarely the site may first report the items in the wrong order.
                if (newEvent == m_currentEvent || m_eventLog.Contains(newEvent))
                    break;

                // Determine the order in which to add the event.
                if (m_sessionStarted)
                {
                    if (i < m_eventLog.Count)
                        m_eventLog.Insert(i, newEvent);
                    else
                        m_eventLog.Add(newEvent);
                }
                else
                    m_eventLog.Add(newEvent);

               // Add session weapon stats unless this event was a death or team kill.
               AddSessionWeapon(newEvent);
            }

            if (m_eventLog.Count > 0)
            {
                m_currentEvent = m_eventLog[0];
                // Update killboard.
                this.eventLogGridView.Rows.Clear();
                this.eventLogGridView.Rows.Add(m_eventLog.Count);
                int i = 0;
                foreach (EventLog eventlog in m_eventLog)
                {
                    string eventName = eventlog.userName;

                    this.eventLogGridView.Rows[i].Cells[0].Value = eventName;
                    this.eventLogGridView.Rows[i].Cells[1].Value = eventlog.method;
                    this.eventLogGridView.Rows[i].Cells[1].Style.ForeColor = Color.Beige;
                    //this.eventLogGridView.Rows[i].Cells[2].Value = eventlog.headshot;
                    if (eventlog.headshot)
                        ((DataGridViewImageCell)eventLogGridView.Rows[i].Cells[2]).Value = Properties.Resources.hsImage;

                    // Set row color depending on kill or death.
                    for (int j = 0; j < this.eventLogGridView.Rows[i].Cells.Count; j++)
                    {
                        if (eventlog.death || eventlog.suicide) // Death.
                            this.eventLogGridView.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        else if(eventlog.faction == m_userFaction) // Friendly kill.
                            this.eventLogGridView.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                        else // Enemy kill.
                            this.eventLogGridView.Rows[i].Cells[j].Style.BackColor = Color.Green;
                    }

                    i++;
                }
                this.eventLogGridView.ClearSelection();
                UpdateEventTextFields();
                UpdateWeaponTextFields(m_sessionWeapons, this.sessionWeaponsGridView);
            }

            m_lastEventFound = true;

            m_updatingEvents = false;
        }

        public void UpdateEventTextFields()
        {
            int kills = 0;
            int deaths = 0;
            int killHS = 0;
            int deathHS = 0;
            int suicide = 0;
            foreach (EventLog eventLog in m_eventLog)
            {
                if (eventLog.IsKill())
                {
                    if (eventLog.faction != m_userFaction)
                    {
                        kills++;
                        killHS += eventLog.headshot ? 1 : 0;
                    }
                }
                else
                {
                    deaths++;
                    deathHS += eventLog.headshot ? 1 : 0;
                    suicide += eventLog.suicide ? 1 : 0;
                }
            }

            this.killsTextBox.Text = kills.ToString();
            this.deathsTextBox.Text = deaths.ToString();
            this.hsKillsTextBox.Text = killHS.ToString();
            this.hsDeathsTextBox.Text = deathHS.ToString();

            float kdr = (deaths == 0 ? (float)kills : (float)kills / (float)deaths);
            float hs = (kills == 0 ? 0.0f : (float)killHS / (float)kills);
            this.kdrTextBox.Text = kdr.ToString("0.000");
            this.hsTextBox.Text = hs.ToString("#0.###%");
        }

        void UpdateOverallStats(float kills, float headshots, float deaths)
        {
            m_totalKills += kills;

            // HSR
            m_totalHS += headshots;

            float ratio = m_totalHS / m_totalKills;

            // Set the start of session head shot ratio.
            if (m_sessionStartHSR == 0.0f)
                m_sessionStartHSR = ratio;

            float dif = ratio - m_sessionStartHSR;

            if (dif < 0)
                this.hsrGrowthLabel.ForeColor = m_lowColor;
            else if (dif > 0)
                this.hsrGrowthLabel.ForeColor = m_highColor;
            else
                this.hsrGrowthLabel.ForeColor = Color.White;

            this.totalKillsTextBox.Text = m_totalKills.ToString();
            this.totalHSTextBox.Text = ratio.ToString("#0.###%");
            this.hsrGrowthLabel.Text = dif.ToString("+#0.###%; -#0.###%");


            // KDR
            m_totalDeaths += deaths;
            ratio = m_totalKills / m_totalDeaths;

            // Set the start of session kd ratio.
            if (m_sessionStartKDR == 0.0f)
                m_sessionStartKDR = ratio;

            dif = ratio - m_sessionStartKDR;

            if (dif < 0)
                this.kdrGrowthLabel.ForeColor = m_lowColor;
            else if (dif > 0)
                this.kdrGrowthLabel.ForeColor = m_highColor;
            else
                this.kdrGrowthLabel.ForeColor = Color.White;

            this.totalDeathsTextBox.Text = m_totalDeaths.ToString();
            this.totalKDRTextBox.Text = ratio.ToString("0.000");
            this.kdrGrowthLabel.Text = dif.ToString("+#0.0000; -#0.0000");
        }

        public void UpdateWeaponTextFields(Dictionary<string, Weapon> weapons, DataGridView gridView)
        {
            gridView.Columns.Clear();

            gridView.Columns.Add("nameCol", "Name");
            gridView.Columns.Add("killsCol", "Kills");
            gridView.Columns.Add("hsPercentCol", "HS%");
            gridView.Columns.Add("accuracyCol", "Acc");
            gridView.Columns.Add("headShotCol", "HS");
            gridView.Columns.Add("fireCountCol", "Fired");
            gridView.Columns.Add("hitsCount", "Hits");

            for (int i = 1; i < gridView.ColumnCount; i++)
            {
                // Resize columns.
                gridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // Set the HSR/ACC column to wrap text.
            gridView.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridView.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Resize the rows.
            gridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;

            // Add all rows before iterating through them.
            if (weapons.Count == 0) return;

            gridView.Rows.Add(weapons.Count);

            for (int i = 0; i < weapons.Count; i++)
            {
                Weapon weapon = weapons.ElementAt(i).Value;
                gridView.Rows[i].Cells[0].Value = weapon.name;
                gridView.Rows[i].Cells[1].Value = weapon.kills;

                // Calculate acc change.
                float currentACC = weapon.hitsCount / weapon.fireCount;
                string accStr = currentACC.ToString("#0.###%");
                // Calculate hsr change.
                float currentHSR = weapon.headShots / weapon.kills;
                string hsrStr = currentHSR.ToString("#0.###%");
                if (gridView.Name == "sessionWeaponsGridView" && m_sessionStarted)
                {
                    // HSR
                    float[] absHSR = GetWeaponHSR(weapon.name, m_sesStartWeapons);
                    float oldTotalHSR = absHSR[0] / (absHSR[1] == 0.0f ? 1 : absHSR[1]);
                    absHSR[0] += weapon.headShots;
                    absHSR[1] += weapon.kills;
                    float newTotalHSR = absHSR[0] / (absHSR[1] == 0.0f ? 1 : absHSR[1]);
                    float hsrDif = newTotalHSR - oldTotalHSR;

                    hsrStr += "\n" + newTotalHSR.ToString("#0.###%") + " " + hsrDif.ToString("+#0.###%; -#0.###%");

                    if (hsrDif < 0)
                        gridView.Rows[i].Cells[2].Style.ForeColor = m_lowColor;
                    else if (hsrDif > 0)
                        gridView.Rows[i].Cells[2].Style.ForeColor = m_highColor;
                    else
                        gridView.Rows[i].Cells[2].Style.ForeColor = Color.Black;

                    // ACC
                    float[] absACC = GetWeaponACC(weapon.name, m_sesStartWeapons);
                    float oldTotalACC = absACC[0] / (absACC[1] == 0.0f ? 1 : absACC[1]);
                    absACC[0] += weapon.hitsCount;
                    absACC[1] += weapon.fireCount;
                    float newTotalACC = absACC[0] / (absACC[1] == 0.0f ? 1 : absACC[1]);
                    float accDif = newTotalACC - oldTotalACC;

                    accStr += "\n" + newTotalACC.ToString("#0.###%") + " " + accDif.ToString("+#0.###%; -#0.###%");

                    if (accDif < 0)
                        gridView.Rows[i].Cells[3].Style.ForeColor = m_lowColor;
                    else if (accDif > 0)
                        gridView.Rows[i].Cells[3].Style.ForeColor = m_highColor;
                    else
                        gridView.Rows[i].Cells[3].Style.ForeColor = Color.Black;
                }

                gridView.Rows[i].Cells[2].Value = hsrStr;
                gridView.Rows[i].Cells[3].Value = accStr;
                gridView.Rows[i].Cells[4].Value = weapon.headShots;
                gridView.Rows[i].Cells[5].Value = weapon.fireCount;
                gridView.Rows[i].Cells[6].Value = weapon.hitsCount;
            }

            gridView.ClearSelection();
        }

        void UpdateMiscFields()
        {
            // Times revived.
            float timesRevived = m_totalDeaths - m_totalDeathsWithRevives;

            this.timesRevivedTextBox.Text = timesRevived.ToString();

            // KDR with revives.
            float fakeRatio = m_totalKills / m_totalDeathsWithRevives;

            this.reviveKDRTextBox.Text = fakeRatio.ToString("0.000");

            // KDR inflation
            float realRatio = m_totalKills / m_totalDeaths;

            float inflation = (fakeRatio - realRatio) / realRatio;

            this.teamRelianceTextBox.Text = inflation.ToString("#0.###%");
        }

        // [hits, fired]
        float[] GetWeaponACC(string name, Dictionary<string, Weapon> weapons)
        {
            float[] returnVal = { 0, 0 };

            if (weapons.ContainsKey(name))
            {
                returnVal[0] = weapons[name].hitsCount;
                returnVal[1] = weapons[name].fireCount;
            }

            return returnVal;
        }

        // [headshots, kills]
        float[] GetWeaponHSR(string name, Dictionary<string, Weapon> weapons)
        {
            float[] returnVal = { 0, 0 };

            if (weapons.ContainsKey(name))
            {
                returnVal[0] = weapons[name].headShots;
                returnVal[1] = weapons[name].kills;
            }

            return returnVal;
        }

        bool IsWeaponInAllWeapons(string name)
        {
            for (int i = 0; i < this.weaponsGridView.RowCount; i++)
            {
                if (this.weaponsGridView.Rows[i].Cells.Count > 0 && this.weaponsGridView.Rows[i].Cells[0].Value.ToString() == name)
                    return true;
            }

            return false;
        }

        void AddSessionWeapon(EventLog newEvent)
        {
            Weapon newWeapon = new Weapon();
            Weapon oldWeapon = new Weapon();
            oldWeapon.Initialize();
            newWeapon.Initialize();
            newWeapon.name = newEvent.method;
            newWeapon.kills += newEvent.IsKill() ? 1 : 0;
            newWeapon.headShots += newEvent.headshot ? 1 : 0;

            // Add to total deaths.
            if (m_sessionStarted)
            {
                if (!newEvent.IsKill())
                {
                    UpdateOverallStats(0, 0, 1);
                }
                // Update overall stats. Should only be called once overall stats have been set initially.
                // *Additionally it will not update a weapon not found in the All Weapons section.
                else
                {
                    // * Testing with always updating stats regardless of if weapon was used previously.
                    //if (m_weaponsUpdated && IsWeaponInAllWeapons(newWeapon.name))
                    //{
                    // Do not give kills or headshots for team kills.
                    if(newEvent.faction != m_userFaction)
                        UpdateOverallStats(newWeapon.kills, newWeapon.headShots, 0);
                    //}
                }
            }
            // Add session weapon stats unless this event was a death or team kill.
            if(!newEvent.death && newEvent.faction != m_userFaction)
                AddSessionWeapon(newWeapon, oldWeapon);
        }

        void AddSessionWeapon(Weapon updatedWeapon, Weapon oldWeapon, bool skipKillsHS = false)
        {
            float kills = updatedWeapon.kills - oldWeapon.kills;
            float hits = updatedWeapon.hitsCount - oldWeapon.hitsCount;
            float hs = updatedWeapon.headShots - oldWeapon.headShots;
            float fired = updatedWeapon.fireCount - oldWeapon.fireCount;

            Weapon sessionWeapon;
            if (!m_sessionWeapons.ContainsKey(updatedWeapon.name))
            {
                sessionWeapon = new Weapon();
                sessionWeapon.Initialize();
                sessionWeapon.name = updatedWeapon.name;
            }
            else
            {
                sessionWeapon = m_sessionWeapons[updatedWeapon.name];
            }

            if (!skipKillsHS)
            {
                sessionWeapon.kills += kills;
                sessionWeapon.headShots += hs;
            }

            sessionWeapon.fireCount += fired;
            sessionWeapon.hitsCount += hits;

            m_sessionWeapons[updatedWeapon.name] = sessionWeapon;
        }

        void SaveUserName()
        {
            if (m_userID.Length == 0) return;
            string fullUser = m_userID + " | " + RemoveWhiteSurroundingSpace(this.playerNameLabel.Text);
            if (!this.usernameTextBox.Items.Contains(fullUser))
            {
                this.usernameTextBox.Items.Add(fullUser);
            }
        }

        // Extracts the user ID from the usernameTextBox.
        string GetUserID(string name)
        {
            if (m_userID.Length == 0)
            {
                string[] result = name.Split('|');

                if (result.Length > 0)
                {
                    m_userID = RemoveWhiteSpace(result[0]);
                }
            }
            return m_userID;
        }

        public string RemoveWhiteSpace(string input)
        {
            string output;
            output = input.Replace(" ", string.Empty);
            output = output.Replace("\t", string.Empty);
            output = output.Replace("\n", string.Empty);
            output = output.Replace("\r", string.Empty);
            return output;
        }

        // Removes white space before the first char and after the last char.
        public string RemoveWhiteSurroundingSpace(string input)
        {
            string output = "";
            input = input.Replace("\t", string.Empty);
            input = input.Replace("\n", string.Empty);
            input = input.Replace("\r", string.Empty);
            bool firstChar = false;

            for (int c = 0; c < input.Length; c++)
            {
                // Wait until finding the first char before creating the string.
                if (input[c] != ' ')
                    firstChar = true;

                if (firstChar)
                {
                    int nextC = c;

                    // If the char is a space check to see if there only spaces left.
                    if (input[c] == ' ')
                    {
                        for (; nextC < input.Length; nextC++)
                        {
                            if (input[nextC] != ' ')
                            {
                                break;
                            }
                        }
                        if (nextC == input.Length)
                            break;
                    }
                    output += input[c];
                }
            }

            return output;
        }
    }
}
