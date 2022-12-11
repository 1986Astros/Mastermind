using MasterMind.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace MasterMind
{
    public partial class Peg : UserControl
    {
        public Peg():base()
        {
            InitializeComponent();
            HitArea = SetHitArea();
        }

        [DefaultValue(DisplayStyles.Colors)]
        public enum DisplayStyles
        {
            Colors,
            Numerals,
            Letters
        }
        public DisplayStyles DisplayStyle
        {
            get
            {
                return shhDisplayStyle;
            }
            set
            {
                if (value != shhDisplayStyle)
                {
                    shhDisplayStyle = value;
                    Invalidate();
                }
            }
        }
        private DisplayStyles shhDisplayStyle = DisplayStyles.Colors;

        [DefaultValue(0)]
        public int Sides
        {
            get
            {
                return shhSides;
            }
            set
            {
                if (value != shhSides)
                {
                    shhSides = value;
                    Invalidate();
                }
            }
        }
        private int shhSides = 0;

        public Color PegColor
        {
            get
            {
                return shhPegColor;
            }
            set
            {
                if (value != shhPegColor)
                {
                    shhPegColor = value;
                    Invalidate();
                }
            }
        }
        private Color shhPegColor = Color.White;

        public Color SymbolColor
        {
            get
            {
                return shhSymbolColor;
            }
            set
            {
                if (!value.Equals(shhSymbolColor))
                {
                    shhPegColor = value;
                    Invalidate();
                }
            }
        }
        private Color shhSymbolColor = Color.White;

        public int Turn = -1;
        public int Column = -1;

        private Region HitArea;

        private void Peg_Load(object sender, EventArgs e)
        {
            SetHitArea();
        }
        private Region SetHitArea()
        {
            // circle:
            int Diameter = Math.Min(Width, Height);
            RectangleF r = new RectangleF((Width - Diameter) / 2, (Height - Diameter) / 2, Diameter, Diameter);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddEllipse(r);
                HitArea = new Region(gp);
            }
            return HitArea;
        }
        private void Peg_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (DisplayStyle == DisplayStyles.Colors)
            {
                using (Brush brush = Enabled ? new SolidBrush(PegColor) : new HatchBrush(HatchStyle.Percent50, PegColor, Parent.BackColor))
                {
                    if (Sides == 0)
                    {
                        int Diameter = Math.Min(Width, Height);
                        RectangleF r = new RectangleF((Width - Diameter) / 2, (Height - Diameter) / 2, Diameter - 1, Diameter - 1);
                        e.Graphics.FillEllipse(brush, r);
                    }
                    else if (Sides == 4)
                    {

                    }
                    else
                    {
                        List<PointF> points = new List<PointF>();
                        e.Graphics.FillPolygon(brush, points.ToArray());
                    }
                }
            }
            else
            {
                // alphanumeric
            }
        }

        // https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.control.dodragdrop?view=windowsdesktop-6.0
        Point mdPoint = new Point(int.MinValue,int.MaxValue);
        private void Peg_MouseDown(object sender, MouseEventArgs e)
        {
            mdPoint = e.Location;
        }
        private void Peg_MouseUp(object sender, MouseEventArgs e)
        {
            mdPoint = new Point(int.MinValue,int.MaxValue);
        }
        private void Peg_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                if (Enabled && (e.X > mdPoint.X + SystemInformation.DragSize.Width || e.X < mdPoint.X - SystemInformation.DragSize.Width || e.Y > mdPoint.Y + SystemInformation.DragSize.Height || e.Y < mdPoint.Y - SystemInformation.DragSize.Height))
                {
                    DragDropEffects dragEffect = DoDragDrop(this, DragDropEffects.Copy | DragDropEffects.Move);
                    if (dragEffect == DragDropEffects.None)
                    {
                        PegDiscarded?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
            else
            {
                if (Enabled && HitArea.IsVisible(e.Location))
                {
                    Cursor = Cursors.Hand;
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }
        }
        public bool AllowCopy { get; set; } = true;
        public bool AllowMove { get; set; } = false;
        public bool AllowSwap { get; set; } = true;
        public delegate void PegDiscardedEventHandler(object sender, EventArgs e);
        public event PegDiscardedEventHandler PegDiscarded;

        private void Peg_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            // BUG: Currently allowing pegs from PegBoard to be dropped on Cradle which can eliminate colors in the Cradle
            if ((e.Effect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                if (AllowMove)
                {
                    Cursor.Current = Globals.PegMoveCursor;
                    e.UseDefaultCursors = false;
                }
                else if (AllowSwap)
                {
                    Cursor.Current = Globals.PegSwapCursor;
                    e.UseDefaultCursors = false;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    e.UseDefaultCursors = true;
                }
            }
            else if ((e.Effect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                if (AllowCopy)
                {
                    Cursor.Current = Globals.PegCopyCursor;
                    e.UseDefaultCursors = false;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    e.UseDefaultCursors = true;
                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                e.UseDefaultCursors = true;
            }
        }

        public delegate void PegSelectedEventHandler(object sender, PegSelectedEventArgs e);
        public event PegSelectedEventHandler PegSelected;
        public class PegSelectedEventArgs: EventArgs
        {
            public PegSelectedEventArgs(int colorIndex) : base()
            {
                this.colorIndex = colorIndex;
            }
            public int colorIndex;
        }
        private void Peg_DoubleClick(object sender, EventArgs e)
        {
            PegSelected?.Invoke(this, new PegSelectedEventArgs(Globals.ColorsInUse.IndexOf(PegColor)));
        }
        private void Peg_Enter(object sender, EventArgs e)
        {
            using (Graphics g = CreateGraphics())
            {
                ControlPaint.DrawFocusRectangle(g, new Rectangle(Point.Empty,Size));
            }
        }
        private void Peg_Leave(object sender, EventArgs e)
        {
            Invalidate();
        }
        private void Peg_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Peg)))
            {
                if  (e.Effect == DragDropEffects.Move)
                {
                    PegDroppedOnPeg?.Invoke(this, new PegDroppedOnPegEventArgs(this, (Peg)e.Data.GetData(typeof(Peg))));
                }
            }
        }

        public delegate void PegDroppedOnPegEventHandler(object sender, PegDroppedOnPegEventArgs e);
        public event PegDroppedOnPegEventHandler PegDroppedOnPeg;
        public class PegDroppedOnPegEventArgs : EventArgs
        {
            public PegDroppedOnPegEventArgs(Peg peg1, Peg peg2) : base()
            {
                this.peg1 = peg1;
                this.peg2 = peg2;
            }
            public Peg peg1;
            public Peg peg2;
        }
        private void Peg_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Peg)))
            {
                Peg peg = (Peg)e.Data.GetData(typeof(Peg));
                if (peg == this)
                {
                    e.Effect = DragDropEffects.None;
                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }
        public override string ToString()
        {
            return $"Peg[{Name}] Style={DisplayStyle.ToString()}, PegColor={PegColor.ToString()}";
        }
    }
}
