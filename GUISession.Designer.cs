namespace PS2StatTracker
{
    partial class GUISession
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
            this.pastEventsNumber = new System.Windows.Forms.NumericUpDown();
            this.eventCountLabel = new System.Windows.Forms.Label();
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.countStatsCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pastEventsNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // pastEventsNumber
            // 
            this.pastEventsNumber.Location = new System.Drawing.Point(12, 36);
            this.pastEventsNumber.Name = "pastEventsNumber";
            this.pastEventsNumber.Size = new System.Drawing.Size(120, 20);
            this.pastEventsNumber.TabIndex = 0;
            // 
            // eventCountLabel
            // 
            this.eventCountLabel.AutoSize = true;
            this.eventCountLabel.Location = new System.Drawing.Point(9, 20);
            this.eventCountLabel.Name = "eventCountLabel";
            this.eventCountLabel.Size = new System.Drawing.Size(90, 13);
            this.eventCountLabel.TabIndex = 1;
            this.eventCountLabel.Text = "Past Event Count";
            // 
            // confirmButton
            // 
            this.confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmButton.Location = new System.Drawing.Point(116, 227);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 19;
            this.confirmButton.Text = "Create";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(197, 227);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 18;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // countStatsCheckBox
            // 
            this.countStatsCheckBox.AutoSize = true;
            this.countStatsCheckBox.Location = new System.Drawing.Point(12, 62);
            this.countStatsCheckBox.Name = "countStatsCheckBox";
            this.countStatsCheckBox.Size = new System.Drawing.Size(81, 17);
            this.countStatsCheckBox.TabIndex = 20;
            this.countStatsCheckBox.Text = "Count Stats";
            this.countStatsCheckBox.UseVisualStyleBackColor = true;
            // 
            // GUISession
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.countStatsCheckBox);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.eventCountLabel);
            this.Controls.Add(this.pastEventsNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GUISession";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Session Manager";
            ((System.ComponentModel.ISupportInitialize)(this.pastEventsNumber)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.NumericUpDown pastEventsNumber;
        private System.Windows.Forms.Label eventCountLabel;
        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
        public System.Windows.Forms.CheckBox countStatsCheckBox;
    }
}