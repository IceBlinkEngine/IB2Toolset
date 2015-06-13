namespace IBBToolset
{
    partial class EventObjectSelect
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
            this.cmbObjectTagFilename = new System.Windows.Forms.ComboBox();
            this.rtxtScript = new System.Windows.Forms.RichTextBox();
            this.parm1 = new System.Windows.Forms.TextBox();
            this.parm4 = new System.Windows.Forms.TextBox();
            this.parm3 = new System.Windows.Forms.TextBox();
            this.parm2 = new System.Windows.Forms.TextBox();
            this.gbScriptParms = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gbTransitionLocation = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.transitionY = new System.Windows.Forms.NumericUpDown();
            this.transitionX = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.gbScriptParms.SuspendLayout();
            this.gbTransitionLocation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transitionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.transitionX)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbObjectTagFilename
            // 
            this.cmbObjectTagFilename.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbObjectTagFilename.FormattingEnabled = true;
            this.cmbObjectTagFilename.Items.AddRange(new object[] {
            "none"});
            this.cmbObjectTagFilename.Location = new System.Drawing.Point(299, 12);
            this.cmbObjectTagFilename.Name = "cmbObjectTagFilename";
            this.cmbObjectTagFilename.Size = new System.Drawing.Size(150, 21);
            this.cmbObjectTagFilename.TabIndex = 17;
            this.cmbObjectTagFilename.SelectedIndexChanged += new System.EventHandler(this.cmbObjectTagFilename_SelectedIndexChanged);
            // 
            // rtxtScript
            // 
            this.rtxtScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxtScript.BackColor = System.Drawing.Color.White;
            this.rtxtScript.Location = new System.Drawing.Point(12, 120);
            this.rtxtScript.Name = "rtxtScript";
            this.rtxtScript.ReadOnly = true;
            this.rtxtScript.Size = new System.Drawing.Size(437, 151);
            this.rtxtScript.TabIndex = 18;
            this.rtxtScript.Text = "";
            this.rtxtScript.WordWrap = false;
            // 
            // parm1
            // 
            this.parm1.Location = new System.Drawing.Point(29, 19);
            this.parm1.Name = "parm1";
            this.parm1.Size = new System.Drawing.Size(115, 20);
            this.parm1.TabIndex = 19;
            this.parm1.TextChanged += new System.EventHandler(this.parm1_TextChanged);
            // 
            // parm4
            // 
            this.parm4.Location = new System.Drawing.Point(175, 45);
            this.parm4.Name = "parm4";
            this.parm4.Size = new System.Drawing.Size(115, 20);
            this.parm4.TabIndex = 20;
            this.parm4.TextChanged += new System.EventHandler(this.parm4_TextChanged);
            // 
            // parm3
            // 
            this.parm3.Location = new System.Drawing.Point(29, 45);
            this.parm3.Name = "parm3";
            this.parm3.Size = new System.Drawing.Size(115, 20);
            this.parm3.TabIndex = 21;
            this.parm3.TextChanged += new System.EventHandler(this.parm3_TextChanged);
            // 
            // parm2
            // 
            this.parm2.Location = new System.Drawing.Point(175, 19);
            this.parm2.Name = "parm2";
            this.parm2.Size = new System.Drawing.Size(115, 20);
            this.parm2.TabIndex = 22;
            this.parm2.TextChanged += new System.EventHandler(this.parm2_TextChanged);
            // 
            // gbScriptParms
            // 
            this.gbScriptParms.Controls.Add(this.label4);
            this.gbScriptParms.Controls.Add(this.label3);
            this.gbScriptParms.Controls.Add(this.label2);
            this.gbScriptParms.Controls.Add(this.label1);
            this.gbScriptParms.Controls.Add(this.parm1);
            this.gbScriptParms.Controls.Add(this.parm2);
            this.gbScriptParms.Controls.Add(this.parm4);
            this.gbScriptParms.Controls.Add(this.parm3);
            this.gbScriptParms.Location = new System.Drawing.Point(13, 39);
            this.gbScriptParms.Name = "gbScriptParms";
            this.gbScriptParms.Size = new System.Drawing.Size(300, 74);
            this.gbScriptParms.TabIndex = 23;
            this.gbScriptParms.TabStop = false;
            this.gbScriptParms.Text = "Script Parameters";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "#3:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "#4:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "#2:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "#1:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(171, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 26);
            this.label5.TabIndex = 24;
            this.label5.Text = "Select the Filename or \r\nObject Tag of this Event:";
            // 
            // gbTransitionLocation
            // 
            this.gbTransitionLocation.Controls.Add(this.label7);
            this.gbTransitionLocation.Controls.Add(this.transitionY);
            this.gbTransitionLocation.Controls.Add(this.transitionX);
            this.gbTransitionLocation.Controls.Add(this.label6);
            this.gbTransitionLocation.Location = new System.Drawing.Point(319, 39);
            this.gbTransitionLocation.Name = "gbTransitionLocation";
            this.gbTransitionLocation.Size = new System.Drawing.Size(130, 74);
            this.gbTransitionLocation.TabIndex = 25;
            this.gbTransitionLocation.TabStop = false;
            this.gbTransitionLocation.Text = "Transition Location";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(63, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 13);
            this.label7.TabIndex = 40;
            this.label7.Text = "Y:";
            // 
            // transitionY
            // 
            this.transitionY.Location = new System.Drawing.Point(82, 28);
            this.transitionY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.transitionY.Name = "transitionY";
            this.transitionY.Size = new System.Drawing.Size(34, 20);
            this.transitionY.TabIndex = 39;
            this.transitionY.ValueChanged += new System.EventHandler(this.transitionY_ValueChanged);
            // 
            // transitionX
            // 
            this.transitionX.Location = new System.Drawing.Point(24, 28);
            this.transitionX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.transitionX.Name = "transitionX";
            this.transitionX.Size = new System.Drawing.Size(34, 20);
            this.transitionX.TabIndex = 38;
            this.transitionX.ValueChanged += new System.EventHandler(this.transitionX_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 37;
            this.label6.Text = "X:";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(13, 10);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(144, 23);
            this.btnReset.TabIndex = 26;
            this.btnReset.Text = "Reset Parameters";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // EventObjectSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 283);
            this.ControlBox = false;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.gbTransitionLocation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbScriptParms);
            this.Controls.Add(this.rtxtScript);
            this.Controls.Add(this.cmbObjectTagFilename);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EventObjectSelect";
            this.ShowInTaskbar = false;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EventObjectSelect_FormClosed);
            this.Load += new System.EventHandler(this.EventObjectSelect_Load);
            this.gbScriptParms.ResumeLayout(false);
            this.gbScriptParms.PerformLayout();
            this.gbTransitionLocation.ResumeLayout(false);
            this.gbTransitionLocation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transitionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.transitionX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cmbObjectTagFilename;
        public System.Windows.Forms.RichTextBox rtxtScript;
        public System.Windows.Forms.TextBox parm1;
        public System.Windows.Forms.TextBox parm4;
        public System.Windows.Forms.TextBox parm3;
        public System.Windows.Forms.TextBox parm2;
        private System.Windows.Forms.GroupBox gbScriptParms;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbTransitionLocation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown transitionY;
        private System.Windows.Forms.NumericUpDown transitionX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnReset;

    }
}