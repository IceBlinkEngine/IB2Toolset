namespace IB2Toolset
{
    /*partial class LogicTreeEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogicTreeEditor));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.ctxTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relocateCopiedNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteNodesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteAsLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.followLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.expandAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConvoIcons = new System.Windows.Forms.ImageList(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRedoText = new System.Windows.Forms.Button();
            this.btnUndoText = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLinkTo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOrderNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIndex = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtText = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.convoMainSplitPanel = new System.Windows.Forms.SplitContainer();
            this.bottomSplitPanel = new System.Windows.Forms.SplitContainer();
            this.CAScriptSplitPanel = new System.Windows.Forms.SplitContainer();
            this.CondActSplitPanel2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCondVariables = new System.Windows.Forms.Button();
            this.btnCondMoveDown = new System.Windows.Forms.Button();
            this.btnCondMoveUp = new System.Windows.Forms.Button();
            this.btnCondRemove = new System.Windows.Forms.Button();
            this.btnCondAdd = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvCondition = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AndOr = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Not = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Script = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Parameter1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameter2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameter3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Parameter4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnActVariables = new System.Windows.Forms.Button();
            this.btnActMoveDown = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.btnActMoveUp = new System.Windows.Forms.Button();
            this.btnActAdd = new System.Windows.Forms.Button();
            this.btnActRemove = new System.Windows.Forms.Button();
            this.dgvAction = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rtxtScript = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsAddNode = new System.Windows.Forms.ToolStripButton();
            this.tsRemoveNode = new System.Windows.Forms.ToolStripButton();
            this.tsCopyNodes = new System.Windows.Forms.ToolStripButton();
            this.tsPasteNodes = new System.Windows.Forms.ToolStripButton();
            this.tsPasteAsLink = new System.Windows.Forms.ToolStripButton();
            this.tsRelocateCopyNodes = new System.Windows.Forms.ToolStripButton();
            this.tsUndo = new System.Windows.Forms.ToolStripButton();
            this.tsRedo = new System.Windows.Forms.ToolStripButton();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsMoveDown = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.chkShowTextOnly = new System.Windows.Forms.CheckBox();
            this.conditionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.conditionBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.ctxTreeView.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.convoMainSplitPanel.Panel1.SuspendLayout();
            this.convoMainSplitPanel.Panel2.SuspendLayout();
            this.convoMainSplitPanel.SuspendLayout();
            this.bottomSplitPanel.Panel1.SuspendLayout();
            this.bottomSplitPanel.Panel2.SuspendLayout();
            this.bottomSplitPanel.SuspendLayout();
            this.CAScriptSplitPanel.Panel1.SuspendLayout();
            this.CAScriptSplitPanel.Panel2.SuspendLayout();
            this.CAScriptSplitPanel.SuspendLayout();
            this.CondActSplitPanel2.Panel1.SuspendLayout();
            this.CondActSplitPanel2.Panel2.SuspendLayout();
            this.CondActSplitPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCondition)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAction)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conditionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.conditionBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.ctxTreeView;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.ConvoIcons;
            this.treeView1.ItemHeight = 18;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(954, 93);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCollapse);
            this.treeView1.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterExpand);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // ctxTreeView
            // 
            this.ctxTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNodeToolStripMenuItem,
            this.removeNodeToolStripMenuItem,
            this.copyNodeToolStripMenuItem,
            this.relocateCopiedNodeToolStripMenuItem,
            this.pasteNodesToolStripMenuItem,
            this.pasteAsLinkToolStripMenuItem,
            this.followLinkToolStripMenuItem,
            this.toolStripSeparator1,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator3,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.toolStripSeparator2,
            this.expandAllToolStripMenuItem,
            this.collapseAllToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.ctxTreeView.Name = "ctxTreeView";
            this.ctxTreeView.ShowImageMargin = false;
            this.ctxTreeView.Size = new System.Drawing.Size(168, 330);
            this.ctxTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.ctxTreeView_Opening);
            // 
            // addNodeToolStripMenuItem
            // 
            this.addNodeToolStripMenuItem.Name = "addNodeToolStripMenuItem";
            this.addNodeToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.addNodeToolStripMenuItem.Text = "Add Node";
            this.addNodeToolStripMenuItem.Click += new System.EventHandler(this.addNodeToolStripMenuItem_Click);
            // 
            // removeNodeToolStripMenuItem
            // 
            this.removeNodeToolStripMenuItem.Name = "removeNodeToolStripMenuItem";
            this.removeNodeToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.removeNodeToolStripMenuItem.Text = "Remove Node";
            this.removeNodeToolStripMenuItem.Click += new System.EventHandler(this.removeNodeToolStripMenuItem_Click);
            // 
            // copyNodeToolStripMenuItem
            // 
            this.copyNodeToolStripMenuItem.Name = "copyNodeToolStripMenuItem";
            this.copyNodeToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.copyNodeToolStripMenuItem.Text = "Copy Node";
            this.copyNodeToolStripMenuItem.Click += new System.EventHandler(this.copyNodeToolStripMenuItem_Click);
            // 
            // relocateCopiedNodeToolStripMenuItem
            // 
            this.relocateCopiedNodeToolStripMenuItem.Name = "relocateCopiedNodeToolStripMenuItem";
            this.relocateCopiedNodeToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.relocateCopiedNodeToolStripMenuItem.Text = "Relocate Copied Node";
            this.relocateCopiedNodeToolStripMenuItem.Click += new System.EventHandler(this.relocateCopiedNodeToolStripMenuItem_Click);
            // 
            // pasteNodesToolStripMenuItem
            // 
            this.pasteNodesToolStripMenuItem.Name = "pasteNodesToolStripMenuItem";
            this.pasteNodesToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.pasteNodesToolStripMenuItem.Text = "Paste Nodes";
            this.pasteNodesToolStripMenuItem.Click += new System.EventHandler(this.pasteNodesToolStripMenuItem_Click);
            // 
            // pasteAsLinkToolStripMenuItem
            // 
            this.pasteAsLinkToolStripMenuItem.Name = "pasteAsLinkToolStripMenuItem";
            this.pasteAsLinkToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.pasteAsLinkToolStripMenuItem.Text = "Paste As Link";
            this.pasteAsLinkToolStripMenuItem.Click += new System.EventHandler(this.pasteAsLinkToolStripMenuItem_Click);
            // 
            // followLinkToolStripMenuItem
            // 
            this.followLinkToolStripMenuItem.Name = "followLinkToolStripMenuItem";
            this.followLinkToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.followLinkToolStripMenuItem.Text = "Follow Link";
            this.followLinkToolStripMenuItem.Click += new System.EventHandler(this.followLinkToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(164, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(164, 6);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.moveUpToolStripMenuItem.Text = "Move Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.moveDownToolStripMenuItem.Text = "Move Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(164, 6);
            // 
            // expandAllToolStripMenuItem
            // 
            this.expandAllToolStripMenuItem.Name = "expandAllToolStripMenuItem";
            this.expandAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.expandAllToolStripMenuItem.Text = "Expand All";
            this.expandAllToolStripMenuItem.Click += new System.EventHandler(this.expandAllToolStripMenuItem_Click);
            // 
            // collapseAllToolStripMenuItem
            // 
            this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
            this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.collapseAllToolStripMenuItem.Text = "Collapse All";
            this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.collapseAllToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // ConvoIcons
            // 
            this.ConvoIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ConvoIcons.ImageStream")));
            this.ConvoIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ConvoIcons.Images.SetKeyName(0, "btnNode.png");
            this.ConvoIcons.Images.SetKeyName(1, "btnCondition.png");
            this.ConvoIcons.Images.SetKeyName(2, "btnAction.png");
            this.ConvoIcons.Images.SetKeyName(3, "btnCondAct.png");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkShowTextOnly);
            this.groupBox1.Controls.Add(this.btnRedoText);
            this.groupBox1.Controls.Add(this.btnUndoText);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtLinkTo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtOrderNum);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtIndex);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtText);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 394);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Node Info";
            // 
            // btnRedoText
            // 
            this.btnRedoText.Location = new System.Drawing.Point(123, 110);
            this.btnRedoText.Name = "btnRedoText";
            this.btnRedoText.Size = new System.Drawing.Size(80, 23);
            this.btnRedoText.TabIndex = 36;
            this.btnRedoText.Text = "RedoText";
            this.btnRedoText.UseVisualStyleBackColor = true;
            this.btnRedoText.Click += new System.EventHandler(this.btnRedoText_Click);
            // 
            // btnUndoText
            // 
            this.btnUndoText.Location = new System.Drawing.Point(40, 110);
            this.btnUndoText.Name = "btnUndoText";
            this.btnUndoText.Size = new System.Drawing.Size(80, 23);
            this.btnUndoText.TabIndex = 35;
            this.btnUndoText.Text = "UndoText";
            this.btnUndoText.UseVisualStyleBackColor = true;
            this.btnUndoText.Click += new System.EventHandler(this.btnUndoText_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(76, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "LinkTo:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLinkTo
            // 
            this.txtLinkTo.Location = new System.Drawing.Point(120, 50);
            this.txtLinkTo.Name = "txtLinkTo";
            this.txtLinkTo.ReadOnly = true;
            this.txtLinkTo.Size = new System.Drawing.Size(30, 20);
            this.txtLinkTo.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(86, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 26);
            this.label5.TabIndex = 10;
            this.label5.Text = "Order\r\nNum:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOrderNum
            // 
            this.txtOrderNum.Location = new System.Drawing.Point(120, 25);
            this.txtOrderNum.Name = "txtOrderNum";
            this.txtOrderNum.ReadOnly = true;
            this.txtOrderNum.Size = new System.Drawing.Size(30, 20);
            this.txtOrderNum.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Index:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIndex
            // 
            this.txtIndex.Location = new System.Drawing.Point(43, 50);
            this.txtIndex.Name = "txtIndex";
            this.txtIndex.ReadOnly = true;
            this.txtIndex.Size = new System.Drawing.Size(30, 20);
            this.txtIndex.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Text:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IdNum:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtText
            // 
            this.txtText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtText.Location = new System.Drawing.Point(8, 138);
            this.txtText.Multiline = true;
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(219, 250);
            this.txtText.TabIndex = 2;
            this.txtText.TextChanged += new System.EventHandler(this.txtText_TextChanged);
            this.txtText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtText_KeyDown);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(43, 25);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(30, 20);
            this.txtName.TabIndex = 0;
            // 
            // convoMainSplitPanel
            // 
            this.convoMainSplitPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.convoMainSplitPanel.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.convoMainSplitPanel.Location = new System.Drawing.Point(0, 25);
            this.convoMainSplitPanel.Name = "convoMainSplitPanel";
            this.convoMainSplitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // convoMainSplitPanel.Panel1
            // 
            this.convoMainSplitPanel.Panel1.Controls.Add(this.treeView1);
            // 
            // convoMainSplitPanel.Panel2
            // 
            this.convoMainSplitPanel.Panel2.Controls.Add(this.bottomSplitPanel);
            this.convoMainSplitPanel.Size = new System.Drawing.Size(954, 491);
            this.convoMainSplitPanel.SplitterDistance = 93;
            this.convoMainSplitPanel.TabIndex = 25;
            // 
            // bottomSplitPanel
            // 
            this.bottomSplitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bottomSplitPanel.Location = new System.Drawing.Point(0, 0);
            this.bottomSplitPanel.Name = "bottomSplitPanel";
            // 
            // bottomSplitPanel.Panel1
            // 
            this.bottomSplitPanel.Panel1.Controls.Add(this.CAScriptSplitPanel);
            // 
            // bottomSplitPanel.Panel2
            // 
            this.bottomSplitPanel.Panel2.Controls.Add(this.groupBox1);
            this.bottomSplitPanel.Size = new System.Drawing.Size(954, 394);
            this.bottomSplitPanel.SplitterDistance = 711;
            this.bottomSplitPanel.TabIndex = 26;
            // 
            // CAScriptSplitPanel
            // 
            this.CAScriptSplitPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CAScriptSplitPanel.Location = new System.Drawing.Point(0, 0);
            this.CAScriptSplitPanel.Name = "CAScriptSplitPanel";
            this.CAScriptSplitPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // CAScriptSplitPanel.Panel1
            // 
            this.CAScriptSplitPanel.Panel1.Controls.Add(this.CondActSplitPanel2);
            // 
            // CAScriptSplitPanel.Panel2
            // 
            this.CAScriptSplitPanel.Panel2.Controls.Add(this.rtxtScript);
            this.CAScriptSplitPanel.Size = new System.Drawing.Size(711, 394);
            this.CAScriptSplitPanel.SplitterDistance = 274;
            this.CAScriptSplitPanel.TabIndex = 0;
            // 
            // CondActSplitPanel2
            // 
            this.CondActSplitPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CondActSplitPanel2.Location = new System.Drawing.Point(0, 0);
            this.CondActSplitPanel2.Name = "CondActSplitPanel2";
            this.CondActSplitPanel2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // CondActSplitPanel2.Panel1
            // 
            this.CondActSplitPanel2.Panel1.Controls.Add(this.panel1);
            this.CondActSplitPanel2.Panel1.Controls.Add(this.dgvCondition);
            // 
            // CondActSplitPanel2.Panel2
            // 
            this.CondActSplitPanel2.Panel2.Controls.Add(this.panel2);
            this.CondActSplitPanel2.Panel2.Controls.Add(this.dgvAction);
            this.CondActSplitPanel2.Size = new System.Drawing.Size(711, 274);
            this.CondActSplitPanel2.SplitterDistance = 134;
            this.CondActSplitPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.btnCondVariables);
            this.panel1.Controls.Add(this.btnCondMoveDown);
            this.panel1.Controls.Add(this.btnCondMoveUp);
            this.panel1.Controls.Add(this.btnCondRemove);
            this.panel1.Controls.Add(this.btnCondAdd);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(711, 30);
            this.panel1.TabIndex = 4;
            // 
            // btnCondVariables
            // 
            this.btnCondVariables.Location = new System.Drawing.Point(482, 4);
            this.btnCondVariables.Name = "btnCondVariables";
            this.btnCondVariables.Size = new System.Drawing.Size(131, 23);
            this.btnCondVariables.TabIndex = 5;
            this.btnCondVariables.Text = "Variables";
            this.btnCondVariables.UseVisualStyleBackColor = true;
            this.btnCondVariables.Click += new System.EventHandler(this.btnCondVariables_Click);
            // 
            // btnCondMoveDown
            // 
            this.btnCondMoveDown.Location = new System.Drawing.Point(363, 4);
            this.btnCondMoveDown.Name = "btnCondMoveDown";
            this.btnCondMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnCondMoveDown.TabIndex = 4;
            this.btnCondMoveDown.Text = "Move Down";
            this.btnCondMoveDown.UseVisualStyleBackColor = true;
            this.btnCondMoveDown.Click += new System.EventHandler(this.btnCondMoveDown_Click);
            // 
            // btnCondMoveUp
            // 
            this.btnCondMoveUp.Location = new System.Drawing.Point(282, 4);
            this.btnCondMoveUp.Name = "btnCondMoveUp";
            this.btnCondMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnCondMoveUp.TabIndex = 3;
            this.btnCondMoveUp.Text = "Move Up";
            this.btnCondMoveUp.UseVisualStyleBackColor = true;
            this.btnCondMoveUp.Click += new System.EventHandler(this.btnCondMoveUp_Click);
            // 
            // btnCondRemove
            // 
            this.btnCondRemove.Location = new System.Drawing.Point(201, 4);
            this.btnCondRemove.Name = "btnCondRemove";
            this.btnCondRemove.Size = new System.Drawing.Size(75, 23);
            this.btnCondRemove.TabIndex = 2;
            this.btnCondRemove.Text = "Remove";
            this.btnCondRemove.UseVisualStyleBackColor = true;
            this.btnCondRemove.Click += new System.EventHandler(this.btnCondRemove_Click);
            // 
            // btnCondAdd
            // 
            this.btnCondAdd.Location = new System.Drawing.Point(120, 4);
            this.btnCondAdd.Name = "btnCondAdd";
            this.btnCondAdd.Size = new System.Drawing.Size(75, 23);
            this.btnCondAdd.TabIndex = 1;
            this.btnCondAdd.Text = "Add";
            this.btnCondAdd.UseVisualStyleBackColor = true;
            this.btnCondAdd.Click += new System.EventHandler(this.btnCondAdd_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "CONDITIONS";
            // 
            // dgvCondition
            // 
            this.dgvCondition.AllowUserToAddRows = false;
            this.dgvCondition.AllowUserToDeleteRows = false;
            this.dgvCondition.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCondition.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCondition.ColumnHeadersHeight = 25;
            this.dgvCondition.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCondition.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.AndOr,
            this.Not,
            this.Script,
            this.Parameter1,
            this.Parameter2,
            this.Parameter3,
            this.Parameter4});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCondition.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCondition.Location = new System.Drawing.Point(0, 30);
            this.dgvCondition.MultiSelect = false;
            this.dgvCondition.Name = "dgvCondition";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCondition.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCondition.RowHeadersVisible = false;
            this.dgvCondition.Size = new System.Drawing.Size(711, 104);
            this.dgvCondition.TabIndex = 3;
            this.dgvCondition.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCondition_CellClick);
            this.dgvCondition.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCondition_CellContentClick);
            this.dgvCondition.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCondition_CellValueChanged);
            this.dgvCondition.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvCondition_CurrentCellDirtyStateChanged);
            this.dgvCondition.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvCondition_DataError);
            this.dgvCondition.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCondition_EditingControlShowing);
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Width = 45;
            // 
            // AndOr
            // 
            this.AndOr.HeaderText = "AndOr";
            this.AndOr.Name = "AndOr";
            this.AndOr.Width = 45;
            // 
            // Not
            // 
            this.Not.HeaderText = "Not";
            this.Not.Name = "Not";
            this.Not.Width = 35;
            // 
            // Script
            // 
            this.Script.HeaderText = "Script";
            this.Script.Name = "Script";
            this.Script.Width = 150;
            // 
            // Parameter1
            // 
            this.Parameter1.HeaderText = "Parameter 1";
            this.Parameter1.Name = "Parameter1";
            this.Parameter1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Parameter1.Width = 130;
            // 
            // Parameter2
            // 
            this.Parameter2.HeaderText = "Parameter 2";
            this.Parameter2.Name = "Parameter2";
            this.Parameter2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Parameter2.Width = 130;
            // 
            // Parameter3
            // 
            this.Parameter3.HeaderText = "Parameter 3";
            this.Parameter3.Name = "Parameter3";
            this.Parameter3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Parameter3.Width = 130;
            // 
            // Parameter4
            // 
            this.Parameter4.HeaderText = "Parameter 4";
            this.Parameter4.Name = "Parameter4";
            this.Parameter4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Parameter4.Width = 130;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.panel2.Controls.Add(this.btnActVariables);
            this.panel2.Controls.Add(this.btnActMoveDown);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.btnActMoveUp);
            this.panel2.Controls.Add(this.btnActAdd);
            this.panel2.Controls.Add(this.btnActRemove);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(711, 30);
            this.panel2.TabIndex = 5;
            // 
            // btnActVariables
            // 
            this.btnActVariables.Location = new System.Drawing.Point(482, 4);
            this.btnActVariables.Name = "btnActVariables";
            this.btnActVariables.Size = new System.Drawing.Size(131, 23);
            this.btnActVariables.TabIndex = 9;
            this.btnActVariables.Text = "Variables";
            this.btnActVariables.UseVisualStyleBackColor = true;
            this.btnActVariables.Click += new System.EventHandler(this.btnActVariables_Click);
            // 
            // btnActMoveDown
            // 
            this.btnActMoveDown.Location = new System.Drawing.Point(363, 4);
            this.btnActMoveDown.Name = "btnActMoveDown";
            this.btnActMoveDown.Size = new System.Drawing.Size(75, 23);
            this.btnActMoveDown.TabIndex = 8;
            this.btnActMoveDown.Text = "Move Down";
            this.btnActMoveDown.UseVisualStyleBackColor = true;
            this.btnActMoveDown.Click += new System.EventHandler(this.btnActMoveDown_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(4, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "ACTIONS";
            // 
            // btnActMoveUp
            // 
            this.btnActMoveUp.Location = new System.Drawing.Point(282, 4);
            this.btnActMoveUp.Name = "btnActMoveUp";
            this.btnActMoveUp.Size = new System.Drawing.Size(75, 23);
            this.btnActMoveUp.TabIndex = 7;
            this.btnActMoveUp.Text = "Move Up";
            this.btnActMoveUp.UseVisualStyleBackColor = true;
            this.btnActMoveUp.Click += new System.EventHandler(this.btnActMoveUp_Click);
            // 
            // btnActAdd
            // 
            this.btnActAdd.Location = new System.Drawing.Point(120, 4);
            this.btnActAdd.Name = "btnActAdd";
            this.btnActAdd.Size = new System.Drawing.Size(75, 23);
            this.btnActAdd.TabIndex = 5;
            this.btnActAdd.Text = "Add";
            this.btnActAdd.UseVisualStyleBackColor = true;
            this.btnActAdd.Click += new System.EventHandler(this.btnActAdd_Click);
            // 
            // btnActRemove
            // 
            this.btnActRemove.Location = new System.Drawing.Point(201, 4);
            this.btnActRemove.Name = "btnActRemove";
            this.btnActRemove.Size = new System.Drawing.Size(75, 23);
            this.btnActRemove.TabIndex = 6;
            this.btnActRemove.Text = "Remove";
            this.btnActRemove.UseVisualStyleBackColor = true;
            this.btnActRemove.Click += new System.EventHandler(this.btnActRemove_Click);
            // 
            // dgvAction
            // 
            this.dgvAction.AllowUserToAddRows = false;
            this.dgvAction.AllowUserToDeleteRows = false;
            this.dgvAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAction.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAction.ColumnHeadersHeight = 25;
            this.dgvAction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvAction.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAction.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAction.Location = new System.Drawing.Point(0, 30);
            this.dgvAction.MultiSelect = false;
            this.dgvAction.Name = "dgvAction";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAction.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAction.RowHeadersVisible = false;
            this.dgvAction.Size = new System.Drawing.Size(711, 108);
            this.dgvAction.TabIndex = 4;
            this.dgvAction.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAction_CellClick);
            this.dgvAction.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAction_CellContentClick);
            this.dgvAction.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAction_CellValueChanged);
            this.dgvAction.CurrentCellDirtyStateChanged += new System.EventHandler(this.dgvAction_CurrentCellDirtyStateChanged);
            this.dgvAction.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvAction_DataError);
            this.dgvAction.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvAction_EditingControlShowing);
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.HeaderText = "Select";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.Width = 45;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.HeaderText = "Script";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Width = 150;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Parameter 1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 130;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Parameter 2";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Parameter 3";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 130;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Parameter 4";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 130;
            // 
            // rtxtScript
            // 
            this.rtxtScript.BackColor = System.Drawing.Color.White;
            this.rtxtScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtScript.Location = new System.Drawing.Point(0, 0);
            this.rtxtScript.Name = "rtxtScript";
            this.rtxtScript.ReadOnly = true;
            this.rtxtScript.Size = new System.Drawing.Size(711, 116);
            this.rtxtScript.TabIndex = 19;
            this.rtxtScript.Text = "";
            this.rtxtScript.WordWrap = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddNode,
            this.tsRemoveNode,
            this.tsCopyNodes,
            this.tsPasteNodes,
            this.tsPasteAsLink,
            this.tsRelocateCopyNodes,
            this.tsUndo,
            this.tsRedo,
            this.tsRefresh,
            this.tsMoveUp,
            this.tsMoveDown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(954, 25);
            this.toolStrip1.TabIndex = 27;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsAddNode
            // 
            this.tsAddNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsAddNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAddNode.Name = "tsAddNode";
            this.tsAddNode.Size = new System.Drawing.Size(62, 22);
            this.tsAddNode.Text = "AddNode";
            this.tsAddNode.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.tsAddNode.Click += new System.EventHandler(this.tsAddNode_Click);
            // 
            // tsRemoveNode
            // 
            this.tsRemoveNode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsRemoveNode.Image = ((System.Drawing.Image)(resources.GetObject("tsRemoveNode.Image")));
            this.tsRemoveNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRemoveNode.Name = "tsRemoveNode";
            this.tsRemoveNode.Size = new System.Drawing.Size(83, 22);
            this.tsRemoveNode.Text = "RemoveNode";
            this.tsRemoveNode.Click += new System.EventHandler(this.tsRemoveNode_Click);
            // 
            // tsCopyNodes
            // 
            this.tsCopyNodes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsCopyNodes.Image = ((System.Drawing.Image)(resources.GetObject("tsCopyNodes.Image")));
            this.tsCopyNodes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCopyNodes.Name = "tsCopyNodes";
            this.tsCopyNodes.Size = new System.Drawing.Size(73, 22);
            this.tsCopyNodes.Text = "CopyNodes";
            this.tsCopyNodes.ToolTipText = "CopyNodes\r\nSelects the node to the clipboard for pasting as a copy, pasting as a " +
                "link, or relocating (original, not a copy) the node to another location.";
            this.tsCopyNodes.Click += new System.EventHandler(this.tsCopyNodes_Click);
            // 
            // tsPasteNodes
            // 
            this.tsPasteNodes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsPasteNodes.Image = ((System.Drawing.Image)(resources.GetObject("tsPasteNodes.Image")));
            this.tsPasteNodes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPasteNodes.Name = "tsPasteNodes";
            this.tsPasteNodes.Size = new System.Drawing.Size(73, 22);
            this.tsPasteNodes.Text = "PasteNodes";
            this.tsPasteNodes.Click += new System.EventHandler(this.tsPasteNodes_Click);
            // 
            // tsPasteAsLink
            // 
            this.tsPasteAsLink.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsPasteAsLink.Image = ((System.Drawing.Image)(resources.GetObject("tsPasteAsLink.Image")));
            this.tsPasteAsLink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPasteAsLink.Name = "tsPasteAsLink";
            this.tsPasteAsLink.Size = new System.Drawing.Size(74, 22);
            this.tsPasteAsLink.Text = "PasteAsLink";
            this.tsPasteAsLink.Click += new System.EventHandler(this.tsPasteAsLink_Click);
            // 
            // tsRelocateCopyNodes
            // 
            this.tsRelocateCopyNodes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsRelocateCopyNodes.Image = ((System.Drawing.Image)(resources.GetObject("tsRelocateCopyNodes.Image")));
            this.tsRelocateCopyNodes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRelocateCopyNodes.Name = "tsRelocateCopyNodes";
            this.tsRelocateCopyNodes.Size = new System.Drawing.Size(128, 22);
            this.tsRelocateCopyNodes.Text = "RelocateCopiedNodes";
            this.tsRelocateCopyNodes.Click += new System.EventHandler(this.tsRelocateCopyNodes_Click);
            // 
            // tsUndo
            // 
            this.tsUndo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tsUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsUndo.Image = ((System.Drawing.Image)(resources.GetObject("tsUndo.Image")));
            this.tsUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUndo.Name = "tsUndo";
            this.tsUndo.Size = new System.Drawing.Size(40, 22);
            this.tsUndo.Text = "Undo";
            this.tsUndo.ToolTipText = "Undo\r\nAn Undo State is stored upon\r\neach click on node, add node,\r\nremove node, p" +
                "aste node, paste\r\nnode as link, move up and\r\nmove down.";
            this.tsUndo.Click += new System.EventHandler(this.tsUndo_Click);
            // 
            // tsRedo
            // 
            this.tsRedo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.tsRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsRedo.Image = ((System.Drawing.Image)(resources.GetObject("tsRedo.Image")));
            this.tsRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRedo.Name = "tsRedo";
            this.tsRedo.Size = new System.Drawing.Size(38, 22);
            this.tsRedo.Text = "Redo";
            this.tsRedo.Click += new System.EventHandler(this.tsRedo_Click);
            // 
            // tsRefresh
            // 
            this.tsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsRefresh.Image")));
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(50, 22);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.Click += new System.EventHandler(this.tsRefresh_Click);
            // 
            // tsMoveUp
            // 
            this.tsMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("tsMoveUp.Image")));
            this.tsMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMoveUp.Name = "tsMoveUp";
            this.tsMoveUp.Size = new System.Drawing.Size(56, 22);
            this.tsMoveUp.Text = "MoveUp";
            this.tsMoveUp.Click += new System.EventHandler(this.tsMoveUp_Click);
            // 
            // tsMoveDown
            // 
            this.tsMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("tsMoveDown.Image")));
            this.tsMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMoveDown.Name = "tsMoveDown";
            this.tsMoveDown.Size = new System.Drawing.Size(72, 22);
            this.tsMoveDown.Text = "MoveDown";
            this.tsMoveDown.Click += new System.EventHandler(this.tsMoveDown_Click);
            // 
            // chkShowTextOnly
            // 
            this.chkShowTextOnly.AutoSize = true;
            this.chkShowTextOnly.Location = new System.Drawing.Point(19, 75);
            this.chkShowTextOnly.Name = "chkShowTextOnly";
            this.chkShowTextOnly.Size = new System.Drawing.Size(148, 30);
            this.chkShowTextOnly.TabIndex = 37;
            this.chkShowTextOnly.Text = "Hide Conditionals/Actions\r\nin TreeView Above";
            this.chkShowTextOnly.UseVisualStyleBackColor = true;
            this.chkShowTextOnly.CheckedChanged += new System.EventHandler(this.chkShowTextOnly_CheckedChanged);
            // 
            // conditionBindingSource
            // 
            this.conditionBindingSource.DataSource = typeof(IB2Toolset.Condition);
            // 
            // conditionBindingSource1
            // 
            this.conditionBindingSource1.DataSource = typeof(IB2Toolset.Condition);
            // 
            // LogicTreeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 516);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.convoMainSplitPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(856, 554);
            this.Name = "LogicTreeEditor";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.Text = "LogicTree Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConvoEditor_FormClosing);
            this.Load += new System.EventHandler(this.LogicTreeEditor_Load);
            this.ctxTreeView.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.convoMainSplitPanel.Panel1.ResumeLayout(false);
            this.convoMainSplitPanel.Panel2.ResumeLayout(false);
            this.convoMainSplitPanel.ResumeLayout(false);
            this.bottomSplitPanel.Panel1.ResumeLayout(false);
            this.bottomSplitPanel.Panel2.ResumeLayout(false);
            this.bottomSplitPanel.ResumeLayout(false);
            this.CAScriptSplitPanel.Panel1.ResumeLayout(false);
            this.CAScriptSplitPanel.Panel2.ResumeLayout(false);
            this.CAScriptSplitPanel.ResumeLayout(false);
            this.CondActSplitPanel2.Panel1.ResumeLayout(false);
            this.CondActSplitPanel2.Panel2.ResumeLayout(false);
            this.CondActSplitPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCondition)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAction)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conditionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.conditionBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIndex;
        private System.Windows.Forms.SplitContainer convoMainSplitPanel;
        private System.Windows.Forms.SplitContainer bottomSplitPanel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtLinkTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtOrderNum;
        public System.Windows.Forms.RichTextBox rtxtScript;
        private System.Windows.Forms.DataGridView dgvCondition;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsAddNode;
        private System.Windows.Forms.ToolStripButton tsRemoveNode;
        private System.Windows.Forms.ToolStripButton tsCopyNodes;
        private System.Windows.Forms.ToolStripButton tsPasteNodes;
        private System.Windows.Forms.ToolStripButton tsPasteAsLink;
        private System.Windows.Forms.ToolStripButton tsRefresh;
        private System.Windows.Forms.ToolStripButton tsMoveUp;
        private System.Windows.Forms.ToolStripButton tsMoveDown;
        private System.Windows.Forms.SplitContainer CAScriptSplitPanel;
        private System.Windows.Forms.SplitContainer CondActSplitPanel2;
        private System.Windows.Forms.DataGridView dgvAction;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCondMoveDown;
        private System.Windows.Forms.Button btnCondMoveUp;
        private System.Windows.Forms.Button btnCondRemove;
        private System.Windows.Forms.Button btnCondAdd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnActMoveDown;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnActMoveUp;
        private System.Windows.Forms.Button btnActAdd;
        private System.Windows.Forms.Button btnActRemove;
        private System.Windows.Forms.ImageList ConvoIcons;
        private System.Windows.Forms.BindingSource conditionBindingSource1;
        private System.Windows.Forms.BindingSource conditionBindingSource;
        private System.Windows.Forms.ContextMenuStrip ctxTreeView;
        private System.Windows.Forms.ToolStripMenuItem addNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteNodesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteAsLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem expandAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsUndo;
        private System.Windows.Forms.ToolStripButton tsRedo;
        private System.Windows.Forms.Button btnRedoText;
        private System.Windows.Forms.Button btnUndoText;
        private System.Windows.Forms.Button btnCondVariables;
        private System.Windows.Forms.Button btnActVariables;
        private System.Windows.Forms.ToolStripMenuItem followLinkToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewButtonColumn AndOr;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Not;
        private System.Windows.Forms.DataGridViewComboBoxColumn Script;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Parameter4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.ToolStripButton tsRelocateCopyNodes;
        private System.Windows.Forms.ToolStripMenuItem relocateCopiedNodeToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkShowTextOnly;
    }
    */
}

