using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MasterMind
{
    public partial class PlayerControl : UserControl
    {
        public PlayerControl()
        {
            InitializeComponent();
        }

        [Browsable(false)]
        public string PlayerName
        {
            get
            {
                return shhPlayerName;
            }
            set
            {
                if (!value.Equals(shhPlayerName, StringComparison.CurrentCultureIgnoreCase))
                {
                    shhPlayerName = value;
                    playerToolStripDropDownButton.Text = shhPlayerName;
                    LoadPlayerStats();
                }
            }
        }
        private string shhPlayerName = "";

        private bool GameUnderway = false;

        #region "events"
        public class NewPlayerEventArgs : EventArgs
        {
            public NewPlayerEventArgs(string PlayerName)
            {
                this.PlayerName = PlayerName;
            }
            public string PlayerName;
        }
        public delegate void NewPlayerEventHandler(object sender, NewPlayerEventArgs e);
        public event NewPlayerEventHandler? NewPlayer;
        public class ReplacePlayerEventArgs : EventArgs
        {
            public ReplacePlayerEventArgs(string PreviousPlayer, string ReplacementPlayer) : base()
            {
                PreviousPlayerName = PreviousPlayer;
                ReplacingPlayerName = ReplacementPlayer;
            }
            public string PreviousPlayerName;
            public string ReplacingPlayerName;
        }
        public delegate void ReplacePlayerEventHandler(object sender, ReplacePlayerEventArgs e);
        public event ReplacePlayerEventHandler? ReplacePlayer;
        public delegate void RemovePlayerEventHandler(object sender, EventArgs e);
        public event RemovePlayerEventHandler? RemovePlayer;
        #endregion
        [Browsable(false)]
        public bool Solved
        {
            get
            {
                if (LastGame == null)
                {
                    return false;
                }
                else
                {
                    return LastGame.Solved;
                }
            }
        }
        [Browsable(false)]
        public int Turns
        {
            get
            {
                if (LastGame == null)
                {
                    return 0;
                }
                else
                {
                    return LastGame.Turns.Count();
                }
            }
        } 
        [Browsable(false)]
        public CurrentGame LastGame = null;

        public bool ShowPegboard
        {
            get
            {
                return pegBoard1.Visible;
            }
            set
            {
                if (value != pegBoard1.Visible)
                {
                    pegBoard1.Visible = value;
                    lvPerformance.Visible = !value;
                    if (lvPerformance.Visible)
                    {
                        tlpPerformance.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 00);
                        tlpPerformance.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 100);
                    }
                    else
                    {
                        tlpPerformance.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 100);
                        tlpPerformance.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0);
                    }
                }
            }
        }
        public void ShowPegboardResults()
        {
            pegBoard1.ArrangePegs();
            pegBoard1.ShowSolution = true;
            pegBoard1.CurrentGame = LastGame;
        }
        private void PlayerControl_Load(object sender, EventArgs e)
        {
            pegBoard1.Visible = ShowPegboard;
            lvPerformance.Visible = !ShowPegboard;
        }

        public void StartGame(bool ExposePegboard)
        {
            pegBoard1.CurrentGame = null;
            ShowPegboard = ExposePegboard;
            playerToolStripDropDownButton.Enabled = false;
            addOpponentToolStripMenuItem.Enabled = false;
            removeFromGameToolStripMenuItem.Enabled = false;
            LastGame = null;
            GameUnderway = true;
        }
        public void GameOver()
        {
            playerToolStripDropDownButton.Enabled = true;
            addOpponentToolStripMenuItem.Enabled = true;
            removeFromGameToolStripMenuItem.Enabled = Globals.NamePlates.Count > 1;
            GameUnderway = false;
            LastGame = Globals.CurrentGame;
            Globals.CurrentGame = new CurrentGame(LastGame.Pattern);
            LoadPlayerStats();
        }
        public void Clear()
        {
            LastGame = null;
            ShowPegboard = false;
            pegBoard1.InitializeGame();
            // BUG: Me/Tati/Op after playing the game then starting next, Op's scoring pegs are still drawn

            pegBoard1.CurrentGame = null;
            pegBoard1.Invalidate();     // is this needed? trying to fix a bug where the last human's scoring pegs are still there next game
            Invalidate();
        }

        private void LoadPlayerStats()
        {
            lvPerformance.SuspendLayout();
            lvPerformance.Items.Clear();

            Records.PlayerInfo playerInfo = Globals.Records.AllPlayers.First(pi => pi.PlayerName.Equals(PlayerName, StringComparison.CurrentCultureIgnoreCase));
            if (playerInfo is not null && playerInfo.PuzzleHistory is not null)
            {
                List<ListViewItem> statLines = new List<ListViewItem>();
                IEnumerable<Records.PuzzleInfo> puzzlesInfo = playerInfo.PuzzleHistory;
                ListViewItem lvi;
                IEnumerable<Records.PuzzleInfo> earlierPuzzlesInfo;
                foreach (Records.PuzzleInfo pi in puzzlesInfo.Reverse())
                {
                    lvi = new ListViewItem(new string[lvPerformance.Columns.Count - 1]);
                    lvi.SubItems[puzzleColumnHeader.Index].Text = pi.Colors;
                    lvi.SubItems[wonLostcolumnHeader.Index].Text = pi.Won ? "W-" + pi.Turns.ToString(): "L";
                    if (pi.Won)
                    {
                        earlierPuzzlesInfo = puzzlesInfo.Where(puzz => puzz.TimeStamp <= pi.TimeStamp);
                        lvi.SubItems[puzzleAvgcolumnHeader.Index].Text = ((float)(earlierPuzzlesInfo.Where(epi => epi.Won).Sum(epi => epi.Turns)) / ((float)(earlierPuzzlesInfo.Where(epi => epi.Won).Count()))).ToString("N2");
                        lvi.SubItems[puzzleAvgColorsColumnHeader.Index].Text = ((float)(earlierPuzzlesInfo.Where(epi => pi.AgnosticPatternByColor == epi.AgnosticPatternByColor).Sum(epi => epi.Turns)) / ((float)(earlierPuzzlesInfo.Where(epi => pi.AgnosticPatternByColor == epi.AgnosticPatternByColor).Count()))).ToString("N2");
                        lvi.SubItems[puzzleAvgOrderColumnHeader.Index].Text = ((float)(earlierPuzzlesInfo.Where(epi => pi.AgnosticPatternByOrder == epi.AgnosticPatternByOrder).Sum(epi => epi.Turns)) / ((float)(earlierPuzzlesInfo.Where(epi => pi.AgnosticPatternByOrder == epi.AgnosticPatternByOrder).Count()))).ToString("N2");
                    }
                    lvi.SubItems[whenColumnHeader.Index].Text = pi.TimeStamp.ToString("g");
                    statLines.Add(lvi);
                }
                lvPerformance.Items.AddRange(statLines.ToArray());
                if (statLines.Count > 0)
                {
                    puzzleColumnHeader.Width = (lvPerformance.GetItemRect(0).Height + pegRectInset + pegRectOffset) * playerInfo.PuzzleHistory.Max(pi => pi.PegCount);
                }
                foreach (ColumnHeader ch in lvPerformance.Columns)
                {
                    if (ch != puzzleColumnHeader)
                    {
                        ch.Width = -2;
                    }
                }
            }

            lvPerformance.ResumeLayout();
        }

        private List<ToolStripItem> PlayerMenuItems = new List<ToolStripItem>();

        #region "Player menu"
        private void playerToolStripDropDownButton_DropDownOpening(object sender, EventArgs e)
        {
            if (PlayerMenuItems.Count() > 0)
            {
                foreach (ToolStripMenuItem _tsmi in PlayerMenuItems)
                {
                    playerToolStripDropDownButton.DropDownItems.Remove(_tsmi);
                }
                PlayerMenuItems.Clear();
                playerToolStripDropDownButton.DropDownItems.RemoveAt(playerToolStripDropDownButton.DropDownItems.Count - 1);    // the separator that was added previously
            }

            ToolStripMenuItem tsmi;
            playerToolStripDropDownButton.Text = PlayerName;
            List<ToolStripItem> ComputerPlayerMenuItems() 
            {
                List<ToolStripItem> items = new List<ToolStripItem>();
                foreach (Records.PlayerInfo pi in Globals.Records.AllPlayers.Where(pi => pi.IsAI))
                {
                    tsmi = new ToolStripMenuItem(pi.PlayerName, null, new EventHandler(humanPlayerToolStripMenuItem_Click))
                    {
                        Checked = pi.PlayerName == PlayerName,
                        Enabled = !Globals.NamePlates.Any(np => np.PlayerName.Equals(pi.PlayerName, StringComparison.CurrentCultureIgnoreCase))
                    };
                    items.Add(tsmi);
                }
                return items;
            }
            List<ToolStripItem> HumanPlayersMenuItems() 
            {
                List<ToolStripItem> items = new List<ToolStripItem>();
                foreach (Records.PlayerInfo pi in Globals.Records.AllPlayers.Where(pi => pi.IsHuman))
                {
                    tsmi = new ToolStripMenuItem(pi.PlayerName, null, new EventHandler(humanPlayerToolStripMenuItem_Click))
                    {
                        Checked = pi.PlayerName == PlayerName,
                        Enabled = !Globals.NamePlates.Any(np => np.PlayerName.Equals(pi.PlayerName, StringComparison.CurrentCultureIgnoreCase))
                    };
                    items.Add(tsmi);
                }
                return items;
            }
            List<ToolStripItem> items;
            ToolStripItem separator = new ToolStripSeparator();
            if (Records.Player(PlayerName).IsAI)
            {
                items = ComputerPlayerMenuItems();
                PlayerMenuItems.AddRange(items);
                playerToolStripDropDownButton.DropDownItems.AddRange( items.ToArray());
                playerToolStripDropDownButton.DropDownItems.Add(separator);
                items = HumanPlayersMenuItems();
                PlayerMenuItems.AddRange(items);
                playerToolStripDropDownButton.DropDownItems.AddRange(items.ToArray());
            }
            else
            {
                items = HumanPlayersMenuItems();
                PlayerMenuItems.AddRange(items);
                playerToolStripDropDownButton.DropDownItems.AddRange(items.ToArray());
                playerToolStripDropDownButton.DropDownItems.Add(separator);
                items = ComputerPlayerMenuItems();
                PlayerMenuItems.AddRange(items);
                playerToolStripDropDownButton.DropDownItems.AddRange(items.ToArray());
            }
        }

        #region "New player name"
        private void newPlayerNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewPlayer();
        }

        private void newPlayerToolStripTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return && newPlayerLabelToolStripMenuItem.Enabled)
            {
                e.Handled = true;
                AddNewPlayer();
            }
        }

        private void newPlayerToolStripTextBox_TextChanged(object sender, EventArgs e)
        {
            if (newPlayerToolStripTextBox.Text.Contains(','))
            {
                newPlayerToolStripTextBox.BackColor = Color.MistyRose;
                newPlayerLabelToolStripMenuItem.Text = "Commas not allowed.";
                newPlayerLabelToolStripMenuItem.Enabled = false;
            }
            else if (Globals.Records.AllPlayers.Any(pi => pi.PlayerName.Equals(newPlayerToolStripTextBox.Text, StringComparison.CurrentCultureIgnoreCase)))
            {
                newPlayerToolStripTextBox.BackColor = Color.MistyRose;
                newPlayerLabelToolStripMenuItem.Text = "Player exists.";
                newPlayerLabelToolStripMenuItem.Enabled = false;
            }
            else if (newToolStripMenuItem.Text.Trim().Length == 0)
            {
                newPlayerToolStripTextBox.BackColor = Color.MistyRose;
                newPlayerLabelToolStripMenuItem.Text = "New player name:";
                newPlayerLabelToolStripMenuItem.Enabled = false;
            }
            else
            {
                newPlayerToolStripTextBox.BackColor = Color.LightGreen;
                newPlayerLabelToolStripMenuItem.Text = "Click or press Enter to add player.";
                newPlayerLabelToolStripMenuItem.Enabled = true;
            }
        }
        private void AddNewPlayer()
        {
            Records.PlayerInfo pi = Globals.Records.AddPlayer(newPlayerToolStripTextBox.Text);
            ToolStripMenuItem tsmi = new ToolStripMenuItem(newPlayerToolStripTextBox.Text, null, humanPlayerToolStripMenuItem_Click) { Checked = true, Enabled = false };
            PlayerMenuItems.Add(tsmi);

            newPlayerToolStripTextBox.Text = "";
            newPlayerToolStripTextBox.BackColor = Color.MistyRose;
            newToolStripMenuItem.HideDropDown();

            NewPlayer?.Invoke(this, new NewPlayerEventArgs(pi.PlayerName));
            Globals.Records.WriteRecords();
        }
        #endregion
        private void humanPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string PreviousPlayerName = playerToolStripDropDownButton.Text;
            ToolStripMenuItem tsmi = (ToolStripMenuItem)PlayerMenuItems.FirstOrDefault(pmi => pmi.Text == PreviousPlayerName);
            if (tsmi != null)
            {
                tsmi.Checked = false;
                tsmi.Enabled = true;
            }
            tsmi = (ToolStripMenuItem)sender;
            PlayerName = tsmi.Text;
            playerToolStripDropDownButton.Text = PlayerName;
            tsmi.Enabled = false;
            tsmi.Checked = true;
            ReplacePlayer?.Invoke(this, new ReplacePlayerEventArgs(PreviousPlayerName, PlayerName));
        }
        #endregion

        #region "Settings menu"
        private void settingsToolStripDropDownButton_DropDownOpening(object sender, EventArgs e)
        {
            removeFromGameToolStripMenuItem.Enabled = Globals.NamePlates.Count() > 1;

            addOpponentToolStripMenuItem.DropDownItems.Clear();
            bool separatorAdded = false;
            IEnumerable<Records.PlayerInfo> items;
            if (Records.Player(PlayerName).IsHuman)
            {
                items = Globals.Records.AllPlayers.Where(pi => pi.IsHuman).OrderBy(pi => pi.ID);
                foreach (Records.PlayerInfo item in items)
                {
                    addOpponentToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(item.PlayerName, null, addOpponent_Click)
                    {
                        Enabled = !Globals.NamePlates.Any(np => np.PlayerName==item.PlayerName),
                        Checked = item.PlayerName == PlayerName
                    });
                }
                addOpponentToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
                items = Globals.Records.AllPlayers.Where(pi => pi.IsAI).OrderBy(pi => pi.ID);
                foreach (Records.PlayerInfo item in items)
                {
                    addOpponentToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(item.PlayerName, null, addOpponent_Click)
                    { 
                        Enabled = !Globals.NamePlates.Any(np => np.PlayerName==item.PlayerName) 
                    });
                }
            }
            else
            {
                items = Globals.Records.AllPlayers.Where(pi => pi.IsAI).OrderBy(pi => pi.ID);
                foreach (Records.PlayerInfo item in items)
                {
                    addOpponentToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(item.PlayerName, null, addOpponent_Click)
                    {
                        Enabled = !Globals.NamePlates.Any(np => np.PlayerName==item.PlayerName),
                        Checked = item.PlayerName == PlayerName
                    });

                }
                addOpponentToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
                items = Globals.Records.AllPlayers.Where(pi => pi.IsHuman).OrderBy(pi => pi.ID);
                foreach (Records.PlayerInfo item in items)
                {
                    addOpponentToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(item.PlayerName, null, addOpponent_Click)
                    {
                        Enabled = !Globals.NamePlates.Any(np => np.PlayerName == item.PlayerName)
                    });
                }

            }
            //foreach (Records.PlayerInfo pi in Globals.Records.AllPlayers.Where(pi => !Globals.NamePlates.Any(np => np.PlayerName.Equals(pi.PlayerName, StringComparison.CurrentCultureIgnoreCase))).OrderBy(pi => pi.ID))
            //{
            //    if (!separatorAdded && pi.ID >=0)
            //    {
            //        addOpponentToolStripMenuItem.DropDownItems.Add(new ToolStripSeparator());
            //        separatorAdded = true;
            //    }
            //    addOpponentToolStripMenuItem.DropDownItems.Add(new ToolStripMenuItem(pi.PlayerName, null, addOpponent_Click));
            //}
            //addOpponentToolStripMenuItem.Enabled = !GameUnderway;
        }

        private void addOpponent_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            NewPlayer?.Invoke(this, new NewPlayerEventArgs(tsmi.Text));
        }

        private void removeFromGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemovePlayer?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        public override string ToString()
        {
            return $"PlayerControl PlayerName = {PlayerName}";
        }

        private void lvPerformance_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lvPerformance_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = false;
        }

        private const int pegRectInset = -4;
        private const int pegRectOffset = 2;
        private void lvPerformance_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == puzzleColumnHeader.Index)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                Rectangle pegRect = new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Height, e.Bounds.Height);
                pegRect.Inflate(pegRectInset, pegRectInset);
                for (int i = 0; i < e.SubItem.Text.Length; i++)
                {
                    using (Brush brush = new SolidBrush(Globals.ColorsInUse[Convert.ToInt32(e.SubItem.Text[i].ToString())]))
                    {
                        e.Graphics.FillEllipse(brush, pegRect);
                    }
                    using (Pen pen = new Pen(Color.Black, 0.25f) { Alignment = System.Drawing.Drawing2D.PenAlignment.Inset })
                    {
                        e.Graphics.DrawEllipse(Pens.Black, pegRect);
                    }
                    pegRect.Offset(pegRect.Width + pegRectOffset, 0);
                }
                e.DrawDefault = false;
            }
            else
            {
                e.DrawDefault = true;
            }
        }
    }
}
