﻿using System;
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
using Newtonsoft.Json;

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
                ((e1.attacker == null && e2.attacker == null) || (e1.attacker != null && e2.attacker != null && e1.attacker.fullName == e2.attacker.fullName)) &&
                ((e1.defender == null && e2.defender == null) || (e1.defender != null && e2.defender != null && e1.defender.fullName == e2.defender.fullName)));
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
            location = "";
            headshot = false;
            death = false;
            suicide = false;
            isVehicle = false;
        }

        public
            Player attacker,
            defender,
            opponent;           // The best one of the two for information that is not the users.

        public
            bool isVehicle;
        public
        string timeStamp,
            method,
            location;
        public long methodID;
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
            return (Key.vehicleId == 0 && Key.weaponId == 0) || (kills == 0.0f && deaths == 0.0f);
        }
        public object Clone() {
            return this.MemberwiseClone();
        }

        public long BestID {
            get {
                if (IsVehicle)
                    return Key.vehicleId;
                else
                    return Key.weaponId;
            }
        }

        public bool IsVehicle {
            get {
                if (Key.weaponId != 0)
                    return false;
                if (Key.vehicleId != 0)
                    return true;
                return false;
            }
        }

        public WeaponKey Key;
        public string name;
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

    public struct WeaponKey {
        public long weaponId;
        public long vehicleId;

        public WeaponKey(long weaponId, long vehicleId) {
            this.weaponId = weaponId;
            this.vehicleId = vehicleId;
        }

        public WeaponKey(long id, bool isVehicle) {
            if (isVehicle) {
                weaponId = 0;
                vehicleId = id;
            } else {
                weaponId = id;
                vehicleId = 0;
            }
        }

        internal class Comparer : IEqualityComparer<WeaponKey> {
            internal static readonly Comparer Instance = new Comparer();
            private Comparer() { }

            public bool Equals(WeaponKey x, WeaponKey y) {
                return x.weaponId == y.weaponId && x.vehicleId == y.vehicleId;
            }

            public int GetHashCode(WeaponKey x) {
                return x.weaponId.GetHashCode() ^ x.vehicleId.GetHashCode();
            }
        }
    }

    public class Player : ICloneable {
        public string fullName;
        public string name;
        public long id;
        public long faction;
        public string outfit;
        public int battleRank;
        public float battleRankPer;
        public float totalHeadshots;
        public bool isOnline;
        public kdrJson kdr;
        public Dictionary<WeaponKey, Weapon> weapons;

        public void CalculateHeadshots() {
            float val = 0;
            foreach (var weapon in weapons.Values) {
                val += weapon.headShots;
            }
            totalHeadshots = val;
        }
        public object Clone() {
            Player outPlayer = new Player();
            outPlayer = (Player)this.MemberwiseClone();
            outPlayer.weapons = new Dictionary<WeaponKey, Weapon>();
            foreach (KeyValuePair<WeaponKey, Weapon> element in this.weapons) {
                Weapon weapon = new Weapon();
                weapon = (Weapon)element.Value.Clone();
                outPlayer.weapons.Add(element.Key, weapon);
            }
            return outPlayer;
        }
    }

    public partial class StatTracker {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const string VEHICLE_OFFSET = "V";
        public class eventJson {
            public eventJson() {
                is_critical = "0";
                is_headshot = "0";
                timestamp = "0";
                table_type = "0";
            }
            public long attacker_character_id { get; set; }
            public long attacker_vehicle_id { get; set; }
            public long attacker_weapon_id { get; set; }
            public long character_id { get; set; }
            public string is_critical { get; set; }
            public string is_headshot { get; set; }
            public string timestamp { get; set; }
            public string table_type { get; set; }
            public long world_id { get; set; }
            public long zone_id { get; set; }
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
            public long item_id { get; set; }
            public string stat_name { get; set; }
            public int value { get; set; }
            public int value_nc { get; set; }
            public int value_tr { get; set; }
            public int value_vs { get; set; }
            public long vehicle_id { get; set; }
        }

        public class weaponListJson {
            public List<weaponJson> weapon_stat { get; set; }
            public List<weaponJson> weapon_stat_by_faction { get; set; }
        }

        public class outfitJson {
            public string name { get; set; }
            public string alias { get; set; }
            public string alias_lower { get; set; }
            public long leader_character_id { get; set; }
            public string member_count { get; set; }
            public string name_lower { get; set; }
            public long outfit_id { get; set; }
            public string time_created { get; set; }
            public string time_created_date { get; set; }
        }

        public class onlineStatusJson {
            public long character_id { get; set; }
            public string online_status { get; set; }
        }

        public class playerJson {
            public nameJson name { get; set; }
            public long faction_id { get; set; }
            public brJson battle_rank { get; set; }
            public weaponListJson stats { get; set; }
            public outfitJson outfit { get; set; }
        }

        public class WeaponsJson {
            public int returned { get; set; }
            public List<WeaponDataSheet> weapon_datasheet_list { get; set; }

            public class WeaponDataSheet {
                public long item_id { get; set; }
                public Item item { get; set; }

                public class Item {
                    [JsonConverter(typeof(BoolConverter))]
                    public bool is_vehicle_weapon { get; set; }
                    public Name name { get; set; }

                    public class Name {
                        public string en { get; set; }
                    }
                }
            }
        }

        public class VehiclesJson {
            public int returned { get; set; }
            public List<Vehicle> vehicle_list { get; set; }

            public class Vehicle {
                public long vehicle_id { get; set; }
                public Name name { get; set; }

                public class Name {
                    public string en { get; set; }
                }
            }
        }

        public class ItemsJson {
            public int returned { get; set; }
            public List<Item> item_list { get; set; }

            public class Item {
                public long item_id { get; set; }
                public Name name { get; set; }
                public class Name {
                    public string en { get; set; }
                }
            }
        }

        public class BoolConverter : JsonConverter {
            public override bool CanConvert(Type objectType) {
                return objectType == typeof(bool);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
                if (reader.TokenType == JsonToken.Null)
                    return false;
                if (reader.TokenType == JsonToken.Integer) {
                    int intValue = (int)reader.Value;
                    if (intValue == 0)
                        return false;
                    return true;
                }
                if (reader.TokenType == JsonToken.String) {
                    string stringValue = (string)reader.Value;
                    bool value;
                    if (stringValue == "1")
                        return true;
                    else if (stringValue == "0")
                        return false;
                    if (bool.TryParse((string)reader.Value, out value)) // works for "True", "False", "true", "false"
                        return value;
                    return false;
                }
                throw new JsonReaderException(string.Format("Unexcepted token {0}", reader.TokenType));
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
                writer.WriteValue(value);
            }
        }

        private HttpClient m_httpClient;
        internal async Task<string> GetAsyncRequest(string address) {
            log.Debug(address);
            if (m_httpClient == null) {
                ServicePointManager.DefaultConnectionLimit = 20000;
                m_httpClient = new System.Net.Http.HttpClient();
                m_httpClient.BaseAddress = new Uri("http://census.soe.com/get/ps2:v2/");
            }
            int attempts = 3;
            while (attempts > 0) {
                attempts--;
                using (HttpResponseMessage response = await m_httpClient.GetAsync(address)) {
                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();
                    else if (attempts < 1)
                        response.EnsureSuccessStatusCode(); // throws an exception when IsSuccessStatusCode is false
                    else
                        await Task.Delay(1000);
                }
            }
            return null; // Will never execute, but it makes the compiler happy
        }

        public async Task LoadCache() {
            Task t1 = RefreshWeaponCacheAsync();
            Task t2 = RefreshVehicleCacheAsync();
            Task t3 = RefreshItemCacheAsync();
            await Task.WhenAll(t1, t2, t3);
        }

        async Task RefreshWeaponCacheAsync() {
            if (m_weaponCache == null)
                m_weaponCache = new Dictionary<long, string>();
            else
                m_weaponCache.Clear();

            string result = await GetAsyncRequest("weapon_datasheet/?c:show=item_id&c:limit=10000&c:join=type:item^inject_at:item^show:name.en'is_vehicle_weapon^outer:0");
            WeaponsJson weapons = JsonConvert.DeserializeObject<WeaponsJson>(result);
            if (weapons != null && weapons.weapon_datasheet_list != null) {
                foreach (var weapon in weapons.weapon_datasheet_list) {
                    if (weapon.item != null && weapon.item.name != null) {
                        if (!m_weaponCache.ContainsKey(weapon.item_id))
                            m_weaponCache.Add(weapon.item_id, weapon.item.name.en);
                    }
                }
            }
        }

        async Task RefreshVehicleCacheAsync() {
            if (m_vehicleCache == null)
                m_vehicleCache = new Dictionary<long, string>();
            else
                m_vehicleCache.Clear();

            string result = await GetAsyncRequest("vehicle/?c:show=name.en,vehicle_id&c:limit=100&c:lang=en");
            VehiclesJson vehicles = JsonConvert.DeserializeObject<VehiclesJson>(result);
            if (vehicles != null && vehicles.vehicle_list != null) {
                foreach (var vehicle in vehicles.vehicle_list) {
                    if (vehicle.name != null) {
                        if (!m_vehicleCache.ContainsKey(vehicle.vehicle_id))
                            m_vehicleCache.Add(vehicle.vehicle_id, vehicle.name.en);
                    }
                }
            }
        }

        async Task RefreshItemCacheAsync() {
            if (m_allItemsCache == null)
                m_allItemsCache = new Dictionary<long, string>();
            else
                m_allItemsCache.Clear();
            log.Debug("Start loading all items cache");
            Task t1 = PopulateItemCacheAsync("item/?c:show=item_id,name.en&c:limit=5000&c:start=0");
            Task t2 = PopulateItemCacheAsync("item/?c:show=item_id,name.en&c:limit=5000&c:start=5000");
            Task t3 = PopulateItemCacheAsync("item/?c:show=item_id,name.en&c:limit=5000&c:start=10000");
            await Task.WhenAll(t1, t2, t3);
            log.Debug("Finished loading all items cache");
        }

        private async Task PopulateItemCacheAsync(string apiRequest) {
            string result = await GetAsyncRequest(apiRequest);
            ItemsJson items = await JsonConvert.DeserializeObjectAsync<ItemsJson>(result);
            if (items != null && items.item_list != null) {
                foreach (var item in items.item_list) {
                    if (item.name != null) {
                        m_allItemsCache[item.item_id] = item.name.en;
                        //if (!m_allItemsCache.ContainsKey(item.item_id))
                        //    m_allItemsCache.Add(item.item_id, item.name.en);
                    }
                }
            }
        }

        public async Task<string> GetWeaponNameAsync(Weapon weapon) {
            if (weapon.BestID == 0)
                return "Unknown";

            long id;
            string result;
            if (weapon.IsVehicle) {
                id = weapon.Key.vehicleId;
                if (m_vehicleCache.TryGetValue(id, out result))
                    return result;
                result = await GetAsyncRequest("vehicle/?vehicle_id=" + id + "&c:show=name.en");
                VehiclesJson vehicles = JsonConvert.DeserializeObject<VehiclesJson>(result);
                if (vehicles != null && vehicles.vehicle_list != null) {
                    var vehicle = vehicles.vehicle_list[0];
                    if (vehicle.name != null && vehicle.name.en != null) {
                        string name = vehicle.name.en;
                        m_vehicleCache.Add(id, name);
                        return name;
                    }
                }
            } else {
                id = weapon.Key.weaponId;
                if (m_weaponCache.TryGetValue(id, out result))
                    return result;
                if (m_allItemsCache.TryGetValue(id, out result)) {
                    m_weaponCache.Add(id, result);
                    return result;
                }
                log.DebugFormat("Can't find item_id {0}, checking API", id);
                result = await GetAsyncRequest("item/?item_id=" + id + "&c:show=name.en,item_id");
                ItemsJson items = JsonConvert.DeserializeObject<ItemsJson>(result);
                if (items != null && items.item_list != null && items.item_list.Count > 0) {
                    var item = items.item_list[0];
                    if (item.name != null && item.name.en != null) {
                        string name = item.name.en;
                        m_weaponCache.Add(id, name);
                        return name;
                    }
                }
            }
            return "Unknown";
        }

        async Task<Player> CreatePlayer(long id, bool updateWeapons = false, bool forceUpdate = false,
            bool skipHS_KDR = false) {

            Player existingPlayer = null;
            kdrJson kdr = null;

            // Check local cache.
            if (m_playerCache.ContainsKey(id)) {
                existingPlayer = m_playerCache[id];
                kdr = existingPlayer.kdr;

                if (!forceUpdate)
                    return existingPlayer;
            }

            // Assemble a player object taking json values and converting them to correct data types.
            playerJson pJson = await GetPlayerJson(id, updateWeapons);
            
            if (pJson == null) {
                return null;
            }

            Player player = new Player();

            // Do not overwrite the kdr unless requested to do so.
            if(!skipHS_KDR || kdr == null)
                kdr = await GetTotalKDR(id);

            player.battleRank = Int32.Parse(pJson.battle_rank.value);
            player.battleRankPer = float.Parse(pJson.battle_rank.percent_to_next) / 100.0f;
            player.kdr = kdr;
            player.name = pJson.name.first;
            if (pJson.outfit == null || pJson.outfit.alias.Length == 0) {
                player.outfit = "n/a";
                player.fullName = player.name;
            } else {
                player.outfit = pJson.outfit.name;
                if (string.IsNullOrEmpty(pJson.outfit.alias))
                    player.fullName = player.name;
                else
                    player.fullName = string.Format("[{0}] {1}", pJson.outfit.alias, player.name);
            }
            player.faction = pJson.faction_id;
            player.id = id;

            // PERFORMANCE Weapon stats may not have been requested.
            if (pJson.stats != null) {
                // Populate weapons.
                if (player.weapons == null)
                    player.weapons = new Dictionary<WeaponKey, Weapon>();

                // Update weapon specific stats.
                foreach (weaponJson jweapon in pJson.stats.weapon_stat) {
                    WeaponKey key = new WeaponKey(jweapon.item_id, jweapon.vehicle_id);
                    Weapon currentWep;
                    if (!player.weapons.TryGetValue(key, out currentWep)) {
                        currentWep = new Weapon();
                        currentWep.Key = key;
                    }
                    // Group the different stats under the same ID.
                    if (jweapon.stat_name == "weapon_fire_count")
                        currentWep.fireCount = jweapon.value;
                    else if (jweapon.stat_name == "weapon_hit_count")
                        currentWep.hitsCount = jweapon.value;
                    else if (jweapon.stat_name == "weapon_play_time")
                        currentWep.playTime = jweapon.value;
                    else if (jweapon.stat_name == "weapon_deaths")
                        currentWep.deaths = jweapon.value;

                    if (!currentWep.IsNull()) {
                        player.weapons[currentWep.Key] = currentWep;
                    }
                }

                // Update faction specific stats.
                foreach (weaponJson jweapon in pJson.stats.weapon_stat_by_faction) {
                    WeaponKey key = new WeaponKey(jweapon.item_id, jweapon.vehicle_id);
                    Weapon currentWep;
                    if (!player.weapons.TryGetValue(key, out currentWep)) {
                        currentWep = new Weapon();
                        currentWep.Key = key;
                    }
                    // Group the different stats under the same ID.
                    if (jweapon.stat_name == "weapon_kills") {
                        currentWep.killsNC = jweapon.value_nc;
                        currentWep.killsTR = jweapon.value_tr;
                        currentWep.killsVS = jweapon.value_vs;
                        currentWep.kills = currentWep.killsNC + currentWep.killsTR + currentWep.killsVS;
                    } else if (jweapon.stat_name == "weapon_headshots") {
                        currentWep.headShots = jweapon.value_nc + jweapon.value_tr + jweapon.value_vs;
                    }

                    if (!currentWep.IsNull()) {
                        player.weapons[currentWep.Key] = currentWep;
                    }
                }

                if (!skipHS_KDR) {
                    player.CalculateHeadshots();
                } else {
                    player.totalHeadshots = existingPlayer.totalHeadshots;
                }
            }

            // Add to local cache.
            m_playerCache[id] = player;
            return player;
        }

        // Updates the player cache with their online status.
        async Task<bool> CheckOnlineStatus() {
            List<string> sites = new List<string>();
            
            int siteIndex = 0;
            int maxUri = 1900;
            for(int i = 0; i < m_playerCache.Count; i++) {
                Player player = m_playerCache.ElementAt(i).Value;
                if (player != null && player.id != 0) {
                    // Add a new url.
                    if (sites.Count <= siteIndex) {
                        sites.Add("characters_online_status/?character_id=" + m_player.id);
                    } else {
                        sites[siteIndex] += "&character_id=" + player.id;
                    }
                }
                // If the url has become too long.
                if (sites.Count > siteIndex && sites[siteIndex].Length >= maxUri) {
                    siteIndex++;
                }
            }

            foreach (string site in sites) {
                string result = await GetAsyncRequest(site);
                Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

                if (jObject == null || !jObject.HasValues)
                    continue;

                Newtonsoft.Json.Linq.JToken jToken = jObject["characters_online_status_list"];
                List<onlineStatusJson> onlineList = new List<onlineStatusJson>();
                if (jToken != null && jToken.HasValues)
                    onlineList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<onlineStatusJson>>(jToken.ToString());

                // Update the player cache including m_player.
                foreach (onlineStatusJson status in onlineList) {
                    bool currentVal = int.Parse(status.online_status) == 1 ? true : false;
                    if (m_playerCache[status.character_id].isOnline != currentVal) {
                        m_playerCache[status.character_id].isOnline = currentVal;
                        // Make sure the tracker signals an update has occurred.
                        m_hasOnlineStatusChanged = true;
                    }
                }
            }
            return true;
        }

        async Task<playerJson> GetPlayerJson(long id, bool getWeapons = false) {
            string site = "character/" + id;

            if (getWeapons)
                site += "?c:resolve=weapon_stat,weapon_stat_by_faction,outfit";
            else
                site += "?c:resolve=outfit";

            string result = await GetAsyncRequest(site);
            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            if (jObject == null || !jObject.HasValues)
                return null;

            Newtonsoft.Json.Linq.JToken jToken = jObject["character_list"];

            if (jToken != null && jToken.HasValues)
                return Newtonsoft.Json.JsonConvert.DeserializeObject<playerJson>(jToken[0].ToString());

            return null;
        }

        async Task<kdrJson> GetTotalKDR(long id) {
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

        public async Task<bool> GetEventStats(int numEvents = 50) {
            string result = await GetAsyncRequest("characters_event/?character_id=" + m_userID + "&c:limit=" + numEvents + "&type=KILL,DEATH");

            Newtonsoft.Json.Linq.JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(result);

            if (jObject == null || !jObject.HasValues)
                return false;

            Newtonsoft.Json.Linq.JToken jToken = jObject["characters_event_list"];

            if (jToken == null || !jToken.HasValues)
                return false;

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
                Weapon weapon = new Weapon();
                weapon.Initialize();
                weapon.Key = new WeaponKey(jsonEvent.attacker_weapon_id, jsonEvent.attacker_vehicle_id);
                newEvent.methodID = weapon.BestID;
                newEvent.isVehicle = weapon.IsVehicle;
                newEvent.method = await GetWeaponNameAsync(weapon);
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
                    newEvent.opponent = m_player;
                }

                // Determine the best option for new information.
                if (attacker != null && attacker != m_player)
                    newEvent.opponent = attacker;

                if (defender != null && defender != m_player)
                    newEvent.opponent = defender;

                // Check if the new event being added is the latest event. A full check needs to be done
                // if the current event doesn't match. Rarely the site may first report the items in the wrong order.
                if (newEvent == m_currentEvent)
                    break;

                // Don't add the same event. The API can sometimes report it twice.
                if (m_eventLog.Contains(newEvent))
                    continue;

                // If an iteration has gone through then new information has been gathered.
                m_hasEventUpdated = true;

                // Determine the order in which to add the event.
                if (m_sessionStarted) {
                    if (i < m_eventLog.Count)
                        m_eventLog.Insert(i, newEvent);
                    else
                        m_eventLog.Add(newEvent);
                } else
                    m_eventLog.Add(newEvent);

                if (!m_preparingSession) {
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
            // Update loaded players' online status.
            await CheckOnlineStatus();

            return true;
        }

        public async Task GetPlayerWeapons() {
            // if m_player is valid then kdr and hsr will not be overwritten.
            m_player = await CreatePlayer(m_player.id, true, true, m_player != null);
            // Update stats of the session weapon other than headshots/kills.
            foreach (var currentWep in m_player.weapons.Values) {
                WeaponKey key = currentWep.Key;
                if (m_sessionStats.weapons.ContainsKey(key)) {
                    if (m_sessionStats.startPlayer.weapons.ContainsKey(key))
                        await AddSessionWeapon(currentWep, m_sessionStats.startPlayer.weapons[key], true);
                    else
                        m_sessionStats.startPlayer.weapons.Add(key, (Weapon)currentWep.Clone());
                }
            }
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

        // [hits, fired]
        public float[] GetWeaponACC(WeaponKey key, Dictionary<WeaponKey, Weapon> weapons) {
            float[] returnVal = { 0, 0 };

            Weapon weapon;
            if (weapons.TryGetValue(key, out weapon)) {
                returnVal[0] = weapon.hitsCount;
                returnVal[1] = weapon.fireCount;
            }

            return returnVal;
        }

        // [headshots, kills]
        public float[] GetWeaponHSR(WeaponKey key, Dictionary<WeaponKey, Weapon> weapons) {
            float[] returnVal = { 0, 0 };

            Weapon weapon;
            if (weapons.TryGetValue(key, out weapon)) {
                returnVal[0] = weapon.headShots;
                returnVal[1] = weapon.kills;
            }

            return returnVal;
        }

        async Task AddSessionWeapon(EventLog newEvent) {
            Weapon newWeapon = new Weapon();
            Weapon oldWeapon = new Weapon();
            oldWeapon.Initialize();
            newWeapon.Initialize();
            newWeapon.Key = new WeaponKey(newEvent.methodID, newEvent.isVehicle);
            newWeapon.name = await GetWeaponNameAsync(newWeapon);
            newWeapon.kills += newEvent.IsKill() ? 1 : 0;
            newWeapon.headShots += newEvent.headshot ? 1 : 0;

            // Add to total deaths.
            if (m_sessionStarted || m_countEvents) {
                if (!newEvent.IsKill()) {
                    UpdateOverallStats(0, 0, 1);
                }
                    // Update overall stats. Should only be called once overall stats have been set initially.
                else {
                    // Do not give kills or headshots for team kills.
                    // TODO: If you kill someone that is so brand new that even their ID does not come through,
                    // the defender will be null. So if the defender is null then a kill is currently counted. However,
                    // the defender might be a team mate. The only fix I know of is to keep track of this and check it
                    // occasionally and reverse the kill once the name is resolved and same faction is discovered.
                    if (newEvent.defender == null || (newEvent.defender != null && newEvent.defender.faction != m_player.faction))
                        UpdateOverallStats(newWeapon.kills, newWeapon.headShots, 0);
                }
            }
            // Add session weapon stats unless this event was a death or team kill.
            if (!newEvent.death && (newEvent.defender == null || (newEvent.defender != null && newEvent.defender.faction != m_player.faction)))
                await AddSessionWeapon(newWeapon, oldWeapon);
        }

        async Task AddSessionWeapon(Weapon updatedWeapon, Weapon oldWeapon, bool skipKillsHS = false) {
            float kills = updatedWeapon.kills - oldWeapon.kills;
            float hits = updatedWeapon.hitsCount - oldWeapon.hitsCount;
            float hs = updatedWeapon.headShots - oldWeapon.headShots;
            float fired = updatedWeapon.fireCount - oldWeapon.fireCount;

            if (kills < 0 || hits < 0 || hs < 0 || fired < 0)
                return;

            Weapon sessionWeapon;
            if (!m_sessionStats.weapons.TryGetValue(updatedWeapon.Key, out sessionWeapon)) {
                sessionWeapon = new Weapon();
                sessionWeapon.Initialize();
                sessionWeapon.Key = updatedWeapon.Key;
            }

            if (!skipKillsHS) {
                sessionWeapon.kills += kills;
                sessionWeapon.headShots += hs;
            }

            sessionWeapon.fireCount += fired;
            sessionWeapon.hitsCount += hits;
            sessionWeapon.name = await GetWeaponNameAsync(sessionWeapon);

            m_sessionStats.weapons[sessionWeapon.Key] = sessionWeapon;
        }

        void CancelInitialize() {
            m_initializing = false;
            m_preparingSession = false;
        }

        public async Task Initialize(int numEvents = 1) {
            if (m_userID != 0) {
                m_initializing = true;
                if (numEvents <= 0)
                    numEvents = 1;
                m_player = null;
                m_sessionStats.startPlayer = null;
                m_playerCache.Clear();
                ClearSession();
                Disconnect();
                m_lastEventFound = false;

                await LoadCache();

                // Get this player's information.
                m_player = await CreatePlayer(m_userID, true);

                if (m_player == null) {
                    CancelInitialize();
                    return;
                }

                // Copy player information so it can be compared to later.
                m_sessionStats.startPlayer = (Player)m_player.Clone();

                // Set start hsr and kdr values.
                UpdateOverallStats(0, 0, 0);

                // Load events.
                await GetEventStats(numEvents);

                // Make sure to start weapon changes off of recorded events.
                if (!m_preparingSession && !m_countEvents) {
                    foreach (var weapon in m_sessionStats.weapons) {
                        m_sessionStats.startSesWeapons[weapon.Key] = (Weapon)weapon.Value.Clone();
                    }
                }

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
            m_sessionStats.startSesWeapons.Clear();
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
