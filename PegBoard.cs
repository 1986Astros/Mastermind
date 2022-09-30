using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMind
{
    public partial class PegBoard : UserControl
    {
        public PegBoard()
        {
            InitializeComponent();
        }

        public int MaxTurns = 8;
        public int PegsPerRow = 4;
        public int GamePegDiameter = 20;
        public int ResultsPegDiameter = 8;

        private void PegBoard_Load(object sender, EventArgs e)
        {
            // move these to any resizing event handlers
            MeasureEverything();
            ResizeForAutoSize();
            Invalidate();
        }

        #region "Control sizing"
        private Size RowSize;
        private Size PegRowSize;
        private Size ResultsRowSize;
        private Size shhPreferredSize;

        private void MeasureEverything()
        {
            PegRowSize = new Size(2 * PegsPerRow * GamePegDiameter, 2 * GamePegDiameter);
            ResultsRowSize = new Size(2 * (int)Math.Ceiling((float)PegsPerRow / 2f) * ResultsPegDiameter, 2 * ResultsPegDiameter);
            int RowHeight = Math.Max(2 * GamePegDiameter, 4 * ResultsPegDiameter);
            RowSize = new Size(PegRowSize.Width + ResultsRowSize.Width, RowHeight);

            shhPreferredSize = new Size(RowSize.Width, (MaxTurns + 1) * RowHeight + 2 * (MaxTurns - 1));

            #region "collapse"
            //    using (Graphics g = CreateGraphics())
            //    {
            //        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //        HeaderHeight = HeaderFont.Height;
            //        LineScoreHeight = LinescoreFont.Height;
            //        AnnotationHeight = AnnotationFont.Height;
            //        BonusHeight = BonusFont.Height;
            //        ScoreFontHeight = ScoreFont.Height;

            //        LinescoreColumnWidth = g.MeasureString("BONUSMM", BonusFont).Width + g.MeasureString("If total score", AnnotationFont).Width;
            //        HowToScoreColumnWidth = g.MeasureString("HOW TO SCORE", HeaderFont).Width;
            //        GameColumnWidth = g.MeasureString("99999", ScoreFont).Width;

            //        MaxWidthOfLeftColumnText = g.MeasureString("SM Straight", LinescoreFont).Width;
            //    }
            //    float AutosizeWidth = LinescoreColumnWidth + HowToScoreColumnWidth + (TripleYahtzee ? 3 : 1) * GameColumnWidth;
            //    float AutosizeHeight = 7 * HeaderHeight + 13 * LineScoreHeight + BonusHeight + PaddingConst;

            //    if (YahtzeeBonuses)
            //    {
            //        AutosizeHeight += 2 * BonusHeight;
            //    }
            //    else if (TripleYahtzee)
            //    {

            //        AutosizeHeight += 3 * HeaderHeight;
            //    }
            //    shhPreferredSize = new Size((int)Math.Ceiling(AutosizeWidth) + 1, (int)Math.Ceiling(AutosizeHeight) + 1);

            //    // prepare for painting
            //    RectangleF r;
            //    AllHitAreas.Clear();
            //    UpperLines.Clear();
            //    UpperLines.Add(HeaderHeight);
            //    r = new RectangleF(LinescoreColumnWidth + HowToScoreColumnWidth, HeaderHeight, GameColumnWidth, LineScoreHeight);
            //    r.Inflate(-2, -2);
            //    for (int i = 0; i < 6; i++)
            //    {
            //        AllHitAreas.Add(new LayoutArea(LayoutArea.Areas.UpperSectionScoring, r, 0, i));
            //        UpperLines.Add(UpperLines.Last() + LineScoreHeight);
            //        r.Offset(0, LineScoreHeight);
            //    }
            //    UpperLines.Add(UpperLines.Last() + HeaderHeight);
            //    AllHitAreas.Add(new LayoutArea(LayoutArea.Areas.UpperSectionTotalWithoutBonus, r, 0));
            //    r.Offset(0, HeaderHeight);
            //    UpperLines.Add(UpperLines.Last() + BonusHeight);
            //    AllHitAreas.Add(new LayoutArea(LayoutArea.Areas.UpperSectionBonus, r, 0));
            //    r.Offset(0, BonusHeight);
            //    UpperLines.Add(UpperLines.Last() + HeaderHeight);
            //    AllHitAreas.Add(new LayoutArea(LayoutArea.Areas.UpperSectionBonusTotal, r, 0));
            //    r.Offset(0, 2 * HeaderHeight + PaddingConst);

            //    LowerLines.Clear();
            //    LowerLines.Add(UpperLines.Last() + PaddingConst);
            //    LowerLines.Add(LowerLines.Last() + HeaderHeight);
            //    for (int i = 0; i < 7; i++)
            //    {
            //        AllHitAreas.Add(new LayoutArea(LayoutArea.Areas.LowerSectionScoring, r, 0, i));
            //        LowerLines.Add(LowerLines.Last() + LineScoreHeight);
            //        r.Offset(0, LineScoreHeight);
            //    }
            //    if (YahtzeeBonuses)
            //    {
            //        r.Offset(0, BonusHeight - LineScoreHeight);
            //        AllHitAreas.Add(new LayoutArea(LayoutArea.Areas.YahtzeeBonuses, r, 0));
            //        LowerLines.Add(LowerLines.Last() + BonusHeight);
            //        r.Offset(0, BonusHeight);
            //        AllHitAreas.Add(new LayoutArea(LayoutArea.Areas.YahtzeeBonusScore, r, 0));
            //        LowerLines.Add(LowerLines.Last() + BonusHeight);
            //        r.Offset(0, BonusHeight);
            //    }
            //    LayoutArea.Areas[] LowerTotalAreas = new LayoutArea.Areas[] { LayoutArea.Areas.LowerSectionTotal, LayoutArea.Areas.LowerSectionUpperTotal, LayoutArea.Areas.LowerSectionGrandTotal };
            //    for (int i = 0; i < 3; i++)
            //    {
            //        AllHitAreas.Add(new LayoutArea(LowerTotalAreas[i], r, 0));
            //        LowerLines.Add(LowerLines.Last() + HeaderHeight);
            //        r.Offset(0, HeaderHeight);
            //    }
            //    if (TripleYahtzee)
            //    {
            //        LowerLines.Add(LowerLines.Last() + HeaderHeight);
            //        LowerLines.Add(LowerLines.Last() + HeaderHeight);
            //        LowerLines.Add(LowerLines.Last() + HeaderHeight);
            //    }
            //    // add the hitpoints in other columns
            //    List<LayoutArea> OriginalList = new List<LayoutArea>(AllHitAreas);
            //    foreach (LayoutArea la in OriginalList)
            //    {
            //        r = la.BoundingRectangle;
            //        if (TripleYahtzee)
            //        {
            //            r.Offset(GameColumnWidth, 0);
            //            AllHitAreas.Add(la.Clone(r, 1));
            //            r.Offset(GameColumnWidth, 0);
            //            AllHitAreas.Add(la.Clone(r, 2));
            //        }
            //    }
            //    // add the hitpoints for tourney total
            //    if (TripleYahtzee)
            //    {
            //        r = new RectangleF(LinescoreColumnWidth + HowToScoreColumnWidth, ((IEnumerable<float>)LowerLines).Reverse().Skip(2).First(), AutosizeWidth - (LinescoreColumnWidth + HowToScoreColumnWidth), HeaderHeight);
            //        for (int i = 0; i < 3; i++)
            //        {
            //            RectangleF MultiplierRectangle = new RectangleF(r.Left + i * (r.Width / 3), r.Top, r.Width / 3, r.Height);
            //            MultiplierRectangle.Inflate(-2, -2);
            //            AllHitAreas.Add(new LayoutArea(LayoutArea.Areas.TripleYahtzeeGrandTotal, MultiplierRectangle, i));
            //        }
            //        r = new RectangleF(LinescoreColumnWidth + HowToScoreColumnWidth, ((IEnumerable<float>)LowerLines).Reverse().Skip(1).First(), AutosizeWidth - (LinescoreColumnWidth + HowToScoreColumnWidth), HeaderHeight);
            //        r.Inflate(-2, -2);
            //        AllHitAreas.Add(new LayoutArea(LayoutArea.Areas.TournamentTotal, r, 0));
            //    }
            #endregion
        }

        // https://stackoverflow.com/questions/9857041/how-can-i-implement-an-autosize-property-to-a-custom-control
        /// <summary>
        /// Method that forces the control to resize itself when in AutoSize following
        /// a change in its state that affect the size.
        /// </summary>
        private void ResizeForAutoSize()
        {
            if (this.AutoSize)
            {
                this.SetBoundsCore(this.Left, this.Top, this.Width, this.Height, BoundsSpecified.Size);
            }
        }

        /// <summary>
        /// Retrieves the size of a rectangular area into which
        /// a control can be fitted.
        /// </summary>
        public override Size GetPreferredSize(Size proposedSize)
        {
            return shhPreferredSize;
        }

        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        protected override void SetBoundsCore(int x, int y, int width, int height,
                BoundsSpecified specified)
        {
            //  Only when the size is affected...
            if (this.AutoSize && (specified & BoundsSpecified.Size) != 0)
            {
                Size size = shhPreferredSize;

                width = size.Width;
                height = size.Height;
            }

            base.SetBoundsCore(x, y, width, height, specified);
        }
        #endregion

        private void PegBoard_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int PegLeft = Globals.RightHanded ? ResultsRowSize.Width : 0;
            int ResultsLeft = Globals.RightHanded ? 0 : PegRowSize.Width;
            int RowsOfResultsPegs = PegsPerRow / 2 + PegsPerRow % 2;
            int y = Globals.BottomToTop ? Height - RowSize.Height : RowSize.Height;

            Rectangle PlayingPieceRect = new Rectangle(PegLeft + GamePegDiameter / 2, y + GamePegDiameter / 2, GamePegDiameter, GamePegDiameter);
            Rectangle ResultsPieceRect = new Rectangle(ResultsLeft + ResultsPegDiameter / 2,y + ResultsPegDiameter / 2,  ResultsPegDiameter, ResultsPegDiameter);
            if (PlayingPieceRect.Height > RowsOfResultsPegs * ResultsPieceRect.Height)
            {
                ResultsPieceRect.Offset(0, (PlayingPieceRect.Height - RowsOfResultsPegs * ResultsPieceRect.Height) / 2);
            }
            else if (PlayingPieceRect.Height < RowsOfResultsPegs * ResultsPieceRect.Height)
            {
                PlayingPieceRect.Offset(0, (RowsOfResultsPegs * ResultsPieceRect.Height - PlayingPieceRect.Height) / 2);
            }

            Rectangle r, rHole;
            for (int row = 0; row < MaxTurns; row++)
            {
                // draw the playing pegs
                r = PlayingPieceRect;
                for (int peg  = 0; peg < PegsPerRow; peg++)
                {
                    if (row < Globals.CurrentGame.Turns.Count() && Globals.CurrentGame.Turns.ElementAt(row).Guesses.ElementAt(peg) >= 0)
                    {
                        using (Brush brush = new SolidBrush(Globals.ColorsInUse.ElementAt(Globals.CurrentGame.Turns.ElementAt(row).Guesses.ElementAt(peg))))
                        {
                            e.Graphics.FillEllipse(brush, r);
                        }
                    }
                    else
                    {
                        // peg hole
                        rHole = r;
                        rHole.Inflate(GamePegDiameter / -3, GamePegDiameter / -3);
                        using (Pen p = new Pen(Color.Black, 2))
                        {
                            e.Graphics.DrawEllipse(p, rHole);
                        }
                    }
                    r.Offset(2 * GamePegDiameter, 0);
                }

                // draw the results pegs
                r = ResultsPieceRect;
                for (int peg = 0; peg < PegsPerRow; peg++)
                {
                    if (row < Globals.CurrentGame.Turns.Count() && Globals.CurrentGame.Turns.ElementAt(row).Completed && peg < Globals.CurrentGame.Turns.ElementAt(row).CorrectlyPlaced + Globals.CurrentGame.Turns.ElementAt(row).IncorrectlyPlaced)
                    {
                        Color c;
                        if (Globals.CurrentGame.Turns.ElementAt(row).CorrectlyPlaced > peg)
                        {
                            c = Color.Red;
                        }
                        else
                        {
                            c = Color.White;
                        }
                        using (Brush brush = new SolidBrush(c))
                        {
                            e.Graphics.FillEllipse(brush, r);
                        }
                    }
                    else
                    {
                        rHole = r;
                        rHole.Inflate(ResultsPegDiameter / -3, ResultsPegDiameter / -3);
                        e.Graphics.DrawEllipse(Pens.Black, rHole);
                    }
                    if (peg % 2 == 0)
                    {
                        r.Offset(2 * ResultsPegDiameter, 0);
                    }
                    else
                    {
                        r.Offset(-2 * ResultsPegDiameter, 2 * ResultsPegDiameter);
                    }
                }

                // draw the lines between rows
                y = Globals.BottomToTop ? PlayingPieceRect.Top - GamePegDiameter / 2 - 1 : PlayingPieceRect.Bottom;
                e.Graphics.DrawLine(Pens.Black, 0, y,  RowSize.Width, y);
                y++;
                e.Graphics.DrawLine(Pens.White, 0, y,  RowSize.Width, y);

                PlayingPieceRect.Offset(0, Globals.BottomToTop ? - RowSize.Height - 2 : RowSize.Height  + 2);
                ResultsPieceRect.Offset(0, Globals.BottomToTop ? - RowSize.Height - 2 : RowSize.Height  + 2);
            }
            if ((Globals.CheatMode || Globals.CurrentGame.Solved) && !DesignMode )
            {
                r = PlayingPieceRect;
                for (int peg = 0; peg < PegsPerRow; peg++)
                {
                    using (Brush brush = new SolidBrush(Globals.ColorsInUse.ElementAt(Globals.CurrentGame.Pattern[peg])))
                    {
                        e.Graphics.FillEllipse(brush, r);
                    }
                    r.Offset(2 * GamePegDiameter, 0);
                }

            }
        }

        private void PegBoard_MouseLeave(object sender, EventArgs e)
        {

        }

        private void PegBoard_MouseMove(object sender, MouseEventArgs e)
        {

        }

        public void AttachCradle(Cradle cradle)
        {
            cradle.PegSelected += Cradle_PegSelected;
        }

        private void Cradle_PegSelected(object sender, Cradle.PegSelectedEventArgs e)
        {
            CurrentGame game = Globals.CurrentGame;
            if (game.Turns.Count() == 0)
            {
                game.PlacePeg(e.Index, 0);
                Invalidate();
            }
            else
            {
                CurrentGame.Guess guess = game.Turns.Last();
                if (guess.Completed)
                {
                    game.PlacePeg(e.Index, 0);
                    Invalidate();
                }
                else
                {
                    // temp code to place peg in next available hole
                    for (int i = 0; i < Globals.PegsPerRow; i++)
                    {
                        if (guess.Guesses[i] == -1)
                        {
                            game.PlacePeg(e.Index, i);
                            Invalidate();
                            break;
                        }
                    }
                    if (guess.Guesses.All(g => g > -1))
                    {
                        if (game.EvaluateTurn())
                        {
                            Globals.CheatMode = true;
                            Invalidate();
                            Application.DoEvents();
                            MessageBox.Show($"You won in {game.Turns.Count()} turns.");
                        }
                        else if(game.Turns.Count() == Globals.MaxTurns)
                        {
                            Globals.CheatMode = true;
                            Invalidate();
                            Application.DoEvents();
                            MessageBox.Show("You ran out of turns.");
                        }
                    }
                }
            }
        }
    }
}
