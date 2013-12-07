using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.IO;
using Newtonsoft;

namespace PS2StatTracker
{
    public partial class GUIMain
    {
        public class eventJson
        {
            public string attacker_character_id {get; set;}
            public string attacker_vehicle_id {get; set;}
            public string attacker_weapon_id {get; set;}
            public string character_id {get; set;}
            public string is_critical {get; set;}
            public string is_headshot {get; set;}
            public string timestamp { get; set; }
            public string table_type {get; set;}
            public string world_id { get; set; }
            public string zone_id { get; set; }
        }

        public class nameJson
        {
            public string first { get; set; }
            public string first_lower { get; set; } 
        }

        public class brJson
        {
            public string percent_to_next { get; set; }
            public string value { get; set; }
        }

        public class weaponJson
        {
            public string item_id { get; set; }
            public string stat_name { get; set; }
            public string value { get; set; }
            public string value_nc { get; set; }
            public string value_tr { get; set; }
            public string value_vs { get; set; }
            public string vehicle_id { get; set; }
        }

        public class weaponListJson
        {
            public List<weaponJson> weapon_stat { get; set; }
            public List<weaponJson> weapon_stat_by_faction { get; set; }
        }

        public class playerJson
        {
            public nameJson name { get; set; }
            public string faction_id { get; set; }
            public brJson battle_rank { get; set; }
            public weaponListJson stats { get; set; }
        }

        public class kdrJson
        {
            public int kills,
                actualDeaths,
                reviveDeaths;
        }

        public struct Weapon
        {
            public void Initialize()
            {
                kills = fireCount = headShots = hitsCount = lastFireCount = playTime = deaths = 0.0f;
            }
            public bool IsNull()
            {
                if (kills == 0.0f && deaths == 0.0f)
                {
                    return true;
                }
                return false;
            }
            public
                string id;
            public
                float kills,
                killsNC,
                killsTR,
                killsVS,
                deaths,
                fireCount,
                headShots,
                hitsCount,
                lastFireCount,
                playTime;

        }

        public class Player : ICloneable
        {
            public string name,
                id,
                faction;
            public int battleRank;
            public float battleRankPer;
            public float totalHeadshots;
            public kdrJson kdr;
            public Dictionary<String,
                Weapon> weapons;

            public void CalculateHeadshots()
            {
                float val = 0;
                foreach (KeyValuePair<string, Weapon> weapon in weapons)
                {
                    val += weapon.Value.headShots;
                }
                totalHeadshots = val;
            }
            public object Clone()
            {
                return this.MemberwiseClone();
            }
        }

        public struct EventLog
        {
            public static bool operator ==(EventLog e1, EventLog e2)
            {
                return (e1.headshot == e2.headshot && e1.location == e2.location && e1.method == e2.method &&
                    e1.suicide == e2.suicide && e1.timeStamp == e2.timeStamp && e1.attacker.name == e2.attacker.name &&
                    e1.defender.name == e2.defender.name);
            }
            public static bool operator !=(EventLog e1, EventLog e2)
            {
                return !(e1.headshot == e2.headshot && e1.location == e2.location && e1.method == e2.method &&
                    e1.suicide == e2.suicide && e1.timeStamp == e2.timeStamp && e1.attacker.name == e2.attacker.name &&
                    e1.defender.name == e2.defender.name);
            }

            public bool IsKill()
            {
                return !death;
            }

            public void Initialize()
            {
                timeStamp = "";
                method = "";
                methodID = "";
                location = "";
                headshot = false;
                death = false;
                suicide = false;
            }

            public
                Player attacker,
                defender;

            public
            string timeStamp,
                method,
                methodID,
                location;

            public
            bool headshot,
                death,
                suicide;
        };

        HttpWebRequest GetWebRequest(string site)
        {
            HttpWebRequest req = WebRequest.Create(site) as HttpWebRequest;
            req.Proxy = null;
            return req;
        }

        string ResponseString(HttpWebRequest req)
        {
            string result = null;
            using (HttpWebResponse resp = req.GetResponse()
                                          as HttpWebResponse)
            {
                StreamReader reader =
                    new StreamReader(resp.GetResponseStream());
                result = reader.ReadToEnd();
                reader.Close();
                resp.Close();
            }

            return result;
        }

        string SiteToString(string site)
        {
            return ResponseString(GetWebRequest(site));
        }

        Player GetPlayer(string id, bool updateWeapons = false, bool forceUpdate = false)
        {
            // Check local cache.
            if (!forceUpdate && m_playerCache.ContainsKey(id))
                return m_playerCache[id];

            // Assemble a player object taking json values and converting them to correct data types.
            playerJson pJson = GetPlayerJson(id, updateWeapons);
            Player player = new Player();
            if (pJson == null)
            {
                player.name = "N/A";
                return player;
            }
            kdrJson kdr = GetTotalKDR(id);
            player.battleRank = Int32.Parse(pJson.battle_rank.value);
            player.battleRankPer = float.Parse(pJson.battle_rank.percent_to_next) / 100.0f;
            player.kdr = kdr;
            player.name = pJson.name.first;
            player.faction = pJson.faction_id;
            player.id = id;

            // PERFORMANCE Weapon stats may not have been requested.
            if (pJson.stats != null)
            {
                // Populate weapons.
                if (player.weapons == null)
                    player.weapons = new Dictionary<string, Weapon>();

                // Update weapon specific stats.
                foreach (weaponJson jweapon in pJson.stats.weapon_stat)
                {
                    Weapon currentWep = player.weapons.ContainsKey(jweapon.item_id) ? player.weapons[jweapon.item_id] : new Weapon();
                    // Group the different stats under the same ID.
                    if (jweapon.stat_name == "weapon_fire_count")
                        currentWep.fireCount = float.Parse(jweapon.value);
                    else if (jweapon.stat_name == "weapon_hit_count")
                        currentWep.hitsCount = float.Parse(jweapon.value);
                    else if (jweapon.stat_name == "weapon_play_time")
                        currentWep.playTime = float.Parse(jweapon.value);
                    else if (jweapon.stat_name == "weapon_deaths")
                        currentWep.deaths = float.Parse(jweapon.value);

                    currentWep.id = jweapon.item_id;

                    if (!currentWep.IsNull())
                        player.weapons[jweapon.item_id] = currentWep;
                }

                // Update faction specific stats.
                foreach (weaponJson jweapon in pJson.stats.weapon_stat_by_faction)
                {
                    Weapon currentWep = player.weapons.ContainsKey(jweapon.item_id) ? player.weapons[jweapon.item_id] : new Weapon();
                    // Group the different stats under the same ID.
                    if (jweapon.stat_name == "weapon_kills")
                    {
                        currentWep.killsNC = float.Parse(jweapon.value_nc);
                        currentWep.killsTR = float.Parse(jweapon.value_tr);
                        currentWep.killsVS = float.Parse(jweapon.value_vs);
                        currentWep.kills = currentWep.killsNC + currentWep.killsTR + currentWep.killsVS;
                    }
                    else if (jweapon.stat_name == "weapon_headshots")
                    {
                        currentWep.headShots = float.Parse(jweapon.value_nc) + float.Parse(jweapon.value_tr) +
                            float.Parse(jweapon.value_vs);
                    }

                    currentWep.id = jweapon.item_id;

                    if (!currentWep.IsNull())
                        player.weapons[jweapon.item_id] = currentWep;
                }

                player.CalculateHeadshots();
            }

            // Add to local cache.
            m_playerCache[id] = player;

            return player;
        }

        string GetItemName(string id)
        {
            // Check local cache.
            if (m_itemCache.ContainsKey(id))
                return m_itemCache[id];

            string result = SiteToString("http://census.soe.com/get/ps2:v2/item/?item_id=" + id +
                "&c:show=name.en");

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
            if (jObject["item_list"].HasValues)
            {
                string name = jObject["item_list"][0]["name"]["en"].ToString();
                // Add the object to the local cache.
                m_itemCache[id] = name;
                return name;
            }
            return "Unknown";
        }

        playerJson GetPlayerJson(string id, bool getWeapons = false)
        {
            string site = "http://census.soe.com/get/ps2/character/" + id;
            
            if (getWeapons)
                site += "?c:resolve=weapon_stat,weapon_stat_by_faction";

            string result = SiteToString(site);
            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
            Newtonsoft.Json.Linq.JToken jToken = jObject["character_list"];

            if(jToken != null && jToken.HasValues)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<playerJson>(jToken[0].ToString());

            return null;
        }

        kdrJson GetTotalKDR(string id)
        {
            kdrJson kdr = new kdrJson();

            // Get total real deaths. Not fake deaths. Get good.
            string result = SiteToString("http://census.soe.com/get/ps2/characters_stat/?character_id=" + id +
                "&stat_name=weapon_deaths&c:show=value_forever");

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            if (jObject.HasValues)
            {
                if (jObject["characters_stat_list"].ToArray().Count() == 0)
                    return kdr;

                Newtonsoft.Json.Linq.JToken jToken = jObject["characters_stat_list"].ToArray()[0];

                kdr.actualDeaths = Int32.Parse(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jToken.ToString())["value_forever"]);

                // Get kills.
                result = SiteToString("http://census.soe.com/get/ps2/characters_stat_history?character_id=" + id +
                    "&stat_name=kills&c:show=all_time&c:limit=100");

                jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                Newtonsoft.Json.Linq.JToken[] jTokenArr = jObject["characters_stat_history_list"].ToArray();

                kdr.kills = Int32.Parse(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jTokenArr[0].ToString())["all_time"]);

                // Get revive deaths.
                result = SiteToString("http://census.soe.com/get/ps2/characters_stat_history?character_id=" + id +
                    "&stat_name=deaths&c:show=all_time&c:limit=100");

                jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                if (jObject.HasValues)
                {
                    jTokenArr = jObject["characters_stat_history_list"].ToArray();
                    kdr.reviveDeaths = Int32.Parse(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jTokenArr[0].ToString())["all_time"]);
                }
            }
            return kdr;
        }

        void ShowUpdateText(String text)
        {
            this.updatingLabel.Text = text;
            this.updatingLabel.Visible = true;
            this.Refresh();
            //m_dragging = false;
            //m_resizing = false;
        }

        void HideUpdateText()
        {
            this.updatingLabel.Visible = false;
        }

        void GetEventStats(int numEvents = 100)
        {
            //ShowUpdateText("Updating Events...");

            string result = SiteToString("http://census.soe.com/get/ps2/characters_event/?character_id=" + GetUserID(this.usernameTextBox.Text) +
                "&c:limit="+numEvents+"&type=KILL,DEATH");

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
            Newtonsoft.Json.Linq.JToken jToken = jObject["characters_event_list"];

            // Create a list of each json object.
            List<eventJson> jsonEvents = new List<eventJson>();
            jsonEvents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<eventJson>>(jToken.ToString());

            int i = 0;
            // Convert the json object into an event with IDs resolved.
            foreach (eventJson jsonEvent in jsonEvents)
            {
                // Initialize new event.
                EventLog newEvent = new EventLog();
                newEvent.Initialize();

                Player attacker = GetPlayer(jsonEvent.attacker_character_id);
                Player defender = GetPlayer(jsonEvent.character_id);
                newEvent.method = GetItemName(jsonEvent.attacker_weapon_id);
                newEvent.headshot = Int32.Parse(jsonEvent.is_headshot) == 1 ? true : false;
                newEvent.timeStamp = jsonEvent.timestamp;
                newEvent.attacker = attacker;
                newEvent.defender = defender;
                newEvent.methodID = jsonEvent.attacker_weapon_id;
                newEvent.death = defender == m_player;

                // Check for suicide.
                if ((attacker == m_player && defender == m_player))
                {
                    newEvent.death = true;
                    newEvent.suicide = true;
                    newEvent.method = "Suicide";
                }

                // Check if the new event being added is the latest event. A full check needs to be done
                // if the current event doesn't match. Rarely the site may first report the items in the wrong order.
                if (newEvent == m_currentEvent)
                    break;

                // Don't add the same event. The API can sometimes report it twice.
                if (m_eventLog.Contains(newEvent))
                    continue;

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
                i++;
            }

            // Display the killboard.
            if (m_eventLog.Count > 0)
            {
                m_currentEvent = m_eventLog[0];
                // Update killboard.
                this.eventLogGridView.Rows.Clear();
                this.eventLogGridView.Rows.Add(m_eventLog.Count);
                i = 0;
                foreach (EventLog eventlog in m_eventLog)
                {
                    string eventName = eventlog.death ? eventlog.attacker.name : eventlog.defender.name;

                    this.eventLogGridView.Rows[i].Cells[0].Value = eventName;
                    this.eventLogGridView.Rows[i].Cells[1].Value = eventlog.method;
                    this.eventLogGridView.Rows[i].Cells[1].Style.ForeColor = Color.Beige;
                    if (eventlog.headshot)
                        ((DataGridViewImageCell)eventLogGridView.Rows[i].Cells[2]).Value = Properties.Resources.hsImage;

                    // Set row color depending on kill or death.
                    for (int j = 0; j < this.eventLogGridView.Rows[i].Cells.Count; j++)
                    {
                        if (eventlog.death || eventlog.suicide) // Death.
                            this.eventLogGridView.Rows[i].Cells[j].Style.BackColor = Color.Red;
                        else if (eventlog.defender.faction == m_player.faction) // Friendly kill.
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
            //HideUpdateText();
        }

        void GetPlayerWeapons()
        {
            ShowUpdateText("Updating Weapons...");
            m_player = GetPlayer(m_player.id, true, true);
            UpdateWeaponTextFields(m_player.weapons, this.weaponsGridView);
            UpdateWeaponTextFields(m_sessionWeapons, this.sessionWeaponsGridView);
            HideUpdateText();
        }

        void CancelOperation()
        {
        }

        void Initialize(int numEvents = 1)
        {
            if (this.usernameTextBox.Text.Length > 0)
            {
                if (numEvents <= 0) numEvents = 1;
                ShowUpdateText("Initializing...");
                m_player = null;
                m_startPlayer = null;
                m_playerCache.Clear();
                m_sessionStartHSR = 0.0f;
                m_sessionStartKDR = 0.0f;
                Disconnect();
                m_lastEventFound = false;
                m_userID = "";
                // Get this player's information.
                m_player = GetPlayer(GetUserID(this.usernameTextBox.Text), true);
                this.playerNameLabel.Text = m_player.name;
                this.playerNameLabel.Visible = true;
                // Copy player information so it can be compared to later.
                m_startPlayer = (Player)m_player.Clone();
                UpdateWeaponTextFields(m_player.weapons, this.weaponsGridView);
                // Update total stats.
                UpdateOverallStats(0.0f, 0.0f, 0.0f);
                // Update text relating to API.
                UpdateMiscFields();
                // Load events.
                GetEventStats(numEvents);
                if (m_countEvents)
                {
                    this.hsrGrowthLabel.Visible = true;
                    this.kdrGrowthLabel.Visible = true;
                }
                //if (!this.startSessionButton.Enabled && m_lastEventFound)
                //    this.startSessionButton.Enabled = true;
                SaveUserName();
                m_initialized = true;
                HideUpdateText();
            }
        }

        private void ManageSessionButtons()
        {
            if (m_lastEventFound)
            {
                if (m_sessionStarted)
                {
                    this.startSessionButton.Text = "End Session";
                    this.connectButton.Visible = false;
                }
                else
                {
                    this.startSessionButton.Text = "Start";
                    this.connectButton.Visible = true;
                }
            }
        }

        private void ResumeSession()
        {
            m_sessionStarted = true;
            this.hsrGrowthLabel.Visible = true;
            this.kdrGrowthLabel.Visible = true;
            GetEventStats();
            timer1.Start();
            UpdateEventTextFields();
            UpdateOverallStats(0.0f, 0.0f, 0.0f);
            ManageSessionButtons();
        }

        private void StartSession()
        {
            if (m_sessionStarted == false)
            {
                if (m_lastEventFound)
                {
                    ResetSession();
                    this.hsrGrowthLabel.Visible = true;
                    this.kdrGrowthLabel.Visible = true;
                    m_sessionStartHSR = m_sessionStartKDR = 0.0f;
                    UpdateEventTextFields();
                    UpdateOverallStats(0.0f, 0.0f, 0.0f);
                }
            }
            else
            {
                EndSession();
            }

            ManageSessionButtons();
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
            ManageSessionButtons();
        }

        private void Disconnect()
        {
            EndSession();
            m_eventLog.Clear();
            m_sessionWeapons.Clear();
            m_player = null;
            m_startPlayer = null;
            m_currentEvent.Initialize();
            this.eventLogGridView.Rows.Clear();
            this.sessionWeaponsGridView.Columns.Clear();
            //this.startSessionButton.Enabled = false;
            this.playerNameLabel.Visible = false;
        }
    }
}
