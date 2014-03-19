namespace WordClockScr
{
    /// <summary>
    /// A class, that contains Informations about the time of the current day. It's a bit similar to the System.TimeSpan-Class
    /// </summary>
    public class TimeOfDay
    {
        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="Hours">the value of the elapsed hours of this day as short</param>
        /// <param name="Minutes">the value of the elapsed minutes of this hour as short</param>
        /// <param name="Seconds">the value of the elapsed seconds of this minute as short</param>
        public TimeOfDay(short Hours, short Minutes, short Seconds)
        {
            this.Hours = Hours;
            this.Minutes = Minutes;
            this.Seconds = Seconds;
        }

        private short _hours;
        /// <summary>
        /// The Hours-componente of the time of the current day
        /// </summary>
        public short Hours 
        {
            get { return this._hours; }
            set
            {
                if (value >= 0 && value <= 24)
                {
                    if (this._hours != value)
                    { this._hours = value; }
                }
                else
                { throw new System.ArgumentException("Ungültige Zahl für Stunden!"); }
            }
        }

        private short _minutes;
        /// <summary>
        /// The Minutes-componente of the time of the current day
        /// </summary>
        public short Minutes 
        {
            get { return this._minutes; }
            set
            {
                if (value >= 0 && value <= 60)
                {
                    if (this._minutes != value)
                    { this._minutes = value; }
                }
                else
                { throw new System.ArgumentException("Ungültige Zahl für Minuten!"); }
            }
        }

        private short _seconds;
        /// <summary>
        /// The Seconds-componente of the time of the current day
        /// </summary>
        public short Seconds 
        {
            get { return this._seconds; }
            set
            {
                if (value >= 0 && value <= 60)
                {
                    if (this._seconds != value)
                    { this._seconds = value; }
                }
                else
                { throw new System.ArgumentException("Ungültige Zahl für Sekunden!"); }
            }
        }
    }
}