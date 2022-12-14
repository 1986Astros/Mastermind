using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMind
{
    public partial class Cradle : UserControl
    {
        public Cradle()
        {
            InitializeComponent();
            InitializeControls();
        }
        private void InitializeControls()
        {
            Controls.Clear();
            TableLayoutPanel tlp = new TableLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill,
                Margin = new Padding(3),
            };
            Peg peg;
            List<Peg> Pegs = new List<Peg>();
            for (int i = 0; i < shhPegCount; i++)
            {
                if (IsSource)
                {
                    peg = new Peg() { Name = $"Cradle[{Name}][{Globals.ColorsInUse[i]}],IsSource=true", PegColor = Globals.ColorsInUse[i], Margin = new Padding(Globals.GamePegDiameter / 2), Size = new Size(Globals.GamePegDiameter, Globals.GamePegDiameter), AllowCopy = false, AllowDrop = true, AllowMove = false, AllowSwap = true };
                }
                else
                {
                    peg = new Peg() { Name = $"Cradle[{Name}][{Globals.ColorsInUse[i]}],IsSource=false", Margin = new Padding(Globals.GamePegDiameter / 2), Size = new Size(Globals.GamePegDiameter, Globals.GamePegDiameter), AllowCopy = true, AllowDrop = true, AllowMove = true, AllowSwap = true };
                }
                peg.PegSelected += Peg_Selected;
                peg.PegDroppedOnPeg += Peg_DroppedOnPeg;
                Pegs.Add(peg);
            }
            if (Orientation == Orientations.Vertical)
            {
                tlp.RowCount = shhPegCount;
                tlp.ColumnCount = 1;
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                for (int i = 0; i < shhPegCount; i++)
                {
                    tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                }
            }
            else
            {
                tlp.RowCount = 1;
                tlp.ColumnCount = shhPegCount;
                tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                for (int i = 0; i < shhPegCount; i++)
                {
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                }
            }
            tlp.Controls.AddRange(Pegs.ToArray());
            Controls.Add(tlp);
        }
        public enum Orientations
        {
            Horizontal,
            Vertical
        }
        [DefaultValue(Orientations.Vertical)]
        public Orientations Orientation
        {
            get
            {
                return shhOrientation;
            }
            set
            {
                if (value != shhOrientation)
                {
                    shhOrientation = value;
                    TableLayoutPanel tlp =  this.Controls.OfType<TableLayoutPanel>().First();
                    tlp.SuspendLayout();
                    tlp.ColumnStyles.Clear();
                    tlp.RowStyles.Clear();
                    if (shhOrientation == Orientations.Vertical)
                    {
                        tlp.ColumnCount = 1;
                        tlp.RowCount = Globals.ColorsInUse.Count();
                        tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                        for (int i = 0; i < tlp.RowCount; i++)
                        {
                            tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        }
                    }
                    else
                    {
                        tlp.ColumnCount = Globals.ColorsInUse.Count();
                        tlp.RowCount = 1;
                        tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        for (int i = 0; i < tlp.ColumnCount; i++)
                        {
                            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                        }
                    }
                    tlp.ResumeLayout(false);
                    tlp.PerformLayout();
                }
            }
        }
        private Orientations shhOrientation = Orientations.Vertical;
        public enum PegDirections
        {
            LeftToRight,
            RightToLeft
        }
        public PegDirections PegDirection
        {
            get
            {
                return shhPegDirection;
            }
            set
            {
                if (value != shhPegDirection)
                {
                    TableLayoutPanel tlp = this.Controls.OfType<TableLayoutPanel>().First();
                    Peg[] pegs = new Peg[6];
                    tlp.SuspendLayout();
                    if (Orientation == Orientations.Vertical)
                    {
                        if (tlp.RowCount < 6)
                        {
                            tlp.Invalidate();
                        }
                        for (int row = Globals.ColorsInUse.Count - 1; row >= 0; row--)
                        {
                            pegs[row] = (Peg)(tlp.GetControlFromPosition(0, row));
                            if (pegs[row] == null)
                            {
                                throw new Exception();
                            }
                            tlp.Controls.Remove(pegs[row]);
                        }
                        for (int newRow = 0, oldRow = Globals.ColorsInUse.Count - 1; oldRow >= 0; newRow++, oldRow--)
                        {
                            tlp.Controls.Add(pegs[oldRow], 0, newRow);
                        }
                    }
                    else
                    {
                        if (tlp.ColumnCount < 6)
                        {
                            tlp.Invalidate();
                        }
                        for (int col = Globals.ColorsInUse.Count - 1; col >= 0; col--)
                        {
                            pegs[col] = (Peg)(tlp.GetControlFromPosition(col, 0));
                            if (pegs[col] == null)
                            {
                                throw new Exception();
                            }
                            tlp.Controls.Remove(pegs[col]);
                        }
                        for (int newCol = 0, oldCol = Globals.ColorsInUse.Count - 1; oldCol >= 0; newCol++, oldCol--)
                        {
                            tlp.Controls.Add(pegs[oldCol], newCol, 0);
                        }
                    }
                    shhPegDirection = value;
                    tlp.ResumeLayout(false);
                    tlp.PerformLayout();
                }
            }
        }
        private PegDirections shhPegDirection = PegDirections.LeftToRight;
        public int PegCount
        {
            get
            {
                return shhPegCount;
            }
            set
            {
                if (value != shhPegCount)
                {
                    shhPegCount = value;
                    InitializeControls();
                }
            }
        }
        private int shhPegCount = Globals.ColorsInUse.Count;
        public bool IsSource { get; set; } = true;
        public void SetColors(IEnumerable<int> colors)
        {
            TableLayoutPanel tlp = this.Controls.OfType<TableLayoutPanel>().First();
            IEnumerator<int> enumColors = colors.GetEnumerator();
            foreach (Peg peg in tlp.Controls)
            {
                enumColors.MoveNext();
                peg.PegColor = Globals.ColorsInUse[enumColors.Current];
            }
        }
        public IEnumerable<int> GetColors()
        {
            List<int> colors = new List<int>();
            TableLayoutPanel tlp = this.Controls.OfType<TableLayoutPanel>().First();
            foreach (Peg peg in tlp.Controls)
            {
                colors.Add(Globals.ColorsInUse.IndexOf(peg.PegColor));
            }
            return colors;
        }

        public delegate void PegSelectedEventHandler(object sender, PegSelectedEventArgs e);
        public event PegSelectedEventHandler? PegSelected;
        public class PegSelectedEventArgs : EventArgs
        {
            public PegSelectedEventArgs(int Index, Peg peg) : base()
            {
                this.colorIndex = Index;
                this.peg = peg;
            }
            public int colorIndex;
            public Peg peg;
        }
        private void Peg_Selected(object sender, Peg.PegSelectedEventArgs e)
        {
            PegSelected?.Invoke(this, new PegSelectedEventArgs(e.colorIndex, (Peg)sender));
        }
        private void Peg_DroppedOnPeg(object sender, Peg.PegDroppedOnPegEventArgs e)
        {
            Color color1 = e.peg1.PegColor;
            Color color2 = e.peg2.PegColor;
            e.peg1.PegColor = color2;
            e.peg2.PegColor = color1;
        }
    }
}
