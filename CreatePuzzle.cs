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
    public partial class CreatePuzzle : Form
    {
        public CreatePuzzle()
        {
            InitializeComponent();
            Randomize();
        }

        private void Randomize()
        {
            List<int> colors = new List<int>();
            for (int i = 0; i < cradlePuzzle.PegCount; i++)
            {
                colors.Add(Globals.rnd.Next(0, Globals.ColorsInUse.Count));
            }
            cradlePuzzle.SetColors(colors);
        }

        private void btnRandomize_Click(object sender, EventArgs e)
        {
            Randomize();
        }

        private void btnUseThisPuzzle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public IEnumerable<int> GetColors()
        {
            return cradlePuzzle.GetColors();
        }
    }
}
