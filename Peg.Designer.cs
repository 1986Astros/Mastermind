namespace MasterMind
{
    partial class Peg
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
            // Peg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Peg";
            this.Load += new System.EventHandler(this.Peg_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Peg_Paint);
            this.DoubleClick += new System.EventHandler(this.Peg_DoubleClick);
            this.Enter += new System.EventHandler(this.Peg_Enter);
            this.Leave += new System.EventHandler(this.Peg_Leave);
            this.MouseLeave += new System.EventHandler(this.Peg_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Peg_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
