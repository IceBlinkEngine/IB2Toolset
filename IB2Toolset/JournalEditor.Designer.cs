namespace IB2miniToolset
{
    partial class JournalEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JournalEditor));
            this.MainContainer = new System.Windows.Forms.SplitContainer();
            this.LeftContainer = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ctxTreeView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addParentNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addChildNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbAddParent = new System.Windows.Forms.ToolStripButton();
            this.tsbAddChild = new System.Windows.Forms.ToolStripButton();
            this.tsbRemove = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveUp = new System.Windows.Forms.ToolStripButton();
            this.tsbMoveDown = new System.Windows.Forms.ToolStripButton();
            this.tsbExpandAll = new System.Windows.Forms.ToolStripButton();
            this.tsbCollapseAll = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.MainContainer.Panel1.SuspendLayout();
            this.MainContainer.Panel2.SuspendLayout();
            this.MainContainer.SuspendLayout();
            this.LeftContainer.Panel1.SuspendLayout();
            this.LeftContainer.Panel2.SuspendLayout();
            this.LeftContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.ctxTreeView.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainContainer
            // 
            this.MainContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainContainer.Location = new System.Drawing.Point(0, 25);
            this.MainContainer.Name = "MainContainer";
            // 
            // MainContainer.Panel1
            // 
            this.MainContainer.Panel1.Controls.Add(this.LeftContainer);
            // 
            // MainContainer.Panel2
            // 
            this.MainContainer.Panel2.Controls.Add(this.richTextBox1);
            this.MainContainer.Size = new System.Drawing.Size(684, 592);
            this.MainContainer.SplitterDistance = 273;
            this.MainContainer.TabIndex = 0;
            // 
            // LeftContainer
            // 
            this.LeftContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LeftContainer.Location = new System.Drawing.Point(0, 0);
            this.LeftContainer.Name = "LeftContainer";
            this.LeftContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // LeftContainer.Panel1
            // 
            this.LeftContainer.Panel1.Controls.Add(this.treeView1);
            // 
            // LeftContainer.Panel2
            // 
            this.LeftContainer.Panel2.Controls.Add(this.propertyGrid1);
            this.LeftContainer.Size = new System.Drawing.Size(273, 592);
            this.LeftContainer.SplitterDistance = 338;
            this.LeftContainer.TabIndex = 0;
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.ctxTreeView;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(273, 338);
            this.treeView1.TabIndex = 4;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(273, 250);
            this.propertyGrid1.TabIndex = 5;
            this.propertyGrid1.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(407, 592);
            this.richTextBox1.TabIndex = 8;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddParent,
            this.tsbAddChild,
            this.tsbRemove,
            this.tsbMoveUp,
            this.tsbMoveDown,
            this.tsbExpandAll,
            this.tsbCollapseAll,
            this.tsbRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(684, 25);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ctxTreeView
            // 
            this.ctxTreeView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addParentNodeToolStripMenuItem,
            this.addChildNodeToolStripMenuItem,
            this.removeNodeToolStripMenuItem});
            this.ctxTreeView.Name = "ctxTreeView";
            this.ctxTreeView.Size = new System.Drawing.Size(166, 70);
            this.ctxTreeView.Opening += new System.ComponentModel.CancelEventHandler(this.ctxTreeView_Opening_1);
            // 
            // addParentNodeToolStripMenuItem
            // 
            this.addParentNodeToolStripMenuItem.Name = "addParentNodeToolStripMenuItem";
            this.addParentNodeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addParentNodeToolStripMenuItem.Text = "Add Parent Node";
            this.addParentNodeToolStripMenuItem.Click += new System.EventHandler(this.addParentNodeToolStripMenuItem_Click);
            // 
            // addChildNodeToolStripMenuItem
            // 
            this.addChildNodeToolStripMenuItem.Name = "addChildNodeToolStripMenuItem";
            this.addChildNodeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.addChildNodeToolStripMenuItem.Text = "Add Child Node";
            this.addChildNodeToolStripMenuItem.Click += new System.EventHandler(this.addChildNodeToolStripMenuItem_Click);
            // 
            // removeNodeToolStripMenuItem
            // 
            this.removeNodeToolStripMenuItem.Name = "removeNodeToolStripMenuItem";
            this.removeNodeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.removeNodeToolStripMenuItem.Text = "Remove Node";
            this.removeNodeToolStripMenuItem.Click += new System.EventHandler(this.removeNodeToolStripMenuItem_Click);
            // 
            // tsbAddParent
            // 
            this.tsbAddParent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAddParent.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddParent.Image")));
            this.tsbAddParent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddParent.Name = "tsbAddParent";
            this.tsbAddParent.Size = new System.Drawing.Size(70, 22);
            this.tsbAddParent.Text = "Add Parent";
            this.tsbAddParent.Click += new System.EventHandler(this.tsbAddParent_Click);
            // 
            // tsbAddChild
            // 
            this.tsbAddChild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAddChild.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddChild.Image")));
            this.tsbAddChild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddChild.Name = "tsbAddChild";
            this.tsbAddChild.Size = new System.Drawing.Size(64, 22);
            this.tsbAddChild.Text = "Add Child";
            this.tsbAddChild.Click += new System.EventHandler(this.tsbAddChild_Click);
            // 
            // tsbRemove
            // 
            this.tsbRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRemove.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemove.Image")));
            this.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemove.Name = "tsbRemove";
            this.tsbRemove.Size = new System.Drawing.Size(54, 22);
            this.tsbRemove.Text = "Remove";
            this.tsbRemove.Click += new System.EventHandler(this.tsbRemove_Click);
            // 
            // tsbMoveUp
            // 
            this.tsbMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveUp.Image")));
            this.tsbMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveUp.Name = "tsbMoveUp";
            this.tsbMoveUp.Size = new System.Drawing.Size(59, 22);
            this.tsbMoveUp.Text = "Move Up";
            this.tsbMoveUp.Click += new System.EventHandler(this.tsbMoveUp_Click);
            // 
            // tsbMoveDown
            // 
            this.tsbMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("tsbMoveDown.Image")));
            this.tsbMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMoveDown.Name = "tsbMoveDown";
            this.tsbMoveDown.Size = new System.Drawing.Size(75, 22);
            this.tsbMoveDown.Text = "Move Down";
            this.tsbMoveDown.Click += new System.EventHandler(this.tsbMoveDown_Click);
            // 
            // tsbExpandAll
            // 
            this.tsbExpandAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbExpandAll.Image")));
            this.tsbExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExpandAll.Name = "tsbExpandAll";
            this.tsbExpandAll.Size = new System.Drawing.Size(66, 22);
            this.tsbExpandAll.Text = "Expand All";
            this.tsbExpandAll.Click += new System.EventHandler(this.tsbExpandAll_Click);
            // 
            // tsbCollapseAll
            // 
            this.tsbCollapseAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbCollapseAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbCollapseAll.Image")));
            this.tsbCollapseAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCollapseAll.Name = "tsbCollapseAll";
            this.tsbCollapseAll.Size = new System.Drawing.Size(73, 22);
            this.tsbCollapseAll.Text = "Collapse All";
            this.tsbCollapseAll.Click += new System.EventHandler(this.tsbCollapseAll_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(101, 22);
            this.tsbRefresh.Text = "Refresh TreeView";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // JournalEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 617);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.MainContainer);
            this.Name = "JournalEditor";
            this.Text = "Journal Editor";
            this.Load += new System.EventHandler(this.JournalEditor_Load);
            this.MainContainer.Panel1.ResumeLayout(false);
            this.MainContainer.Panel2.ResumeLayout(false);
            this.MainContainer.ResumeLayout(false);
            this.LeftContainer.Panel1.ResumeLayout(false);
            this.LeftContainer.Panel2.ResumeLayout(false);
            this.LeftContainer.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ctxTreeView.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainContainer;
        private System.Windows.Forms.SplitContainer LeftContainer;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddParent;
        private System.Windows.Forms.ToolStripButton tsbAddChild;
        private System.Windows.Forms.ToolStripButton tsbRemove;
        private System.Windows.Forms.ToolStripButton tsbMoveUp;
        private System.Windows.Forms.ToolStripButton tsbMoveDown;
        private System.Windows.Forms.ToolStripButton tsbExpandAll;
        private System.Windows.Forms.ToolStripButton tsbCollapseAll;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.ContextMenuStrip ctxTreeView;
        private System.Windows.Forms.ToolStripMenuItem addParentNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addChildNodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeNodeToolStripMenuItem;

    }
}