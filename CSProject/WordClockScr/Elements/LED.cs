using WordClockScr.tools.Animations;

namespace WordClockScr.Elements
{
    public class LED
    {
        
        #region Location in Matrix

      private int _xlocation;
      public int xLocation { get { return this._xlocation; } }
      private int _ylocation;
      public int yLocation { get { return this._ylocation; } }

      public LED(int xLocation, int yLocation)
      {
          this._xlocation = xLocation; this._ylocation = yLocation;
      }

      #endregion Location in Matrix

        #region Animation

        private const double AnimationDurance = 300;
        
        private System.Timers.Timer _animationTimer;
        public System.Timers.Timer AnimationTimer
        {
            get
            {
                if (this._animationTimer == null)
                {
                    this._animationTimer = new System.Timers.Timer(10);
                    this._animationTimer.Elapsed += AnimationTimer_Elapsed;
                }
                return this._animationTimer;
            }
        }

        private void AnimationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            double p = this.LumenPercent;
            switch (this.Fading)
	        {
                case FadeMode.fadeIn:
                    p += this.AnimationTimer.Interval * 1/AnimationDurance;
                    if (p >= 1.0) // || !MySettings.Animations)
                    {
                        p = 1.0;
                        this.Fading = FadeMode.nothing;
                    }
                 break;
                case FadeMode.fadeOut:
                 p -= this.AnimationTimer.Interval * 1 / AnimationDurance;
                 if (p <= 0) // || !MySettings.Animations)
                 {
                     p = 0.0;
                     this.Fading = FadeMode.nothing;
                 }
                 break;
                case FadeMode.nothing: this.AnimationTimer.Stop(); break;
                default: throw new System.ArgumentException("Wrong FadeMode detected!");
	        }
            this.LumenPercent = p;
        }

        private FadeMode _fading = FadeMode.nothing;
        public FadeMode Fading
        {
            get { return this._fading; }
            set
            {
                if (this._fading != value)
                {
                    this._fading = value;
                    this.AnimationTimer.Enabled = ((value != FadeMode.nothing));// &&  MySettings.Animations);
                }
            }
        }

        private double _lumenPercent;
        public double LumenPercent
        {
            get { return this._lumenPercent; }
            set 
            {
                if (this._lumenPercent != value)
                {
                    if (value > 1.0 || value < 0.0)
                        throw new System.ArgumentException("PercentValue must be between 0.0 and 1.0");
                    else
                    {
                        AnyDrawingValueChanged args = new AnyDrawingValueChanged(value, this._lumenPercent, "LumenPercent", this.xLocation, this.yLocation);
                        this._lumenPercent = value;
                        OnAnyDrawingValueChanged(args);
                    }
                }
            }
        }

      #endregion Animation

        #region 

      private bool _active = false;
        public bool Active
      {
            get
            {
                return this._active;
            }
            set
            {
                if (value != this._active)
                {
                    AnyDrawingValueChanged args = new AnyDrawingValueChanged(value, this._active, "Active", this.xLocation, this.yLocation);
                    this._active = value;
                    bool useAnimations;
                    if (Program.optionsForm != null)
                    {
                        useAnimations = Program.optionsForm.MySettingsMachine.Animations;
                    }
                    else if (Program.screenSaver != null)
                    {
                        useAnimations = Program.screenSaver.MySettingsMachine.Animations;
                    }
                    else
                    {
                        useAnimations = SettingsMachine.AnimationsDef;
                    }
                    if (useAnimations)
                        this.Fading = (value) ? FadeMode.fadeIn : FadeMode.fadeOut;
                    else
                        this.LumenPercent = (value) ? 1.0 : 0.0;

                    OnAnyDrawingValueChanged(args);
                }
            }
        }
        
        #endregion 

        #region Event

         protected void OnAnyDrawingValueChanged(AnyDrawingValueChanged e)
         {
             AnyDrawingValueChangedEvent(this, e);
         }
         public event AnyDrawingValueChangedEventHandler AnyDrawingValueChangedEvent;
         public delegate void AnyDrawingValueChangedEventHandler(LED sender, AnyDrawingValueChanged e);

    public class AnyDrawingValueChanged : System.EventArgs
    {
        private object _newValue;
        private object _oldValue;
        private string _fieldName;
        private int _xlocation, _ylocation;

        public AnyDrawingValueChanged(object NewValue, object OldValue, string FieldName, int xLocation, int yLocation)
        {
            this._newValue = NewValue;
            this._oldValue = OldValue;
            this._fieldName = FieldName;
            this.xLocation = xLocation; this.yLocation = yLocation;
        }

        public object Active
        {
            get { return this._newValue; }
        }

        public object oldValue
        {
            get { return this._oldValue; }
            set { this._oldValue = value; }
        }

        public string FieldName
        {
            get { return this._fieldName; }
            set { this._fieldName = value; }
        }

        public int xLocation
        {
            get { return this._xlocation; }
            set { this._xlocation = value; }
        }

        public int yLocation
        {
            get { return this._ylocation; }
            set { this._ylocation = value; }
        }
    }

#endregion Event
    
    }
}