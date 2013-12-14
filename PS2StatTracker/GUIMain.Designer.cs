namespace PS2StatTracker
{
    partial class GUIMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                if (m_statTracker != null)
                    m_statTracker.Shutdown();
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIMain));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.resumeButton = new System.Windows.Forms.Button();
            this.killsTextBox = new System.Windows.Forms.RichTextBox();
            this.killsLabel = new System.Windows.Forms.Label();
            this.deathsLabel = new System.Windows.Forms.Label();
            this.deathsTextBox = new System.Windows.Forms.RichTextBox();
            this.hsKillsLabel = new System.Windows.Forms.Label();
            this.hsKillsTextBox = new System.Windows.Forms.RichTextBox();
            this.hsDeathsLabel = new System.Windows.Forms.Label();
            this.hsDeathsTextBox = new System.Windows.Forms.RichTextBox();
            this.kdrLabel = new System.Windows.Forms.Label();
            this.kdrTextBox = new System.Windows.Forms.RichTextBox();
            this.hsLabel = new System.Windows.Forms.Label();
            this.hsTextBox = new System.Windows.Forms.RichTextBox();
            this.usernameLabel = new System.Windows.Forms.Label();
            this.killBoardLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.hsrGrowthLabel = new System.Windows.Forms.Label();
            this.totalHSLabel = new System.Windows.Forms.Label();
            this.totalHSTextBox = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.sessionWeaponsTab = new System.Windows.Forms.TabPage();
            this.sessionWeaponsGridView = new System.Windows.Forms.DataGridView();
            this.allWeaponsTab = new System.Windows.Forms.TabPage();
            this.weaponsGridView = new System.Windows.Forms.DataGridView();
            this.miscTab = new System.Windows.Forms.TabPage();
            this.teamImpactLabel = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.teamRelianceLabel = new System.Windows.Forms.Label();
            this.teamRelianceTextBox = new System.Windows.Forms.RichTextBox();
            this.revivesTakenLabel = new System.Windows.Forms.Label();
            this.kdrReviveLabel = new System.Windows.Forms.Label();
            this.timesRevivedTextBox = new System.Windows.Forms.RichTextBox();
            this.reviveKDRTextBox = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.startSessionButton = new System.Windows.Forms.Button();
            this.updatingLabel = new System.Windows.Forms.Label();
            this.playerNameLabel = new System.Windows.Forms.Label();
            this.totalKillsLabel = new System.Windows.Forms.Label();
            this.totalKillsTextBox = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.kdrGrowthLabel = new System.Windows.Forms.Label();
            this.totalKDRLabel = new System.Windows.Forms.Label();
            this.totalKDRTextBox = new System.Windows.Forms.RichTextBox();
            this.totalDeathsLabel = new System.Windows.Forms.Label();
            this.totalDeathsTextBox = new System.Windows.Forms.RichTextBox();
            this.sessionLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateEventsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateWeaponsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createSessionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelOperationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.streamingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startOverlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.positiveColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negativeColorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearUsersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventLogGridView = new System.Windows.Forms.DataGridView();
            this.brCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.playerCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hsCol = new System.Windows.Forms.DataGridViewImageColumn();
            this.kdrCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usernameTextBox = new System.Windows.Forms.ComboBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.sessionWeaponsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sessionWeaponsGridView)).BeginInit();
            this.allWeaponsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weaponsGridView)).BeginInit();
            this.miscTab.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLogGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // resumeButton
            // 
            this.resumeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resumeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.resumeButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resumeButton.Location = new System.Drawing.Point(250, 0);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(83, 23);
            this.resumeButton.TabIndex = 0;
            this.resumeButton.Text = "Resume";
            this.resumeButton.UseVisualStyleBackColor = true;
            this.resumeButton.Visible = false;
            this.resumeButton.Click += new System.EventHandler(this.resumeButton_Click);
            // 
            // killsTextBox
            // 
            this.killsTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.killsTextBox.Location = new System.Drawing.Point(6, 29);
            this.killsTextBox.Multiline = false;
            this.killsTextBox.Name = "killsTextBox";
            this.killsTextBox.ReadOnly = true;
            this.killsTextBox.Size = new System.Drawing.Size(47, 23);
            this.killsTextBox.TabIndex = 2;
            this.killsTextBox.Text = "";
            // 
            // killsLabel
            // 
            this.killsLabel.AutoSize = true;
            this.killsLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.killsLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.killsLabel.Location = new System.Drawing.Point(3, 13);
            this.killsLabel.Name = "killsLabel";
            this.killsLabel.Size = new System.Drawing.Size(31, 14);
            this.killsLabel.TabIndex = 3;
            this.killsLabel.Text = "Kills";
            // 
            // deathsLabel
            // 
            this.deathsLabel.AutoSize = true;
            this.deathsLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deathsLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.deathsLabel.Location = new System.Drawing.Point(3, 58);
            this.deathsLabel.Name = "deathsLabel";
            this.deathsLabel.Size = new System.Drawing.Size(46, 14);
            this.deathsLabel.TabIndex = 5;
            this.deathsLabel.Text = "Deaths";
            // 
            // deathsTextBox
            // 
            this.deathsTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deathsTextBox.Location = new System.Drawing.Point(6, 74);
            this.deathsTextBox.Multiline = false;
            this.deathsTextBox.Name = "deathsTextBox";
            this.deathsTextBox.ReadOnly = true;
            this.deathsTextBox.Size = new System.Drawing.Size(47, 23);
            this.deathsTextBox.TabIndex = 4;
            this.deathsTextBox.Text = "";
            // 
            // hsKillsLabel
            // 
            this.hsKillsLabel.AutoSize = true;
            this.hsKillsLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hsKillsLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.hsKillsLabel.Location = new System.Drawing.Point(77, 13);
            this.hsKillsLabel.Name = "hsKillsLabel";
            this.hsKillsLabel.Size = new System.Drawing.Size(81, 14);
            this.hsKillsLabel.TabIndex = 7;
            this.hsKillsLabel.Text = "by Headshots";
            // 
            // hsKillsTextBox
            // 
            this.hsKillsTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hsKillsTextBox.Location = new System.Drawing.Point(80, 29);
            this.hsKillsTextBox.Multiline = false;
            this.hsKillsTextBox.Name = "hsKillsTextBox";
            this.hsKillsTextBox.ReadOnly = true;
            this.hsKillsTextBox.Size = new System.Drawing.Size(47, 23);
            this.hsKillsTextBox.TabIndex = 6;
            this.hsKillsTextBox.Text = "";
            // 
            // hsDeathsLabel
            // 
            this.hsDeathsLabel.AutoSize = true;
            this.hsDeathsLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hsDeathsLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.hsDeathsLabel.Location = new System.Drawing.Point(77, 58);
            this.hsDeathsLabel.Name = "hsDeathsLabel";
            this.hsDeathsLabel.Size = new System.Drawing.Size(94, 14);
            this.hsDeathsLabel.TabIndex = 9;
            this.hsDeathsLabel.Text = "from Headshots";
            // 
            // hsDeathsTextBox
            // 
            this.hsDeathsTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hsDeathsTextBox.Location = new System.Drawing.Point(80, 74);
            this.hsDeathsTextBox.Multiline = false;
            this.hsDeathsTextBox.Name = "hsDeathsTextBox";
            this.hsDeathsTextBox.ReadOnly = true;
            this.hsDeathsTextBox.Size = new System.Drawing.Size(47, 23);
            this.hsDeathsTextBox.TabIndex = 8;
            this.hsDeathsTextBox.Text = "";
            // 
            // kdrLabel
            // 
            this.kdrLabel.AutoSize = true;
            this.kdrLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kdrLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.kdrLabel.Location = new System.Drawing.Point(3, 100);
            this.kdrLabel.Name = "kdrLabel";
            this.kdrLabel.Size = new System.Drawing.Size(28, 14);
            this.kdrLabel.TabIndex = 13;
            this.kdrLabel.Text = "KDR";
            // 
            // kdrTextBox
            // 
            this.kdrTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kdrTextBox.Location = new System.Drawing.Point(6, 116);
            this.kdrTextBox.Multiline = false;
            this.kdrTextBox.Name = "kdrTextBox";
            this.kdrTextBox.ReadOnly = true;
            this.kdrTextBox.Size = new System.Drawing.Size(47, 23);
            this.kdrTextBox.TabIndex = 12;
            this.kdrTextBox.Text = "";
            // 
            // hsLabel
            // 
            this.hsLabel.AutoSize = true;
            this.hsLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hsLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.hsLabel.Location = new System.Drawing.Point(77, 100);
            this.hsLabel.Name = "hsLabel";
            this.hsLabel.Size = new System.Drawing.Size(21, 14);
            this.hsLabel.TabIndex = 17;
            this.hsLabel.Text = "HS";
            // 
            // hsTextBox
            // 
            this.hsTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hsTextBox.Location = new System.Drawing.Point(80, 116);
            this.hsTextBox.Multiline = false;
            this.hsTextBox.Name = "hsTextBox";
            this.hsTextBox.ReadOnly = true;
            this.hsTextBox.Size = new System.Drawing.Size(47, 23);
            this.hsTextBox.TabIndex = 16;
            this.hsTextBox.Text = "";
            // 
            // usernameLabel
            // 
            this.usernameLabel.AutoSize = true;
            this.usernameLabel.BackColor = System.Drawing.Color.Transparent;
            this.usernameLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.usernameLabel.Location = new System.Drawing.Point(3, 4);
            this.usernameLabel.Name = "usernameLabel";
            this.usernameLabel.Size = new System.Drawing.Size(55, 14);
            this.usernameLabel.TabIndex = 19;
            this.usernameLabel.Text = "Player ID";
            // 
            // killBoardLabel
            // 
            this.killBoardLabel.AutoSize = true;
            this.killBoardLabel.BackColor = System.Drawing.Color.Transparent;
            this.killBoardLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.killBoardLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.killBoardLabel.Location = new System.Drawing.Point(3, 58);
            this.killBoardLabel.Name = "killBoardLabel";
            this.killBoardLabel.Size = new System.Drawing.Size(60, 14);
            this.killBoardLabel.TabIndex = 20;
            this.killBoardLabel.Text = "Kill Board";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.killsLabel);
            this.panel1.Controls.Add(this.killsTextBox);
            this.panel1.Controls.Add(this.deathsTextBox);
            this.panel1.Controls.Add(this.deathsLabel);
            this.panel1.Controls.Add(this.hsKillsTextBox);
            this.panel1.Controls.Add(this.hsKillsLabel);
            this.panel1.Controls.Add(this.hsLabel);
            this.panel1.Controls.Add(this.hsDeathsTextBox);
            this.panel1.Controls.Add(this.hsTextBox);
            this.panel1.Controls.Add(this.hsDeathsLabel);
            this.panel1.Controls.Add(this.kdrTextBox);
            this.panel1.Controls.Add(this.kdrLabel);
            this.panel1.Location = new System.Drawing.Point(12, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 155);
            this.panel1.TabIndex = 23;
            // 
            // hsrGrowthLabel
            // 
            this.hsrGrowthLabel.AutoSize = true;
            this.hsrGrowthLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hsrGrowthLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.hsrGrowthLabel.Location = new System.Drawing.Point(126, 28);
            this.hsrGrowthLabel.Name = "hsrGrowthLabel";
            this.hsrGrowthLabel.Size = new System.Drawing.Size(43, 14);
            this.hsrGrowthLabel.TabIndex = 20;
            this.hsrGrowthLabel.Text = "0.000%";
            this.hsrGrowthLabel.Visible = false;
            // 
            // totalHSLabel
            // 
            this.totalHSLabel.AutoSize = true;
            this.totalHSLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalHSLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.totalHSLabel.Location = new System.Drawing.Point(76, 9);
            this.totalHSLabel.Name = "totalHSLabel";
            this.totalHSLabel.Size = new System.Drawing.Size(21, 14);
            this.totalHSLabel.TabIndex = 19;
            this.totalHSLabel.Text = "HS";
            // 
            // totalHSTextBox
            // 
            this.totalHSTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalHSTextBox.Location = new System.Drawing.Point(79, 25);
            this.totalHSTextBox.Multiline = false;
            this.totalHSTextBox.Name = "totalHSTextBox";
            this.totalHSTextBox.ReadOnly = true;
            this.totalHSTextBox.Size = new System.Drawing.Size(47, 23);
            this.totalHSTextBox.TabIndex = 18;
            this.totalHSTextBox.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.sessionWeaponsTab);
            this.tabControl1.Controls.Add(this.allWeaponsTab);
            this.tabControl1.Controls.Add(this.miscTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.MinimumSize = new System.Drawing.Size(400, 340);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(400, 342);
            this.tabControl1.TabIndex = 24;
            // 
            // sessionWeaponsTab
            // 
            this.sessionWeaponsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sessionWeaponsTab.Controls.Add(this.sessionWeaponsGridView);
            this.sessionWeaponsTab.Location = new System.Drawing.Point(4, 23);
            this.sessionWeaponsTab.Name = "sessionWeaponsTab";
            this.sessionWeaponsTab.Padding = new System.Windows.Forms.Padding(3);
            this.sessionWeaponsTab.Size = new System.Drawing.Size(392, 315);
            this.sessionWeaponsTab.TabIndex = 0;
            this.sessionWeaponsTab.Text = "Session Weapons";
            // 
            // sessionWeaponsGridView
            // 
            this.sessionWeaponsGridView.AllowUserToAddRows = false;
            this.sessionWeaponsGridView.AllowUserToDeleteRows = false;
            this.sessionWeaponsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sessionWeaponsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.sessionWeaponsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.sessionWeaponsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sessionWeaponsGridView.Location = new System.Drawing.Point(6, 6);
            this.sessionWeaponsGridView.MinimumSize = new System.Drawing.Size(170, 216);
            this.sessionWeaponsGridView.Name = "sessionWeaponsGridView";
            this.sessionWeaponsGridView.ReadOnly = true;
            this.sessionWeaponsGridView.RowHeadersVisible = false;
            this.sessionWeaponsGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            this.sessionWeaponsGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sessionWeaponsGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
            this.sessionWeaponsGridView.ShowEditingIcon = false;
            this.sessionWeaponsGridView.Size = new System.Drawing.Size(380, 303);
            this.sessionWeaponsGridView.TabIndex = 22;
            // 
            // allWeaponsTab
            // 
            this.allWeaponsTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.allWeaponsTab.Controls.Add(this.weaponsGridView);
            this.allWeaponsTab.Location = new System.Drawing.Point(4, 23);
            this.allWeaponsTab.Name = "allWeaponsTab";
            this.allWeaponsTab.Padding = new System.Windows.Forms.Padding(3);
            this.allWeaponsTab.Size = new System.Drawing.Size(392, 315);
            this.allWeaponsTab.TabIndex = 1;
            this.allWeaponsTab.Text = "All Weapons";
            // 
            // weaponsGridView
            // 
            this.weaponsGridView.AllowUserToAddRows = false;
            this.weaponsGridView.AllowUserToDeleteRows = false;
            this.weaponsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.weaponsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.weaponsGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.weaponsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.weaponsGridView.Location = new System.Drawing.Point(6, 6);
            this.weaponsGridView.MinimumSize = new System.Drawing.Size(170, 216);
            this.weaponsGridView.Name = "weaponsGridView";
            this.weaponsGridView.ReadOnly = true;
            this.weaponsGridView.RowHeadersVisible = false;
            this.weaponsGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Gray;
            this.weaponsGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponsGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
            this.weaponsGridView.ShowEditingIcon = false;
            this.weaponsGridView.Size = new System.Drawing.Size(380, 303);
            this.weaponsGridView.TabIndex = 21;
            // 
            // miscTab
            // 
            this.miscTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.miscTab.Controls.Add(this.teamImpactLabel);
            this.miscTab.Controls.Add(this.panel3);
            this.miscTab.Location = new System.Drawing.Point(4, 23);
            this.miscTab.Name = "miscTab";
            this.miscTab.Padding = new System.Windows.Forms.Padding(3);
            this.miscTab.Size = new System.Drawing.Size(392, 315);
            this.miscTab.TabIndex = 2;
            this.miscTab.Text = "Miscellaneous";
            // 
            // teamImpactLabel
            // 
            this.teamImpactLabel.AutoSize = true;
            this.teamImpactLabel.BackColor = System.Drawing.Color.Transparent;
            this.teamImpactLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teamImpactLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.teamImpactLabel.Location = new System.Drawing.Point(6, 4);
            this.teamImpactLabel.Name = "teamImpactLabel";
            this.teamImpactLabel.Size = new System.Drawing.Size(76, 14);
            this.teamImpactLabel.TabIndex = 35;
            this.teamImpactLabel.Text = "Team Impact";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.teamRelianceLabel);
            this.panel3.Controls.Add(this.teamRelianceTextBox);
            this.panel3.Controls.Add(this.revivesTakenLabel);
            this.panel3.Controls.Add(this.kdrReviveLabel);
            this.panel3.Controls.Add(this.timesRevivedTextBox);
            this.panel3.Controls.Add(this.reviveKDRTextBox);
            this.panel3.Location = new System.Drawing.Point(5, 21);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(371, 75);
            this.panel3.TabIndex = 38;
            // 
            // teamRelianceLabel
            // 
            this.teamRelianceLabel.AutoSize = true;
            this.teamRelianceLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teamRelianceLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.teamRelianceLabel.Location = new System.Drawing.Point(157, 12);
            this.teamRelianceLabel.Name = "teamRelianceLabel";
            this.teamRelianceLabel.Size = new System.Drawing.Size(108, 14);
            this.teamRelianceLabel.TabIndex = 39;
            this.teamRelianceLabel.Text = "Team Dependence";
            // 
            // teamRelianceTextBox
            // 
            this.teamRelianceTextBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.teamRelianceTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.teamRelianceTextBox.Location = new System.Drawing.Point(160, 29);
            this.teamRelianceTextBox.Multiline = false;
            this.teamRelianceTextBox.Name = "teamRelianceTextBox";
            this.teamRelianceTextBox.ReadOnly = true;
            this.teamRelianceTextBox.Size = new System.Drawing.Size(47, 23);
            this.teamRelianceTextBox.TabIndex = 38;
            this.teamRelianceTextBox.Text = "";
            // 
            // revivesTakenLabel
            // 
            this.revivesTakenLabel.AutoSize = true;
            this.revivesTakenLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.revivesTakenLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.revivesTakenLabel.Location = new System.Drawing.Point(15, 12);
            this.revivesTakenLabel.Name = "revivesTakenLabel";
            this.revivesTakenLabel.Size = new System.Drawing.Size(48, 14);
            this.revivesTakenLabel.TabIndex = 35;
            this.revivesTakenLabel.Text = "Revives";
            // 
            // kdrReviveLabel
            // 
            this.kdrReviveLabel.AutoSize = true;
            this.kdrReviveLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kdrReviveLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.kdrReviveLabel.Location = new System.Drawing.Point(86, 12);
            this.kdrReviveLabel.Name = "kdrReviveLabel";
            this.kdrReviveLabel.Size = new System.Drawing.Size(28, 14);
            this.kdrReviveLabel.TabIndex = 37;
            this.kdrReviveLabel.Text = "KDR";
            // 
            // timesRevivedTextBox
            // 
            this.timesRevivedTextBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.timesRevivedTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timesRevivedTextBox.Location = new System.Drawing.Point(18, 29);
            this.timesRevivedTextBox.Multiline = false;
            this.timesRevivedTextBox.Name = "timesRevivedTextBox";
            this.timesRevivedTextBox.ReadOnly = true;
            this.timesRevivedTextBox.Size = new System.Drawing.Size(47, 23);
            this.timesRevivedTextBox.TabIndex = 34;
            this.timesRevivedTextBox.Text = "";
            // 
            // reviveKDRTextBox
            // 
            this.reviveKDRTextBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.reviveKDRTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reviveKDRTextBox.Location = new System.Drawing.Point(89, 29);
            this.reviveKDRTextBox.Multiline = false;
            this.reviveKDRTextBox.Name = "reviveKDRTextBox";
            this.reviveKDRTextBox.ReadOnly = true;
            this.reviveKDRTextBox.Size = new System.Drawing.Size(47, 23);
            this.reviveKDRTextBox.TabIndex = 36;
            this.reviveKDRTextBox.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // startSessionButton
            // 
            this.startSessionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startSessionButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.startSessionButton.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startSessionButton.Location = new System.Drawing.Point(250, 26);
            this.startSessionButton.Name = "startSessionButton";
            this.startSessionButton.Size = new System.Drawing.Size(83, 23);
            this.startSessionButton.TabIndex = 25;
            this.startSessionButton.Text = "Start";
            this.startSessionButton.UseVisualStyleBackColor = true;
            this.startSessionButton.Click += new System.EventHandler(this.startSessionButton_Click);
            // 
            // updatingLabel
            // 
            this.updatingLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updatingLabel.AutoSize = true;
            this.updatingLabel.BackColor = System.Drawing.Color.Transparent;
            this.updatingLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updatingLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.updatingLabel.Location = new System.Drawing.Point(235, 60);
            this.updatingLabel.Name = "updatingLabel";
            this.updatingLabel.Size = new System.Drawing.Size(103, 14);
            this.updatingLabel.TabIndex = 18;
            this.updatingLabel.Text = "Updating Events...";
            this.updatingLabel.Visible = false;
            // 
            // playerNameLabel
            // 
            this.playerNameLabel.AutoSize = true;
            this.playerNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.playerNameLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playerNameLabel.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.playerNameLabel.Location = new System.Drawing.Point(59, 4);
            this.playerNameLabel.Name = "playerNameLabel";
            this.playerNameLabel.Size = new System.Drawing.Size(77, 14);
            this.playerNameLabel.TabIndex = 26;
            this.playerNameLabel.Text = "PLAYER NAME";
            this.playerNameLabel.Visible = false;
            // 
            // totalKillsLabel
            // 
            this.totalKillsLabel.AutoSize = true;
            this.totalKillsLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalKillsLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.totalKillsLabel.Location = new System.Drawing.Point(3, 9);
            this.totalKillsLabel.Name = "totalKillsLabel";
            this.totalKillsLabel.Size = new System.Drawing.Size(31, 14);
            this.totalKillsLabel.TabIndex = 28;
            this.totalKillsLabel.Text = "Kills";
            // 
            // totalKillsTextBox
            // 
            this.totalKillsTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalKillsTextBox.Location = new System.Drawing.Point(6, 25);
            this.totalKillsTextBox.Multiline = false;
            this.totalKillsTextBox.Name = "totalKillsTextBox";
            this.totalKillsTextBox.ReadOnly = true;
            this.totalKillsTextBox.Size = new System.Drawing.Size(47, 23);
            this.totalKillsTextBox.TabIndex = 27;
            this.totalKillsTextBox.Text = "";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.kdrGrowthLabel);
            this.panel2.Controls.Add(this.totalKDRLabel);
            this.panel2.Controls.Add(this.totalKDRTextBox);
            this.panel2.Controls.Add(this.totalDeathsLabel);
            this.panel2.Controls.Add(this.totalDeathsTextBox);
            this.panel2.Controls.Add(this.totalKillsLabel);
            this.panel2.Controls.Add(this.totalHSTextBox);
            this.panel2.Controls.Add(this.totalKillsTextBox);
            this.panel2.Controls.Add(this.totalHSLabel);
            this.panel2.Controls.Add(this.hsrGrowthLabel);
            this.panel2.Location = new System.Drawing.Point(12, 219);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 124);
            this.panel2.TabIndex = 29;
            // 
            // kdrGrowthLabel
            // 
            this.kdrGrowthLabel.AutoSize = true;
            this.kdrGrowthLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kdrGrowthLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.kdrGrowthLabel.Location = new System.Drawing.Point(126, 76);
            this.kdrGrowthLabel.Name = "kdrGrowthLabel";
            this.kdrGrowthLabel.Size = new System.Drawing.Size(43, 14);
            this.kdrGrowthLabel.TabIndex = 33;
            this.kdrGrowthLabel.Text = "0.000%";
            this.kdrGrowthLabel.Visible = false;
            // 
            // totalKDRLabel
            // 
            this.totalKDRLabel.AutoSize = true;
            this.totalKDRLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalKDRLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.totalKDRLabel.Location = new System.Drawing.Point(76, 56);
            this.totalKDRLabel.Name = "totalKDRLabel";
            this.totalKDRLabel.Size = new System.Drawing.Size(28, 14);
            this.totalKDRLabel.TabIndex = 32;
            this.totalKDRLabel.Text = "KDR";
            // 
            // totalKDRTextBox
            // 
            this.totalKDRTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalKDRTextBox.Location = new System.Drawing.Point(79, 73);
            this.totalKDRTextBox.Multiline = false;
            this.totalKDRTextBox.Name = "totalKDRTextBox";
            this.totalKDRTextBox.ReadOnly = true;
            this.totalKDRTextBox.Size = new System.Drawing.Size(47, 23);
            this.totalKDRTextBox.TabIndex = 31;
            this.totalKDRTextBox.Text = "";
            // 
            // totalDeathsLabel
            // 
            this.totalDeathsLabel.AutoSize = true;
            this.totalDeathsLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalDeathsLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.totalDeathsLabel.Location = new System.Drawing.Point(3, 56);
            this.totalDeathsLabel.Name = "totalDeathsLabel";
            this.totalDeathsLabel.Size = new System.Drawing.Size(46, 14);
            this.totalDeathsLabel.TabIndex = 30;
            this.totalDeathsLabel.Text = "Deaths";
            // 
            // totalDeathsTextBox
            // 
            this.totalDeathsTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalDeathsTextBox.Location = new System.Drawing.Point(6, 73);
            this.totalDeathsTextBox.Multiline = false;
            this.totalDeathsTextBox.Name = "totalDeathsTextBox";
            this.totalDeathsTextBox.ReadOnly = true;
            this.totalDeathsTextBox.Size = new System.Drawing.Size(47, 23);
            this.totalDeathsTextBox.TabIndex = 29;
            this.totalDeathsTextBox.Text = "";
            // 
            // sessionLabel
            // 
            this.sessionLabel.AutoSize = true;
            this.sessionLabel.BackColor = System.Drawing.Color.Transparent;
            this.sessionLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sessionLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.sessionLabel.Location = new System.Drawing.Point(12, 29);
            this.sessionLabel.Name = "sessionLabel";
            this.sessionLabel.Size = new System.Drawing.Size(80, 14);
            this.sessionLabel.TabIndex = 18;
            this.sessionLabel.Text = "Session Stats";
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.BackColor = System.Drawing.Color.Transparent;
            this.totalLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.totalLabel.Location = new System.Drawing.Point(12, 203);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(64, 14);
            this.totalLabel.TabIndex = 30;
            this.totalLabel.Text = "Total Stats";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.sessionToolStripMenuItem,
            this.streamingToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(948, 24);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.exitToolStripMenuItem.ForeColor = System.Drawing.SystemColors.WindowText;
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // sessionToolStripMenuItem
            // 
            this.sessionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateEventsToolStripMenuItem,
            this.updateWeaponsToolStripMenuItem,
            this.createSessionToolStripMenuItem,
            this.cancelOperationToolStripMenuItem});
            this.sessionToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.sessionToolStripMenuItem.Name = "sessionToolStripMenuItem";
            this.sessionToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.sessionToolStripMenuItem.Text = "&Session";
            // 
            // updateEventsToolStripMenuItem
            // 
            this.updateEventsToolStripMenuItem.Name = "updateEventsToolStripMenuItem";
            this.updateEventsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.updateEventsToolStripMenuItem.Text = "Update Events";
            this.updateEventsToolStripMenuItem.Click += new System.EventHandler(this.updateEventsToolStripMenuItem_Click);
            // 
            // updateWeaponsToolStripMenuItem
            // 
            this.updateWeaponsToolStripMenuItem.Name = "updateWeaponsToolStripMenuItem";
            this.updateWeaponsToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.updateWeaponsToolStripMenuItem.Text = "Update Weapons";
            this.updateWeaponsToolStripMenuItem.Click += new System.EventHandler(this.updateWeaponsToolStripMenuItem_Click);
            // 
            // createSessionToolStripMenuItem
            // 
            this.createSessionToolStripMenuItem.Name = "createSessionToolStripMenuItem";
            this.createSessionToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.createSessionToolStripMenuItem.Text = "Create Session";
            this.createSessionToolStripMenuItem.Click += new System.EventHandler(this.createSessionToolStripMenuItem_Click);
            // 
            // cancelOperationToolStripMenuItem
            // 
            this.cancelOperationToolStripMenuItem.Name = "cancelOperationToolStripMenuItem";
            this.cancelOperationToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.cancelOperationToolStripMenuItem.Text = "Cancel Operation";
            this.cancelOperationToolStripMenuItem.Visible = false;
            this.cancelOperationToolStripMenuItem.Click += new System.EventHandler(this.cancelOperationToolStripMenuItem_Click);
            // 
            // streamingToolStripMenuItem
            // 
            this.streamingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startOverlayToolStripMenuItem});
            this.streamingToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.streamingToolStripMenuItem.Name = "streamingToolStripMenuItem";
            this.streamingToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.streamingToolStripMenuItem.Text = "Streaming";
            // 
            // startOverlayToolStripMenuItem
            // 
            this.startOverlayToolStripMenuItem.Name = "startOverlayToolStripMenuItem";
            this.startOverlayToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.startOverlayToolStripMenuItem.Text = "Start Overlay";
            this.startOverlayToolStripMenuItem.Click += new System.EventHandler(this.startOverlayToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.colorToolStripMenuItem,
            this.clearUsersToolStripMenuItem});
            this.toolsToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Enabled = false;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Visible = false;
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.positiveColorsToolStripMenuItem,
            this.negativeColorsToolStripMenuItem});
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.colorToolStripMenuItem.Text = "Color";
            // 
            // positiveColorsToolStripMenuItem
            // 
            this.positiveColorsToolStripMenuItem.Name = "positiveColorsToolStripMenuItem";
            this.positiveColorsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.positiveColorsToolStripMenuItem.Text = "Positive Colors";
            this.positiveColorsToolStripMenuItem.Click += new System.EventHandler(this.positiveColorsToolStripMenuItem_Click);
            // 
            // negativeColorsToolStripMenuItem
            // 
            this.negativeColorsToolStripMenuItem.Name = "negativeColorsToolStripMenuItem";
            this.negativeColorsToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.negativeColorsToolStripMenuItem.Text = "Negative Colors";
            this.negativeColorsToolStripMenuItem.Click += new System.EventHandler(this.negativeColorsToolStripMenuItem_Click);
            // 
            // clearUsersToolStripMenuItem
            // 
            this.clearUsersToolStripMenuItem.Name = "clearUsersToolStripMenuItem";
            this.clearUsersToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.clearUsersToolStripMenuItem.Text = "Clear Users";
            this.clearUsersToolStripMenuItem.Click += new System.EventHandler(this.clearUsersToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateToolStripMenuItem,
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Enabled = false;
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.updateToolStripMenuItem.Text = "Update";
            this.updateToolStripMenuItem.Visible = false;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(113, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // eventLogGridView
            // 
            this.eventLogGridView.AllowUserToAddRows = false;
            this.eventLogGridView.AllowUserToDeleteRows = false;
            this.eventLogGridView.AllowUserToResizeRows = false;
            this.eventLogGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.eventLogGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.eventLogGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.eventLogGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.eventLogGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.eventLogGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventLogGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.brCol,
            this.playerCol,
            this.methodCol,
            this.hsCol,
            this.kdrCol});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.eventLogGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.eventLogGridView.EnableHeadersVisualStyles = false;
            this.eventLogGridView.Location = new System.Drawing.Point(2, 77);
            this.eventLogGridView.MinimumSize = new System.Drawing.Size(170, 216);
            this.eventLogGridView.Name = "eventLogGridView";
            this.eventLogGridView.ReadOnly = true;
            this.eventLogGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.eventLogGridView.RowHeadersVisible = false;
            this.eventLogGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventLogGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
            this.eventLogGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.eventLogGridView.ShowEditingIcon = false;
            this.eventLogGridView.ShowRowErrors = false;
            this.eventLogGridView.Size = new System.Drawing.Size(329, 263);
            this.eventLogGridView.TabIndex = 23;
            // 
            // brCol
            // 
            this.brCol.FillWeight = 10F;
            this.brCol.HeaderText = "BR";
            this.brCol.MinimumWidth = 30;
            this.brCol.Name = "brCol";
            this.brCol.ReadOnly = true;
            // 
            // playerCol
            // 
            this.playerCol.FillWeight = 30F;
            this.playerCol.HeaderText = "Player";
            this.playerCol.Name = "playerCol";
            this.playerCol.ReadOnly = true;
            this.playerCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // methodCol
            // 
            this.methodCol.FillWeight = 30F;
            this.methodCol.HeaderText = "Method";
            this.methodCol.Name = "methodCol";
            this.methodCol.ReadOnly = true;
            // 
            // hsCol
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.hsCol.DefaultCellStyle = dataGridViewCellStyle2;
            this.hsCol.FillWeight = 10F;
            this.hsCol.HeaderText = "HS";
            this.hsCol.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.hsCol.MinimumWidth = 30;
            this.hsCol.Name = "hsCol";
            this.hsCol.ReadOnly = true;
            this.hsCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.hsCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // kdrCol
            // 
            this.kdrCol.FillWeight = 10F;
            this.kdrCol.HeaderText = "KDR";
            this.kdrCol.MinimumWidth = 35;
            this.kdrCol.Name = "kdrCol";
            this.kdrCol.ReadOnly = true;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.usernameTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.usernameTextBox.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.usernameTextBox.FormattingEnabled = true;
            this.usernameTextBox.Location = new System.Drawing.Point(6, 27);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(239, 22);
            this.usernameTextBox.TabIndex = 33;
            // 
            // versionLabel
            // 
            this.versionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.versionLabel.AutoSize = true;
            this.versionLabel.BackColor = System.Drawing.Color.Transparent;
            this.versionLabel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.versionLabel.Location = new System.Drawing.Point(12, 355);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(164, 14);
            this.versionLabel.TabIndex = 34;
            this.versionLabel.Text = "Real Time Stat Tracker V 0.0.0";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.splitContainer1.Location = new System.Drawing.Point(200, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            this.splitContainer1.Panel1MinSize = 400;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.resumeButton);
            this.splitContainer1.Panel2.Controls.Add(this.usernameLabel);
            this.splitContainer1.Panel2.Controls.Add(this.usernameTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.killBoardLabel);
            this.splitContainer1.Panel2.Controls.Add(this.eventLogGridView);
            this.splitContainer1.Panel2.Controls.Add(this.startSessionButton);
            this.splitContainer1.Panel2.Controls.Add(this.updatingLabel);
            this.splitContainer1.Panel2.Controls.Add(this.playerNameLabel);
            this.splitContainer1.Panel2MinSize = 310;
            this.splitContainer1.Size = new System.Drawing.Size(736, 342);
            this.splitContainer1.SplitterDistance = 400;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 35;
            // 
            // GUIMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(948, 380);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.totalLabel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.sessionLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(948, 380);
            this.Name = "GUIMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Real Time Stat Tracker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GUIMain_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.sessionWeaponsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sessionWeaponsGridView)).EndInit();
            this.allWeaponsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.weaponsGridView)).EndInit();
            this.miscTab.ResumeLayout(false);
            this.miscTab.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLogGridView)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button resumeButton;
        private System.Windows.Forms.RichTextBox killsTextBox;
        private System.Windows.Forms.Label killsLabel;
        private System.Windows.Forms.Label deathsLabel;
        private System.Windows.Forms.RichTextBox deathsTextBox;
        private System.Windows.Forms.Label hsKillsLabel;
        private System.Windows.Forms.RichTextBox hsKillsTextBox;
        private System.Windows.Forms.Label hsDeathsLabel;
        private System.Windows.Forms.RichTextBox hsDeathsTextBox;
        private System.Windows.Forms.Label kdrLabel;
        private System.Windows.Forms.RichTextBox kdrTextBox;
        private System.Windows.Forms.Label hsLabel;
        private System.Windows.Forms.RichTextBox hsTextBox;
        private System.Windows.Forms.Label usernameLabel;
        private System.Windows.Forms.Label killBoardLabel;
        private System.Windows.Forms.DataGridView weaponsGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage sessionWeaponsTab;
        private System.Windows.Forms.TabPage allWeaponsTab;
        private System.Windows.Forms.DataGridView sessionWeaponsGridView;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button startSessionButton;
        private System.Windows.Forms.Label updatingLabel;
        private System.Windows.Forms.Label playerNameLabel;
        private System.Windows.Forms.Label totalHSLabel;
        private System.Windows.Forms.RichTextBox totalHSTextBox;
        private System.Windows.Forms.Label hsrGrowthLabel;
        private System.Windows.Forms.Label totalKillsLabel;
        private System.Windows.Forms.RichTextBox totalKillsTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label sessionLabel;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.DataGridView eventLogGridView;
        private System.Windows.Forms.ComboBox usernameTextBox;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.ToolStripMenuItem sessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateEventsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateWeaponsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearUsersToolStripMenuItem;
        private System.Windows.Forms.Label kdrGrowthLabel;
        private System.Windows.Forms.Label totalKDRLabel;
        private System.Windows.Forms.RichTextBox totalKDRTextBox;
        private System.Windows.Forms.Label totalDeathsLabel;
        private System.Windows.Forms.RichTextBox totalDeathsTextBox;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem positiveColorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negativeColorsToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TabPage miscTab;
        private System.Windows.Forms.Label revivesTakenLabel;
        private System.Windows.Forms.RichTextBox timesRevivedTextBox;
        private System.Windows.Forms.Label kdrReviveLabel;
        private System.Windows.Forms.RichTextBox reviveKDRTextBox;
        private System.Windows.Forms.Label teamImpactLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label teamRelianceLabel;
        private System.Windows.Forms.RichTextBox teamRelianceTextBox;
        private System.Windows.Forms.ToolStripMenuItem cancelOperationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createSessionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem streamingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startOverlayToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn brCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn methodCol;
        private System.Windows.Forms.DataGridViewImageColumn hsCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn kdrCol;

    }
}

