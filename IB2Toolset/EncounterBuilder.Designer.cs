namespace IB2Toolset
{
    partial class EncounterBuilder
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
            this.panelView = new System.Windows.Forms.Panel();
            this.btnFillWithSelected = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDuplicate = new System.Windows.Forms.Button();
            this.btnAddEncounter = new System.Windows.Forms.Button();
            this.btnRemoveEncounter = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.lbxEncounters = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbxCreatures = new System.Windows.Forms.ListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.btnPlacePCs = new System.Windows.Forms.Button();
            this.btnDeletePCs = new System.Windows.Forms.Button();
            this.btnDeleteCreatures = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbItems = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxItems = new System.Windows.Forms.ListBox();
            this.btnAddItems = new System.Windows.Forms.Button();
            this.btnRemoveItems = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numGold = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.flPanelTab1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.rbtnLoS = new System.Windows.Forms.RadioButton();
            this.rbtnInfo = new System.Windows.Forms.RadioButton();
            this.rbtnWalkable = new System.Windows.Forms.RadioButton();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGold)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelView
            // 
            this.panelView.BackColor = System.Drawing.Color.Gainsboro;
            this.panelView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelView.Location = new System.Drawing.Point(352, 4);
            this.panelView.MinimumSize = new System.Drawing.Size(350, 350);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(356, 356);
            this.panelView.TabIndex = 15;
            this.panelView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelView_MouseDown);
            this.panelView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelView_MouseMove);
            // 
            // btnFillWithSelected
            // 
            this.btnFillWithSelected.Location = new System.Drawing.Point(16, 324);
            this.btnFillWithSelected.Name = "btnFillWithSelected";
            this.btnFillWithSelected.Size = new System.Drawing.Size(86, 39);
            this.btnFillWithSelected.TabIndex = 17;
            this.btnFillWithSelected.Text = "Fill Map with Selected Tile";
            this.btnFillWithSelected.UseVisualStyleBackColor = true;
            this.btnFillWithSelected.Click += new System.EventHandler(this.btnFillWithSelected_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDuplicate);
            this.groupBox2.Controls.Add(this.btnAddEncounter);
            this.groupBox2.Controls.Add(this.btnRemoveEncounter);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lbxEncounters);
            this.groupBox2.Location = new System.Drawing.Point(13, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(208, 280);
            this.groupBox2.TabIndex = 22;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Encounters";
            // 
            // btnDuplicate
            // 
            this.btnDuplicate.Location = new System.Drawing.Point(71, 18);
            this.btnDuplicate.Name = "btnDuplicate";
            this.btnDuplicate.Size = new System.Drawing.Size(65, 23);
            this.btnDuplicate.TabIndex = 101;
            this.btnDuplicate.Text = "Duplicate";
            this.btnDuplicate.UseVisualStyleBackColor = true;
            this.btnDuplicate.Click += new System.EventHandler(this.btnDuplicate_Click);
            // 
            // btnAddEncounter
            // 
            this.btnAddEncounter.Location = new System.Drawing.Point(6, 18);
            this.btnAddEncounter.Name = "btnAddEncounter";
            this.btnAddEncounter.Size = new System.Drawing.Size(65, 23);
            this.btnAddEncounter.TabIndex = 96;
            this.btnAddEncounter.Text = "Add";
            this.btnAddEncounter.UseVisualStyleBackColor = true;
            this.btnAddEncounter.Click += new System.EventHandler(this.btnAddEncounter_Click_1);
            // 
            // btnRemoveEncounter
            // 
            this.btnRemoveEncounter.Location = new System.Drawing.Point(136, 18);
            this.btnRemoveEncounter.Name = "btnRemoveEncounter";
            this.btnRemoveEncounter.Size = new System.Drawing.Size(65, 23);
            this.btnRemoveEncounter.TabIndex = 97;
            this.btnRemoveEncounter.Text = "Remove";
            this.btnRemoveEncounter.UseVisualStyleBackColor = true;
            this.btnRemoveEncounter.Click += new System.EventHandler(this.btnRemoveEncounter_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 94;
            this.label3.Text = "Selected Encounter Name:";
            this.label3.Visible = false;
            // 
            // lbxEncounters
            // 
            this.lbxEncounters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxEncounters.FormattingEnabled = true;
            this.lbxEncounters.Location = new System.Drawing.Point(6, 46);
            this.lbxEncounters.Name = "lbxEncounters";
            this.lbxEncounters.Size = new System.Drawing.Size(196, 225);
            this.lbxEncounters.TabIndex = 93;
            this.lbxEncounters.SelectedIndexChanged += new System.EventHandler(this.lbxEncounters_SelectedIndexChanged_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lbxCreatures);
            this.groupBox3.Location = new System.Drawing.Point(716, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(154, 261);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Creatures";
            // 
            // lbxCreatures
            // 
            this.lbxCreatures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxCreatures.FormattingEnabled = true;
            this.lbxCreatures.Location = new System.Drawing.Point(6, 15);
            this.lbxCreatures.Name = "lbxCreatures";
            this.lbxCreatures.Size = new System.Drawing.Size(142, 238);
            this.lbxCreatures.TabIndex = 94;
            this.lbxCreatures.SelectedIndexChanged += new System.EventHandler(this.lbxCreatures_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 491);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1172, 22);
            this.statusStrip1.TabIndex = 24;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.propertyGrid1.Location = new System.Drawing.Point(12, 290);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(209, 192);
            this.propertyGrid1.TabIndex = 25;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // btnPlacePCs
            // 
            this.btnPlacePCs.Location = new System.Drawing.Point(6, 19);
            this.btnPlacePCs.Name = "btnPlacePCs";
            this.btnPlacePCs.Size = new System.Drawing.Size(86, 39);
            this.btnPlacePCs.TabIndex = 28;
            this.btnPlacePCs.Text = "Place PCs";
            this.btnPlacePCs.UseVisualStyleBackColor = true;
            this.btnPlacePCs.Click += new System.EventHandler(this.btnPlacePCs_Click);
            // 
            // btnDeletePCs
            // 
            this.btnDeletePCs.Location = new System.Drawing.Point(98, 19);
            this.btnDeletePCs.Name = "btnDeletePCs";
            this.btnDeletePCs.Size = new System.Drawing.Size(86, 39);
            this.btnDeletePCs.TabIndex = 29;
            this.btnDeletePCs.Text = "Clear PCs";
            this.btnDeletePCs.UseVisualStyleBackColor = true;
            this.btnDeletePCs.Click += new System.EventHandler(this.btnDeletePCs_Click);
            // 
            // btnDeleteCreatures
            // 
            this.btnDeleteCreatures.Location = new System.Drawing.Point(190, 19);
            this.btnDeleteCreatures.Name = "btnDeleteCreatures";
            this.btnDeleteCreatures.Size = new System.Drawing.Size(86, 39);
            this.btnDeleteCreatures.TabIndex = 30;
            this.btnDeleteCreatures.Text = "Clear Creatures";
            this.btnDeleteCreatures.UseVisualStyleBackColor = true;
            this.btnDeleteCreatures.Click += new System.EventHandler(this.btnDeleteCreatures_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbItems);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.lbxItems);
            this.groupBox4.Controls.Add(this.btnAddItems);
            this.groupBox4.Controls.Add(this.btnRemoveItems);
            this.groupBox4.Location = new System.Drawing.Point(714, 271);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(156, 210);
            this.groupBox4.TabIndex = 66;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Encounter Drop Items";
            // 
            // cmbItems
            // 
            this.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItems.FormattingEnabled = true;
            this.cmbItems.Location = new System.Drawing.Point(12, 45);
            this.cmbItems.Name = "cmbItems";
            this.cmbItems.Size = new System.Drawing.Size(138, 21);
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
            this.lbxItems.Size = new System.Drawing.Size(138, 95);
            this.lbxItems.TabIndex = 39;
            // 
            // btnAddItems
            // 
            this.btnAddItems.Location = new System.Drawing.Point(12, 73);
            this.btnAddItems.Name = "btnAddItems";
            this.btnAddItems.Size = new System.Drawing.Size(58, 23);
            this.btnAddItems.TabIndex = 40;
            this.btnAddItems.Text = "Add";
            this.btnAddItems.UseVisualStyleBackColor = true;
            this.btnAddItems.Click += new System.EventHandler(this.btnAddItems_Click);
            // 
            // btnRemoveItems
            // 
            this.btnRemoveItems.Location = new System.Drawing.Point(76, 73);
            this.btnRemoveItems.Name = "btnRemoveItems";
            this.btnRemoveItems.Size = new System.Drawing.Size(74, 23);
            this.btnRemoveItems.TabIndex = 41;
            this.btnRemoveItems.Text = "Remove";
            this.btnRemoveItems.UseVisualStyleBackColor = true;
            this.btnRemoveItems.Click += new System.EventHandler(this.btnRemoveItems_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numGold);
            this.groupBox5.Location = new System.Drawing.Point(281, 8);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(74, 55);
            this.groupBox5.TabIndex = 67;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Gold Drop";
            // 
            // numGold
            // 
            this.numGold.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numGold.Location = new System.Drawing.Point(6, 18);
            this.numGold.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numGold.Name = "numGold";
            this.numGold.Size = new System.Drawing.Size(62, 29);
            this.numGold.TabIndex = 0;
            this.numGold.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numGold.ThousandsSeparator = true;
            this.numGold.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.numGold.ValueChanged += new System.EventHandler(this.numGold_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.flPanelTab1);
            this.panel3.Location = new System.Drawing.Point(876, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(285, 469);
            this.panel3.TabIndex = 68;
            // 
            // flPanelTab1
            // 
            this.flPanelTab1.Location = new System.Drawing.Point(3, 3);
            this.flPanelTab1.Name = "flPanelTab1";
            this.flPanelTab1.Size = new System.Drawing.Size(264, 5000);
            this.flPanelTab1.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.panel1);
            this.groupBox6.Location = new System.Drawing.Point(16, 373);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(86, 94);
            this.groupBox6.TabIndex = 69;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Selected Tile";
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel1.Location = new System.Drawing.Point(18, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(50, 50);
            this.panel1.TabIndex = 7;
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Controls.Add(this.radioButton1);
            this.groupBox7.Controls.Add(this.radioButton2);
            this.groupBox7.Location = new System.Drawing.Point(17, 160);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(86, 75);
            this.groupBox7.TabIndex = 70;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Edit Layer";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(10, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 17);
            this.radioButton1.TabIndex = 4;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Layer 1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(10, 44);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 17);
            this.radioButton2.TabIndex = 5;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Layer 2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox8.Controls.Add(this.checkBox1);
            this.groupBox8.Controls.Add(this.checkBox2);
            this.groupBox8.Location = new System.Drawing.Point(17, 241);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(86, 75);
            this.groupBox8.TabIndex = 71;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Show";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(14, 21);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(61, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Layer 1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(14, 46);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(61, 17);
            this.checkBox2.TabIndex = 3;
            this.checkBox2.Text = "Layer 2";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.rbtnLoS);
            this.groupBox9.Controls.Add(this.rbtnInfo);
            this.groupBox9.Controls.Add(this.groupBox8);
            this.groupBox9.Controls.Add(this.groupBox7);
            this.groupBox9.Controls.Add(this.rbtnWalkable);
            this.groupBox9.Controls.Add(this.groupBox6);
            this.groupBox9.Controls.Add(this.btnFillWithSelected);
            this.groupBox9.Location = new System.Drawing.Point(227, 4);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(118, 478);
            this.groupBox9.TabIndex = 72;
            this.groupBox9.TabStop = false;
            // 
            // rbtnLoS
            // 
            this.rbtnLoS.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnLoS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnLoS.Location = new System.Drawing.Point(9, 109);
            this.rbtnLoS.Name = "rbtnLoS";
            this.rbtnLoS.Size = new System.Drawing.Size(103, 45);
            this.rbtnLoS.TabIndex = 5;
            this.rbtnLoS.TabStop = true;
            this.rbtnLoS.Text = "Toggle\r\nLine-of-Sight";
            this.rbtnLoS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnLoS.UseVisualStyleBackColor = true;
            this.rbtnLoS.CheckedChanged += new System.EventHandler(this.rbtnLoS_CheckedChanged);
            // 
            // rbtnInfo
            // 
            this.rbtnInfo.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnInfo.Location = new System.Drawing.Point(9, 15);
            this.rbtnInfo.Name = "rbtnInfo";
            this.rbtnInfo.Size = new System.Drawing.Size(103, 30);
            this.rbtnInfo.TabIndex = 4;
            this.rbtnInfo.TabStop = true;
            this.rbtnInfo.Text = "INFO";
            this.rbtnInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnInfo.UseVisualStyleBackColor = true;
            this.rbtnInfo.CheckedChanged += new System.EventHandler(this.rbtnInfo_CheckedChanged);
            // 
            // rbtnWalkable
            // 
            this.rbtnWalkable.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnWalkable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnWalkable.Location = new System.Drawing.Point(9, 56);
            this.rbtnWalkable.Name = "rbtnWalkable";
            this.rbtnWalkable.Size = new System.Drawing.Size(103, 45);
            this.rbtnWalkable.TabIndex = 3;
            this.rbtnWalkable.TabStop = true;
            this.rbtnWalkable.Text = "Toggle\r\nWalkable";
            this.rbtnWalkable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnWalkable.UseVisualStyleBackColor = true;
            this.rbtnWalkable.CheckedChanged += new System.EventHandler(this.rbtnWalkable_CheckedChanged);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnPlacePCs);
            this.groupBox10.Controls.Add(this.btnDeleteCreatures);
            this.groupBox10.Controls.Add(this.btnDeletePCs);
            this.groupBox10.Controls.Add(this.groupBox5);
            this.groupBox10.Location = new System.Drawing.Point(349, 366);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(361, 70);
            this.groupBox10.TabIndex = 73;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Objects";
            // 
            // EncounterBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1172, 513);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.panelView);
            this.Name = "EncounterBuilder";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.EncounterBuilder_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numGold)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox10.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelView;
        private System.Windows.Forms.Button btnFillWithSelected;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDuplicate;
        private System.Windows.Forms.Button btnAddEncounter;
        private System.Windows.Forms.Button btnRemoveEncounter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbxEncounters;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox lbxCreatures;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Button btnPlacePCs;
        private System.Windows.Forms.Button btnDeletePCs;
        private System.Windows.Forms.Button btnDeleteCreatures;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbItems;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxItems;
        private System.Windows.Forms.Button btnAddItems;
        private System.Windows.Forms.Button btnRemoveItems;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.NumericUpDown numGold;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel flPanelTab1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton rbtnLoS;
        private System.Windows.Forms.RadioButton rbtnInfo;
        private System.Windows.Forms.RadioButton rbtnWalkable;
        private System.Windows.Forms.GroupBox groupBox10;
    }
}

