using System.Diagnostics;
using System.Windows.Forms;

namespace MasterMind
{
    public partial class ConsoleMM : Form
    {
        public ConsoleMM()
        {
            InitializeComponent();
            //Controls.Add(new GameflowControl() { Location = new Point(10, 50) });
        }
        
        private void ConsoleMM_Load(object sender, EventArgs e)
        {
            Globals.Registry.AddFormKey(this);

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

            IEnumerable<string> LastPlayers = Globals.LastPlayers;
            Globals.NamePlates.Add(playerControl1);
            playerControl1.PlayerName = LastPlayers.ElementAt(0);
            for (int i = 1; i < LastPlayers.Count(); i++)
            {
                playerControl_NewPlayer(this, new PlayerControl.NewPlayerEventArgs(LastPlayers.ElementAt(i)));
            }
            tableLayoutPanelScorecards_Resize(this, EventArgs.Empty);

            Globals.CurrentGame.InitializeGame();
            UpdateGameStatus();
            Initialized = true;
        }
        private bool Initialized = false;

        private void PegBoard_GameOver(object sender, PegBoard.GameOverEventArgs e)
        {
            gameflowPegboardGameOver(e);
            return;
            LastHuman = NextHuman++;
            PlayerControl pc =  Globals.NamePlates.First(np => np.PlayerName==HumansPlaying[LastHuman]);
            Globals.Records.AddGameResult(HumansPlaying[LastHuman], Globals.CurrentGame.Pattern.ToArray(), e.Won, e.Turns);
            pc.GameOver();
            pegBoard1.Enabled = false;
            cradle1.Enabled = false;
            UpdateGameStatus();
        }

        private void humansSamePuzzlesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private List<string> HumansPlaying;
        private int LastHuman;
        private int NextHuman;
        private void startGameToolStripMenuItem_Click(object sender, EventArgs e)
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
                startGameToolStripMenuItem.Enabled = false;
            }
            UpdateGameStatus();
        }

        enum LinkActions
        {
            None = 0,
            Start,
            Close,
            Next,
            Skip
        }
        private void UpdateGameStatus()
        {
            if (HumansPlaying == null )
            {
                // game hasn't begun
                labelInfo.Visible = false;
                linkLabelInfo.Text = "Start";
                linkLabelInfo.Anchor = AnchorStyles.Left;
                linkLabelInfo.Links.Clear();
                linkLabelInfo.Links.Add(new LinkLabel.Link(0, -1, LinkActions.Start));
                linkLabelInfo.Visible = true;
            }
            else if(NextHuman == HumansPlaying.Count)
            {
                // game over, determine the winner
                int WinningScore = Globals.NamePlates.Min(np => np.Turns);
                List<PlayerControl> pis = new List<PlayerControl>(Globals.NamePlates.Where(np => np.Turns == WinningScore && np.Solved));
                switch (pis.Count)
                {
                    case 0:
                        if (Globals.NamePlates.Count() == 1)
                        {
                            labelInfo.Text = $"{Globals.NamePlates[0].PlayerName} lost.";
                        }
                        else
                        {
                            labelInfo.Text = $"No one won.";
                        }
                        break;
                    case 1:
                        labelInfo.Text = $"{Globals.NamePlates.First(np => np.Turns == WinningScore).PlayerName} won.";
                        break;
                    default:
                        labelInfo.Text = "It's a tie.";
                        break;
                }
                foreach (PlayerControl pc in Globals.NamePlates)
                {
                    pc.ShowPegboard = true;
                    pc.ShowPegboardResults();
                }
                labelInfo.Visible = true;
                linkLabelInfo.Text = "Close game";
                linkLabelInfo.Anchor = AnchorStyles.Right;
                linkLabelInfo.Links.Clear();
                linkLabelInfo.Links.Add(new LinkLabel.Link(0, -1, LinkActions.Close));
                linkLabelInfo.Visible = true;
            }
            else if(pegBoard1.Enabled)
            {
                // say whose turn it is
                labelInfo.Text = $"{HumansPlaying[NextHuman]}, it's your turn.";
                labelInfo.Visible = true;
                linkLabelInfo.Visible = false;
            }
            else
            {
                // prompt for next player
                pegBoard1.CurrentGame = null;
                CurrentGame lastGame = Globals.NamePlates.First(np => np.PlayerName == HumansPlaying[LastHuman]).LastGame;
                if (lastGame.Solved)
                {
                    if (Globals.CurrentGame.Turns.Count() == 1)
                    {
                        labelInfo.Text = $"{HumansPlaying[LastHuman]} solved in 1 turn.";
                    }
                    else
                    {
                        labelInfo.Text = $"{HumansPlaying[LastHuman]} solved in {lastGame.Turns.Count()} turns.";
                    }
                }
                else
                {
                    labelInfo.Text = $"{HumansPlaying[LastHuman]} failed to solve.";
                }
                labelInfo.Visible = true;
                string NextPlayerName = HumansPlaying[NextHuman];
                linkLabelInfo.Text = $"Next: {NextPlayerName} | Skip player";
                linkLabelInfo.Links.Clear();
                LinkLabel.Link newLLL = new LinkLabel.Link("Next: ".Length, NextPlayerName.Length, LinkActions.Next);
                linkLabelInfo.Links.Add(newLLL);
                newLLL = new LinkLabel.Link(newLLL.Start + newLLL.Length + " | Skip ".Length, "player".Length, LinkActions.Skip);
                linkLabelInfo.Links.Add(newLLL);
                linkLabelInfo.Visible = true;

                pegBoard1.Enabled = true;
                cradle1.Enabled = true;
                acceptClearButtons1.Enabled = true;
            }
        }

        private void linkLabelInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch ((LinkActions)e.Link.LinkData)
            {
                case LinkActions.Start:
                    startGameToolStripMenuItem_Click(this, EventArgs.Empty);
                    break;
                case LinkActions.Close:
                    foreach(PlayerControl pc in Globals.NamePlates)
                    {
                        pc.Clear();
                        pc.Enabled = true;
                    }
                    HumansPlaying = null;
                    UpdateGameStatus();
                    break;
                case LinkActions.Next:
                    pegBoard1.InitializeGame();
                    pegBoard1.Enabled = true;
                    cradle1.Enabled = true;
                    CurrentGame newGame = new CurrentGame();
                    newGame.InitializeGame(Globals.CurrentGame.Pattern);
                    Globals.CurrentGame = newGame;
                    UpdateGameStatus();
                    break;
                case LinkActions.Skip:
                    if (MessageBox.Show("Really?", "Skip player", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                    {
                        NextHuman += 2;
                        newGame = new CurrentGame();
                        newGame.InitializeGame(Globals.CurrentGame.Pattern);
                        Globals.CurrentGame = newGame;
                        pegBoard1.InitializeGame();
                        UpdateGameStatus();
                    }
                    break;
            }
        }

        #region "Tabletop configuration"
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
        #endregion

        #region "Computer players"
        private const bool UseHardCodedPuzzle = false;
        private void renaldoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolveWithAI(new ComputerPlayer("Renaldo") , false);
        }

        private void úrsulaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolveWithAI(new ComputerPlayer("Úrsula"), false);
        }

        private void andrésToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolveWithAI(new ComputerPlayer("Andrés"), false);
        }

        private void tatiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolveWithAI(new ComputerPlayer("Tati"),false);
        }

        private void pepitoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolveWithAI(new ComputerPlayer("Pepito"), false);
        }

        private void SolveWithAI(ComputerPlayer hal, bool Testing)
        {
            if (Testing)
            {
                pegBoard1.ShowSolution = true;
                //Globals.ShowSolution = true;
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
                    //CreatePuzzle cp = new CreatePuzzle();
                    //DialogResult dr = cp.ShowDialog();
                    //if (dr == DialogResult.OK)
                    //{
                    //    Globals.CurrentGame.InitializeGame(cp.GetColors());
                    //}
                    //else
                    //{
                    //}
                    Globals.CurrentGame.InitializeGame();
                }

                pegBoard1.ShowSolution = true;
                //Globals.ShowSolution = true;
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
                startGameToolStripMenuItem.Enabled = false;

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
            int WinningScore = Globals.NamePlates.Min(np => np.Turns);
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
                pc.ShowPegboardResults();
            }
        }

        // When the pegboard fires the GameOver event, it's for that player's turn.
        // If there are more players, proceed for them to play.
        // If solo player done, GameflowControl1.SoloGameOver()
        // else If all players done, PostCompetitionResult()

        private void gameflowControl1_NextPlayer(object sender, GameflowControl.NextPlayerEventArgs e)
        {
            if (e.SkipNextPlayer)
            {

            }
            pegBoard1.InitializeGame();
            pegBoard1.Enabled = true;
            cradle1.Enabled = true;
            CurrentGame newGame = new CurrentGame();
            newGame.InitializeGame(Globals.CurrentGame.Pattern);
            Globals.CurrentGame = newGame;
            LastHuman = NextHuman;
            gameflowControl1.YourTurn(HumansPlaying[NextHuman]);
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
            //switch (e.MessageType)
            //{
            //    case GameflowControl.MessageTypes.SoloGameOver:
            //        break;
            //    case GameflowControl.MessageTypes.MultiPlayerWinner:
            //        break;
            //    case GameflowControl.MessageTypes.MultiPlayerTie:
            //        break;
            //    case GameflowControl.MessageTypes.MultiPlayerLosers:
            //        break;
            //}
        }
#endregion
    }
}