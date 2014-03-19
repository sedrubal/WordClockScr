using System;
using System.Windows.Forms;

using WordClockScr.Elements;

namespace WordClockScr
{
    public partial class SettingsDialog : Form
    {
        #region Form

        #region load

        public SettingsDialog(SettingsMachine SettingsM, bool IsScreensaver = false, Action<bool> ShowTimeBanner = null)
        {
            InitializeComponent();
            this.MySettingsMachine = SettingsM;
            this.Screensavermode = IsScreensaver;
            //this.loadSettings();
            this.ShowTimeBanner = ShowTimeBanner;

            this.UpdateBTN.Text = "nach &Updates suchen\r\nVersion: " + Application.ProductVersion.ToString();

            this.BindData();
        }

        //public void loadSettings()
        //{
        //    try
        //    {
        //        this.AutoSecondsCB.Checked = this.MySettingsMachine.AutoSeconds;
        //        this.AutoNightModeCB.Checked = this.MySettingsMachine.AutoNightMode;
        //        this.SecondsModeCB.Checked = this.MySettingsMachine.SecondsMode;
        //        this.NightModeCB.Checked = this.MySettingsMachine.NightMode;
        //        this.NightModeCB.Enabled = !this.MySettingsMachine.AutoNightMode;
        //        this.SecondsModeCB.Enabled = !this.MySettingsMachine.AutoSeconds;

        //        this.ScrOpacityTB.Value = (int)(this.ScrOpacity * 100);
        //    }
        //    catch (System.Configuration.SettingsPropertyNotFoundException)
        //    {
        //        MessageBox.Show("The default settings could not be located.",
        //            "Failed to Load Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        #endregion

        #region DataBinding

        private void BindData()
        {
            //this.AutoNightModeCB.DataBindings.Add("Checked", Program.MySettings, "AutoNightMode");
            this.ActiveColorBTN.DataBindings.Add("BackColor", this.MySettingsMachine, "ActiveLEDColor");
            this.BackColorBTN.DataBindings.Add("BackColor", this.MySettingsMachine, "BackColor");
        }

        #endregion DataBinding

        private void CloseBTN_Click(object sender, EventArgs e)
        {
            this.MySettingsMachine.Save();
            //MySettings.Read();
            this.Close();
        }

        private void ResetBTN_Click(object sender, EventArgs e)
        {
            this.MySettingsMachine.Reset();
            //MySettings.Read();
            //this.loadSettings();
        }

        #endregion Form

        #region Properties

        private SettingsMachine _mySettingsMachine;

        public SettingsMachine MySettingsMachine
        {
            get { return this._mySettingsMachine; }
            set { this._mySettingsMachine = value; }
        }

        private bool _screensavermode;
        public bool Screensavermode
        {
            get { return _screensavermode; }
            set
            {
                _screensavermode = value;

                this.ShowTimeBannerCB.Visible = !value;
                this.label1.Visible = value;
                this.ScrOpacityTB.Visible = value;
            }
        }

        private void AutoNightModeCB_CheckedChanged(object sender, EventArgs e)
        {
            this.MySettingsMachine.AutoNightMode = this.AutoNightModeCB.Checked;
            this.NightModeCB.Enabled = !(this.MySettingsMachine.AutoNightMode);
        }

        private void NightModeCB_CheckedChanged(object sender, EventArgs e)
        {
            this.MySettingsMachine.NightMode = this.NightModeCB.Checked;
        }

        private void AutoSecondsCB_CheckedChanged(object sender, EventArgs e)
        {
            this.MySettingsMachine.AutoSeconds = this.AutoSecondsCB.Checked;
            this.SecondsModeCB.Enabled = !(this.MySettingsMachine.AutoSeconds);
        }

        private void SecondsModeCB_CheckedChanged(object sender, EventArgs e)
        {
            this.MySettingsMachine.SecondsMode = this.SecondsModeCB.Checked;
        }

        #region Appearance

        public double ScrOpacity
        {
            get
            {
                return this.MySettingsMachine.ScrOpacity;
            }
            set
            {
                if (value != this.MySettingsMachine.ScrOpacity)
                {
                    this.MySettingsMachine.ScrOpacity = value;
                }
            }
        }

        private void OpacityTB_Scroll(object sender, EventArgs e)
        {
            this.ScrOpacity = ((double)this.ScrOpacityTB.Value) / 100;
        }

        private Action<bool> ShowTimeBanner;

        private void ShowTimeBannerCB_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ShowTimeBanner != null)
                this.ShowTimeBanner(this.ShowTimeBannerCB.Checked);
        }

        #region Farben

        private void BackColorBTN_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.MySettingsMachine.BackColor;
            this.colorDialog1.ShowDialog(this);
            this.MySettingsMachine.BackColor = this.colorDialog1.Color;
        }

        private void InactiveColorBTN_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.MySettingsMachine.InactiveLEDColor;
            this.colorDialog1.ShowDialog(this);
            this.MySettingsMachine.InactiveLEDColor = this.colorDialog1.Color;
        }

        private void ActiveColorBTN_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.MySettingsMachine.ActiveLEDColor;
            this.colorDialog1.ShowDialog(this);
            this.MySettingsMachine.ActiveLEDColor = this.colorDialog1.Color;
        }

        #endregion Farben

        #endregion Appearance

        #endregion Properites

        private void SearchForUpdates(object sender, EventArgs e)
        {
            UpdateModule.CheckForUpdates();
            //System.Diagnostics.Process.Start("http://diy-electronics.net/?page_id=93#downloads");
        }

        private void SettingsDialog_Shown(object sender, EventArgs e)
        {
            this.MySettingsMachine.Read();
            //MySettings.updateGUI();
        }
    }
}