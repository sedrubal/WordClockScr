namespace WordClockScr
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.CloseBTN = new System.Windows.Forms.Button();
            this.ResetBTN = new System.Windows.Forms.Button();
            this.SecondsModeCB = new System.Windows.Forms.CheckBox();
            this.AutoSecondsCB = new System.Windows.Forms.CheckBox();
            this.NightModeCB = new System.Windows.Forms.CheckBox();
            this.AutoNightModeCB = new System.Windows.Forms.CheckBox();
            this.ScrOpacityTB = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.ShowTimeBannerCB = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.BackColorBTN = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.InactiveColorBTN = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.ActiveColorBTN = new System.Windows.Forms.Button();
            this.AnimationsCB = new System.Windows.Forms.CheckBox();
            this.UpdateBTN = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.ScrOpacityTB)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CloseBTN
            // 
            this.CloseBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseBTN.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseBTN.Location = new System.Drawing.Point(3, 620);
            this.CloseBTN.Name = "CloseBTN";
            this.CloseBTN.Size = new System.Drawing.Size(263, 38);
            this.CloseBTN.TabIndex = 16;
            this.CloseBTN.Text = "&Schließen";
            this.CloseBTN.UseVisualStyleBackColor = true;
            this.CloseBTN.Click += new System.EventHandler(this.CloseBTN_Click);
            // 
            // ResetBTN
            // 
            this.ResetBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetBTN.Location = new System.Drawing.Point(3, 576);
            this.ResetBTN.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.ResetBTN.Name = "ResetBTN";
            this.ResetBTN.Size = new System.Drawing.Size(263, 38);
            this.ResetBTN.TabIndex = 15;
            this.ResetBTN.Text = "&Reset";
            this.ResetBTN.UseVisualStyleBackColor = true;
            this.ResetBTN.Click += new System.EventHandler(this.ResetBTN_Click);
            // 
            // SecondsModeCB
            // 
            this.SecondsModeCB.AutoSize = true;
            this.SecondsModeCB.Location = new System.Drawing.Point(4, 110);
            this.SecondsModeCB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SecondsModeCB.Name = "SecondsModeCB";
            this.SecondsModeCB.Size = new System.Drawing.Size(148, 25);
            this.SecondsModeCB.TabIndex = 3;
            this.SecondsModeCB.Text = "Sekunden-&Modus";
            this.SecondsModeCB.UseVisualStyleBackColor = true;
            this.SecondsModeCB.CheckedChanged += new System.EventHandler(this.SecondsModeCB_CheckedChanged);
            // 
            // AutoSecondsCB
            // 
            this.AutoSecondsCB.AutoSize = true;
            this.AutoSecondsCB.Checked = true;
            this.AutoSecondsCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoSecondsCB.Location = new System.Drawing.Point(4, 75);
            this.AutoSecondsCB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AutoSecondsCB.Name = "AutoSecondsCB";
            this.AutoSecondsCB.Size = new System.Drawing.Size(244, 25);
            this.AutoSecondsCB.TabIndex = 2;
            this.AutoSecondsCB.Text = "A&utomatische Sekundenanzeige";
            this.AutoSecondsCB.UseVisualStyleBackColor = true;
            this.AutoSecondsCB.CheckedChanged += new System.EventHandler(this.AutoSecondsCB_CheckedChanged);
            // 
            // NightModeCB
            // 
            this.NightModeCB.AutoSize = true;
            this.NightModeCB.Location = new System.Drawing.Point(4, 40);
            this.NightModeCB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NightModeCB.Name = "NightModeCB";
            this.NightModeCB.Size = new System.Drawing.Size(120, 25);
            this.NightModeCB.TabIndex = 1;
            this.NightModeCB.Text = "&Nacht-Modus";
            this.NightModeCB.UseVisualStyleBackColor = true;
            this.NightModeCB.CheckedChanged += new System.EventHandler(this.NightModeCB_CheckedChanged);
            // 
            // AutoNightModeCB
            // 
            this.AutoNightModeCB.AutoSize = true;
            this.AutoNightModeCB.Checked = true;
            this.AutoNightModeCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoNightModeCB.Location = new System.Drawing.Point(4, 5);
            this.AutoNightModeCB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AutoNightModeCB.Name = "AutoNightModeCB";
            this.AutoNightModeCB.Size = new System.Drawing.Size(231, 25);
            this.AutoNightModeCB.TabIndex = 0;
            this.AutoNightModeCB.Text = "Automatische Tag/Nacht-&Wahl";
            this.AutoNightModeCB.UseVisualStyleBackColor = true;
            this.AutoNightModeCB.CheckedChanged += new System.EventHandler(this.AutoNightModeCB_CheckedChanged);
            // 
            // ScrOpacityTB
            // 
            this.ScrOpacityTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ScrOpacityTB.Location = new System.Drawing.Point(3, 206);
            this.ScrOpacityTB.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ScrOpacityTB.Maximum = 100;
            this.ScrOpacityTB.Name = "ScrOpacityTB";
            this.ScrOpacityTB.Size = new System.Drawing.Size(263, 45);
            this.ScrOpacityTB.TabIndex = 6;
            this.ScrOpacityTB.TickFrequency = 10;
            this.ScrOpacityTB.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.ScrOpacityTB.Value = 100;
            this.ScrOpacityTB.Scroll += new System.EventHandler(this.OpacityTB_Scroll);
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.Location = new System.Drawing.Point(4, 180);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "&Undurchsichtigkeit:";
            // 
            // ShowTimeBannerCB
            // 
            this.ShowTimeBannerCB.AutoSize = true;
            this.ShowTimeBannerCB.Checked = true;
            this.ShowTimeBannerCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowTimeBannerCB.Location = new System.Drawing.Point(4, 145);
            this.ShowTimeBannerCB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ShowTimeBannerCB.Name = "ShowTimeBannerCB";
            this.ShowTimeBannerCB.Size = new System.Drawing.Size(167, 25);
            this.ShowTimeBannerCB.TabIndex = 14;
            this.ShowTimeBannerCB.Text = "&Zeitbanner anzeigen";
            this.ShowTimeBannerCB.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.AutoNightModeCB);
            this.flowLayoutPanel1.Controls.Add(this.NightModeCB);
            this.flowLayoutPanel1.Controls.Add(this.AutoSecondsCB);
            this.flowLayoutPanel1.Controls.Add(this.SecondsModeCB);
            this.flowLayoutPanel1.Controls.Add(this.ShowTimeBannerCB);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.ScrOpacityTB);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.BackColorBTN);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.InactiveColorBTN);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.ActiveColorBTN);
            this.flowLayoutPanel1.Controls.Add(this.AnimationsCB);
            this.flowLayoutPanel1.Controls.Add(this.UpdateBTN);
            this.flowLayoutPanel1.Controls.Add(this.ResetBTN);
            this.flowLayoutPanel1.Controls.Add(this.CloseBTN);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(267, 661);
            this.flowLayoutPanel1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Location = new System.Drawing.Point(4, 259);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 26);
            this.label2.TabIndex = 7;
            this.label2.Text = "&Hintergrundfarbe:";
            // 
            // BackColorBTN
            // 
            this.BackColorBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BackColorBTN.Location = new System.Drawing.Point(3, 288);
            this.BackColorBTN.Name = "BackColorBTN";
            this.BackColorBTN.Size = new System.Drawing.Size(263, 38);
            this.BackColorBTN.TabIndex = 8;
            this.BackColorBTN.Text = "Farbe wählen...";
            this.BackColorBTN.UseVisualStyleBackColor = false;
            this.BackColorBTN.Click += new System.EventHandler(this.BackColorBTN_Click);
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            this.label3.Location = new System.Drawing.Point(4, 334);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 26);
            this.label3.TabIndex = 9;
            this.label3.Text = "&Inaktive LEDs-Farbe:";
            // 
            // InactiveColorBTN
            // 
            this.InactiveColorBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InactiveColorBTN.Location = new System.Drawing.Point(3, 363);
            this.InactiveColorBTN.Name = "InactiveColorBTN";
            this.InactiveColorBTN.Size = new System.Drawing.Size(263, 38);
            this.InactiveColorBTN.TabIndex = 10;
            this.InactiveColorBTN.Text = "Farbe wählen...";
            this.InactiveColorBTN.UseVisualStyleBackColor = false;
            this.InactiveColorBTN.Click += new System.EventHandler(this.InactiveColorBTN_Click);
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            this.label4.Location = new System.Drawing.Point(4, 409);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(261, 26);
            this.label4.TabIndex = 11;
            this.label4.Text = "Aktive &LEDs-Farbe:";
            // 
            // ActiveColorBTN
            // 
            this.ActiveColorBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActiveColorBTN.Location = new System.Drawing.Point(3, 438);
            this.ActiveColorBTN.Name = "ActiveColorBTN";
            this.ActiveColorBTN.Size = new System.Drawing.Size(263, 38);
            this.ActiveColorBTN.TabIndex = 12;
            this.ActiveColorBTN.Text = "Farbe wählen...";
            this.ActiveColorBTN.UseVisualStyleBackColor = false;
            this.ActiveColorBTN.Click += new System.EventHandler(this.ActiveColorBTN_Click);
            // 
            // AnimationsCB
            // 
            this.AnimationsCB.AutoSize = true;
            this.AnimationsCB.Checked = true;
            this.AnimationsCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AnimationsCB.Location = new System.Drawing.Point(4, 484);
            this.AnimationsCB.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.AnimationsCB.Name = "AnimationsCB";
            this.AnimationsCB.Size = new System.Drawing.Size(180, 25);
            this.AnimationsCB.TabIndex = 4;
            this.AnimationsCB.Text = "&Animationen anzeigen";
            this.AnimationsCB.UseVisualStyleBackColor = true;
            // 
            // UpdateBTN
            // 
            this.UpdateBTN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateBTN.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UpdateBTN.Font = new System.Drawing.Font("Segoe UI Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UpdateBTN.Location = new System.Drawing.Point(3, 517);
            this.UpdateBTN.Name = "UpdateBTN";
            this.UpdateBTN.Size = new System.Drawing.Size(263, 46);
            this.UpdateBTN.TabIndex = 16;
            this.UpdateBTN.Text = "nach &Updates suchen\r\nVersion:";
            this.UpdateBTN.UseVisualStyleBackColor = false;
            this.UpdateBTN.Click += new System.EventHandler(this.SearchForUpdates);
            // 
            // colorDialog1
            // 
            this.colorDialog1.AnyColor = true;
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.SolidColorOnly = true;
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.CloseBTN;
            this.ClientSize = new System.Drawing.Size(273, 667);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "Einstellungen";
            this.Shown += new System.EventHandler(this.SettingsDialog_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.ScrOpacityTB)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox AutoNightModeCB;
        private System.Windows.Forms.CheckBox NightModeCB;
        private System.Windows.Forms.CheckBox AutoSecondsCB;
        private System.Windows.Forms.CheckBox SecondsModeCB;
        public System.Windows.Forms.CheckBox ShowTimeBannerCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar ScrOpacityTB;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button BackColorBTN;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Button InactiveColorBTN;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button ActiveColorBTN;
        private System.Windows.Forms.ColorDialog colorDialog1;
        public System.Windows.Forms.CheckBox AnimationsCB;
        private System.Windows.Forms.Button ResetBTN;
        private System.Windows.Forms.Button CloseBTN;
        private System.Windows.Forms.Button UpdateBTN;
    }
}