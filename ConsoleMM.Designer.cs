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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsoleMM));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.startGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gamePlayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colors4Pegs10Turns30PointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medium100PointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.difficult30PointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.difficult100PointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.humansSamePuzzlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.humansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vsComputerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renaldoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.úrsulaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.andrésToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tatiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pepitoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tlpCradleBoardCradle = new System.Windows.Forms.TableLayoutPanel();
            this.tlpBoard = new System.Windows.Forms.TableLayoutPanel();
            this.playerControl1 = new MasterMind.PlayerControl();
            this.menuStrip1.SuspendLayout();
            this.tlpMain.SuspendLayout();
            this.tlpCradleBoardCradle.SuspendLayout();
            this.tlpBoard.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startGameToolStripMenuItem,
            this.gamePlayToolStripMenuItem,
            this.playersToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1196, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // startGameToolStripMenuItem
            // 
            this.startGameToolStripMenuItem.Name = "startGameToolStripMenuItem";
            this.startGameToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.startGameToolStripMenuItem.Text = "Start game";
            this.startGameToolStripMenuItem.Click += new System.EventHandler(this.startGameToolStripMenuItem_Click);
            // 
            // gamePlayToolStripMenuItem
            // 
            this.gamePlayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.colors4Pegs10Turns30PointsToolStripMenuItem,
            this.medium100PointsToolStripMenuItem,
            this.difficult30PointsToolStripMenuItem,
            this.difficult100PointsToolStripMenuItem,
            this.customToolStripMenuItem});
            this.gamePlayToolStripMenuItem.Name = "gamePlayToolStripMenuItem";
            this.gamePlayToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.gamePlayToolStripMenuItem.Text = "Game play";
            // 
            // colors4Pegs10Turns30PointsToolStripMenuItem
            // 
            this.colors4Pegs10Turns30PointsToolStripMenuItem.Checked = true;
            this.colors4Pegs10Turns30PointsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.colors4Pegs10Turns30PointsToolStripMenuItem.Enabled = false;
            this.colors4Pegs10Turns30PointsToolStripMenuItem.Name = "colors4Pegs10Turns30PointsToolStripMenuItem";
            this.colors4Pegs10Turns30PointsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.colors4Pegs10Turns30PointsToolStripMenuItem.Text = "Medium - 30 points";
            this.colors4Pegs10Turns30PointsToolStripMenuItem.ToolTipText = "6 colors, 4 pegs, 10 turns";
            // 
            // medium100PointsToolStripMenuItem
            // 
            this.medium100PointsToolStripMenuItem.Enabled = false;
            this.medium100PointsToolStripMenuItem.Name = "medium100PointsToolStripMenuItem";
            this.medium100PointsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.medium100PointsToolStripMenuItem.Text = "Medium - 100 points";
            this.medium100PointsToolStripMenuItem.ToolTipText = "6 colors, 4 pegs, 10 turns";
            // 
            // difficult30PointsToolStripMenuItem
            // 
            this.difficult30PointsToolStripMenuItem.Enabled = false;
            this.difficult30PointsToolStripMenuItem.Name = "difficult30PointsToolStripMenuItem";
            this.difficult30PointsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.difficult30PointsToolStripMenuItem.Text = "Difficult - 30 points";
            this.difficult30PointsToolStripMenuItem.ToolTipText = "8 colors, 5 pegs, 12 turns";
            // 
            // difficult100PointsToolStripMenuItem
            // 
            this.difficult100PointsToolStripMenuItem.Enabled = false;
            this.difficult100PointsToolStripMenuItem.Name = "difficult100PointsToolStripMenuItem";
            this.difficult100PointsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.difficult100PointsToolStripMenuItem.Text = "Difficult - 100 points";
            this.difficult100PointsToolStripMenuItem.ToolTipText = "8 colors, 5 pegs, 12 turns";
            // 
            // customToolStripMenuItem
            // 
            this.customToolStripMenuItem.Enabled = false;
            this.customToolStripMenuItem.Name = "customToolStripMenuItem";
            this.customToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.customToolStripMenuItem.Text = "Custom";
            this.customToolStripMenuItem.ToolTipText = "Customize for children or experts";
            // 
            // playersToolStripMenuItem
            // 
            this.playersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soloToolStripMenuItem,
            this.humansSamePuzzlesToolStripMenuItem,
            this.humansToolStripMenuItem,
            this.vsComputerToolStripMenuItem});
            this.playersToolStripMenuItem.Name = "playersToolStripMenuItem";
            this.playersToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.playersToolStripMenuItem.Text = "Players";
            // 
            // soloToolStripMenuItem
            // 
            this.soloToolStripMenuItem.Checked = true;
            this.soloToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.soloToolStripMenuItem.Name = "soloToolStripMenuItem";
            this.soloToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.soloToolStripMenuItem.Text = "Solo";
            // 
            // humansSamePuzzlesToolStripMenuItem
            // 
            this.humansSamePuzzlesToolStripMenuItem.Enabled = false;
            this.humansSamePuzzlesToolStripMenuItem.Name = "humansSamePuzzlesToolStripMenuItem";
            this.humansSamePuzzlesToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.humansSamePuzzlesToolStripMenuItem.Text = "Humans - computer is maker";
            this.humansSamePuzzlesToolStripMenuItem.Click += new System.EventHandler(this.humansSamePuzzlesToolStripMenuItem_Click);
            // 
            // humansToolStripMenuItem
            // 
            this.humansToolStripMenuItem.Enabled = false;
            this.humansToolStripMenuItem.Name = "humansToolStripMenuItem";
            this.humansToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.humansToolStripMenuItem.Text = "Humans - makers and breakers";
            // 
            // vsComputerToolStripMenuItem
            // 
            this.vsComputerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renaldoToolStripMenuItem,
            this.úrsulaToolStripMenuItem,
            this.andrésToolStripMenuItem,
            this.tatiToolStripMenuItem,
            this.pepitoToolStripMenuItem});
            this.vsComputerToolStripMenuItem.Name = "vsComputerToolStripMenuItem";
            this.vsComputerToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.vsComputerToolStripMenuItem.Text = "Vs. Computer";
            // 
            // renaldoToolStripMenuItem
            // 
            this.renaldoToolStripMenuItem.Enabled = false;
            this.renaldoToolStripMenuItem.Name = "renaldoToolStripMenuItem";
            this.renaldoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.renaldoToolStripMenuItem.Text = "Renaldo";
            this.renaldoToolStripMenuItem.Click += new System.EventHandler(this.renaldoToolStripMenuItem_Click);
            // 
            // úrsulaToolStripMenuItem
            // 
            this.úrsulaToolStripMenuItem.Enabled = false;
            this.úrsulaToolStripMenuItem.Name = "úrsulaToolStripMenuItem";
            this.úrsulaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.úrsulaToolStripMenuItem.Text = "Úrsula";
            this.úrsulaToolStripMenuItem.Click += new System.EventHandler(this.úrsulaToolStripMenuItem_Click);
            // 
            // andrésToolStripMenuItem
            // 
            this.andrésToolStripMenuItem.Enabled = false;
            this.andrésToolStripMenuItem.Name = "andrésToolStripMenuItem";
            this.andrésToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.andrésToolStripMenuItem.Text = "Andrés";
            this.andrésToolStripMenuItem.Click += new System.EventHandler(this.andrésToolStripMenuItem_Click);
            // 
            // tatiToolStripMenuItem
            // 
            this.tatiToolStripMenuItem.Enabled = false;
            this.tatiToolStripMenuItem.Name = "tatiToolStripMenuItem";
            this.tatiToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.tatiToolStripMenuItem.Text = "Tati";
            this.tatiToolStripMenuItem.Click += new System.EventHandler(this.tatiToolStripMenuItem_Click);
            // 
            // pepitoToolStripMenuItem
            // 
            this.pepitoToolStripMenuItem.Enabled = false;
            this.pepitoToolStripMenuItem.Name = "pepitoToolStripMenuItem";
            this.pepitoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pepitoToolStripMenuItem.Text = "Pepito";
            this.pepitoToolStripMenuItem.Click += new System.EventHandler(this.pepitoToolStripMenuItem_Click);
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
            this.pegBoard1.Enabled = false;
            this.pegBoard1.Location = new System.Drawing.Point(29, 3);
            this.pegBoard1.Name = "pegBoard1";
            this.pegBoard1.Size = new System.Drawing.Size(192, 462);
            this.pegBoard1.TabIndex = 2;
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
            this.cradle1.Location = new System.Drawing.Point(227, 225);
            this.cradle1.Name = "cradle1";
            this.cradle1.PegCount = 6;
            this.cradle1.Size = new System.Drawing.Size(40, 240);
            this.cradle1.TabIndex = 3;
            // 
            // acceptClearButtons1
            // 
            this.acceptClearButtons1.AcceptEnabled = false;
            this.acceptClearButtons1.ClearEnabled = false;
            this.acceptClearButtons1.Location = new System.Drawing.Point(3, 3);
            this.acceptClearButtons1.Name = "acceptClearButtons1";
            this.acceptClearButtons1.Size = new System.Drawing.Size(20, 39);
            this.acceptClearButtons1.TabIndex = 4;
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.tlpCradleBoardCradle, 0, 0);
            this.tlpMain.Controls.Add(this.playerControl1, 1, 0);
            this.tlpMain.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpMain.Location = new System.Drawing.Point(12, 37);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.Size = new System.Drawing.Size(638, 480);
            this.tlpMain.TabIndex = 6;
            // 
            // tlpCradleBoardCradle
            // 
            this.tlpCradleBoardCradle.AutoSize = true;
            this.tlpCradleBoardCradle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpCradleBoardCradle.ColumnCount = 1;
            this.tlpCradleBoardCradle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpCradleBoardCradle.Controls.Add(this.tlpBoard, 0, 1);
            this.tlpCradleBoardCradle.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpCradleBoardCradle.Location = new System.Drawing.Point(3, 3);
            this.tlpCradleBoardCradle.Name = "tlpCradleBoardCradle";
            this.tlpCradleBoardCradle.RowCount = 3;
            this.tlpCradleBoardCradle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCradleBoardCradle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCradleBoardCradle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpCradleBoardCradle.Size = new System.Drawing.Size(276, 474);
            this.tlpCradleBoardCradle.TabIndex = 0;
            // 
            // tlpBoard
            // 
            this.tlpBoard.AutoSize = true;
            this.tlpBoard.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpBoard.ColumnCount = 3;
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpBoard.Controls.Add(this.cradle1, 2, 0);
            this.tlpBoard.Controls.Add(this.pegBoard1, 1, 0);
            this.tlpBoard.Controls.Add(this.acceptClearButtons1, 0, 0);
            this.tlpBoard.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tlpBoard.Location = new System.Drawing.Point(3, 3);
            this.tlpBoard.Name = "tlpBoard";
            this.tlpBoard.RowCount = 1;
            this.tlpBoard.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpBoard.Size = new System.Drawing.Size(270, 468);
            this.tlpBoard.TabIndex = 0;
            // 
            // playerControl1
            // 
            this.playerControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.playerControl1.Location = new System.Drawing.Point(285, 3);
            this.playerControl1.Name = "playerControl1";
            this.playerControl1.PlayerName = "";
            this.playerControl1.Size = new System.Drawing.Size(350, 474);
            this.playerControl1.TabIndex = 1;
            // 
            // ConsoleMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1196, 659);
            this.Controls.Add(this.tlpMain);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ConsoleMM";
            this.Text = "MasterMind";
            this.Load += new System.EventHandler(this.ConsoleMM_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpCradleBoardCradle.ResumeLayout(false);
            this.tlpCradleBoardCradle.PerformLayout();
            this.tlpBoard.ResumeLayout(false);
            this.tlpBoard.PerformLayout();
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
        private ToolStripMenuItem startGameToolStripMenuItem;
        private ToolStripMenuItem gamePlayToolStripMenuItem;
        private ToolStripMenuItem colors4Pegs10Turns30PointsToolStripMenuItem;
        private ToolStripMenuItem medium100PointsToolStripMenuItem;
        private ToolStripMenuItem difficult30PointsToolStripMenuItem;
        private ToolStripMenuItem difficult100PointsToolStripMenuItem;
        private ToolStripMenuItem customToolStripMenuItem;
        private ToolStripMenuItem playersToolStripMenuItem;
        private ToolStripMenuItem soloToolStripMenuItem;
        private ToolStripMenuItem humansSamePuzzlesToolStripMenuItem;
        private ToolStripMenuItem humansToolStripMenuItem;
        private ToolStripMenuItem vsComputerToolStripMenuItem;
        private AcceptClearButtons acceptClearButtons1;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem verticalTrayToolStripMenuItem;
        private ToolStripMenuItem horizontalTrayToolStripMenuItem;
        private TableLayoutPanel tlpMain;
        private TableLayoutPanel tlpCradleBoardCradle;
        private TableLayoutPanel tlpBoard;
        private PlayerControl playerControl1;
        private ToolStripMenuItem renaldoToolStripMenuItem;
        private ToolStripMenuItem úrsulaToolStripMenuItem;
        private ToolStripMenuItem andrésToolStripMenuItem;
        private ToolStripMenuItem tatiToolStripMenuItem;
        private ToolStripMenuItem pepitoToolStripMenuItem;
    }
}