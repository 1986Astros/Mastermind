using System.Diagnostics;
using System.Windows.Forms;

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
            Globals.Registry.AddFormKey(this);

            pegBoard1.GameOver += PegBoard_GameOver;
            Globals.NamePlates.Add(playerControl1);
            playerControl1.PlayerName = "Guest";

            verticalTrayToolStripMenuItem.Checked = Globals.CradleOrientation == Cradle.Orientations.Vertical;
            horizontalTrayToolStripMenuItem.Checked = Globals.CradleOrientation == Cradle.Orientations.Horizontal;
            if  (Globals.CradleOrientation == Cradle.Orientations.Horizontal)
            {
                MakeCradleHorizontal();
            }
            if (Globals.BottomToTop)
            {
                SetPlayerCradleForPlayerAtBottom();
            }
            else
            {
                SetPlayerCradleForPlayerAtTop();
            }
            righthandedToolStripMenuItem.Checked = Globals.RightHanded;
            lefthandedToolStripMenuItem.Checked = !Globals.RightHanded;
            if (Globals.LeftHanded)
            {
                MakeLayoutLefthanded();
            }
            playerAtBottomToolStripMenuItem.Checked = Globals.BottomToTop;
            playerAtTopToolStripMenuItem.Checked = !Globals.BottomToTop;

            pegBoard1.AttachCradle(cradle1);
            pegBoard1.AttachAcceptClearButtons(acceptClearButtons1);

            Globals.CurrentGame.InitializeGame();
        }

        private void PegBoard_GameOver(object sender, PegBoard.GameOverEventArgs e)
        {
            Globals.Records.AddGameResult("Guest", Globals.CurrentGame.Pattern.ToArray(), e.Won, e.Turns);
            pegBoard1.Enabled = false;
            cradle1.Enabled = false;
            acceptClearButtons1.Enabled = false;
            startGameToolStripMenuItem.Enabled = true;
        }

        private void humansSamePuzzlesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void startGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.ShowSolution = false;
            Globals.CurrentGame.InitializeGame();
            pegBoard1.Enabled = true;
            pegBoard1.InitializeGame();
            cradle1.Enabled = true;
            acceptClearButtons1.Enabled = true;
            startGameToolStripMenuItem.Enabled = false;
        }

        private void righthandedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lefthandedToolStripMenuItem.Checked)
            {
                MakeLayoutRighthanded();
            }
        }
        private void MakeLayoutRighthanded()
        {
            righthandedToolStripMenuItem.Checked = true;
            lefthandedToolStripMenuItem.Checked = false;
            Globals.RightHanded = true;

            tlpBoard.SuspendLayout();
            tlpBoard.Controls.Remove(acceptClearButtons1);
            if (tlpBoard.Controls.Contains(cradle1))
            {
                tlpBoard.Controls.Remove(cradle1);
                if (Globals.BottomToTop)
                {
                    cradle1.Dock = DockStyle.Bottom;
                    tlpBoard.Controls.Add(cradle1, Globals.RightHanded ? 2 : 0, 0);
                }
                else
                {
                    cradle1.Dock = DockStyle.Top;
                    tlpBoard.Controls.Add(cradle1,  Globals.RightHanded? 0 : 2, 0);
                }
            }
            if (Globals.BottomToTop)
            {
                tlpBoard.Controls.Add(acceptClearButtons1, 0, 0);
            }
            else
            {
                tlpBoard.Controls.Add(acceptClearButtons1, 2, 0);
            }
            tlpBoard.ResumeLayout(false);
            tlpBoard.PerformLayout();

            pegBoard1.ChangedOrientation();
        }

        private void lefthandedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (righthandedToolStripMenuItem.Checked)
            {
                MakeLayoutLefthanded();
            }
        }
        private void MakeLayoutLefthanded()
        {
            righthandedToolStripMenuItem.Checked = false;
            lefthandedToolStripMenuItem.Checked = true;
            Globals.LeftHanded = true;

            tlpBoard.SuspendLayout();
            tlpBoard.Controls.Remove(acceptClearButtons1);
            if (tlpBoard.Controls.Contains(cradle1))
            {
                tlpBoard.Controls.Remove(cradle1);
                tlpBoard.Controls.Add(cradle1,Globals.BottomToTop ? 0 : 2, 0);
            }
            tlpBoard.Controls.Add(acceptClearButtons1, Globals.BottomToTop ? 2 : 0, 0);
            tlpBoard.ResumeLayout(false);
            tlpBoard.PerformLayout();

            pegBoard1.ChangedOrientation();
        }

        private void playerAtBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globals.BottomToTop)
            {
                Globals.BottomToTop = true;
                playerAtBottomToolStripMenuItem.Checked = true;
                playerAtTopToolStripMenuItem.Checked = false;

                tlpBoard.SuspendLayout();
                tlpBoard.Controls.Remove(acceptClearButtons1);
                tlpBoard.Controls.Add(acceptClearButtons1, Globals.RightHanded ? 2 : 0, 0);
                tlpBoard.ResumeLayout(false);
                tlpBoard.PerformLayout();

                pegBoard1.ChangedOrientation();
                SetPlayerCradleForPlayerAtBottom();
            }
        }
        private void SetPlayerCradleForPlayerAtBottom()
        {
            if (tlpCradleBoardCradle.Controls.Contains(cradle1))
            {
                tlpCradleBoardCradle.SuspendLayout();
                tlpCradleBoardCradle.Controls.Remove(cradle1);
                tlpCradleBoardCradle.Controls.Add(cradle1, 0, 2);
                tlpCradleBoardCradle.ResumeLayout(false);
                tlpCradleBoardCradle.PerformLayout();
            }
            else
            {
                tlpBoard.SuspendLayout();
                tlpBoard.Controls.Remove(acceptClearButtons1);
                tlpBoard.Controls.Remove(cradle1);
                tlpBoard.Controls.Add(cradle1, Globals.RightHanded ? 2 : 0, 0);
                tlpBoard.Controls.Add(acceptClearButtons1, Globals.RightHanded ? 0 : 2, 0);
                cradle1.Dock = DockStyle.Bottom;
                tlpBoard.ResumeLayout(false);
                tlpBoard.PerformLayout();
            }
        }

        private void playerAtTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // bug: this is not moving the accept/clear buttons to the other side
            if (Globals.BottomToTop)
            {
                Globals.TopToBottom = true;
                playerAtBottomToolStripMenuItem.Checked = false;
                playerAtTopToolStripMenuItem.Checked = true;
                
                tlpBoard.SuspendLayout();
                tlpBoard.Controls.Remove(acceptClearButtons1);
                tlpBoard.Controls.Add(acceptClearButtons1, Globals.RightHanded ? 2 : 0, 0);
                tlpBoard.ResumeLayout(false);
                tlpBoard.PerformLayout();

                pegBoard1.ChangedOrientation();
                SetPlayerCradleForPlayerAtTop();
            }
        }
        private void SetPlayerCradleForPlayerAtTop()
        {
            if (tlpCradleBoardCradle.Controls.Contains(cradle1))
            {
                tlpCradleBoardCradle.SuspendLayout();
                tlpCradleBoardCradle.Controls.Remove(cradle1);
                tlpCradleBoardCradle.Controls.Add(cradle1, 0, 0);
                tlpCradleBoardCradle.ResumeLayout(false);
                tlpCradleBoardCradle.PerformLayout();
            }
            else
            {
                tlpBoard.SuspendLayout();
                tlpBoard.Controls.Remove(acceptClearButtons1);
                tlpBoard.Controls.Remove(cradle1);
                tlpBoard.Controls.Add(cradle1, Globals.LeftHanded ? 2 : 0, 0);
                tlpBoard.Controls.Add(acceptClearButtons1, Globals.LeftHanded ? 0 : 2, 0);
                cradle1.Dock = DockStyle.Top;
                tlpBoard.ResumeLayout(false);
                tlpBoard.PerformLayout();
            }
        }

        private void verticalTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cradle1.Orientation == Cradle.Orientations.Horizontal)
            {
                MakeCradleVertical();
            }
        }
        private void MakeCradleVertical()
        {
            verticalTrayToolStripMenuItem.Checked = true;
            horizontalTrayToolStripMenuItem.Checked = false;
            cradle1.Orientation = Cradle.Orientations.Vertical;
            Globals.CradleOrientation = Cradle.Orientations.Vertical;

            tlpCradleBoardCradle.SuspendLayout();
            tlpCradleBoardCradle.Controls.Remove(cradle1);
            tlpCradleBoardCradle.ResumeLayout(false);
            tlpCradleBoardCradle.PerformLayout();
            tlpBoard.SuspendLayout();
            if (Globals.BottomToTop)
            {
                tlpBoard.Controls.Add(cradle1, Globals.RightHanded ? 2 : 0, 0);
            }
            else
            {
                tlpBoard.Controls.Add(cradle1, Globals.RightHanded ? 0 : 2, 0);
            }
            tlpBoard.ResumeLayout(false);
            tlpBoard.PerformLayout();
        }

        private void horizontalTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cradle1.Orientation == Cradle.Orientations.Vertical)
            {
                MakeCradleHorizontal();
            }
        }
        private void MakeCradleHorizontal()
        {
            verticalTrayToolStripMenuItem.Checked = false;
            horizontalTrayToolStripMenuItem.Checked = true;
            cradle1.Orientation = Cradle.Orientations.Horizontal;
            Globals.CradleOrientation = Cradle.Orientations.Horizontal;

            tlpBoard.SuspendLayout();
            tlpBoard.Controls.Remove(cradle1);
            tlpBoard.ResumeLayout(false);
            tlpBoard.PerformLayout();
            tlpCradleBoardCradle.SuspendLayout();
            tlpCradleBoardCradle.Controls.Add(cradle1, 0, Globals.BottomToTop ? 2 : 0);
            tlpCradleBoardCradle.ResumeLayout(false);
            tlpCradleBoardCradle.PerformLayout();
        }

        private void renaldoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolveWithAI(new ComputerPlayer("Renaldo") , true);
        }

        private void úrsulaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void andrésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolveWithAI(new ComputerPlayer("Andrés"), true);
        }

        private const bool UseHardCodedPuzzle = true;
        private void tatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolveWithAI(new ComputerPlayer("Tati"),true);
            return;

            //ComputerPlayer Tati = new ComputerPlayer() { PlayerName = "Tati" };

#if true
            Globals.ShowSolution = true;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    for (int k = 0; k < 6; k++)
                    {
                        for (int m = 0; m < 6; m++)
                        {
                            Debug.WriteLine($"{i},{j},{k},{m}");
                            Globals.CurrentGame.InitializeGame(new int[] { i, j, k, m });
                            pegBoard1.InitializeGame();
                            //Tati.Solve();

                            pegBoard1.ShowCurrentGame();

                            if (Globals.CurrentGame.Solved)
                            {
                                Debug.WriteLine($"Tati solved in {Globals.CurrentGame.Turns.Count()} turns.");
                            }
                            else
                            {
                                Debug.WriteLine($"Tati FAILED to solve in {Globals.CurrentGame.Turns.Count()} turns.");
                            }
                            Debug.WriteLine(new string('-', 20));
                        }
                    }
                }
            }
 #else
           if (TestTati)
            {
                // 0,0,3,4 exposes a bug in Renaldo when then 3rd and 4th are found it still considers 1 and 2 as possibilities
                Globals.CurrentGame.InitializeGame(new int[] { 0,3,2,1 });
            }
            else
            {
                CreatePuzzle cp = new CreatePuzzle();
                DialogResult dr = cp.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Globals.CurrentGame.InitializeGame(cp.GetColors());
                }
                else
                {
                    Globals.CurrentGame.InitializeGame();
                }
            }

            Globals.ShowSolution = true;
            pegBoard1.Enabled = true;
            pegBoard1.InitializeGame();
            cradle1.Enabled = true;
            acceptClearButtons1.Enabled = true;

            Tati.Solve();

            pegBoard1.ShowCurrentGame();

            if (Globals.CurrentGame.Solved)
            {
                MessageBox.Show($"Tati solved in {Globals.CurrentGame.Turns.Count()} turns.");
            }
            else
            {
                MessageBox.Show($"Tati failed to solve.");
            }
#endif
        }

        private void pepitoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void SolveWithAI(ComputerPlayer hal, bool Testing)
        {
            if (Testing)
            {
                Globals.ShowSolution = true;
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        for (int k = 0; k < 6; k++)
                        {
                            for (int m = 0; m < 6; m++)
                            {
                                Debug.WriteLine($"{i},{j},{k},{m}");
                                Globals.CurrentGame.InitializeGame(new int[] { i, j, k, m });
                                pegBoard1.InitializeGame();
                                hal.Solve();

                                pegBoard1.ShowCurrentGame();

                                if (Globals.CurrentGame.Solved)
                                {
                                    Debug.WriteLine($"{hal.PlayerName} solved in {Globals.CurrentGame.Turns.Count()} turns.");
                                }
                                else
                                {
                                    Debug.WriteLine($"{hal.PlayerName} FAILED to solve in {Globals.CurrentGame.Turns.Count()} turns.");
                                }
                                Debug.WriteLine(new string('-', 20));
                                //MessageBox.Show("next");
                            }
                        }
                    }
                }
            }
            else
            {
                if (UseHardCodedPuzzle)
                {
                    // 0,0,3,4 exposes a bug in Renaldo when then 3rd and 4th are found it still considers 1 and 2 as possibilities
                    Globals.CurrentGame.InitializeGame(new int[] { 0, 3, 2, 1 });
                }
                else
                {
                    CreatePuzzle cp = new CreatePuzzle();
                    DialogResult dr = cp.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        Globals.CurrentGame.InitializeGame(cp.GetColors());
                    }
                    else
                    {
                        Globals.CurrentGame.InitializeGame();
                    }
                }

                Globals.ShowSolution = true;
                pegBoard1.Enabled = true;
                pegBoard1.InitializeGame();
                cradle1.Enabled = true;
                acceptClearButtons1.Enabled = true;

                hal.Solve();

                pegBoard1.ShowCurrentGame();

                if (Globals.CurrentGame.Solved)
                {
                    MessageBox.Show($"{hal.PlayerName} solved in {Globals.CurrentGame.Turns.Count()} turns.");
                }
                else
                {
                    MessageBox.Show($"{hal.PlayerName} failed to solve.");
                }
            }
        }
    }
}