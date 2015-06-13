namespace IB2Toolset
{
    partial class PlayerClassEditor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainerTraitsSpells = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvTraitsAllowed = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dgvSpellsAllowed = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxItemsAllowed = new System.Windows.Forms.CheckedListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnDuplicatePlayerClass = new System.Windows.Forms.Button();
            this.btnAddPlayerClass = new System.Windows.Forms.Button();
            this.btnRemovePlayerClass = new System.Windows.Forms.Button();
            this.lbxPlayerClasses = new System.Windows.Forms.ListBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainerTraitsSpells.Panel1.SuspendLayout();
            this.splitContainerTraitsSpells.Panel2.SuspendLayout();
            this.splitContainerTraitsSpells.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraitsAllowed)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpellsAllowed)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(241, 489);
            this.propertyGrid1.TabIndex = 0;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainerTraitsSpells);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.propertyGrid1);
            this.splitContainer1.Size = new System.Drawing.Size(1057, 489);
            this.splitContainer1.SplitterDistance = 812;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainerTraitsSpells
            // 
            this.splitContainerTraitsSpells.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerTraitsSpells.Location = new System.Drawing.Point(399, 12);
            this.splitContainerTraitsSpells.Name = "splitContainerTraitsSpells";
            this.splitContainerTraitsSpells.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerTraitsSpells.Panel1
            // 
            this.splitContainerTraitsSpells.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainerTraitsSpells.Panel2
            // 
            this.splitContainerTraitsSpells.Panel2.Controls.Add(this.groupBox4);
            this.splitContainerTraitsSpells.Size = new System.Drawing.Size(405, 466);
            this.splitContainerTraitsSpells.SplitterDistance = 165;
            this.splitContainerTraitsSpells.TabIndex = 7;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dgvTraitsAllowed);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(405, 165);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Allowed Traits";
            // 
            // dgvTraitsAllowed
            // 
            this.dgvTraitsAllowed.AllowUserToAddRows = false;
            this.dgvTraitsAllowed.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTraitsAllowed.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTraitsAllowed.ColumnHeadersHeight = 45;
            this.dgvTraitsAllowed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTraitsAllowed.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTraitsAllowed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTraitsAllowed.Location = new System.Drawing.Point(3, 16);
            this.dgvTraitsAllowed.MultiSelect = false;
            this.dgvTraitsAllowed.Name = "dgvTraitsAllowed";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTraitsAllowed.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvTraitsAllowed.RowHeadersVisible = false;
            this.dgvTraitsAllowed.Size = new System.Drawing.Size(399, 146);
            this.dgvTraitsAllowed.TabIndex = 2;
            this.dgvTraitsAllowed.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvTraitsAllowed_DataError);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.dgvSpellsAllowed);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(405, 297);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Allowed Spells";
            // 
            // dgvSpellsAllowed
            // 
            this.dgvSpellsAllowed.AllowUserToAddRows = false;
            this.dgvSpellsAllowed.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpellsAllowed.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSpellsAllowed.ColumnHeadersHeight = 45;
            this.dgvSpellsAllowed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSpellsAllowed.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvSpellsAllowed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSpellsAllowed.Location = new System.Drawing.Point(3, 16);
            this.dgvSpellsAllowed.MultiSelect = false;
            this.dgvSpellsAllowed.Name = "dgvSpellsAllowed";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSpellsAllowed.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvSpellsAllowed.RowHeadersVisible = false;
            this.dgvSpellsAllowed.Size = new System.Drawing.Size(399, 278);
            this.dgvSpellsAllowed.TabIndex = 2;
            this.dgvSpellsAllowed.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvSpellsAllowed_DataError);
            this.dgvSpellsAllowed.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvSpellsAllowed_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.cbxItemsAllowed);
            this.groupBox2.Location = new System.Drawing.Point(195, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(198, 466);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Allowed Items";
            // 
            // cbxItemsAllowed
            // 
            this.cbxItemsAllowed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxItemsAllowed.CheckOnClick = true;
            this.cbxItemsAllowed.FormattingEnabled = true;
            this.cbxItemsAllowed.Location = new System.Drawing.Point(3, 16);
            this.cbxItemsAllowed.Name = "cbxItemsAllowed";
            this.cbxItemsAllowed.ScrollAlwaysVisible = true;
            this.cbxItemsAllowed.Size = new System.Drawing.Size(192, 447);
            this.cbxItemsAllowed.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.btnSort);
            this.groupBox1.Controls.Add(this.btnDuplicatePlayerClass);
            this.groupBox1.Controls.Add(this.btnAddPlayerClass);
            this.groupBox1.Controls.Add(this.btnRemovePlayerClass);
            this.groupBox1.Controls.Add(this.lbxPlayerClasses);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(177, 466);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "List of Player Classes";
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(5, 14);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(40, 23);
            this.btnSort.TabIndex = 89;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // btnDuplicatePlayerClass
            // 
            this.btnDuplicatePlayerClass.Location = new System.Drawing.Point(105, 14);
            this.btnDuplicatePlayerClass.Name = "btnDuplicatePlayerClass";
            this.btnDuplicatePlayerClass.Size = new System.Drawing.Size(66, 23);
            this.btnDuplicatePlayerClass.TabIndex = 87;
            this.btnDuplicatePlayerClass.Text = "Duplicate";
            this.btnDuplicatePlayerClass.UseVisualStyleBackColor = true;
            this.btnDuplicatePlayerClass.Click += new System.EventHandler(this.btnDuplicatePlayerClass_Click);
            // 
            // btnAddPlayerClass
            // 
            this.btnAddPlayerClass.Location = new System.Drawing.Point(5, 37);
            this.btnAddPlayerClass.Name = "btnAddPlayerClass";
            this.btnAddPlayerClass.Size = new System.Drawing.Size(166, 23);
            this.btnAddPlayerClass.TabIndex = 85;
            this.btnAddPlayerClass.Text = "Add";
            this.btnAddPlayerClass.UseVisualStyleBackColor = true;
            this.btnAddPlayerClass.Click += new System.EventHandler(this.btnAddPlayerClass_Click);
            // 
            // btnRemovePlayerClass
            // 
            this.btnRemovePlayerClass.Location = new System.Drawing.Point(45, 14);
            this.btnRemovePlayerClass.Name = "btnRemovePlayerClass";
            this.btnRemovePlayerClass.Size = new System.Drawing.Size(60, 23);
            this.btnRemovePlayerClass.TabIndex = 86;
            this.btnRemovePlayerClass.Text = "Remove";
            this.btnRemovePlayerClass.UseVisualStyleBackColor = true;
            this.btnRemovePlayerClass.Click += new System.EventHandler(this.btnRemovePlayerClass_Click);
            // 
            // lbxPlayerClasses
            // 
            this.lbxPlayerClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lbxPlayerClasses.FormattingEnabled = true;
            this.lbxPlayerClasses.Location = new System.Drawing.Point(10, 61);
            this.lbxPlayerClasses.Name = "lbxPlayerClasses";
            this.lbxPlayerClasses.Size = new System.Drawing.Size(157, 394);
            this.lbxPlayerClasses.TabIndex = 82;
            this.lbxPlayerClasses.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lbxPlayerClasses_MouseClick);
            this.lbxPlayerClasses.SelectedIndexChanged += new System.EventHandler(this.lbxPlayerClasses_SelectedIndexChanged);
            // 
            // PlayerClassEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 489);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PlayerClassEditor";
            this.Text = "Player Class Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PlayerClassEditor_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.splitContainerTraitsSpells.Panel1.ResumeLayout(false);
            this.splitContainerTraitsSpells.Panel2.ResumeLayout(false);
            this.splitContainerTraitsSpells.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTraitsAllowed)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSpellsAllowed)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDuplicatePlayerClass;
        private System.Windows.Forms.Button btnAddPlayerClass;
        private System.Windows.Forms.Button btnRemovePlayerClass;
        private System.Windows.Forms.ListBox lbxPlayerClasses;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckedListBox cbxItemsAllowed;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataGridView dgvSpellsAllowed;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dgvTraitsAllowed;
        private System.Windows.Forms.SplitContainer splitContainerTraitsSpells;
        private System.Windows.Forms.Button btnSort;

    }
}