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

            verticalTrayToolStripMenuItem.Checked = Globals.CradleOrientation == Cradle.Orientations.Vertical;
            horizontalTrayToolStripMenuItem.Checked = Globals.CradleOrientation == Cradle.Orientations.Horizontal;
            righthandedToolStripMenuItem.Checked = Globals.RightHanded;
            lefthandedToolStripMenuItem.Checked = Globals.LeftHanded;
            playerAtBottomToolStripMenuItem.Checked = Globals.BottomToTop;
            playerAtTopToolStripMenuItem.Checked = Globals.TopToBottom;
            ArrangeGameSurface();

            pegBoard1.AttachCradle(cradle1);
            pegBoard1.AttachAcceptClearButtons(acceptClearButtons1);

            IEnumerable<string> LastPlayers = Globals.LastPlayers;
            Globals.NamePlates.Add(playerControl1);
            playerControl1.PlayerName = LastPlayers.ElementAt(0);
            for (int i = 1; i < LastPlayers.Count(); i++)
            {
                playerControl_NewPlayer(this, new PlayerControl.NewPlayerEventArgs(LastPlayers.ElementAt(i)));
            }
            tableLayoutPanelScorecards_Resize(this, EventArgs.Empty);

            Globals.CurrentGame.InitializeGame();
            Initialized = true;
        }
        private bool Initialized = false;

        private void PegBoard_GameOver(object sender, PegBoard.GameOverEventArgs e)
        {
            gameflowPegboardGameOver(e);
        }

        private List<string> HumansPlaying;
        private int LastHuman;
        private int NextHuman;

        #region "Tabletop configuration"
        private void righthandedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("RightHanded");
            righthandedToolStripMenuItem.Checked = true;
            lefthandedToolStripMenuItem.Checked = false;
            Globals.RightHanded = true;
            ArrangeGameSurface();
        }
        private void lefthandedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("LeftHanded");
            righthandedToolStripMenuItem.Checked = false;
            lefthandedToolStripMenuItem.Checked = true;
            Globals.LeftHanded = true;
            ArrangeGameSurface();
        }
        private void playerAtBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("BottomToTop");
            playerAtBottomToolStripMenuItem.Checked = true;
            playerAtTopToolStripMenuItem.Checked = false;
            Globals.BottomToTop = true;
            ArrangeGameSurface();
        }
        private void playerAtTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("TopToBottom");
            playerAtBottomToolStripMenuItem.Checked = false;
            playerAtTopToolStripMenuItem.Checked = true;
            Globals.TopToBottom = true;
            ArrangeGameSurface();
        }
        private void verticalTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Vertical");
            verticalTrayToolStripMenuItem.Checked = true;
            horizontalTrayToolStripMenuItem.Checked = false;
            Globals.CradleOrientation = Cradle.Orientations.Vertical;
            ArrangeGameSurface();
        }
        private void horizontalTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Horizontal");
            verticalTrayToolStripMenuItem.Checked = false;
            horizontalTrayToolStripMenuItem.Checked = true;
            Globals.CradleOrientation = Cradle.Orientations.Horizontal;
            ArrangeGameSurface();
        }

        private void ArrangeGameSurface()
        {
            tlpCradleBoardCradle.SuspendLayout();
            tlpBoard.SuspendLayout();

            if (Globals.CradleOrientation == Cradle.Orientations.Vertical)
            {
                cradle1.Orientation = Cradle.Orientations.Vertical;
                if (tlpCradleBoardCradle.Controls.Contains(cradle1))
                {
                    tlpCradleBoardCradle.Controls.Remove(cradle1);
                    if (Globals.BottomToTop)
                    {
                        if (Globals.RightHanded)
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 2)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 0, 0);
                            }
                            tlpBoard.Controls.Add(cradle1, 2, 0);
                        }
                        else
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 0)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 2, 0);
                            }
                            tlpBoard.Controls.Add(cradle1, 0, 0);
                        }
                        cradle1.Dock = DockStyle.Bottom;
                        cradle1.PegDirection = Cradle.PegDirections.LeftToRight;
                    }
                    else
                    {
                        if (Globals.LeftHanded)
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 2)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 0, 0);
                            }
                            tlpBoard.Controls.Add(cradle1, 2, 0);
                        }
                        else
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 0)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 2, 0);
                            }
                            tlpBoard.Controls.Add(cradle1, 0, 0);
                        }
                        cradle1.Dock = DockStyle.Top;
                        cradle1.PegDirection = Cradle.PegDirections.RightToLeft;
                    }
                }
                else
                {
                    if (Globals.BottomToTop)
                    {
                        if (Globals.RightHanded)
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 2)
                            {
                                tlpBoard.Controls.Remove(cradle1);
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 0, 0);
                                tlpBoard.Controls.Add(cradle1, 2, 0);
                            }
                        }
                        else
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 0)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Remove(cradle1);
                                tlpBoard.Controls.Add(cradle1, 0, 0);
                                tlpBoard.Controls.Add(acceptClearButtons1, 2, 0);
                             }
                        }
                        cradle1.Dock = DockStyle.Bottom;
                        cradle1.PegDirection = Cradle.PegDirections.LeftToRight;
                    }
                    else
                    {
                        if (Globals.LeftHanded)
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 2)
                            {
                                tlpBoard.Controls.Remove(cradle1);
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 0, 0);
                                tlpBoard.Controls.Add(cradle1, 2, 0);
                            }
                        }
                        else
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 0)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Remove(cradle1);
                                tlpBoard.Controls.Add(cradle1, 0, 0);
                                tlpBoard.Controls.Add(acceptClearButtons1, 2, 0);
                            }
                        }
                        cradle1.Dock = DockStyle.Top;
                        cradle1.PegDirection = Cradle.PegDirections.RightToLeft;
                    }
                }
            }
            else 
            {
                cradle1.Orientation = Cradle.Orientations.Horizontal;
                cradle1.Dock = DockStyle.None;
                cradle1.Anchor = AnchorStyles.None;
                if (tlpBoard.Controls.Contains(cradle1))
                {
                    tlpBoard.Controls.Remove(cradle1);
                    if (Globals.BottomToTop)
                    {
                        if (Globals.RightHanded)
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 2)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 0, 0);
                            }
                        }
                        else
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 0)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 2, 0);
                            }
                        }
                        tlpCradleBoardCradle.Controls.Add(cradle1, 0, 2);
                        cradle1.PegDirection = Cradle.PegDirections.LeftToRight;
                    }
                    else
                    {
                        if (Globals.LeftHanded)
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 2)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 0, 0);
                            }
                        }
                        else
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 0)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 2, 0);
                            }
                        }
                        tlpCradleBoardCradle.Controls.Add(cradle1, 0, 0);
                        cradle1.PegDirection = Cradle.PegDirections.RightToLeft;
                    }
                }
                else
                {
                    if (Globals.BottomToTop)
                    {
                        if (tlpCradleBoardCradle.GetPositionFromControl(cradle1).Row== 0)
                        {
                            tlpCradleBoardCradle.Controls.Remove(cradle1);
                            tlpCradleBoardCradle.Controls.Add(cradle1, 0, 2);
                        }
                        if (Globals.RightHanded)
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 2)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 0, 0);
                            }
                        }
                        else
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 0)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 2, 0);
                            }
                        }
                        cradle1.Dock = DockStyle.Bottom;
                        cradle1.PegDirection = Cradle.PegDirections.LeftToRight;
                    }
                    else
                    {
                        if (tlpCradleBoardCradle.GetPositionFromControl(cradle1).Row == 2)
                        {
                            tlpCradleBoardCradle.Controls.Remove(cradle1);
                            tlpCradleBoardCradle.Controls.Add(cradle1, 0, 0);
                        }
                        if (Globals.LeftHanded)
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 2)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 0, 0);
                            }
                        }
                        else
                        {
                            if (tlpBoard.GetPositionFromControl(acceptClearButtons1).Column == 0)
                            {
                                tlpBoard.Controls.Remove(acceptClearButtons1);
                                tlpBoard.Controls.Add(acceptClearButtons1, 2, 0);
                            }
                        }
                        cradle1.Dock = DockStyle.Top;
                        cradle1.PegDirection = Cradle.PegDirections.RightToLeft;
                    }
                }
            }
            pegBoard1.ChangedOrientation();     // updates peg positions

            tlpBoard.ResumeLayout(false);
            tlpCradleBoardCradle.ResumeLayout(false);
            tlpBoard.PerformLayout();
            tlpCradleBoardCradle.PerformLayout();
        }
        #endregion

        #region "Computer players"
        private const bool UseHardCodedPuzzle = false;
#if UseThisCodeToTestNewAIPlayers
        private void SolveWithAI(ComputerPlayer hal, bool Testing)
        {
            if (Testing)
            {
                pegBoard1.ShowSolution = true;
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
                    Globals.CurrentGame.InitializeGame();
                }

                pegBoard1.ShowSolution = true;
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
#endif
        #endregion

        private void tableLayoutPanelScorecards_Resize(object sender, EventArgs e)
        {
            panelScorecardOuter.Size = new Size(panelScorecardOuter.Width, tableLayoutPanelScorecards.Margin.Vertical + panelScorecardInner.Margin.Vertical + 2 + tableLayoutPanelScorecards.Height + SystemInformation.HorizontalScrollBarHeight);
        }

#region "Changes to roster"
        private void playerControl_NewPlayer(object sender, PlayerControl.NewPlayerEventArgs e)
        {
            PlayerControl pc = new PlayerControl() { PlayerName = e.PlayerName, Dock = DockStyle.Fill, Margin = Globals.NamePlates[0].Margin };
            pc.NewPlayer += playerControl_NewPlayer;
            pc.ReplacePlayer += playerControl_ReplacePlayer;
            pc.RemovePlayer += playerControl_RemovePlayer;
            Globals.NamePlates.Add(pc);
            tableLayoutPanelScorecards.ColumnCount++;
            tableLayoutPanelScorecards.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutPanelScorecards.Controls.Add(pc, Globals.NamePlates.Count - 1, 0);
            if (Initialized)
            {
                Globals.LastPlayers = Globals.NamePlates.Select(np => np.PlayerName).ToArray();
            }
        }
        private void playerControl_RemovePlayer(object sender, EventArgs e)
        {
            PlayerControl pc = (PlayerControl)sender;
            int col = tableLayoutPanelScorecards.GetColumn(pc);
            tableLayoutPanelScorecards.Controls.Remove(pc);
            foreach (Control c in tableLayoutPanelScorecards.Controls)
            {
                if (tableLayoutPanelScorecards.GetColumn(c) > col)
                {
                    tableLayoutPanelScorecards.SetColumn(c, tableLayoutPanelScorecards.GetColumn(c) - 1);
                }
            }
            tableLayoutPanelScorecards.ColumnStyles.RemoveAt(--tableLayoutPanelScorecards.ColumnCount);
            Globals.NamePlates.Remove(pc);
            if (Initialized)
            {
                Globals.LastPlayers = Globals.NamePlates.Select(np => np.PlayerName).ToArray();
            }
        }
        private void playerControl_ReplacePlayer(object sender, PlayerControl.ReplacePlayerEventArgs e)
        {
            if (Initialized)
            {
                Globals.LastPlayers = Globals.NamePlates.Select(np => np.PlayerName).ToArray();
            }
        }
#endregion

#region "game flow"
        private void gameflowControl1_StartGame(object sender, EventArgs e)
        {
            pegBoard1.ShowSolution = false;
            Globals.CurrentGame.InitializeGame();

            HumansPlaying = new List<string>(Globals.NamePlates.Where(np => Records.Player(np.PlayerName).IsHuman).Select(np => np.PlayerName));
            NextHuman = 0;
            LastHuman = -1;

            foreach (PlayerControl pc in Globals.NamePlates)
            {
                pc.Enabled = false;
                if (HumansPlaying.Count > 1 || Records.Player(pc.PlayerName).IsAI)
                {
                    pc.ShowPegboard = true;
                }
            }

            ComputerPlayer hal;
            foreach (PlayerControl playerControl in Globals.NamePlates.Where(np => Records.Player(np.PlayerName).ID < 0))
            {
                playerControl.StartGame(true);
                hal = new ComputerPlayer(playerControl.PlayerName);
                hal.Solve();
                playerControl.LastGame = hal.CurrentGame;
                Globals.Records.AddGameResult(playerControl.PlayerName, hal.CurrentGame.Pattern.ToArray(), hal.CurrentGame.Solved, hal.CurrentGame.Turns.Count());
                playerControl.GameOver();
            }

            pegBoard1.CurrentGame = null;
            if (HumansPlaying.Count > 0)
            {
                pegBoard1.Enabled = true;
                cradle1.Enabled = true;
                acceptClearButtons1.Enabled = true;

                PlayerControl playerControl = Globals.NamePlates.First(np => np.PlayerName.Equals(HumansPlaying[NextHuman], StringComparison.InvariantCultureIgnoreCase));
                pegBoard1.InitializeGame();
                playerControl.StartGame(HumansPlaying.Count > 1);

                gameflowControl1.YourTurn(HumansPlaying[0]);
            }
            else
            {
                PostCompetitionResult();
            }
        }
        private void PostCompetitionResult()
        {
            // game over, determine the winner
            int WinningScore = Globals.NamePlates.Where(np => np.Solved).Min(np => np.Turns);
            List<PlayerControl> pis = new List<PlayerControl>(Globals.NamePlates.Where(np => np.Turns == WinningScore && np.Solved));
            switch (pis.Count)
            {
                case 0:
                    gameflowControl1.MultiPlayerLosers();
                    break;
                case 1:
                    gameflowControl1.MultiPlayerWinner(pis[0].PlayerName, WinningScore);
                    break;
                default:
                    gameflowControl1.MultiPlayerTie(pis.Select(pi => pi.PlayerName), WinningScore);
                    break;
            }
            foreach (PlayerControl pc in Globals.NamePlates)
            {
                pc.ShowPegboard = true;
                if (pc.Solved)
                {
                    pc.ShowPegboardResults();
                }
            }
        }

        // When the pegboard fires the GameOver event, it's for that player's turn.
        // If there are more players, proceed for them to play.
        // If solo player done, GameflowControl1.SoloGameOver()
        // else If all players done, PostCompetitionResult()

        private void gameflowControl1_NextPlayer(object sender, GameflowControl.NextPlayerEventArgs e)
        {
            pegBoard1.InitializeGame();
            pegBoard1.Enabled = true;
            cradle1.Enabled = true;
            Globals.CurrentGame = new CurrentGame(Globals.CurrentGame.Pattern);
            if (e.SkipNextPlayer)
            {
                LastHuman = ++NextHuman;
                if (NextHuman == HumansPlaying.Count)
                {
                    PostCompetitionResult();
                }
                else
                {
                    gameflowControl1.YourTurn(HumansPlaying[NextHuman]);
                }
            }
            else
            {
                LastHuman = NextHuman;
                gameflowControl1.YourTurn(HumansPlaying[NextHuman]);
            }
        }
        private void gameflowPegboardGameOver(PegBoard.GameOverEventArgs e)
        {
            LastHuman = NextHuman++;
            PlayerControl pc = Globals.NamePlates.First(np => np.PlayerName == HumansPlaying[LastHuman]);
            Globals.Records.AddGameResult(HumansPlaying[LastHuman], Globals.CurrentGame.Pattern.ToArray(), e.Won, e.Turns);
            pc.GameOver();
            pegBoard1.Enabled = false;
            cradle1.Enabled = false;
            if (HumansPlaying.Count == 1)
            {
                if (Globals.NamePlates.Count == 1)
                {
                    gameflowControl1.SoloGameOver(HumansPlaying[0], pc.LastGame.Solved, pc.LastGame.Turns.Count());
                    pegBoard1.ShowSolution = true;
                }
                else
                {
                    PostCompetitionResult();
                }
            }
            else if (NextHuman == HumansPlaying.Count)
            {
                PostCompetitionResult();
            }
            else
            {
                pegBoard1.ShowSolution = true;
                gameflowControl1.NextHuman(HumansPlaying[LastHuman],e.Won,e.Turns,HumansPlaying[NextHuman]);
            }
        }
        private void gameflowControl1_NextGame(object sender, GameflowControl.NextGameEventArgs e)
        {
            foreach (PlayerControl pc in Globals.NamePlates)
            {
                pc.Clear();
                pc.Enabled = true;
            }
            HumansPlaying = null;
            gameflowControl1.Start();
        }
#endregion
    }
}