using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace IB2Toolset
{
    public partial class EncounterEditor : DockContent
    {
        public ParentForm prntForm;
        public Module mod;

        private List<TileBitmapNamePair> tileList = new List<TileBitmapNamePair>();
        private Graphics device;
        private Bitmap surface;
        private Bitmap mapBitmap;
        private Bitmap selectedBitmap;
        public Bitmap g_walkPass;
        public Bitmap g_walkBlock;
        public Bitmap g_LoSBlock;
        private int sqr = 25;
        private int mSizeW = 800;
        private int mSizeH = 800;
        private Point currentPoint = new Point(0, 0);
        private Point lastPoint = new Point(0, 0);
        private int gridX = 0;
        private int gridY = 0;
        public string saveFilenameNoExt = "";
        public string returnMapFilenameNoExt;
        public int _selectedLbxEncounterIndex = 0;
        public int _selectedLbxCreatureIndex = 0;
        public string currentTileFilename = "t_grass";
        public bool tileSelected = true;
        public bool PcSelected = false;
        public bool CrtSelected = false;
        Font drawFont = new Font("Arial", 6);
        Font drawFontNum = new Font("Arial", 24);
        SolidBrush drawBrush = new SolidBrush(Color.Yellow);
        Pen blackPen = new Pen(Color.Black, 1);
        public Point currentSquareClicked = new Point(0, 0);
        public Point lastSquareClicked = new Point(0, 0);
        public selectionStruct selectedTile;
        public Point lastSelectedCreaturePropIcon;
        public string lastSelectedObjectTag;
        public string lastSelectedObjectResRef;
        public Creature le_selectedCreature = new Creature();
        public Prop le_selectedProp = new Prop();
        //public List<Bitmap> crtBitmapList = new List<Bitmap>(); //index will match AreaCreatureList index
        public List<Bitmap> propBitmapList = new List<Bitmap>(); //index will match AreaPropList index
        public Encounter thisEnc = new Encounter();

        public EncounterEditor(Module m, ParentForm p)
        {
            InitializeComponent();
            mod = m;
            prntForm = p;
            thisEnc = prntForm.encountersList[prntForm._selectedLbxEncounterIndex];
            createTileImageButtons();
            surface = new Bitmap(mSizeW, mSizeH);
            panelView.BackgroundImage = surface;            
            device = Graphics.FromImage(surface);
            try
            {
                g_walkPass = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_pass.png");
                g_walkBlock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_block.png");
                g_LoSBlock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\los_block.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to load walkPass and walkBlock bitmaps: " + ex.ToString());
            }
            refreshCmbItems();
            refreshLbxItems();
            refreshGoldDrop();
        }
        
        private void EncounterEditor_Load(object sender, EventArgs e)
        {
            //LoadEncounters();
            radioButton1.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            //createTileImageButtons(); 

            //thisEnc = prntForm.encountersList[_selectedLbxEncounterIndex];
            //thisEnc.MapSizeX = 7;
            //thisEnc.MapSizeY = 7;

            //createNewArea(thisEnc.MapSizeX, thisEnc.MapSizeY);

            //set up level drawing surface
            panelView.Width = thisEnc.MapSizeX * sqr;
            panelView.Height = thisEnc.MapSizeY * sqr;           
            surface = new Bitmap(panelView.Size.Width, panelView.Size.Height);
            //UpdatePB();
            device = Graphics.FromImage(surface);
            panelView.BackgroundImage = (Image)surface;

            rbtnInfo.Checked = true;
            rbtnZoom1x.Checked = true;
        }

        #region Encounter Stuff
        public void refreshCmbItems()
        {
            cmbItems.BeginUpdate();
            cmbItems.DataSource = null;
            cmbItems.DataSource = prntForm.itemsList;
            cmbItems.DisplayMember = "name";
            cmbItems.EndUpdate();
        }
        private void refreshLbxItems()
        {
            lbxItems.BeginUpdate();
            lbxItems.DataSource = null;
            lbxItems.DataSource = thisEnc.encounterInventoryRefsList;
            lbxItems.DisplayMember = "name";
            lbxItems.EndUpdate();
        }
        public void refreshGoldDrop()
        {
            numGold.Value = (int)thisEnc.goldDrop;
        }
        private void numGold_ValueChanged(object sender, EventArgs e)
        {

        }        
        public void saveEncountersFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(prntForm.encountersList, Formatting.Indented);
                using (StreamWriter sw = new StreamWriter(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\data\\encounters.json"))
                {
                    sw.Write(json.ToString());
                }
                MessageBox.Show("Saved File");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to Save File: " + ex.ToString());
            }
        }
        public List<Encounter> loadEncountersFile()
        {
            List<Encounter> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(prntForm._mainDirectory + "\\data\\encounters.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Encounter>)serializer.Deserialize(file, typeof(List<Encounter>));
            }
            return toReturn;
        }
        public List<Item> loadItemsFile()
        {
            List<Item> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(prntForm._mainDirectory + "\\data\\items.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Item>)serializer.Deserialize(file, typeof(List<Item>));
            }
            return toReturn;
        }
        private void btnAddItems_Click(object sender, EventArgs e)
        {
            try
            {
                Item it = prntForm.itemsList[cmbItems.SelectedIndex];
                ItemRefs newIR = prntForm.createItemRefsFromItem(it);
                thisEnc.encounterInventoryRefsList.Add(newIR);
                refreshLbxItems();
            }
            catch { }
        }
        private void btnRemoveItems_Click(object sender, EventArgs e)
        {
            if (lbxItems.Items.Count > 0)
            {
                try
                {
                    if (lbxItems.SelectedIndex >= 0)
                        thisEnc.encounterInventoryRefsList.RemoveAt(lbxItems.SelectedIndex);
                }
                catch { }
                refreshLbxItems();
            }
        }
        private void btnPlacePCs_Click(object sender, EventArgs e)
        {
            tileSelected = false;
            PcSelected = true;
            CrtSelected = false;
        }
        private void btnDeletePCs_Click(object sender, EventArgs e)
        {
            thisEnc.encounterPcStartLocations.Clear();
            refreshMap(true);
        }
        private void btnDeleteCreatures_Click(object sender, EventArgs e)
        {
            thisEnc.encounterCreatureRefsList.Clear();
            refreshMap(true);
        }
        #endregion

        private void resetPanelAndDeviceSize()
        {
            panelView.Width = thisEnc.MapSizeX * sqr;
            panelView.Height = thisEnc.MapSizeY * sqr;
            surface = new Bitmap(thisEnc.MapSizeX * sqr, thisEnc.MapSizeY * sqr);
            device = Graphics.FromImage(surface);
        }
        private void createTileImageButtons()
        {
            try
            {
                this.flPanelTab1.Controls.Clear();
                tileList.Clear();
                foreach (string f in Directory.GetFiles(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\", "*.png"))
                {
                    string filename = Path.GetFullPath(f);
                    using (Bitmap bit = new Bitmap(filename))
                    {
                        //bit = ResizeBitmap(bit, 50, 50);
                        Button btnNew = new Button();
                        btnNew.BackgroundImage = (Image)bit.Clone();
                        btnNew.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                        btnNew.FlatAppearance.BorderSize = 2;
                        btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                        btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
                        btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        btnNew.Size = new System.Drawing.Size(50 + 2, 50 + 2);
                        btnNew.BackgroundImageLayout = ImageLayout.Zoom;
                        btnNew.Text = Path.GetFileNameWithoutExtension(f);
                        btnNew.UseVisualStyleBackColor = true;
                        btnNew.Click += new System.EventHandler(this.btnSelectedTerrain_Click);
                        this.flPanelTab1.Controls.Add(btnNew);

                        //fill tileList as well
                        TileBitmapNamePair t = new TileBitmapNamePair((Bitmap)bit.Clone(), Path.GetFileNameWithoutExtension(f));
                        tileList.Add(t);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error: " + ex.ToString());
            }
        }
        private TileBitmapNamePair getTileByName(string name)
        {
            foreach (TileBitmapNamePair t in tileList)
            {
                if (t.filename == name)
                {
                    return t;
                }
            }
            return null;
        }
        private void btnSelectedTerrain_Click(object sender, EventArgs e)
        {
            Button selectBtn = (Button)sender;
            currentTileFilename = selectBtn.Text;
            selectedBitmap = (Bitmap)selectBtn.BackgroundImage.Clone();
            panel1.BackgroundImage = selectedBitmap;
        }
        private void refreshMap(bool refreshAll)
        {
            this.Cursor = Cursors.WaitCursor;
            if (thisEnc == null) { return; }
            if (refreshAll)
            {
                device.Clear(Color.Gainsboro);
            }
            try
            {
                //draw map from single image
                if ((thisEnc.UseMapImage) && (thisEnc.MapImage != "none"))
                {
                    //draw single image type map
                    if (mapBitmap == null)
                    {                        
                        if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + thisEnc.MapImage + ".jpg"))
                        {
                            mapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + thisEnc.MapImage + ".jpg");
                        }
                        else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + thisEnc.MapImage))
                        {
                            mapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + thisEnc.MapImage);
                        }
                        else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + thisEnc.MapImage + ".png"))
                        {
                            mapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + thisEnc.MapImage + ".png");
                        }
                        else
                        {
                            MessageBox.Show("Couldn't find map image " + thisEnc.MapImage + " in graphics folder, setting UseMapImage to false.");
                            thisEnc.UseMapImage = false;
                            return;
                        }
                    }
                    Rectangle src = new Rectangle(0, 0, mapBitmap.Width, mapBitmap.Height);
                    Rectangle dst = new Rectangle(0, 0, thisEnc.MapSizeX * sqr, thisEnc.MapSizeY * sqr);
                    device.DrawImage(mapBitmap, dst, src, GraphicsUnit.Pixel);
                    //Draw Grid stuff
                    for (int x = 0; x < thisEnc.MapSizeY; x++)
                    {
                        for (int y = 0; y < thisEnc.MapSizeX; y++)
                        {
                            TileEnc tile = thisEnc.encounterTiles[y * thisEnc.MapSizeX + x];
                            Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                            //draw square walkmesh and LoS stuff
                            src = new Rectangle(0, 0, 50, 50);
                            if (chkGrid.Checked) //if show grid is turned on, draw grid squares
                            {
                                if (tile.LoSBlocked)
                                {
                                    device.DrawImage(g_LoSBlock, target, src, GraphicsUnit.Pixel);
                                    device.DrawImage(g_LoSBlock, target, src, GraphicsUnit.Pixel);
                                }
                                if (tile.Walkable)
                                {
                                    device.DrawImage(g_walkPass, target, src, GraphicsUnit.Pixel);
                                }
                                else
                                {
                                    target = new Rectangle(x * sqr + 1, y * sqr + 1, sqr - 1, sqr - 1);
                                    device.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                                    device.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                                    device.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }
                }
                else
                {
                    //draw tile map
                    for (int y = 0; y < thisEnc.MapSizeY; y++)
                    {
                        for (int x = 0; x < thisEnc.MapSizeX; x++)
                        {
                            if ((refreshAll) || (currentSquareClicked == new Point(x, y)) || (lastSquareClicked == new Point(x, y)))
                            {
                                TileEnc tile = thisEnc.encounterTiles[y * thisEnc.MapSizeX + x];
                                Bitmap lyr1 = null;
                                Bitmap lyr2 = null;
                                Rectangle src1 = new Rectangle(0, 0, 50, 50);
                                Rectangle src2 = new Rectangle(0, 0, 50, 50);
                                //check to see if 100x100 tile image size
                                if (getTileByName(tile.Layer1Filename) != null)
                                {
                                    lyr1 = getTileByName(tile.Layer1Filename).bitmap;
                                    src1 = new Rectangle(0, 0, lyr1.Width, lyr1.Height);
                                }
                                if (getTileByName(tile.Layer2Filename) != null)
                                {
                                    lyr2 = getTileByName(tile.Layer2Filename).bitmap;
                                    src2 = new Rectangle(0, 0, lyr2.Width, lyr2.Height);
                                }

                                Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                                //draw layer 1 first
                                if (checkBox1.Checked)
                                {
                                    if (lyr1 != null)
                                    {
                                        device.DrawImage(lyr1, target, src1, GraphicsUnit.Pixel);
                                    }
                                }
                                //draw layer 2
                                if (checkBox2.Checked)
                                {
                                    if (lyr2 != null)
                                    {
                                        device.DrawImage(lyr2, target, src2, GraphicsUnit.Pixel);
                                    }
                                }
                                //draw square walkmesh and LoS stuff
                                Rectangle src = new Rectangle(0, 0, 50, 50);
                                if (chkGrid.Checked) //if show grid is turned on, draw grid squares
                                {
                                    if (tile.LoSBlocked)
                                    {
                                        device.DrawImage(g_LoSBlock, target, src, GraphicsUnit.Pixel);
                                        device.DrawImage(g_LoSBlock, target, src, GraphicsUnit.Pixel);
                                    }
                                    if (tile.Walkable)
                                    {
                                        device.DrawImage(g_walkPass, target, src, GraphicsUnit.Pixel);
                                    }
                                    else
                                    {
                                        target = new Rectangle(x * sqr + 1, y * sqr + 1, sqr - 1, sqr - 1);
                                        device.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                                        device.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                                        device.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                                    }
                                }
                                target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                                if (chkGrid.Checked)
                                {
                                    //device.DrawRectangle(blackPen, target);
                                }
                            }
                        }
                    }
                }

                //draw creatures
                foreach (CreatureRefs crtRef in thisEnc.encounterCreatureRefsList)
                {
                    int cspx = crtRef.creatureStartLocationX * sqr;
                    int cspy = crtRef.creatureStartLocationY * sqr;
                    Creature crt = prntForm.getCreatureByResRef(crtRef.creatureResRef);
                    Rectangle src = new Rectangle(0, 0, crt.creatureIconBitmap.Width, crt.creatureIconBitmap.Width);
                    Rectangle dst = new Rectangle(cspx, cspy, sqr, sqr);
                    if (crt.creatureIconBitmap.Width > 100)
                    {                    
                        dst = new Rectangle(cspx, cspy, sqr * 2, sqr * 2);
                    }
                    device.DrawImage(crt.creatureIconBitmap, dst, src, GraphicsUnit.Pixel);
                    if (sqr == 50) { drawFont = new Font("Arial", 6); }
                    else if (sqr == 25) { drawFont = new Font("Arial", 3); }
                    else if (sqr == 10) { drawFont = new Font("Arial", 2); }
                    device.DrawString(crtRef.creatureTag, drawFont, drawBrush, new Point(cspx, cspy + (sqr/2)));
                }
                //draw PCs
                int cnt = 0;
                foreach (Coordinate PCpoint in thisEnc.encounterPcStartLocations)
                {
                    int cspx = PCpoint.X * sqr;
                    int cspy = PCpoint.Y * sqr;
                    if (sqr == 50) { drawFontNum = new Font("Arial", 24); }
                    else if (sqr == 25) { drawFontNum = new Font("Arial", 12); }
                    else if (sqr == 10) { drawFontNum = new Font("Arial", 5); }
                    device.DrawString((cnt + 1).ToString(), drawFontNum, drawBrush, new Point(cspx, cspy + 5));
                    cnt++;
                }

                /*TODOint cnt = 0;
                foreach (Prop prpRef in thisEnc.Props)
                {
                    int cspx = prpRef.LocationX;
                    int cspy = prpRef.LocationY;
                    if ((refreshAll) || (currentSquareClicked == new Point(cspx, cspy)) || (lastSquareClicked == new Point(cspx, cspy)))
                    {                        
                        spritePropDraw(cspx, cspy, cnt);
                    }
                    cnt++;
                }*/
                /*TODOforeach (Trigger t in thisEnc.Triggers)
                {
                    foreach (Coordinate p in t.TriggerSquaresList)
                    {
                        if ((refreshAll) || (currentSquareClicked == new Point(p.X, p.Y)) || (lastSquareClicked == new Point(p.X, p.Y)))
                        {
                            int dx = p.X * sqr;
                            int dy = p.Y * sqr;
                            Pen pen = new Pen(Color.Orange, 2);
                            if ((t.Event1Type == "encounter") || (t.Event2Type == "encounter") || (t.Event3Type == "encounter"))
                            {
                                pen = new Pen(Color.Red, 2);
                            }
                            else if (t.Event1Type == "conversation")
                            {
                                pen = new Pen(Color.Yellow, 2);
                            }
                            else if (t.Event1Type == "script")
                            {
                                pen = new Pen(Color.Blue, 2);
                            }
                            else if (t.Event1Type == "transition")
                            {
                                pen = new Pen(Color.Lime, 2);
                            }
                            Rectangle rect = new Rectangle(dx + 3, dy + 3, sqr - 6, sqr - 6);
                            device.DrawRectangle(pen, rect);
                        }
                    }
                }*/
                UpdatePB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed on refresh map: " + ex.ToString());                
            }            
        }
        private void spritePropDraw(int cspx, int cspy, int spriteListIndex)
        {
            //source image
            Bitmap prpBitmap = propBitmapList[spriteListIndex];
            //Rectangle source = new Rectangle(0, 0, le_selectedProp.propBitmap.Width, le_selectedProp.propBitmap.Height);
            Rectangle source = new Rectangle(0, 0, prpBitmap.Width, prpBitmap.Height);
            //target location
            Rectangle target = new Rectangle(cspx * sqr, cspy * sqr, sqr, sqr);
            //draw sprite
            device.DrawImage((Image)prpBitmap, target, source, GraphicsUnit.Pixel);
        }
        /*private void drawTileSettings()
        {
            //for (int index = 0; index < area.MapSizeInSquares.Width * area.MapSizeInSquares.Height; index++)
            /*for (int x = 0; x < area.MapSizeX; x++)
            {
                for (int y = 0; y < area.MapSizeY; y++)
                {
                    if (chkGrid.Checked) //if show grid is turned on, draw grid squares
                    {
                        if (area.Tiles[y * this.area.MapSizeX + x].LoSBlocked)
                        {
                            Rectangle src = new Rectangle(0, 0, sqr, sqr);
                            int dx = x * sqr;
                            int dy = y * sqr;
                            Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                            device.DrawImage(g_LoSBlock, target, src, GraphicsUnit.Pixel);
                        }
                        if (area.Tiles[y * this.area.MapSizeX + x].Walkable)
                        {
                            Rectangle src = new Rectangle(0, 0, sqr, sqr);
                            int dx = x * sqr;
                            int dy = y * sqr;
                            Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                            device.DrawImage(g_walkPass, target, src, GraphicsUnit.Pixel);
                        }
                        else
                        {
                            Rectangle src = new Rectangle(0, 0, sqr, sqr);
                            int dx = x * sqr;
                            int dy = y * sqr;
                            Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                            device.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                        }
                    }
                }
            }*/
            /*foreach (Trigger t in area.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    int dx = p.X * sqr;
                    int dy = p.Y * sqr;
                    Pen pen = new Pen(Color.Orange, 2);
                    if ((t.Event1Type == "encounter") || (t.Event2Type == "encounter") || (t.Event3Type == "encounter"))
                    {
                        pen = new Pen(Color.Red, 2);
                    }
                    else if (t.Event1Type == "conversation")
                    {
                        pen = new Pen(Color.Yellow, 2);
                    }
                    else if (t.Event1Type == "script")
                    {
                        pen = new Pen(Color.Blue, 2);
                    }
                    else if (t.Event1Type == "transition")
                    {
                        pen = new Pen(Color.Lime, 2);
                    }
                    Rectangle rect = new Rectangle(dx + 3, dy + 3, sqr - 6, sqr - 6);
                    device.DrawRectangle(pen, rect);
                }
            }*/
            //panelView.BackgroundImage = surface;
        //}*/
        public void drawSelectionBox(int gridx, int gridy)
        {
            //draw selection box around tile
            int dx = gridx * sqr;
            int dy = gridy * sqr;
            Pen pen = new Pen(Color.DarkMagenta, 2);
            Rectangle rect = new Rectangle(dx + 1, dy + 1, sqr - 2, sqr - 2);
            device.DrawRectangle(pen, rect);

            //save changes
            UpdatePB();
        }
        public void UpdatePB()
        {
            this.Cursor = Cursors.Default;
            panelView.BackgroundImage = surface;
            panelView.Invalidate();
        }        
        
        private void panelView_MouseMove(object sender, MouseEventArgs e)
        {            
            gridX = e.X / sqr;
            gridY = e.Y / sqr;
            lblMouseInfo.Text = "gridX = " + gridX.ToString() + " : gridY = " + gridY.ToString();
            if (currentPoint != new Point(gridX, gridY))
            {
                if ((rbtnPaintTile.Checked) && (e.Button == MouseButtons.Left))
                {
                    clickDrawArea(e);
                }
            }
            if (prntForm.PropSelected)
            {
                refreshMap(true);
                try
                {
                    if (selectedBitmap != null)
                    {
                        //source image size
                        Rectangle frame = new Rectangle(0, 0, selectedBitmap.Width, selectedBitmap.Height);
                        //target location
                        Rectangle target = new Rectangle(gridX * sqr, gridY * sqr, sqr, sqr);
                        //draw sprite
                        device.DrawImage((Image)selectedBitmap, target, frame, GraphicsUnit.Pixel);
                    }
                }
                catch (Exception ex) { MessageBox.Show("failed mouse move: " + ex.ToString()); }
                //save changes
                UpdatePB();
            }
        }
        private void panelView_MouseDown(object sender, MouseEventArgs e)
        {
            /*
            int col = e.X / sqr;
            int row = e.Y / sqr;

            if (tileSelected)
            {
                //prntForm.encountersList[_selectedLbxEncounterIndex].encounterMapLayout[row, col] = currentTile;
                if (radioButton1.Checked)
                {
                    prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[row * 7 + col].Layer1Filename = currentTileFilename;
                }
                else if (radioButton2.Checked)
                {
                    prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[row * 7 + col].Layer2Filename = currentTileFilename;
                }
            }
            else if (rbtnWalkable.Checked)
            {
                if (prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[row * 7 + col].Walkable == true)
                {
                    prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[row * 7 + col].Walkable = false;
                }
                else
                {
                    prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[row * 7 + col].Walkable = true;
                }
                refreshMap();
            }
            else if (rbtnLoS.Checked)
            {
                if (prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[row * 7 + col].LoSBlocked == true)
                {
                    prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[row * 7 + col].LoSBlocked = false;
                }
                else
                {
                    prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[row * 7 + col].LoSBlocked = true;
                }
                refreshMap();
            }
            refreshMap();  
            */
        }
        private void panelView_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                //refreshMap();
                //UpdatePB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed on mouse leave map: " + ex.ToString());
            }
        }
        private void panelView_MouseEnter(object sender, EventArgs e)
        {
            panelView.Select();
            try
            {
                if (prntForm.selectedLevelMapCreatureTag != "")
                {
                    prntForm.CreatureSelected = true;
                }
                if (prntForm.selectedLevelMapPropTag != "")
                {
                    prntForm.PropSelected = true;
                }
                if (prntForm.CreatureSelected)
                {
                    string selectedCrt = prntForm.selectedEncounterCreatureTag;
                    le_selectedCreature = prntForm.getCreatureByTag(selectedCrt);
                    if (le_selectedCreature != null)
                    {
                        selectedBitmap = le_selectedCreature.creatureIconBitmap;
                        //selectedBitmapSize = le_selectedCreature.Size;
                    }
                }
                else if (prntForm.PropSelected)
                {
                    string selectedProp = prntForm.selectedLevelMapPropTag;
                    le_selectedProp = prntForm.getPropByTag(selectedProp);
                    if (le_selectedProp != null)
                    {
                        selectedBitmap = le_selectedProp.propBitmap;
                        //selectedBitmapSize = le_selectedProp.propBitmap.Width / (tileSize * 2);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed on mouse enter map: " + ex.ToString());
            }
        }
        private void panelView_MouseClick(object sender, MouseEventArgs e)
        {
            clickDrawArea(e);
        }
        private void clickDrawArea(MouseEventArgs e)
        {
            switch (e.Button)
            {
                #region Left Button
                case MouseButtons.Left:
                    refreshLeftPanelInfo();
                    prntForm.currentSelectedTrigger = null; 
                    gridX = e.X / sqr;
                    gridY = e.Y / sqr;
                    lastSquareClicked.X = currentSquareClicked.X;
                    lastSquareClicked.Y = currentSquareClicked.Y;
                    currentSquareClicked.X = gridX;
                    currentSquareClicked.Y = gridY;

                    #region PC Selected
                    if (PcSelected)
                    {
                        if (thisEnc.encounterPcStartLocations.Count < 6)
                        {
                            thisEnc.encounterPcStartLocations.Add(new Coordinate(gridX, gridY));
                        }
                        refreshMap(true);
                    }
                    #endregion
                    #region Creature Selected
                    else if (prntForm.CreatureSelected)
                    {
                        CreatureRefs crtRef = new CreatureRefs();
                        string _nodeTag = prntForm.frmBlueprints.tvCreatures.SelectedNode.Name;
                        crtRef.creatureResRef = prntForm.creaturesList[prntForm.frmBlueprints.GetCreatureIndex(_nodeTag)].cr_resref;
                        crtRef.creatureTag = prntForm.creaturesList[prntForm.frmBlueprints.GetCreatureIndex(_nodeTag)].cr_tag + "_" + prntForm.mod.nextIdNumber;
                        crtRef.creatureStartLocationX = gridX;
                        crtRef.creatureStartLocationY = gridY;
                        thisEnc.encounterCreatureRefsList.Add(crtRef);
                        refreshMap(true);
                    }
                    #endregion
                    #region Tile Selected
                    else if (rbtnPaintTile.Checked)
                    {
                        //gridX = e.X / sqr;
                        //gridY = e.Y / sqr;
                        selectedTile.index = gridY * thisEnc.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        if (radioButton1.Checked)
                        {
                            thisEnc.encounterTiles[selectedTile.index].Layer1Filename = currentTileFilename;
                            //if shift key is down, draw all between here and lastclickedsquare
                            if (Control.ModifierKeys == Keys.Shift)
                            {
                                Point cSqr = new Point(currentSquareClicked.X, currentSquareClicked.Y);
                                Point lSqr = new Point(lastSquareClicked.X, lastSquareClicked.Y);

                                int startX = lSqr.X;
                                int startY = lSqr.Y;
                                int endX = cSqr.X;
                                int endY = cSqr.Y;
                                if (lSqr.X >= cSqr.X)
                                {
                                    startX = cSqr.X;
                                    endX = lSqr.X;
                                }
                                if (lSqr.Y >= cSqr.Y)
                                {
                                    startY = cSqr.Y;
                                    endY = lSqr.Y;
                                }
                                for (int x = startX; x <= endX; x++)
                                {
                                    for (int y = startY; y <= endY; y++)
                                    {
                                        thisEnc.encounterTiles[y * thisEnc.MapSizeX + x].Layer1Filename = currentTileFilename;
                                        currentSquareClicked = new Point(x, y);
                                        refreshMap(false);
                                    }
                                }


                                /*if (cSqr.X == lSqr.X)
                                {
                                    if (cSqr.Y > lSqr.Y)
                                    {
                                        for (int i = lSqr.Y; i <= cSqr.Y; i++)
                                        {
                                            area.Tiles[i * area.MapSizeX + cSqr.X].Layer1Filename = currentTileFilename;
                                            currentSquareClicked = new Point(cSqr.X, i);
                                            refreshMap(false);
                                        }
                                    }
                                    else
                                    {
                                        for (int i = cSqr.Y; i <= lSqr.Y; i++)
                                        {
                                            area.Tiles[i * area.MapSizeX + cSqr.X].Layer1Filename = currentTileFilename;
                                            currentSquareClicked = new Point(cSqr.X, i);
                                            refreshMap(false);
                                        }
                                    }                                     
                                }
                                else if (cSqr.Y == lSqr.Y)
                                {
                                    if (cSqr.X > lSqr.X)
                                    {
                                        for (int i = lSqr.X; i <= cSqr.X; i++)
                                        {
                                            area.Tiles[cSqr.Y * area.MapSizeX + i].Layer1Filename = currentTileFilename;
                                            currentSquareClicked = new Point(i, cSqr.Y);
                                            refreshMap(false);
                                        }
                                    }
                                    else
                                    {
                                        for (int i = cSqr.X; i <= lSqr.X; i++)
                                        {
                                            area.Tiles[cSqr.Y * area.MapSizeX + i].Layer1Filename = currentTileFilename;
                                            currentSquareClicked = new Point(i, cSqr.Y);
                                            refreshMap(false);
                                        }
                                    }
                                }
                                else
                                {
                                    int startX = lSqr.X;
                                    int startY = lSqr.Y;
                                    int endX = cSqr.X;
                                    int endY = cSqr.Y;
                                    if (lSqr.X >= cSqr.X)
                                    {
                                        startX = cSqr.X;
                                        endX = lSqr.X;
                                    }
                                    if (lSqr.Y >= cSqr.Y)
                                    {
                                        startY = cSqr.Y;
                                        endY = lSqr.Y;
                                    }
                                    for (int x = startX; x <= endX; x++)
                                    {
                                        for (int y = startY; y <= endY; y++)
                                        {
                                            area.Tiles[y * area.MapSizeX + x].Layer1Filename = currentTileFilename;
                                            currentSquareClicked = new Point(x, y);
                                            refreshMap(false);
                                        }
                                    }
                                    
                                    prntForm.logText("Shift key down for line painting but last click must be a straight line to this click");
                                    prntForm.logText(Environment.NewLine);
                                }*/
                            }
                        }
                        else if (radioButton2.Checked)
                        {
                            thisEnc.encounterTiles[selectedTile.index].Layer2Filename = currentTileFilename;
                            if (Control.ModifierKeys == Keys.Shift)
                            {
                                Point cSqr = new Point(currentSquareClicked.X, currentSquareClicked.Y);
                                Point lSqr = new Point(lastSquareClicked.X, lastSquareClicked.Y);
                                int startX = lSqr.X;
                                int startY = lSqr.Y;
                                int endX = cSqr.X;
                                int endY = cSqr.Y;
                                if (lSqr.X >= cSqr.X)
                                {
                                    startX = cSqr.X;
                                    endX = lSqr.X;
                                }
                                if (lSqr.Y >= cSqr.Y)
                                {
                                    startY = cSqr.Y;
                                    endY = lSqr.Y;
                                }
                                for (int x = startX; x <= endX; x++)
                                {
                                    for (int y = startY; y <= endY; y++)
                                    {
                                        thisEnc.encounterTiles[y * thisEnc.MapSizeX + x].Layer2Filename = currentTileFilename;
                                        currentSquareClicked = new Point(x, y);
                                        refreshMap(false);
                                    }
                                }
                            }
                        }
                        refreshMap(false);
                    }
                    #endregion
                    #region Prop Selected
                    else if (prntForm.PropSelected)
                    {
                        /*TODO still need to add props to encounters
                        string selectedProp = prntForm.selectedLevelMapPropTag;
                        prntForm.logText(selectedProp);
                        prntForm.logText(Environment.NewLine);

                        //gridX = e.X / sqr;
                        //gridY = e.Y / sqr;

                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        // verify that there is no creature, blocked, or PC already on this location
                        // add to a List<> a new item with the x,y coordinates
                        if (le_selectedProp.ImageFileName == "blank")
                        {
                            return;
                        }
                        Prop newProp = new Prop();
                        newProp = le_selectedProp.DeepCopy();
                        newProp.PropTag = le_selectedProp.PropTag + "_" + prntForm.mod.nextIdNumber;
                        newProp.LocationX = gridX;
                        newProp.LocationY = gridY;
                        thisEnc.Props.Add(newProp);
                        // show the item on the map
                        if (mod.moduleName != "NewModule")
                        {
                            Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics\\" + le_selectedProp.ImageFileName + ".png");
                            propBitmapList.Add(newBitmap);
                        }
                        else
                        {
                            //do nothing if default module
                        }
                        refreshMap(false);
                        */
                    }
                    #endregion
                    #region Paint New Trigger Selected
                    else if (rbtnPaintTrigger.Checked)
                    {
                        /*TODO stil need to add triggers to encounters
                        string selectedTrigger = prntForm.selectedLevelMapTriggerTag;
                        prntForm.logText(selectedTrigger);
                        prntForm.logText(Environment.NewLine);

                        //gridX = e.X / sqr;
                        //gridY = e.Y / sqr;

                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        Point newPoint = new Point(gridX, gridY);
                        //add the selected square to the squareList if doesn't already exist
                        try
                        {
                            //check: if click square already exists, then erase from list                            
                            Trigger newTrigger = thisEnc.getTriggerByTag(selectedTrigger);
                            bool exists = false;
                            foreach (Coordinate p in newTrigger.TriggerSquaresList)
                            {
                                if ((p.X == newPoint.X) && (p.Y == newPoint.Y))
                                {
                                    //already exists, erase
                                    newTrigger.TriggerSquaresList.Remove(p);
                                    exists = true;
                                    break;
                                }
                            }
                            if (!exists) //doesn't exist so is a new point, add to list
                            {
                                Coordinate newCoor = new Coordinate();
                                newCoor.X = newPoint.X;
                                newCoor.Y = newPoint.Y;
                                newTrigger.TriggerSquaresList.Add(newCoor);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("The tag of the selected Trigger was not found in the area's trigger list");
                        }
                        //update the map to show colored squares    
                        refreshMap(false);
                        */
                    }
                    #endregion
                    #region Edit Trigger Selected
                    else if (rbtnEditTrigger.Checked)
                    {
                        /*TODO add triggers to encounters
                        if (prntForm.selectedLevelMapTriggerTag != null)
                        {
                            string selectedTrigger = prntForm.selectedLevelMapTriggerTag;
                            prntForm.logText(selectedTrigger);
                            prntForm.logText(Environment.NewLine);

                            //gridX = e.X / sqr;
                            //gridY = e.Y / sqr;

                            prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                            prntForm.logText(Environment.NewLine);
                            Point newPoint = new Point(gridX, gridY);
                            try
                            {
                                //check: if click square already exists, then erase from list  
                                Trigger newTrigger = thisEnc.getTriggerByTag(selectedTrigger);
                                if (newTrigger == null)
                                {
                                    MessageBox.Show("error: make sure to select a trigger to edit first (click info button then click on trigger)");
                                }
                                bool exists = false;
                                foreach (Coordinate p in newTrigger.TriggerSquaresList)
                                {
                                    if ((p.X == newPoint.X) && (p.Y == newPoint.Y))
                                    {
                                        //already exists, erase
                                        newTrigger.TriggerSquaresList.Remove(p);
                                        exists = true;
                                        break;
                                    }
                                }
                                if (!exists) //doesn't exist so is a new point, add to list
                                {
                                    Coordinate newCoor = new Coordinate();
                                    newCoor.X = newPoint.X;
                                    newCoor.Y = newPoint.Y;
                                    newTrigger.TriggerSquaresList.Add(newCoor);
                                    //newTrigger.TriggerSquaresList.Add(newPoint);
                                }
                            }
                            catch
                            {
                                MessageBox.Show("The tag of the selected Trigger was not found in the area's trigger list");
                            }
                            //update the map to show colored squares    
                            refreshMap(false);
                        }
                        */
                    }
                    #endregion
                    #region Walkmesh Toggle Selected
                    else if (rbtnWalkable.Checked)
                    {
                        //gridX = e.X / sqr;
                        //gridY = e.Y / sqr;
                        selectedTile.index = gridY * thisEnc.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        if (thisEnc.encounterTiles[selectedTile.index].Walkable == true)
                        {
                            thisEnc.encounterTiles[selectedTile.index].Walkable = false;
                        }
                        else
                        {
                            thisEnc.encounterTiles[selectedTile.index].Walkable = true;
                        }
                        refreshMap(false);
                    }
                    #endregion
                    #region LoS mesh Toggle Selected
                    else if (rbtnLoS.Checked)
                    {
                        //gridX = e.X / sqr;
                        //gridY = e.Y / sqr;
                        selectedTile.index = gridY * thisEnc.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        if (thisEnc.encounterTiles[selectedTile.index].LoSBlocked == true)
                        {
                            thisEnc.encounterTiles[selectedTile.index].LoSBlocked = false;
                        }
                        else
                        {
                            thisEnc.encounterTiles[selectedTile.index].LoSBlocked = true;
                        }
                        refreshMap(false);
                    }
                    #endregion
                    #region None Selected
                    else // not placing, just getting info and possibly deleteing icon
                    {
                        contextMenuStrip1.Items.Clear();
                        //when left click, get location
                        //gridX = e.X / sqr;
                        //gridY = e.Y / sqr;
                        Point newPoint = new Point(gridX, gridY);
                        EventHandler handler = new EventHandler(HandleContextMenuClick);
                        //loop through all the objects
                        //if has that location, add the tag to the list                    
                        //draw selection box
                        refreshMap(false);
                        drawSelectionBox(gridX, gridY);
                        txtSelectedIconInfo.Text = "";

                        foreach (CreatureRefs crt in thisEnc.encounterCreatureRefsList)
                        {
                            if ((crt.creatureStartLocationX == newPoint.X) && (crt.creatureStartLocationY == newPoint.Y))
                            {
                                Creature c = prntForm.getCreatureByResRef(crt.creatureResRef);
                                // if so then give details about that icon (name, tag, etc.)
                                txtSelectedIconInfo.Text = "name: " + c.cr_name + Environment.NewLine
                                                            + "tag: " + crt.creatureTag + Environment.NewLine
                                                            + "resref: " + crt.creatureResRef;
                                //lastSelectedObjectResRef = crt.CreatureResRef;
                                lastSelectedObjectTag = crt.creatureTag;
                                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = crt;
                            }
                        }
                        /*TODOforeach (Prop prp in thisEnc.Props)
                        {
                            if ((prp.LocationX == newPoint.X) && (prp.LocationY == newPoint.Y))
                            {
                                // if so then give details about that icon (name, tag, etc.)
                                txtSelectedIconInfo.Text = "name: " + prp.ImageFileName + Environment.NewLine
                                                            + "tag: " + prp.PropTag + Environment.NewLine;
                                lastSelectedObjectTag = prp.PropTag;
                                //prntForm.selectedLevelMapPropTag = prp.PropTag;
                                panelView.ContextMenuStrip.Items.Add(prp.PropTag, null, handler); //string, image, handler
                                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = prp;
                            }
                        }*/
                        /*TODOforeach (Trigger t in thisEnc.Triggers)
                        {
                            foreach (Coordinate p in t.TriggerSquaresList)
                            {
                                if ((p.X == newPoint.X) && (p.Y == newPoint.Y))
                                {
                                    txtSelectedIconInfo.Text = "Trigger Tag: " + Environment.NewLine + t.TriggerTag;
                                    lastSelectedObjectTag = t.TriggerTag;
                                    prntForm.currentSelectedTrigger = t;
                                    prntForm.frmTriggerEvents.refreshTriggers();
                                    panelView.ContextMenuStrip.Items.Add(t.TriggerTag, null, handler); //string, image, handler
                                    //prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = t;
                                }
                            }
                        }*/
                        //if the list is less than 2, do nothing
                        if (panelView.ContextMenuStrip.Items.Count > 1)
                        {
                            contextMenuStrip1.Show(panelView, e.Location);
                        }
                        prntForm.frmTriggerEvents.refreshTriggers();
                    }
                    #endregion
                    break;
                #endregion
                #region Right Button
                case MouseButtons.Right:
                    // exit by right click or ESC
                    prntForm.logText("entered right-click");
                    prntForm.logText(Environment.NewLine);
                    prntForm.selectedLevelMapCreatureTag = "";
                    prntForm.selectedLevelMapPropTag = "";
                    prntForm.CreatureSelected = false;
                    prntForm.PropSelected = false;
                    prntForm.currentSelectedTrigger = null;
                    refreshMap(true);
                    UpdatePB();
                    rbtnInfo.Checked = true;
                    break;
                #endregion
            }
        }
        private void btnRefreshMap_Click(object sender, EventArgs e)
        {
            refreshMap(true);
        }
        
        private void btnFillWithSelected_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < thisEnc.MapSizeX; x++)
            {
                for (int y = 0; y < thisEnc.MapSizeY; y++)
                {
                    selectedTile.index = y * thisEnc.MapSizeX + x;
                    if (radioButton1.Checked)
                    {
                        thisEnc.encounterTiles[selectedTile.index].Layer1Filename = currentTileFilename;
                    }
                    else if (radioButton2.Checked)
                    {
                        thisEnc.encounterTiles[selectedTile.index].Layer2Filename = currentTileFilename;
                    }                    
                }
            }
            refreshMap(true);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            refreshMap(true);
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            refreshMap(true);
        }
        

        #region Methods
        private void loadAreaObjectBitmapLists()
        {
            /*TODO add props to encounters
            foreach (Prop prp in thisEnc.Props)
            {
                // get Prop by Tag and then get Icon filename, add Bitmap to list
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + prp.ImageFileName + ".png"))
                {
                    Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + prp.ImageFileName + ".png");
                    propBitmapList.Add(newBitmap);
                }
                else
                {
                    MessageBox.Show("Failed to find prop image (" + prp.ImageFileName + ") in graphics folder");
                }
            }
            */
        }
        /*private void openLevel(string g_dir, string g_fil, string g_filNoEx)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                area = area.loadAreaFile(g_dir + "\\" + g_fil + ".lvl");
                if (area == null)
                {
                    MessageBox.Show("returned a null area");
                }
                loadAreaObjectBitmapLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open file: " + ex.ToString());
            }
            
            refreshLeftPanelInfo();
            panelView.Width = area.MapSizeX * sqr;
            panelView.Height = area.MapSizeY * sqr;
            panelView.BackgroundImage = (Image)surface;
            device = Graphics.FromImage(surface);
            if (surface == null)
            {
                MessageBox.Show("returned a null Map bitmap");
                return;
            }
            refreshMap(true);
            this.Cursor = Cursors.Arrow;
        }*/
        private void createNewArea(int width, int height)
        {
            //create tilemap
            thisEnc = null;
            thisEnc = new Encounter();
            thisEnc.MapSizeX = width;
            thisEnc.MapSizeY = height;
            for (int index = 0; index < (width * height); index++)
            {
                TileEnc newTile = new TileEnc();
                newTile.Walkable = true;
                newTile.LoSBlocked = false;
                thisEnc.encounterTiles.Add(newTile);
            }
            refreshLeftPanelInfo();
            panelView.Width = thisEnc.MapSizeX * sqr;
            panelView.Height = thisEnc.MapSizeY * sqr;
            panelView.BackgroundImage = (Image)surface;
            device = Graphics.FromImage(surface);
            if (surface == null)
            {
                MessageBox.Show("returned a null Map bitmap");
                return;
            }
            refreshMap(true);
        }
        private void resetAreaTileValues(int width, int height)
        {
            thisEnc.MapSizeX = width;
            thisEnc.MapSizeY = height;
            thisEnc.encounterTiles.Clear();
            for (int index = 0; index < (width * height); index++)
            {
                TileEnc newTile = new TileEnc();
                newTile.Walkable = true;
                newTile.LoSBlocked = false;
                thisEnc.encounterTiles.Add(newTile);
            }
        }
        public void refreshLeftPanelInfo()
        {
            lblMapSizeX.Text = thisEnc.MapSizeX.ToString();
            lblMapSizeY.Text = thisEnc.MapSizeY.ToString();
            selectedTile.x = gridX;
            selectedTile.y = gridY;
            selectedTile.index = gridY * thisEnc.MapSizeX + gridX;
            drawSelectionBox(gridX, gridY);
        }
        private void mapSizeChangeStuff()
        {
            lblMapSizeX.Text = thisEnc.MapSizeX.ToString();
            lblMapSizeY.Text = thisEnc.MapSizeY.ToString();
            resetPanelAndDeviceSize();
        }
        #endregion

        #region Event Handlers        
        private void EncounterEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("closing editor and removing from openAreaList");
            //prntForm.openAreasList.Remove(area);
        }
        private void EncounterEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            device.Dispose();
            surface.Dispose();
            //gfxSelected.Dispose();
            //selectedBitmap.Dispose();
            //this.Close();
        }
        private void rbtnInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnInfo.Checked)
            {
                prntForm.logText("info on selecting map objects");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                //refreshMap(true);
                //UpdatePB();
            }
        }
        private void rbtnWalkable_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnWalkable.Checked)
            {
                prntForm.logText("editing walkmesh");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.selectedLevelMapTriggerTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                //refreshMap(true);
                //UpdatePB();
            }
        }
        private void rbtnLoS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLoS.Checked)
            {
                prntForm.logText("editing line-of-sight mesh");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.selectedLevelMapTriggerTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                //refreshMap(true);
                //UpdatePB();
            }
        }
        private void btnRemoveSelectedObject_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (CreatureRefs crt in thisEnc.encounterCreatureRefsList)
            {
                if (crt.creatureTag == lastSelectedObjectTag)
                {
                    // remove at index of matched location
                    thisEnc.encounterCreatureRefsList.RemoveAt(cnt);
                    //crtBitmapList.RemoveAt(cnt);
                    refreshMap(true);
                    return;
                }
                cnt++;
            }
            /*TODOforeach (Prop prp in thisEnc.Props)
            {
                if (prp.PropTag == lastSelectedObjectTag)
                {
                    // remove at index of matched tag
                    area.Props.RemoveAt(cnt);
                    propBitmapList.RemoveAt(cnt);
                    refreshMap(true);
                    return;
                }
                cnt++;
            }*/
            /*TODOforeach (Trigger t in area.Triggers)
            {
                if (t.TriggerTag == lastSelectedObjectTag)
                {
                    // remove at index of matched tag
                    area.Triggers.Remove(t);
                    refreshMap(true);
                    return;
                }
            }*/
        }
        private void rbtnPaintTrigger_CheckedChanged(object sender, EventArgs e)
        {
            /*TODOif (rbtnPaintTrigger.Checked)
            {
                //create a new trigger object
                Trigger newTrigger = new Trigger();
                //increment the tag to something unique
                newTrigger.TriggerTag = "newTrigger_" + prntForm.mod.nextIdNumber;
                prntForm.selectedLevelMapTriggerTag = newTrigger.TriggerTag;
                thisEnc.Triggers.Add(newTrigger);
                //set propertygrid to the new object
                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = newTrigger;
                prntForm.logText("painting a new trigger");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                //refreshMap(true);
                //UpdatePB();
            }*/
        }
        private void rbtnEditTrigger_CheckedChanged(object sender, EventArgs e)
        {
            /*TODOif (rbtnEditTrigger.Checked)
            {
                prntForm.logText("edit trigger: ");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                prntForm.selectedLevelMapTriggerTag = lastSelectedObjectTag;
                //refreshMap(true);
                //UpdatePB();
            }*/
        }
        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            refreshMap(true);
        }
        public void HandleContextMenuClick(object sender, EventArgs e)
        {
            //else, handler returns the selected tag
            ToolStripMenuItem menuItm = (ToolStripMenuItem)sender;
            /*TODOforeach (Prop prp in area.Props)
            {
                if (prp.PropTag == menuItm.Text)
                {
                    // if so then give details about that icon (name, tag, etc.)
                    txtSelectedIconInfo.Text = "name: " + prp.PropName + Environment.NewLine + "tag: " + prp.PropTag;
                    lastSelectedObjectTag = prp.PropTag;
                    //prntForm.selectedLevelMapPropTag = prp.PropTag;
                    prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = prp;
                    return;
                }
            }*/
            /*TODOforeach (Trigger t in area.Triggers)
            {
                if (t.TriggerTag == menuItm.Text)
                {
                    txtSelectedIconInfo.Text = "Trigger Tag: " + Environment.NewLine + t.TriggerTag;
                    lastSelectedObjectTag = t.TriggerTag;
                    prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = t;
                    return;
                }
            }*/
        }
        private void btnPlusLeftX_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            int oldX = thisEnc.MapSizeX;
            for (int i = thisEnc.encounterTiles.Count - oldX; i >= 0; i -= oldX)
            {
                TileEnc newTile = new TileEnc();
                thisEnc.encounterTiles.Insert(i, newTile);
            }
            /*TODOforeach (Prop prpRef in thisEnc.Props)
            {
                prpRef.LocationX++;
            }
            foreach (Trigger t in thisEnc.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    p.X++;
                }
            }*/
            thisEnc.MapSizeX++;
            mapSizeChangeStuff();
        }
        private void btnMinusLeftX_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            int oldX = thisEnc.MapSizeX;
            for (int i = thisEnc.encounterTiles.Count - oldX; i >= 0; i -= oldX)
            {
                thisEnc.encounterTiles.RemoveAt(i);
            }
            /*foreach (Prop prpRef in thisEnc.Props)
            {
                prpRef.LocationX--;
            }
            foreach (Trigger t in thisEnc.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    p.X--;
                }
            }*/
            thisEnc.MapSizeX--;
            mapSizeChangeStuff();
        }
        private void btnPlusRightX_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            int oldX = thisEnc.MapSizeX;
            for (int i = thisEnc.encounterTiles.Count - 1; i >= 0; i -= oldX)
            {
                TileEnc newTile = new TileEnc();
                thisEnc.encounterTiles.Insert(i + 1, newTile);
            }
            thisEnc.MapSizeX++;
            mapSizeChangeStuff();
        }
        private void btnMinusRightX_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            int oldX = thisEnc.MapSizeX;
            for (int i = thisEnc.encounterTiles.Count - 1; i >= 0; i -= oldX)
            {
                thisEnc.encounterTiles.RemoveAt(i);
            }
            thisEnc.MapSizeX--;
            mapSizeChangeStuff();
        }
        private void btnPlusTopY_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            for (int i = 0; i < thisEnc.MapSizeX; i++)
            {
                TileEnc newTile = new TileEnc();
                thisEnc.encounterTiles.Insert(0, newTile);
            }
            /*TODOforeach (Prop prpRef in thisEnc.Props)
            {
                prpRef.LocationY++;
            }
            foreach (Trigger t in thisEnc.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    p.Y++;
                }
            }*/
            thisEnc.MapSizeY++;
            mapSizeChangeStuff();
        }
        private void btnMinusTopY_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            for (int i = 0; i < thisEnc.MapSizeX; i++)
            {
                thisEnc.encounterTiles.RemoveAt(0);
            }
            /*TODOforeach (Prop prpRef in thisEnc.Props)
            {
                prpRef.LocationY--;
            }
            foreach (Trigger t in thisEnc.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    p.Y--;
                }
            }*/
            thisEnc.MapSizeY--;
            mapSizeChangeStuff();
        }
        private void btnPlusBottumY_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            for (int i = 0; i < thisEnc.MapSizeX; i++)
            {
                TileEnc newTile = new TileEnc();
                thisEnc.encounterTiles.Add(newTile);
            }
            thisEnc.MapSizeY++;
            mapSizeChangeStuff();
        }
        private void btnMinusBottumY_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            for (int i = 0; i < thisEnc.MapSizeX; i++)
            {
                thisEnc.encounterTiles.RemoveAt(thisEnc.encounterTiles.Count - 1);
            }
            thisEnc.MapSizeY--;
            mapSizeChangeStuff();
        }
        private void btnProperties_Click(object sender, EventArgs e)
        {
            prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = thisEnc;
        }
        private void panelView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                // exit by right click or ESC
                prntForm.logText("pressed escape");
                prntForm.logText(Environment.NewLine);
                //prntForm.selectedEncounterCreatureTag = "";
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                refreshMap(true);
                UpdatePB();
                rbtnInfo.Checked = true;
            }
        }
        #endregion

        private void rbtnZoom1x_CheckedChanged(object sender, EventArgs e)
        {
            sqr = 50;
            resetPanelAndDeviceSize();
            refreshMap(true);
        }

        private void rbtnZoom2x_CheckedChanged(object sender, EventArgs e)
        {
            sqr = 25;
            resetPanelAndDeviceSize();
            refreshMap(true);
        }

        private void rbtnZoom5x_CheckedChanged(object sender, EventArgs e)
        {
            sqr = 10;
            resetPanelAndDeviceSize();
            refreshMap(true);
        }
    }    
}
