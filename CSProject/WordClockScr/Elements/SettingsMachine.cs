using System;
using System.Collections.Generic;
using System.Text;

namespace WordClockScr.Elements
{
    public class SettingsMachine
    {
        #region 

        //public bool AutoNightMode
        //{
        //    get
        //    {
        //        return this.AutoNightMode;
        //    }
        //    set
        //    {
        //        if (value != this.AutoNightMode)
        //        {
        //            this.AutoNightMode = value;
        //        }
        //    }
        //}

        //public bool NightMode
        //{
        //    get
        //    {
        //        return this.NightMode;
        //    }
        //    set
        //    {
        //        if (this.NightMode != value)
        //        {
        //            this.NightMode = value;
        //            if (!(this.parentControler == null))
        //                this.parentControler.MyOutput.NightMode = value;
        //        }
        //    }
        //}

        //public bool AutoSeconds
        //{
        //    get
        //    {
        //        return this.AutoSeconds;
        //    }
        //    set
        //    {
        //        if (value != this.AutoSeconds)
        //        {
        //            this.AutoSeconds = value;
        //        }
        //    }
        //}

        //public bool SecondsMode
        //{
        //    get
        //    {
        //        return this.SecondsMode;
        //    }
        //    set
        //    {
        //        this.SecondsMode = value;
        //    }
        //}

        private WordClockControler _myparentControler;
        public WordClockControler parentControler
        {
            get { return _myparentControler; }
            set
            {
                if (this._myparentControler != value)
                    this._myparentControler = value;
            }
        }

        #endregion

        #region variables

        #region locale
        private bool _autoNightMode = AutoNightModeDef;
        private bool _autoSeconds = AutoSecondsDef;
        private bool _nightMode = NightModeDef;
        private bool _secondsMode = SecondsModeDef;
        private double _scrOpacity = ScrOpacityDef;
        private bool _animations = AnimationsDef;
        private System.Drawing.Color _activeLEDColor = ActiveLEDColorDef;
        private System.Drawing.Color _inactiveLEDColor = InactiveLEDColorDef;
        private System.Drawing.Color _backColor = BackColorDef;
        private bool _showTimeBanner = ShowTimeBannerDef;
        #endregion locale

        #region Properties
        public  bool AutoNightMode
        {
            get
            {
                return this._autoNightMode;
            }
            set
            {
                if(this._autoNightMode != value)
                {
                    this._autoNightMode = value;
                    this.updateGUI();
                }
            }
        }
        public  bool AutoSeconds
        {
            get
            {
                return this._autoSeconds;
            }
            set
            {
                if (this._autoSeconds != value)
                {
                    this._autoSeconds = value;
                    this.updateGUI();
                }
            }
        }
        public  bool NightMode
        {
            get
            {
                return this._nightMode;
            }
            set
            {
                if (this._nightMode != value)
                {
                    this._nightMode = value;
                    this.updateGUI();
                }
            }
        }
        public  bool SecondsMode
        {
            get
            {
                return _secondsMode;
            }
            set
            {
                if (this._secondsMode != value)
                {
                    this._secondsMode = value;
                    this.updateGUI();
                }
            }
        }
        public  double ScrOpacity
        {
            get
            {
                return this._scrOpacity;
            }
            set
            {
                if (this._scrOpacity != value)
                {
                    this._scrOpacity = value;
                    this.updateGUI();
                }
            }
        }
        public  bool Animations
        {
            get
            {
                return this._animations;
            }
            set
            {
                if (this._animations != value)
                {
                    this._animations = value;
                    this.updateGUI();
                }
            }
        }
        public  System.Drawing.Color ActiveLEDColor
        {
            get
            {
                return this._activeLEDColor;
            }
            set
            {
                if (this._activeLEDColor != value)
                {
                    this._activeLEDColor = value;
                    this.updateGUI();
                }
            }
        }
        public  System.Drawing.Color InactiveLEDColor
        {
            get
            {
                return this._inactiveLEDColor;
            }
            set
            {
                if (this._inactiveLEDColor != value)
                {
                    this._inactiveLEDColor = value;
                    this.updateGUI();
                }
            }
        }
        public  System.Drawing.Color BackColor
        {
            get
            {
                return this._backColor;
            }
            set
            {
                if (this._backColor != value)
                {
                    this._backColor = value;
                    this.updateGUI();
                }
            }
        }
        public  bool ShowTimeBanner
        {
            get
            {
                return this._showTimeBanner;
            }
            set
            {
                if (this._showTimeBanner != value)
                {
                    this._showTimeBanner = value;
                    this.updateGUI();
                }
            }
        }
        #endregion Properties

        #region defaultvalues

        public static bool AutoNightModeDef
        {
            get { return true; }
        }
        public static bool AutoSecondsDef
        {
            get { return true; }
        }
        public static bool NightModeDef
        {
            get { return true; }
        }
        public static bool SecondsModeDef
        {
            get { return true; }
        }
        public static double ScrOpacityDef
        {
            get { return 0.9; }
        }
        public static bool AnimationsDef
        {
            get { return true; }
        }
        public static System.Drawing.Color ActiveLEDColorDef
        {
            get { return System.Drawing.Color.WhiteSmoke; }
        }
        public static System.Drawing.Color InactiveLEDColorDef
        {
            get { return System.Drawing.Color.DarkGray; }
        }
        public static System.Drawing.Color BackColorDef
        {
            get { return System.Drawing.Color.Black; }
        }
        public static bool ShowTimeBannerDef
        {
            get { return true; }
        }

        #endregion defaultvalues

        #endregion variables

        #region File

        public  string SettingsPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WordClockScr\\settings.conf";
            }
        }

        public  void Read()
        {
            string sContent = "";
            try
            {
                if (System.IO.File.Exists(SettingsPath))
                {
                    System.IO.StreamReader myFile = new System.IO.StreamReader(SettingsPath, System.Text.Encoding.Default);
                    sContent = myFile.ReadToEnd();
                    myFile.Close();
                }
                else
                {
                    this.Reset();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("An error occured, while reading the application-settings:" + ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            List<string> lines = new List<string>(sContent.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));

            foreach (string ln in lines)
            {
                string line = ln.Trim().ToLower();
                if (!line.StartsWith("#"))
                {
                    if (line.Contains("autonightmode"))
                    {
                        try {
                            bool var;
                            bool.TryParse(line.Replace("autonightmode", "").Trim(), out var);
                            this.AutoNightMode = var;
                        }
                        catch (Exception) { }
                    }
                    else if (line.Contains("autoseconds"))
                    {
                        try
                        {
                            bool var;
                            bool.TryParse(line.Replace("autoseconds", "").Trim(), out var);
                            this.AutoSeconds = var;
                        }
                        catch (Exception) { }
                    }
                    else if (line.Contains("nightmode"))
                    {
                        try
                        {
                            bool var;
                            bool.TryParse(line.Replace("nightmode", "").Trim(), out var);
                            this.NightMode = var;
                        }
                        catch (Exception) { }
                    }
                    else if (line.Contains("secondsmode"))
                    {
                        try
                        {
                            bool var;
                            bool.TryParse(line.Replace("secondsmode", "").Trim(), out var);
                            this.SecondsMode = var;
                        }
                        catch (Exception) { }
                    }
                    else if (line.Contains("scropacity"))
                    {
                        try
                        {
                            double var;
                            double.TryParse(line.Replace("scropacity", "").Trim(), out var);
                            this.ScrOpacity = var;
                        }
                        catch (Exception) { }
                    }
                    else if (line.Contains("animations"))
                    {
                        try
                        {
                            bool var;
                            bool.TryParse(line.Replace("animations", "").Trim(), out var);
                            this.Animations = var;
                        }
                        catch (Exception) { }
                    }
                    else if (line.Contains("activeledcolor"))
                    {
                        try
                        {
                            System.Drawing.Color var;
                            ColorTryParse(line.Replace("activeledcolor", "").Trim(), out var);
                            this.ActiveLEDColor = var;
                        }
                        catch (Exception) { }
                    }
                    else if (line.Contains("inactiveledcolor"))
                    {
                        try
                        {
                            System.Drawing.Color var;
                            ColorTryParse(line.Replace("inactiveledcolor", "").Trim(), out var);
                            this.InactiveLEDColor = var;
                        }
                        catch (Exception) { }
                    }
                    else if (line.Contains("backcolor"))
                    {
                        try
                        {
                            System.Drawing.Color var;
                            ColorTryParse(line.Replace("backcolor", "").Trim(), out var);
                            this.BackColor = var;
                        }
                        catch (Exception) { }
                    }
                    else if (line.Contains("showtimebanner"))
                    {
                        try
                        {
                            bool var;
                            bool.TryParse(line.Replace("showtimebanner", "").Trim(), out var);
                            this.ShowTimeBanner = var;
                        }
                        catch (Exception) { }
                    }
                }
            }
        }

        public  void Save()
        {
            string sLines = "# Configuration-file for the wordclock-screensaver";

            if (AutoNightMode != AutoNightModeDef) { sLines += Environment.NewLine + "AutoNightMode " + AutoNightMode.ToString(); }
            if (AutoSeconds != AutoSecondsDef) { sLines += Environment.NewLine + "AutoSeconds " + AutoSeconds.ToString(); }
            if (NightMode != NightModeDef) { sLines += Environment.NewLine + "NightMode " + NightMode.ToString(); }
            if (SecondsMode != SecondsModeDef) { sLines += Environment.NewLine + "SecondsMode " + SecondsMode.ToString(); }
            if (ScrOpacity != ScrOpacityDef) { sLines += Environment.NewLine + "ScrOpacity " + ScrOpacity.ToString(); }
            if (Animations != AnimationsDef) { sLines += Environment.NewLine + "Animations " + Animations.ToString(); }
            if (ActiveLEDColor != ActiveLEDColorDef) { sLines += Environment.NewLine + "ActiveLEDColor " + ColorToString(ActiveLEDColor); }
            if (InactiveLEDColor != InactiveLEDColorDef) { sLines += Environment.NewLine + "InactiveLEDColor " + ColorToString(InactiveLEDColor); }
            if (BackColor != BackColorDef) { sLines += Environment.NewLine + "BackColor " + ColorToString(BackColor); }
            if (ShowTimeBanner != ShowTimeBannerDef) { sLines += Environment.NewLine + "ShowTimeBanner " + ShowTimeBanner.ToString(); }

            if (sLines != "# Configuration-file for the wordclock-screensaver")
            {
                try
                {
                    System.IO.DirectoryInfo dir = System.IO.Directory.GetParent(SettingsPath);
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    System.IO.StreamWriter myFile = new System.IO.StreamWriter(SettingsPath);
                    myFile.Write(sLines);
                    myFile.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("An error occured, while saving the application-settings:" + Environment.NewLine + ex.Message, "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            
        }

        public  void Reset()
        {
            AutoNightMode = AutoNightModeDef;
            AutoSeconds = AutoSecondsDef;
            NightMode = NightModeDef;
            SecondsMode = SecondsModeDef;
            ScrOpacity = ScrOpacityDef;
            Animations = AnimationsDef;
            System.Drawing.Color ActiveLEDColor = ActiveLEDColorDef;
            System.Drawing.Color InactiveLEDColor = InactiveLEDColorDef;
            System.Drawing.Color BackColor = BackColorDef;
            ShowTimeBanner = ShowTimeBannerDef;
        }

        #endregion File

        #region Update

        public  void updateGUI()
        {
            //HACK todo
            if (Program.screenSaver != null)
            {
                //autoNightMode
                //autoSeconds
                //NightMode
                //SecondsMode
                //ScrOpacity
                //Animations
                //ShowTimeBanner
                //ActiveLEDColor
                Program.screenSaver.WordClockPNL.ForeColor = this.ActiveLEDColor;
                //InactiveLEDColor
                Program.screenSaver.WordClockPNL.InactiveLEDColor = this.InactiveLEDColor;
                //BackColor
                Program.screenSaver.WordClockPNL.BackColor = this.BackColor;
                //ShowTimeBanner
                Program.screenSaver.TimeLBL.Visible = this.ShowTimeBanner;

            }
            //if (Program.optionsForm != null)
            //{
            //    //autoNightMode
            //    //autoSeconds
            //    //NightMode
            //    //SecondsMode
            //    //ScrOpacity
            //    //Animations
            //    Program.optionsForm.AnimationsCB.Checked = this.Animations;
            //    //ShowTimeBanner
            //    Program.optionsForm.ShowTimeBannerCB.Checked = this.ShowTimeBanner;
            //    //ActiveLEDColor
            //    Program.optionsForm.ActiveColorBTN.BackColor = this.ActiveLEDColor;
            //    Program.optionsForm.BackColorBTN.ForeColor = this.ActiveLEDColor;
            //    Program.optionsForm.InactiveColorBTN.ForeColor = this.ActiveLEDColor;
            //    //InactiveLEDColor
            //    Program.optionsForm.InactiveColorBTN.BackColor = this.InactiveLEDColor;
            //    //BackColor
            //    Program.optionsForm.BackColorBTN.BackColor = this.BackColor;
            //    Program.optionsForm.ActiveColorBTN.ForeColor = this.BackColor;
            //    //ShowTimeBanner
            //    Program.screenSaver.TimeLBL.Visible = this.ShowTimeBanner;
            //    //BackColor
            //}

        }

        #endregion Update

        #region tools

        private  void ColorTryParse(String clrstr, out System.Drawing.Color ncol)
        {
            short a = 0, r = 0, g = 0, b = 0;
            string [] var = clrstr.Split(',');
            if (var.Length == 4)
            {
                try { short.TryParse(var[0], out a); }
                catch (Exception) { }
                try { short.TryParse(var[1], out r); }
                catch (Exception) { }
                try { short.TryParse(var[2], out g); }
                catch (Exception) { }
                try { short.TryParse(var[3], out b); }
                catch (Exception) { }
            }
            else if (var.Length == 3)
            {
                try { short.TryParse(var[0], out r); }
                catch (Exception) { }
                try { short.TryParse(var[1], out g); }
                catch (Exception) { }
                try { short.TryParse(var[2], out b); }
                catch (Exception) { }
            }
            ncol = System.Drawing.Color.FromArgb(a,r,g,b);
        }

        private  string ColorToString(System.Drawing.Color clr)
        {
            return clr.A + "," + clr.R + "," + clr.G + "," + clr.B;
        }

        #endregion tools
    }
}