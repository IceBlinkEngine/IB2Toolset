namespace IBBToolset
{
    partial class EncounterEditor
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
            this.EcounterEditorPanel = new System.Windows.Forms.Panel();
            this.btnProperties = new System.Windows.Forms.Button();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.panel2 = new PanelNoScrollOnFocus();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDeleteAllPCStartPoints = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemoveSelectedCreature = new System.Windows.Forms.Button();
            this.txtSelectedIconInfo = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbItems = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxItems = new System.Windows.Forms.ListBox();
            this.btnAddItems = new System.Windows.Forms.Button();
            this.btnRemoveItems = new System.Windows.Forms.Button();
            this.btnPlacePC = new System.Windows.Forms.Button();
            this.btnLoadEncounter = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMouseInfo = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.EcounterEditorPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EcounterEditorPanel
            // 
            this.EcounterEditorPanel.Controls.Add(this.btnProperties);
            this.EcounterEditorPanel.Controls.Add(this.chkGrid);
            this.EcounterEditorPanel.Controls.Add(this.panel2);
            this.EcounterEditorPanel.Controls.Add(this.btnDeleteAllPCStartPoints);
            this.EcounterEditorPanel.Controls.Add(this.groupBox1);
            this.EcounterEditorPanel.Controls.Add(this.groupBox3);
            this.EcounterEditorPanel.Controls.Add(this.btnPlacePC);
            this.EcounterEditorPanel.Controls.Add(this.btnLoadEncounter);
            this.EcounterEditorPanel.Controls.Add(this.panel1);
            this.EcounterEditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EcounterEditorPanel.Location = new System.Drawing.Point(0, 0);
            this.EcounterEditorPanel.Name = "EcounterEditorPanel";
            this.EcounterEditorPanel.Size = new System.Drawing.Size(1006, 742);
            this.EcounterEditorPanel.TabIndex = 16;
            // 
            // btnProperties
            // 
            this.btnProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProperties.Location = new System.Drawing.Point(8, 515);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(130, 48);
            this.btnProperties.TabIndex = 73;
            this.btnProperties.Text = "Encounter Properties";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // chkGrid
            // 
            this.chkGrid.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkGrid.Checked = true;
            this.chkGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGrid.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chkGrid.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.chkGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGrid.Location = new System.Drawing.Point(141, 515);
            this.chkGrid.Margin = new System.Windows.Forms.Padding(0);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.Size = new System.Drawing.Size(48, 48);
            this.chkGrid.TabIndex = 69;
            this.chkGrid.Text = "GRID";
            this.chkGrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkGrid.UseVisualStyleBackColor = true;
            this.chkGrid.CheckedChanged += new System.EventHandler(this.chkGrid_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(195, 7);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(805, 729);
            this.panel2.TabIndex = 68;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(800, 800);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox1_PreviewKeyDown);
            // 
            // btnDeleteAllPCStartPoints
            // 
            this.btnDeleteAllPCStartPoints.Location = new System.Drawing.Point(21, 89);
            this.btnDeleteAllPCStartPoints.Name = "btnDeleteAllPCStartPoints";
            this.btnDeleteAllPCStartPoints.Size = new System.Drawing.Size(152, 27);
            this.btnDeleteAllPCStartPoints.TabIndex = 67;
            this.btnDeleteAllPCStartPoints.Text = "Delete All PC Start Points";
            this.btnDeleteAllPCStartPoints.UseVisualStyleBackColor = true;
            this.btnDeleteAllPCStartPoints.Click += new System.EventHandler(this.btnDeleteAllPCStartPoints_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemoveSelectedCreature);
            this.groupBox1.Controls.Add(this.txtSelectedIconInfo);
            this.groupBox1.Location = new System.Drawing.Point(8, 182);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 100);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Icon Info";
            // 
            // btnRemoveSelectedCreature
            // 
            this.btnRemoveSelectedCreature.Location = new System.Drawing.Point(6, 74);
            this.btnRemoveSelectedCreature.Name = "btnRemoveSelectedCreature";
            this.btnRemoveSelectedCreature.Size = new System.Drawing.Size(169, 23);
            this.btnRemoveSelectedCreature.TabIndex = 1;
            this.btnRemoveSelectedCreature.Text = "remove last selected creature";
            this.btnRemoveSelectedCreature.UseVisualStyleBackColor = true;
            this.btnRemoveSelectedCreature.Click += new System.EventHandler(this.btnRemoveSelectedCreature_Click);
            // 
            // txtSelectedIconInfo
            // 
            this.txtSelectedIconInfo.Location = new System.Drawing.Point(6, 19);
            this.txtSelectedIconInfo.Multiline = true;
            this.txtSelectedIconInfo.Name = "txtSelectedIconInfo";
            this.txtSelectedIconInfo.Size = new System.Drawing.Size(169, 54);
            this.txtSelectedIconInfo.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbItems);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.lbxItems);
            this.groupBox3.Controls.Add(this.btnAddItems);
            this.groupBox3.Controls.Add(this.btnRemoveItems);
            this.groupBox3.Location = new System.Drawing.Point(8, 297);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(181, 210);
            this.groupBox3.TabIndex = 65;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Encounter Drop Items";
            // 
            // cmbItems
            // 
            this.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItems.FormattingEnabled = true;
            this.cmbItems.Location = new System.Drawing.Point(12, 45);
            this.cmbItems.Name = "cmbItems";
            this.cmbItems.Size = new System.Drawing.Size(150, 21);
            this.cmbItems.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 26);
            this.label2.TabIndex = 62;
            this.label2.Text = "Select an Item to Add to the \r\nEncounter drop items:";
            // 
            // lbxItems
            // 
            this.lbxItems.FormattingEnabled = true;
            this.lbxItems.Location = new System.Drawing.Point(12, 103);
            this.lbxItems.Name = "lbxItems";
            this.lbxItems.ScrollAlwaysVisible = true;
            this.lbxItems.Size = new System.Drawing.Size(150, 95);
            this.lbxItems.TabIndex = 39;
            // 
            // btnAddItems
            // 
            this.btnAddItems.Location = new System.Drawing.Point(12, 73);
            this.btnAddItems.Name = "btnAddItems";
            this.btnAddItems.Size = new System.Drawing.Size(70, 23);
            this.btnAddItems.TabIndex = 40;
            this.btnAddItems.Text = "Add";
            this.btnAddItems.UseVisualStyleBackColor = true;
            this.btnAddItems.Click += new System.EventHandler(this.btnAddItems_Click);
            // 
            // btnRemoveItems
            // 
            this.btnRemoveItems.Location = new System.Drawing.Point(88, 73);
            this.btnRemoveItems.Name = "btnRemoveItems";
            this.btnRemoveItems.Size = new System.Drawing.Size(74, 23);
            this.btnRemoveItems.TabIndex = 41;
            this.btnRemoveItems.Text = "Remove";
            this.btnRemoveItems.UseVisualStyleBackColor = true;
            this.btnRemoveItems.Click += new System.EventHandler(this.btnRemoveItems_Click);
            // 
            // btnPlacePC
            // 
            this.btnPlacePC.Location = new System.Drawing.Point(21, 56);
            this.btnPlacePC.Name = "btnPlacePC";
            this.btnPlacePC.Size = new System.Drawing.Size(152, 27);
            this.btnPlacePC.TabIndex = 13;
            this.btnPlacePC.Text = "Place PC Start Points";
            this.btnPlacePC.UseVisualStyleBackColor = true;
            this.btnPlacePC.Click += new System.EventHandler(this.btnPlacePC_Click);
            // 
            // btnLoadEncounter
            // 
            this.btnLoadEncounter.Location = new System.Drawing.Point(21, 15);
            this.btnLoadEncounter.Name = "btnLoadEncounter";
            this.btnLoadEncounter.Size = new System.Drawing.Size(152, 36);
            this.btnLoadEncounter.TabIndex = 11;
            this.btnLoadEncounter.Text = "         Load Area File         (previously created area file)";
            this.btnLoadEncounter.UseVisualStyleBackColor = true;
            this.btnLoadEncounter.Click += new System.EventHandler(this.btnLoadEncounter_Click);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.lblMouseInfo);
            this.panel1.Location = new System.Drawing.Point(8, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(181, 29);
            this.panel1.TabIndex = 1;
            // 
            // lblMouseInfo
            // 
            this.lblMouseInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMouseInfo.AutoSize = true;
            this.lblMouseInfo.Location = new System.Drawing.Point(2, 13);
            this.lblMouseInfo.Name = "lblMouseInfo";
            this.lblMouseInfo.Size = new System.Drawing.Size(85, 13);
            this.lblMouseInfo.TabIndex = 0;
            this.lblMouseInfo.Text = "CURSOR/GRID";
            this.lblMouseInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // EncounterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1006, 742);
            this.Controls.Add(this.EcounterEditorPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EncounterEditor";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.Text = "EncounterEditor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EncounterEditor_FormClosed);
            this.Load += new System.EventHandler(this.EncounterEditor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EncounterEditor_KeyDown);
            this.EcounterEditorPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel EcounterEditorPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMouseInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnLoadEncounter;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnPlacePC;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxItems;
        private System.Windows.Forms.Button btnAddItems;
        private System.Windows.Forms.Button btnRemoveItems;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSelectedIconInfo;
        private System.Windows.Forms.Button btnDeleteAllPCStartPoints;
        private System.Windows.Forms.Button btnRemoveSelectedCreature;
        private PanelNoScrollOnFocus panel2;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.Button btnProperties;
    }
}