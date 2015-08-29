namespace IB2Toolset
{
    partial class RulesEditor
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
            this.gbMoveDiagonalCost = new System.Windows.Forms.GroupBox();
            this.rbtnOnePointFiveSquares = new System.Windows.Forms.RadioButton();
            this.rbtnOneSquare = new System.Windows.Forms.RadioButton();
            this.rtxtInfo = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gbArmorClassDisplay = new System.Windows.Forms.GroupBox();
            this.rbtnDescendingAC = new System.Windows.Forms.RadioButton();
            this.rbtnAscendingAC = new System.Windows.Forms.RadioButton();
            this.gbMoveDiagonalCost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gbArmorClassDisplay.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMoveDiagonalCost
            // 
            this.gbMoveDiagonalCost.Controls.Add(this.rbtnOnePointFiveSquares);
            this.gbMoveDiagonalCost.Controls.Add(this.rbtnOneSquare);
            this.gbMoveDiagonalCost.Location = new System.Drawing.Point(12, 12);
            this.gbMoveDiagonalCost.Name = "gbMoveDiagonalCost";
            this.gbMoveDiagonalCost.Size = new System.Drawing.Size(129, 64);
            this.gbMoveDiagonalCost.TabIndex = 0;
            this.gbMoveDiagonalCost.TabStop = false;
            this.gbMoveDiagonalCost.Text = "Diagonal Move Cost";
            this.gbMoveDiagonalCost.MouseHover += new System.EventHandler(this.gbMoveDiagonalCost_MouseHover);
            // 
            // rbtnOnePointFiveSquares
            // 
            this.rbtnOnePointFiveSquares.AutoSize = true;
            this.rbtnOnePointFiveSquares.Location = new System.Drawing.Point(11, 38);
            this.rbtnOnePointFiveSquares.Name = "rbtnOnePointFiveSquares";
            this.rbtnOnePointFiveSquares.Size = new System.Drawing.Size(88, 17);
            this.rbtnOnePointFiveSquares.TabIndex = 1;
            this.rbtnOnePointFiveSquares.TabStop = true;
            this.rbtnOnePointFiveSquares.Text = "1.5 per Move";
            this.rbtnOnePointFiveSquares.UseVisualStyleBackColor = true;
            this.rbtnOnePointFiveSquares.CheckedChanged += new System.EventHandler(this.rbtnOnePointFiveSquares_CheckedChanged);
            this.rbtnOnePointFiveSquares.MouseHover += new System.EventHandler(this.rbtnOnePointFiveSquares_MouseHover);
            // 
            // rbtnOneSquare
            // 
            this.rbtnOneSquare.AutoSize = true;
            this.rbtnOneSquare.Location = new System.Drawing.Point(11, 18);
            this.rbtnOneSquare.Name = "rbtnOneSquare";
            this.rbtnOneSquare.Size = new System.Drawing.Size(88, 17);
            this.rbtnOneSquare.TabIndex = 0;
            this.rbtnOneSquare.TabStop = true;
            this.rbtnOneSquare.Text = "1.0 per Move";
            this.rbtnOneSquare.UseVisualStyleBackColor = true;
            this.rbtnOneSquare.CheckedChanged += new System.EventHandler(this.rbtnOneSquare_CheckedChanged);
            this.rbtnOneSquare.MouseHover += new System.EventHandler(this.rbtnOneSquare_MouseHover);
            // 
            // rtxtInfo
            // 
            this.rtxtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtInfo.Location = new System.Drawing.Point(0, 0);
            this.rtxtInfo.Name = "rtxtInfo";
            this.rtxtInfo.Size = new System.Drawing.Size(270, 514);
            this.rtxtInfo.TabIndex = 1;
            this.rtxtInfo.Text = "";
            this.rtxtInfo.MouseHover += new System.EventHandler(this.rtxtInfo_MouseHover);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gbArmorClassDisplay);
            this.splitContainer1.Panel1.Controls.Add(this.gbMoveDiagonalCost);
            this.splitContainer1.Panel1.MouseHover += new System.EventHandler(this.splitContainer1_Panel1_MouseHover);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtxtInfo);
            this.splitContainer1.Size = new System.Drawing.Size(825, 514);
            this.splitContainer1.SplitterDistance = 551;
            this.splitContainer1.TabIndex = 2;
            // 
            // gbArmorClassDisplay
            // 
            this.gbArmorClassDisplay.Controls.Add(this.rbtnDescendingAC);
            this.gbArmorClassDisplay.Controls.Add(this.rbtnAscendingAC);
            this.gbArmorClassDisplay.Location = new System.Drawing.Point(147, 12);
            this.gbArmorClassDisplay.Name = "gbArmorClassDisplay";
            this.gbArmorClassDisplay.Size = new System.Drawing.Size(129, 64);
            this.gbArmorClassDisplay.TabIndex = 1;
            this.gbArmorClassDisplay.TabStop = false;
            this.gbArmorClassDisplay.Text = "Armor Class Shown";
            this.gbArmorClassDisplay.MouseHover += new System.EventHandler(this.gbArmorClassDisplay_MouseHover);
            // 
            // rbtnDescendingAC
            // 
            this.rbtnDescendingAC.AutoSize = true;
            this.rbtnDescendingAC.Location = new System.Drawing.Point(11, 38);
            this.rbtnDescendingAC.Name = "rbtnDescendingAC";
            this.rbtnDescendingAC.Size = new System.Drawing.Size(82, 17);
            this.rbtnDescendingAC.TabIndex = 1;
            this.rbtnDescendingAC.TabStop = true;
            this.rbtnDescendingAC.Text = "Descending";
            this.rbtnDescendingAC.UseVisualStyleBackColor = true;
            this.rbtnDescendingAC.CheckedChanged += new System.EventHandler(this.rbtnDescendingAC_CheckedChanged);
            this.rbtnDescendingAC.MouseHover += new System.EventHandler(this.rbtnDescendingAC_MouseHover);
            // 
            // rbtnAscendingAC
            // 
            this.rbtnAscendingAC.AutoSize = true;
            this.rbtnAscendingAC.Location = new System.Drawing.Point(11, 18);
            this.rbtnAscendingAC.Name = "rbtnAscendingAC";
            this.rbtnAscendingAC.Size = new System.Drawing.Size(75, 17);
            this.rbtnAscendingAC.TabIndex = 0;
            this.rbtnAscendingAC.TabStop = true;
            this.rbtnAscendingAC.Text = "Ascending";
            this.rbtnAscendingAC.UseVisualStyleBackColor = true;
            this.rbtnAscendingAC.CheckedChanged += new System.EventHandler(this.rbtnAscendingAC_CheckedChanged);
            this.rbtnAscendingAC.MouseHover += new System.EventHandler(this.rbtnAscendingAC_MouseHover);
            // 
            // RulesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 514);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RulesEditor";
            this.Text = "RulesEditor";
            this.gbMoveDiagonalCost.ResumeLayout(false);
            this.gbMoveDiagonalCost.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gbArmorClassDisplay.ResumeLayout(false);
            this.gbArmorClassDisplay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMoveDiagonalCost;
        private System.Windows.Forms.RadioButton rbtnOnePointFiveSquares;
        private System.Windows.Forms.RadioButton rbtnOneSquare;
        private System.Windows.Forms.RichTextBox rtxtInfo;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gbArmorClassDisplay;
        private System.Windows.Forms.RadioButton rbtnDescendingAC;
        private System.Windows.Forms.RadioButton rbtnAscendingAC;
    }
}