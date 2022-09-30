namespace MasterMind
{
    public partial class ConsoleMM : Form
    {
        public ConsoleMM()
        {
            InitializeComponent();
        }

        private void ConsoleMM_Load(object sender, EventArgs e)
        {
            pegBoard1.AttachCradle(cradle1);

            Globals.CurrentGame.InitializeGame();
            //for (int i = 0; i < 4; i++)
            //{
            //    Globals.CurrentGame.PlacePeg(i,i);
            //}
            //Globals.CurrentGame.EvaluateTurn();
            //Globals.CurrentGame.PlacePeg(0, 3);
            //Globals.CurrentGame.PlacePeg(4,2);
            //Globals.CurrentGame.PlacePeg(5,1);
            //Globals.CurrentGame.RemovePeg(3);

            //pegBoard1.Invalidate();
        }

        private void humansSamePuzzlesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}