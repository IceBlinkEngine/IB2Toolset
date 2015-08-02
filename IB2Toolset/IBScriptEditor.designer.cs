namespace IB2Toolset
{
    partial class IBScriptEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IBScriptEditor));
            this.scintilla1 = new ScintillaNET.Scintilla();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerScript = new System.Windows.Forms.SplitContainer();
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.cmbFunctions = new System.Windows.Forms.ComboBox();
            this.lbxFunctions = new System.Windows.Forms.ListBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsSaveScript = new System.Windows.Forms.ToolStripButton();
            this.tsShowWhiteSpace = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerScript)).BeginInit();
            this.splitContainerScript.Panel1.SuspendLayout();
            this.splitContainerScript.Panel2.SuspendLayout();
            this.splitContainerScript.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scintilla1
            // 
            this.scintilla1.AutoCMaxHeight = 25;
            this.scintilla1.AutoCOrder = ScintillaNET.Order.PerformSort;
            this.scintilla1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintilla1.Location = new System.Drawing.Point(0, 0);
            this.scintilla1.Name = "scintilla1";
            this.scintilla1.Size = new System.Drawing.Size(738, 408);
            this.scintilla1.TabIndex = 1;
            this.scintilla1.TabWidth = 3;
            this.scintilla1.UseTabs = false;
            this.scintilla1.CharAdded += new System.EventHandler<ScintillaNET.CharAddedEventArgs>(this.scintilla1_CharAdded);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.Location = new System.Drawing.Point(0, 28);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerScript);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.cmbFunctions);
            this.splitContainerMain.Panel2.Controls.Add(this.lbxFunctions);
            this.splitContainerMain.Size = new System.Drawing.Size(972, 527);
            this.splitContainerMain.SplitterDistance = 738;
            this.splitContainerMain.TabIndex = 2;
            // 
            // splitContainerScript
            // 
            this.splitContainerScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerScript.Location = new System.Drawing.Point(0, 0);
            this.splitContainerScript.Name = "splitContainerScript";
            this.splitContainerScript.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerScript.Panel1
            // 
            this.splitContainerScript.Panel1.Controls.Add(this.scintilla1);
            // 
            // splitContainerScript.Panel2
            // 
            this.splitContainerScript.Panel2.Controls.Add(this.txtInfo);
            this.splitContainerScript.Size = new System.Drawing.Size(738, 527);
            this.splitContainerScript.SplitterDistance = 408;
            this.splitContainerScript.TabIndex = 0;
            // 
            // txtInfo
            // 
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.Location = new System.Drawing.Point(0, 0);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(738, 115);
            this.txtInfo.TabIndex = 0;
            // 
            // cmbFunctions
            // 
            this.cmbFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFunctions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFunctions.FormattingEnabled = true;
            this.cmbFunctions.Items.AddRange(new object[] {
            "~ga|~gc|~og|~os",
            "BasicCommands",
            "ModuleProperties",
            "PlayerProperties",
            "PropProperties"});
            this.cmbFunctions.Location = new System.Drawing.Point(3, 3);
            this.cmbFunctions.Name = "cmbFunctions";
            this.cmbFunctions.Size = new System.Drawing.Size(224, 21);
            this.cmbFunctions.TabIndex = 1;
            this.cmbFunctions.SelectedIndexChanged += new System.EventHandler(this.cmbFunctions_SelectedIndexChanged);
            // 
            // lbxFunctions
            // 
            this.lbxFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxFunctions.FormattingEnabled = true;
            this.lbxFunctions.Location = new System.Drawing.Point(3, 26);
            this.lbxFunctions.Name = "lbxFunctions";
            this.lbxFunctions.Size = new System.Drawing.Size(224, 498);
            this.lbxFunctions.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSaveScript,
            this.tsShowWhiteSpace});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(972, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsSaveScript
            // 
            this.tsSaveScript.BackColor = System.Drawing.Color.LightGreen;
            this.tsSaveScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsSaveScript.Image = ((System.Drawing.Image)(resources.GetObject("tsSaveScript.Image")));
            this.tsSaveScript.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSaveScript.Name = "tsSaveScript";
            this.tsSaveScript.Size = new System.Drawing.Size(68, 22);
            this.tsSaveScript.Text = "Save Script";
            this.tsSaveScript.Click += new System.EventHandler(this.tsSaveScript_Click);
            // 
            // tsShowWhiteSpace
            // 
            this.tsShowWhiteSpace.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tsShowWhiteSpace.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsShowWhiteSpace.Image = ((System.Drawing.Image)(resources.GetObject("tsShowWhiteSpace.Image")));
            this.tsShowWhiteSpace.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsShowWhiteSpace.Name = "tsShowWhiteSpace";
            this.tsShowWhiteSpace.Size = new System.Drawing.Size(102, 22);
            this.tsShowWhiteSpace.Text = "ShowWhiteSpace";
            this.tsShowWhiteSpace.Click += new System.EventHandler(this.tsShowWhiteSpace_Click);
            // 
            // IBScriptEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 555);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainerMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(856, 554);
            this.Name = "IBScriptEditor";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.Text = "IB Script Editor";
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerScript.Panel1.ResumeLayout(false);
            this.splitContainerScript.Panel2.ResumeLayout(false);
            this.splitContainerScript.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerScript)).EndInit();
            this.splitContainerScript.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScintillaNET.Scintilla scintilla1;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerScript;
        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.ComboBox cmbFunctions;
        private System.Windows.Forms.ListBox lbxFunctions;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsSaveScript;
        private System.Windows.Forms.ToolStripButton tsShowWhiteSpace;
    }
}

