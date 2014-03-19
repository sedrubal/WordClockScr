using WordClockScr.Elements;

namespace WordClockScr
{
    partial class OutPutWindow
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutPutWindow));
            this.TimeLBL = new System.Windows.Forms.Label();
            this.OptionsBTN = new System.Windows.Forms.Button();
            this.TimeTMR = new System.Windows.Forms.Timer(this.components);
            this.WordClockPNL = new WordClockScr.Elements.OutPut();
            this.SuspendLayout();
            // 
            // TimeLBL
            // 
            this.TimeLBL.AutoEllipsis = true;
            this.TimeLBL.Dock = System.Windows.Forms.DockStyle.Top;
            this.TimeLBL.Location = new System.Drawing.Point(0, 0);
            this.TimeLBL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TimeLBL.Name = "TimeLBL";
            this.TimeLBL.Size = new System.Drawing.Size(949, 30);
            this.TimeLBL.TabIndex = 0;
            this.TimeLBL.Text = "--:--";
            this.TimeLBL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OptionsBTN
            // 
            this.OptionsBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OptionsBTN.AutoSize = true;
            this.OptionsBTN.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OptionsBTN.Image = global::WordClockScr.Properties.Resources.Options;
            this.OptionsBTN.Location = new System.Drawing.Point(919, 0);
            this.OptionsBTN.Name = "OptionsBTN";
            this.OptionsBTN.Padding = new System.Windows.Forms.Padding(2);
            this.OptionsBTN.Size = new System.Drawing.Size(30, 30);
            this.OptionsBTN.TabIndex = 2;
            this.OptionsBTN.UseVisualStyleBackColor = true;
            // 
            // TimeTMR
            // 
            this.TimeTMR.Enabled = true;
            this.TimeTMR.Interval = 500;
            // 
            // WordClockPNL
            // 
            this.WordClockPNL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WordClockPNL.InactiveLEDColor = System.Drawing.Color.Empty;
            this.WordClockPNL.Location = new System.Drawing.Point(0, 30);
            this.WordClockPNL.Margin = new System.Windows.Forms.Padding(0);
            this.WordClockPNL.Name = "WordClockPNL";
            this.WordClockPNL.NightMode = false;
            this.WordClockPNL.Size = new System.Drawing.Size(949, 531);
            this.WordClockPNL.TabIndex = 0;
            // 
            // OutPutWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(949, 561);
            this.Controls.Add(this.OptionsBTN);
            this.Controls.Add(this.WordClockPNL);
            this.Controls.Add(this.TimeLBL);
            this.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "OutPutWindow";
            this.Text = "WordClock";
            this.Shown += new System.EventHandler(this.OutPutWindow_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label TimeLBL;
        private System.Windows.Forms.Button OptionsBTN;
        private System.Windows.Forms.Timer TimeTMR;
        public OutPut WordClockPNL;
    }
}

