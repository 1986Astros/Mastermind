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
    public partial class Cradle : UserControl
    {
        public Cradle()
        {
            InitializeComponent();

            TableLayoutPanel tlp = new TableLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill,
                Margin = new Padding(3),
            };
            Peg peg;
            if (Orientation == Orientations.Vertical)
            {
                tlp.RowCount = Globals.ColorsInUse.Count();
                tlp.ColumnCount = 1;
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                for (int i = 0; i < Globals.ColorsInUse.Count(); i++)
                {
                    tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    peg = new Peg() { PegColor = Globals.ColorsInUse[i], Margin = new Padding(Globals.GamePegDiameter / 2), Size = new Size(Globals.GamePegDiameter, Globals.GamePegDiameter) };
                    peg.PegSelected += Peg_Selected;
                    tlp.Controls.Add(peg);
                }
            }
            else
            {
                tlp.RowCount = 1;
                tlp.ColumnCount = Globals.ColorsInUse.Count();
                tlp.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                for (int i = 0; i < Globals.ColorsInUse.Count(); i++)
                {
                    tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    peg = new Peg() { PegColor = Globals.ColorsInUse[i], Margin = new Padding(Globals.GamePegDiameter / 2), Size = new Size(Globals.GamePegDiameter, Globals.GamePegDiameter) };
                    peg.PegSelected += Peg_Selected;
                    tlp.Controls.Add(peg);
                }
            }
            Controls.Add(tlp);
        }

        public enum Orientations
        {
            Horizontal,
            Vertical
        }
        public Orientations Orientation
        {
            get
            {
                return Globals.CradleOrientation;
            }
            set
            {
                if (value != Globals.CradleOrientation)
                {
                    Globals.CradleOrientation = value;
                    TableLayoutPanel tlp = this.Controls.OfType<TableLayoutPanel>().First();
                    tlp.SuspendLayout();
                    tlp.ColumnStyles.Clear();
                    tlp.RowStyles.Clear();
                    if (Globals.CradleOrientation == Orientations.Vertical)
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
                        for (int i = 0; i < tlp.RowCount; i++)
                        {
                            tlp.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                        }
                    }
                    tlp.ResumeLayout();
                }
            }
        }

        public delegate void PegSelectedEventHandler(object sender, PegSelectedEventArgs e);
        public event PegSelectedEventHandler PegSelected;
        public class PegSelectedEventArgs : EventArgs
        {
            public PegSelectedEventArgs(int Index, Peg peg) : base()
            {
                this.Index = Index;
                this.peg = peg;
            }
            public int Index;
            public Peg peg;
        }

        private void Peg_Selected(object sender, Peg.PegSelectedEventArgs e)
        {
            PegSelected.Invoke(this, new PegSelectedEventArgs(e.Index, (Peg)sender));
        }
    }
}
