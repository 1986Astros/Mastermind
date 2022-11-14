namespace MasterMind
{
    partial class CreatePuzzle
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cradleSource = new MasterMind.Cradle();
            this.cradlePuzzle = new MasterMind.Cradle();
            this.btnUseThisPuzzle = new System.Windows.Forms.Button();
            this.btnRandomize = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cradleSource
            // 
            this.cradleSource.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cradleSource.AutoSize = true;
            this.cradleSource.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cradleSource.BackColor = System.Drawing.Color.SandyBrown;
            this.cradleSource.IsSource = true;
            this.cradleSource.Location = new System.Drawing.Point(21, 86);
            this.cradleSource.Name = "cradleSource";
            this.cradleSource.Orientation = MasterMind.Cradle.Orientations.Horizontal;
            this.cradleSource.PegCount = 6;
            this.cradleSource.Size = new System.Drawing.Size(240, 40);
            this.cradleSource.TabIndex = 0;
            // 
            // cradlePuzzle
            // 
            this.cradlePuzzle.AllowDrop = true;
            this.cradlePuzzle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cradlePuzzle.AutoSize = true;
            this.cradlePuzzle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cradlePuzzle.BackColor = System.Drawing.Color.SandyBrown;
            this.cradlePuzzle.IsSource = false;
            this.cradlePuzzle.Location = new System.Drawing.Point(61, 3);
            this.cradlePuzzle.Name = "cradlePuzzle";
            this.cradlePuzzle.Orientation = MasterMind.Cradle.Orientations.Horizontal;
            this.cradlePuzzle.PegCount = 4;
            this.cradlePuzzle.Size = new System.Drawing.Size(160, 40);
            this.cradlePuzzle.TabIndex = 1;
            // 
            // btnUseThisPuzzle
            // 
            this.btnUseThisPuzzle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUseThisPuzzle.AutoSize = true;
            this.btnUseThisPuzzle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUseThisPuzzle.Location = new System.Drawing.Point(103, 3);
            this.btnUseThisPuzzle.Margin = new System.Windows.Forms.Padding(12, 3, 12, 3);
            this.btnUseThisPuzzle.Name = "btnUseThisPuzzle";
            this.btnUseThisPuzzle.Size = new System.Drawing.Size(94, 25);
            this.btnUseThisPuzzle.TabIndex = 2;
            this.btnUseThisPuzzle.Text = "Use this puzzle";
            this.btnUseThisPuzzle.UseVisualStyleBackColor = true;
            this.btnUseThisPuzzle.Click += new System.EventHandler(this.btnUseThisPuzzle_Click);
            // 
            // btnRandomize
            // 
            this.btnRandomize.AutoSize = true;
            this.btnRandomize.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRandomize.Location = new System.Drawing.Point(3, 3);
            this.btnRandomize.Margin = new System.Windows.Forms.Padding(3, 3, 12, 3);
            this.btnRandomize.Name = "btnRandomize";
            this.btnRandomize.Size = new System.Drawing.Size(76, 25);
            this.btnRandomize.TabIndex = 3;
            this.btnRandomize.Text = "Randomize";
            this.btnRandomize.UseVisualStyleBackColor = true;
            this.btnRandomize.Click += new System.EventHandler(this.btnRandomize_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnCancel.Location = new System.Drawing.Point(221, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(12, 3, 3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(53, 25);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.btnRandomize, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnUseThisPuzzle, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(277, 31);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.cradlePuzzle, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.cradleSource, 0, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(283, 129);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // CreatePuzzle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(308, 151);
            this.Controls.Add(this.tableLayoutPanel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "CreatePuzzle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create Puzzle";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Cradle cradleSource;
        private Cradle cradlePuzzle;
        private Button btnUseThisPuzzle;
        private Button btnRandomize;
        private Button btnCancel;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}