using System;
using WordClockScr.Elements;
using System.Windows.Forms;

namespace WordClockScr
{
    /// <summary>
    /// The main control-unit of a WordClock
    /// </summary>
    public class WordClockControler
    {
        #region Properties

        /// <summary>
        /// the Interval to update the current time information and the output
        /// </summary>
        private const int tmrInterval = 500;

        private WordClockScr.Elements.OutPut _myOutput;
        /// <summary>
        /// The wordClock-Output. This class will display the informations, that are given to it
        /// </summary>
        public WordClockScr.Elements.OutPut MyOutput
        {
            get
            {
                //if (this._myOutput == null) this._myOutput = new Substitutes.OutPutSubstitute(); 
                return this._myOutput;
            }
            set
            {
                if (this._myOutput != value) this._myOutput = value;
            }
        }

        private TimeMachine _myTime;
        /// <summary>
        /// a class, that manages the observation of the current time. It's like a clockwork mechanism.
        /// </summary>
        public TimeMachine MyTime
        {
            get
            {
                //if (this._myTime == null) this._myTime = new Time();
                return this._myTime;
            }
            set
            {
                if (this._myTime != value) this._myTime = value;
            }
        }

        private SettingsMachine _mySettingsInput;
        /// <summary>
        /// a class, that manages the import and export of the settings. Its the user-interface
        /// </summary>
        public SettingsMachine MySettingsInput
        {
            get
            {
                return this._mySettingsInput;
            }
            set
            {
                if (this._mySettingsInput != value) this._mySettingsInput = value; this._mySettingsInput.parentControler = this;
            }
        }

        private NightModeController _myNightModeController;
        /// <summary>
        /// A controller, that gives informations about the darkness and defines, if it's better to turn on the nightmode or not.
        /// </summary>
        public NightModeController MyNightModeController
        {
            get
            {
                return this._myNightModeController;
            }
            set
            {
                if (this._myNightModeController != value) this._myNightModeController = value;
            }
        }

        /// <summary>
        /// If the nightmode is active, the the output should be a bit more dark
        /// </summary>
        private bool NightMode
        {
            get
            {
                if (this.MySettingsInput.AutoNightMode)
                {
                    this.MySettingsInput.NightMode = this.MyNightModeController.NightMode;
                    return this.MyNightModeController.NightMode;
                }
                else
                    return this.MySettingsInput.NightMode;
            }
        }

        private Timer _mytmr;
        /// <summary>
        /// the main timer, which updates the timeinformation and so the informations will be send to the output.
        /// </summary>
        public Timer MyTimer
        {
            get
            {
                //if (this._mytmr == null) this._mytmr = new Timer(); _mytmr.Interval = tmrInterval; _mytmr.Elapsed += _mytmr_Tick; _mytmr.Enabled = this.Enabled;
                return this._mytmr;
            }
            set
            {
                if (this._mytmr != value)
                {
                    this._mytmr = value;
                    this._mytmr.Tick += _mytmr_Tick;
                    this._mytmr.Interval = tmrInterval;
                    this._mytmr.Enabled = this.Enabled;
                }
            }
        }

        private bool _enabled = false;
        /// <summary>
        /// a value, wether the controller-class is enabled or not. But the LEDs stay turned on...
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this._enabled;
            }
            set
            {
                if (this._enabled != value)
                {
                    this._enabled = value;
                    this.MyTimer.Enabled = value;
                }
            }
        }

        /// <summary>
        /// a counter variable to switch between text- and secondsmode.
        /// </summary>
        private double switchSecondsTextMode = 0.0;

        #endregion Properties

        /// <summary>
        /// The Constructor of the main Controller
        /// </summary>
        /// <param name="nOutput">a wordClock output (has to inherit from Output)</param>
        /// <param name="nTime">a "clockwork mechanism"(has to inherit from Time)</param>
        /// <param name="nSettingsInput">a user-settings-interface (has to inherit from Settings)</param>
        /// <param name="nTimer">a timer (has to inherit from Timer)</param>
        /// <param name="nNightModeController">a day / night detector (has to inherit from NightmodeController)</param>
        public WordClockControler(OutPut nOutput, TimeMachine nTime, SettingsMachine nSettingsInput, Timer nTimer, NightModeController nNightModeController)
        {
            this.MyOutput = nOutput;
            this.MyTime = nTime;
            this.MySettingsInput = nSettingsInput;
            this.MyTimer = nTimer;
            this.MyNightModeController = nNightModeController;
            this.Enabled = true;
        }

        #region Anzeige

        /// <summary>
        /// The Tick-method, that is called by the main-timer. This method sends converts the time into a string
        /// </summary>
        public void _mytmr_Tick(object sender, EventArgs e) 
        {
            //Automatisch zw. Sekundenmodus und Textmodus umschalten
            if (MySettingsInput.AutoSeconds)
            {
                if (((this.switchSecondsTextMode > 10000) && MySettingsInput.SecondsMode) || ((this.switchSecondsTextMode > 30000) && !MySettingsInput.SecondsMode))
                {
                    MySettingsInput.SecondsMode = !MySettingsInput.SecondsMode;
                    this.switchSecondsTextMode = 0;
                }
                this.switchSecondsTextMode += this.MyTimer.Interval;
            }

            if (MySettingsInput.SecondsMode)
            {
                this.showSeconds(MyTime.GetTimeOfDay().Seconds, this.NightMode);
            }
            else
            {
                TimeOfDay tod = MyTime.GetTimeOfDay();
                string Phrase = "ES IST ";

                if (tod.Minutes >= 55)
                {
                    Phrase += "FÜNF VOR "; tod.Hours += 1;
                }
                else if (tod.Minutes >= 50)
                {
                    Phrase += "ZEHN VOR "; tod.Hours += 1;
                }
                else if (tod.Minutes >= 45)
                {
                    Phrase += "DREIVIERTEL "; tod.Hours += 1;
                }
                else if (tod.Minutes >= 40)
                {
                    Phrase += "ZEHN NACH HALB "; tod.Hours += 1;
                }
                else if (tod.Minutes >= 35)
                {
                    Phrase += "FÜNF NACH HALB "; tod.Hours += 1;
                }
                else if (tod.Minutes >= 30)
                {
                    Phrase += "HALB "; tod.Hours += 1;
                }
                else if (tod.Minutes >= 25)
                {
                    Phrase += "FÜNF VOR HALB "; tod.Hours += 1;
                }
                else if (tod.Minutes >= 20)
                {
                    Phrase += "ZEHN VOR HALB "; tod.Hours += 1;
                }
                else if (tod.Minutes >= 15)
                {
                    Phrase += "VIERTEL "; tod.Hours += 1;
                }
                else if (tod.Minutes >= 10)
                {
                    Phrase += "ZEHN NACH "; ;
                }
                else if (tod.Minutes >= 5)
                {
                    Phrase += "FÜNF NACH "; ;
                }
                //Todo vor nach;
                switch (tod.Hours)
                {
                    case 1: case 13: Phrase += "EINS "; break;
                    case 2: case 14: Phrase += "ZWEI "; break;
                    case 3: case 15: Phrase += "DREI "; break;
                    case 4: case 16: Phrase += "VIER "; break;
                    case 5: case 17: Phrase += "FÜNF "; break;
                    case 6: case 18: Phrase += "SECHS "; break;
                    case 7: case 19: Phrase += "SIEBEN "; break;
                    case 8: case 20: Phrase += "ACHT "; break;
                    case 9: case 21: Phrase += "NEUN "; break;
                    case 10: case 22: Phrase += "ZEHN "; break;
                    case 11: case 23: Phrase += "ELF "; break;
                    case 12: case 24: case 0: Phrase += "ZWÖLF "; break;
                    default:
                        throw new System.ArgumentException("Falsche Uhrzeit");
                }
                if (!(Phrase.Contains("VOR") || Phrase.Contains("NACH") || Phrase.Contains("VIERTEL") || Phrase.Contains("HALB")))
                {
                    Phrase += "UHR";
                    if (Phrase.Contains("EINS"))
                        Phrase = Phrase.Replace("EINS", "EIN");
                }
                this.showPhrase(Phrase, this.NightMode);
                this.showMinutes((short)(tod.Minutes % 5), this.NightMode);
            }
        }

        private object _value;
        /// <summary>
        /// The current value, that is shown on the output. If it is a string, then a phrase there will be show, otherwise you can see the seconds.
        /// </summary>
        public object Value
        {
            get { return this._value; }
            set 
            {
                if (!Equals(this._value, value))
                {
                    this._value = value;

                    for (int x = 0; x < this.MyOutput.LEDMatrix.Length; x++)
                    {
                        for (int y = 0; y < this.MyOutput.LEDMatrix[0].Length; y++)
                        {
                            this.MyOutput.LEDMatrix[x][y].Active = false;
                        }
                    }
                    if (this.Value is string)
                    {
                        activatePhrase((string)this.Value);
                    }
                    else if (this.Value is short)
                    {
                        activateSeconds((short)this.Value);
                    }

                }
            }
        }

        private short _minutesValue;
        /// <summary>
        /// The value of the count of the shown minuteLEDs (0-4)
        /// </summary>
        public short MinutesValue
        {
            get { return this._minutesValue; }
            set
            {
                if (!(this._minutesValue == value))
                {
                    if (value > 4 || value < 0)
                        throw new ArgumentException("Diese Minutenzahl kann nicht angezeigt werden");
                    this._minutesValue = value;

                    for (int i = 0; i < 4; i++)
                    { this.MyOutput.MinutesLEDs[i].Active = false; }
                    if (this.Value is string)
                    { this.activateMinute(this.MinutesValue); }
                }
            }
        }

        /// <summary>
        /// Activates the LEDs for the phrase
        /// </summary>
        /// <param name="Phrase">The phrase, that should be displayed on the output</param>
        private void activatePhrase(string Phrase)
        {
            Phrase = Phrase.ToUpper();
            string[] words = Phrase.Split(' ');
            foreach (string word in words)
            {
                if (!(string.IsNullOrWhiteSpace(word)))
                {
                    int c = CountInArray(words, word);
                    switch (word)
                    {
                        case "ES": this.MyOutput.LEDMatrix[0][0].Active = true; this.MyOutput.LEDMatrix[1][0].Active = true;
                            break;
                        case "IST": this.MyOutput.LEDMatrix[3][0].Active = true; this.MyOutput.LEDMatrix[4][0].Active = true; this.MyOutput.LEDMatrix[5][0].Active = true;
                            break;
                        case "FÜNF":
                            if (Phrase.Contains("VOR") || Phrase.Contains("NACH"))
                            {
                                if (c == 2)
                                {
                                    this.MyOutput.LEDMatrix[7][4].Active = true; this.MyOutput.LEDMatrix[8][4].Active = true; this.MyOutput.LEDMatrix[9][4].Active = true; this.MyOutput.LEDMatrix[10][4].Active = true;
                                    this.MyOutput.LEDMatrix[7][0].Active = true; this.MyOutput.LEDMatrix[8][0].Active = true; this.MyOutput.LEDMatrix[9][0].Active = true; this.MyOutput.LEDMatrix[10][0].Active = true;
                                }
                                else if (c == 1)
                                {
                                    string vornach;
                                    if (Phrase.Contains("VOR")) vornach = "VOR"; else vornach = "NACH";
                                    if (Phrase.IndexOf(vornach) > Phrase.IndexOf(word))
                                    { this.MyOutput.LEDMatrix[7][0].Active = true; this.MyOutput.LEDMatrix[8][0].Active = true; this.MyOutput.LEDMatrix[9][0].Active = true; this.MyOutput.LEDMatrix[10][0].Active = true; }
                                    else
                                    { this.MyOutput.LEDMatrix[7][4].Active = true; this.MyOutput.LEDMatrix[8][4].Active = true; this.MyOutput.LEDMatrix[9][4].Active = true; this.MyOutput.LEDMatrix[10][4].Active = true; }
                                }
                                else
                                    throw new ArgumentException("Das Wort '" + word + "' kommt zu oft vor!");
                            }
                            else
                            { this.MyOutput.LEDMatrix[7][4].Active = true; this.MyOutput.LEDMatrix[8][4].Active = true; this.MyOutput.LEDMatrix[9][4].Active = true; this.MyOutput.LEDMatrix[10][4].Active = true; }
                            break;
                        case "ZEHN":
                            if (Phrase.Contains("VOR") || Phrase.Contains("NACH"))
                            {
                                if (c == 2)
                                {
                                    this.MyOutput.LEDMatrix[0][9].Active = true; this.MyOutput.LEDMatrix[1][9].Active = true; this.MyOutput.LEDMatrix[2][9].Active = true; this.MyOutput.LEDMatrix[3][9].Active = true;
                                    this.MyOutput.LEDMatrix[0][1].Active = true; this.MyOutput.LEDMatrix[1][1].Active = true; this.MyOutput.LEDMatrix[2][1].Active = true; this.MyOutput.LEDMatrix[3][1].Active = true;
                                }
                                else if (c == 1)
                                {
                                    string vornach;
                                    if (Phrase.Contains("VOR")) vornach = "VOR"; else vornach = "NACH";
                                    if (Phrase.IndexOf(vornach) > Phrase.IndexOf(word))
                                    { this.MyOutput.LEDMatrix[0][1].Active = true; this.MyOutput.LEDMatrix[1][1].Active = true; this.MyOutput.LEDMatrix[2][1].Active = true; this.MyOutput.LEDMatrix[3][1].Active = true; }
                                    else
                                    { this.MyOutput.LEDMatrix[0][9].Active = true; this.MyOutput.LEDMatrix[1][9].Active = true; this.MyOutput.LEDMatrix[2][9].Active = true; this.MyOutput.LEDMatrix[3][9].Active = true; }
                                }
                                else
                                    throw new ArgumentException("Das Wort '" + word + "' kommt zu oft vor!");
                            }
                            else
                            { this.MyOutput.LEDMatrix[0][9].Active = true; this.MyOutput.LEDMatrix[1][9].Active = true; this.MyOutput.LEDMatrix[2][9].Active = true; this.MyOutput.LEDMatrix[3][9].Active = true; }
                            break;
                        case "ZWANZIG": this.MyOutput.LEDMatrix[4][1].Active = true; this.MyOutput.LEDMatrix[5][1].Active = true; this.MyOutput.LEDMatrix[6][1].Active = true; this.MyOutput.LEDMatrix[7][1].Active = true; this.MyOutput.LEDMatrix[8][1].Active = true; this.MyOutput.LEDMatrix[9][1].Active = true; this.MyOutput.LEDMatrix[10][1].Active = true;
                            break;
                        case "VIERTEL": this.MyOutput.LEDMatrix[4][2].Active = true; this.MyOutput.LEDMatrix[5][2].Active = true; this.MyOutput.LEDMatrix[6][2].Active = true;
                            this.MyOutput.LEDMatrix[7][2].Active = true; this.MyOutput.LEDMatrix[8][2].Active = true; this.MyOutput.LEDMatrix[9][2].Active = true; this.MyOutput.LEDMatrix[10][2].Active = true;
                            break;
                        case "DREIVIERTEL": this.MyOutput.LEDMatrix[0][2].Active = true; this.MyOutput.LEDMatrix[1][2].Active = true; this.MyOutput.LEDMatrix[2][2].Active = true; this.MyOutput.LEDMatrix[3][2].Active = true; this.MyOutput.LEDMatrix[4][2].Active = true; this.MyOutput.LEDMatrix[5][2].Active = true; this.MyOutput.LEDMatrix[6][2].Active = true;
                            this.MyOutput.LEDMatrix[7][2].Active = true; this.MyOutput.LEDMatrix[8][2].Active = true; this.MyOutput.LEDMatrix[9][2].Active = true; this.MyOutput.LEDMatrix[10][2].Active = true;
                            break;
                        case "VOR": this.MyOutput.LEDMatrix[0][3].Active = true; this.MyOutput.LEDMatrix[1][3].Active = true; this.MyOutput.LEDMatrix[2][3].Active = true;
                            break;
                        case "FUNK": this.MyOutput.LEDMatrix[3][3].Active = true; this.MyOutput.LEDMatrix[4][3].Active = true; this.MyOutput.LEDMatrix[5][3].Active = true; this.MyOutput.LEDMatrix[6][3].Active = true;
                            break;
                        case "NACH": this.MyOutput.LEDMatrix[7][3].Active = true; this.MyOutput.LEDMatrix[8][3].Active = true; this.MyOutput.LEDMatrix[9][3].Active = true; this.MyOutput.LEDMatrix[10][3].Active = true;
                            break;
                        case "HALB": this.MyOutput.LEDMatrix[0][4].Active = true; this.MyOutput.LEDMatrix[1][4].Active = true; this.MyOutput.LEDMatrix[2][4].Active = true; this.MyOutput.LEDMatrix[3][4].Active = true;
                            break;
                        case "EINS": this.MyOutput.LEDMatrix[0][5].Active = true; this.MyOutput.LEDMatrix[1][5].Active = true; this.MyOutput.LEDMatrix[2][5].Active = true; this.MyOutput.LEDMatrix[3][5].Active = true;
                            break;
                        case "EIN": this.MyOutput.LEDMatrix[0][5].Active = true; this.MyOutput.LEDMatrix[1][5].Active = true; this.MyOutput.LEDMatrix[2][5].Active = true;
                            break;
                        case "ZWEI": this.MyOutput.LEDMatrix[7][5].Active = true; this.MyOutput.LEDMatrix[8][5].Active = true; this.MyOutput.LEDMatrix[9][5].Active = true; this.MyOutput.LEDMatrix[10][5].Active = true;
                            break;
                        case "DREI": this.MyOutput.LEDMatrix[0][6].Active = true; this.MyOutput.LEDMatrix[1][6].Active = true; this.MyOutput.LEDMatrix[2][6].Active = true; this.MyOutput.LEDMatrix[3][6].Active = true;
                            break;
                        case "VIER": this.MyOutput.LEDMatrix[7][6].Active = true; this.MyOutput.LEDMatrix[8][6].Active = true; this.MyOutput.LEDMatrix[9][6].Active = true; this.MyOutput.LEDMatrix[10][6].Active = true;
                            break;
                        case "SECHS": this.MyOutput.LEDMatrix[0][7].Active = true; this.MyOutput.LEDMatrix[1][7].Active = true; this.MyOutput.LEDMatrix[2][7].Active = true; this.MyOutput.LEDMatrix[3][7].Active = true; this.MyOutput.LEDMatrix[4][7].Active = true;
                            break;
                        case "SIEBEN": this.MyOutput.LEDMatrix[0][8].Active = true; this.MyOutput.LEDMatrix[1][8].Active = true; this.MyOutput.LEDMatrix[2][8].Active = true; this.MyOutput.LEDMatrix[3][8].Active = true; this.MyOutput.LEDMatrix[4][8].Active = true; this.MyOutput.LEDMatrix[5][8].Active = true;
                            break;
                        case "ACHT": this.MyOutput.LEDMatrix[7][7].Active = true; this.MyOutput.LEDMatrix[8][7].Active = true; this.MyOutput.LEDMatrix[9][7].Active = true; this.MyOutput.LEDMatrix[10][7].Active = true;
                            break;
                        case "NEUN": this.MyOutput.LEDMatrix[3][9].Active = true; this.MyOutput.LEDMatrix[4][9].Active = true; this.MyOutput.LEDMatrix[5][9].Active = true; this.MyOutput.LEDMatrix[6][9].Active = true;
                            break;
                        case "ELF": this.MyOutput.LEDMatrix[5][4].Active = true; this.MyOutput.LEDMatrix[6][4].Active = true; this.MyOutput.LEDMatrix[7][4].Active = true;
                            break;
                        case "ZWÖLF": this.MyOutput.LEDMatrix[6][8].Active = true; this.MyOutput.LEDMatrix[7][8].Active = true; this.MyOutput.LEDMatrix[8][8].Active = true; this.MyOutput.LEDMatrix[9][8].Active = true; this.MyOutput.LEDMatrix[10][8].Active = true;
                            break;
                        //case "ZEHNE": this.MyOutput.LEDMatrix[0][9].Active = true; this.MyOutput.LEDMatrix[1][9].Active = true; this.MyOutput.LEDMatrix[2][9].Active = true;this.MyOutput.LEDMatrix[3][9].Active = true;this.MyOutput.LEDMatrix[4][9].Active = true;
                        //                    break;
                        case "UHR": this.MyOutput.LEDMatrix[8][9].Active = true; this.MyOutput.LEDMatrix[9][9].Active = true; this.MyOutput.LEDMatrix[10][9].Active = true;
                            break;
                        default:
                            //break;
                            throw new ArgumentException("Dieses Wort kann nicht angezeigt werden.");
                    }
                    if (c > 1 && word != "ZEHN" && word != "FÜNF")
                        throw new ArgumentException("Das Wort '" + word + "' kommt zu oft vor!");
                }
            }
        }

        /// <summary>
        /// Activates the LEDs for the numbers of the seconds
        /// </summary>
        /// <param name="seconds">The phrase, that should be displayed on the output</param>
        private void activateSeconds(short seconds)
        {
            if (seconds >= 60 || seconds < 0) throw new ArgumentException("Diese Zahll kann nicht angezeigt werden.");
            this.DrawNumber(0, 2, WordClockControler.GetNumberArray(seconds)[0]);
            this.DrawNumber(6, 2, WordClockControler.GetNumberArray(seconds)[1]);
        }

        /// <summary>
        /// Activates the MinuteLEDs for the minutes
        /// </summary>
        /// <param name="minute">The numbers, that should be displayed on the output</param>
        private void activateMinute(short minute)
        {
            for (short i = 0; i < 4; i++)
            {
                this.MyOutput.MinutesLEDs[i].Active = (minute > i);
            }
            if (minute > 4 || minute < 0)
                throw new ArgumentException("Diese Minutenzahl kann nicht angezeigt werden");
        }
        
        /// <summary>
        /// activates the LEDs for the number on x-, und y-position
        /// </summary>
        /// <param name="x">the x-Position of the number (left side)</param>
        /// <param name="y">the y-Position og the number (upper side)</param>
        /// <param name="Number">the number, that should be displayed</param>
        private void DrawNumber(int x, int y, int Number)
        {
            switch (Number)
            {
                case 0:
                                                        this.MyOutput.LEDMatrix[x + 1][y].Active = true; this.MyOutput.LEDMatrix[x + 2][y].Active = true; this.MyOutput.LEDMatrix[x + 3][y].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 1].Active = true;                                                                                                                 this.MyOutput.LEDMatrix[x + 4][y + 1].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 2].Active = true;                                                                             this.MyOutput.LEDMatrix[x + 3][y + 2].Active = true;this.MyOutput.LEDMatrix[x + 4][y + 2].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 3].Active = true;                                     this.MyOutput.LEDMatrix[x + 2][y+3].Active = true;                                 this.MyOutput.LEDMatrix[x + 4][y + 3].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 4].Active = true; this.MyOutput.LEDMatrix[x + 1][y+4].Active = true;                                                                                                                this.MyOutput.LEDMatrix[x + 4][y+4].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 5].Active = true;                                                                                                                 this.MyOutput.LEDMatrix[x + 4][y+5].Active = true;
                                                        this.MyOutput.LEDMatrix[x + 1][y+6].Active = true; this.MyOutput.LEDMatrix[x + 2][y+6].Active = true; this.MyOutput.LEDMatrix[x + 3][y+6].Active = true;
                    break;
                case 1:
                                                            this.MyOutput.LEDMatrix[x + 2][y].Active = true;
                    this.MyOutput.LEDMatrix[x+1][y+1].Active = true; this.MyOutput.LEDMatrix[x + 2][y+1].Active = true;
                                                            this.MyOutput.LEDMatrix[x + 2][y + 2].Active = true;
                                                            this.MyOutput.LEDMatrix[x + 2][y + 3].Active = true;
                                                            this.MyOutput.LEDMatrix[x + 2][y + 4].Active = true;
                                                            this.MyOutput.LEDMatrix[x + 2][y + 5].Active = true;
                    this.MyOutput.LEDMatrix[x+1][y+6].Active = true; this.MyOutput.LEDMatrix[x+2][y+6].Active = true; this.MyOutput.LEDMatrix[x+3][y+6].Active = true;
                    break;
                case 2:
                                                        this.MyOutput.LEDMatrix[x + 1][y].Active = true; this.MyOutput.LEDMatrix[x + 2][y].Active = true; this.MyOutput.LEDMatrix[x + 3][y].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 1].Active = true;                                                                                                                 this.MyOutput.LEDMatrix[x + 4][y + 1].Active = true;
                                                                                                                                                                             this.MyOutput.LEDMatrix[x + 4][y + 2].Active = true;
                                                                                                                                        this.MyOutput.LEDMatrix[x + 3][y+3].Active = true;
                                                                                                this.MyOutput.LEDMatrix[x + 2][y+4].Active = true;
                                                        this.MyOutput.LEDMatrix[x + 1][y + 5].Active = true; 
                    this.MyOutput.LEDMatrix[x][y+6].Active = true; this.MyOutput.LEDMatrix[x+1][y+6].Active = true; this.MyOutput.LEDMatrix[x+2][y+6].Active = true; this.MyOutput.LEDMatrix[x+3][y+6].Active = true;this.MyOutput.LEDMatrix[x+4][y+6].Active = true; 

                    break;
                case 3:
                    this.MyOutput.LEDMatrix[x][y].Active = true; this.MyOutput.LEDMatrix[x + 1][y].Active = true; this.MyOutput.LEDMatrix[x + 2][y].Active = true; this.MyOutput.LEDMatrix[x + 3][y].Active = true; this.MyOutput.LEDMatrix[x + 4][y].Active = true;
                                                                                                                                        this.MyOutput.LEDMatrix[x + 3][y + 1].Active = true;
                                                                                                this.MyOutput.LEDMatrix[x + 2][y + 2].Active = true;
                                                                                                                                        this.MyOutput.LEDMatrix[x + 3][y + 3].Active = true;
                                                                                                                                                                                this.MyOutput.LEDMatrix[x + 4][y + 4].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 5].Active = true;                                                                                                                     this.MyOutput.LEDMatrix[x + 4][y + 5].Active = true;
                                                        this.MyOutput.LEDMatrix[x + 1][y + 6].Active = true; this.MyOutput.LEDMatrix[x + 2][y + 6].Active = true; this.MyOutput.LEDMatrix[x + 3][y + 6].Active = true;       
                    break;
                case 4:
                                                                                                                                                        this.MyOutput.LEDMatrix[x+3][y].Active = true;
                                                                                                            this.MyOutput.LEDMatrix[x + 2][y+1].Active = true;   this.MyOutput.LEDMatrix[x + 3][y+1].Active = true;
                                                             this.MyOutput.LEDMatrix[x + 1][y + 2].Active = true;                                                this.MyOutput.LEDMatrix[x + 3][y + 2].Active = true;
                     this.MyOutput.LEDMatrix[x][y + 3].Active = true;                                                                                            this.MyOutput.LEDMatrix[x + 3][y + 3].Active = true;
                     this.MyOutput.LEDMatrix[x][y + 4].Active = true; this.MyOutput.LEDMatrix[x + 1][y + 4].Active = true; this.MyOutput.LEDMatrix[x + 2][y + 4].Active = true;this.MyOutput.LEDMatrix[x + 3][y + 4].Active = true; this.MyOutput.LEDMatrix[x + 4][y + 4].Active = true;
                                                                                                                                                        this.MyOutput.LEDMatrix[x + 3][y + 5].Active = true;
                                                                                                                                                        this.MyOutput.LEDMatrix[x + 3][y + 6].Active = true;
                    break;
                case 5:
                    this.MyOutput.LEDMatrix[x][y].Active = true; this.MyOutput.LEDMatrix[x+1][y].Active = true;   this.MyOutput.LEDMatrix[x+2][y].Active = true;   this.MyOutput.LEDMatrix[x+3][y].Active = true; this.MyOutput.LEDMatrix[x+4][y].Active = true;
                    this.MyOutput.LEDMatrix[x][y+1].Active = true;                                                                         
                    this.MyOutput.LEDMatrix[x][y+2].Active = true;                                                                                                            
                    this.MyOutput.LEDMatrix[x][y+3].Active = true; this.MyOutput.LEDMatrix[x+1][y+3].Active = true;this.MyOutput.LEDMatrix[x+2][y+3].Active = true; this.MyOutput.LEDMatrix[x+3][y+3].Active = true;
                                                                                                                                                                                this.MyOutput.LEDMatrix[x+4][y+4].Active = true; 
                    this.MyOutput.LEDMatrix[x][y+5].Active = true;                                                                                                                       this.MyOutput.LEDMatrix[x+4][y+5].Active = true;
                                                        this.MyOutput.LEDMatrix[x+1][y+6].Active = true; this.MyOutput.LEDMatrix[x+2][y+6].Active = true; this.MyOutput.LEDMatrix[x+3][y+6].Active = true; 

                    break;
                case 6:
                                                                                                this.MyOutput.LEDMatrix[x+2][y].Active = true;   this.MyOutput.LEDMatrix[x+3][y].Active = true;
                                                        this.MyOutput.LEDMatrix[x+1][y+1].Active = true;                                                                         
                    this.MyOutput.LEDMatrix[x][y+2].Active = true;                                                                                                            
                    this.MyOutput.LEDMatrix[x][y+3].Active = true; this.MyOutput.LEDMatrix[x+1][y+3].Active = true; this.MyOutput.LEDMatrix[x+2][y+3].Active = true; this.MyOutput.LEDMatrix[x+3][y+3].Active = true;
                    this.MyOutput.LEDMatrix[x][y+4].Active = true;                                                                                 this.MyOutput.LEDMatrix[x+3][y+4].Active = true; 
                    this.MyOutput.LEDMatrix[x][y+5].Active = true;                                                                                 this.MyOutput.LEDMatrix[x+3][y+5].Active = true;
                                                        this.MyOutput.LEDMatrix[x+1][y+6].Active = true; this.MyOutput.LEDMatrix[x+2][y+6].Active = true; 
                    break;
                case 7:
                    this.MyOutput.LEDMatrix[x][y].Active = true; this.MyOutput.LEDMatrix[x+1][y].Active = true; this.MyOutput.LEDMatrix[x+2][y].Active = true; this.MyOutput.LEDMatrix[x+3][y].Active = true; this.MyOutput.LEDMatrix[x+4][y].Active = true;
                                                                                                                                                                          this.MyOutput.LEDMatrix[x+4][y+1].Active = true;
                                                                                                                                    this.MyOutput.LEDMatrix[x+3][y+2].Active = true;
                                                                                              this.MyOutput.LEDMatrix[x + 2][y + 3].Active = true;
                                                        this.MyOutput.LEDMatrix[x + 1][y + 4].Active = true;
                                                        this.MyOutput.LEDMatrix[x + 1][y + 5].Active = true;
                                                        this.MyOutput.LEDMatrix[x + 1][y + 6].Active = true;
                    break;
                case 8:
                                                        this.MyOutput.LEDMatrix[x + 1][y].Active = true; this.MyOutput.LEDMatrix[x + 2][y].Active = true; this.MyOutput.LEDMatrix[x + 3][y].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 1].Active = true;                                                                                                                 this.MyOutput.LEDMatrix[x + 4][y + 1].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 2].Active = true;                                                                                                                 this.MyOutput.LEDMatrix[x + 4][y + 2].Active = true;
                                                        this.MyOutput.LEDMatrix[x + 1][y+3].Active = true; this.MyOutput.LEDMatrix[x + 2][y+3].Active = true; this.MyOutput.LEDMatrix[x + 3][y+3].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 4].Active = true;                                                                                                                 this.MyOutput.LEDMatrix[x + 4][y+4].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 5].Active = true;                                                                                                                 this.MyOutput.LEDMatrix[x + 4][y+5].Active = true;
                                                        this.MyOutput.LEDMatrix[x + 1][y+6].Active = true; this.MyOutput.LEDMatrix[x + 2][y+6].Active = true; this.MyOutput.LEDMatrix[x + 3][y+6].Active = true;
                    break;
                case 9:
                                                        this.MyOutput.LEDMatrix[x + 1][y].Active = true; this.MyOutput.LEDMatrix[x + 2][y].Active = true; this.MyOutput.LEDMatrix[x + 3][y].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 1].Active = true;                                                                                                                 this.MyOutput.LEDMatrix[x + 4][y + 1].Active = true;
                    this.MyOutput.LEDMatrix[x][y + 2].Active = true;                                                                                                                 this.MyOutput.LEDMatrix[x + 4][y + 2].Active = true;
                                                        this.MyOutput.LEDMatrix[x + 1][y+3].Active = true; this.MyOutput.LEDMatrix[x + 2][y+3].Active = true; this.MyOutput.LEDMatrix[x + 3][y+3].Active = true; this.MyOutput.LEDMatrix[x + 4][y+3].Active = true;
                                                                                                                                                                         this.MyOutput.LEDMatrix[x + 4][y+4].Active = true;
                                                                                                                                     this.MyOutput.LEDMatrix[x + 3][y+5].Active = true;
                                                        this.MyOutput.LEDMatrix[x + 1][y+6].Active = true; this.MyOutput.LEDMatrix[x + 2][y+6].Active = true;
                    break;
                default:
                    throw new ArgumentException("Diese Nummer kann nicht angezeigt werden.");
            }
        }

        /// <summary>
        /// initialisizes the displaying of the seconds
        /// </summary>
        /// <param name="seconds">the number of the current seconds (00-99)</param>
        /// <param name="NightMode">if the nightmode should turned on, than say true, else false</param>
        public void showSeconds(short seconds, bool NightMode = false)
        {
            this.MyOutput.NightMode = NightMode;
            this.Value = seconds;
            this.MinutesValue = 0;
        }

        /// <summary>
        /// initialisizes the displaying of the Phrase
        /// </summary>
        /// <param name="phrase">the number of the current phrase</param>
        /// <param name="NightMode">if the nightmode should turned on, than say true, else false</param>
        public void showPhrase(string phrase, bool NightMode = false)
        {
            this.MyOutput.NightMode = NightMode;
            this.Value = phrase;
        }

        /// <summary>
        /// initialisizes the displaying of the MinuteLEDs
        /// </summary>
        /// <param name="minutes">the number of the current minutes (0-4)</param>
        /// <param name="NightMode">if the nightmode should turned on, than say true, else false</param>
        public void showMinutes(short minutes, bool NightMode = false)
        {
            this.MyOutput.NightMode = NightMode;
            this.MinutesValue = minutes;
        }

        #endregion

        #region Tools

        /// <summary>
        /// Returns an array with the digits of a number (tens, ones)
        /// </summary>
        /// <param name="Number">the number, which should be splitted</param>
        /// <returns>the array with the digits of the number</returns>
        public static int[] GetNumberArray(int Number)
        {
            //System.Collections.Generic.List<int> numbersArr = new System.Collections.Generic.List<int>();
            //System.Collections.Generic.List<int> numbersArrInverse = new System.Collections.Generic.List<int>();
            //int i = Math.Abs(Number);
            //while (i >= 10)
            //{
            //    i /= 10;
            //    numbersArr.Add(Number - i * 10);
            //}
            //numbersArr.Add((Number - i) / 10);

            //foreach (int number in numbersArr)
            //{
            //    numbersArrInverse.Insert(0, number);
            //}

            int Zehner = Number / 10;
            int Einer = Number - Zehner * 10;
            return new int[2] { Zehner, Einer };

            //return numbersArrInverse.ToArray();
        }

        /// <summary>
        /// Returns the count of an object, how often it is in an array
        /// </summary>
        /// <param name="array">the array, in which this function should count the object</param>
        /// <param name="TargetObj">the object, which count is asked for</param>
        /// <returns>the count of the TargetObject in the array</returns>
        public static int CountInArray(Object[] array, Object TargetObj)
        {
            int count = 0;
            foreach (Object obj in array)
            {
                if (Equals(obj, TargetObj))
                    count++;
            }
            return count;
        }

        #endregion Tools

    }
}