namespace PS2StatTracker
{
    partial class GUIOverlay
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
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUIOverlay));
            this.killsLabel = new System.Windows.Forms.Label();
            this.killsNumber = new System.Windows.Forms.Label();
            this.deathsNum = new System.Windows.Forms.Label();
            this.deathsLabel = new System.Windows.Forms.Label();
            this.kdrNum = new System.Windows.Forms.Label();
            this.kdrLabel = new System.Windows.Forms.Label();
            this.hsrNum = new System.Windows.Forms.Label();
            this.hsrLabel = new System.Windows.Forms.Label();
            this.eventLogGridView = new System.Windows.Forms.DataGridView();
            this.playerCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.methodCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hsCol = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.weaponAccNum = new System.Windows.Forms.Label();
            this.weaponAccLabel = new System.Windows.Forms.Label();
            this.weaponTotalAccNum = new System.Windows.Forms.Label();
            this.weaponTotalAccLabel = new System.Windows.Forms.Label();
            this.weaponSessionLabel = new System.Windows.Forms.Label();
            this.weaponTotalKDRNum = new System.Windows.Forms.Label();
            this.weaponTotalKDRLabel = new System.Windows.Forms.Label();
            this.weaponTotalHSRNum = new System.Windows.Forms.Label();
            this.weaponHSRTotal = new System.Windows.Forms.Label();
            this.weaponKillsTotalNum = new System.Windows.Forms.Label();
            this.weaponKillsTotalLabel = new System.Windows.Forms.Label();
            this.weaponTotalLabel = new System.Windows.Forms.Label();
            this.weaponHSRNum = new System.Windows.Forms.Label();
            this.weaponHSRLabel = new System.Windows.Forms.Label();
            this.weaponName = new System.Windows.Forms.Label();
            this.weaponKillsNum = new System.Windows.Forms.Label();
            this.lastWeaponLabel = new System.Windows.Forms.Label();
            this.weaponKillsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.eventLogGridView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // killsLabel
            // 
            this.killsLabel.AutoSize = true;
            this.killsLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.killsLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.killsLabel.Location = new System.Drawing.Point(5, 9);
            this.killsLabel.Name = "killsLabel";
            this.killsLabel.Size = new System.Drawing.Size(36, 19);
            this.killsLabel.TabIndex = 0;
            this.killsLabel.Text = "Kills";
            // 
            // killsNumber
            // 
            this.killsNumber.AutoSize = true;
            this.killsNumber.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.killsNumber.ForeColor = System.Drawing.Color.DarkGreen;
            this.killsNumber.Location = new System.Drawing.Point(9, 29);
            this.killsNumber.Name = "killsNumber";
            this.killsNumber.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.killsNumber.Size = new System.Drawing.Size(17, 19);
            this.killsNumber.TabIndex = 1;
            this.killsNumber.Text = "0";
            // 
            // deathsNum
            // 
            this.deathsNum.AutoSize = true;
            this.deathsNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deathsNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.deathsNum.Location = new System.Drawing.Point(54, 29);
            this.deathsNum.Name = "deathsNum";
            this.deathsNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.deathsNum.Size = new System.Drawing.Size(17, 19);
            this.deathsNum.TabIndex = 3;
            this.deathsNum.Text = "0";
            // 
            // deathsLabel
            // 
            this.deathsLabel.AutoSize = true;
            this.deathsLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deathsLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.deathsLabel.Location = new System.Drawing.Point(54, 9);
            this.deathsLabel.Name = "deathsLabel";
            this.deathsLabel.Size = new System.Drawing.Size(56, 19);
            this.deathsLabel.TabIndex = 2;
            this.deathsLabel.Text = "Deaths";
            // 
            // kdrNum
            // 
            this.kdrNum.AutoSize = true;
            this.kdrNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kdrNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.kdrNum.Location = new System.Drawing.Point(115, 29);
            this.kdrNum.Name = "kdrNum";
            this.kdrNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.kdrNum.Size = new System.Drawing.Size(17, 19);
            this.kdrNum.TabIndex = 5;
            this.kdrNum.Text = "0";
            // 
            // kdrLabel
            // 
            this.kdrLabel.AutoSize = true;
            this.kdrLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.kdrLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.kdrLabel.Location = new System.Drawing.Point(115, 9);
            this.kdrLabel.Name = "kdrLabel";
            this.kdrLabel.Size = new System.Drawing.Size(37, 19);
            this.kdrLabel.TabIndex = 4;
            this.kdrLabel.Text = "KDR";
            // 
            // hsrNum
            // 
            this.hsrNum.AutoSize = true;
            this.hsrNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hsrNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.hsrNum.Location = new System.Drawing.Point(160, 29);
            this.hsrNum.Name = "hsrNum";
            this.hsrNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.hsrNum.Size = new System.Drawing.Size(17, 19);
            this.hsrNum.TabIndex = 7;
            this.hsrNum.Text = "0";
            // 
            // hsrLabel
            // 
            this.hsrLabel.AutoSize = true;
            this.hsrLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hsrLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.hsrLabel.Location = new System.Drawing.Point(160, 9);
            this.hsrLabel.Name = "hsrLabel";
            this.hsrLabel.Size = new System.Drawing.Size(36, 19);
            this.hsrLabel.TabIndex = 6;
            this.hsrLabel.Text = "HSR";
            // 
            // eventLogGridView
            // 
            this.eventLogGridView.AllowUserToAddRows = false;
            this.eventLogGridView.AllowUserToDeleteRows = false;
            this.eventLogGridView.AllowUserToResizeRows = false;
            this.eventLogGridView.BackgroundColor = System.Drawing.Color.Gray;
            this.eventLogGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.eventLogGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.eventLogGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.eventLogGridView.ColumnHeadersVisible = false;
            this.eventLogGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.playerCol,
            this.methodCol,
            this.hsCol});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Green;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.eventLogGridView.DefaultCellStyle = dataGridViewCellStyle5;
            this.eventLogGridView.EnableHeadersVisualStyles = false;
            this.eventLogGridView.GridColor = System.Drawing.SystemColors.AppWorkspace;
            this.eventLogGridView.Location = new System.Drawing.Point(0, 51);
            this.eventLogGridView.MinimumSize = new System.Drawing.Size(170, 50);
            this.eventLogGridView.Name = "eventLogGridView";
            this.eventLogGridView.ReadOnly = true;
            this.eventLogGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.eventLogGridView.RowHeadersVisible = false;
            this.eventLogGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventLogGridView.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Silver;
            this.eventLogGridView.RowTemplate.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.White;
            this.eventLogGridView.ShowEditingIcon = false;
            this.eventLogGridView.ShowRowErrors = false;
            this.eventLogGridView.Size = new System.Drawing.Size(213, 115);
            this.eventLogGridView.TabIndex = 24;
            // 
            // playerCol
            // 
            this.playerCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            this.playerCol.DefaultCellStyle = dataGridViewCellStyle2;
            this.playerCol.FillWeight = 45F;
            this.playerCol.HeaderText = "Player";
            this.playerCol.Name = "playerCol";
            this.playerCol.ReadOnly = true;
            this.playerCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.playerCol.Width = 5;
            // 
            // methodCol
            // 
            this.methodCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.methodCol.DefaultCellStyle = dataGridViewCellStyle3;
            this.methodCol.FillWeight = 40F;
            this.methodCol.HeaderText = "Method";
            this.methodCol.MinimumWidth = 15;
            this.methodCol.Name = "methodCol";
            this.methodCol.ReadOnly = true;
            this.methodCol.Width = 15;
            // 
            // hsCol
            // 
            this.hsCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.hsCol.DefaultCellStyle = dataGridViewCellStyle4;
            this.hsCol.FillWeight = 15F;
            this.hsCol.HeaderText = "HeadShot";
            this.hsCol.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.hsCol.Name = "hsCol";
            this.hsCol.ReadOnly = true;
            this.hsCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.hsCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.hsCol.Width = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.weaponAccNum);
            this.panel1.Controls.Add(this.weaponAccLabel);
            this.panel1.Controls.Add(this.weaponTotalAccNum);
            this.panel1.Controls.Add(this.weaponTotalAccLabel);
            this.panel1.Controls.Add(this.weaponSessionLabel);
            this.panel1.Controls.Add(this.weaponTotalKDRNum);
            this.panel1.Controls.Add(this.weaponTotalKDRLabel);
            this.panel1.Controls.Add(this.weaponTotalHSRNum);
            this.panel1.Controls.Add(this.weaponHSRTotal);
            this.panel1.Controls.Add(this.weaponKillsTotalNum);
            this.panel1.Controls.Add(this.weaponKillsTotalLabel);
            this.panel1.Controls.Add(this.weaponTotalLabel);
            this.panel1.Controls.Add(this.weaponHSRNum);
            this.panel1.Controls.Add(this.weaponHSRLabel);
            this.panel1.Controls.Add(this.weaponName);
            this.panel1.Controls.Add(this.weaponKillsNum);
            this.panel1.Controls.Add(this.lastWeaponLabel);
            this.panel1.Controls.Add(this.weaponKillsLabel);
            this.panel1.ForeColor = System.Drawing.Color.DarkGreen;
            this.panel1.Location = new System.Drawing.Point(0, 167);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 124);
            this.panel1.TabIndex = 25;
            // 
            // weaponAccNum
            // 
            this.weaponAccNum.AutoSize = true;
            this.weaponAccNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponAccNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponAccNum.Location = new System.Drawing.Point(3, 98);
            this.weaponAccNum.Name = "weaponAccNum";
            this.weaponAccNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.weaponAccNum.Size = new System.Drawing.Size(17, 19);
            this.weaponAccNum.TabIndex = 42;
            this.weaponAccNum.Text = "0";
            // 
            // weaponAccLabel
            // 
            this.weaponAccLabel.AutoSize = true;
            this.weaponAccLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponAccLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponAccLabel.Location = new System.Drawing.Point(3, 78);
            this.weaponAccLabel.Name = "weaponAccLabel";
            this.weaponAccLabel.Size = new System.Drawing.Size(33, 19);
            this.weaponAccLabel.TabIndex = 41;
            this.weaponAccLabel.Text = "Acc";
            // 
            // weaponTotalAccNum
            // 
            this.weaponTotalAccNum.AutoSize = true;
            this.weaponTotalAccNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponTotalAccNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponTotalAccNum.Location = new System.Drawing.Point(147, 98);
            this.weaponTotalAccNum.Name = "weaponTotalAccNum";
            this.weaponTotalAccNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.weaponTotalAccNum.Size = new System.Drawing.Size(17, 19);
            this.weaponTotalAccNum.TabIndex = 40;
            this.weaponTotalAccNum.Text = "0";
            // 
            // weaponTotalAccLabel
            // 
            this.weaponTotalAccLabel.AutoSize = true;
            this.weaponTotalAccLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponTotalAccLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponTotalAccLabel.Location = new System.Drawing.Point(147, 78);
            this.weaponTotalAccLabel.Name = "weaponTotalAccLabel";
            this.weaponTotalAccLabel.Size = new System.Drawing.Size(33, 19);
            this.weaponTotalAccLabel.TabIndex = 39;
            this.weaponTotalAccLabel.Text = "Acc";
            // 
            // weaponSessionLabel
            // 
            this.weaponSessionLabel.AutoSize = true;
            this.weaponSessionLabel.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponSessionLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponSessionLabel.Location = new System.Drawing.Point(3, 20);
            this.weaponSessionLabel.Name = "weaponSessionLabel";
            this.weaponSessionLabel.Size = new System.Drawing.Size(59, 19);
            this.weaponSessionLabel.TabIndex = 38;
            this.weaponSessionLabel.Text = "Session";
            // 
            // weaponTotalKDRNum
            // 
            this.weaponTotalKDRNum.AutoSize = true;
            this.weaponTotalKDRNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponTotalKDRNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponTotalKDRNum.Location = new System.Drawing.Point(101, 98);
            this.weaponTotalKDRNum.Name = "weaponTotalKDRNum";
            this.weaponTotalKDRNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.weaponTotalKDRNum.Size = new System.Drawing.Size(17, 19);
            this.weaponTotalKDRNum.TabIndex = 37;
            this.weaponTotalKDRNum.Text = "0";
            // 
            // weaponTotalKDRLabel
            // 
            this.weaponTotalKDRLabel.AutoSize = true;
            this.weaponTotalKDRLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponTotalKDRLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponTotalKDRLabel.Location = new System.Drawing.Point(101, 78);
            this.weaponTotalKDRLabel.Name = "weaponTotalKDRLabel";
            this.weaponTotalKDRLabel.Size = new System.Drawing.Size(37, 19);
            this.weaponTotalKDRLabel.TabIndex = 36;
            this.weaponTotalKDRLabel.Text = "KDR";
            // 
            // weaponTotalHSRNum
            // 
            this.weaponTotalHSRNum.AutoSize = true;
            this.weaponTotalHSRNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponTotalHSRNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponTotalHSRNum.Location = new System.Drawing.Point(147, 59);
            this.weaponTotalHSRNum.Name = "weaponTotalHSRNum";
            this.weaponTotalHSRNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.weaponTotalHSRNum.Size = new System.Drawing.Size(17, 19);
            this.weaponTotalHSRNum.TabIndex = 35;
            this.weaponTotalHSRNum.Text = "0";
            // 
            // weaponHSRTotal
            // 
            this.weaponHSRTotal.AutoSize = true;
            this.weaponHSRTotal.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponHSRTotal.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponHSRTotal.Location = new System.Drawing.Point(147, 39);
            this.weaponHSRTotal.Name = "weaponHSRTotal";
            this.weaponHSRTotal.Size = new System.Drawing.Size(36, 19);
            this.weaponHSRTotal.TabIndex = 34;
            this.weaponHSRTotal.Text = "HSR";
            // 
            // weaponKillsTotalNum
            // 
            this.weaponKillsTotalNum.AutoSize = true;
            this.weaponKillsTotalNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponKillsTotalNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponKillsTotalNum.Location = new System.Drawing.Point(101, 59);
            this.weaponKillsTotalNum.Name = "weaponKillsTotalNum";
            this.weaponKillsTotalNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.weaponKillsTotalNum.Size = new System.Drawing.Size(17, 19);
            this.weaponKillsTotalNum.TabIndex = 33;
            this.weaponKillsTotalNum.Text = "0";
            // 
            // weaponKillsTotalLabel
            // 
            this.weaponKillsTotalLabel.AutoSize = true;
            this.weaponKillsTotalLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponKillsTotalLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponKillsTotalLabel.Location = new System.Drawing.Point(101, 39);
            this.weaponKillsTotalLabel.Name = "weaponKillsTotalLabel";
            this.weaponKillsTotalLabel.Size = new System.Drawing.Size(36, 19);
            this.weaponKillsTotalLabel.TabIndex = 32;
            this.weaponKillsTotalLabel.Text = "Kills";
            // 
            // weaponTotalLabel
            // 
            this.weaponTotalLabel.AutoSize = true;
            this.weaponTotalLabel.Font = new System.Drawing.Font("Calibri", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponTotalLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponTotalLabel.Location = new System.Drawing.Point(101, 20);
            this.weaponTotalLabel.Name = "weaponTotalLabel";
            this.weaponTotalLabel.Size = new System.Drawing.Size(43, 19);
            this.weaponTotalLabel.TabIndex = 31;
            this.weaponTotalLabel.Text = "Total";
            // 
            // weaponHSRNum
            // 
            this.weaponHSRNum.AutoSize = true;
            this.weaponHSRNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponHSRNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponHSRNum.Location = new System.Drawing.Point(40, 59);
            this.weaponHSRNum.Name = "weaponHSRNum";
            this.weaponHSRNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.weaponHSRNum.Size = new System.Drawing.Size(17, 19);
            this.weaponHSRNum.TabIndex = 30;
            this.weaponHSRNum.Text = "0";
            // 
            // weaponHSRLabel
            // 
            this.weaponHSRLabel.AutoSize = true;
            this.weaponHSRLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponHSRLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponHSRLabel.Location = new System.Drawing.Point(40, 39);
            this.weaponHSRLabel.Name = "weaponHSRLabel";
            this.weaponHSRLabel.Size = new System.Drawing.Size(36, 19);
            this.weaponHSRLabel.TabIndex = 29;
            this.weaponHSRLabel.Text = "HSR";
            // 
            // weaponName
            // 
            this.weaponName.AutoSize = true;
            this.weaponName.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponName.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponName.Location = new System.Drawing.Point(68, 1);
            this.weaponName.Name = "weaponName";
            this.weaponName.Size = new System.Drawing.Size(49, 19);
            this.weaponName.TabIndex = 28;
            this.weaponName.Text = "Name";
            // 
            // weaponKillsNum
            // 
            this.weaponKillsNum.AutoSize = true;
            this.weaponKillsNum.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponKillsNum.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponKillsNum.Location = new System.Drawing.Point(3, 59);
            this.weaponKillsNum.Name = "weaponKillsNum";
            this.weaponKillsNum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.weaponKillsNum.Size = new System.Drawing.Size(17, 19);
            this.weaponKillsNum.TabIndex = 27;
            this.weaponKillsNum.Text = "0";
            // 
            // lastWeaponLabel
            // 
            this.lastWeaponLabel.AutoSize = true;
            this.lastWeaponLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastWeaponLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.lastWeaponLabel.Location = new System.Drawing.Point(3, 1);
            this.lastWeaponLabel.Name = "lastWeaponLabel";
            this.lastWeaponLabel.Size = new System.Drawing.Size(66, 19);
            this.lastWeaponLabel.TabIndex = 26;
            this.lastWeaponLabel.Text = "Weapon";
            // 
            // weaponKillsLabel
            // 
            this.weaponKillsLabel.AutoSize = true;
            this.weaponKillsLabel.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.weaponKillsLabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.weaponKillsLabel.Location = new System.Drawing.Point(3, 39);
            this.weaponKillsLabel.Name = "weaponKillsLabel";
            this.weaponKillsLabel.Size = new System.Drawing.Size(36, 19);
            this.weaponKillsLabel.TabIndex = 26;
            this.weaponKillsLabel.Text = "Kills";
            // 
            // GUIOverlay
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(213, 292);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.eventLogGridView);
            this.Controls.Add(this.hsrNum);
            this.Controls.Add(this.hsrLabel);
            this.Controls.Add(this.kdrNum);
            this.Controls.Add(this.kdrLabel);
            this.Controls.Add(this.deathsNum);
            this.Controls.Add(this.deathsLabel);
            this.Controls.Add(this.killsNumber);
            this.Controls.Add(this.killsLabel);
            this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Green;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(229, 320);
            this.Name = "GUIOverlay";
            this.Text = "Streaming Overlay";
            ((System.ComponentModel.ISupportInitialize)(this.eventLogGridView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label killsLabel;
        private System.Windows.Forms.Label killsNumber;
        private System.Windows.Forms.Label deathsNum;
        private System.Windows.Forms.Label deathsLabel;
        private System.Windows.Forms.Label kdrNum;
        private System.Windows.Forms.Label kdrLabel;
        private System.Windows.Forms.Label hsrNum;
        private System.Windows.Forms.Label hsrLabel;
        private System.Windows.Forms.DataGridView eventLogGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lastWeaponLabel;
        private System.Windows.Forms.Label weaponName;
        private System.Windows.Forms.Label weaponKillsNum;
        private System.Windows.Forms.Label weaponKillsLabel;
        private System.Windows.Forms.Label weaponHSRNum;
        private System.Windows.Forms.Label weaponHSRLabel;
        private System.Windows.Forms.Label weaponTotalKDRNum;
        private System.Windows.Forms.Label weaponTotalKDRLabel;
        private System.Windows.Forms.Label weaponTotalHSRNum;
        private System.Windows.Forms.Label weaponHSRTotal;
        private System.Windows.Forms.Label weaponKillsTotalNum;
        private System.Windows.Forms.Label weaponKillsTotalLabel;
        private System.Windows.Forms.Label weaponTotalLabel;
        private System.Windows.Forms.Label weaponSessionLabel;
        private System.Windows.Forms.Label weaponAccNum;
        private System.Windows.Forms.Label weaponAccLabel;
        private System.Windows.Forms.Label weaponTotalAccNum;
        private System.Windows.Forms.Label weaponTotalAccLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn playerCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn methodCol;
        private System.Windows.Forms.DataGridViewImageColumn hsCol;
    }
}