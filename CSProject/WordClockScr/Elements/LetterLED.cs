namespace WordClockScr.Elements
{
    public class LetterLED : LED
    {
      #region Substitution

         public LetterLED(char value, int xLoc, int yLoc) : base(xLoc, yLoc)
         {
             this.Value = value;
         }

         private char _value;
         public char Value
         {
             get { return this._value; }
             set { this._value = value; }
         }

      #endregion Substitution
    }
}
