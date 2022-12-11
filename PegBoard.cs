using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection.Metadata;
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
            Size sz = new Size(Globals.GamePegDiameter, Globals.GamePegDiameter);
            Padding mg = new Padding(Globals.GamePegDiameter / 2);
            Peg peg;
            for (int turn = 0; turn < Globals.MaxTurns; turn++)
            {
                for (int  col = 0; col < Globals.PegsPerRow; col++)
                {
                    peg = new Peg() { Name = $"{Name}: {turn},{col}", Size = sz, Margin = mg, Turn = turn, Column = col, Visible = false , AllowCopy = true, AllowMove = false, AllowSwap = false};
                    peg.PegDiscarded += Peg_Discarded;
                    Controls.Add(peg);
                }
            }
        }

        [Browsable(false)]
        public CurrentGame CurrentGame
        {
            get
            {
                if (shhCurrentGame is null)
                {
                    return Globals.CurrentGame;
                }
                else
                {
                    return shhCurrentGame;
                }
            }
            set
            {
                shhCurrentGame = value;
                if (value != null)
                {
                    for (int turn = 0; turn < value.Turns.Count; turn++)
                    {
                        for (int column = 0; column < Globals.PegsPerRow; column++)
                        {
                            Peg peg = Controls.OfType<Peg>().First(p => p.Turn == turn && p.Column == column);
                            peg.PegColor = Globals.ColorsInUse[value.Turns[turn].Guesses[column]];
                            peg.Visible = true;
                        }
                    }
                }
                Invalidate();
            }
        }
        private CurrentGame shhCurrentGame = null;

        [Browsable(false)]
        public bool ShowSolution {
            get
            {
                return shhShowSolution;
            }
            set{
                if (value != shhShowSolution)
                {
                    shhShowSolution = value;
                    Invalidate();
                }
            }
            }
        private bool shhShowSolution = false;

        private void PegBoard_Load(object sender, EventArgs e)
        {
            Recompose();
        }

        private void Recompose()
        {
            MeasureEverything();
            ResizeForAutoSize();
            if (basicBoardBitmapEnabled is not null)
            {
                basicBoardBitmapEnabled.Dispose();
            }
            basicBoardBitmapEnabled = CreateBasicBitmap(true, false);
            if (basicBoardBitmapDisabled is not null)
            {
                basicBoardBitmapDisabled.Dispose();
            }
            basicBoardBitmapDisabled = CreateBasicBitmap(false, false);
            Invalidate();
        }

        public void InitializeGame()
        {
            SuspendLayout();
            ArrangePegs();
            foreach (Peg peg in Controls.OfType<Peg>())
            {
                peg.Visible = false;
                peg.AllowCopy = true;
                peg.AllowMove = true;
                peg.AllowSwap = true;
            }
            ShowSolution = false;
            ResumeLayout();
        }

        #region "Control sizing"
        private Size RowSize;
        private Size PegRowSize;
        private Size ResultsRowSize;
        private Size shhPreferredSize;

        private void MeasureEverything()
        {
            PegRowSize = new Size(2 * Globals.PegsPerRow * Globals.GamePegDiameter, 2 * Globals.GamePegDiameter);
            ResultsRowSize = new Size(2 * (int)Math.Ceiling((float)Globals.PegsPerRow / 2f) * Globals.ResultsPegDiameter, 2 * Globals.ResultsPegDiameter);
            int RowHeight = Math.Max(2 * Globals.GamePegDiameter, 4 * Globals.ResultsPegDiameter);
            RowSize = new Size(PegRowSize.Width + ResultsRowSize.Width, RowHeight);

            shhPreferredSize = new Size(RowSize.Width, (Globals.MaxTurns + 1) * RowHeight + 2 * Globals.MaxTurns );
            switch (BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    shhPreferredSize += (new Size(0, 2 * SystemInformation.BorderSize.Height));
                    break;
                case BorderStyle.Fixed3D:
                    shhPreferredSize += (new Size(0, 2 * SystemInformation.Border3DSize.Height));
                                        break;
            }
        }
       
        private class PegPosition
        {
            public PegPosition(int turn, int column, Rectangle bounds) : base()
            {
                Turn = turn;
                Column = column;
                Bounds = bounds;
            }
            public int Turn;
            public int Column;
            public Rectangle Bounds;
        }
        private IList<PegPosition> ArrangedPegsBounds(bool righthanded, bool bottomToTop)
        {
            Point p;
            List<PegPosition> PegsBounds = new List<PegPosition>();
            Size sz = new Size(Globals.GamePegDiameter, Globals.GamePegDiameter);
            if (bottomToTop)
            {
                if (righthanded)
                {
                    Point pegLocation = new Point(ResultsRowSize.Width + Globals.GamePegDiameter / 2, shhPreferredSize.Height - Globals.GamePegDiameter - Globals.GamePegDiameter / 2);
                    switch (BorderStyle)
                    {
                        case BorderStyle.FixedSingle:
                            pegLocation -= (new Size(0, 2 * SystemInformation.BorderSize.Height));
                            break;
                        case BorderStyle.Fixed3D:
                            pegLocation -= (new Size(0, 2 * SystemInformation.Border3DSize.Height));
                            break;
                    }
                    for (int turn = 0; turn < Globals.MaxTurns; turn++)
                    {
                        p = pegLocation;
                        for (int col = 0; col < Globals.PegsPerRow; col++)
                        {
                            PegsBounds.Add(new PegPosition(turn, col, new Rectangle(p, sz)));
                            p.Offset(2 * Globals.GamePegDiameter, 0);
                        }
                        pegLocation.Offset(0, -2 * Globals.GamePegDiameter - 2);
                    }
                }
                else
                {
                    Point pegLocation = new Point(Globals.GamePegDiameter / 2, shhPreferredSize.Height - Globals.GamePegDiameter - Globals.GamePegDiameter / 2);
                    switch (BorderStyle)
                    {
                        case BorderStyle.FixedSingle:
                            pegLocation -= (new Size(0, 2 * SystemInformation.BorderSize.Height));
                            break;
                        case BorderStyle.Fixed3D:
                            pegLocation -= (new Size(0, 2 * SystemInformation.Border3DSize.Height));
                            break;
                    }
                    for (int turn = 0; turn < Globals.MaxTurns; turn++)
                    {
                        p = pegLocation;
                        for (int col = 0; col < Globals.PegsPerRow; col++)
                        {
                            PegsBounds.Add(new PegPosition(turn, col, new Rectangle(p, sz)));
                            p.Offset(2 * Globals.GamePegDiameter, 0);
                        }
                        pegLocation.Offset(0, -2 * Globals.GamePegDiameter - 2);
                    }
                }
            }
            else
            {
                if (Globals.RightHanded)
                {
                    Point pegLocation = new Point(Globals.GamePegDiameter / 2, Globals.GamePegDiameter / 2 + 2);
                    for (int turn = 0; turn < Globals.MaxTurns; turn++)
                    {
                        p = pegLocation;
                        for (int col = 0; col < Globals.PegsPerRow; col++)
                        {
                            PegsBounds.Add(new PegPosition(turn, col, new Rectangle(p, sz)));
                            p.Offset(2 * Globals.GamePegDiameter, 0);
                        }
                        pegLocation.Offset(0, 2 * Globals.GamePegDiameter + 2);
                    }
                }
                else
                {
                    Point pegLocation = new Point(ResultsRowSize.Width + Globals.GamePegDiameter / 2, Globals.GamePegDiameter / 2 + 2);
                    for (int turn = 0; turn < Globals.MaxTurns; turn++)
                    {
                        p = pegLocation;
                        for (int col = 0; col < Globals.PegsPerRow; col++)
                        {
                            PegsBounds.Add(new PegPosition(turn, col, new Rectangle(p, sz)));
                            p.Offset(2 * Globals.GamePegDiameter, 0);
                        }
                        pegLocation.Offset(0, 2 * Globals.GamePegDiameter + 2);
                    }
                }
            }

            return PegsBounds;
        }
        public void ArrangePegs()
        {
            Point p;
            Peg peg;
            if (Globals.BottomToTop)
            {
                if (Globals.RightHanded)
                {
                    Point pegLocation = new Point(ResultsRowSize.Width + Globals.GamePegDiameter / 2, shhPreferredSize.Height - Globals.GamePegDiameter - Globals.GamePegDiameter / 2);
                    switch (BorderStyle)
                    {
                        case BorderStyle.FixedSingle:
                            pegLocation -= (new Size(0, 2 * SystemInformation.BorderSize.Height));
                            break;
                        case BorderStyle.Fixed3D:
                            pegLocation -= (new Size(0, 2 * SystemInformation.Border3DSize.Height));
                            break;
                    }
                    for (int turn = 0; turn < Globals.MaxTurns; turn++)
                    {
                        p = pegLocation;
                        for (int col = 0; col < Globals.PegsPerRow; col++)
                        {
                            peg = Controls.OfType<Peg>().First(c => c.Turn == turn && c.Column == col);
                            peg.Location = p;
                            p.Offset(2 * Globals.GamePegDiameter, 0);
                        }
                        pegLocation.Offset(0, -2 * Globals.GamePegDiameter - 2);
                    }
                }
                else
                {
                    Point pegLocation = new Point(Globals.GamePegDiameter / 2, shhPreferredSize.Height - Globals.GamePegDiameter - Globals.GamePegDiameter / 2);
                    switch (BorderStyle)
                    {
                        case BorderStyle.FixedSingle:
                            pegLocation -= (new Size(0, 2 * SystemInformation.BorderSize.Height));
                            break;
                        case BorderStyle.Fixed3D:
                            pegLocation -= (new Size(0, 2 * SystemInformation.Border3DSize.Height));
                            break;
                    }
                    for (int turn = 0; turn < Globals.MaxTurns; turn++)
                    {
                        p = pegLocation;
                        for (int col = 0; col < Globals.PegsPerRow; col++)
                        {
                            peg = Controls.OfType<Peg>().First(c => c.Turn == turn && c.Column == col);
                            peg.Location = p;
                            p.Offset(2 * Globals.GamePegDiameter, 0);
                        }
                        pegLocation.Offset(0, -2 * Globals.GamePegDiameter - 2);
                    }
                }
            }
            else
            {
                if (Globals.RightHanded)
                {
                    Point pegLocation = new Point( Globals.GamePegDiameter / 2, Globals.GamePegDiameter / 2 + 2);
                    for (int turn = 0; turn < Globals.MaxTurns; turn++)
                    {
                        p = pegLocation;
                        for (int col = 0; col < Globals.PegsPerRow; col++)
                        {
                            peg = Controls.OfType<Peg>().First(c => c.Turn == turn && c.Column == (3 - col));
                            peg.Location = p;
                            p.Offset(2 * Globals.GamePegDiameter, 0);
                        }
                        pegLocation.Offset(0, 2 * Globals.GamePegDiameter + 2);
                    }
                }
                else
                {
                    Point pegLocation = new Point(ResultsRowSize.Width + Globals.GamePegDiameter / 2, Globals.GamePegDiameter / 2 + 2);
                    for (int turn = 0; turn < Globals.MaxTurns; turn++)
                    {
                        p = pegLocation;
                        for (int col = 0; col < Globals.PegsPerRow; col++)
                        {
                            peg = Controls.OfType<Peg>().First(c => c.Turn == turn && c.Column == (3-col));
                            peg.Location = p;
                            p.Offset(2 * Globals.GamePegDiameter, 0);
                        }
                        pegLocation.Offset(0, 2 * Globals.GamePegDiameter + 2);
                    }
                }
            }
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

        private Bitmap basicBoardBitmapDisabled = null;
        private Bitmap basicBoardBitmapEnabled = null;

        public void ChangedOrientation()
        {
            ArrangePegs();
            SetPositionOfAcceptClearButtons();
            Invalidate();
        }

        private void PegBoard_Paint(object sender, PaintEventArgs e)
        {
            Bitmap boardBitmap;
            if (Enabled)
            {
                if (ShowSolution)
                {
                    boardBitmap = CreateBasicBitmap(true, true);
                }
                else
                {
                    boardBitmap = (Bitmap)basicBoardBitmapEnabled.Clone();
                }
            }
            else
            {
                if (ShowSolution)
                {
                    boardBitmap = CreateBasicBitmap(false, true);
                }
                else
                {
                    boardBitmap = (Bitmap)basicBoardBitmapDisabled.Clone();
                }
            }

            using (Bitmap bm = (Bitmap)boardBitmap.Clone())
            {
                if (Globals.TopToBottom)
                {
                    bm.RotateFlip(RotateFlipType.Rotate180FlipNone);
                }

                if (Globals.LeftHanded)
                {
                    bm.RotateFlip(RotateFlipType.RotateNoneFlipX);
                }

                e.Graphics.DrawImage(bm, new Rectangle(0, 0, boardBitmap.Width, boardBitmap.Height));
            }

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int PegLeft;
            int ResultsLeft;
            int RowsOfResultsPegs = Globals.PegsPerRow / 2 + Globals.PegsPerRow % 2;
            int y = Globals.BottomToTop ? Height - RowSize.Height + 1 : 0;
            if (Globals.BottomToTop)
            {
                PegLeft = Globals.RightHanded ? ResultsRowSize.Width : 0;
                ResultsLeft = Globals.RightHanded ? 0 : PegRowSize.Width;

            }
            else
            {
                PegLeft = Globals.RightHanded ? 0 : ResultsRowSize.Width;
                ResultsLeft = Globals.RightHanded ? PegRowSize.Width : 0;
            }

            Rectangle ResultsPieceRect = new Rectangle(ResultsLeft + Globals.ResultsPegDiameter / 2 + (Globals.RightHanded ? 1 : -1), y + Globals.ResultsPegDiameter / 2, Globals.ResultsPegDiameter, Globals.ResultsPegDiameter);
            if (Globals.BottomToTop)
            {
                y = Height - RowSize.Height;
                switch (BorderStyle)
                {
                    case BorderStyle.FixedSingle:
                        y += SystemInformation.BorderSize.Height;
                        break;
                    case BorderStyle.Fixed3D:
                        y += SystemInformation.Border3DSize.Height;
                        break;
                }
                ResultsPieceRect = new Rectangle(ResultsLeft + Globals.ResultsPegDiameter / 2 + (Globals.RightHanded ? 0 : -1), y + Globals.ResultsPegDiameter / 2, Globals.ResultsPegDiameter, Globals.ResultsPegDiameter);
            }
            else
            {
                switch (BorderStyle)
                {
                    case BorderStyle.None:
                        y = 0;
                        break;
                    case BorderStyle.FixedSingle:
                        y = SystemInformation.BorderSize.Height + 5;
                        break;
                    case BorderStyle.Fixed3D:
                        y = SystemInformation.Border3DSize.Height + 2;
                        break;
                }
                ResultsPieceRect = new Rectangle(ResultsLeft + Globals.ResultsPegDiameter / 2 + (Globals.RightHanded ? -1 : 0), y + Globals.ResultsPegDiameter / 2, Globals.ResultsPegDiameter, Globals.ResultsPegDiameter);
            }

            Rectangle r;
            for (int row = 0; row < Globals.MaxTurns; row++)
            {
                // draw the results pegs
                r = ResultsPieceRect;
                for (int peg = 0; peg < Globals.PegsPerRow; peg++)
                {
                    if (row < CurrentGame.Turns.Count() && CurrentGame.Turns.ElementAt(row).Completed && peg < CurrentGame.Turns.ElementAt(row).CorrectlyPlaced + CurrentGame.Turns.ElementAt(row).IncorrectlyPlaced)
                    {
                        Color c;
                        if (CurrentGame.Turns.ElementAt(row).CorrectlyPlaced > peg)
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
                    if (peg % 2 == 0)
                    {
                        r.Offset(2 * Globals.ResultsPegDiameter, 0);
                    }
                    else
                    {
                        r.Offset(-2 * Globals.ResultsPegDiameter, 2 * Globals.ResultsPegDiameter);
                    }
                }
                ResultsPieceRect.Offset(0, Globals.BottomToTop ? -RowSize.Height - 2 : RowSize.Height + 2);
            }

            // todo: this shouldn't be done in the paint event handler, but it's working
            SetPositionOfAcceptClearButtons();
        }
        private Bitmap CreateBasicBitmap(bool enabled, bool showWinner)
        {
            IList<PegPosition> PegSlots = ArrangedPegsBounds(true, true); // Globals.RightHanded, Globals.BottomToTop);
            Bitmap boardBitmap = new Bitmap(Width, Height);
            using (Graphics g = Graphics.FromImage(boardBitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                using (Brush brush = new SolidBrush(BackColor))
                {
                    g.FillRectangle(brush, 0, 0, Width, Height);
                }

                int RowsOfResultsPegs = Globals.PegsPerRow / 2 + Globals.PegsPerRow % 2;
                int y = Height - RowSize.Height - 2;
                switch (BorderStyle)
                {
                    case BorderStyle.FixedSingle:
                        y -= SystemInformation.BorderSize.Height;
                        break;
                    case BorderStyle.Fixed3D:
                        y -= SystemInformation.Border3DSize.Height;
                        break;
                }

                Rectangle PlayingPieceRect = new Rectangle(ResultsRowSize.Width + Globals.GamePegDiameter / 2, y + Globals.GamePegDiameter / 2, Globals.GamePegDiameter, Globals.GamePegDiameter);
                Rectangle ResultsPieceRect = new Rectangle(Globals.ResultsPegDiameter / 2, y + Globals.ResultsPegDiameter / 2 + 3, Globals.ResultsPegDiameter, Globals.ResultsPegDiameter);
                if (PlayingPieceRect.Height > RowsOfResultsPegs * ResultsPieceRect.Height)
                {
                    ResultsPieceRect.Offset(0, (PlayingPieceRect.Height - RowsOfResultsPegs * ResultsPieceRect.Height) / 2);
                }
                else if (PlayingPieceRect.Height < RowsOfResultsPegs * ResultsPieceRect.Height)
                {
                    PlayingPieceRect.Offset(0, (RowsOfResultsPegs * ResultsPieceRect.Height - PlayingPieceRect.Height) / 2);
                }

                Rectangle r, rHole;
                for (int turn = 0; turn < Globals.MaxTurns; turn++)
                {
                    // draw the playing pegs
                    r = PlayingPieceRect;
                    for (int column = 0; column < Globals.PegsPerRow; column++)
                    {
                        // peg hole
                        RectangleF rPegHole = PegSlots.First(ps => ps.Turn == turn && ps.Column == column).Bounds;
                        rPegHole.Inflate((float)Globals.GamePegDiameter / -3f, (float)Globals.GamePegDiameter / -3f);
                        using (Pen p = new Pen(enabled ? Color.Black : Color.Gray, 2))
                        {
                            g.DrawEllipse(p, rPegHole);
                        }
                        r.Offset(2 * Globals.GamePegDiameter, 0);
                    }

                    // draw the results pegs
                    r = ResultsPieceRect;
                    for (int peg = 0; peg < Globals.PegsPerRow; peg++)
                    {
                        rHole = r;
                        rHole.Inflate(Globals.ResultsPegDiameter / -3, Globals.ResultsPegDiameter / -3);
                        g.DrawEllipse(enabled ? Pens.Black : Pens.Gray, rHole);
                        if (peg % 2 == 0)
                        {
                            r.Offset(2 * Globals.ResultsPegDiameter, 0);
                        }
                        else
                        {
                            r.Offset(-2 * Globals.ResultsPegDiameter, 2 * Globals.ResultsPegDiameter - 1);
                        }
                    }

                    // draw the lines between rows
                    y = PlayingPieceRect.Top - Globals.GamePegDiameter / 2 - 1;
                    g.DrawLine(enabled ? Pens.Black : Pens.Gray, 0, y, RowSize.Width, y);
                    y++;
                    g.DrawLine(enabled ? Pens.White : Pens.LightGray, 0, y, RowSize.Width, y);

                    PlayingPieceRect.Offset(0, -RowSize.Height - 2);
                    ResultsPieceRect.Offset(0, -RowSize.Height - 2);
                }

                if (showWinner)
                {
                    r = PlayingPieceRect;
                    for (int pegRight = 0, pegLeft = Globals.PegsPerRow - 1; pegRight < Globals.PegsPerRow; pegRight++, pegLeft--)
                    {
                        using (Brush brush = new SolidBrush(Globals.ColorsInUse.ElementAt(CurrentGame.Pattern[Globals.RightHanded ? pegRight : pegLeft])))
                        {
                            g.FillEllipse(brush, r);
                        }
                        r.Offset(2 * Globals.GamePegDiameter, 0);
                    }
                }
            }

            return boardBitmap;
        }
        public void SetPositionOfAcceptClearButtons()
        {
            if (acceptClearButtons is not null)
            {
                if (Globals.BottomToTop)
                {
                    acceptClearButtons.Margin = new Padding(3, Height - (CurrentGame.CurrentTurnIndex() + 1) * (RowSize.Height + 2) + 5, 3, 0);
                }
                else
                {
                    acceptClearButtons.Margin = new Padding(3, CurrentGame.CurrentTurnIndex() * (RowSize.Height + 2) + 5, 3, 0);
                }
            }
        }

        public void AttachCradle(Cradle cradle)
        {
            cradle.PegSelected += Cradle_PegSelected;
        }
        public void AttachAcceptClearButtons(AcceptClearButtons acb)
        {
            acb.AcceptClick += AcceptAndClearButtons_Accepted;
            acb.ClearClick += AcceptAndClearButtons_Cleared;
            acceptClearButtons = acb;
        }
        private AcceptClearButtons? acceptClearButtons;

        public class GameOverEventArgs : EventArgs
        {
            public GameOverEventArgs(bool won, int turns) : base()
            {
                Won = won;
                Turns = turns;
            }
            public bool Won;
            public int Turns;
        }
        public delegate void GameOverEventHandler(object sender, GameOverEventArgs e);
        public event GameOverEventHandler? GameOver;

        private void Cradle_PegSelected(object sender, Cradle.PegSelectedEventArgs e)
        {
            CurrentGame game = CurrentGame;
            if (game.Turns.Count() == 0)
            {
                PlacePeg(e.colorIndex, 0);
                acceptClearButtons.ClearEnabled = true;
                Invalidate();
            }
            else
            {
                CurrentGame.Guess guess = game.Turns.Last();
                if (guess.Completed)
                {
                    PlacePeg(e.colorIndex, 0);
                    acceptClearButtons.ClearEnabled = true;
                    Invalidate();
                }
                else
                {
                    for (int i = 0; i < Globals.PegsPerRow; i++)
                    {
                        if (guess.Guesses[i] == -1)
                        {
                            PlacePeg(e.colorIndex, i);
                            Invalidate();
                            break;
                        }
                    }
                    if (guess.Guesses.All(g => g > -1))
                    {
                        acceptClearButtons.AcceptEnabled = true;
                        acceptClearButtons.ClearEnabled = true;
                    }
                    else
                    {
                        acceptClearButtons.AcceptEnabled = false;
                        acceptClearButtons.ClearEnabled = true;
                    }
                }
            }
        }
        private void AcceptAndClearButtons_Accepted(object sender, EventArgs e)
        {
            CurrentGame game = CurrentGame;
            game.Turns.Last().Completed = true;
            if (game.EvaluateTurn())
            {
                SetPositionOfAcceptClearButtons();
                Invalidate();
                Application.DoEvents();
                GameOver?.Invoke(this, new GameOverEventArgs(true, CurrentGame.Turns.Count()));
            }
            else if (game.Turns.Count() == Globals.MaxTurns)
            {
                Invalidate();
                Application.DoEvents();
                GameOver?.Invoke(this, new GameOverEventArgs(false, CurrentGame.Turns.Count()));
            }
            else
            {
                foreach (Peg peg in Controls.OfType<Peg>().Where(p => p.Turn == (game.CurrentTurnIndex() - 1)))
                {
                    peg.AllowMove = false;
                    peg.AllowSwap = false;
                }
                SetPositionOfAcceptClearButtons();
                Invalidate();       // todo: need to invalidate only the scoring pegs for this row
            }
            acceptClearButtons.AcceptEnabled = false;
            acceptClearButtons.ClearEnabled = false;
        }

        private void AcceptAndClearButtons_Cleared(object sender, EventArgs e)
        {
            acceptClearButtons.AcceptEnabled = false;
            acceptClearButtons.ClearEnabled = false;
            CurrentGame.ClearPegs(); 
            for (int i = 0; i < Globals.PegsPerRow; i++)
            {
                Controls.OfType<Peg>().First(p => p.Turn == CurrentGame.Turns.Count - 1 && p.Column == i).Visible = false;
            }
            Invalidate();
        }
        private void PegBoard_DragDrop(object sender, DragEventArgs e)
        {
            Peg? peg = (e.Data?.GetData(typeof(Peg))) as Peg;
            Point p = PointToClient(new Point(e.X, e.Y));
            int color = Globals.ColorsInUse.IndexOf(peg.PegColor);
            int column = XtoColumn(p.X);

            if (peg.Parent == this)
            {
                if (CurrentGame.Turns.ElementAt(peg.Turn).Completed || ((e.KeyState & 8) == 8))
                {
                    // * copy to the next turn or if Ctrl is pressed, same turn
                    PlacePeg(color, column);
                }
                else if (CurrentGame.Turns.Last().Guesses[column] >= 0)
                {
                    // * swap two pegs on the same row
                    SwapPegs(peg.Column, column);
                }
                else
                {
                    // * move a peg from one hole to the other
                    peg.Visible = false;
                    CurrentGame.RemovePeg(peg.Column);
                    PlacePeg(color, column);
                }
            }
            else
            {
                PlacePeg(color, column);
            }
            if (!LastDragDropTarget.Equals(Rectangle.Empty))
            {
                // erase the old rectangle
                Rectangle rInner = LastDragDropTarget;
                Region rgn = new Region(LastDragDropTarget);
                rInner.Inflate(-1, -1);
                rgn.Xor(rInner);
                Invalidate(rgn);
                LastDragDropTarget = Rectangle.Empty;
            }
            acceptClearButtons.AcceptEnabled = CurrentGame.Turns.Last().Guesses.All(g => g >= 0);
            acceptClearButtons.ClearEnabled = true;
            Invalidate();   // won't be needed after switching from drawing to placing Peg controls in this.Controls
        }
        private void Peg_Discarded(object sender, EventArgs e)
        {
            Peg peg = (Peg)sender;
            if (peg.Turn != CurrentGame.CurrentTurnIndex())
            {
                return;
            }
            peg.Visible = false;
            CurrentGame.RemovePeg(peg.Column);
            acceptClearButtons.AcceptEnabled = false;
            acceptClearButtons.ClearEnabled = CurrentGame.Turns.Last().Guesses.Any(g => g >= 0);
        }

        private void PlacePeg(int color, int column)
        {
            CurrentGame.PlacePeg(color, column);
            Peg peg = Controls.OfType<Peg>().First(p => p.Turn == CurrentGame.Turns.Count - 1 && p.Column == column);
            peg.PegColor = Globals.ColorsInUse[color];
            peg.Visible = true;
        }
        private void SwapPegs(int Column1, int Column2)
        {
            Color c1 = Globals.ColorsInUse[CurrentGame.Turns.Last().Guesses[Column1]];
            Color c2 = Globals.ColorsInUse[CurrentGame.Turns.Last().Guesses[Column2]];
            Controls.OfType<Peg>().First(p => p.Turn == CurrentGame.CurrentTurnIndex() && p.Column == Column1).PegColor = c2;
            Controls.OfType<Peg>().First(p => p.Turn == CurrentGame.CurrentTurnIndex() && p.Column == Column2).PegColor = c1;
            CurrentGame.PlacePeg(Globals.ColorsInUse.IndexOf(c2), Column1);
            CurrentGame.PlacePeg(Globals.ColorsInUse.IndexOf(c1), Column2);
        }

        Rectangle LastDragDropTarget = Rectangle.Empty;
        private void PegBoard_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data != null && (e.Data.GetDataPresent(typeof(Peg))))
            {
                Point p = PointToClient(new Point(e.X, e.Y));
                int column = XtoColumn(p.X);

                Peg peg = (Peg)e.Data.GetData(typeof(Peg));
                if (((e.KeyState & 8) == 8) || peg.Turn < CurrentGame.CurrentTurnIndex())
                {
                    e.Effect = DragDropEffects.Copy;
                    peg.AllowCopy = true;
                    peg.AllowMove = false;
                    peg.AllowSwap = false;
                }
                else
                {
                    // same row
                    if (peg.Column == column)
                    {
                        // same peg
                        peg.AllowCopy = false;
                        peg.AllowMove = true;
                        peg.AllowSwap = false;
                    }
                    else if (Controls.OfType<Peg>().First(p => p.Turn == peg.Turn && p.Column == column).Visible)
                    {
                        // allow swap
                        peg.AllowCopy = false;
                        peg.AllowMove = false;
                        peg.AllowSwap = true;
                    }
                    else
                    {
                        // new column same turn
                        peg.AllowCopy = false;
                        peg.AllowMove = true;
                        peg.AllowSwap = false;
                    }
                        e.Effect = DragDropEffects.Move;
                }

                Rectangle rNewDragDropTarget  = Controls.OfType<Peg>().First(p => p.Turn == CurrentGame.CurrentTurnIndex() && p.Column == column).Bounds;

                Region rgn;
                if (!rNewDragDropTarget.Equals(LastDragDropTarget))
                {
                    if (!LastDragDropTarget.Equals(Rectangle.Empty))
                    {
                        // erase the old rectangle
                        Rectangle rInner = LastDragDropTarget;
                        rgn = new Region(LastDragDropTarget);
                        rInner.Inflate(-1, -1);
                        rgn.Xor(rInner);
                        Invalidate(rgn);
                    }
                    // draw the new rectangle
                    LastDragDropTarget = rNewDragDropTarget;
                    rgn = new Region(LastDragDropTarget);
                    rNewDragDropTarget.Inflate(-1, -1);
                    rgn.Xor(rNewDragDropTarget);
                    using (Brush brush = new HatchBrush(HatchStyle.Percent50, Color.Black, BackColor))
                    {
                        using (Graphics g = CreateGraphics())
                        {
                            g.FillRegion(brush, rgn);
                        }
                    }
                    Update();
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private int XtoColumn(int X)
        {
            int column;

            if (Globals.BottomToTop)
            {
                if (Globals.RightHanded)
                {
                    int PegLeft = ResultsRowSize.Width;

                    if (X < PegLeft)
                    {
                        column = 0;
                    }
                    else
                    {
                        column = Math.Min(Globals.PegsPerRow - 1, (X - PegLeft) / (2 * Globals.GamePegDiameter));
                    }
                }
                else
                {
                    column = Math.Min(Globals.PegsPerRow - 1, X  / (2 * Globals.GamePegDiameter));
                }
            }
            else
            {
                int PegLeft = Globals.RightHanded ? 0 : ResultsRowSize.Width;
                if (X < PegLeft)
                {
                    column = 3;
                }
                else
                {
                    column = Globals.PegsPerRow - 1 - Math.Min(Globals.PegsPerRow - 1, (X - PegLeft) / (2 * Globals.GamePegDiameter));
                }
            }

            return column;
        }
        private void PegBoard_DragLeave(object sender, EventArgs e)
        {
            if (!LastDragDropTarget.Equals(Rectangle.Empty))
            {
                Rectangle r = LastDragDropTarget;
                Region rgn = new Region(LastDragDropTarget);
                r.Inflate(-1, -1);
                rgn.Xor(r);
                Invalidate(rgn);
                Update();
                LastDragDropTarget = Rectangle.Empty;
            }
            // todo: (probably not possible in .Net, only with ole32.dll entry points) if the peg came from the pegboard, change the cursor to a trash can for Delete (remove peg from board)
        }
    }
}
