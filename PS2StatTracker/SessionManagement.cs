using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Net;
using System.IO;
using System.Net.Http;
using Newtonsoft;

namespace PS2StatTracker {
    public class kdrJson {
        public int kills,
            actualDeaths,
            reviveDeaths;
    }

    public struct EventLog {
        public static bool operator ==(EventLog e1, EventLog e2) {
            return MyEquals(e1, e2);
        }
        public static bool operator !=(EventLog e1, EventLog e2) {
            return !MyEquals(e1, e2);
        }
        public override bool Equals(object obj) {
            if (obj == null || !(obj is EventLog))
                return false;
            EventLog e2 = (EventLog)obj;
            return MyEquals(this, e2);
        }
        private static bool MyEquals(EventLog e1, EventLog e2) {
            return (e1.headshot == e2.headshot && e1.location == e2.location && e1.method == e2.method &&
                e1.suicide == e2.suicide && e1.timeStamp == e2.timeStamp &&
                ((e1.attacker == null && e2.attacker == null) || (e1.attacker != null && e2.attacker != null && e1.attacker.name == e2.attacker.name)) &&
                ((e1.defender == null && e2.defender == null) || (e1.defender != null && e2.defender != null && e1.defender.name == e2.defender.name)));
        }
        public override int GetHashCode() {
            return 0;
        }

        public bool IsKill() {
            return !death;
        }

        public void Initialize() {
            timeStamp = "";
            method = "";
            methodID = "";
            location = "";
            headshot = false;
            death = false;
            suicide = false;
            isVehicle = false;
        }

        public
            Player attacker,
            defender;

        public
            bool isVehicle;
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


    public struct Weapon : ICloneable {
        public void Initialize() {
            kills = fireCount = headShots = hitsCount = lastFireCount = playTime = deaths = 0.0f;
        }
        public bool IsNull() {
            if (kills == 0.0f && deaths == 0.0f) {
                return true;
            }
            return false;
        }
        public object Clone() {
            return this.MemberwiseClone();
        }
        public
            string id,
            vehicleId,
            name;
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

    public class Player : ICloneable {
        public string name,
            id,
            faction;
        public int battleRank;
        public float battleRankPer;
        public float totalHeadshots;
        public kdrJson kdr;
        public Dictionary<String,
            Weapon> weapons;

        public void CalculateHeadshots() {
            float val = 0;
            foreach (KeyValuePair<string, Weapon> weapon in weapons) {
                val += weapon.Value.headShots;
            }
            totalHeadshots = val;
        }
        public object Clone() {
            Player outPlayer = new Player();
            outPlayer = (Player)this.MemberwiseClone();
            outPlayer.weapons = new Dictionary<string, Weapon>();
            foreach (KeyValuePair<string, Weapon> element in this.weapons) {
                Weapon weapon = new Weapon();
                weapon = (Weapon)element.Value.Clone();
                outPlayer.weapons.Add(element.Key, weapon);
            }
            return outPlayer;
        }
    }

    public partial class StatTracker {
        public const string VEHICLE_OFFSET = "V";
        public class eventJson {
            public eventJson() {
                attacker_character_id = "0";
                attacker_vehicle_id = "0";
                character_id = "0";
                is_critical = "0";
                is_headshot = "0";
                timestamp = "0";
                table_type = "0";
                world_id = "0";
                zone_id = "0";
            }
            public string attacker_character_id { get; set; }
            public string attacker_vehicle_id { get; set; }
            public string attacker_weapon_id { get; set; }
            public string character_id { get; set; }
            public string is_critical { get; set; }
            public string is_headshot { get; set; }
            public string timestamp { get; set; }
            public string table_type { get; set; }
            public string world_id { get; set; }
            public string zone_id { get; set; }
        }

        public class nameJson {
            public string first { get; set; }
            public string first_lower { get; set; }
        }

        public class brJson {
            public string percent_to_next { get; set; }
            public string value { get; set; }
        }

        public class weaponJson {
            public string item_id { get; set; }
            public string stat_name { get; set; }
            public string value { get; set; }
            public string value_nc { get; set; }
            public string value_tr { get; set; }
            public string value_vs { get; set; }
            public string vehicle_id { get; set; }
        }

        public class weaponListJson {
            public List<weaponJson> weapon_stat { get; set; }
            public List<weaponJson> weapon_stat_by_faction { get; set; }
        }

        public class playerJson {
            public nameJson name { get; set; }
            public string faction_id { get; set; }
            public brJson battle_rank { get; set; }
            public weaponListJson stats { get; set; }
        }

        private HttpClient m_httpClient;
        internal async Task<string> GetAsyncRequest(string address) {
            if (m_httpClient == null) {
                ServicePointManager.DefaultConnectionLimit = 20000;
                m_httpClient = new System.Net.Http.HttpClient();
                m_httpClient.BaseAddress = new Uri("http://census.soe.com/get/ps2:v2/");
            }
            string result = null;
            using (HttpResponseMessage response = await m_httpClient.GetAsync(address)) {
                response.EnsureSuccessStatusCode();
                result = await response.Content.ReadAsStringAsync();
            }
            return result;
        }

        async Task<Player> CreatePlayer(string id, bool updateWeapons = false, bool forceUpdate = false) {
            // Check local cache.
            if (!forceUpdate && m_playerCache.ContainsKey(id))
                return m_playerCache[id];

            // Assemble a player object taking json values and converting them to correct data types.
            playerJson pJson = await GetPlayerJson(id, updateWeapons);
            Player player = new Player();
            if (pJson == null) {
                return null;
            }

            kdrJson kdr = await GetTotalKDR(id);
            player.battleRank = Int32.Parse(pJson.battle_rank.value);
            player.battleRankPer = float.Parse(pJson.battle_rank.percent_to_next) / 100.0f;
            player.kdr = kdr;
            player.name = pJson.name.first;
            player.faction = pJson.faction_id;
            player.id = id;

            // PERFORMANCE Weapon stats may not have been requested.
            if (pJson.stats != null) {
                // Populate weapons.
                if (player.weapons == null)
                    player.weapons = new Dictionary<string, Weapon>();

                // Update weapon specific stats.
                foreach (weaponJson jweapon in pJson.stats.weapon_stat) {
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
                    currentWep.vehicleId = jweapon.vehicle_id;

                    if (!currentWep.IsNull()) {
                        player.weapons[GetBestWeaponID(currentWep)] = currentWep;
                    }
                }

                // Update faction specific stats.
                foreach (weaponJson jweapon in pJson.stats.weapon_stat_by_faction) {
                    Weapon currentWep = player.weapons.ContainsKey(jweapon.item_id) ? player.weapons[jweapon.item_id] : new Weapon();
                    // Group the different stats under the same ID.
                    if (jweapon.stat_name == "weapon_kills") {
                        currentWep.killsNC = float.Parse(jweapon.value_nc);
                        currentWep.killsTR = float.Parse(jweapon.value_tr);
                        currentWep.killsVS = float.Parse(jweapon.value_vs);
                        currentWep.kills = currentWep.killsNC + currentWep.killsTR + currentWep.killsVS;
                    } else if (jweapon.stat_name == "weapon_headshots") {
                        currentWep.headShots = float.Parse(jweapon.value_nc) + float.Parse(jweapon.value_tr) +
                            float.Parse(jweapon.value_vs);
                    }

                    currentWep.id = jweapon.item_id;
                    currentWep.vehicleId = jweapon.vehicle_id;

                    if (!currentWep.IsNull()) {
                        player.weapons[GetBestWeaponID(currentWep)] = currentWep;
                    }
                }

                player.CalculateHeadshots();
            }

            // Add to local cache.
            m_playerCache[id] = player;
            return player;
        }

        public async Task<string> GetItemName(string id) {
            string useId = id;

            if (useId.Contains(VEHICLE_OFFSET)) {
                return await GetVehicleName(useId);
            }

            // Check local cache.
            if (m_itemCache.ContainsKey(id))
                return m_itemCache[id];

            string result = await GetAsyncRequest("item/?item_id=" + useId + "&c:show=name.en");

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
            if (jObject["item_list"].HasValues) {
                if (jObject["item_list"][0].HasValues && jObject["item_list"][0]["name"].HasValues) {
                    string name = jObject["item_list"][0]["name"]["en"].ToString();
                    // Add the object to the local cache.
                    m_itemCache[id] = name;
                    return name;
                }
            }
            return "Unknown";
        }

        public async Task<string> GetVehicleName(string id) {
            // Check local cache.
            if (id != null) {
                if (m_itemCache.ContainsKey(id))
                    return m_itemCache[id];

                string useId = id;

                if (useId.Contains(VEHICLE_OFFSET)) {
                    useId = useId.Remove(0, 1);
                }

                string result = await GetAsyncRequest("vehicle/?vehicle_id=" + useId + "&c:show=name.en");

                Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                if (jObject["vehicle_list"].HasValues) {
                    string name = jObject["vehicle_list"][0]["name"]["en"].ToString();
                    // Add the object to the local cache.
                    m_itemCache[VEHICLE_OFFSET + useId] = name;
                    return name;
                }
            }
            return "Unknown";
        }

        async Task<playerJson> GetPlayerJson(string id, bool getWeapons = false) {
            string site = "character/" + id;

            if (getWeapons)
                site += "?c:resolve=weapon_stat,weapon_stat_by_faction";

            string result = await GetAsyncRequest(site);
            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            if (!jObject.HasValues)
                return null;

            Newtonsoft.Json.Linq.JToken jToken = jObject["character_list"];

            if (jToken != null && jToken.HasValues)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<playerJson>(jToken[0].ToString());

            return null;
        }

        async Task<kdrJson> GetTotalKDR(string id) {
            kdrJson kdr = new kdrJson();

            // Get total real deaths. Not fake deaths. Get good.
            string result = await GetAsyncRequest("characters_stat/?character_id=" + id + "&stat_name=weapon_deaths&c:show=value_forever");

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            if (jObject.HasValues) {
                if (jObject["characters_stat_list"].ToArray().Count() == 0)
                    return kdr;

                Newtonsoft.Json.Linq.JToken jToken = jObject["characters_stat_list"].ToArray()[0];

                kdr.actualDeaths = Int32.Parse(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jToken.ToString())["value_forever"]);

                // Get kills.
                result = await GetAsyncRequest("characters_stat_history?character_id=" + id + "&stat_name=kills&c:show=all_time&c:limit=100");

                jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                Newtonsoft.Json.Linq.JToken[] jTokenArr = jObject["characters_stat_history_list"].ToArray();

                kdr.kills = Int32.Parse(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jTokenArr[0].ToString())["all_time"]);

                // Get revive deaths.
                result = await GetAsyncRequest("characters_stat_history?character_id=" + id + "&stat_name=deaths&c:show=all_time&c:limit=100");

                jObject = Newtonsoft.Json.Linq.JObject.Parse(result);
                if (jObject.HasValues) {
                    jTokenArr = jObject["characters_stat_history_list"].ToArray();
                    kdr.reviveDeaths = Int32.Parse(Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(jTokenArr[0].ToString())["all_time"]);
                }
            }
            return kdr;
        }

        public async Task GetEventStats(int numEvents = 50) {
            string result = await GetAsyncRequest("characters_event/?character_id=" + m_userID + "&c:limit=" + numEvents + "&type=KILL,DEATH");

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            if (jObject == null || !jObject.HasValues)
                return;

            Newtonsoft.Json.Linq.JToken jToken = jObject["characters_event_list"];

            if (jToken == null || !jToken.HasValues)
                return;

            // Create a list of each json object.
            List<eventJson> jsonEvents = new List<eventJson>();
            jsonEvents = Newtonsoft.Json.JsonConvert.DeserializeObject<List<eventJson>>(jToken.ToString());

            int i = 0;
            // Convert the json object into an event with IDs resolved.
            foreach (eventJson jsonEvent in jsonEvents) {
                // Initialize new event.
                EventLog newEvent = new EventLog();
                newEvent.Initialize();

                Player attacker = await CreatePlayer(jsonEvent.attacker_character_id);
                Player defender = await CreatePlayer(jsonEvent.character_id);

                // Weapon IDs take priority over vehicle IDs. Weapon IDs such as breaker rocket pods
                // also show up with vehicle IDs like Reavers. A Reaver should only count if no
                // other weapon was used.
                if (jsonEvent.attacker_vehicle_id != "0" && jsonEvent.attacker_weapon_id == "0") {
                    newEvent.methodID = jsonEvent.attacker_vehicle_id;
                    newEvent.method = await GetVehicleName(jsonEvent.attacker_vehicle_id);
                    newEvent.isVehicle = true;
                } else {
                    newEvent.methodID = jsonEvent.attacker_weapon_id;
                    newEvent.method = await GetItemName(jsonEvent.attacker_weapon_id);
                }
                newEvent.headshot = Int32.Parse(jsonEvent.is_headshot) == 1 ? true : false;
                newEvent.timeStamp = jsonEvent.timestamp;
                newEvent.attacker = attacker;
                newEvent.defender = defender;
                newEvent.death = defender == m_player;

                // Check for suicide.
                if ((attacker == m_player && defender == m_player)) {
                    newEvent.death = true;
                    newEvent.suicide = true;
                    newEvent.method = "Suicide";
                }

                // Check if the new event being added is the latest event. A full check needs to be done
                // if the current event doesn't match. Rarely the site may first report the items in the wrong order.
                if (newEvent == m_currentEvent)
                    break;

                // If an iteration has gone through then new information has been gathered.
                m_hasUpdated = true;

                // Don't add the same event. The API can sometimes report it twice.
                if (m_eventLog.Contains(newEvent))
                    continue;

                // Determine the order in which to add the event.
                if (m_sessionStarted) {
                    if (i < m_eventLog.Count)
                        m_eventLog.Insert(i, newEvent);
                    else
                        m_eventLog.Add(newEvent);
                } else
                    m_eventLog.Add(newEvent);

                if (!m_preparingSession || m_countEvents) {
                    // Add session weapon stats unless this event was a death or team kill.
                    if(i < numEvents - 1)
                        await AddSessionWeapon(newEvent);
                }
                i++;
            }

            // Display the killboard.
            // Only update the fields if a change in events occurred.
            if (i > 0) {
                m_currentEvent = m_eventLog[0];
                m_lastEventFound = true;
            }
        }

        public async Task GetPlayerWeapons() {
            //ShowUpdateText("Updating Weapons...");

            m_player = await CreatePlayer(m_player.id, true, true);
            // Update stats of the session weapon other than headshots/kills.
            foreach (KeyValuePair<string, Weapon> currentWep in m_player.weapons) {
                string id = GetBestWeaponID(currentWep.Value);
                if (m_sessionStats.weapons.ContainsKey(id)) {
                    if (m_sessionStats.startPlayer.weapons.ContainsKey(id))
                        await AddSessionWeapon(currentWep.Value, m_sessionStats.startPlayer.weapons[id], true);
                    else
                        m_sessionStats.startPlayer.weapons.Add(id, (Weapon)currentWep.Value.Clone());
                }
            }
            /*
            await UpdateWeaponTextFields(m_player.weapons, this.weaponsGridView);
            await UpdateWeaponTextFields(m_sessionWeapons, this.sessionWeaponsGridView);
            HideUpdateText();
            UpdateOverlay();*/
        }

        void UpdateOverallStats(float kills, float headshots, float deaths) {
            m_player.kdr.kills += (int)kills;

            // HSR
            m_player.totalHeadshots += headshots;

            float ratio = m_player.totalHeadshots / (float)m_player.kdr.kills;

            // Set the start of session head shot ratio.
            if (m_sessionStats.startHSR == 0.0f)
                m_sessionStats.startHSR = ratio;

            // KDR
            m_player.kdr.actualDeaths += (int)deaths;
            ratio = (float)m_player.kdr.kills / (float)m_player.kdr.actualDeaths;

            // Set the start of session kd ratio.
            if (m_sessionStats.startKDR == 0.0f)
                m_sessionStats.startKDR = ratio;
        }

        void CancelInitialize() {
            m_initializing = false;
            m_preparingSession = false;
        }

        public async Task Initialize(int numEvents = 1) {
            if (m_userID.Length > 0) {
                m_initializing = true;
                if (numEvents <= 0)
                    numEvents = 1;
                //ShowUpdateText("Initializing...");
                m_player = null;
                m_sessionStats.startPlayer = null;
                m_playerCache.Clear();
                ClearSession();
                Disconnect();
                m_lastEventFound = false;
                // Get this player's information.
                m_player = await CreatePlayer(m_userID, true);

                if (m_player == null) {
                    //ShowUpdateText("Invalid ID");
                    CancelInitialize();
                    return;
                }

                // Copy player information so it can be compared to later.
                m_sessionStats.startPlayer = (Player)m_player.Clone();

                // Set start hsr and kdr values.
                UpdateOverallStats(0, 0, 0);

                // Load events.
                await GetEventStats(numEvents);

                m_initialized = true;
                m_initializing = false;
            }
        }

        public async Task StartSession() {
            if (m_sessionStarted == false) {
                m_preparingSession = true;
                ClearSession();
                await Program.Retry(Initialize(), "Initializing", 2, true);
                if (m_lastEventFound) {
                    m_sessionStarted = true;
                }
            } else {
                if(!m_preparingSession)
                    EndSession();
            }

            m_preparingSession = false;
        }

        public async Task ResumeSession() {
            m_sessionStarted = true;
            await GetEventStats();
        }

        void ClearSession() {
            m_activeSeconds = 0;
            m_eventLog.Clear();
            m_sessionStats.weapons.Clear();
            m_sessionStats.startHSR = m_sessionStats.startKDR = 0.0f;
        }

        public void EndSession() {
            m_sessionStarted = false;
            m_activeSeconds = 0;
        }

        private void Disconnect() {
            EndSession();
            m_eventLog.Clear();
            m_sessionStats.weapons.Clear();
            m_player = null;
            m_sessionStats.startPlayer = null;
            m_currentEvent.Initialize();
        }
    }
}
