namespace IB2Toolset
{
    partial class RacesEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvTraitsAllowed = new System.Windows.Forms.DataGridView();
            this.gbAllowedClasses = new System.Windows.Forms.GroupBox();
            this.cbxClassesAllowed = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDuplicateRace = new System.Windows.Forms.Button();
            this.btnAddRace = new System.Windows.Forms.Button();
            this.btnRemoveRace = new System.Windows.Forms.Button();
            this.lbxRaces = new System.Windows.Forms.ListBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.btnSort = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraitsAllowed)).BeginInit();
            this.gbAllowedClasses.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.groupBox5);
            this.splitContainer1.Panel1.Controls.Add(this.gbAllowedClasses);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(878, 483);
            this.splitContainer1.SplitterDistance = 579;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.dgvTraitsAllowed);
            this.groupBox5.Location = new System.Drawing.Point(195, 226);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(381, 245);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Racial Traits";
            // 
            // dgvTraitsAllowed
            // 
            this.dgvTraitsAllowed.AllowUserToAddRows = false;
            this.dgvTraitsAllowed.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTraitsAllowed.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvTraitsAllowed.ColumnHeadersHeight = 45;
            this.dgvTraitsAllowed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTraitsAllowed.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvTraitsAllowed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTraitsAllowed.Location = new System.Drawing.Point(3, 16);
            this.dgvTraitsAllowed.MultiSelect = false;
            this.dgvTraitsAllowed.Name = "dgvTraitsAllowed";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTraitsAllowed.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvTraitsAllowed.RowHeadersVisible = false;
            this.dgvTraitsAllowed.Size = new System.Drawing.Size(375, 226);
            this.dgvTraitsAllowed.TabIndex = 2;
            // 
            // gbAllowedClasses
            // 
            this.gbAllowedClasses.Controls.Add(this.cbxClassesAllowed);
            this.gbAllowedClasses.Location = new System.Drawing.Point(195, 12);
            this.gbAllowedClasses.Name = "gbAllowedClasses";
            this.gbAllowedClasses.Size = new System.Drawing.Size(198, 208);
            this.gbAllowedClasses.TabIndex = 3;
            this.gbAllowedClasses.TabStop = false;
            this.gbAllowedClasses.Text = "Allowed Classes";
            // 
            // cbxClassesAllowed
            // 
            this.cbxClassesAllowed.CheckOnClick = true;
            this.cbxClassesAllowed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxClassesAllowed.FormattingEnabled = true;
            this.cbxClassesAllowed.Location = new System.Drawing.Point(3, 16);
            this.cbxClassesAllowed.Name = "cbxClassesAllowed";
            this.cbxClassesAllowed.ScrollAlwaysVisible = true;
            this.cbxClassesAllowed.Size = new System.Drawing.Size(192, 189);
            this.cbxClassesAllowed.TabIndex = 1;
            this.cbxClassesAllowed.SelectedIndexChanged += new System.EventHandler(this.cbxClassesAllowed_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnSort);
            this.groupBox1.Controls.Add(this.btnDuplicateRace);
            this.groupBox1.Controls.Add(this.btnAddRace);
            this.groupBox1.Controls.Add(this.btnRemoveRace);
            this.groupBox1.Controls.Add(this.lbxRaces);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 460);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of Races";
            // 
            // btnDuplicateRace
            // 
            this.btnDuplicateRace.Location = new System.Drawing.Point(107, 14);
            this.btnDuplicateRace.Name = "btnDuplicateRace";
            this.btnDuplicateRace.Size = new System.Drawing.Size(62, 23);
            this.btnDuplicateRace.TabIndex = 87;
            this.btnDuplicateRace.Text = "Duplicate";
            this.btnDuplicateRace.UseVisualStyleBackColor = true;
            this.btnDuplicateRace.Click += new System.EventHandler(this.btnDuplicateRace_Click);
            // 
            // btnAddRace
            // 
            this.btnAddRace.Location = new System.Drawing.Point(6, 37);
            this.btnAddRace.Name = "btnAddRace";
            this.btnAddRace.Size = new System.Drawing.Size(163, 23);
            this.btnAddRace.TabIndex = 85;
            this.btnAddRace.Text = "Add";
            this.btnAddRace.UseVisualStyleBackColor = true;
            this.btnAddRace.Click += new System.EventHandler(this.btnAddRace_Click);
            // 
            // btnRemoveRace
            // 
            this.btnRemoveRace.Location = new System.Drawing.Point(47, 14);
            this.btnRemoveRace.Name = "btnRemoveRace";
            this.btnRemoveRace.Size = new System.Drawing.Size(60, 23);
            this.btnRemoveRace.TabIndex = 86;
            this.btnRemoveRace.Text = "Remove";
            this.btnRemoveRace.UseVisualStyleBackColor = true;
            this.btnRemoveRace.Click += new System.EventHandler(this.btnRemoveRace_Click);
            // 
            // lbxRaces
            // 
            this.lbxRaces.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxRaces.FormattingEnabled = true;
            this.lbxRaces.Location = new System.Drawing.Point(10, 61);
            this.lbxRaces.Name = "lbxRaces";
            this.lbxRaces.Size = new System.Drawing.Size(157, 381);
            this.lbxRaces.TabIndex = 82;
            this.lbxRaces.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbxRaces_MouseClick);
            this.lbxRaces.SelectedIndexChanged += new System.EventHandler(this.lbxRaces_SelectedIndexChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(295, 483);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(6, 14);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(40, 23);
            this.btnSort.TabIndex = 90;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // RacesEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 14f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 483);
            this.Controls.Add(this.splitContainer1);
            this.Name = "RacesEditor";
            this.Text = "RacesEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RacesEditor_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraitsAllowed)).EndInit();
            this.gbAllowedClasses.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDuplicateRace;
        private System.Windows.Forms.Button btnAddRace;
        private System.Windows.Forms.Button btnRemoveRace;
        private System.Windows.Forms.ListBox lbxRaces;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.GroupBox gbAllowedClasses;
        private System.Windows.Forms.CheckedListBox cbxClassesAllowed;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvTraitsAllowed;
        private System.Windows.Forms.Button btnSort;
    }
}