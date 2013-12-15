using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PS2StatTracker {
    public partial class StatTracker{
        public class SessionStats {
            public int headshotsReceived;
            public Player startPlayer;      // Start player stats.
            public Dictionary<string,
                Weapon> weapons;
            public Dictionary<string,
                Weapon> startSesWeapons;
            public float startHSR, startKDR;

            public SessionStats() {
                weapons = new Dictionary<string, Weapon>();
                startSesWeapons = new Dictionary<string, Weapon>();
                startPlayer = new Player();
            }
        }

        List<EventLog> m_eventLog;

        Dictionary<string,
            string> m_itemCache;            // Cache of item IDs to their name.
        Dictionary<string, Player>
            m_playerCache;                  // Cache of player IDs to their struct. 
        EventLog m_currentEvent;
        SessionStats m_sessionStats;
        Player m_player;                    // Current player stats.
        string m_userID;
        int m_activeSeconds;
        bool m_countEvents;                 // True if events should be counted toward stats even if a session has not started.
        bool m_lastEventFound;
        bool m_sessionStarted;
        bool m_initialized;
        bool m_initializing;
        bool m_preparingSession;
        bool m_hasEventUpdated;
        bool m_hasOnlineStatusChanged;
        bool m_weaponsUpdated;

        public SessionStats GetSessionStats() {
            return m_sessionStats;
        }

        public StatTracker() {
            m_eventLog = new List<EventLog>();
            m_playerCache = new Dictionary<string, Player>();
            m_itemCache = new Dictionary<string, string>();
            m_currentEvent = new EventLog();
            m_player = new Player();
            m_sessionStats = new SessionStats();
            m_userID = "";
            m_sessionStarted = false;
            m_lastEventFound = false;
            m_initialized = false;
            m_initializing = false;
            m_countEvents = false;
            m_preparingSession = false;
            m_activeSeconds = 0;

            m_currentEvent.Initialize();
        }

        public void Shutdown() {
            if (m_httpClient != null)
                m_httpClient.Dispose();
        }

        public bool IsInitializing() {
            return m_initializing;
        }

        public bool HasInitialized(){
            return m_initialized;
        }

        public bool SessionStarted() {
            return m_sessionStarted;
        }

        public void StartPreparing() {
            m_preparingSession = true;
        }

        public void StopPreparing() {
            m_preparingSession = false;
        }

        public bool PreparingSession() {
            return m_preparingSession;
        }

        public bool HasFoundLastEvent() {
            return m_lastEventFound;
        }

        public bool CountingEvents() {
            return m_countEvents;
        }

        public void SetCountEvents(bool val) {
            m_countEvents = val;
        }

        // Extracts the user ID.
        public void SetUserID(string name) {
            m_userID = name;
        }

        // Gets the user ID.
        public string GetUserID() {
            return m_userID;
        }

        public Player GetPlayer() {
            return m_player;
        }

        public List<EventLog> GetEventLog() {
            return m_eventLog;
        }

        public void IncreaseActiveSeconds(int seconds) {
            m_activeSeconds += seconds;
        }

        public EventLog GetCurrentEvent() {
            return m_currentEvent;
        }

        // Updates the program based on time passed.
        public async Task Update() {
            await Program.Retry(GetEventStats(), "Getting event stats", 2, false);

            // Update weapons every 30 minutes. Currently hardcoded. May eventually add sliders under options.
            // Also may eventually add options.
            if (m_activeSeconds % 1800 == 0) {
                await GetPlayerWeapons();
                m_weaponsUpdated = true;
            }
        }

        public bool HasEventUpdated() {
            bool returnVal = m_hasEventUpdated;
            m_hasEventUpdated = false;
            // Event updates will include online status updates.
            if (returnVal)
                m_hasOnlineStatusChanged = false;
            return returnVal;
        }

        public bool HasOnlineStatusChanged() {
            bool returnVal = m_hasOnlineStatusChanged;
            m_hasOnlineStatusChanged = false;
            return returnVal;
        }

        public bool HaveWeaponsUpdated() {
            bool returnVal = m_weaponsUpdated;
            m_weaponsUpdated = false;
            return returnVal;
        }
    }
}
