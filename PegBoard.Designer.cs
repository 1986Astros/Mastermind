﻿namespace MasterMind
{
    partial class PegBoard
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
            this.SuspendLayout();
            // 
            // PegBoard
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "PegBoard";
            this.Size = new System.Drawing.Size(0, 0);
            this.Load += new System.EventHandler(this.PegBoard_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PegBoard_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.PegBoard_DragOver);
            this.DragLeave += new System.EventHandler(this.PegBoard_DragLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PegBoard_Paint);
            this.MouseLeave += new System.EventHandler(this.PegBoard_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PegBoard_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
