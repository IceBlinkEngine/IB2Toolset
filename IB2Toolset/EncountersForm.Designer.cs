namespace IB2Toolset
{
    partial class EncountersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EncountersForm));
            this.btnEditEncounter = new System.Windows.Forms.Button();
            this.btnAddEncounter = new System.Windows.Forms.Button();
            this.btnRemoveEncounter = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEncounterName = new System.Windows.Forms.TextBox();
            this.lbxEncounters = new System.Windows.Forms.ListBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnSort = new System.Windows.Forms.Button();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnEditEncounter
            // 
            this.btnEditEncounter.Location = new System.Drawing.Point(134, 29);
            this.btnEditEncounter.Name = "btnEditEncounter";
            this.btnEditEncounter.Size = new System.Drawing.Size(65, 23);
            this.btnEditEncounter.TabIndex = 84;
            this.btnEditEncounter.Text = "Edit";
            this.btnEditEncounter.UseVisualStyleBackColor = true;
            this.btnEditEncounter.Click += new System.EventHandler(this.btnEditEncounter_Click_1);
            // 
            // btnAddEncounter
            // 
            this.btnAddEncounter.Location = new System.Drawing.Point(4, 4);
            this.btnAddEncounter.Name = "btnAddEncounter";
            this.btnAddEncounter.Size = new System.Drawing.Size(65, 23);
            this.btnAddEncounter.TabIndex = 82;
            this.btnAddEncounter.Text = "Add";
            this.btnAddEncounter.UseVisualStyleBackColor = true;
            this.btnAddEncounter.Click += new System.EventHandler(this.btnAddEncounter_Click_1);
            // 
            // btnRemoveEncounter
            // 
            this.btnRemoveEncounter.Location = new System.Drawing.Point(134, 4);
            this.btnRemoveEncounter.Name = "btnRemoveEncounter";
            this.btnRemoveEncounter.Size = new System.Drawing.Size(65, 23);
            this.btnRemoveEncounter.TabIndex = 83;
            this.btnRemoveEncounter.Text = "Remove";
            this.btnRemoveEncounter.UseVisualStyleBackColor = true;
            this.btnRemoveEncounter.Click += new System.EventHandler(this.btnRemoveEncounter_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 80;
            this.label3.Text = "Selected Encounter Name:";
            this.label3.Visible = false;
            // 
            // txtEncounterName
            // 
            this.txtEncounterName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEncounterName.Location = new System.Drawing.Point(4, 17);
            this.txtEncounterName.Name = "txtEncounterName";
            this.txtEncounterName.Size = new System.Drawing.Size(196, 20);
            this.txtEncounterName.TabIndex = 81;
            this.txtEncounterName.Visible = false;
            this.txtEncounterName.TextChanged += new System.EventHandler(this.txtEncounterName_TextChanged_1);
            // 
            // lbxEncounters
            // 
            this.lbxEncounters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxEncounters.FormattingEnabled = true;
            this.lbxEncounters.Location = new System.Drawing.Point(4, 54);
            this.lbxEncounters.Name = "lbxEncounters";
            this.lbxEncounters.Size = new System.Drawing.Size(196, 199);
            this.lbxEncounters.TabIndex = 79;
            this.lbxEncounters.SelectedIndexChanged += new System.EventHandler(this.lbxEncounters_SelectedIndexChanged_1);
            this.lbxEncounters.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbxEncounters_MouseDoubleClick);
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
            // EncountersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 257);
            this.Controls.Add(this.btnDuplicate);
            this.Controls.Add(this.btnSort);
            this.Controls.Add(this.btnRename);
            this.Controls.Add(this.btnEditEncounter);
            this.Controls.Add(this.btnAddEncounter);
            this.Controls.Add(this.btnRemoveEncounter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEncounterName);
            this.Controls.Add(this.lbxEncounters);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(219, 295);
            this.Name = "EncountersForm";
            this.Text = "Encounters";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEditEncounter;
        private System.Windows.Forms.Button btnAddEncounter;
        private System.Windows.Forms.Button btnRemoveEncounter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEncounterName;
        private System.Windows.Forms.ListBox lbxEncounters;
        private System.Windows.Forms.Button btnRename;
        private System.Windows.Forms.Button btnSort;
        private System.Windows.Forms.Button btnDuplicate;
    }
}