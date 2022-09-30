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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.righthandedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lefthandedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.playerAtBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerAtTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pegBoard1 = new MasterMind.PegBoard();
            this.cradle1 = new MasterMind.Cradle();
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
            this.menuStrip1.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
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
            this.playerAtTopToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // righthandedToolStripMenuItem
            // 
            this.righthandedToolStripMenuItem.Checked = true;
            this.righthandedToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.righthandedToolStripMenuItem.Name = "righthandedToolStripMenuItem";
            this.righthandedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.righthandedToolStripMenuItem.Text = "Right-handed";
            // 
            // lefthandedToolStripMenuItem
            // 
            this.lefthandedToolStripMenuItem.Name = "lefthandedToolStripMenuItem";
            this.lefthandedToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.lefthandedToolStripMenuItem.Text = "Left-handed";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // playerAtBottomToolStripMenuItem
            // 
            this.playerAtBottomToolStripMenuItem.Checked = true;
            this.playerAtBottomToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.playerAtBottomToolStripMenuItem.Name = "playerAtBottomToolStripMenuItem";
            this.playerAtBottomToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.playerAtBottomToolStripMenuItem.Text = "Player at bottom";
            // 
            // playerAtTopToolStripMenuItem
            // 
            this.playerAtTopToolStripMenuItem.Name = "playerAtTopToolStripMenuItem";
            this.playerAtTopToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.playerAtTopToolStripMenuItem.Text = "Player at top";
            // 
            // pegBoard1
            // 
            this.pegBoard1.AutoSize = true;
            this.pegBoard1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pegBoard1.BackColor = System.Drawing.Color.BurlyWood;
            this.pegBoard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pegBoard1.Location = new System.Drawing.Point(40, 27);
            this.pegBoard1.Name = "pegBoard1";
            this.pegBoard1.Size = new System.Drawing.Size(192, 374);
            this.pegBoard1.TabIndex = 2;
            // 
            // cradle1
            // 
            this.cradle1.AutoSize = true;
            this.cradle1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cradle1.BackColor = System.Drawing.Color.SandyBrown;
            this.cradle1.Location = new System.Drawing.Point(263, 161);
            this.cradle1.Name = "cradle1";
            this.cradle1.Orientation = MasterMind.Cradle.Orientations.Vertical;
            this.cradle1.Size = new System.Drawing.Size(40, 240);
            this.cradle1.TabIndex = 3;
            // 
            // startGameToolStripMenuItem
            // 
            this.startGameToolStripMenuItem.Enabled = false;
            this.startGameToolStripMenuItem.Name = "startGameToolStripMenuItem";
            this.startGameToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.startGameToolStripMenuItem.Text = "Start game";
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
            this.colors4Pegs10Turns30PointsToolStripMenuItem.Name = "colors4Pegs10Turns30PointsToolStripMenuItem";
            this.colors4Pegs10Turns30PointsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.colors4Pegs10Turns30PointsToolStripMenuItem.Text = "Medium - 30 points";
            this.colors4Pegs10Turns30PointsToolStripMenuItem.ToolTipText = "6 colors, 4 pegs, 10 turns";
            // 
            // medium100PointsToolStripMenuItem
            // 
            this.medium100PointsToolStripMenuItem.Name = "medium100PointsToolStripMenuItem";
            this.medium100PointsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.medium100PointsToolStripMenuItem.Text = "Medium - 100 points";
            this.medium100PointsToolStripMenuItem.ToolTipText = "6 colors, 4 pegs, 10 turns";
            // 
            // difficult30PointsToolStripMenuItem
            // 
            this.difficult30PointsToolStripMenuItem.Name = "difficult30PointsToolStripMenuItem";
            this.difficult30PointsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.difficult30PointsToolStripMenuItem.Text = "Difficult - 30 points";
            this.difficult30PointsToolStripMenuItem.ToolTipText = "8 colors, 5 pegs, 12 turns";
            // 
            // difficult100PointsToolStripMenuItem
            // 
            this.difficult100PointsToolStripMenuItem.Name = "difficult100PointsToolStripMenuItem";
            this.difficult100PointsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.difficult100PointsToolStripMenuItem.Text = "Difficult - 100 points";
            this.difficult100PointsToolStripMenuItem.ToolTipText = "8 colors, 5 pegs, 12 turns";
            // 
            // customToolStripMenuItem
            // 
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
            this.humansSamePuzzlesToolStripMenuItem.Name = "humansSamePuzzlesToolStripMenuItem";
            this.humansSamePuzzlesToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.humansSamePuzzlesToolStripMenuItem.Text = "Humans - computer is maker";
            this.humansSamePuzzlesToolStripMenuItem.Click += new System.EventHandler(this.humansSamePuzzlesToolStripMenuItem_Click);
            // 
            // humansToolStripMenuItem
            // 
            this.humansToolStripMenuItem.Name = "humansToolStripMenuItem";
            this.humansToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.humansToolStripMenuItem.Text = "Humans - makers and breakers";
            // 
            // vsComputerToolStripMenuItem
            // 
            this.vsComputerToolStripMenuItem.Name = "vsComputerToolStripMenuItem";
            this.vsComputerToolStripMenuItem.Size = new System.Drawing.Size(238, 22);
            this.vsComputerToolStripMenuItem.Text = "Vs. Computer";
            // 
            // ConsoleMM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 659);
            this.Controls.Add(this.cradle1);
            this.Controls.Add(this.pegBoard1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ConsoleMM";
            this.Text = "MasterMind";
            this.Load += new System.EventHandler(this.ConsoleMM_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
    }
}