namespace IB2Toolset
{
    partial class ContainersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContainersForm));
            this.btnEditContainer = new System.Windows.Forms.Button();
            this.btnAddContainer = new System.Windows.Forms.Button();
            this.btnRemoveContainer = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtContainerName = new System.Windows.Forms.TextBox();
            this.lbxContainers = new System.Windows.Forms.ListBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEditContainer
            // 
            this.btnEditContainer.Location = new System.Drawing.Point(134, 29);
            this.btnEditContainer.Name = "btnEditContainer";
            this.btnEditContainer.Size = new System.Drawing.Size(65, 23);
            this.btnEditContainer.TabIndex = 81;
            this.btnEditContainer.Text = "Edit";
            this.btnEditContainer.UseVisualStyleBackColor = true;
            this.btnEditContainer.Click += new System.EventHandler(this.btnEditContainer_Click_1);
            // 
            // btnAddContainer
            // 
            this.btnAddContainer.Location = new System.Drawing.Point(4, 4);
            this.btnAddContainer.Name = "btnAddContainer";
            this.btnAddContainer.Size = new System.Drawing.Size(65, 23);
            this.btnAddContainer.TabIndex = 79;
            this.btnAddContainer.Text = "Add";
            this.btnAddContainer.UseVisualStyleBackColor = true;
            this.btnAddContainer.Click += new System.EventHandler(this.btnAddContainer_Click_1);
            // 
            // btnRemoveContainer
            // 
            this.btnRemoveContainer.Location = new System.Drawing.Point(134, 4);
            this.btnRemoveContainer.Name = "btnRemoveContainer";
            this.btnRemoveContainer.Size = new System.Drawing.Size(65, 23);
            this.btnRemoveContainer.TabIndex = 80;
            this.btnRemoveContainer.Text = "Remove";
            this.btnRemoveContainer.UseVisualStyleBackColor = true;
            this.btnRemoveContainer.Click += new System.EventHandler(this.btnRemoveContainer_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 13);
            this.label4.TabIndex = 77;
            this.label4.Text = "Selected Container Name:";
            this.label4.Visible = false;
            // 
            // txtContainerName
            // 
            this.txtContainerName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContainerName.Location = new System.Drawing.Point(4, 17);
            this.txtContainerName.Name = "txtContainerName";
            this.txtContainerName.Size = new System.Drawing.Size(196, 20);
            this.txtContainerName.TabIndex = 78;
            this.txtContainerName.Visible = false;
            this.txtContainerName.TextChanged += new System.EventHandler(this.txtContainerName_TextChanged_1);
            // 
            // lbxContainers
            // 
            this.lbxContainers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxContainers.FormattingEnabled = true;
            this.lbxContainers.Location = new System.Drawing.Point(4, 54);
            this.lbxContainers.Name = "lbxContainers";
            this.lbxContainers.Size = new System.Drawing.Size(196, 199);
            this.lbxContainers.TabIndex = 76;
            this.lbxContainers.SelectedIndexChanged += new System.EventHandler(this.lbxContainers_SelectedIndexChanged_1);
            this.lbxContainers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxContainers_MouseDoubleClick);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(69, 29);
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
            this.btnSort.Size = new System.Drawing.Size(65, 23);
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
            this.btnDuplicate.TabIndex = 92;
            this.btnDuplicate.Text = "Duplicate";
            this.btnDuplicate.UseVisualStyleBackColor = true;
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // ContainersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 257);
            this.Controls.Add(this.btnDuplicate);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnEditContainer);
            this.Controls.Add(this.btnAddContainer);
            this.Controls.Add(this.btnRemoveContainer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtContainerName);
            this.Controls.Add(this.lbxContainers);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(219, 295);
            this.Name = "ContainersForm";
            this.Text = "Containers";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEditContainer;
        private System.Windows.Forms.Button btnAddContainer;
        private System.Windows.Forms.Button btnRemoveContainer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtContainerName;
        private System.Windows.Forms.ListBox lbxContainers;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnDuplicate;
    }
}