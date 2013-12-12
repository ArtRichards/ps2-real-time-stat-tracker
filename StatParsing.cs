using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PS2StatTracker
{
    public partial class GUIMain
    {
        public string GetBestWeaponID(Weapon weapon)
        {
            if (weapon.id != null && weapon.id != "0")
            {
                return weapon.id;
            }
            if (weapon.vehicleId != null && weapon.vehicleId != "0")
            {
                return VEHICLE_OFFSET + weapon.vehicleId;
            }

            return "0";
        }

        public string PercentString(float input, int digits = 3)
        {
            string digitCount = "";
            for(int i = 0; i < digits; i++)
            {
                digitCount += "#";
            }
            return ((input >= 0 ? "+" : "") + input.ToString("0." + digitCount) + "%");
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
                    if (eventLog.defender != null && eventLog.defender.faction != m_player.faction)
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
            m_player.kdr.kills += (int)kills;

            // HSR
            m_player.totalHeadshots += headshots;

            float ratio = m_player.totalHeadshots / (float)m_player.kdr.kills;

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

            this.totalKillsTextBox.Text = m_player.kdr.kills.ToString();
            this.totalHSTextBox.Text = ratio.ToString("#0.###%");
            this.hsrGrowthLabel.Text = PercentString(dif);
            // KDR
            m_player.kdr.actualDeaths += (int)deaths;
            ratio = (float)m_player.kdr.kills / (float)m_player.kdr.actualDeaths;

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

            this.totalDeathsTextBox.Text = m_player.kdr.actualDeaths.ToString();
            this.totalKDRTextBox.Text = ratio.ToString("0.000");
            this.kdrGrowthLabel.Text = dif.ToString("+#0.0000;-#0.0000");
        }

        public async Task UpdateWeaponTextFields(Dictionary<string, Weapon> weapons, DataGridView gridView)
        {
            if (weapons == null)
                return;

            DataGridViewColumn oldSortedColumn = gridView.SortedColumn;
            System.ComponentModel.ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not currently sorted. 
            if (oldSortedColumn != null)
            {
                // Sort the same column again, reversing the SortOrder. 
                if (gridView.SortOrder == SortOrder.Ascending)
                {
                    direction = System.ComponentModel.ListSortDirection.Ascending;
                }
                else
                {
                    // Sort a new column and remove the old SortGlyph.
                    direction = System.ComponentModel.ListSortDirection.Descending;
                }
            }
            else
            {
                direction = System.ComponentModel.ListSortDirection.Descending;
            }

            gridView.Columns.Clear();

            gridView.Columns.Add("nameCol", "Name");
            gridView.Columns.Add("killsCol", "Kills");
            gridView.Columns.Add("hsPercentCol", "HS%");
            gridView.Columns.Add("accuracyCol", "Acc");
            gridView.Columns.Add("headShotCol", "HS");
            gridView.Columns.Add("fireCountCol", "Fired");
            gridView.Columns.Add("hitsCount", "Hits");

            // Sort the new gridview.
            int sortedIndex = 1;
            for (int i = 1; i < gridView.ColumnCount; i++)
            {
                // Resize columns.
                gridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (oldSortedColumn != null)
                {
                    if (gridView.Columns[i].HeaderText == oldSortedColumn.HeaderText)
                    {
                        sortedIndex = i;
                    }
                }
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
                string bestID = GetBestWeaponID(weapon);

                // Will get either item or vehicle name.
                gridView.Rows[i].Cells[0].Value = await GetItemName(GetBestWeaponID(weapon));

                gridView.Rows[i].Cells[1].Value = weapon.kills;

                // Calculate acc change.
                float currentACC = weapon.hitsCount / weapon.fireCount;
                string accStr = currentACC.ToString("#0.###%");
                // Calculate hsr change.
                float currentHSR = weapon.headShots / weapon.kills;
                string hsrStr = currentHSR.ToString("#0.###%");
                if (gridView.Name == "sessionWeaponsGridView" && (m_sessionStarted || m_countEvents))
                {
                    // HSR
                    float[] absHSR = GetWeaponHSR(GetBestWeaponID(weapon), m_startPlayer.weapons);
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
                    float[] absACC = GetWeaponACC(GetBestWeaponID(weapon), m_startPlayer.weapons);
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

            gridView.Sort(gridView.Columns[sortedIndex], direction);
            gridView.Columns[sortedIndex].HeaderCell.SortGlyphDirection =
                direction == System.ComponentModel.ListSortDirection.Ascending ?
                SortOrder.Ascending : SortOrder.Descending;

            gridView.ClearSelection();
        }

        void UpdateMiscFields()
        {
            // Times revived.
            float timesRevived = (float)m_player.kdr.actualDeaths - (float)m_player.kdr.reviveDeaths;

            this.timesRevivedTextBox.Text = timesRevived.ToString();

            // KDR with revives.
            float fakeRatio = (float)m_player.kdr.kills / (float)m_player.kdr.reviveDeaths;

            this.reviveKDRTextBox.Text = fakeRatio.ToString("0.000");

            // KDR inflation
            float realRatio = (float)m_player.kdr.kills / (float)m_player.kdr.actualDeaths;

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
        float[] GetWeaponHSR(string id, Dictionary<string, Weapon> weapons)
        {
            float[] returnVal = { 0, 0 };

            if (weapons.ContainsKey(id))
            {
                returnVal[0] = weapons[id].headShots;
                returnVal[1] = weapons[id].kills;
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

        async Task AddSessionWeapon(EventLog newEvent)
        {
            Weapon newWeapon = new Weapon();
            Weapon oldWeapon = new Weapon();
            oldWeapon.Initialize();
            newWeapon.Initialize();
            if (newEvent.isVehicle)
            {
                newWeapon.id = "0";
                newWeapon.vehicleId = newEvent.methodID;
            }
            else
                newWeapon.id = newEvent.methodID;

            newWeapon.name = await GetItemName(GetBestWeaponID(newWeapon));
            newWeapon.kills += newEvent.IsKill() ? 1 : 0;
            newWeapon.headShots += newEvent.headshot ? 1 : 0;

            // Add to total deaths.
            if (m_sessionStarted || m_countEvents)
            {
                if (!newEvent.IsKill())
                {
                    UpdateOverallStats(0, 0, 1);
                }
                // Update overall stats. Should only be called once overall stats have been set initially.
                else
                {
                    // Do not give kills or headshots for team kills.
                    if(newEvent.defender != null && newEvent.defender.faction != m_player.faction)
                        UpdateOverallStats(newWeapon.kills, newWeapon.headShots, 0);
                }
            }
            // Add session weapon stats unless this event was a death or team kill.
            if(!newEvent.death && newEvent.defender != null && newEvent.defender.faction != m_player.faction)
                await AddSessionWeapon(newWeapon, oldWeapon);
        }

        async Task AddSessionWeapon(Weapon updatedWeapon, Weapon oldWeapon, bool skipKillsHS = false)
        {
            float kills = updatedWeapon.kills - oldWeapon.kills;
            float hits = updatedWeapon.hitsCount - oldWeapon.hitsCount;
            float hs = updatedWeapon.headShots - oldWeapon.headShots;
            float fired = updatedWeapon.fireCount - oldWeapon.fireCount;

            if (kills < 0 || hits < 0 || hs < 0 || fired < 0)
                return;

            string id = GetBestWeaponID(updatedWeapon);

            Weapon sessionWeapon;
            if (!m_sessionWeapons.ContainsKey(id))
            {
                sessionWeapon = new Weapon();
                sessionWeapon.Initialize();
                sessionWeapon.id = updatedWeapon.id;
                sessionWeapon.vehicleId = updatedWeapon.vehicleId;
            }
            else
            {
                sessionWeapon = m_sessionWeapons[id];
            }

            if (!skipKillsHS)
            {
                sessionWeapon.kills += kills;
                sessionWeapon.headShots += hs;
            }

            sessionWeapon.fireCount += fired;
            sessionWeapon.hitsCount += hits;

            sessionWeapon.name = await GetItemName(GetBestWeaponID(sessionWeapon));

            m_sessionWeapons[id] = sessionWeapon;
        }

        void SaveUserName()
        {
            if (m_userID.Length == 0) return;
            string fullUser = m_userID + " | " + m_player.name;
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
