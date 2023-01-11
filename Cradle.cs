using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private void InitializeControls(IEnumerable<Peg> sourcePegs = null)
        {
            Controls.Clear();
            PerformLayout();
            TableLayoutPanel tlp = new TableLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill,
                GrowStyle = TableLayoutPanelGrowStyle.FixedSize,
                Margin = new Padding(3),
            };
            Peg peg;
            List<Peg> Pegs = new List<Peg>();
            if (sourcePegs == null)
            {
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
            }
            else
            {
                Pegs.AddRange(sourcePegs);
            }
            if (Orientation == Orientations.Vertical)
            {
                tlp.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
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
                tlp.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
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
            tlp.PerformLayout();
            PerformLayout();
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

                    Peg[] pegs = new Peg[Globals.ColorsInUse.Count];
                    (this.Controls.OfType<TableLayoutPanel>().First()).Controls.CopyTo(pegs, 0);

                    InitializeControls(pegs);
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
                    Peg[] pegs = new Peg[Globals.ColorsInUse.Count];
                    if (Orientation == Orientations.Vertical)
                    {
                        if (tlp.RowCount < Globals.ColorsInUse.Count)
                        {
                            for (int col = Globals.ColorsInUse.Count - 1; col >= 0; col--)
                            {
                                pegs[col] = (Peg)(tlp.Controls[col]);
                                tlp.Controls.Remove(pegs[col]);
                            }
                        }
                        else
                        {
                            for (int row = Globals.ColorsInUse.Count - 1; row >= 0; row--)
                            {
                                pegs[row] = (Peg)(tlp.Controls[row]);
                                tlp.Controls.Remove(pegs[row]);
                            }
                        }
                        for (int newRow = 0, oldRow = Globals.ColorsInUse.Count - 1; oldRow >= 0; newRow++, oldRow--)
                        {
                            tlp.Controls.Add(pegs[oldRow], 0, newRow);
                        }
                    }
                    else
                    {
                        if (tlp.ColumnCount < Globals.ColorsInUse.Count)
                        {
                            for (int row = Globals.ColorsInUse.Count - 1; row >= 0; row--)
                            {
                                pegs[row] = (Peg)(tlp.Controls[row]);
                                tlp.Controls.Remove(pegs[row]);
                            }
                        }
                        else
                        {
                            for (int col = Globals.ColorsInUse.Count - 1; col >= 0; col--)
                            {
                                pegs[col] = (Peg)(tlp.Controls[col]);
                                tlp.Controls.Remove(pegs[col]);
                            }
                        }
                        for (int newCol = 0, oldCol = Globals.ColorsInUse.Count - 1; oldCol >= 0; newCol++, oldCol--)
                        {
                            tlp.Controls.Add(pegs[oldCol], newCol, 0);
                        }
                    }
                    shhPegDirection = value;
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
