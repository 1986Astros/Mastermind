namespace MasterMind
{
    partial class GameflowControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpLosers = new System.Windows.Forms.TableLayoutPanel();
            this.lblNextGameLosers = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.tlpTie = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabelNextGameTie = new System.Windows.Forms.LinkLabel();
            this.lblTie = new System.Windows.Forms.Label();
            this.tlpYourTurn = new System.Windows.Forms.TableLayoutPanel();
            this.labelPlayerName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tlpNextPlayer = new System.Windows.Forms.TableLayoutPanel();
            this.lblLastTurnNextTurn = new System.Windows.Forms.Label();
            this.linkLabelNextPlayer = new System.Windows.Forms.LinkLabel();
            this.linkLabelSkipNextPlayer = new System.Windows.Forms.LinkLabel();
            this.tlpSoloGameOver = new System.Windows.Forms.TableLayoutPanel();
            this.lblSoloGame = new System.Windows.Forms.Label();
            this.linkLabelNextGameSolo = new System.Windows.Forms.LinkLabel();
            this.tlpWinner = new System.Windows.Forms.TableLayoutPanel();
            this.linkLabelNextGameWinner = new System.Windows.Forms.LinkLabel();
            this.lblWinner = new System.Windows.Forms.Label();
            this.panelStart = new System.Windows.Forms.Panel();
            this.linkLabelStart = new System.Windows.Forms.LinkLabel();
            this.linkLabel5 = new System.Windows.Forms.LinkLabel();
            this.tlpMain.SuspendLayout();
            this.tlpLosers.SuspendLayout();
            this.tlpTie.SuspendLayout();
            this.tlpYourTurn.SuspendLayout();
            this.tlpNextPlayer.SuspendLayout();
            this.tlpSoloGameOver.SuspendLayout();
            this.tlpWinner.SuspendLayout();
            this.panelStart.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpLosers, 0, 6);
            this.tlpMain.Controls.Add(this.tlpTie, 0, 5);
            this.tlpMain.Controls.Add(this.tlpYourTurn, 0, 1);
            this.tlpMain.Controls.Add(this.tlpNextPlayer, 0, 2);
            this.tlpMain.Controls.Add(this.tlpSoloGameOver, 0, 3);
            this.tlpMain.Controls.Add(this.tlpWinner, 0, 4);
            this.tlpMain.Controls.Add(this.panelStart, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 7;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Size = new System.Drawing.Size(270, 784);
            this.tlpMain.TabIndex = 0;
            // 
            // tlpLosers
            // 
            this.tlpLosers.ColumnCount = 1;
            this.tlpLosers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLosers.Controls.Add(this.lblNextGameLosers, 0, 1);
            this.tlpLosers.Controls.Add(this.label8, 0, 0);
            this.tlpLosers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLosers.Location = new System.Drawing.Point(0, 672);
            this.tlpLosers.Margin = new System.Windows.Forms.Padding(0);
            this.tlpLosers.Name = "tlpLosers";
            this.tlpLosers.RowCount = 2;
            this.tlpLosers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tlpLosers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpLosers.Size = new System.Drawing.Size(270, 112);
            this.tlpLosers.TabIndex = 7;
            // 
            // lblNextGameLosers
            // 
            this.lblNextGameLosers.AutoSize = true;
            this.lblNextGameLosers.BackColor = System.Drawing.Color.White;
            this.lblNextGameLosers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNextGameLosers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNextGameLosers.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNextGameLosers.Location = new System.Drawing.Point(0, 75);
            this.lblNextGameLosers.Margin = new System.Windows.Forms.Padding(0);
            this.lblNextGameLosers.Name = "lblNextGameLosers";
            this.lblNextGameLosers.Size = new System.Drawing.Size(270, 37);
            this.lblNextGameLosers.TabIndex = 3;
            this.lblNextGameLosers.TabStop = true;
            this.lblNextGameLosers.Text = "Next game";
            this.lblNextGameLosers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNextGameLosers.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblNextGameLosers_LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Black;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(0, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(270, 75);
            this.label8.TabIndex = 2;
            this.label8.Text = "No one solved the puzzle.";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpTie
            // 
            this.tlpTie.ColumnCount = 1;
            this.tlpTie.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTie.Controls.Add(this.linkLabelNextGameTie, 0, 1);
            this.tlpTie.Controls.Add(this.lblTie, 0, 0);
            this.tlpTie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTie.Location = new System.Drawing.Point(0, 560);
            this.tlpTie.Margin = new System.Windows.Forms.Padding(0);
            this.tlpTie.Name = "tlpTie";
            this.tlpTie.RowCount = 2;
            this.tlpTie.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tlpTie.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpTie.Size = new System.Drawing.Size(270, 112);
            this.tlpTie.TabIndex = 6;
            // 
            // linkLabelNextGameTie
            // 
            this.linkLabelNextGameTie.AutoSize = true;
            this.linkLabelNextGameTie.BackColor = System.Drawing.Color.White;
            this.linkLabelNextGameTie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkLabelNextGameTie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabelNextGameTie.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.linkLabelNextGameTie.Location = new System.Drawing.Point(0, 75);
            this.linkLabelNextGameTie.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabelNextGameTie.Name = "linkLabelNextGameTie";
            this.linkLabelNextGameTie.Size = new System.Drawing.Size(270, 37);
            this.linkLabelNextGameTie.TabIndex = 3;
            this.linkLabelNextGameTie.TabStop = true;
            this.linkLabelNextGameTie.Text = "Next game";
            this.linkLabelNextGameTie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelNextGameTie.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelNextGameTie_LinkClicked);
            // 
            // lblTie
            // 
            this.lblTie.AutoSize = true;
            this.lblTie.BackColor = System.Drawing.Color.Black;
            this.lblTie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTie.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTie.ForeColor = System.Drawing.Color.White;
            this.lblTie.Location = new System.Drawing.Point(0, 0);
            this.lblTie.Margin = new System.Windows.Forms.Padding(0);
            this.lblTie.Name = "lblTie";
            this.lblTie.Size = new System.Drawing.Size(270, 75);
            this.lblTie.TabIndex = 2;
            this.lblTie.Tag = "Tied at {0}: {1}";
            this.lblTie.Text = " Tied at 10 turns: Opossum, Boris, Renaldo";
            this.lblTie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpYourTurn
            // 
            this.tlpYourTurn.ColumnCount = 1;
            this.tlpYourTurn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpYourTurn.Controls.Add(this.labelPlayerName, 0, 0);
            this.tlpYourTurn.Controls.Add(this.label2, 0, 1);
            this.tlpYourTurn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpYourTurn.Location = new System.Drawing.Point(0, 112);
            this.tlpYourTurn.Margin = new System.Windows.Forms.Padding(0);
            this.tlpYourTurn.Name = "tlpYourTurn";
            this.tlpYourTurn.RowCount = 2;
            this.tlpYourTurn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpYourTurn.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpYourTurn.Size = new System.Drawing.Size(270, 112);
            this.tlpYourTurn.TabIndex = 1;
            // 
            // labelPlayerName
            // 
            this.labelPlayerName.AutoSize = true;
            this.labelPlayerName.BackColor = System.Drawing.Color.White;
            this.labelPlayerName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPlayerName.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelPlayerName.ForeColor = System.Drawing.Color.Black;
            this.labelPlayerName.Location = new System.Drawing.Point(0, 0);
            this.labelPlayerName.Margin = new System.Windows.Forms.Padding(0);
            this.labelPlayerName.Name = "labelPlayerName";
            this.labelPlayerName.Size = new System.Drawing.Size(270, 77);
            this.labelPlayerName.TabIndex = 0;
            this.labelPlayerName.Text = "Opossum";
            this.labelPlayerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(94, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "It\'s your turn.";
            // 
            // tlpNextPlayer
            // 
            this.tlpNextPlayer.ColumnCount = 1;
            this.tlpNextPlayer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNextPlayer.Controls.Add(this.lblLastTurnNextTurn, 0, 0);
            this.tlpNextPlayer.Controls.Add(this.linkLabelNextPlayer, 0, 1);
            this.tlpNextPlayer.Controls.Add(this.linkLabelSkipNextPlayer, 0, 2);
            this.tlpNextPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpNextPlayer.Location = new System.Drawing.Point(0, 224);
            this.tlpNextPlayer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpNextPlayer.Name = "tlpNextPlayer";
            this.tlpNextPlayer.RowCount = 3;
            this.tlpNextPlayer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNextPlayer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpNextPlayer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpNextPlayer.Size = new System.Drawing.Size(270, 112);
            this.tlpNextPlayer.TabIndex = 2;
            // 
            // lblLastTurnNextTurn
            // 
            this.lblLastTurnNextTurn.AutoSize = true;
            this.lblLastTurnNextTurn.BackColor = System.Drawing.Color.Black;
            this.lblLastTurnNextTurn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLastTurnNextTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblLastTurnNextTurn.ForeColor = System.Drawing.Color.White;
            this.lblLastTurnNextTurn.Location = new System.Drawing.Point(0, 0);
            this.lblLastTurnNextTurn.Margin = new System.Windows.Forms.Padding(0);
            this.lblLastTurnNextTurn.Name = "lblLastTurnNextTurn";
            this.lblLastTurnNextTurn.Size = new System.Drawing.Size(270, 45);
            this.lblLastTurnNextTurn.TabIndex = 0;
            this.lblLastTurnNextTurn.Tag = "{0} solved the puzzle in {1}.|{0} did not solve the puzzle.";
            this.lblLastTurnNextTurn.Text = "Opossum solved the puzzle in 10 turns.";
            this.lblLastTurnNextTurn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabelNextPlayer
            // 
            this.linkLabelNextPlayer.AutoSize = true;
            this.linkLabelNextPlayer.BackColor = System.Drawing.Color.White;
            this.linkLabelNextPlayer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkLabelNextPlayer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabelNextPlayer.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.linkLabelNextPlayer.LinkArea = new System.Windows.Forms.LinkArea(13, 5);
            this.linkLabelNextPlayer.Location = new System.Drawing.Point(0, 45);
            this.linkLabelNextPlayer.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabelNextPlayer.Name = "linkLabelNextPlayer";
            this.linkLabelNextPlayer.Size = new System.Drawing.Size(270, 45);
            this.linkLabelNextPlayer.TabIndex = 1;
            this.linkLabelNextPlayer.TabStop = true;
            this.linkLabelNextPlayer.Tag = "Next player: {0}";
            this.linkLabelNextPlayer.Text = "Next player: Boris";
            this.linkLabelNextPlayer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelNextPlayer.UseCompatibleTextRendering = true;
            this.linkLabelNextPlayer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblNextPlayer_LinkClicked);
            // 
            // linkLabelSkipNextPlayer
            // 
            this.linkLabelSkipNextPlayer.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.linkLabelSkipNextPlayer.AutoSize = true;
            this.linkLabelSkipNextPlayer.LinkArea = new System.Windows.Forms.LinkArea(0, 4);
            this.linkLabelSkipNextPlayer.Location = new System.Drawing.Point(194, 90);
            this.linkLabelSkipNextPlayer.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabelSkipNextPlayer.Name = "linkLabelSkipNextPlayer";
            this.linkLabelSkipNextPlayer.Size = new System.Drawing.Size(76, 21);
            this.linkLabelSkipNextPlayer.TabIndex = 2;
            this.linkLabelSkipNextPlayer.TabStop = true;
            this.linkLabelSkipNextPlayer.Text = "Skip player ...";
            this.linkLabelSkipNextPlayer.UseCompatibleTextRendering = true;
            this.linkLabelSkipNextPlayer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSkipNextPlayer_LinkClicked);
            // 
            // tlpSoloGameOver
            // 
            this.tlpSoloGameOver.ColumnCount = 1;
            this.tlpSoloGameOver.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSoloGameOver.Controls.Add(this.lblSoloGame, 0, 0);
            this.tlpSoloGameOver.Controls.Add(this.linkLabelNextGameSolo, 0, 1);
            this.tlpSoloGameOver.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSoloGameOver.Location = new System.Drawing.Point(0, 336);
            this.tlpSoloGameOver.Margin = new System.Windows.Forms.Padding(0);
            this.tlpSoloGameOver.Name = "tlpSoloGameOver";
            this.tlpSoloGameOver.RowCount = 2;
            this.tlpSoloGameOver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tlpSoloGameOver.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpSoloGameOver.Size = new System.Drawing.Size(270, 112);
            this.tlpSoloGameOver.TabIndex = 3;
            // 
            // lblSoloGame
            // 
            this.lblSoloGame.AutoSize = true;
            this.lblSoloGame.BackColor = System.Drawing.Color.Black;
            this.lblSoloGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSoloGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSoloGame.ForeColor = System.Drawing.Color.White;
            this.lblSoloGame.Location = new System.Drawing.Point(0, 0);
            this.lblSoloGame.Margin = new System.Windows.Forms.Padding(0);
            this.lblSoloGame.Name = "lblSoloGame";
            this.lblSoloGame.Size = new System.Drawing.Size(270, 75);
            this.lblSoloGame.TabIndex = 1;
            this.lblSoloGame.Tag = "{0} solved the puzzle in {1}.|{0} did not solve the puzzle.";
            this.lblSoloGame.Text = "Opossum solved the puzzle in 10 turns.";
            this.lblSoloGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // linkLabelNextGameSolo
            // 
            this.linkLabelNextGameSolo.AutoSize = true;
            this.linkLabelNextGameSolo.BackColor = System.Drawing.Color.White;
            this.linkLabelNextGameSolo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkLabelNextGameSolo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabelNextGameSolo.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.linkLabelNextGameSolo.Location = new System.Drawing.Point(0, 75);
            this.linkLabelNextGameSolo.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabelNextGameSolo.Name = "linkLabelNextGameSolo";
            this.linkLabelNextGameSolo.Size = new System.Drawing.Size(270, 37);
            this.linkLabelNextGameSolo.TabIndex = 2;
            this.linkLabelNextGameSolo.TabStop = true;
            this.linkLabelNextGameSolo.Text = "Next game";
            this.linkLabelNextGameSolo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelNextGameSolo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelNextGameSolo_LinkClicked);
            // 
            // tlpWinner
            // 
            this.tlpWinner.ColumnCount = 1;
            this.tlpWinner.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpWinner.Controls.Add(this.linkLabelNextGameWinner, 0, 1);
            this.tlpWinner.Controls.Add(this.lblWinner, 0, 0);
            this.tlpWinner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpWinner.Location = new System.Drawing.Point(0, 448);
            this.tlpWinner.Margin = new System.Windows.Forms.Padding(0);
            this.tlpWinner.Name = "tlpWinner";
            this.tlpWinner.RowCount = 2;
            this.tlpWinner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67F));
            this.tlpWinner.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tlpWinner.Size = new System.Drawing.Size(270, 112);
            this.tlpWinner.TabIndex = 5;
            // 
            // linkLabelNextGameWinner
            // 
            this.linkLabelNextGameWinner.AutoSize = true;
            this.linkLabelNextGameWinner.BackColor = System.Drawing.Color.White;
            this.linkLabelNextGameWinner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.linkLabelNextGameWinner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabelNextGameWinner.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.linkLabelNextGameWinner.Location = new System.Drawing.Point(0, 75);
            this.linkLabelNextGameWinner.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabelNextGameWinner.Name = "linkLabelNextGameWinner";
            this.linkLabelNextGameWinner.Size = new System.Drawing.Size(270, 37);
            this.linkLabelNextGameWinner.TabIndex = 3;
            this.linkLabelNextGameWinner.TabStop = true;
            this.linkLabelNextGameWinner.Text = "Next game";
            this.linkLabelNextGameWinner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelNextGameWinner.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelNextGameWinner_LinkClicked);
            // 
            // lblWinner
            // 
            this.lblWinner.AutoSize = true;
            this.lblWinner.BackColor = System.Drawing.Color.Black;
            this.lblWinner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWinner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWinner.ForeColor = System.Drawing.Color.White;
            this.lblWinner.Location = new System.Drawing.Point(0, 0);
            this.lblWinner.Margin = new System.Windows.Forms.Padding(0);
            this.lblWinner.Name = "lblWinner";
            this.lblWinner.Size = new System.Drawing.Size(270, 75);
            this.lblWinner.TabIndex = 2;
            this.lblWinner.Tag = "{0} won in {1}.";
            this.lblWinner.Text = "Opossum won in 10 turns.";
            this.lblWinner.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelStart
            // 
            this.panelStart.BackColor = System.Drawing.Color.LimeGreen;
            this.panelStart.Controls.Add(this.linkLabelStart);
            this.panelStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStart.Location = new System.Drawing.Point(0, 0);
            this.panelStart.Margin = new System.Windows.Forms.Padding(0);
            this.panelStart.Name = "panelStart";
            this.panelStart.Size = new System.Drawing.Size(270, 112);
            this.panelStart.TabIndex = 8;
            // 
            // linkLabelStart
            // 
            this.linkLabelStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabelStart.Font = new System.Drawing.Font("Cooper Black", 24F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            this.linkLabelStart.ForeColor = System.Drawing.Color.White;
            this.linkLabelStart.LinkColor = System.Drawing.Color.White;
            this.linkLabelStart.Location = new System.Drawing.Point(0, 0);
            this.linkLabelStart.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabelStart.Name = "linkLabelStart";
            this.linkLabelStart.Size = new System.Drawing.Size(270, 112);
            this.linkLabelStart.TabIndex = 4;
            this.linkLabelStart.TabStop = true;
            this.linkLabelStart.Text = "Start";
            this.linkLabelStart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabelStart.VisitedLinkColor = System.Drawing.Color.White;
            this.linkLabelStart.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelStart_LinkClicked);
            // 
            // linkLabel5
            // 
            this.linkLabel5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLabel5.AutoSize = true;
            this.linkLabel5.Location = new System.Drawing.Point(68, 57);
            this.linkLabel5.Margin = new System.Windows.Forms.Padding(0);
            this.linkLabel5.Name = "linkLabel5";
            this.linkLabel5.Size = new System.Drawing.Size(64, 15);
            this.linkLabel5.TabIndex = 2;
            this.linkLabel5.TabStop = true;
            this.linkLabel5.Text = "Next game";
            // 
            // GameflowControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tlpMain);
            this.Name = "GameflowControl";
            this.Size = new System.Drawing.Size(270, 784);
            this.tlpMain.ResumeLayout(false);
            this.tlpLosers.ResumeLayout(false);
            this.tlpLosers.PerformLayout();
            this.tlpTie.ResumeLayout(false);
            this.tlpTie.PerformLayout();
            this.tlpYourTurn.ResumeLayout(false);
            this.tlpYourTurn.PerformLayout();
            this.tlpNextPlayer.ResumeLayout(false);
            this.tlpNextPlayer.PerformLayout();
            this.tlpSoloGameOver.ResumeLayout(false);
            this.tlpSoloGameOver.PerformLayout();
            this.tlpWinner.ResumeLayout(false);
            this.tlpWinner.PerformLayout();
            this.panelStart.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel tlpMain;
        private TableLayoutPanel tlpYourTurn;
        private Label labelPlayerName;
        private Label label2;
        private TableLayoutPanel tlpNextPlayer;
        private Label lblLastTurnNextTurn;
        private LinkLabel lblNextPlayer;
        private LinkLabel linkLabelSkipNextPlayer;
        private TableLayoutPanel tlpSoloGameOver;
        private Label lblSoloGame;
        private LinkLabel linkLabelNextGameSolo;
        private LinkLabel linkLabelStart;
        private TableLayoutPanel tlpLosers;
        private LinkLabel lblNextGameLosers;
        private Label label8;
        private TableLayoutPanel tlpTie;
        private LinkLabel linkLabelNextGameTie;
        private Label lblTie;
        private TableLayoutPanel tlpWinner;
        private LinkLabel linkLabelNextGameWinner;
        private Label lblWinner;
        private LinkLabel linkLabel5;
        private Panel panelStart;
        private LinkLabel linkLabelNextPlayer;
    }
}
