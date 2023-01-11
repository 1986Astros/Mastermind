namespace MasterMind
{
    partial class ConsoleMM
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MasterMind.CurrentGame currentGame1 = new MasterMind.CurrentGame();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsoleMM));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.righthandedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lefthandedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.playerAtBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerAtTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.verticalTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.horizontalTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pegBoard1 = new MasterMind.PegBoard();
            this.cradle1 = new MasterMind.Cradle();
            this.acceptClearButtons1 = new MasterMind.AcceptClearButtons();
            this.tlpVerso = new System.Windows.Forms.TableLayoutPanel();
            this.gameflowControl1 = new MasterMind.GameflowControl();
            this.tlpCradleBoardCradle = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBoard = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelScorecards = new System.Windows.Forms.TableLayoutPanel();
            this.playerControl1 = new MasterMind.PlayerControl();
            this.panelScorecardOuter = new System.Windows.Forms.Panel();
            this.panelScorecardInner = new System.Windows.Forms.Panel();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.tlpVerso.SuspendLayout();
            this.tlpCradleBoardCradle.SuspendLayout();
            this.tlpBoard.SuspendLayout();
            this.tableLayoutPanelScorecards.SuspendLayout();
            this.panelScorecardOuter.SuspendLayout();
            this.panelScorecardInner.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1196, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.righthandedToolStripMenuItem,
            this.lefthandedToolStripMenuItem,
            this.toolStripMenuItem1,
            this.playerAtBottomToolStripMenuItem,
            this.playerAtTopToolStripMenuItem,
            this.toolStripMenuItem2,
            this.verticalTrayToolStripMenuItem,
            this.horizontalTrayToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // righthandedToolStripMenuItem
            // 
            this.righthandedToolStripMenuItem.Checked = true;
            this.righthandedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.righthandedToolStripMenuItem.Name = "righthandedToolStripMenuItem";
            this.righthandedToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.righthandedToolStripMenuItem.Text = "Right-handed";
            this.righthandedToolStripMenuItem.Click += new System.EventHandler(this.righthandedToolStripMenuItem_Click);
            // 
            // lefthandedToolStripMenuItem
            // 
            this.lefthandedToolStripMenuItem.Name = "lefthandedToolStripMenuItem";
            this.lefthandedToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.lefthandedToolStripMenuItem.Text = "Left-handed";
            this.lefthandedToolStripMenuItem.Click += new System.EventHandler(this.lefthandedToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
            // 
            // playerAtBottomToolStripMenuItem
            // 
            this.playerAtBottomToolStripMenuItem.Checked = true;
            this.playerAtBottomToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.playerAtBottomToolStripMenuItem.Name = "playerAtBottomToolStripMenuItem";
            this.playerAtBottomToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.playerAtBottomToolStripMenuItem.Text = "Player at bottom";
            this.playerAtBottomToolStripMenuItem.Click += new System.EventHandler(this.playerAtBottomToolStripMenuItem_Click);
            // 
            // playerAtTopToolStripMenuItem
            // 
            this.playerAtTopToolStripMenuItem.Name = "playerAtTopToolStripMenuItem";
            this.playerAtTopToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.playerAtTopToolStripMenuItem.Text = "Player at top";
            this.playerAtTopToolStripMenuItem.Click += new System.EventHandler(this.playerAtTopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(161, 6);
            // 
            // verticalTrayToolStripMenuItem
            // 
            this.verticalTrayToolStripMenuItem.Checked = true;
            this.verticalTrayToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.verticalTrayToolStripMenuItem.Name = "verticalTrayToolStripMenuItem";
            this.verticalTrayToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.verticalTrayToolStripMenuItem.Text = "Vertical cradle";
            this.verticalTrayToolStripMenuItem.Click += new System.EventHandler(this.verticalTrayToolStripMenuItem_Click);
            // 
            // horizontalTrayToolStripMenuItem
            // 
            this.horizontalTrayToolStripMenuItem.Name = "horizontalTrayToolStripMenuItem";
            this.horizontalTrayToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.horizontalTrayToolStripMenuItem.Text = "Horizontal cradle";
            this.horizontalTrayToolStripMenuItem.Click += new System.EventHandler(this.horizontalTrayToolStripMenuItem_Click);
            // 
            // pegBoard1
            // 
            this.pegBoard1.AllowDrop = true;
            this.pegBoard1.AutoSize = true;
            this.pegBoard1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pegBoard1.BackColor = System.Drawing.Color.BurlyWood;
            this.pegBoard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pegBoard1.CurrentGame = currentGame1;
            this.pegBoard1.Enabled = false;
            this.pegBoard1.Location = new System.Drawing.Point(31, 4);
            this.pegBoard1.Name = "pegBoard1";
            this.pegBoard1.ShowSolution = false;
            this.pegBoard1.Size = new System.Drawing.Size(192, 462);
            this.pegBoard1.TabIndex = 2;
            this.pegBoard1.GameOver += new MasterMind.PegBoard.GameOverEventHandler(this.PegBoard_GameOver);
            // 
            // cradle1
            // 
            this.cradle1.AllowDrop = true;
            this.cradle1.AutoSize = true;
            this.cradle1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cradle1.BackColor = System.Drawing.Color.SandyBrown;
            this.cradle1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cradle1.Enabled = false;
            this.cradle1.IsSource = true;
            this.cradle1.Location = new System.Drawing.Point(230, 226);
            this.cradle1.Name = "cradle1";
            this.cradle1.PegCount = 6;
            this.cradle1.PegDirection = MasterMind.Cradle.PegDirections.LeftToRight;
            this.cradle1.Size = new System.Drawing.Size(40, 240);
            this.cradle1.TabIndex = 3;
            // 
            // acceptClearButtons1
            // 
            this.acceptClearButtons1.AcceptEnabled = false;
            this.acceptClearButtons1.ClearEnabled = false;
            this.acceptClearButtons1.Location = new System.Drawing.Point(4, 4);
            this.acceptClearButtons1.Name = "acceptClearButtons1";
            this.acceptClearButtons1.Size = new System.Drawing.Size(20, 39);
            this.acceptClearButtons1.TabIndex = 4;
            // 
            // tlpVerso
            // 
            this.tlpVerso.AutoSize = true;
            this.tlpVerso.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpVerso.ColumnCount = 1;
            this.tlpVerso.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpVerso.Controls.Add(this.gameflowControl1, 0, 0);
            this.tlpVerso.Controls.Add(this.tlpCradleBoardCradle, 0, 1);
            this.tlpVerso.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpVerso.Location = new System.Drawing.Point(3, 3);
            this.tlpVerso.Name = "tlpVerso";
            this.tlpVerso.RowCount = 2;
            this.tlpVerso.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpVerso.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpVerso.Size = new System.Drawing.Size(288, 604);
            this.tlpVerso.TabIndex = 6;
            // 
            // gameflowControl1
            // 
            this.gameflowControl1.AutoSize = true;
            this.gameflowControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gameflowControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameflowControl1.Location = new System.Drawing.Point(3, 3);
            this.gameflowControl1.MessageType = MasterMind.GameflowControl.MessageTypes.Start;
            this.gameflowControl1.Name = "gameflowControl1";
            this.gameflowControl1.Size = new System.Drawing.Size(282, 112);
            this.gameflowControl1.TabIndex = 10;
            this.gameflowControl1.StartGame += new MasterMind.GameflowControl.StartEventHandler(this.gameflowControl1_StartGame);
            this.gameflowControl1.NextPlayer += new MasterMind.GameflowControl.NextPlayerHandler(this.gameflowControl1_NextPlayer);
            this.gameflowControl1.NextGame += new MasterMind.GameflowControl.NextGameEventHandler(this.gameflowControl1_NextGame);
            // 
            // tlpCradleBoardCradle
            // 
            this.tlpCradleBoardCradle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpCradleBoardCradle.AutoSize = true;
            this.tlpCradleBoardCradle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpCradleBoardCradle.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpCradleBoardCradle.ColumnCount = 1;
            this.tlpCradleBoardCradle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpCradleBoardCradle.Controls.Add(this.tlpBoard, 0, 1);
            this.tlpCradleBoardCradle.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpCradleBoardCradle.Location = new System.Drawing.Point(3, 121);
            this.tlpCradleBoardCradle.Name = "tlpCradleBoardCradle";
            this.tlpCradleBoardCradle.RowCount = 3;
            this.tlpCradleBoardCradle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCradleBoardCradle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCradleBoardCradle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCradleBoardCradle.Size = new System.Drawing.Size(282, 480);
            this.tlpCradleBoardCradle.TabIndex = 0;
            // 
            // tlpBoard
            // 
            this.tlpBoard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tlpBoard.AutoSize = true;
            this.tlpBoard.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpBoard.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpBoard.ColumnCount = 3;
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpBoard.Controls.Add(this.cradle1, 2, 0);
            this.tlpBoard.Controls.Add(this.pegBoard1, 1, 0);
            this.tlpBoard.Controls.Add(this.acceptClearButtons1, 0, 0);
            this.tlpBoard.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpBoard.Location = new System.Drawing.Point(4, 5);
            this.tlpBoard.Name = "tlpBoard";
            this.tlpBoard.RowCount = 1;
            this.tlpBoard.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpBoard.Size = new System.Drawing.Size(274, 470);
            this.tlpBoard.TabIndex = 0;
            // 
            // tableLayoutPanelScorecards
            // 
            this.tableLayoutPanelScorecards.AutoSize = true;
            this.tableLayoutPanelScorecards.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanelScorecards.ColumnCount = 1;
            this.tableLayoutPanelScorecards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelScorecards.Controls.Add(this.playerControl1, 0, 0);
            this.tableLayoutPanelScorecards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelScorecards.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelScorecards.Name = "tableLayoutPanelScorecards";
            this.tableLayoutPanelScorecards.RowCount = 1;
            this.tableLayoutPanelScorecards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelScorecards.Size = new System.Drawing.Size(376, 566);
            this.tableLayoutPanelScorecards.TabIndex = 2;
            this.tableLayoutPanelScorecards.Resize += new System.EventHandler(this.tableLayoutPanelScorecards_Resize);
            // 
            // playerControl1
            // 
            this.playerControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.playerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerControl1.Location = new System.Drawing.Point(3, 3);
            this.playerControl1.Name = "playerControl1";
            this.playerControl1.PlayerName = "";
            this.playerControl1.ShowPegboard = false;
            this.playerControl1.Size = new System.Drawing.Size(370, 560);
            this.playerControl1.TabIndex = 1;
            this.playerControl1.NewPlayer += new MasterMind.PlayerControl.NewPlayerEventHandler(this.playerControl_NewPlayer);
            this.playerControl1.ReplacePlayer += new MasterMind.PlayerControl.ReplacePlayerEventHandler(this.playerControl_ReplacePlayer);
            this.playerControl1.RemovePlayer += new MasterMind.PlayerControl.RemovePlayerEventHandler(this.playerControl_RemovePlayer);
            // 
            // panelScorecardOuter
            // 
            this.panelScorecardOuter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelScorecardOuter.AutoScroll = true;
            this.panelScorecardOuter.Controls.Add(this.panelScorecardInner);
            this.panelScorecardOuter.Location = new System.Drawing.Point(297, 3);
            this.panelScorecardOuter.Name = "panelScorecardOuter";
            this.panelScorecardOuter.Size = new System.Drawing.Size(896, 604);
            this.panelScorecardOuter.TabIndex = 7;
            // 
            // panelScorecardInner
            // 
            this.panelScorecardInner.AutoSize = true;
            this.panelScorecardInner.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelScorecardInner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelScorecardInner.Controls.Add(this.tableLayoutPanelScorecards);
            this.panelScorecardInner.Location = new System.Drawing.Point(3, 3);
            this.panelScorecardInner.Name = "panelScorecardInner";
            this.panelScorecardInner.Size = new System.Drawing.Size(378, 568);
            this.panelScorecardInner.TabIndex = 0;
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tlpVerso, 0, 0);
            this.tlpMain.Controls.Add(this.panelScorecardOuter, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 24);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(1196, 606);
            this.tlpMain.TabIndex = 8;
            // 
            // ConsoleMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 630);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ConsoleMM";
            this.Text = "MasterMind";
            this.Load += new System.EventHandler(this.ConsoleMM_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tlpVerso.ResumeLayout(false);
            this.tlpVerso.PerformLayout();
            this.tlpCradleBoardCradle.ResumeLayout(false);
            this.tlpCradleBoardCradle.PerformLayout();
            this.tlpBoard.ResumeLayout(false);
            this.tlpBoard.PerformLayout();
            this.tableLayoutPanelScorecards.ResumeLayout(false);
            this.panelScorecardOuter.ResumeLayout(false);
            this.panelScorecardOuter.PerformLayout();
            this.panelScorecardInner.ResumeLayout(false);
            this.panelScorecardInner.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem righthandedToolStripMenuItem;
        private ToolStripMenuItem lefthandedToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem playerAtBottomToolStripMenuItem;
        private ToolStripMenuItem playerAtTopToolStripMenuItem;
        private PegBoard pegBoard1;
        private Cradle cradle1;
        private AcceptClearButtons acceptClearButtons1;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem verticalTrayToolStripMenuItem;
        private ToolStripMenuItem horizontalTrayToolStripMenuItem;
        private TableLayoutPanel tlpVerso;
        private TableLayoutPanel tlpCradleBoardCradle;
        private TableLayoutPanel tlpBoard;
        private PlayerControl playerControl1;
        private TableLayoutPanel tableLayoutPanelScorecards;
        private Panel panelScorecardOuter;
        private Panel panelScorecardInner;
        private GameflowControl gameflowControl1;
        private TableLayoutPanel tlpMain;
    }
}