using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MasterMind
{
    public partial class GameflowControl : UserControl
    {
        public GameflowControl()
        {
            InitializeComponent();
            ExpandCollapse(panelStart);
        }

        #region "event definitions"
        public delegate void StartEventHandler(object sender, EventArgs e);
        public event StartEventHandler StartGame;
        public class NextPlayerEventArgs : EventArgs
        {
            public NextPlayerEventArgs(bool skipNextPlayer) : base()
            {
                SkipNextPlayer = skipNextPlayer;
            }
            public bool SkipNextPlayer { get; }
        }
        public delegate void NextPlayerHandler(object sender, NextPlayerEventArgs e);
        public event NextPlayerHandler NextPlayer;
        public class NextGameEventArgs : EventArgs
        {
            public NextGameEventArgs(MessageTypes messageType) : base()
            {
                MessageType = messageType;
            }
            public MessageTypes MessageType { get; }
        }
        public delegate void NextGameEventHandler(object sender, NextGameEventArgs e);
        public event NextGameEventHandler NextGame;
        #endregion

        #region "click handlers"
        private void linkLabelStart_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StartGame.Invoke(this, EventArgs.Empty);
        }
        private void lblNextPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NextPlayer.Invoke(this, new NextPlayerEventArgs(false));
        }
        private void linkLabelSkipNextPlayer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show($"Are you sure you want to skip {(string)(linkLabelNextPlayer.Tag)}?","Skip a player",MessageBoxButtons.YesNo,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                NextPlayer.Invoke(this, new NextPlayerEventArgs(true));
            }
        }
        private void linkLabelNextGameSolo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NextGame.Invoke(this, new NextGameEventArgs(MessageTypes.SoloGameOver));
        }
        private void linkLabelNextGameWinner_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NextGame.Invoke(this, new NextGameEventArgs(MessageTypes.MultiPlayerWinner));
        }
        private void linkLabelNextGameTie_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NextGame.Invoke(this, new NextGameEventArgs(MessageTypes.MultiPlayerTie));
        }
        private void lblNextGameLosers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NextGame.Invoke(this, new NextGameEventArgs(MessageTypes.MultiPlayerLosers));
        }
        #endregion

        #region "MessageType"
        public enum MessageTypes
        {
            Start,
            YourTurn,
            NextHuman,
            SoloGameOver,
            MultiPlayerWinner,
            MultiPlayerTie,
            MultiPlayerLosers
        }
        public MessageTypes MessageType
        {
            get
            {
                return shhMessageType;
            }
            set
            {
                if (value != shhMessageType)
                {
                    shhMessageType = value;
                    switch (shhMessageType)
                    {
                        case MessageTypes.Start:
                            ExpandCollapse(panelStart);
                            break;
                        case MessageTypes.YourTurn:
                            ExpandCollapse(tlpYourTurn);
                            break;
                        case MessageTypes.NextHuman:
                            ExpandCollapse(tlpNextPlayer);
                            break;
                        case MessageTypes.SoloGameOver:
                            ExpandCollapse(tlpSoloGameOver);
                            break;
                        case MessageTypes.MultiPlayerWinner:
                            ExpandCollapse(tlpWinner);
                            break;
                        case MessageTypes.MultiPlayerTie:
                            ExpandCollapse(tlpTie);
                            break;
                        case MessageTypes.MultiPlayerLosers:
                            ExpandCollapse(tlpLosers);
                            break;
                    }
                }
            }
        }
        private MessageTypes shhMessageType = MessageTypes.Start;

        public void Start()
        {
            MessageType = MessageTypes.Start;
        }
        public void YourTurn(string thisPlayer)
        {
            labelPlayerName.Text = thisPlayer;
            MessageType = MessageTypes.YourTurn;
        }
        public void NextHuman(string lastPlayer, bool Solved, int Turns, string nextPlayer)
        {
            string msgTemplate;
            if (Solved)
            {
                msgTemplate = ((string)(lblLastTurnNextTurn.Tag)).Split('|')[0];
            }
            else
            {
                msgTemplate = ((string)(lblLastTurnNextTurn.Tag)).Split('|')[1];
            }
            lblLastTurnNextTurn.Text = String.Format(msgTemplate,lastPlayer,Solved ? ((Turns == 1) ? "1 turn" : $"{Turns} turns") : "");

            linkLabelNextPlayer.Text = String.Format(((string)(linkLabelNextPlayer.Tag)), nextPlayer);
            linkLabelNextPlayer.Tag = nextPlayer;
            linkLabelNextPlayer.Links[0].Length = nextPlayer.Length;

            MessageType = MessageTypes.NextHuman;
        }
        public void SoloGameOver(string lastPlayer, bool Solved, int Turns)
        {
            string msgTemplate;
            if (Solved)
            {
                msgTemplate = ((string)(lblSoloGame.Tag)).Split('|')[0];
            }
            else
            {
                msgTemplate = ((string)(lblSoloGame.Tag)).Split('|')[1];

            }
            lblSoloGame.Text = String.Format(msgTemplate, lastPlayer, Solved ? ((Turns == 1) ? "1 turn" : $"{Turns} turns") : "");
            MessageType = MessageTypes.SoloGameOver;
        }
        public void MultiPlayerWinner(string lastPlayer, int Turns)
        {
            lblWinner.Text = String.Format(((string)(lblWinner.Tag)), lastPlayer, (Turns == 1) ? "1 turn" : $"{Turns} turns");
            MessageType = MessageTypes.MultiPlayerWinner;
        }
        public void MultiPlayerTie(IEnumerable<string> players, int Turns)
        {
            lblTie.Text = String.Format(((string)(lblTie.Tag)), (Turns == 1) ? "1 turn" : $"{Turns} turns", players.Skip(1).Aggregate(players.ElementAt(0), (ag, pn) => $"{ag}, {pn}"));
            MessageType = MessageTypes.MultiPlayerTie;
        }
        public void MultiPlayerLosers()
        {
            MessageType = MessageTypes.MultiPlayerLosers;
        }
        private void ExpandCollapse(Control expandedPanel)
        {
            tlpMain.SuspendLayout();
            int rowIndex;
            foreach (Control c in tlpMain.Controls)
            {
                rowIndex = tlpMain.GetPositionFromControl(c).Row;
                if (c == expandedPanel)
                {
                    tlpMain.RowStyles[rowIndex] = new RowStyle(SizeType.Percent, 100);
                }
                else
                {
                    tlpMain.RowStyles[rowIndex] = new RowStyle(SizeType.Absolute, 0);
                }
            }
            tlpMain.ResumeLayout();
            tlpMain.PerformLayout();
        }
        #endregion
    }
}
