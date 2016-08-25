namespace IB2Toolset
{
    partial class WeatherEffectsEditor
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
            this.btnSort = new System.Windows.Forms.Button();
            this.btnDuplicateTrait = new System.Windows.Forms.Button();
            this.btnAddTrait = new System.Windows.Forms.Button();
            this.btnRemoveTrait = new System.Windows.Forms.Button();
            this.lbxWeatherEffects = new System.Windows.Forms.ListBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
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
            this.groupBox1.Controls.Add(this.btnDuplicateTrait);
            this.groupBox1.Controls.Add(this.btnAddTrait);
            this.groupBox1.Controls.Add(this.btnRemoveTrait);
            this.groupBox1.Controls.Add(this.lbxWeatherEffects);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 426);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of Traits";
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(8, 14);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(40, 23);
            this.btnSort.TabIndex = 91;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // btnDuplicateTrait
            // 
            this.btnDuplicateTrait.Location = new System.Drawing.Point(108, 14);
            this.btnDuplicateTrait.Name = "btnDuplicateTrait";
            this.btnDuplicateTrait.Size = new System.Drawing.Size(61, 23);
            this.btnDuplicateTrait.TabIndex = 87;
            this.btnDuplicateTrait.Text = "Duplicate";
            this.btnDuplicateTrait.UseVisualStyleBackColor = true;
            this.btnDuplicateTrait.Click += new System.EventHandler(this.btnDuplicateTrait_Click);
            // 
            // btnAddTrait
            // 
            this.btnAddTrait.Location = new System.Drawing.Point(8, 37);
            this.btnAddTrait.Name = "btnAddTrait";
            this.btnAddTrait.Size = new System.Drawing.Size(161, 23);
            this.btnAddTrait.TabIndex = 85;
            this.btnAddTrait.Text = "Add";
            this.btnAddTrait.UseVisualStyleBackColor = true;
            this.btnAddTrait.Click += new System.EventHandler(this.btnAddTrait_Click);
            // 
            // btnRemoveTrait
            // 
            this.btnRemoveTrait.Location = new System.Drawing.Point(48, 14);
            this.btnRemoveTrait.Name = "btnRemoveTrait";
            this.btnRemoveTrait.Size = new System.Drawing.Size(60, 23);
            this.btnRemoveTrait.TabIndex = 86;
            this.btnRemoveTrait.Text = "Remove";
            this.btnRemoveTrait.UseVisualStyleBackColor = true;
            this.btnRemoveTrait.Click += new System.EventHandler(this.btnRemoveTrait_Click);
            // 
            // lbxWeatherEffects
            // 
            this.lbxWeatherEffects.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxWeatherEffects.FormattingEnabled = true;
            this.lbxWeatherEffects.Location = new System.Drawing.Point(10, 61);
            this.lbxWeatherEffects.Name = "lbxWeatherEffects";
            this.lbxWeatherEffects.Size = new System.Drawing.Size(157, 329);
            this.lbxWeatherEffects.TabIndex = 82;
            this.lbxWeatherEffects.SelectedIndexChanged += new System.EventHandler(this.lbxTraits_SelectedIndexChanged_1);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(275, 449);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // WeatherEffectsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 449);
            this.Controls.Add(this.splitContainer1);
            this.Name = "WeatherEffectsEditor";
            this.Text = "WeatherEffectsEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TraitEditor_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDuplicateTrait;
        private System.Windows.Forms.Button btnAddTrait;
        private System.Windows.Forms.Button btnRemoveTrait;
        private System.Windows.Forms.ListBox lbxWeatherEffects;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button btnSort;
    }
}