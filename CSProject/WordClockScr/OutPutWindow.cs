using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using WordClockScr.Elements;
using System.Runtime.InteropServices;
using WordClockScr.tools.Animations;

namespace WordClockScr
{
    public partial class OutPutWindow : Form
    {
        #region WordClockComponenten

        public WordClockControler MyWordClockControler;

        private SettingsMachine _mySettingsMachine;
        public SettingsMachine MySettingsMachine
        {
            get
            {
                if (this._mySettingsMachine == null)
                    this._mySettingsMachine = new SettingsMachine();
                return this._mySettingsMachine;
            }
            set { this._mySettingsMachine = value; }
        }

        #endregion WordClockComponenten

        public OutPutWindow(bool screensaver = false, IntPtr PreviewHandle = default(IntPtr))
        {
            if (screensaver && !(PreviewHandle != default(IntPtr)) && this.MySettingsMachine.Animations)
                this.Opacity = 0.0;
            InitializeComponent();

            this.IsScreensaver = screensaver;
            this.PreviewWndHandle = PreviewHandle;
            this.IsPreview = (PreviewHandle != default(IntPtr));

            this.TimeLBL.Visible = (this.MySettingsMachine.ShowTimeBanner && !this.IsScreensaver);

            // Use double buffering to improve drawing performance
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            this.MyWordClockControler = new WordClockControler(this.WordClockPNL, new TimeMachine(), this.MySettingsMachine, new Timer(), new NightModeController());

            if (IsScreensaver && !IsPreview && this.MySettingsMachine.Animations)
                this.StartFading(true);
        }

        #region Screensaver

        private bool _isScreensaver = false;
        public bool IsScreensaver
        {
            get { return this._isScreensaver; }
            set
            {
                this._isScreensaver = value;
                if (value)
                {
                    // Capture the mouse
                    this.Capture = true;

                    // Set the application to full screen mode and hide the mouse
                    Cursor.Hide();
                    ToggleFullscreen(FullscreenMode.ForceFullscreen);
                    Bounds = Screen.PrimaryScreen.Bounds;

                    this.KeyDown += this.Screensaver_KeyDown;
                    this.WordClockPNL.KeyDown += this.Screensaver_KeyDown;
                    this.MouseMove += this.Screensaver_MouseMove;
                    this.WordClockPNL.MouseMove += this.Screensaver_MouseMove;
                    this.FormClosing += this.Screensaver_FormClosing;

                    this.TimeTMR.Tick -= this.TimeTMR_Tick;
                    this.OptionsBTN.Click -= this.OptionsBTN_Click;
                    this.MouseDoubleClick -= new System.Windows.Forms.MouseEventHandler(this.ToggleFullscreen);
                    this.WordClockPNL.MouseDoubleClick -= new System.Windows.Forms.MouseEventHandler(this.ToggleFullscreen);
                    this.KeyDown -= new System.Windows.Forms.KeyEventHandler(Window_KeyDown);

                    ShowInTaskbar = false;
                }
                else
                {
                    this.Capture = false;
                    Cursor.Show();
                    ToggleFullscreen(FullscreenMode.PreventFullscreen);

                    this.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.Screensaver_KeyDown);
                    this.WordClockPNL.KeyDown -= new System.Windows.Forms.KeyEventHandler(this.Screensaver_KeyDown);
                    this.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.Screensaver_MouseMove);
                    this.WordClockPNL.MouseMove -= new System.Windows.Forms.MouseEventHandler(this.Screensaver_MouseMove);
                    this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.Screensaver_FormClosing);

                    this.TimeTMR.Tick += new System.EventHandler(this.TimeTMR_Tick);
                    this.OptionsBTN.Click += new System.EventHandler(this.OptionsBTN_Click);
                    this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ToggleFullscreen);
                    this.WordClockPNL.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ToggleFullscreen);
                    this.KeyDown += new System.Windows.Forms.KeyEventHandler(Window_KeyDown);

                    ShowInTaskbar = true;
                }
            }
        }

        #region Preview

        private IntPtr PreviewWndHandle;
        private bool _isPreview = false;
        public bool IsPreview
        {
            get { return this._isPreview; }
            set
            {
                this._isPreview = value;
                if (value)
                {
                    // Set the preview window as the parent of this window
                    SetParent(this.Handle, PreviewWndHandle);

                    // Make this a child window so it will close when the parent dialog closes
                    // GWL_STYLE = -16, WS_CHILD = 0x40000000
                    SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

                    // Place our window inside the parent
                    Rectangle ParentRect;
                    GetClientRect(PreviewWndHandle, out ParentRect);
                    this.Size = ParentRect.Size;
                    this.Location = new Point(0, 0);

                    Cursor.Show();
                }
                else
                {
                    // Set the preview window as the parent of this window
                    //SetParent(this.Handle, IntPtr.Zero);

                    //// Make this a child window so it will close when the parent dialog closes
                    //// GWL_STYLE = -16, WS_CHILD = 0x40000000
                    //SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

                    //// Place our window inside the parent
                    //Rectangle ParentRect;
                    //GetClientRect(PreviewWndHandle, out ParentRect);
                    //Size = ParentRect.Size;
                    //Location = new Point(0, 0);
                }
            }
        }

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion Preview

        #region Animation

        private System.Windows.Forms.Timer _animationTimer;
        internal System.Windows.Forms.Timer AnimationTimer
        {
            get
            {
                if (this._animationTimer == null)
                {
                    this._animationTimer = new System.Windows.Forms.Timer();
                    this._animationTimer.Interval = 1;
                    this._animationTimer.Tick += AnimationTimer_Elapsed;
                }
                return this._animationTimer;
            }
        }

        private const int FadeInTime = 50;
        private const int FadeOutTime = 50;
        private double AnimationTicks = 0;

        private FadeMode _fading = FadeMode.nothing;
        internal FadeMode Fading
        {
            get { return this._fading; }
            set
            {
                if (value != this._fading)
                {
                    this._fading = value;
                    this.AnimationTicks = 0;
                    this.AnimationTimer.Start();
                }
            }
        }

        private void AnimationTimer_Elapsed(object sender, EventArgs e)
        {
            if (IsScreensaver && !IsPreview && this.MySettingsMachine.Animations)
            {
                this.AnimationTicks += AnimationTimer.Interval;
                if (Fading == FadeMode.fadeOut)
                {
                    if (this.AnimationTicks >= FadeOutTime)
                    {
                        this.AnimationTimer.Stop(); this.Opacity = 0; this.Fading = FadeMode.nothing; this.Close(); Application.Exit();
                    }
                    else
                    {
                        this.Opacity = (1 - AnimationTicks / FadeOutTime) * this.MySettingsMachine.ScrOpacity;
                    }
                }
                else if (Fading == FadeMode.fadeIn)
                {
                    if (this.AnimationTicks >= FadeInTime)
                    {
                        this.AnimationTimer.Stop(); this.Opacity = this.MySettingsMachine.ScrOpacity; this.Fading = FadeMode.nothing;
                    }
                    else
                    {
                        this.Opacity = AnimationTicks / FadeInTime * this.MySettingsMachine.ScrOpacity;
                    }
                }
            }
            else
            { this.Opacity = 1.0; AnimationTimer.Stop(); }
        }

        public void StartFading(bool FadingIn)
        {
            if (FadingIn)
            {
                this.Opacity = 0.0; this.Visible = true; this.Fading = FadeMode.fadeIn;
            }
            else
            {
                this.Opacity = this.MySettingsMachine.ScrOpacity; this.Visible = true; this.Fading = FadeMode.fadeOut;
            }
        }

        #endregion Animation

        #region Beenden

        private void Screensaver_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsScreensaver && !IsPreview && this.Fading != FadeMode.fadeOut)
            {
                if (this.MySettingsMachine.Animations)
                    this.StartFading(false);
                else
                    this.Close();
                //this.Hide();
                //Application.Exit();

            }
        }

        static int OldX, OldY;
        private void Screensaver_MouseMove(object sender, MouseEventArgs e)
        {
            //Determines whether the mouse was moved and whether the movement was large.
            //if so, the screen saver is ended.
            if ((OldX > 0 & OldY > 0) & (System.Math.Abs(e.X - OldX) > 3 | System.Math.Abs(e.Y - OldY) > 3))
            {
                if (IsScreensaver && !IsPreview && this.Fading != FadeMode.fadeOut)
                {
                    if (this.MySettingsMachine.Animations)
                        this.StartFading(false);
                    else
                        this.Close();
                    //this.Hide();
                    //Application.Exit();
                }
            }

            //Assigns the current X and Y locations to OldX and OldY.
            OldX = e.X;
            OldY = e.Y;
        }

        private void Screensaver_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Cursor.Show();
        }

        #endregion Beenden

        #endregion Screensaver

        #region WindowsForm

        private void TimeTMR_Tick(object sender, EventArgs e)
        {
            this.TimeLBL.Text = DateTime.Now.ToString();
        }

        #region Fullscreen
        private void ToggleFullscreen(object sender, EventArgs e)
        {
            this.ToggleFullscreen();
        }
        private void ToggleFullscreen(object sender, MouseEventArgs e)
        {
            this.ToggleFullscreen();
        }
        private enum FullscreenMode { nothing, ForceFullscreen, PreventFullscreen }
        private void ToggleFullscreen(FullscreenMode mode = FullscreenMode.nothing)
        {
            if (((mode != FullscreenMode.PreventFullscreen) && this.FormBorderStyle == System.Windows.Forms.FormBorderStyle.Sizable && this.WindowState == FormWindowState.Normal && !this.TopMost && OptionsBTN.Visible) || (mode == FullscreenMode.ForceFullscreen))
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; this.WindowState = FormWindowState.Maximized; this.TopMost = true; OptionsBTN.Visible = false; this.TimeLBL.Visible = (this.MySettingsMachine.ShowTimeBanner && !this.IsScreensaver);
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable; this.WindowState = FormWindowState.Normal; this.TopMost = false; OptionsBTN.Visible = !(this.IsScreensaver); this.TimeLBL.Visible = this.MySettingsMachine.ShowTimeBanner;
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
                this.ToggleFullscreen();
            if (e.KeyCode == Keys.Escape)
                this.ToggleFullscreen(FullscreenMode.PreventFullscreen);
        }
        #endregion Fullscreen

        public void ShowTimeBanner(bool show = true)
        {
            this.TimeLBL.Visible = show;
        }
        private void OptionsBTN_Click(object sender, EventArgs e)
        {
            SettingsDialog sd = new SettingsDialog(this.MySettingsMachine, IsScreensaver, new Action<bool>(ShowTimeBanner));
            sd.ShowDialog(this);
            this.TimeLBL.Visible = (this.MySettingsMachine.ShowTimeBanner && !this.IsScreensaver);
        }

        #endregion WindowsForm

        private void OutPutWindow_Shown(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.MySettingsMachine.Read();
                this.MySettingsMachine.updateGUI();
            }
        }
    }
}