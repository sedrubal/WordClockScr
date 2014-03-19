using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using WordClockScr;
using WordClockScr.Elements;

namespace WordClockScr.Elements
{
    public class OutPut : System.Windows.Forms.Panel
    {

        #region Panel

        public OutPut()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        private Color _inactiveLEDColor;
        public Color InactiveLEDColor
        {
            get
            {
                if (this._inactiveLEDColor == null)
                    this._inactiveLEDColor = Color.FromArgb(64,64,64);
                return this._inactiveLEDColor;
            }
            set
            {
                this._inactiveLEDColor = value;
                Invalidate();
            }
        }

        private bool _nightMode = false;
        public bool NightMode 
        {
            get { return this._nightMode; }
            set
            {
                if (this._nightMode != value)
                {
                    _nightMode = value;
                    this.Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            //DateTime s = DateTime.Now;
            try
            {
                
                //LED Status setzen
                //for (int x = 0; x < LEDMatrixSubst.Length; x++ )
                //{
                //    for (int y = 0; y < LEDMatrixSubst[0].Length; y++)
                //    {
                //        LEDMatrixSubst[x][y].Active = false;
                //    }
                //}
                //if (this.Value is string)
                //{
                //    activatePhrase((string)this.Value);
                //}
                //else if (this.Value is short)
                //{
                //    activateSeconds((short) this.Value);
                //}

                //for (int i = 0; i < 4; i++)
                //{ this.MinutesLEDsSubst[i].Active = false; }
                //if (this.Value is string)
                //{ this.activateMinute(this.MinutesValue); }

                //LED-Leucht-Status zeichnen (LEDs würden schon leuchten)
                float RectWidth, RectHeight, FieldSize;
                if ((this.Width - Padding.Left - Padding.Right) *12 > (this.Height - Padding.Top - Padding.Bottom) *13)
                {
                    RectHeight = this.Height - Padding.Top - Padding.Bottom;
                    FieldSize = RectHeight / 12;
                    RectWidth = 13 * RectHeight / 12;
                }
                else
                {
                    RectWidth = this.Width - Padding.Left - Padding.Right;
                    FieldSize = RectWidth / 13;
                    RectHeight = 12 * RectWidth / 13;
                }
                float top = (this.Height - RectHeight) / 2, left = (this.Width - RectWidth) / 2, right = this.Width - left, bottom = this.Height - top;
                Color activeCol, inactiveCol;
                if (this.NightMode)
                {
                    inactiveCol = this.BackColor;
                    activeCol = Color.FromArgb(120, this.ForeColor);
                }
                else
                {
                    activeCol = this.ForeColor;
                    inactiveCol  = this.InactiveLEDColor;
                }
                Font Lettersfont = new Font(this.Font.FontFamily, FieldSize / 4);

                //Raster zeichnen:
                //for (int x = 0; x <= 13; x++)
                //{
                //    e.Graphics.DrawLine(new Pen(inactiveSB), left + x * FieldSize, top, left + x * FieldSize, bottom);
                //}
                //for (int y = 0; y <= 12; y++)
                //{
                //    e.Graphics.DrawLine(new Pen(inactiveSB), left, top + y * FieldSize, right, top + y * FieldSize);
                //}
                SolidBrush wcolSB;

                for (int x = 0; x < LEDMatrixSubst.Length; x++)
                {
                    for (int y = 0; y < LEDMatrixSubst[0].Length; y++)
                    {
                        string Letter = ((LetterLED)LEDMatrixSubst[x][y]).Value.ToString();
                        Size textSize = TextRenderer.MeasureText(Letter, Lettersfont);
                        wcolSB = new SolidBrush(Tools.GetWeightedColor(inactiveCol, activeCol, this.LEDMatrixSubst[x][y].LumenPercent));
                        
                        e.Graphics.DrawString(Letter, Lettersfont, wcolSB, left + (x + 1.5f) * FieldSize - textSize.Width / 2, top + (y + 1.5f) * FieldSize - textSize.Height / 2);
                    }
                }
                float widthHeight = FieldSize / 6;
                float t = top + (FieldSize - widthHeight) / 2, l = left + (FieldSize - widthHeight) / 2, b = bottom - FieldSize + (FieldSize - widthHeight) / 2, r = right - FieldSize + (FieldSize - widthHeight) / 2;

                wcolSB = new SolidBrush(Tools.GetWeightedColor(inactiveCol, activeCol, this.MinutesLEDsSubst[0].LumenPercent));
                e.Graphics.FillEllipse(wcolSB, l, t, widthHeight, widthHeight);

                wcolSB = new SolidBrush(Tools.GetWeightedColor(inactiveCol, activeCol, this.MinutesLEDsSubst[1].LumenPercent));
                e.Graphics.FillEllipse(wcolSB, r, t, widthHeight, widthHeight);

                wcolSB = new SolidBrush(Tools.GetWeightedColor(inactiveCol, activeCol, this.MinutesLEDsSubst[2].LumenPercent));
                e.Graphics.FillEllipse(wcolSB, r, b, widthHeight, widthHeight);

                wcolSB = new SolidBrush(Tools.GetWeightedColor(inactiveCol, activeCol, this.MinutesLEDsSubst[3].LumenPercent));
                e.Graphics.FillEllipse(wcolSB, l, b, widthHeight, widthHeight);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(this, ex.Message, "Fehler beim Zeichnen", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //TimeSpan rendertime = DateTime.Now - s;
            //e.Graphics.DrawString(rendertime.TotalMilliseconds + " ms", this.Font, new SolidBrush(Color.Red), 0, 0);
        }

        protected override void OnPaddingChanged(EventArgs e)
        {
            base.OnPaddingChanged(e);
            this.Invalidate();
        }

        private void initializeMatrix()
        {
            this.LEDMatrixSubst[0][0] = new LetterLED('E', 0, 0);
            this.LEDMatrixSubst[1][0] = new LetterLED('S', 1, 0);
            this.LEDMatrixSubst[2][0] = new LetterLED('K', 2, 0);
            this.LEDMatrixSubst[3][0] = new LetterLED('I', 3, 0);
            this.LEDMatrixSubst[4][0] = new LetterLED('S', 4, 0);
            this.LEDMatrixSubst[5][0] = new LetterLED('T', 5, 0);
            this.LEDMatrixSubst[6][0] = new LetterLED('L', 6, 0);
            this.LEDMatrixSubst[7][0] = new LetterLED('F', 7, 0);
            this.LEDMatrixSubst[8][0] = new LetterLED('Ü', 8, 0);
            this.LEDMatrixSubst[9][0] = new LetterLED('N', 9, 0);
            this.LEDMatrixSubst[10][0] = new LetterLED('F', 10, 0);

            this.LEDMatrixSubst[0][1] = new LetterLED('Z', 0, 1);
            this.LEDMatrixSubst[1][1] = new LetterLED('E', 1, 1);
            this.LEDMatrixSubst[2][1] = new LetterLED('H', 2, 1);
            this.LEDMatrixSubst[3][1] = new LetterLED('N', 3, 1);
            this.LEDMatrixSubst[4][1] = new LetterLED('Z', 4, 1);
            this.LEDMatrixSubst[5][1] = new LetterLED('W', 5, 1);
            this.LEDMatrixSubst[6][1] = new LetterLED('A', 6, 1);
            this.LEDMatrixSubst[7][1] = new LetterLED('N', 7, 1);
            this.LEDMatrixSubst[8][1] = new LetterLED('Z', 8, 1);
            this.LEDMatrixSubst[9][1] = new LetterLED('I', 9, 1);
            this.LEDMatrixSubst[10][1] = new LetterLED('G', 10, 1);

            this.LEDMatrixSubst[0][2] = new LetterLED('D', 0, 2);
            this.LEDMatrixSubst[1][2] = new LetterLED('R', 1, 2);
            this.LEDMatrixSubst[2][2] = new LetterLED('E', 2, 2);
            this.LEDMatrixSubst[3][2] = new LetterLED('I', 3, 2);
            this.LEDMatrixSubst[4][2] = new LetterLED('V', 4, 2);
            this.LEDMatrixSubst[5][2] = new LetterLED('I', 5, 2);
            this.LEDMatrixSubst[6][2] = new LetterLED('E', 6, 2);
            this.LEDMatrixSubst[7][2] = new LetterLED('R', 7, 2);
            this.LEDMatrixSubst[8][2] = new LetterLED('T', 8, 2);
            this.LEDMatrixSubst[9][2] = new LetterLED('E', 9, 2);
            this.LEDMatrixSubst[10][2] = new LetterLED('L', 10, 2);

            this.LEDMatrixSubst[0][3] = new LetterLED('V', 0, 3);
            this.LEDMatrixSubst[1][3] = new LetterLED('O', 1, 3);
            this.LEDMatrixSubst[2][3] = new LetterLED('R', 2, 3);
            this.LEDMatrixSubst[3][3] = new LetterLED('F', 3, 3);
            this.LEDMatrixSubst[4][3] = new LetterLED('U', 4, 3);
            this.LEDMatrixSubst[5][3] = new LetterLED('N', 5, 3);
            this.LEDMatrixSubst[6][3] = new LetterLED('K', 6, 3);
            this.LEDMatrixSubst[7][3] = new LetterLED('N', 7, 3);
            this.LEDMatrixSubst[8][3] = new LetterLED('A', 8, 3);
            this.LEDMatrixSubst[9][3] = new LetterLED('C', 9, 3);
            this.LEDMatrixSubst[10][3] = new LetterLED('H', 10, 3);

            this.LEDMatrixSubst[0][4] = new LetterLED('H', 0, 4);
            this.LEDMatrixSubst[1][4] = new LetterLED('A', 1, 4);
            this.LEDMatrixSubst[2][4] = new LetterLED('L', 2, 4);
            this.LEDMatrixSubst[3][4] = new LetterLED('B', 3, 4);
            this.LEDMatrixSubst[4][4] = new LetterLED('A', 4, 4);
            this.LEDMatrixSubst[5][4] = new LetterLED('E', 5, 4);
            this.LEDMatrixSubst[6][4] = new LetterLED('L', 6, 4);
            this.LEDMatrixSubst[7][4] = new LetterLED('F', 7, 4);
            this.LEDMatrixSubst[8][4] = new LetterLED('Ü', 8, 4);
            this.LEDMatrixSubst[9][4] = new LetterLED('N', 9, 4);
            this.LEDMatrixSubst[10][4] = new LetterLED('F', 10, 4);

            this.LEDMatrixSubst[0][5] = new LetterLED('E', 0, 5);
            this.LEDMatrixSubst[1][5] = new LetterLED('I', 1, 5);
            this.LEDMatrixSubst[2][5] = new LetterLED('N', 2, 5);
            this.LEDMatrixSubst[3][5] = new LetterLED('S', 3, 5);
            this.LEDMatrixSubst[4][5] = new LetterLED('X', 4, 5);
            this.LEDMatrixSubst[5][5] = new LetterLED('Ä', 5, 5);
            this.LEDMatrixSubst[6][5] = new LetterLED('M', 6, 5);
            this.LEDMatrixSubst[7][5] = new LetterLED('Z', 7, 5);
            this.LEDMatrixSubst[8][5] = new LetterLED('W', 8, 5);
            this.LEDMatrixSubst[9][5] = new LetterLED('E', 9, 5);
            this.LEDMatrixSubst[10][5] = new LetterLED('I', 10, 5);

            this.LEDMatrixSubst[0][6] = new LetterLED('D', 0, 6);
            this.LEDMatrixSubst[1][6] = new LetterLED('R', 1, 6);
            this.LEDMatrixSubst[2][6] = new LetterLED('E', 2, 6);
            this.LEDMatrixSubst[3][6] = new LetterLED('I', 3, 6);
            this.LEDMatrixSubst[4][6] = new LetterLED('A', 4, 6);
            this.LEDMatrixSubst[5][6] = new LetterLED('U', 5, 6);
            this.LEDMatrixSubst[6][6] = new LetterLED('J', 6, 6);
            this.LEDMatrixSubst[7][6] = new LetterLED('V', 7, 6);
            this.LEDMatrixSubst[8][6] = new LetterLED('I', 8, 6);
            this.LEDMatrixSubst[9][6] = new LetterLED('E', 9, 6);
            this.LEDMatrixSubst[10][6] = new LetterLED('R', 10, 6);

            this.LEDMatrixSubst[0][7] = new LetterLED('S', 0, 7);
            this.LEDMatrixSubst[1][7] = new LetterLED('E', 1, 7);
            this.LEDMatrixSubst[2][7] = new LetterLED('C', 2, 7);
            this.LEDMatrixSubst[3][7] = new LetterLED('H', 3, 7);
            this.LEDMatrixSubst[4][7] = new LetterLED('S', 4, 7);
            this.LEDMatrixSubst[5][7] = new LetterLED('N', 5, 7);
            this.LEDMatrixSubst[6][7] = new LetterLED('L', 6, 7);
            this.LEDMatrixSubst[7][7] = new LetterLED('A', 7, 7);
            this.LEDMatrixSubst[8][7] = new LetterLED('C', 8, 7);
            this.LEDMatrixSubst[9][7] = new LetterLED('H', 9, 7);
            this.LEDMatrixSubst[10][7] = new LetterLED('T', 10, 7);

            this.LEDMatrixSubst[0][8] = new LetterLED('S', 0, 8);
            this.LEDMatrixSubst[1][8] = new LetterLED('I', 1, 8);
            this.LEDMatrixSubst[2][8] = new LetterLED('E', 2, 8);
            this.LEDMatrixSubst[3][8] = new LetterLED('B', 3, 8);
            this.LEDMatrixSubst[4][8] = new LetterLED('E', 4, 8);
            this.LEDMatrixSubst[5][8] = new LetterLED('N', 5, 8);
            this.LEDMatrixSubst[6][8] = new LetterLED('Z', 6, 8);
            this.LEDMatrixSubst[7][8] = new LetterLED('W', 7, 8);
            this.LEDMatrixSubst[8][8] = new LetterLED('Ö', 8, 8);
            this.LEDMatrixSubst[9][8] = new LetterLED('L', 9, 8);
            this.LEDMatrixSubst[10][8] = new LetterLED('F', 10, 8);

            this.LEDMatrixSubst[0][9] = new LetterLED('Z', 0, 9);
            this.LEDMatrixSubst[1][9] = new LetterLED('E', 1, 9);
            this.LEDMatrixSubst[2][9] = new LetterLED('H', 2, 9);
            this.LEDMatrixSubst[3][9] = new LetterLED('N', 3, 9);
            this.LEDMatrixSubst[4][9] = new LetterLED('E', 4, 9);
            this.LEDMatrixSubst[5][9] = new LetterLED('U', 5, 9);
            this.LEDMatrixSubst[6][9] = new LetterLED('N', 6, 9);
            this.LEDMatrixSubst[7][9] = new LetterLED('K', 7, 9);
            this.LEDMatrixSubst[8][9] = new LetterLED('U', 8, 9);
            this.LEDMatrixSubst[9][9] = new LetterLED('H', 9, 9);
            this.LEDMatrixSubst[10][9] = new LetterLED('R', 10, 9);

            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 10; y++)
                {
                    this.LEDMatrixSubst[x][y].AnyDrawingValueChangedEvent += this.OnLEDStateValueChanged;
                }
            }
        }

        private void initializeMinutesLEDs()
        {
            this.MinutesLEDsSubst[0] = new LED(0, -1); this.MinutesLEDsSubst[0].AnyDrawingValueChangedEvent += this.OnLEDStateValueChanged;
            this.MinutesLEDsSubst[1] = new LED(1, -1); this.MinutesLEDsSubst[1].AnyDrawingValueChangedEvent += this.OnLEDStateValueChanged;
            this.MinutesLEDsSubst[2] = new LED(2, -1); this.MinutesLEDsSubst[2].AnyDrawingValueChangedEvent += this.OnLEDStateValueChanged;
            this.MinutesLEDsSubst[3] = new LED(3, -1); this.MinutesLEDsSubst[3].AnyDrawingValueChangedEvent += this.OnLEDStateValueChanged;
        }

        #region Event

        public void OnLEDStateValueChanged(LED sender, LED.AnyDrawingValueChanged e)
        {
            //Rectangle invalideRect;
            //int RectWidth, RectHeight, FieldSize;
            //if ((this.Width - Padding.Left - Padding.Right) * 12 > (this.Height - Padding.Top - Padding.Bottom) * 13)
            //{
            //    RectHeight = this.Height - Padding.Top - Padding.Bottom;
            //    FieldSize = RectHeight / 12;
            //    RectWidth = 13 * RectHeight / 12;
            //}
            //else
            //{
            //    RectWidth = this.Width - Padding.Left - Padding.Right;
            //    FieldSize = RectWidth / 13;
            //    RectHeight = 12 * RectWidth / 13;
            //}
            //int top = (this.Height - RectHeight) / 2, left = (this.Width - RectWidth) / 2, right = this.Width - left, bottom = this.Height - top;
            ////int widthHeight = FieldSize / 6;
            ////int t = top + (FieldSize - widthHeight) / 2, l = left + (FieldSize - widthHeight) / 2, b = bottom - FieldSize + (FieldSize - widthHeight) / 2, r = right - FieldSize + (FieldSize - widthHeight) / 2;


            //if (e.yLocation == -1)
            //{
            //    switch (e.xLocation)
            //    {
            //        case 0:
            //            invalideRect = new Rectangle(left, top, FieldSize, FieldSize);
            //            break;
            //        case 1:
            //            invalideRect = new Rectangle(right, top, FieldSize, FieldSize);
            //            break;
            //        case 2:
            //            invalideRect = new Rectangle(right, bottom, FieldSize, FieldSize);
            //            break;
            //        case 3:
            //            invalideRect = new Rectangle(left, bottom, FieldSize, FieldSize);
            //            break;
            //        default:
            //            throw new ArgumentException("Ungültige Position einer Minuten-LED");
            //    }
            //}
            //else
            //{
            //    invalideRect = new Rectangle(left + (e.xLocation + 1) * FieldSize, top + (e.yLocation) * FieldSize, FieldSize, FieldSize);
            //}

            //this.Invalidate(invalideRect);
            //TODO: Nur die betroffene LED malen!!!!!
            this.Invalidate();
        }

        #endregion Event

        #endregion Panel

        #region OutPut

        private LetterLED[][] _lEDMatrix;
        public LetterLED[][] LEDMatrixSubst
        {
            get
            {
                if (this._lEDMatrix == null)
                {
                    _lEDMatrix = new LetterLED[11][];
                    for (short i = 0; i < 11; i++ )
                    {
                        this._lEDMatrix[i] = new LetterLED[10];
                    }
                    this.initializeMatrix();
                }
                return this._lEDMatrix;
            }
            //set
            //{
            //    if (this._lEDMatrix != value) this._lEDMatrix = (Elements.LetterLED[][])value; this.Invalidate();
            //}
        }
        public LED[][] LEDMatrix
        {
            get { return this.LEDMatrixSubst; }
        }

        private LED[] _minutesLEDs;
        public LED[] MinutesLEDsSubst
        {
            get
            {
                if (this._minutesLEDs == null)
                {
                    _minutesLEDs = new LED[4];
                    this.initializeMinutesLEDs();
                }
                return this._minutesLEDs;
            }
            //set
            //{
            //    if (this._minutesLEDs != value) this._minutesLEDs = (LED[])value; this.Invalidate();
            //}
        }
        public LED[] MinutesLEDs
        {
            get { return this.MinutesLEDsSubst; }
        }

        #endregion Output

    }
}