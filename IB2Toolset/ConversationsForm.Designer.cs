namespace IB2Toolset
{
    partial class ConversationsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConversationsForm));
            this.btnEditConvo = new System.Windows.Forms.Button();
            this.btnAddConvo = new System.Windows.Forms.Button();
            this.btnRemoveConvo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConvoName = new System.Windows.Forms.TextBox();
            this.lbxConvos = new System.Windows.Forms.ListBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.btnLoadAllConvo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEditConvo
            // 
            this.btnEditConvo.Location = new System.Drawing.Point(103, 29);
            this.btnEditConvo.Name = "btnEditConvo";
            this.btnEditConvo.Size = new System.Drawing.Size(35, 23);
            this.btnEditConvo.TabIndex = 87;
            this.btnEditConvo.Text = "Edit";
            this.btnEditConvo.UseVisualStyleBackColor = true;
            this.btnEditConvo.Click += new System.EventHandler(this.btnEditConvo_Click_1);
            // 
            // btnAddConvo
            // 
            this.btnAddConvo.Location = new System.Drawing.Point(4, 4);
            this.btnAddConvo.Name = "btnAddConvo";
            this.btnAddConvo.Size = new System.Drawing.Size(65, 23);
            this.btnAddConvo.TabIndex = 85;
            this.btnAddConvo.Text = "Add";
            this.btnAddConvo.UseVisualStyleBackColor = true;
            this.btnAddConvo.Click += new System.EventHandler(this.btnAddConvo_Click_1);
            // 
            // btnRemoveConvo
            // 
            this.btnRemoveConvo.Location = new System.Drawing.Point(134, 4);
            this.btnRemoveConvo.Name = "btnRemoveConvo";
            this.btnRemoveConvo.Size = new System.Drawing.Size(65, 23);
            this.btnRemoveConvo.TabIndex = 86;
            this.btnRemoveConvo.Text = "Remove";
            this.btnRemoveConvo.UseVisualStyleBackColor = true;
            this.btnRemoveConvo.Click += new System.EventHandler(this.btnRemoveConvo_Click_1);
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
            this.txtConvoName.TextChanged += new System.EventHandler(this.txtConvoName_TextChanged_1);
            // 
            // lbxConvos
            // 
            this.lbxConvos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxConvos.FormattingEnabled = true;
            this.lbxConvos.Location = new System.Drawing.Point(4, 54);
            this.lbxConvos.Name = "lbxConvos";
            this.lbxConvos.Size = new System.Drawing.Size(196, 199);
            this.lbxConvos.TabIndex = 82;
            this.lbxConvos.SelectedIndexChanged += new System.EventHandler(this.lbxConvos_SelectedIndexChanged_1);
            this.lbxConvos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxConvos_MouseDoubleClick);
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
            // btnLoadAllConvo
            // 
            this.btnLoadAllConvo.Location = new System.Drawing.Point(139, 29);
            this.btnLoadAllConvo.Name = "btnLoadAllConvo";
            this.btnLoadAllConvo.Size = new System.Drawing.Size(60, 23);
            this.btnLoadAllConvo.TabIndex = 91;
            this.btnLoadAllConvo.Text = "LoadAll";
            this.btnLoadAllConvo.UseVisualStyleBackColor = true;
            this.btnLoadAllConvo.Click += new System.EventHandler(this.btnLoadAllConvo_Click);
            // 
            // ConversationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6f, 14f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 257);
            this.Controls.Add(this.btnLoadAllConvo);
            this.Controls.Add(this.btnDuplicate);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnEditConvo);
            this.Controls.Add(this.btnAddConvo);
            this.Controls.Add(this.btnRemoveConvo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConvoName);
            this.Controls.Add(this.lbxConvos);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(219, 295);
            this.Name = "ConversationsForm";
            this.Text = "Conversations";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEditConvo;
        private System.Windows.Forms.Button btnAddConvo;
        private System.Windows.Forms.Button btnRemoveConvo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtConvoName;
        public System.Windows.Forms.ListBox lbxConvos;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.Button btnLoadAllConvo;
    }
}