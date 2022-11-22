namespace MasterMind
{
    partial class PlayerControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerControl));
            MasterMind.CurrentGame currentGame1 = new MasterMind.CurrentGame();
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.playerToolStrip = new System.Windows.Forms.ToolStrip();
            this.playerToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPlayerLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPlayerToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.humanSeparatorToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.addOpponentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeFromGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tlpPerformance = new System.Windows.Forms.TableLayoutPanel();
            this.lvPerformance = new System.Windows.Forms.ListView();
            this.puzzleColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.wonLostcolumnHeader = new System.Windows.Forms.ColumnHeader();
            this.puzzleAvgcolumnHeader = new System.Windows.Forms.ColumnHeader();
            this.puzzleAvgOrderColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.puzzleAvgColorsColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.whenColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.statsBlankColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.panelScoring = new System.Windows.Forms.Panel();
            this.pegBoard1 = new MasterMind.PegBoard();
            this.tlpMain.SuspendLayout();
            this.playerToolStrip.SuspendLayout();
            this.tlpPerformance.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.AutoSize = true;
            this.tlpMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpMain.Controls.Add(this.playerToolStrip, 0, 0);
            this.tlpMain.Controls.Add(this.tlpPerformance, 0, 1);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(350, 499);
            this.tlpMain.TabIndex = 0;
            // 
            // playerToolStrip
            // 
            this.playerToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.playerToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playerToolStripDropDownButton,
            this.settingsToolStripDropDownButton});
            this.playerToolStrip.Location = new System.Drawing.Point(0, 0);
            this.playerToolStrip.Name = "playerToolStrip";
            this.playerToolStrip.Size = new System.Drawing.Size(350, 25);
            this.playerToolStrip.TabIndex = 0;
            this.playerToolStrip.Text = "toolStrip1";
            // 
            // playerToolStripDropDownButton
            // 
            this.playerToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.playerToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.humanSeparatorToolStripMenuItem});
            this.playerToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("playerToolStripDropDownButton.Image")));
            this.playerToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.playerToolStripDropDownButton.Name = "playerToolStripDropDownButton";
            this.playerToolStripDropDownButton.Size = new System.Drawing.Size(52, 22);
            this.playerToolStripDropDownButton.Text = "Player";
            this.playerToolStripDropDownButton.DropDownOpening += new System.EventHandler(this.playerToolStripDropDownButton_DropDownOpening);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newPlayerLabelToolStripMenuItem,
            this.newPlayerToolStripTextBox});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.newToolStripMenuItem.Text = "New ...";
            // 
            // newPlayerLabelToolStripMenuItem
            // 
            this.newPlayerLabelToolStripMenuItem.Enabled = false;
            this.newPlayerLabelToolStripMenuItem.Name = "newPlayerLabelToolStripMenuItem";
            this.newPlayerLabelToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.newPlayerLabelToolStripMenuItem.Text = "New player name:";
            this.newPlayerLabelToolStripMenuItem.Click += new System.EventHandler(this.newPlayerNameToolStripMenuItem_Click);
            // 
            // newPlayerToolStripTextBox
            // 
            this.newPlayerToolStripTextBox.BackColor = System.Drawing.Color.MistyRose;
            this.newPlayerToolStripTextBox.Name = "newPlayerToolStripTextBox";
            this.newPlayerToolStripTextBox.Size = new System.Drawing.Size(100, 23);
            this.newPlayerToolStripTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.newPlayerToolStripTextBox_KeyDown);
            this.newPlayerToolStripTextBox.TextChanged += new System.EventHandler(this.newPlayerToolStripTextBox_TextChanged);
            // 
            // humanSeparatorToolStripMenuItem
            // 
            this.humanSeparatorToolStripMenuItem.Name = "humanSeparatorToolStripMenuItem";
            this.humanSeparatorToolStripMenuItem.Size = new System.Drawing.Size(107, 6);
            // 
            // settingsToolStripDropDownButton
            // 
            this.settingsToolStripDropDownButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.settingsToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.settingsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addOpponentToolStripMenuItem,
            this.removeFromGameToolStripMenuItem});
            this.settingsToolStripDropDownButton.Image = global::MasterMind.Properties.Resources.Settings_dots_horizontal;
            this.settingsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsToolStripDropDownButton.Margin = new System.Windows.Forms.Padding(0, 1, 20, 2);
            this.settingsToolStripDropDownButton.Name = "settingsToolStripDropDownButton";
            this.settingsToolStripDropDownButton.ShowDropDownArrow = false;
            this.settingsToolStripDropDownButton.Size = new System.Drawing.Size(20, 22);
            this.settingsToolStripDropDownButton.Text = "settingsToolStripDropDownButton";
            this.settingsToolStripDropDownButton.DropDownOpening += new System.EventHandler(this.settingsToolStripDropDownButton_DropDownOpening);
            // 
            // addOpponentToolStripMenuItem
            // 
            this.addOpponentToolStripMenuItem.Name = "addOpponentToolStripMenuItem";
            this.addOpponentToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.addOpponentToolStripMenuItem.Text = "Add opponent";
            // 
            // removeFromGameToolStripMenuItem
            // 
            this.removeFromGameToolStripMenuItem.Enabled = false;
            this.removeFromGameToolStripMenuItem.Image = global::MasterMind.Properties.Resources.close;
            this.removeFromGameToolStripMenuItem.Name = "removeFromGameToolStripMenuItem";
            this.removeFromGameToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.removeFromGameToolStripMenuItem.Text = "Remove from game";
            this.removeFromGameToolStripMenuItem.Click += new System.EventHandler(this.removeFromGameToolStripMenuItem_Click);
            // 
            // tlpPerformance
            // 
            this.tlpPerformance.ColumnCount = 3;
            this.tlpPerformance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPerformance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPerformance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpPerformance.Controls.Add(this.lvPerformance, 1, 0);
            this.tlpPerformance.Controls.Add(this.panelScoring, 2, 0);
            this.tlpPerformance.Controls.Add(this.pegBoard1, 0, 0);
            this.tlpPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPerformance.Location = new System.Drawing.Point(3, 28);
            this.tlpPerformance.Name = "tlpPerformance";
            this.tlpPerformance.RowCount = 1;
            this.tlpPerformance.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPerformance.Size = new System.Drawing.Size(344, 468);
            this.tlpPerformance.TabIndex = 1;
            // 
            // lvPerformance
            // 
            this.lvPerformance.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.puzzleColumnHeader,
            this.wonLostcolumnHeader,
            this.puzzleAvgcolumnHeader,
            this.puzzleAvgOrderColumnHeader,
            this.puzzleAvgColorsColumnHeader,
            this.whenColumnHeader,
            this.statsBlankColumnHeader});
            this.lvPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPerformance.FullRowSelect = true;
            this.lvPerformance.GridLines = true;
            this.lvPerformance.Location = new System.Drawing.Point(198, 0);
            this.lvPerformance.Margin = new System.Windows.Forms.Padding(0);
            this.lvPerformance.Name = "lvPerformance";
            this.lvPerformance.OwnerDraw = true;
            this.lvPerformance.Size = new System.Drawing.Size(96, 468);
            this.lvPerformance.TabIndex = 0;
            this.lvPerformance.UseCompatibleStateImageBehavior = false;
            this.lvPerformance.View = System.Windows.Forms.View.Details;
            this.lvPerformance.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lvPerformance_DrawColumnHeader);
            this.lvPerformance.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lvPerformance_DrawItem);
            this.lvPerformance.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lvPerformance_DrawSubItem);
            // 
            // puzzleColumnHeader
            // 
            this.puzzleColumnHeader.Text = "Puzzle";
            this.puzzleColumnHeader.Width = 45;
            // 
            // wonLostcolumnHeader
            // 
            this.wonLostcolumnHeader.Text = "";
            this.wonLostcolumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.wonLostcolumnHeader.Width = 25;
            // 
            // puzzleAvgcolumnHeader
            // 
            this.puzzleAvgcolumnHeader.Text = "avg";
            this.puzzleAvgcolumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.puzzleAvgcolumnHeader.Width = 31;
            // 
            // puzzleAvgOrderColumnHeader
            // 
            this.puzzleAvgOrderColumnHeader.Text = "/order";
            this.puzzleAvgOrderColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.puzzleAvgOrderColumnHeader.Width = 45;
            // 
            // puzzleAvgColorsColumnHeader
            // 
            this.puzzleAvgColorsColumnHeader.Text = "/colors";
            this.puzzleAvgColorsColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.puzzleAvgColorsColumnHeader.Width = 49;
            // 
            // whenColumnHeader
            // 
            this.whenColumnHeader.Text = "Date/time";
            this.whenColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.whenColumnHeader.Width = 65;
            // 
            // statsBlankColumnHeader
            // 
            this.statsBlankColumnHeader.Text = "";
            this.statsBlankColumnHeader.Width = 5;
            // 
            // panelScoring
            // 
            this.panelScoring.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelScoring.Location = new System.Drawing.Point(294, 0);
            this.panelScoring.Margin = new System.Windows.Forms.Padding(0);
            this.panelScoring.Name = "panelScoring";
            this.panelScoring.Size = new System.Drawing.Size(50, 468);
            this.panelScoring.TabIndex = 1;
            this.panelScoring.Visible = false;
            // 
            // pegBoard1
            // 
            this.pegBoard1.AllowDrop = true;
            this.pegBoard1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pegBoard1.AutoSize = true;
            this.pegBoard1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pegBoard1.BackColor = System.Drawing.Color.BurlyWood;
            this.pegBoard1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pegBoard1.CurrentGame = currentGame1;
            this.pegBoard1.Location = new System.Drawing.Point(3, 3);
            this.pegBoard1.Name = "pegBoard1";
            this.pegBoard1.ShowSolution = false;
            this.pegBoard1.Size = new System.Drawing.Size(192, 462);
            this.pegBoard1.TabIndex = 2;
            this.pegBoard1.Visible = false;
            // 
            // PlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlpMain);
            this.Name = "PlayerControl";
            this.Size = new System.Drawing.Size(350, 499);
            this.Load += new System.EventHandler(this.PlayerControl_Load);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.playerToolStrip.ResumeLayout(false);
            this.playerToolStrip.PerformLayout();
            this.tlpPerformance.ResumeLayout(false);
            this.tlpPerformance.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel tlpMain;
        private ToolStrip playerToolStrip;
        private ToolStripDropDownButton playerToolStripDropDownButton;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem newPlayerLabelToolStripMenuItem;
        private ToolStripTextBox newPlayerToolStripTextBox;
        private ToolStripDropDownButton settingsToolStripDropDownButton;
        private ToolStripMenuItem addOpponentToolStripMenuItem;
        private ToolStripMenuItem removeFromGameToolStripMenuItem;
        private TableLayoutPanel tlpPerformance;
        private ListView lvPerformance;
        private ColumnHeader puzzleColumnHeader;
        private ColumnHeader wonLostcolumnHeader;
        private ColumnHeader puzzleAvgcolumnHeader;
        private ColumnHeader puzzleAvgOrderColumnHeader;
        private ColumnHeader puzzleAvgColorsColumnHeader;
        private ColumnHeader whenColumnHeader;
        private ColumnHeader statsBlankColumnHeader;
        private Panel panelScoring;
        private ToolStripSeparator humanSeparatorToolStripMenuItem;
        private PegBoard pegBoard1;
    }
}
