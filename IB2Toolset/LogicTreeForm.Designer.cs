namespace IB2Toolset
{
    /*partial class LogicTreeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogicTreeForm));
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConvoName = new System.Windows.Forms.TextBox();
            this.lbxLogicTrees = new System.Windows.Forms.ListBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.btnLoadAll = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(103, 29);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(35, 23);
            this.btnEdit.TabIndex = 87;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(4, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(65, 23);
            this.btnAdd.TabIndex = 85;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(134, 4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(65, 23);
            this.btnRemove.TabIndex = 86;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "Selected Conversation Name:";
            this.label2.Visible = false;
            // 
            // txtConvoName
            // 
            this.txtConvoName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConvoName.Location = new System.Drawing.Point(4, 17);
            this.txtConvoName.Name = "txtConvoName";
            this.txtConvoName.Size = new System.Drawing.Size(194, 20);
            this.txtConvoName.TabIndex = 84;
            this.txtConvoName.Visible = false;
            // 
            // lbxLogicTrees
            // 
            this.lbxLogicTrees.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxLogicTrees.FormattingEnabled = true;
            this.lbxLogicTrees.Location = new System.Drawing.Point(4, 54);
            this.lbxLogicTrees.Name = "lbxLogicTrees";
            this.lbxLogicTrees.Size = new System.Drawing.Size(196, 199);
            this.lbxLogicTrees.TabIndex = 82;
            this.lbxLogicTrees.SelectedIndexChanged += new System.EventHandler(this.lbxLogicTrees_SelectedIndexChanged);
            this.lbxLogicTrees.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxLogicTrees_MouseDoubleClick);
            // 
            // btnRename
            // 
            this.btnRename.Location = new System.Drawing.Point(40, 29);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(61, 23);
            this.btnRename.TabIndex = 88;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnSort
            // 
            this.btnSort.Location = new System.Drawing.Point(4, 29);
            this.btnSort.Name = "btnSort";
            this.btnSort.Size = new System.Drawing.Size(35, 23);
            this.btnSort.TabIndex = 89;
            this.btnSort.Text = "Sort";
            this.btnSort.UseVisualStyleBackColor = true;
            this.btnSort.Click += new System.EventHandler(this.btnSort_Click);
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.Location = new System.Drawing.Point(69, 4);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(65, 23);
            this.btnDuplicate.TabIndex = 90;
            this.btnDuplicate.Text = "Duplicate";
            this.btnDuplicate.UseVisualStyleBackColor = true;
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // btnLoadAll
            // 
            this.btnLoadAll.Location = new System.Drawing.Point(139, 29);
            this.btnLoadAll.Name = "btnLoadAll";
            this.btnLoadAll.Size = new System.Drawing.Size(60, 23);
            this.btnLoadAll.TabIndex = 91;
            this.btnLoadAll.Text = "LoadAll";
            this.btnLoadAll.UseVisualStyleBackColor = true;
            this.btnLoadAll.Click += new System.EventHandler(this.btnLoadAll_Click);
            // 
            // LogicTreeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 257);
            this.Controls.Add(this.btnLoadAll);
            this.Controls.Add(this.btnDuplicate);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConvoName);
            this.Controls.Add(this.lbxLogicTrees);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(219, 295);
            this.Name = "LogicTreeForm";
            this.Text = "LogicTrees";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConvoName;
        public System.Windows.Forms.ListBox lbxLogicTrees;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.Button btnLoadAll;
    }
    */
}