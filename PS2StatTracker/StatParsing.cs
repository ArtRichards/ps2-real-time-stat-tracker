using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace PS2StatTracker
{
    public partial class StatTracker
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

        // [hits, fired]
        public float[] GetWeaponACC(string name, Dictionary<string, Weapon> weapons)
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
        public float[] GetWeaponHSR(string id, Dictionary<string, Weapon> weapons)
        {
            float[] returnVal = { 0, 0 };

            if (weapons.ContainsKey(id))
            {
                returnVal[0] = weapons[id].headShots;
                returnVal[1] = weapons[id].kills;
            }

            return returnVal;
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
            if (!m_sessionStats.weapons.ContainsKey(id))
            {
                sessionWeapon = new Weapon();
                sessionWeapon.Initialize();
                sessionWeapon.id = updatedWeapon.id;
                sessionWeapon.vehicleId = updatedWeapon.vehicleId;
            }
            else
            {
                sessionWeapon = m_sessionStats.weapons[id];
            }

            if (!skipKillsHS)
            {
                sessionWeapon.kills += kills;
                sessionWeapon.headShots += hs;
            }

            sessionWeapon.fireCount += fired;
            sessionWeapon.hitsCount += hits;

            sessionWeapon.name = await GetItemName(GetBestWeaponID(sessionWeapon));

            m_sessionStats.weapons[id] = sessionWeapon;
        }
    }
}
