namespace IB2miniToolset
{
    partial class AreaForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AreaForm));
            this.btnEditArea = new System.Windows.Forms.Button();
            this.btnAddArea = new System.Windows.Forms.Button();
            this.btnRemoveArea = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAreaName = new System.Windows.Forms.TextBox();
            this.lbxAreas = new System.Windows.Forms.ListBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.btnLoadAllArea = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEditArea
            // 
            this.btnEditArea.Location = new System.Drawing.Point(105, 29);
            this.btnEditArea.Name = "btnEditArea";
            this.btnEditArea.Size = new System.Drawing.Size(33, 23);
            this.btnEditArea.TabIndex = 84;
            this.btnEditArea.Text = "Edit";
            this.btnEditArea.UseVisualStyleBackColor = true;
            this.btnEditArea.Click += new System.EventHandler(this.btnEditArea_Click_1);
            // 
            // btnAddArea
            // 
            this.btnAddArea.Location = new System.Drawing.Point(4, 4);
            this.btnAddArea.Name = "btnAddArea";
            this.btnAddArea.Size = new System.Drawing.Size(65, 23);
            this.btnAddArea.TabIndex = 82;
            this.btnAddArea.Text = "Add";
            this.btnAddArea.UseVisualStyleBackColor = true;
            this.btnAddArea.Click += new System.EventHandler(this.btnAddArea_Click_1);
            // 
            // btnRemoveArea
            // 
            this.btnRemoveArea.Location = new System.Drawing.Point(134, 4);
            this.btnRemoveArea.Name = "btnRemoveArea";
            this.btnRemoveArea.Size = new System.Drawing.Size(65, 23);
            this.btnRemoveArea.TabIndex = 83;
            this.btnRemoveArea.Text = "Remove";
            this.btnRemoveArea.UseVisualStyleBackColor = true;
            this.btnRemoveArea.Click += new System.EventHandler(this.btnRemoveArea_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 80;
            this.label1.Text = "Selected Area Name:";
            this.label1.Visible = false;
            // 
            // txtAreaName
            // 
            this.txtAreaName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAreaName.Location = new System.Drawing.Point(4, 17);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(196, 20);
            this.txtAreaName.TabIndex = 81;
            this.txtAreaName.Visible = false;
            this.txtAreaName.TextChanged += new System.EventHandler(this.txtAreaName_TextChanged_1);
            // 
            // lbxAreas
            // 
            this.lbxAreas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxAreas.FormattingEnabled = true;
            this.lbxAreas.Location = new System.Drawing.Point(4, 54);
            this.lbxAreas.Name = "lbxAreas";
            this.lbxAreas.Size = new System.Drawing.Size(196, 199);
            this.lbxAreas.TabIndex = 79;
            this.lbxAreas.SelectedIndexChanged += new System.EventHandler(this.lbxAreas_SelectedIndexChanged_1);
            this.lbxAreas.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxAreas_MouseDoubleClick);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(39, 29);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(65, 23);
            this.btnRename.TabIndex = 89;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(4, 29);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(35, 23);
            this.btnSort.TabIndex = 90;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.Location = new System.Drawing.Point(69, 4);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(65, 23);
            this.btnDuplicate.TabIndex = 91;
            this.btnDuplicate.Text = "Duplicate";
            this.btnDuplicate.UseVisualStyleBackColor = true;
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // btnLoadAllArea
            // 
            this.btnLoadAllArea.Location = new System.Drawing.Point(139, 29);
            this.btnLoadAllArea.Name = "btnLoadAllArea";
            this.btnLoadAllArea.Size = new System.Drawing.Size(60, 23);
            this.btnLoadAllArea.TabIndex = 92;
            this.btnLoadAllArea.Text = "Load All";
            this.btnLoadAllArea.UseVisualStyleBackColor = true;
            this.btnLoadAllArea.Click += new System.EventHandler(this.btnLoadAllArea_Click);
            // 
            // AreaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 257);
            this.Controls.Add(this.btnLoadAllArea);
            this.Controls.Add(this.btnDuplicate);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnEditArea);
            this.Controls.Add(this.btnAddArea);
            this.Controls.Add(this.btnRemoveArea);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAreaName);
            this.Controls.Add(this.lbxAreas);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(219, 295);
            this.Name = "AreaForm";
            this.Text = "Areas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEditArea;
        private System.Windows.Forms.Button btnAddArea;
        private System.Windows.Forms.Button btnRemoveArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAreaName;
        public System.Windows.Forms.ListBox lbxAreas;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.Button btnLoadAllArea;
    }
}