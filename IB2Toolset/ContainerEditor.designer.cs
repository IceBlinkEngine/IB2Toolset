namespace IB2miniToolset
{
    partial class ContainerEditor
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbItems = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxItems = new System.Windows.Forms.ListBox();
            this.btnAddItems = new System.Windows.Forms.Button();
            this.btnRemoveItems = new System.Windows.Forms.Button();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbItems);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.lbxItems);
            this.groupBox3.Controls.Add(this.btnAddItems);
            this.groupBox3.Controls.Add(this.btnRemoveItems);
            this.groupBox3.Location = new System.Drawing.Point(12, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(210, 295);
            this.groupBox3.TabIndex = 64;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Container Items";
            // 
            // cmbItems
            // 
            this.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItems.FormattingEnabled = true;
            this.cmbItems.Location = new System.Drawing.Point(12, 45);
            this.cmbItems.Name = "cmbItems";
            this.cmbItems.Size = new System.Drawing.Size(187, 21);
            this.cmbItems.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(190, 13);
            this.label2.TabIndex = 62;
            this.label2.Text = "Select an Item to Add to the Container:";
            // 
            // lbxItems
            // 
            this.lbxItems.FormattingEnabled = true;
            this.lbxItems.Location = new System.Drawing.Point(12, 103);
            this.lbxItems.Name = "lbxItems";
            this.lbxItems.ScrollAlwaysVisible = true;
            this.lbxItems.Size = new System.Drawing.Size(187, 186);
            this.lbxItems.TabIndex = 39;
            // 
            // btnAddItems
            // 
            this.btnAddItems.Location = new System.Drawing.Point(12, 73);
            this.btnAddItems.Name = "btnAddItems";
            this.btnAddItems.Size = new System.Drawing.Size(94, 23);
            this.btnAddItems.TabIndex = 40;
            this.btnAddItems.Text = "Add";
            this.btnAddItems.UseVisualStyleBackColor = true;
            this.btnAddItems.Click += new System.EventHandler(this.btnAddItems_Click);
            // 
            // btnRemoveItems
            // 
            this.btnRemoveItems.Location = new System.Drawing.Point(112, 73);
            this.btnRemoveItems.Name = "btnRemoveItems";
            this.btnRemoveItems.Size = new System.Drawing.Size(87, 23);
            this.btnRemoveItems.TabIndex = 41;
            this.btnRemoveItems.Text = "Remove";
            this.btnRemoveItems.UseVisualStyleBackColor = true;
            this.btnRemoveItems.Click += new System.EventHandler(this.btnRemoveItems_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // ContainerEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 311);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ContainerEditor";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float;
            this.Text = "Container Wizard";
            this.Load += new System.EventHandler(this.ContainerEditor_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxItems;
        private System.Windows.Forms.Button btnAddItems;
        private System.Windows.Forms.Button btnRemoveItems;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}

