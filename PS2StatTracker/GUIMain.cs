using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PS2StatTracker
{
    public partial class GUIMain : Form {

        public string RemoveWhiteSpace(string input) {
            string output;
            output = input.Replace(" ", string.Empty);
            output = output.Replace("\t", string.Empty);
            output = output.Replace("\n", string.Empty);
            output = output.Replace("\r", string.Empty);
            return output;
        }
        // Removes white space before the first char and after the last char.
        public string RemoveWhiteSurroundingSpace(string input) {
            string output = "";
            input = input.Replace("\t", string.Empty);
            input = input.Replace("\n", string.Empty);
            input = input.Replace("\r", string.Empty);
            bool firstChar = false;

            for (int c = 0; c < input.Length; c++) {
                // Wait until finding the first char before creating the string.
                if (input[c] != ' ')
                    firstChar = true;

                if (firstChar) {
                    int nextC = c;

                    // If the char is a space check to see if there only spaces left.
                    if (input[c] == ' ') {
                        for (; nextC < input.Length; nextC++) {
                            if (input[nextC] != ' ') {
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

        StatTracker m_statTracker;
        GUIOverlay m_overlay;
        Color m_highColor;
        Color m_lowColor;
        List<Task> m_tasks;
        CancellationTokenSource m_cts;
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public GUIMain(StatTracker tracker) {
            InitializeComponent();
            m_tasks = new List<Task>();
            m_statTracker = tracker;
            m_overlay = null;
            m_cts = new CancellationTokenSource();

            // Load version.
            this.versionLabel.Text = GlobalVariables.PROGRAM_TITLE + " V " + GlobalVariables.VERSION_NUM;
            m_highColor = Color.FromArgb(0, 192, 0);
            m_lowColor = Color.Red;

            // Prevent X images showing up.
            ((DataGridViewImageColumn)this.eventLogGridView.Columns[0]).DefaultCellStyle.NullValue = null;
            ((DataGridViewImageColumn)this.eventLogGridView.Columns[4]).DefaultCellStyle.NullValue = null;
            // Handle mouse movement and resizing on borderless window.
            this.menuStrip1.MouseDown += OnMouseDown;
            AddMouseEventDown(this);

            // Check for new updates. Even though this is not awaited it still allows other program operation to run.
            UpdateCheckManagement(false);
        }

        // Recursively adds mouse events to controls.
        private void AddMouseEventDown(Control control) {
            control.MouseDown += OnMouseDown;
            foreach (Control ctrl in control.Controls) {
                // ControlContainer required for splitter sides. MouseDownEvent handles the splitter itself.
                if (ctrl is Label || ctrl is Panel || ctrl is ContainerControl) {
                    AddMouseEventDown(ctrl);
                }
            }
        }

        public string PercentString(float input, int digits = 3) {
            string digitCount = "";
            for (int i = 0; i < digits; i++) {
                digitCount += "#";
            }
            return ((input >= 0 ? "+" : "") + input.ToString("0." + digitCount) + "%");
        }

        void ShowUpdateText(String text) {
            this.updatingLabel.Text = text;
            this.updatingLabel.Visible = true;
            this.Refresh();
        }

        void HideUpdateText() {
            this.updatingLabel.Visible = false;
        }

        private void ClearUsers() {
            usernameTextBox.Items.Clear();
        }

        Bitmap GetOnOffBitmap(bool type) {
            if (type)
                return Properties.Resources.statusOn;
            return Properties.Resources.statusOff;
        }

        // Determines if a button should be active or visible based
        // on the current state of the program.
        private void ManageSessionButtons() {
            if (m_statTracker.PreparingSession() || m_statTracker.IsInitializing()) {
                this.resumeButton.Enabled = false;
                this.startSessionButton.Enabled = false;
            } else {
                this.resumeButton.Enabled = true;
                this.startSessionButton.Enabled = true;
            }
            if (m_statTracker.HasFoundLastEvent()) {
                if (m_statTracker.SessionStarted()) {
                    this.startSessionButton.Text = "End Session";
                    this.resumeButton.Visible = false;
                } else {
                    this.startSessionButton.Text = "Start";
                    this.resumeButton.Visible = true;
                }
            }
        }

        public void UpdateEventTextFields() {
            int kills = 0;
            int deaths = 0;
            int killHS = 0;
            int deathHS = 0;
            int suicide = 0;
            Player player = m_statTracker.GetPlayer();
            List<EventLog> eventLog = m_statTracker.GetEventLog();
            for (int i = 0; i < eventLog.Count - 1; i++) {
                if (eventLog[i].IsKill()) {
                    if (eventLog[i].defender == null || (eventLog[i].defender != null && eventLog[i].defender.faction != player.faction)) {
                        kills++;
                        killHS += eventLog[i].headshot ? 1 : 0;
                    }
                } else {
                    deaths++;
                    deathHS += eventLog[i].headshot ? 1 : 0;
                    suicide += eventLog[i].suicide ? 1 : 0;
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

        void UpdateOverallStatsDisplay() {
            // HSR
            Player player = m_statTracker.GetPlayer();

            float ratio = player.totalHeadshots / (float)player.kdr.kills;

            float dif = ratio - m_statTracker.GetSessionStats().startHSR;

            if (dif < 0)
                this.hsrGrowthLabel.ForeColor = m_lowColor;
            else if (dif > 0)
                this.hsrGrowthLabel.ForeColor = m_highColor;
            else
                this.hsrGrowthLabel.ForeColor = Color.White;

            this.totalKillsTextBox.Text = player.kdr.kills.ToString();
            this.totalHSTextBox.Text = ratio.ToString("#0.###%");
            this.hsrGrowthLabel.Text = PercentString(dif);

            // KDR
            ratio = (float)player.kdr.kills / (float)player.kdr.actualDeaths;

            dif = ratio - m_statTracker.GetSessionStats().startKDR;

            if (dif < 0)
                this.kdrGrowthLabel.ForeColor = m_lowColor;
            else if (dif > 0)
                this.kdrGrowthLabel.ForeColor = m_highColor;
            else
                this.kdrGrowthLabel.ForeColor = Color.White;

            this.totalDeathsTextBox.Text = player.kdr.actualDeaths.ToString();
            this.totalKDRTextBox.Text = ratio.ToString("0.000");
            this.kdrGrowthLabel.Text = dif.ToString("+#0.0000;-#0.0000");
        }

        public async Task UpdateWeaponTextFields(Dictionary<string, Weapon> weapons, DataGridView gridView) {
            if (weapons == null)
                return;

            // Maintain the sort order of the sorted column after a refresh.
            DataGridViewColumn oldSortedColumn = gridView.SortedColumn;
            System.ComponentModel.ListSortDirection direction;

            // If oldColumn is null, then the DataGridView is not currently sorted. 
            if (oldSortedColumn != null) {
                // Sort the same column again, reversing the SortOrder. 
                if (gridView.SortOrder == SortOrder.Ascending) {
                    direction = System.ComponentModel.ListSortDirection.Ascending;
                } else {
                    // Sort a new column and remove the old SortGlyph.
                    direction = System.ComponentModel.ListSortDirection.Descending;
                }
            } else {
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

            // Setting the first column to a fill weight and minimum width
            // while setting the other columns to conform to all cells allows
            // the first stats to show up and be scrolled, but all columns will stretch
            // to the size of the gridview even when resizing.
            gridView.Columns[0].FillWeight = 15;
            gridView.Columns[0].MinimumWidth = 100;
            // Sort the new gridview.
            int sortedIndex = 1;
            for (int i = 1; i < gridView.ColumnCount; i++) {
                // Resize columns.
                gridView.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                if (oldSortedColumn != null) {
                    if (gridView.Columns[i].HeaderText == oldSortedColumn.HeaderText) {
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

            for (int i = 0; i < weapons.Count; i++) {
                Weapon weapon = weapons.ElementAt(i).Value;
                string bestID = m_statTracker.GetBestWeaponID(weapon);

                // Will get either item or vehicle name.
                gridView.Rows[i].Cells[0].Value = await m_statTracker.GetItemName(m_statTracker.GetBestWeaponID(weapon));

                gridView.Rows[i].Cells[1].Value = weapon.kills;

                // Calculate acc change.
                float currentACC = weapon.hitsCount / weapon.fireCount;
                string accStr = currentACC.ToString("#0.###%");
                // Calculate hsr change.
                float currentHSR = weapon.headShots / weapon.kills;
                string hsrStr = currentHSR.ToString("#0.###%");
                if (gridView.Name == "sessionWeaponsGridView" && (m_statTracker.SessionStarted() ||
                    m_statTracker.CountingEvents())) {
                        string bestWeaponID = m_statTracker.GetBestWeaponID(weapon);
                    // HSR
                    float[] absHSR = m_statTracker.GetWeaponHSR(bestWeaponID, m_statTracker.GetSessionStats().startPlayer.weapons);
                    float oldTotalHSR = absHSR[0] / (absHSR[1] == 0.0f ? 1 : absHSR[1]);
                    float[] startHSR = m_statTracker.GetWeaponHSR(bestWeaponID, m_statTracker.GetSessionStats().startSesWeapons);
                    absHSR[0] += weapon.headShots - startHSR[0];
                    absHSR[1] += weapon.kills - startHSR[1];
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
                    float[] absACC = m_statTracker.GetWeaponACC(bestWeaponID,
                            m_statTracker.GetSessionStats().startPlayer.weapons);
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

        public async Task UpdateEventboard(){
            List<EventLog> eventLog = m_statTracker.GetEventLog();
            Player player = m_statTracker.GetPlayer();
            // Update killboard.
            this.eventLogGridView.Rows.Clear();
            if (eventLog.Count > 1) {
                this.eventLogGridView.Rows.Add(eventLog.Count - 1);

                for (int i = 0; i < eventLog.Count - 1; i++) {
                    string eventName = "";
                    if (eventLog[i].death) {
                        if (eventLog[i].attacker == null)
                            eventName = "n/a";
                        else
                        {
                            eventName = eventLog[i].attacker.fullName;
                        }
                    } else {
                        if (eventLog[i].defender == null)
                            eventName = "n/a";
                        else
                        {
                            eventName = eventLog[i].defender.fullName;
                        }
                    }

                    if (eventLog[i].opponent != null) {
                        // Online status.
                        ((DataGridViewImageCell)eventLogGridView.Rows[i].Cells[0]).Value = GetOnOffBitmap(eventLog[i].opponent.isOnline);
                        // Battle rank.
                        this.eventLogGridView.Rows[i].Cells[1].Value = eventLog[i].opponent.battleRank;
                        // Set kdr field.
                        float kdr = 0.0f;
                        if (eventLog[i].opponent.kdr != null) {
                            if((float)eventLog[i].opponent.kdr.actualDeaths != 0.0f)
                                kdr = (float)eventLog[i].opponent.kdr.kills / (float)eventLog[i].opponent.kdr.actualDeaths;
                        }
                        this.eventLogGridView.Rows[i].Cells[5].Value = kdr.ToString("0.0");
                    }
                    // Event name.
                    this.eventLogGridView.Rows[i].Cells[2].Value = eventName;

                    // Event method.
                    this.eventLogGridView.Rows[i].Cells[3].Value = eventLog[i].method;
                    this.eventLogGridView.Rows[i].Cells[3].Style.ForeColor = Color.Beige;

                    // Headshot image.
                    if (eventLog[i].headshot) {
                        ((DataGridViewImageCell)eventLogGridView.Rows[i].Cells[4]).Value = Properties.Resources.hsImage;
                    }

                    // Set row color depending on kill or death.
                    for (int j = 0; j < this.eventLogGridView.Rows[i].Cells.Count; j++) {
                        // Death.
                        if (eventLog[i].death || eventLog[i].suicide) {
                            // Friendly death.
                            if(eventLog[i].attacker != null && eventLog[i].attacker.faction == player.faction)
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
            }
            this.eventLogGridView.ClearSelection();

            UpdateEventTextFields();
            await UpdateWeaponTextFields(m_statTracker.GetSessionStats().weapons, this.sessionWeaponsGridView);

            UpdateOverallStatsDisplay();

            // Update player online status
            this.onlineStatusImage.Image = GetOnOffBitmap(m_statTracker.GetPlayer().isOnline);
            this.onlineStatusImage.Visible = true;

            UpdateOverlay();
        }

        void UpdateMiscFields() {
            Player player = m_statTracker.GetPlayer();

            if (player == null || player.kdr == null)
                return;

            // Times revived.
            float timesRevived = (float)player.kdr.actualDeaths - (float)player.kdr.reviveDeaths;

            this.timesRevivedTextBox.Text = timesRevived.ToString();

            // KDR with revives.
            float fakeRatio = (float)player.kdr.kills / (float)player.kdr.reviveDeaths;

            this.reviveKDRTextBox.Text = fakeRatio.ToString("0.000");

            // KDR inflation
            float realRatio = (float)player.kdr.kills / (float)player.kdr.actualDeaths;

            float inflation = (fakeRatio - realRatio) / realRatio;

            this.teamRelianceTextBox.Text = inflation.ToString("#0.###%");
        }

        //////////////////////////////////
        // Enables borderless dragging
        // For details: http://stackoverflow.com/questions/1592876
        //////////////////////////////////
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        private static extern bool ReleaseCapture();

        private void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs evt) {
            try {
                // Make sure the splitter can still move.
                if (sender.GetType() != typeof(SplitContainer)) {
                    if (evt.Button == MouseButtons.Left) {
                        ReleaseCapture();
                        SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                    }
                }
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        //////////////////////////////////
        // Enables borderless resizing
        // For details: http://stackoverflow.com/questions/1535826
        //////////////////////////////////
        protected override void WndProc(ref Message m) {
            const int wmNcHitTest = 0x84;
            const int htBottomLeft = 16;
            const int htBottomRight = 17;
            if (m.Msg == wmNcHitTest) {
                int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                Point pt = PointToClient(new Point(x, y));
                Size clientSize = ClientSize;
                if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16) {
                    m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }
            }
            base.WndProc(ref m);
        }

        // Adds the username to the dropdown list.
        void SaveUserName() {
            if (m_statTracker.GetUserID().Length == 0) return;
            string fullUser = m_statTracker.GetUserID() + " | " + m_statTracker.GetPlayer().fullName;
            if (!this.usernameTextBox.Items.Contains(fullUser)) {
                this.usernameTextBox.Items.Add(fullUser);
            }
        }

        // Sets text fields to visible and displays appropriate information.
        async Task PrepareSession() {
            this.hsrGrowthLabel.Visible = true;
            this.kdrGrowthLabel.Visible = true;
            SetPlayerInformation();
            await UpdateEventboard();
        }

        void ClearUser() {
            this.eventLogGridView.Rows.Clear();
            this.sessionWeaponsGridView.Columns.Clear();
            this.playerNameLabel.Visible = false;
        }

        void SetPlayerInformation() {
            this.playerNameLabel.Text = m_statTracker.GetPlayer().fullName;
            this.playerNameLabel.Visible = true;
            SaveUserName();
        }

        private void RegisterUserAndPrepareForInitialize() {
            string[] result = this.usernameTextBox.Text.Split('|');

            if (result.Length <= 0) {
                return;
            }

            if (!m_statTracker.SessionStarted())
                m_statTracker.StartPreparing();

            // Make sure buttons are not available while processing.
            ManageSessionButtons();

            m_statTracker.SetUserID(RemoveWhiteSpace(result[0]));

            ShowUpdateText("Initializing...");
        }

        private async Task UpdateTracker() {
            m_statTracker.IncreaseActiveSeconds(timer1.Interval / 1000);
            await m_statTracker.Update();
            if (m_statTracker.HasUpdated()) {
                await UpdateEventboard();
            }
            if (m_statTracker.HaveWeaponsUpdated()) {
                // Update overall weapons.
                await UpdateWeaponTextFields(m_statTracker.GetPlayer().weapons, this.weaponsGridView);
            }
        }

        private async Task CreateSession(GUISession session) {
                    // Shutdown an existing session.
                    if (m_statTracker.SessionStarted()) {
                        await m_statTracker.StartSession();
                        timer1.Stop();
                    }
                    RegisterUserAndPrepareForInitialize();
                    m_statTracker.SetCountEvents(session.countStatsCheckBox.Checked);
                    m_statTracker.StopPreparing();
                    await m_statTracker.Initialize((int)session.pastEventsNumber.Value + 1);
                    if (m_statTracker.HasInitialized()) {
                        await PrepareSession();
                        // Update overall weapons.
                        await UpdateWeaponTextFields(m_statTracker.GetPlayer().weapons, this.weaponsGridView);
                        UpdateMiscFields();
                        ManageSessionButtons();
                        HideUpdateText();
                    } else
                        ShowUpdateText("Invalid ID");
            ManageSessionButtons();
        }

        private async Task CreateNewSession() {
            if (!m_statTracker.SessionStarted())
                RegisterUserAndPrepareForInitialize();

            await m_statTracker.StartSession();

            if (!m_statTracker.HasInitialized())
                ShowUpdateText("Invalid ID");

            // Starting session for first time.
            if (m_statTracker.SessionStarted()) {
                await UpdateWeaponTextFields(m_statTracker.GetPlayer().weapons, this.weaponsGridView);
                UpdateMiscFields();
                ClearUser();
                await PrepareSession();
                timer1.Start();
                HideUpdateText();
            } else { // Ending a session.
                timer1.Stop();
            }
            ManageSessionButtons();
        }

        private async Task ResumeSession() {
            // Resumes a session.
            await m_statTracker.ResumeSession();
            if (m_statTracker.SessionStarted()) {
                await PrepareSession();
                timer1.Start();
                ManageSessionButtons();
            }
        }

        private async Task UpdateWeapons() {
            if (m_statTracker.SessionStarted()) {
                await m_statTracker.GetPlayerWeapons();
                await UpdateWeaponTextFields(m_statTracker.GetPlayer().weapons, this.weaponsGridView);
            }
        }

        private async Task UpdateEvents() {
            if (m_statTracker.SessionStarted()) {
                await m_statTracker.GetEventStats();
                await UpdateEventboard();
            }
        }

        public async Task CheckAndDownloadNewVersion(bool displayDialogue) {
            UpdateChecker updater = new UpdateChecker();
            VersionInfo info;
            info = await updater.CheckForNewVersion();
            if (info.isNew) {
                // Create update information to display.
                string updateResult = "Version " + info.version + " was found!\n\n";
                updateResult += info.updateInfo;
                GUIConfirm confirm = new GUIConfirm();
                confirm.infoLabel.Text = GlobalVariables.PROGRAM_TITLE + " Updater";
                confirm.textBox.Text = updateResult;
                confirm.infoLabel2.Text = "Do you wish to update?";
                confirm.ShowDialog(this);
                if (confirm.confirmed)
                    await updater.DownloadFile(true);
            } else {
                if (displayDialogue)
                    MessageBox.Show("The latest version is installed.");
            }
        }

        public async Task UpdateCheckManagement(bool displayDialogue) {
            // Wait for running tasks.
            await Task.WhenAll(m_tasks);
            // Create the new task and add it to the queue.
            Task task;
            m_tasks.Add(task = CheckAndDownloadNewVersion(displayDialogue));
            // Run the event.
            await Task.Run(() => task);
            // Remove it from the queue.
            m_tasks.Remove(task);
        }

        //////////////////////////////////
        // Button clicks.
        //////////////////////////////////

        private async void resumeButton_Click(object sender, EventArgs evt) {
            try {
                // Wait for running tasks.
                await Task.WhenAll(m_tasks);
                // Create the new task and add it to the queue.
                Task task;
                m_tasks.Add(task = ResumeSession());
                // Run the event.
                await Task.Run(() => task);
                // Remove it from the queue.
                m_tasks.Remove(task);
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private async void startSessionButton_Click(object sender, EventArgs evt) {
            try {
                // Wait for running tasks.
                await Task.WhenAll(m_tasks);
                // Create the new task and add it to the queue.
                Task task;
                m_tasks.Add(task = CreateNewSession());
                // Run the event.
                await Task.Run(() => task);
                // Remove it from the queue.
                m_tasks.Remove(task);
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private async void timer1_Tick(object sender, EventArgs evt) {
            try {
                // Wait for running tasks.
                await Task.WhenAll(m_tasks);
                // Create the new task and add it to the queue.
                Task task;
                m_tasks.Add(task = UpdateTracker());
                // Run the event.
                await Task.Run(() => task);
                // Remove it from the queue.
                m_tasks.Remove(task);

            } catch (Exception e) {
                timer1.Stop(); // HandleException won't return until user clicks Ok. Stop before-hand to prevent error message flood.
                Program.HandleException(this, e);
                timer1.Start();
            }
        }

        //////////////////////////////////
        // Tool strip button clicks.
        //////////////////////////////////

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            log.Info("Program quitting");
            Application.Exit();
        }

        private async void updateEventsToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                // Wait for running tasks.
                await Task.WhenAll(m_tasks);
                // Create the new task and add it to the queue.
                Task task;
                m_tasks.Add(task = UpdateEvents());
                // Run the event.
                await Task.Run(() => task);
                // Remove it from the queue.
                m_tasks.Remove(task);
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private async void updateWeaponsToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                // Wait for running tasks.
                await Task.WhenAll(m_tasks);
                // Create the new task and add it to the queue.
                Task task;
                m_tasks.Add(task = UpdateWeapons());
                // Run the event.
                await Task.Run(() => task);
                // Remove it from the queue.
                m_tasks.Remove(task);
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                GUIOptions config = new GUIOptions();
                config.ShowDialog(this);
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private void clearUsersToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                ClearUsers();
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                using (GUIAbout about = new GUIAbout()) {
                    about.SetTitle(GlobalVariables.PROGRAM_TITLE);
                    about.SetVersion("Version " + GlobalVariables.VERSION_NUM);
                    about.ShowDialog(this);
                }
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private void positiveColorsToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                this.colorDialog1.AllowFullOpen = true;
                if (this.colorDialog1.ShowDialog(this) == DialogResult.OK)
                    m_highColor = this.colorDialog1.Color;
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private void negativeColorsToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                this.colorDialog1.AllowFullOpen = true;
                if (this.colorDialog1.ShowDialog(this) == DialogResult.OK)
                    m_lowColor = this.colorDialog1.Color;
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private void cancelOperationToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                //CancelOperation();
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private async void createSessionToolStripMenuItem_Click(object sender, EventArgs evt) {
            try {
                using (GUISession session = new GUISession()) {
                    session.ShowDialog(this);
                    if (session.confirmed == true) {
                        // Wait for running tasks.
                        await Task.WhenAll(m_tasks);
                        // Create the new task and add it to the queue.
                        Task task;
                        m_tasks.Add(task = CreateSession(session));
                        // Run the event.
                        await Task.Run(() => task);
                        // Remove it from the queue.
                        m_tasks.Remove(task);
                    }
                }
            } catch (Exception e) {
                Program.HandleException(this, e);
            }
        }

        private void startOverlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_overlay == null)
            {
                m_overlay = new GUIOverlay(m_statTracker);
                m_overlay.FormClosed += new FormClosedEventHandler(overlay_FormClosed);
                m_overlay.Show(this);
            }
        }

        private void overlay_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_overlay.Dispose();
            m_overlay = null;
        }

        // Called from UpdateEvents and UpdateWeapons.
        private void UpdateOverlay()
        {
            if (m_overlay != null && m_statTracker.SessionStarted())
            {
                m_overlay.UpdateStats();
            }
        }

        private void GUIMain_FormClosing(object sender, FormClosingEventArgs e) {
            SaveConfig();
        }

        // Accept letters, digits, backspace and any other control keys (ctrl -c, etc)
        // Special characters like <> cause issues with API requests.
        private void usernameTextBox_KeyPress(object sender, KeyPressEventArgs e) {
            var regex = new System.Text.RegularExpressions.Regex(@"[^a-zA-Z0-9\s]");
            if (!Char.IsControl((char)e.KeyChar) && regex.IsMatch(((char)e.KeyChar).ToString())) {
                e.Handled = true;
            }
        }

        private async void updateToolStripMenuItem_Click(object sender, EventArgs e) {
            await UpdateCheckManagement(true);
        }
    }
}
