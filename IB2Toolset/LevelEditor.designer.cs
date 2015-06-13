namespace IB2Toolset
{
    partial class LevelEditor
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lblMouseInfo = new System.Windows.Forms.Label();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTilemapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTilemapToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveLevelBMP4096x4096ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuViewShowTileNumbers = new System.Windows.Forms.ToolStripMenuItem();
            this.tilePaletteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.saveEditorPaletteBMP165x960ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTilePaletteBMP512x512ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbtnLoS = new System.Windows.Forms.RadioButton();
            this.rbtnInfo = new System.Windows.Forms.RadioButton();
            this.rbtnWalkable = new System.Windows.Forms.RadioButton();
            this.rbtnEditTrigger = new System.Windows.Forms.RadioButton();
            this.rbtnPaintTrigger = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRemoveSelectedObject = new System.Windows.Forms.Button();
            this.txtSelectedIconInfo = new System.Windows.Forms.TextBox();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.panel3 = new IB2Toolset.PanelNoScrollOnFocus();
            this.btnLoadMap = new System.Windows.Forms.Button();
            this.btnProperties = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblMouseInfo
            // 
            this.lblMouseInfo.Location = new System.Drawing.Point(2, 541);
            this.lblMouseInfo.Name = "lblMouseInfo";
            this.lblMouseInfo.Size = new System.Drawing.Size(121, 30);
            this.lblMouseInfo.TabIndex = 0;
            this.lblMouseInfo.Text = "CURSOR/GRID";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(220, 22);
            this.toolStripMenuItem2.Text = "New Tilemap";
            // 
            // loadTilemapToolStripMenuItem
            // 
            this.loadTilemapToolStripMenuItem.Name = "loadTilemapToolStripMenuItem";
            this.loadTilemapToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.loadTilemapToolStripMenuItem.Text = "Load Tilemap";
            // 
            // saveTilemapToolStripMenuItem1
            // 
            this.saveTilemapToolStripMenuItem1.Name = "saveTilemapToolStripMenuItem1";
            this.saveTilemapToolStripMenuItem1.Size = new System.Drawing.Size(220, 22);
            this.saveTilemapToolStripMenuItem1.Text = "Save Tilemap";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(217, 6);
            // 
            // saveLevelBMP4096x4096ToolStripMenuItem
            // 
            this.saveLevelBMP4096x4096ToolStripMenuItem.Name = "saveLevelBMP4096x4096ToolStripMenuItem";
            this.saveLevelBMP4096x4096ToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.saveLevelBMP4096x4096ToolStripMenuItem.Text = "Save Level BMP (4096x4096)";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuViewShowTileNumbers});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // menuViewShowTileNumbers
            // 
            this.menuViewShowTileNumbers.Checked = true;
            this.menuViewShowTileNumbers.CheckOnClick = true;
            this.menuViewShowTileNumbers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuViewShowTileNumbers.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.menuViewShowTileNumbers.Name = "menuViewShowTileNumbers";
            this.menuViewShowTileNumbers.Size = new System.Drawing.Size(143, 22);
            this.menuViewShowTileNumbers.Text = "Show Tile #\'s";
            // 
            // tilePaletteToolStripMenuItem
            // 
            this.tilePaletteToolStripMenuItem.Name = "tilePaletteToolStripMenuItem";
            this.tilePaletteToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.tilePaletteToolStripMenuItem.Text = "Tile Palette";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(231, 22);
            this.toolStripMenuItem1.Text = "Load Palette Bitmap";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(228, 6);
            // 
            // saveEditorPaletteBMP165x960ToolStripMenuItem
            // 
            this.saveEditorPaletteBMP165x960ToolStripMenuItem.Name = "saveEditorPaletteBMP165x960ToolStripMenuItem";
            this.saveEditorPaletteBMP165x960ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // saveTilePaletteBMP512x512ToolStripMenuItem1
            // 
            this.saveTilePaletteBMP512x512ToolStripMenuItem1.Name = "saveTilePaletteBMP512x512ToolStripMenuItem1";
            this.saveTilePaletteBMP512x512ToolStripMenuItem1.Size = new System.Drawing.Size(231, 22);
            this.saveTilePaletteBMP512x512ToolStripMenuItem1.Text = "Save Palette Texture (512x512)";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 26);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbtnLoS);
            this.groupBox2.Controls.Add(this.rbtnInfo);
            this.groupBox2.Controls.Add(this.rbtnWalkable);
            this.groupBox2.Controls.Add(this.rbtnEditTrigger);
            this.groupBox2.Controls.Add(this.rbtnPaintTrigger);
            this.groupBox2.Location = new System.Drawing.Point(5, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(118, 315);
            this.groupBox2.TabIndex = 70;
            this.groupBox2.TabStop = false;
            // 
            // rbtnLoS
            // 
            this.rbtnLoS.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnLoS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnLoS.Location = new System.Drawing.Point(9, 262);
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
            this.rbtnWalkable.Location = new System.Drawing.Point(9, 209);
            this.rbtnWalkable.Name = "rbtnWalkable";
            this.rbtnWalkable.Size = new System.Drawing.Size(103, 45);
            this.rbtnWalkable.TabIndex = 3;
            this.rbtnWalkable.TabStop = true;
            this.rbtnWalkable.Text = "Toggle\r\nWalkable";
            this.rbtnWalkable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnWalkable.UseVisualStyleBackColor = true;
            this.rbtnWalkable.CheckedChanged += new System.EventHandler(this.rbtnWalkable_CheckedChanged);
            // 
            // rbtnEditTrigger
            // 
            this.rbtnEditTrigger.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnEditTrigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnEditTrigger.Location = new System.Drawing.Point(9, 122);
            this.rbtnEditTrigger.Name = "rbtnEditTrigger";
            this.rbtnEditTrigger.Size = new System.Drawing.Size(103, 79);
            this.rbtnEditTrigger.TabIndex = 2;
            this.rbtnEditTrigger.TabStop = true;
            this.rbtnEditTrigger.Text = "Edit Last\r\nSelected\r\nTrigger\r\nLocation";
            this.rbtnEditTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnEditTrigger.UseVisualStyleBackColor = true;
            this.rbtnEditTrigger.CheckedChanged += new System.EventHandler(this.rbtnEditTrigger_CheckedChanged);
            // 
            // rbtnPaintTrigger
            // 
            this.rbtnPaintTrigger.Appearance = System.Windows.Forms.Appearance.Button;
            this.rbtnPaintTrigger.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnPaintTrigger.Location = new System.Drawing.Point(9, 53);
            this.rbtnPaintTrigger.Name = "rbtnPaintTrigger";
            this.rbtnPaintTrigger.Size = new System.Drawing.Size(103, 61);
            this.rbtnPaintTrigger.TabIndex = 0;
            this.rbtnPaintTrigger.TabStop = true;
            this.rbtnPaintTrigger.Text = "Paint\r\nNew\r\nTrigger";
            this.rbtnPaintTrigger.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbtnPaintTrigger.UseVisualStyleBackColor = true;
            this.rbtnPaintTrigger.CheckedChanged += new System.EventHandler(this.rbtnPaintTrigger_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRemoveSelectedObject);
            this.groupBox1.Controls.Add(this.txtSelectedIconInfo);
            this.groupBox1.Location = new System.Drawing.Point(5, 393);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 142);
            this.groupBox1.TabIndex = 67;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selected Object Info";
            // 
            // btnRemoveSelectedObject
            // 
            this.btnRemoveSelectedObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveSelectedObject.Location = new System.Drawing.Point(6, 74);
            this.btnRemoveSelectedObject.Name = "btnRemoveSelectedObject";
            this.btnRemoveSelectedObject.Size = new System.Drawing.Size(106, 61);
            this.btnRemoveSelectedObject.TabIndex = 1;
            this.btnRemoveSelectedObject.Text = "Remove Last Selected Object";
            this.btnRemoveSelectedObject.UseVisualStyleBackColor = true;
            this.btnRemoveSelectedObject.Click += new System.EventHandler(this.btnRemoveSelectedObject_Click);
            // 
            // txtSelectedIconInfo
            // 
            this.txtSelectedIconInfo.Location = new System.Drawing.Point(6, 19);
            this.txtSelectedIconInfo.Multiline = true;
            this.txtSelectedIconInfo.Name = "txtSelectedIconInfo";
            this.txtSelectedIconInfo.Size = new System.Drawing.Size(106, 54);
            this.txtSelectedIconInfo.TabIndex = 0;
            // 
            // chkGrid
            // 
            this.chkGrid.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkGrid.Checked = true;
            this.chkGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGrid.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.chkGrid.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
            this.chkGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGrid.Location = new System.Drawing.Point(14, 362);
            this.chkGrid.Margin = new System.Windows.Forms.Padding(0);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.Size = new System.Drawing.Size(103, 28);
            this.chkGrid.TabIndex = 20;
            this.chkGrid.Text = "GRID";
            this.chkGrid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkGrid.UseVisualStyleBackColor = true;
            this.chkGrid.CheckedChanged += new System.EventHandler(this.chkGrid_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.AutoScroll = true;
            this.panel3.BackColor = System.Drawing.Color.Black;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Location = new System.Drawing.Point(129, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(725, 650);
            this.panel3.TabIndex = 16;
            // 
            // btnLoadMap
            // 
            this.btnLoadMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadMap.Location = new System.Drawing.Point(14, 6);
            this.btnLoadMap.Name = "btnLoadMap";
            this.btnLoadMap.Size = new System.Drawing.Size(103, 29);
            this.btnLoadMap.TabIndex = 71;
            this.btnLoadMap.Text = "Load Map";
            this.btnLoadMap.UseVisualStyleBackColor = true;
            this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
            // 
            // btnProperties
            // 
            this.btnProperties.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProperties.Location = new System.Drawing.Point(11, 574);
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(106, 48);
            this.btnProperties.TabIndex = 72;
            this.btnProperties.Text = "Area Properties";
            this.btnProperties.UseVisualStyleBackColor = true;
            this.btnProperties.Click += new System.EventHandler(this.btnProperties_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox1.Location = new System.Drawing.Point(-2, -2);
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
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 662);
            this.Controls.Add(this.btnProperties);
            this.Controls.Add(this.lblMouseInfo);
            this.Controls.Add(this.btnLoadMap);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.chkGrid);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.Name = "LevelEditor";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Document;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tilemap Level Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LevelEditor_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LevelEditor_FormClosed);
            this.Load += new System.EventHandler(this.LevelEditor_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LevelEditor_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label lblMouseInfo;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTilemapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTilemapToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveLevelBMP4096x4096ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuViewShowTileNumbers;
        private System.Windows.Forms.ToolStripMenuItem tilePaletteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveEditorPaletteBMP165x960ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTilePaletteBMP512x512ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private PanelNoScrollOnFocus panel3;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnRemoveSelectedObject;
        private System.Windows.Forms.TextBox txtSelectedIconInfo;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbtnWalkable;
        private System.Windows.Forms.RadioButton rbtnEditTrigger;
        private System.Windows.Forms.RadioButton rbtnPaintTrigger;
        private System.Windows.Forms.RadioButton rbtnInfo;
        private System.Windows.Forms.Button btnLoadMap;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnProperties;
        private System.Windows.Forms.RadioButton rbtnLoS;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
