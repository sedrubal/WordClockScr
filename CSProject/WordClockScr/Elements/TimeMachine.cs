using System;

namespace WordClockScr.Elements
{
    public class TimeMachine
    {
        public TimeOfDay GetTimeOfDay()
        {
            return new TimeOfDay((short)DateTime.Now.TimeOfDay.Hours, (short )DateTime.Now.TimeOfDay.Minutes, (short) DateTime.Now.TimeOfDay.Seconds);
        }
    }
}
