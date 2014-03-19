using System;

namespace WordClockScr.Elements
{
    public class NightModeController
    {
        public bool NightMode
        {
            get
            {
                return (System.DateTime.Now.Hour < 6 || System.DateTime.Now.Hour > 22);
            }
        }
    }
}
