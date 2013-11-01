namespace PS2StatTracker
{
    partial class GUIOptions
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
            this.confirmButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.e75Label = new System.Windows.Forms.Label();
            this.e60Label = new System.Windows.Forms.Label();
            this.e45Label = new System.Windows.Forms.Label();
            this.e30Label = new System.Windows.Forms.Label();
            this.eventUpdateRateLabel = new System.Windows.Forms.Label();
            this.eventUpdateBar = new System.Windows.Forms.TrackBar();
            this.clearUsersButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.eventUpdateBar)).BeginInit();
            this.SuspendLayout();
            // 
            // confirmButton
            // 
            this.confirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.confirmButton.Location = new System.Drawing.Point(116, 226);
            this.confirmButton.Name = "confirmButton";
            this.confirmButton.Size = new System.Drawing.Size(75, 23);
            this.confirmButton.TabIndex = 17;
            this.confirmButton.Text = "Save";
            this.confirmButton.UseVisualStyleBackColor = true;
            this.confirmButton.Click += new System.EventHandler(this.confirmButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(197, 226);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 16;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // e75Label
            // 
            this.e75Label.AutoSize = true;
            this.e75Label.Location = new System.Drawing.Point(94, 62);
            this.e75Label.Name = "e75Label";
            this.e75Label.Size = new System.Drawing.Size(19, 13);
            this.e75Label.TabIndex = 15;
            this.e75Label.Text = "75";
            // 
            // e60Label
            // 
            this.e60Label.AutoSize = true;
            this.e60Label.Location = new System.Drawing.Point(69, 62);
            this.e60Label.Name = "e60Label";
            this.e60Label.Size = new System.Drawing.Size(19, 13);
            this.e60Label.TabIndex = 14;
            this.e60Label.Text = "60";
            // 
            // e45Label
            // 
            this.e45Label.AutoSize = true;
            this.e45Label.Location = new System.Drawing.Point(44, 62);
            this.e45Label.Name = "e45Label";
            this.e45Label.Size = new System.Drawing.Size(19, 13);
            this.e45Label.TabIndex = 13;
            this.e45Label.Text = "45";
            // 
            // e30Label
            // 
            this.e30Label.AutoSize = true;
            this.e30Label.Location = new System.Drawing.Point(19, 62);
            this.e30Label.Name = "e30Label";
            this.e30Label.Size = new System.Drawing.Size(19, 13);
            this.e30Label.TabIndex = 12;
            this.e30Label.Text = "30";
            // 
            // eventUpdateRateLabel
            // 
            this.eventUpdateRateLabel.AutoSize = true;
            this.eventUpdateRateLabel.Location = new System.Drawing.Point(12, 14);
            this.eventUpdateRateLabel.Name = "eventUpdateRateLabel";
            this.eventUpdateRateLabel.Size = new System.Drawing.Size(134, 13);
            this.eventUpdateRateLabel.TabIndex = 11;
            this.eventUpdateRateLabel.Text = "Event Updates in Seconds";
            // 
            // eventUpdateBar
            // 
            this.eventUpdateBar.Location = new System.Drawing.Point(15, 30);
            this.eventUpdateBar.Maximum = 3;
            this.eventUpdateBar.Name = "eventUpdateBar";
            this.eventUpdateBar.Size = new System.Drawing.Size(104, 45);
            this.eventUpdateBar.TabIndex = 10;
            // 
            // clearUsersButton
            // 
            this.clearUsersButton.Location = new System.Drawing.Point(197, 14);
            this.clearUsersButton.Name = "clearUsersButton";
            this.clearUsersButton.Size = new System.Drawing.Size(75, 23);
            this.clearUsersButton.TabIndex = 9;
            this.clearUsersButton.Text = "Clear Users";
            this.clearUsersButton.UseVisualStyleBackColor = true;
            // 
            // GUIOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.confirmButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.e75Label);
            this.Controls.Add(this.e60Label);
            this.Controls.Add(this.e45Label);
            this.Controls.Add(this.e30Label);
            this.Controls.Add(this.eventUpdateRateLabel);
            this.Controls.Add(this.eventUpdateBar);
            this.Controls.Add(this.clearUsersButton);
            this.Name = "GUIOptions";
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.eventUpdateBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button confirmButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label e75Label;
        private System.Windows.Forms.Label e60Label;
        private System.Windows.Forms.Label e45Label;
        private System.Windows.Forms.Label e30Label;
        private System.Windows.Forms.Label eventUpdateRateLabel;
        private System.Windows.Forms.TrackBar eventUpdateBar;
        private System.Windows.Forms.Button clearUsersButton;
    }
}