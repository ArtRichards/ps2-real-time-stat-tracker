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
    public partial class GUIOverlay : Form {
        StatTracker m_statTracker;
        public GUIOverlay(StatTracker tracker) {
            InitializeComponent();
            m_statTracker = tracker;

            // Prevent X images showing up.
            ((DataGridViewImageColumn)this.eventLogGridView.Columns[2]).DefaultCellStyle.NullValue = null;

            this.eventLogGridView.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.eventLogGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.eventLogGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            for (int i = 0; i < this.eventLogGridView.Columns.Count; i++) {
                this.eventLogGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            this.eventLogGridView.Columns[0].MinimumWidth = 60;
            this.eventLogGridView.Columns[1].MinimumWidth = 60;
        }

        public void ResetStatsView() {
            this.panel1.Visible = false;
            this.killsNumber.Text = "0";
            this.kdrNum.Text = "0";
            this.hsrNum.Text = "0";
            this.deathsNum.Text = "0";
        }

        private void DisplayLastWeaponStats() {
            Player player = m_statTracker.GetPlayer();
            List<EventLog> eventLog = m_statTracker.GetEventLog();

            // Display Weapon info.
            Weapon lastWeapon = GetLastWeapon(m_statTracker.GetSessionStats().weapons, eventLog);

            if (!lastWeapon.IsNull()) {
                this.weaponName.Text = lastWeapon.name;
                // Session Weapon Stats.
                this.weaponKillsNum.Text = lastWeapon.kills.ToString();
                this.weaponHSRNum.Text = ((float)lastWeapon.headShots / (float)lastWeapon.kills).ToString("#0.#%");
                // Only display accuracy if it has updated from the API.
                if (lastWeapon.fireCount > 0) {
                    this.weaponAccNum.Text = (lastWeapon.hitsCount / lastWeapon.fireCount).ToString("#0.#%");
                    this.weaponAccNum.Visible = true;
                    this.weaponAccLabel.Visible = true;
                } else {
                    this.weaponAccNum.Visible = false;
                    this.weaponAccLabel.Visible = false;
                }
                // Total Weapon Stats.
                string id = m_statTracker.GetBestWeaponID(lastWeapon);
                if (player.weapons.ContainsKey(id)) {
                    Weapon totalWeapon = player.weapons[id];
                    // Calculate the new totals based on start session weapons and existing totals.
                    if (m_statTracker.GetSessionStats().weapons.ContainsKey(id)) {
                        Weapon sesWeapon = m_statTracker.GetSessionStats().weapons[id];
                        // HSR
                        float[] absHSR = m_statTracker.GetWeaponHSR(id, m_statTracker.GetSessionStats().startPlayer.weapons);
                        float[] startHSR = m_statTracker.GetWeaponHSR(id, m_statTracker.GetSessionStats().startSesWeapons);
                        absHSR[0] += sesWeapon.headShots - startHSR[0];
                        float newTotalKills = (absHSR[1] += sesWeapon.kills - startHSR[1]);
                        float newTotalHSR = absHSR[0] / (absHSR[1] == 0.0f ? 1 : absHSR[1]);

                        this.weaponKillsTotalNum.Text = newTotalKills.ToString();
                        this.weaponTotalHSRNum.Text = newTotalHSR.ToString("#0.#%");
                    } else { // If a session weapon for some reason does not exist, use just the total stats.
                        this.weaponKillsTotalNum.Text = totalWeapon.kills.ToString();
                        this.weaponTotalHSRNum.Text = ((float)totalWeapon.headShots / (float)totalWeapon.kills).ToString("#0.#%");
                    }

                    this.weaponTotalKDRNum.Text = (totalWeapon.kills / totalWeapon.deaths).ToString("0.0");
                    this.weaponTotalAccNum.Text = (totalWeapon.hitsCount / totalWeapon.fireCount).ToString("#0.#%");
                    this.panel1.Visible = true;
                }
            } else {
                this.panel1.Visible = false;
            }
        }

        private bool UpdateSessionStatsAndEventboard() {
            int kills = 0;
            int deaths = 0;
            int killHS = 0;
            int suicide = 0;
            Player player = m_statTracker.GetPlayer();
            List<EventLog> eventLog = m_statTracker.GetEventLog();

            int maxkbCount = 4;
            int killboardCount = eventLog.Count;
            if (killboardCount > maxkbCount)
                killboardCount = maxkbCount + 1;

            this.eventLogGridView.Rows.Clear();
            if (killboardCount <= 1) {
                ResetStatsView();
                return false;
            }

            this.eventLogGridView.Rows.Add(killboardCount - 1);

            for (int i = 0; i < eventLog.Count - 1; i++) {
                // Calculate overall session stats.
                if (eventLog[i].IsKill()) {
                    if (eventLog[i].defender == null || (eventLog[i].defender != null && eventLog[i].defender.faction != player.faction)) {
                        kills++;
                        killHS += eventLog[i].headshot ? 1 : 0;
                    }
                } else {
                    deaths++;
                    suicide += eventLog[i].suicide ? 1 : 0;
                }

                // Only display content which fits on the killboard.
                if (i >= killboardCount - 1)
                    continue;

                // Make the overlay killboard larger.
                this.eventLogGridView.Rows[i].Height += 6;

                // Display the event log.
                string eventName = "";
                if (eventLog[i].opponent != null) {
                    eventName = eventLog[i].opponent.fullName;
                } else {
                    eventName = "n/a";
                }

                // Event name.
                this.eventLogGridView.Rows[i].Cells[0].Value = eventName;

                // Event method.
                this.eventLogGridView.Rows[i].Cells[1].Value = eventLog[i].method;
                this.eventLogGridView.Rows[i].Cells[1].Style.ForeColor = Color.Beige;

                // Headshot image.
                if (eventLog[i].headshot) {
                    ((DataGridViewImageCell)eventLogGridView.Rows[i].Cells[2]).Value = Properties.Resources.hsImage;
                }

                // Set row color depending on kill or death.
                for (int j = 0; j < this.eventLogGridView.Rows[i].Cells.Count; j++) {
                    // Death.
                    if (eventLog[i].death || eventLog[i].suicide) {
                        // Friendly death.
                        if (eventLog[i].attacker != null && eventLog[i].attacker.faction == player.faction)
                            this.eventLogGridView.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                        // Enemy death.
                        else
                            this.eventLogGridView.Rows[i].Cells[j].Style.BackColor = Color.Red;
                    }
                        // Friendly kill.
                    else if (eventLog[i].defender != null && eventLog[i].defender.faction == player.faction)
                        this.eventLogGridView.Rows[i].Cells[j].Style.BackColor = Color.LightGreen;
                    // Enemy kill.
                    else
                        this.eventLogGridView.Rows[i].Cells[j].Style.BackColor = Color.Green;
                }
            }

            // Display session stats.
            this.killsNumber.Text = kills.ToString();
            this.deathsNum.Text = deaths.ToString();
            float kdr = (deaths == 0 ? (float)kills : (float)kills / (float)deaths);
            float hs = (kills == 0 ? 0.0f : (float)killHS / (float)kills);
            this.kdrNum.Text = kdr.ToString("0.0");
            this.hsrNum.Text = hs.ToString("#0.#%");

            return true;
        }

        public void UpdateStatsView() {

            if (UpdateSessionStatsAndEventboard())
                DisplayLastWeaponStats();
            this.eventLogGridView.ClearSelection();
        }

        Weapon GetLastWeapon(Dictionary<string, Weapon> weapons, List<EventLog> log) {
            // Search from most recent down. Skip deaths.
            for (int i = 0; i < log.Count; i++) {
                if (log[i].IsKill()) {
                    string id = log[i].isVehicle ? "V" : "";
                    id += log[i].methodID;
                    if (weapons.ContainsKey(id)) {
                        return weapons[id];
                    }
                }
            }
            Weapon weapon = new Weapon();
            weapon.Initialize();
            return weapon;
        }
    }
}
