using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design.Behavior;
using System.Windows.Forms.VisualStyles;

namespace MasterMind
{
    public partial class AcceptClearButtons : UserControl
    {
        public AcceptClearButtons()
        {
            InitializeComponent();
            Size = new Size(Globals.GamePegDiameter, 2 * Globals.GamePegDiameter);
        }

        public bool AcceptEnabled
        {
            get
            {
                return shhAcceptEnabled;
            }
            set
            {
                if (value != shhAcceptEnabled)
                {
                    shhAcceptEnabled = value;
                    Invalidate();
                }
            }
        }
        private bool shhAcceptEnabled = false;
        public bool ClearEnabled
        {
            get
            {
                return shhClearEnabled;
            }
            set
            {
                if (value != shhClearEnabled)
                {
                    shhClearEnabled = value;
                    Invalidate();
                }
            }
        }
        private bool shhClearEnabled = false;

        private void AcceptClearButtons_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rButton = new Rectangle(0, 0, Globals.GamePegDiameter, Globals.GamePegDiameter);
            Rectangle rGlyph = new Rectangle(rButton.Left + 3, rButton.Top + 3, rButton.Width - 6, rButton.Height - 6);

            ControlPaint.DrawButton(e.Graphics, rButton, AcceptEnabled ? ButtonState.Normal : ButtonState.Inactive);
            ControlPaint.DrawMenuGlyph(e.Graphics, rGlyph, MenuGlyph.Checkmark, AcceptEnabled ? Color.Green : SystemColors.InactiveCaption, SystemColors.ButtonFace);
            if (AcceptEnabled && ButtonUnderMouse == 1)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, rGlyph);
            }

            rButton.Offset(0, Height / 2);
            rGlyph.Offset(0, Height / 2);

            ControlPaint.DrawButton(e.Graphics, rButton, ClearEnabled ? ButtonState.Normal : ButtonState.Inactive);
            using (Pen p = new Pen(ClearEnabled ? Color.Red : SystemColors.InactiveCaption, Globals.GamePegDiameter / 8) { StartCap = System.Drawing.Drawing2D.LineCap.Square, EndCap = System.Drawing.Drawing2D.LineCap.Square })
            {
                rGlyph.Inflate(-4, -4);
                rGlyph.Offset(-1, -1);
                e.Graphics.DrawLine(p, rGlyph.Left, rGlyph.Top, rGlyph.Right, rGlyph.Bottom);
                e.Graphics.DrawLine(p, rGlyph.Left, rGlyph.Bottom, rGlyph.Right, rGlyph.Top);
            }
            if (ClearEnabled && ButtonUnderMouse == 2)
            {
                ControlPaint.DrawFocusRectangle(e.Graphics, rGlyph);
            }
        }

        public delegate void AcceptClickedEventHandler(object sender, EventArgs e);
        public event AcceptClickedEventHandler AcceptClick;
        public delegate void ClearClickedEventHandler(object sender, EventArgs e);
        public event ClearClickedEventHandler ClearClick;

        private int ButtonUnderMouse = 0;

        private void AcceptClearButtons_MouseLeave(object sender, EventArgs e)
        {
            if (ButtonUnderMouse != 0)
            {
                Rectangle r = new Rectangle(0, 0, Globals.GamePegDiameter, Globals.GamePegDiameter);
                if (ButtonUnderMouse == 2)
                {
                    r.Offset(0, Globals.GamePegDiameter);
                }
                Invalidate(r);
                ButtonUnderMouse = 0;
            }
        }

        private void AcceptClearButtons_MouseClick(object sender, MouseEventArgs e)
        {
            if (ButtonUnderMouse == 1)
            {
                AcceptClick.Invoke(this, EventArgs.Empty);
            }
            else if (ButtonUnderMouse == 2)
            {
                ClearClick.Invoke(this, EventArgs.Empty);
            }
        }

        private void AcceptClearButtons_MouseMove(object sender, MouseEventArgs e)
        {
            int WhichButton = e.Y < Globals.GamePegDiameter ? 1 : 2;
            if (ButtonUnderMouse != 0 && WhichButton != ButtonUnderMouse)
            {
                Rectangle r = new Rectangle(0, 0, Globals.GamePegDiameter, Globals.GamePegDiameter);
                if (ButtonUnderMouse == 2)
                {
                    r.Offset(0, Globals.GamePegDiameter);
                }
                Invalidate(r);
            }
            if (WhichButton != 0)
            {
                ButtonUnderMouse = WhichButton;
                Rectangle r = new Rectangle(0, 0, Globals.GamePegDiameter, Globals.GamePegDiameter);
                if (ButtonUnderMouse == 2)
                {
                    r.Offset(0, Globals.GamePegDiameter);
                }
                Invalidate(r);
            }
        }
    }
}
