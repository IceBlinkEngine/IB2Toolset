namespace IBBToolset
{
    partial class SpriteEditor
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.numSize = new System.Windows.Forms.NumericUpDown();
            this.txtSpriteSheetFilename = new System.Windows.Forms.TextBox();
            this.btnSelectImage = new System.Windows.Forms.Button();
            this.txtTag = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAnimated = new System.Windows.Forms.CheckBox();
            this.chkShow = new System.Windows.Forms.CheckBox();
            this.chkLooping = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label13 = new System.Windows.Forms.Label();
            this.numIdleDelay = new System.Windows.Forms.NumericUpDown();
            this.numFPS = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtnProp = new System.Windows.Forms.RadioButton();
            this.rbtnActor = new System.Windows.Forms.RadioButton();
            this.numLayoutRows = new System.Windows.Forms.NumericUpDown();
            this.numLayoutColumns = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numNumOfFrames = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.numIdleNoF = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.numIdleFPS = new System.Windows.Forms.NumericUpDown();
            this.numAttackingNoF = new System.Windows.Forms.NumericUpDown();
            this.numWalkingNoF = new System.Windows.Forms.NumericUpDown();
            this.numAttackingFPS = new System.Windows.Forms.NumericUpDown();
            this.numWalkingFPS = new System.Windows.Forms.NumericUpDown();
            this.pbActor = new System.Windows.Forms.PictureBox();
            this.btnIdlePlay = new System.Windows.Forms.Button();
            this.btnAttackingPlay = new System.Windows.Forms.Button();
            this.btnWalkingPlay = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.gbProp = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnPropPlay = new System.Windows.Forms.Button();
            this.pbProp = new System.Windows.Forms.PictureBox();
            this.gbActor = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnIdleLoop = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.rbtn64 = new System.Windows.Forms.RadioButton();
            this.rbtn128 = new System.Windows.Forms.RadioButton();
            this.rbtn192 = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdleDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLayoutRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLayoutColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNumOfFrames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdleNoF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdleFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAttackingNoF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkingNoF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAttackingFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkingFPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActor)).BeginInit();
            this.gbProp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProp)).BeginInit();
            this.gbActor.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Size (pixels):";
            this.toolTip1.SetToolTip(this.label1, "This is the size of each\r\nframe and assumes that\r\nthe frames are square.");
            // 
            // numSize
            // 
            this.numSize.Location = new System.Drawing.Point(99, 13);
            this.numSize.Maximum = new decimal(new int[] {
            192,
            0,
            0,
            0});
            this.numSize.Name = "numSize";
            this.numSize.Size = new System.Drawing.Size(40, 20);
            this.numSize.TabIndex = 1;
            this.numSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.numSize, "This is the size of each");
            this.numSize.ValueChanged += new System.EventHandler(this.numSize_ValueChanged);
            // 
            // txtSpriteSheetFilename
            // 
            this.txtSpriteSheetFilename.BackColor = System.Drawing.Color.White;
            this.txtSpriteSheetFilename.Location = new System.Drawing.Point(99, 45);
            this.txtSpriteSheetFilename.Name = "txtSpriteSheetFilename";
            this.txtSpriteSheetFilename.ReadOnly = true;
            this.txtSpriteSheetFilename.Size = new System.Drawing.Size(146, 20);
            this.txtSpriteSheetFilename.TabIndex = 2;
            this.txtSpriteSheetFilename.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnSelectImage
            // 
            this.btnSelectImage.Location = new System.Drawing.Point(10, 42);
            this.btnSelectImage.Name = "btnSelectImage";
            this.btnSelectImage.Size = new System.Drawing.Size(85, 23);
            this.btnSelectImage.TabIndex = 4;
            this.btnSelectImage.Text = "Select Image";
            this.btnSelectImage.UseVisualStyleBackColor = true;
            this.btnSelectImage.Click += new System.EventHandler(this.btnSelectImage_Click);
            // 
            // txtTag
            // 
            this.txtTag.Location = new System.Drawing.Point(99, 75);
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(146, 20);
            this.txtTag.TabIndex = 5;
            this.txtTag.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTag.TextChanged += new System.EventHandler(this.txtTag_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tag:";
            // 
            // chkAnimated
            // 
            this.chkAnimated.AutoSize = true;
            this.chkAnimated.Location = new System.Drawing.Point(17, 23);
            this.chkAnimated.Name = "chkAnimated";
            this.chkAnimated.Size = new System.Drawing.Size(69, 17);
            this.chkAnimated.TabIndex = 7;
            this.chkAnimated.Text = "animated";
            this.toolTip1.SetToolTip(this.chkAnimated, "checked = animated\r\nunchecked = static image");
            this.chkAnimated.UseVisualStyleBackColor = true;
            this.chkAnimated.CheckedChanged += new System.EventHandler(this.chkAnimated_CheckedChanged);
            // 
            // chkShow
            // 
            this.chkShow.AutoSize = true;
            this.chkShow.Location = new System.Drawing.Point(17, 40);
            this.chkShow.Name = "chkShow";
            this.chkShow.Size = new System.Drawing.Size(51, 17);
            this.chkShow.TabIndex = 8;
            this.chkShow.Text = "show";
            this.toolTip1.SetToolTip(this.chkShow, "checked = can see the sprite if in line-of-sight and visibility range\r\nunchecked " +
                    "= currently hidden from party");
            this.chkShow.UseVisualStyleBackColor = true;
            this.chkShow.CheckedChanged += new System.EventHandler(this.chkShow_CheckedChanged);
            // 
            // chkLooping
            // 
            this.chkLooping.AutoSize = true;
            this.chkLooping.Location = new System.Drawing.Point(17, 57);
            this.chkLooping.Name = "chkLooping";
            this.chkLooping.Size = new System.Drawing.Size(60, 17);
            this.chkLooping.TabIndex = 9;
            this.chkLooping.Text = "looping";
            this.toolTip1.SetToolTip(this.chkLooping, "checked = animation will loop indefinitely\r\nunchecked = animation will play one t" +
                    "ime through");
            this.chkLooping.UseVisualStyleBackColor = true;
            this.chkLooping.CheckedChanged += new System.EventHandler(this.chkLooping_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 140);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 13);
            this.label13.TabIndex = 37;
            this.label13.Text = "Delay Between Idle:";
            this.toolTip1.SetToolTip(this.label13, "This is the time between the end\r\nof playing the Idle animation\r\nand the next tim" +
                    "e the Idle\r\nanimation plays again.");
            // 
            // numIdleDelay
            // 
            this.numIdleDelay.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numIdleDelay.Location = new System.Drawing.Point(124, 136);
            this.numIdleDelay.Maximum = new decimal(new int[] {
            30000,
            0,
            0,
            0});
            this.numIdleDelay.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numIdleDelay.Name = "numIdleDelay";
            this.numIdleDelay.Size = new System.Drawing.Size(82, 20);
            this.numIdleDelay.TabIndex = 38;
            this.numIdleDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numIdleDelay.ThousandsSeparator = true;
            this.toolTip1.SetToolTip(this.numIdleDelay, "This is the time between the end\r\nof playing the Idle animation\r\nand the next tim" +
                    "e the Idle\r\nanimation plays again.");
            this.numIdleDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numIdleDelay.ValueChanged += new System.EventHandler(this.numIdleDelay_ValueChanged);
            // 
            // numFPS
            // 
            this.numFPS.Location = new System.Drawing.Point(106, 140);
            this.numFPS.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numFPS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFPS.Name = "numFPS";
            this.numFPS.Size = new System.Drawing.Size(40, 20);
            this.numFPS.TabIndex = 10;
            this.numFPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numFPS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numFPS.ValueChanged += new System.EventHandler(this.numFPS_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(73, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "FPS:";
            // 
            // rbtnProp
            // 
            this.rbtnProp.AutoSize = true;
            this.rbtnProp.Location = new System.Drawing.Point(12, 110);
            this.rbtnProp.Name = "rbtnProp";
            this.rbtnProp.Size = new System.Drawing.Size(47, 17);
            this.rbtnProp.TabIndex = 12;
            this.rbtnProp.TabStop = true;
            this.rbtnProp.Text = "Prop";
            this.rbtnProp.UseVisualStyleBackColor = true;
            this.rbtnProp.CheckedChanged += new System.EventHandler(this.rbtnProp_CheckedChanged);
            // 
            // rbtnActor
            // 
            this.rbtnActor.AutoSize = true;
            this.rbtnActor.Location = new System.Drawing.Point(277, 110);
            this.rbtnActor.Name = "rbtnActor";
            this.rbtnActor.Size = new System.Drawing.Size(50, 17);
            this.rbtnActor.TabIndex = 13;
            this.rbtnActor.TabStop = true;
            this.rbtnActor.Text = "Actor";
            this.rbtnActor.UseVisualStyleBackColor = true;
            this.rbtnActor.CheckedChanged += new System.EventHandler(this.rbtnActor_CheckedChanged);
            // 
            // numLayoutRows
            // 
            this.numLayoutRows.Location = new System.Drawing.Point(106, 90);
            this.numLayoutRows.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLayoutRows.Name = "numLayoutRows";
            this.numLayoutRows.Size = new System.Drawing.Size(40, 20);
            this.numLayoutRows.TabIndex = 14;
            this.numLayoutRows.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numLayoutRows.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLayoutRows.ValueChanged += new System.EventHandler(this.numLayoutRows_ValueChanged);
            // 
            // numLayoutColumns
            // 
            this.numLayoutColumns.Location = new System.Drawing.Point(150, 90);
            this.numLayoutColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLayoutColumns.Name = "numLayoutColumns";
            this.numLayoutColumns.Size = new System.Drawing.Size(40, 20);
            this.numLayoutColumns.TabIndex = 15;
            this.numLayoutColumns.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numLayoutColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLayoutColumns.ValueChanged += new System.EventHandler(this.numLayoutColumns_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Image Layout:";
            // 
            // numNumOfFrames
            // 
            this.numNumOfFrames.Location = new System.Drawing.Point(106, 115);
            this.numNumOfFrames.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNumOfFrames.Name = "numNumOfFrames";
            this.numNumOfFrames.Size = new System.Drawing.Size(40, 20);
            this.numNumOfFrames.TabIndex = 18;
            this.numNumOfFrames.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numNumOfFrames.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numNumOfFrames.ValueChanged += new System.EventHandler(this.numNumOfFrames_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Number of Frames:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Idle:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Attacking:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 114);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Walking:";
            // 
            // numIdleNoF
            // 
            this.numIdleNoF.Location = new System.Drawing.Point(64, 63);
            this.numIdleNoF.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIdleNoF.Name = "numIdleNoF";
            this.numIdleNoF.Size = new System.Drawing.Size(40, 20);
            this.numIdleNoF.TabIndex = 22;
            this.numIdleNoF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numIdleNoF.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIdleNoF.ValueChanged += new System.EventHandler(this.numIdleNoF_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(61, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 39);
            this.label9.TabIndex = 23;
            this.label9.Text = "Number\r\nof\r\nFrames";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(124, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(27, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "FPS";
            // 
            // numIdleFPS
            // 
            this.numIdleFPS.Location = new System.Drawing.Point(119, 63);
            this.numIdleFPS.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numIdleFPS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIdleFPS.Name = "numIdleFPS";
            this.numIdleFPS.Size = new System.Drawing.Size(40, 20);
            this.numIdleFPS.TabIndex = 25;
            this.numIdleFPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numIdleFPS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numIdleFPS.ValueChanged += new System.EventHandler(this.numIdleFPS_ValueChanged);
            // 
            // numAttackingNoF
            // 
            this.numAttackingNoF.Location = new System.Drawing.Point(64, 87);
            this.numAttackingNoF.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAttackingNoF.Name = "numAttackingNoF";
            this.numAttackingNoF.Size = new System.Drawing.Size(40, 20);
            this.numAttackingNoF.TabIndex = 26;
            this.numAttackingNoF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numAttackingNoF.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAttackingNoF.ValueChanged += new System.EventHandler(this.numAttackingNoF_ValueChanged);
            // 
            // numWalkingNoF
            // 
            this.numWalkingNoF.Location = new System.Drawing.Point(64, 111);
            this.numWalkingNoF.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWalkingNoF.Name = "numWalkingNoF";
            this.numWalkingNoF.Size = new System.Drawing.Size(40, 20);
            this.numWalkingNoF.TabIndex = 27;
            this.numWalkingNoF.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numWalkingNoF.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWalkingNoF.ValueChanged += new System.EventHandler(this.numWalkingNoF_ValueChanged);
            // 
            // numAttackingFPS
            // 
            this.numAttackingFPS.Location = new System.Drawing.Point(119, 87);
            this.numAttackingFPS.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numAttackingFPS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAttackingFPS.Name = "numAttackingFPS";
            this.numAttackingFPS.Size = new System.Drawing.Size(40, 20);
            this.numAttackingFPS.TabIndex = 28;
            this.numAttackingFPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numAttackingFPS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numAttackingFPS.ValueChanged += new System.EventHandler(this.numAttackingFPS_ValueChanged);
            // 
            // numWalkingFPS
            // 
            this.numWalkingFPS.Location = new System.Drawing.Point(119, 111);
            this.numWalkingFPS.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numWalkingFPS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWalkingFPS.Name = "numWalkingFPS";
            this.numWalkingFPS.Size = new System.Drawing.Size(40, 20);
            this.numWalkingFPS.TabIndex = 29;
            this.numWalkingFPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numWalkingFPS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWalkingFPS.ValueChanged += new System.EventHandler(this.numWalkingFPS_ValueChanged);
            // 
            // pbActor
            // 
            this.pbActor.BackColor = System.Drawing.Color.LightGray;
            this.pbActor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbActor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbActor.Location = new System.Drawing.Point(59, 166);
            this.pbActor.Name = "pbActor";
            this.pbActor.Size = new System.Drawing.Size(197, 197);
            this.pbActor.TabIndex = 30;
            this.pbActor.TabStop = false;
            // 
            // btnIdlePlay
            // 
            this.btnIdlePlay.Location = new System.Drawing.Point(172, 61);
            this.btnIdlePlay.Name = "btnIdlePlay";
            this.btnIdlePlay.Size = new System.Drawing.Size(34, 23);
            this.btnIdlePlay.TabIndex = 31;
            this.btnIdlePlay.Text = "play";
            this.btnIdlePlay.UseVisualStyleBackColor = true;
            this.btnIdlePlay.Click += new System.EventHandler(this.btnIdlePlay_Click);
            // 
            // btnAttackingPlay
            // 
            this.btnAttackingPlay.Location = new System.Drawing.Point(172, 85);
            this.btnAttackingPlay.Name = "btnAttackingPlay";
            this.btnAttackingPlay.Size = new System.Drawing.Size(34, 23);
            this.btnAttackingPlay.TabIndex = 33;
            this.btnAttackingPlay.Text = "play";
            this.btnAttackingPlay.UseVisualStyleBackColor = true;
            this.btnAttackingPlay.Click += new System.EventHandler(this.btnAttackingPlay_Click);
            // 
            // btnWalkingPlay
            // 
            this.btnWalkingPlay.Location = new System.Drawing.Point(172, 109);
            this.btnWalkingPlay.Name = "btnWalkingPlay";
            this.btnWalkingPlay.Size = new System.Drawing.Size(34, 23);
            this.btnWalkingPlay.TabIndex = 35;
            this.btnWalkingPlay.Text = "play";
            this.btnWalkingPlay.UseVisualStyleBackColor = true;
            this.btnWalkingPlay.Click += new System.EventHandler(this.btnWalkingPlay_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(467, 13);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 37;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(467, 42);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 38;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(467, 72);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(467, 101);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(75, 23);
            this.btnSaveAs.TabIndex = 40;
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // gbProp
            // 
            this.gbProp.Controls.Add(this.label12);
            this.gbProp.Controls.Add(this.label11);
            this.gbProp.Controls.Add(this.btnPropPlay);
            this.gbProp.Controls.Add(this.pbProp);
            this.gbProp.Controls.Add(this.chkAnimated);
            this.gbProp.Controls.Add(this.chkShow);
            this.gbProp.Controls.Add(this.chkLooping);
            this.gbProp.Controls.Add(this.numFPS);
            this.gbProp.Controls.Add(this.label3);
            this.gbProp.Controls.Add(this.numLayoutRows);
            this.gbProp.Controls.Add(this.numLayoutColumns);
            this.gbProp.Controls.Add(this.label4);
            this.gbProp.Controls.Add(this.label5);
            this.gbProp.Controls.Add(this.numNumOfFrames);
            this.gbProp.Location = new System.Drawing.Point(12, 133);
            this.gbProp.Name = "gbProp";
            this.gbProp.Size = new System.Drawing.Size(257, 371);
            this.gbProp.TabIndex = 41;
            this.gbProp.TabStop = false;
            this.gbProp.Text = "Prop Sprite";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(148, 74);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 13);
            this.label12.TabIndex = 36;
            this.label12.Text = "columns";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(112, 74);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 35;
            this.label11.Text = "rows";
            // 
            // btnPropPlay
            // 
            this.btnPropPlay.Location = new System.Drawing.Point(7, 166);
            this.btnPropPlay.Name = "btnPropPlay";
            this.btnPropPlay.Size = new System.Drawing.Size(34, 23);
            this.btnPropPlay.TabIndex = 34;
            this.btnPropPlay.Text = "play";
            this.btnPropPlay.UseVisualStyleBackColor = true;
            this.btnPropPlay.Click += new System.EventHandler(this.btnPropPlay_Click);
            // 
            // pbProp
            // 
            this.pbProp.BackColor = System.Drawing.Color.LightGray;
            this.pbProp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbProp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbProp.Location = new System.Drawing.Point(47, 166);
            this.pbProp.Name = "pbProp";
            this.pbProp.Size = new System.Drawing.Size(197, 197);
            this.pbProp.TabIndex = 33;
            this.pbProp.TabStop = false;
            // 
            // gbActor
            // 
            this.gbActor.Controls.Add(this.label14);
            this.gbActor.Controls.Add(this.label13);
            this.gbActor.Controls.Add(this.numIdleDelay);
            this.gbActor.Controls.Add(this.label6);
            this.gbActor.Controls.Add(this.label7);
            this.gbActor.Controls.Add(this.label8);
            this.gbActor.Controls.Add(this.numIdleNoF);
            this.gbActor.Controls.Add(this.label9);
            this.gbActor.Controls.Add(this.label10);
            this.gbActor.Controls.Add(this.numIdleFPS);
            this.gbActor.Controls.Add(this.btnWalkingPlay);
            this.gbActor.Controls.Add(this.numAttackingNoF);
            this.gbActor.Controls.Add(this.numWalkingNoF);
            this.gbActor.Controls.Add(this.btnAttackingPlay);
            this.gbActor.Controls.Add(this.numAttackingFPS);
            this.gbActor.Controls.Add(this.btnIdleLoop);
            this.gbActor.Controls.Add(this.numWalkingFPS);
            this.gbActor.Controls.Add(this.btnIdlePlay);
            this.gbActor.Controls.Add(this.pbActor);
            this.gbActor.Location = new System.Drawing.Point(277, 133);
            this.gbActor.Name = "gbActor";
            this.gbActor.Size = new System.Drawing.Size(270, 371);
            this.gbActor.TabIndex = 42;
            this.gbActor.TabStop = false;
            this.gbActor.Text = "Actor Sprite";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(209, 140);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 13);
            this.label14.TabIndex = 39;
            this.label14.Text = "(ms)";
            // 
            // btnIdleLoop
            // 
            this.btnIdleLoop.Location = new System.Drawing.Point(209, 61);
            this.btnIdleLoop.Name = "btnIdleLoop";
            this.btnIdleLoop.Size = new System.Drawing.Size(37, 23);
            this.btnIdleLoop.TabIndex = 32;
            this.btnIdleLoop.Text = "loop";
            this.btnIdleLoop.UseVisualStyleBackColor = true;
            this.btnIdleLoop.Click += new System.EventHandler(this.btnIdleLoop_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbtn192);
            this.groupBox1.Controls.Add(this.rbtn128);
            this.groupBox1.Controls.Add(this.rbtn64);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numSize);
            this.groupBox1.Controls.Add(this.txtSpriteSheetFilename);
            this.groupBox1.Controls.Add(this.btnSelectImage);
            this.groupBox1.Controls.Add(this.txtTag);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(342, 100);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // rbtn64
            // 
            this.rbtn64.AutoSize = true;
            this.rbtn64.Location = new System.Drawing.Point(258, 21);
            this.rbtn64.Name = "rbtn64";
            this.rbtn64.Size = new System.Drawing.Size(62, 17);
            this.rbtn64.TabIndex = 7;
            this.rbtn64.TabStop = true;
            this.rbtn64.Text = "64 X 64";
            this.rbtn64.UseVisualStyleBackColor = true;
            this.rbtn64.CheckedChanged += new System.EventHandler(this.rbtn64_CheckedChanged);
            // 
            // rbtn128
            // 
            this.rbtn128.AutoSize = true;
            this.rbtn128.Location = new System.Drawing.Point(258, 46);
            this.rbtn128.Name = "rbtn128";
            this.rbtn128.Size = new System.Drawing.Size(74, 17);
            this.rbtn128.TabIndex = 8;
            this.rbtn128.TabStop = true;
            this.rbtn128.Text = "128 X 128";
            this.rbtn128.UseVisualStyleBackColor = true;
            this.rbtn128.CheckedChanged += new System.EventHandler(this.rbtn128_CheckedChanged);
            // 
            // rbtn192
            // 
            this.rbtn192.AutoSize = true;
            this.rbtn192.Location = new System.Drawing.Point(258, 71);
            this.rbtn192.Name = "rbtn192";
            this.rbtn192.Size = new System.Drawing.Size(74, 17);
            this.rbtn192.TabIndex = 9;
            this.rbtn192.TabStop = true;
            this.rbtn192.Text = "192 X 192";
            this.rbtn192.UseVisualStyleBackColor = true;
            this.rbtn192.CheckedChanged += new System.EventHandler(this.rbtn192_CheckedChanged);
            // 
            // SpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 514);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbActor);
            this.Controls.Add(this.gbProp);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.rbtnActor);
            this.Controls.Add(this.rbtnProp);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(576, 552);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(576, 552);
            this.Name = "SpriteEditor";
            this.Text = "SpriteEditor";
            ((System.ComponentModel.ISupportInitialize)(this.numSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdleDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLayoutRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLayoutColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numNumOfFrames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdleNoF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numIdleFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAttackingNoF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkingNoF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAttackingFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWalkingFPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbActor)).EndInit();
            this.gbProp.ResumeLayout(false);
            this.gbProp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbProp)).EndInit();
            this.gbActor.ResumeLayout(false);
            this.gbActor.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numSize;
        private System.Windows.Forms.TextBox txtSpriteSheetFilename;
        private System.Windows.Forms.Button btnSelectImage;
        private System.Windows.Forms.TextBox txtTag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAnimated;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chkShow;
        private System.Windows.Forms.CheckBox chkLooping;
        private System.Windows.Forms.NumericUpDown numFPS;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbtnProp;
        private System.Windows.Forms.RadioButton rbtnActor;
        private System.Windows.Forms.NumericUpDown numLayoutRows;
        private System.Windows.Forms.NumericUpDown numLayoutColumns;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numNumOfFrames;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numIdleNoF;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numIdleFPS;
        private System.Windows.Forms.NumericUpDown numAttackingNoF;
        private System.Windows.Forms.NumericUpDown numWalkingNoF;
        private System.Windows.Forms.NumericUpDown numAttackingFPS;
        private System.Windows.Forms.NumericUpDown numWalkingFPS;
        private System.Windows.Forms.PictureBox pbActor;
        private System.Windows.Forms.Button btnIdlePlay;
        private System.Windows.Forms.Button btnAttackingPlay;
        private System.Windows.Forms.Button btnWalkingPlay;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.GroupBox gbProp;
        private System.Windows.Forms.GroupBox gbActor;
        private System.Windows.Forms.Button btnPropPlay;
        private System.Windows.Forms.PictureBox pbProp;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numIdleDelay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button btnIdleLoop;
        private System.Windows.Forms.RadioButton rbtn192;
        private System.Windows.Forms.RadioButton rbtn128;
        private System.Windows.Forms.RadioButton rbtn64;
    }
}