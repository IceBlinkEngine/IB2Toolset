namespace IB2miniToolset
{
    partial class Blueprints
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
            this.tabCreatureItem = new System.Windows.Forms.TabControl();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tvCreatures = new System.Windows.Forms.TreeView();
            this.btnDuplicateCreature = new System.Windows.Forms.Button();
            this.btnRemoveCreature = new System.Windows.Forms.Button();
            this.btnAddCreature = new System.Windows.Forms.Button();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.tvItems = new System.Windows.Forms.TreeView();
            this.btnDuplicateItem = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tvProps = new System.Windows.Forms.TreeView();
            this.btnDuplicateProp = new System.Windows.Forms.Button();
            this.btnRemoveProp = new System.Windows.Forms.Button();
            this.btnAddProp = new System.Windows.Forms.Button();
            this.btnSortCreatures = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSortItems = new System.Windows.Forms.Button();
            this.btnSortProps = new System.Windows.Forms.Button();
            this.tabCreatureItem.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabCreatureItem
            // 
            this.tabCreatureItem.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this.tabCreatureItem.Controls.Add(this.tabPage6);
            this.tabCreatureItem.Controls.Add(this.tabPage7);
            this.tabCreatureItem.Controls.Add(this.tabPage1);
            this.tabCreatureItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCreatureItem.Location = new System.Drawing.Point(0, 0);
            this.tabCreatureItem.Name = "tabCreatureItem";
            this.tabCreatureItem.SelectedIndex = 0;
            this.tabCreatureItem.Size = new System.Drawing.Size(268, 323);
            this.tabCreatureItem.TabIndex = 4;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.btnSortCreatures);
            this.tabPage6.Controls.Add(this.tvCreatures);
            this.tabPage6.Controls.Add(this.btnDuplicateCreature);
            this.tabPage6.Controls.Add(this.btnRemoveCreature);
            this.tabPage6.Controls.Add(this.btnAddCreature);
            this.tabPage6.Location = new System.Drawing.Point(4, 25);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(260, 294);
            this.tabPage6.TabIndex = 0;
            this.tabPage6.Text = "Creatures";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tvCreatures
            // 
            this.tvCreatures.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCreatures.Location = new System.Drawing.Point(3, 29);
            this.tvCreatures.Name = "tvCreatures";
            this.tvCreatures.Size = new System.Drawing.Size(254, 259);
            this.tvCreatures.TabIndex = 42;
            this.tvCreatures.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvCreatures_AfterSelect);
            this.tvCreatures.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvCreatures_MouseClick_1);
            // 
            // btnDuplicateCreature
            // 
            this.btnDuplicateCreature.Location = new System.Drawing.Point(117, 3);
            this.btnDuplicateCreature.Name = "btnDuplicateCreature";
            this.btnDuplicateCreature.Size = new System.Drawing.Size(60, 23);
            this.btnDuplicateCreature.TabIndex = 41;
            this.btnDuplicateCreature.Text = "Duplicate";
            this.btnDuplicateCreature.UseVisualStyleBackColor = true;
            this.btnDuplicateCreature.Click += new System.EventHandler(this.btnDuplicateCreature_Click_1);
            // 
            // btnRemoveCreature
            // 
            this.btnRemoveCreature.Location = new System.Drawing.Point(54, 3);
            this.btnRemoveCreature.Name = "btnRemoveCreature";
            this.btnRemoveCreature.Size = new System.Drawing.Size(55, 23);
            this.btnRemoveCreature.TabIndex = 40;
            this.btnRemoveCreature.Text = "Remove";
            this.btnRemoveCreature.UseVisualStyleBackColor = true;
            this.btnRemoveCreature.Click += new System.EventHandler(this.btnRemoveCreature_Click_1);
            // 
            // btnAddCreature
            // 
            this.btnAddCreature.Location = new System.Drawing.Point(9, 3);
            this.btnAddCreature.Name = "btnAddCreature";
            this.btnAddCreature.Size = new System.Drawing.Size(37, 23);
            this.btnAddCreature.TabIndex = 39;
            this.btnAddCreature.Text = "Add";
            this.btnAddCreature.UseVisualStyleBackColor = true;
            this.btnAddCreature.Click += new System.EventHandler(this.btnAddCreature_Click_1);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btnSortItems);
            this.tabPage7.Controls.Add(this.tvItems);
            this.tabPage7.Controls.Add(this.btnDuplicateItem);
            this.tabPage7.Controls.Add(this.btnRemoveItem);
            this.tabPage7.Controls.Add(this.btnAddItem);
            this.tabPage7.Location = new System.Drawing.Point(4, 25);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(260, 294);
            this.tabPage7.TabIndex = 1;
            this.tabPage7.Text = "Items";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // tvItems
            // 
            this.tvItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvItems.Location = new System.Drawing.Point(3, 29);
            this.tvItems.Name = "tvItems";
            this.tvItems.Size = new System.Drawing.Size(254, 262);
            this.tvItems.TabIndex = 46;
            this.tvItems.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvItems_AfterSelect_1);
            this.tvItems.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvItems_MouseClick_1);
            // 
            // btnDuplicateItem
            // 
            this.btnDuplicateItem.Location = new System.Drawing.Point(117, 3);
            this.btnDuplicateItem.Name = "btnDuplicateItem";
            this.btnDuplicateItem.Size = new System.Drawing.Size(60, 23);
            this.btnDuplicateItem.TabIndex = 45;
            this.btnDuplicateItem.Text = "Duplicate";
            this.btnDuplicateItem.UseVisualStyleBackColor = true;
            this.btnDuplicateItem.Click += new System.EventHandler(this.btnDuplicateItem_Click_1);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Location = new System.Drawing.Point(54, 3);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(55, 23);
            this.btnRemoveItem.TabIndex = 44;
            this.btnRemoveItem.Text = "Remove";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click_1);
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(9, 3);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(37, 23);
            this.btnAddItem.TabIndex = 43;
            this.btnAddItem.Text = "Add";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click_1);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnSortProps);
            this.tabPage1.Controls.Add(this.tvProps);
            this.tabPage1.Controls.Add(this.btnDuplicateProp);
            this.tabPage1.Controls.Add(this.btnRemoveProp);
            this.tabPage1.Controls.Add(this.btnAddProp);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(260, 294);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Props";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tvProps
            // 
            this.tvProps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvProps.Location = new System.Drawing.Point(3, 29);
            this.tvProps.Name = "tvProps";
            this.tvProps.Size = new System.Drawing.Size(254, 262);
            this.tvProps.TabIndex = 50;
            this.tvProps.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvProps_AfterSelect_1);
            this.tvProps.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvProps_MouseClick_1);
            // 
            // btnDuplicateProp
            // 
            this.btnDuplicateProp.Location = new System.Drawing.Point(117, 3);
            this.btnDuplicateProp.Name = "btnDuplicateProp";
            this.btnDuplicateProp.Size = new System.Drawing.Size(60, 23);
            this.btnDuplicateProp.TabIndex = 49;
            this.btnDuplicateProp.Text = "Duplicate";
            this.btnDuplicateProp.UseVisualStyleBackColor = true;
            this.btnDuplicateProp.Click += new System.EventHandler(this.btnDuplicateProp_Click_1);
            // 
            // btnRemoveProp
            // 
            this.btnRemoveProp.Location = new System.Drawing.Point(54, 3);
            this.btnRemoveProp.Name = "btnRemoveProp";
            this.btnRemoveProp.Size = new System.Drawing.Size(55, 23);
            this.btnRemoveProp.TabIndex = 48;
            this.btnRemoveProp.Text = "Remove";
            this.btnRemoveProp.UseVisualStyleBackColor = true;
            this.btnRemoveProp.Click += new System.EventHandler(this.btnRemoveProp_Click_1);
            // 
            // btnAddProp
            // 
            this.btnAddProp.Location = new System.Drawing.Point(9, 3);
            this.btnAddProp.Name = "btnAddProp";
            this.btnAddProp.Size = new System.Drawing.Size(37, 23);
            this.btnAddProp.TabIndex = 47;
            this.btnAddProp.Text = "Add";
            this.btnAddProp.UseVisualStyleBackColor = true;
            this.btnAddProp.Click += new System.EventHandler(this.btnAddProp_Click_1);
            // 
            // btnSortCreatures
            // 
            this.btnSortCreatures.Location = new System.Drawing.Point(182, 3);
            this.btnSortCreatures.Name = "btnSortCreatures";
            this.btnSortCreatures.Size = new System.Drawing.Size(41, 23);
            this.btnSortCreatures.TabIndex = 43;
            this.btnSortCreatures.Text = "Sort";
            this.toolTip1.SetToolTip(this.btnSortCreatures, "Sort:\r\nWill sort all Creatures within\r\neach category by\r\nalphabetical order.");
            this.btnSortCreatures.UseVisualStyleBackColor = true;
            this.btnSortCreatures.Click += new System.EventHandler(this.btnSortCreatures_Click);
            // 
            // btnSortItems
            // 
            this.btnSortItems.Location = new System.Drawing.Point(182, 3);
            this.btnSortItems.Name = "btnSortItems";
            this.btnSortItems.Size = new System.Drawing.Size(41, 23);
            this.btnSortItems.TabIndex = 47;
            this.btnSortItems.Text = "Sort";
            this.toolTip1.SetToolTip(this.btnSortItems, "Sort:\r\nWill sort all Items within\r\neach category by\r\nalphabetical order.");
            this.btnSortItems.UseVisualStyleBackColor = true;
            this.btnSortItems.Click += new System.EventHandler(this.btnSortItems_Click);
            // 
            // btnSortProps
            // 
            this.btnSortProps.Location = new System.Drawing.Point(182, 3);
            this.btnSortProps.Name = "btnSortProps";
            this.btnSortProps.Size = new System.Drawing.Size(41, 23);
            this.btnSortProps.TabIndex = 51;
            this.btnSortProps.Text = "Sort";
            this.toolTip1.SetToolTip(this.btnSortProps, "Sort:\r\nWill sort all Props within\r\neach category by\r\nalphabetical order.");
            this.btnSortProps.UseVisualStyleBackColor = true;
            this.btnSortProps.Click += new System.EventHandler(this.btnSortProps_Click);
            // 
            // Blueprints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 323);
            this.Controls.Add(this.tabCreatureItem);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HideOnClose = true;
            this.Name = "Blueprints";
            this.Text = "Blueprints";
            this.tabCreatureItem.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.TabPage tabPage1;
        public System.Windows.Forms.TabControl tabCreatureItem;
        public System.Windows.Forms.TreeView tvCreatures;
        public System.Windows.Forms.Button btnDuplicateCreature;
        public System.Windows.Forms.Button btnRemoveCreature;
        public System.Windows.Forms.Button btnAddCreature;
        public System.Windows.Forms.TreeView tvItems;
        public System.Windows.Forms.Button btnDuplicateItem;
        public System.Windows.Forms.Button btnRemoveItem;
        public System.Windows.Forms.Button btnAddItem;
        public System.Windows.Forms.TreeView tvProps;
        public System.Windows.Forms.Button btnDuplicateProp;
        public System.Windows.Forms.Button btnRemoveProp;
        public System.Windows.Forms.Button btnAddProp;
        private System.Windows.Forms.Button btnSortCreatures;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSortItems;
        private System.Windows.Forms.Button btnSortProps;
    }
}