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
    public partial class GUIOverlay : Form
    {
        public GUIOverlay()
        {
            InitializeComponent();
            // Prevent X images showing up.
            ((DataGridViewImageColumn)this.eventLogGridView.Columns[2]).DefaultCellStyle.NullValue = null;

            this.eventLogGridView.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.eventLogGridView.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            this.eventLogGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            for (int i = 0; i < this.eventLogGridView.Columns.Count; i++)
            {
                this.eventLogGridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            this.eventLogGridView.Columns[0].MinimumWidth = 60;
            this.eventLogGridView.Columns[1].MinimumWidth = 60;
        }

        public void SetStats(Player player, string kills, string deaths, string kdr, string hsr,
            DataGridView killboard, Dictionary<string, Weapon> sessionWeapons, List<EventLog> eventLog)
        {
            this.killsNumber.Text = kills;
            this.deathsNum.Text = deaths;
            this.kdrNum.Text = float.Parse(kdr).ToString("0.00"); // Drops a decimal place.
            string hsrNoPercent = hsr.Remove(hsr.Length - 1, 1);
            this.hsrNum.Text = float.Parse(hsrNoPercent).ToString("0.00") + "%"; // Drops a decimal place.
            this.eventLogGridView.Rows.Clear();
            for (int i = 0; i < killboard.RowCount; i++)
            {
                this.eventLogGridView.Rows.Add((DataGridViewRow)killboard.Rows[i].Clone());
                this.eventLogGridView.Rows[i].Height += 6;
                for (int j = 0; j < killboard.Rows[i].Cells.Count; j++)
                {
                    this.eventLogGridView.Rows[i].Cells[j].Value = killboard.Rows[i].Cells[j].Value;
                }
                if (i >= 3)
                    break;
            }

            // Display Weapon info.
            Weapon lastWeapon = GetLastWeapon(sessionWeapons, eventLog);

            if (!lastWeapon.IsNull())
            {
                this.weaponName.Text = lastWeapon.name;
                // Session
                this.weaponKillsNum.Text = lastWeapon.kills.ToString();
                this.weaponHSRNum.Text = ((float)lastWeapon.headShots / (float)lastWeapon.kills).ToString("#0.###%");
                if (lastWeapon.fireCount > 0)
                {
                    this.weaponAccNum.Text = (lastWeapon.hitsCount / lastWeapon.fireCount).ToString("#0.###%");
                    this.weaponAccNum.Visible = true;
                    this.weaponAccLabel.Visible = true;
                }
                else
                {
                    this.weaponAccNum.Visible = false;
                    this.weaponAccLabel.Visible = false;
                }
                // Total
                string id = ((GUIMain)Owner).GetBestWeaponID(lastWeapon);
                if (player.weapons.ContainsKey(id))
                {
                    Weapon totalWeapon = player.weapons[id];
                    this.weaponKillsTotalNum.Text = totalWeapon.kills.ToString();
                    this.weaponTotalKDRNum.Text = (totalWeapon.kills / totalWeapon.deaths).ToString("0.00");
                    this.weaponTotalHSRNum.Text = (totalWeapon.headShots / totalWeapon.kills).ToString("#0.###%");
                    this.weaponTotalAccNum.Text = (totalWeapon.hitsCount / totalWeapon.fireCount).ToString("#0.###%");
                    this.panel1.Visible = true;
                }
            }
            else
            {
                this.panel1.Visible = false;
            }
            this.eventLogGridView.ClearSelection();
        }

        Weapon GetLastWeapon(Dictionary<string, Weapon> weapons, List<EventLog> log)
        {
            // Search from most recent down.
            for (int i = 0; i < log.Count; i++)
            {
                if (log[i].IsKill())
                {
                    string id = log[i].isVehicle ? "V" : "";
                    id += log[i].methodID;
                    if (weapons.ContainsKey(id))
                    {
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
