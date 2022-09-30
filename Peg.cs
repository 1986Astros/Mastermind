using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                using (Brush brush = new SolidBrush(PegColor))
                {
                    if (Sides == 0)
                    {
                        int Diameter = Math.Min(Width, Height);
                        RectangleF r = new RectangleF((Width - Diameter) / 2, (Height - Diameter) / 2, Diameter-1, Diameter-1);
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

            }
        }

        private void Peg_MouseMove(object sender, MouseEventArgs e)
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

        private void Peg_MouseLeave(object sender, EventArgs e)
        {

        }

        public delegate void PegSelectedEventHandler(object sender, PegSelectedEventArgs e);
        public event PegSelectedEventHandler PegSelected;
        public class PegSelectedEventArgs: EventArgs
        {
            public PegSelectedEventArgs(int Index) : base()
            {
                this.Index = Index;
            }
            public int Index;
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
    }
}
