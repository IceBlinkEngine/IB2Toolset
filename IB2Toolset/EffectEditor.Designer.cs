namespace IB2Toolset
{
    partial class EffectEditor
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDuplicateEffect = new System.Windows.Forms.Button();
            this.btnAddEffect = new System.Windows.Forms.Button();
            this.btnRemoveEffect = new System.Windows.Forms.Button();
            this.lbxEffects = new System.Windows.Forms.ListBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.btnSort = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(818, 449);
            this.splitContainer1.SplitterDistance = 539;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnSort);
            this.groupBox1.Controls.Add(this.btnDuplicateEffect);
            this.groupBox1.Controls.Add(this.btnAddEffect);
            this.groupBox1.Controls.Add(this.btnRemoveEffect);
            this.groupBox1.Controls.Add(this.lbxEffects);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 426);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of Effects";
            // 
            // btnDuplicateEffect
            // 
            this.btnDuplicateEffect.Location = new System.Drawing.Point(108, 14);
            this.btnDuplicateEffect.Name = "btnDuplicateEffect";
            this.btnDuplicateEffect.Size = new System.Drawing.Size(61, 23);
            this.btnDuplicateEffect.TabIndex = 87;
            this.btnDuplicateEffect.Text = "Duplicate";
            this.btnDuplicateEffect.UseVisualStyleBackColor = true;
            this.btnDuplicateEffect.Click += new System.EventHandler(this.btnDuplicateEffect_Click_1);
            // 
            // btnAddEffect
            // 
            this.btnAddEffect.Location = new System.Drawing.Point(8, 37);
            this.btnAddEffect.Name = "btnAddEffect";
            this.btnAddEffect.Size = new System.Drawing.Size(161, 23);
            this.btnAddEffect.TabIndex = 85;
            this.btnAddEffect.Text = "Add";
            this.btnAddEffect.UseVisualStyleBackColor = true;
            this.btnAddEffect.Click += new System.EventHandler(this.btnAddEffect_Click_1);
            // 
            // btnRemoveEffect
            // 
            this.btnRemoveEffect.Location = new System.Drawing.Point(48, 14);
            this.btnRemoveEffect.Name = "btnRemoveEffect";
            this.btnRemoveEffect.Size = new System.Drawing.Size(60, 23);
            this.btnRemoveEffect.TabIndex = 86;
            this.btnRemoveEffect.Text = "Remove";
            this.btnRemoveEffect.UseVisualStyleBackColor = true;
            this.btnRemoveEffect.Click += new System.EventHandler(this.btnRemoveEffect_Click_1);
            // 
            // lbxEffects
            // 
            this.lbxEffects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxEffects.FormattingEnabled = true;
            this.lbxEffects.Location = new System.Drawing.Point(10, 61);
            this.lbxEffects.Name = "lbxEffects";
            this.lbxEffects.Size = new System.Drawing.Size(157, 355);
            this.lbxEffects.TabIndex = 82;
            this.lbxEffects.SelectedIndexChanged += new System.EventHandler(this.lbxEffects_SelectedIndexChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(275, 449);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(8, 14);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(40, 23);
            this.btnSort.TabIndex = 88;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // EffectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 449);
            this.Controls.Add(this.splitContainer1);
            this.Name = "EffectEditor";
            this.Text = "Effect Editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDuplicateEffect;
        private System.Windows.Forms.Button btnAddEffect;
        private System.Windows.Forms.Button btnRemoveEffect;
        private System.Windows.Forms.ListBox lbxEffects;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button btnSort;
    }
}