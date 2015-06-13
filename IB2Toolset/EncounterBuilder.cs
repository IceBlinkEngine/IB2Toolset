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

namespace IB2Toolset
{
    public partial class EncounterBuilder : Form
    {
        public ParentForm prntForm;
        public Module mod;

        //public List<Encounter> prntForm.encountersList = new List<Encounter>();
        //public List<Creature> prntForm.creaturesList = new List<Creature>();
        //public List<Item> prntForm.itemsList = new List<Item>(); 
        private List<TileBitmapNamePair> tileList = new List<TileBitmapNamePair>();
        private Graphics device;
        private Bitmap surface;
        private Bitmap mapBitmap;
        private Bitmap grass;
        private Bitmap mud;
        private Bitmap black;
        private Bitmap tree;
        private Bitmap rock;
        private Bitmap floor;
        private Bitmap wall;
        private Bitmap selectedBitmap;
        public Bitmap g_walkPass;
        public Bitmap g_walkBlock;
        public Bitmap g_LoSBlock;
        private int sqr = 50;
        private int mSizeW = 350;
        private int mSizeH = 350;
        private Point currentPoint = new Point(0, 0);
        private Point lastPoint = new Point(0, 0);
        private int gridX = 0;
        private int gridY = 0;
        public string saveFilenameNoExt = "";
        //public string prntForm._mainDirectory;
        public string returnMapFilenameNoExt;
        public int _selectedLbxEncounterIndex = 0;
        public int _selectedLbxCreatureIndex = 0;
        public char currentTile = 'g';
        public string currentTileFilename = "t_grass";
        public bool tileSelected = true;
        public bool PcSelected = false;
        public bool CrtSelected = false;
        Font drawFont = new Font("Arial", 6);
        Font drawFontNum = new Font("Arial", 24);
        SolidBrush drawBrush = new SolidBrush(Color.Yellow);
        Pen blackPen = new Pen(Color.Black, 1);

        public EncounterBuilder(Module m, ParentForm p)
        {
            InitializeComponent();
            mod = m;
            prntForm = p;
            createTileImageButtons();
            //prntForm._mainDirectory = Directory.GetCurrentDirectory();
            surface = new Bitmap(mSizeW, mSizeH);
            panelView.BackgroundImage = surface;            
            device = Graphics.FromImage(surface);
            g_walkPass = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_pass.png");
            g_walkBlock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_block.png");
            g_LoSBlock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\los_block.png");
            //grass = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\t_grass.png");
            //mud = new Bitmap(prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics\\t_mud.png");
            black = new Bitmap(prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics\\black_tile.png");
            //tree = new Bitmap(prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics\\tree.png");
            //floor = new Bitmap(prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics\\t_stone_floor.png");
            //wall = new Bitmap(prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics\\t_stone_wall.png");
            //rock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics\\rock.png");
            if (prntForm.encountersList.Count < 1)
            {
                Encounter newEnc = new Encounter();
                newEnc.SetAllToGrass();
                prntForm.encountersList.Add(newEnc);
            }
            refreshMap();
            refreshListBoxEncounters();
            refreshListBoxCreatures();
            refreshCmbItems();
            refreshLbxItems();
            refreshGoldDrop();
        }
        private void EncounterBuilder_Load(object sender, EventArgs e)
        {
            LoadEncounters();
            radioButton1.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            prntForm.refreshDropDownLists();
            //createTileImageButtons();            
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
                        Button btnNew = new Button();
                        btnNew.BackgroundImage = (Image)bit.Clone();
                        btnNew.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                        btnNew.FlatAppearance.BorderSize = 2;
                        btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                        btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
                        btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                        btnNew.Size = new System.Drawing.Size(bit.Width + 2, bit.Height + 2);
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
            tileSelected = true;
            PcSelected = false;
            CrtSelected = false;
            currentTileFilename = selectBtn.Text;
            selectedBitmap = (Bitmap)selectBtn.BackgroundImage.Clone();
            panel1.BackgroundImage = selectedBitmap;
        }
        private void refreshMap()
        {
            device.Clear(Color.Gainsboro);            
            try
            {
                Encounter e = prntForm.encountersList[_selectedLbxEncounterIndex];
                //draw map
                #region new draw routine
                if ((e.UseMapImage) && (e.MapImage != "none"))
                {
                    //draw single image type map
                    if (mapBitmap == null)
                    {
                        if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + e.MapImage + ".jpg"))
                        {
                            mapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + e.MapImage + ".jpg");
                        }
                        else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + e.MapImage))
                        {
                            mapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + e.MapImage);
                        }
                        else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + e.MapImage + ".png"))
                        {
                            mapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + e.MapImage + ".png");
                        }
                        else
                        {
                            MessageBox.Show("Couldn't find map image " + e.MapImage + " in graphics folder, setting UseMapImage to false.");
                            e.UseMapImage = false;
                            return;
                        }                        
                    }
                    Rectangle src = new Rectangle(0, 0, mapBitmap.Width, mapBitmap.Height);
                    Rectangle dst = new Rectangle(0, 0, mSizeW, mSizeH);
                    device.DrawImage(mapBitmap, dst, src, GraphicsUnit.Pixel);
                    //Draw Grid stuff
                    for (int x = 0; x < 7; x++)
                    {
                        for (int y = 0; y < 7; y++)
                        {
                            TileEnc tile = prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[y * 7 + x];
                            src = new Rectangle(0, 0, sqr, sqr);
                            int dx = x * sqr;
                            int dy = y * sqr;
                            Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                            //draw square walkmesh and LoS stuff
                            if (tile.LoSBlocked)
                            {
                                device.DrawImage(g_LoSBlock, target, src, GraphicsUnit.Pixel);
                            }
                            if (tile.Walkable)
                            {
                                device.DrawImage(g_walkPass, target, src, GraphicsUnit.Pixel);
                            }
                            else
                            {
                                device.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                            }
                            device.DrawRectangle(blackPen, target);
                        }
                    }
                }
                else
                {
                    for (int x = 0; x < 7; x++)
                    {
                        for (int y = 0; y < 7; y++)
                        {
                            TileEnc tile = prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[y * 7 + x];
                            Bitmap lyr1 = null;
                            Bitmap lyr2 = null;
                            if (getTileByName(tile.Layer1Filename) != null)
                            {
                                lyr1 = getTileByName(tile.Layer1Filename).bitmap;
                            }
                            if (getTileByName(tile.Layer2Filename) != null)
                            {
                                lyr2 = getTileByName(tile.Layer2Filename).bitmap;
                            }
                            Rectangle src = new Rectangle(0, 0, sqr, sqr);
                            int dx = x * sqr;
                            int dy = y * sqr;
                            Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                            //draw layer 1 first
                            if (checkBox1.Checked)
                            {
                                if (lyr1 != null)
                                {
                                    device.DrawImage(lyr1, target, src, GraphicsUnit.Pixel);
                                }
                            }
                            //draw layer 2
                            if (checkBox2.Checked)
                            {
                                if (lyr2 != null)
                                {
                                    device.DrawImage(lyr2, target, src, GraphicsUnit.Pixel);
                                }
                            }
                            //draw square walkmesh and LoS stuff
                            if (tile.LoSBlocked)
                            {
                                device.DrawImage(g_LoSBlock, target, src, GraphicsUnit.Pixel);
                            }
                            if (tile.Walkable)
                            {
                                device.DrawImage(g_walkPass, target, src, GraphicsUnit.Pixel);
                            }
                            else
                            {
                                device.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                            }
                            device.DrawRectangle(blackPen, target);
                        }
                    }
                }
                #endregion                
                //draw creatures
                foreach (CreatureRefs crtRef in prntForm.encountersList[_selectedLbxEncounterIndex].encounterCreatureRefsList)
                {
                    int cspx = crtRef.creatureStartLocationX * sqr;
                    int cspy = crtRef.creatureStartLocationY * sqr;
                    device.DrawString(crtRef.creatureTag, drawFont, drawBrush, new Point(cspx, cspy+25));
                }
                //draw PCs
                int cnt = 0;
                foreach (Coordinate PCpoint in prntForm.encountersList[_selectedLbxEncounterIndex].encounterPcStartLocations)
                {
                    int cspx = PCpoint.X * sqr;
                    int cspy = PCpoint.Y * sqr;
                    device.DrawString((cnt + 1).ToString(), drawFontNum, drawBrush, new Point(cspx, cspy+5));
                    cnt++;
                }
                UpdatePB();
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed on refresh map: " + ex.ToString());                
            }
        }
        public void UpdatePB()
        {
            panelView.BackgroundImage = surface;
            panelView.Invalidate();
        }        
        private void panelView_MouseMove(object sender, MouseEventArgs e)
        {            
            gridX = e.X / sqr;
            gridY = e.Y / sqr;
            toolStripStatusLabel1.Text = "gridX = " + gridX.ToString() + " : gridY = " + gridY.ToString();
            /*if (currentPoint != new Point(gridX, gridY))
            {
                lastPoint = currentPoint;
                currentPoint = new Point(gridX, gridY);
                renderSelected(gridX, gridY);
            }*/
        }
        private void panelView_MouseDown(object sender, MouseEventArgs e)
        {            
            int col = e.X / sqr;
            int row = e.Y / sqr;

            if (tileSelected)
            {
                prntForm.encountersList[_selectedLbxEncounterIndex].encounterMapLayout[row, col] = currentTile;
                if (radioButton1.Checked)
                {
                    prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[row * 7 + col].Layer1Filename = currentTileFilename;
                }
                else if (radioButton2.Checked)
                {
                    prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[row * 7 + col].Layer2Filename = currentTileFilename;
                }
            }
            else if (PcSelected)
            {
                if (prntForm.encountersList[_selectedLbxEncounterIndex].encounterPcStartLocations.Count < 6)
                {
                    prntForm.encountersList[_selectedLbxEncounterIndex].encounterPcStartLocations.Add(new Coordinate(col, row));
                }
            }
            else if (CrtSelected)
            {
                CreatureRefs crtRef = new CreatureRefs();
                crtRef.creatureResRef = prntForm.creaturesList[_selectedLbxCreatureIndex].cr_resref; 
                crtRef.creatureTag = prntForm.creaturesList[_selectedLbxCreatureIndex].cr_tag + "_" + prntForm.mod.nextIdNumber;
                crtRef.creatureStartLocationX = col;
                crtRef.creatureStartLocationY = row;
                prntForm.encountersList[_selectedLbxEncounterIndex].encounterCreatureRefsList.Add(crtRef);
            }
            else if (rbtnInfo.Checked)
            {
                foreach (CreatureRefs crt in prntForm.encountersList[_selectedLbxEncounterIndex].encounterCreatureRefsList)
                {
                    if ((crt.creatureStartLocationX == col) && (crt.creatureStartLocationY == row))
                    {
                        //lastSelectedObjectResRef = crt.CreatureResRef;
                        this.propertyGrid1.SelectedObject = crt;
                    }
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
        }
        private void btnFillWithSelected_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    if (radioButton1.Checked)
                    {
                        prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[y * 7 + x].Layer1Filename = currentTileFilename;
                    }
                    else if (radioButton2.Checked)
                    {
                        prntForm.encountersList[_selectedLbxEncounterIndex].encounterTiles[y * 7 + x].Layer2Filename = currentTileFilename;
                    }                    
                }
            }
            refreshMap();
        }
        private void rbtnGrass_CheckedChanged(object sender, EventArgs e)
        {
            tileSelected = true;
            currentTile = 'g';
        }
        private void rbtnMud_CheckedChanged(object sender, EventArgs e)
        {
            tileSelected = true;
            currentTile = 'm';
        }
        private void rbtnBlack_CheckedChanged(object sender, EventArgs e)
        {
            tileSelected = true;
            currentTile = 'b';
        }
        private void rbtnTree_CheckedChanged(object sender, EventArgs e)
        {
            tileSelected = true;
            currentTile = 't';
        }
        private void rbtnFloor_CheckedChanged(object sender, EventArgs e)
        {
            tileSelected = true;
            currentTile = 's';
        }
        private void rbtnWall_CheckedChanged(object sender, EventArgs e)
        {
            tileSelected = true;
            currentTile = 'w';
        }
        private void rbtnRock_CheckedChanged(object sender, EventArgs e)
        {
            tileSelected = true;
            currentTile = 'r';
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveEncountersFile();
        }
        private void LoadEncounters()
        {
            refreshListBoxEncounters();
            refreshListBoxCreatures();
            refreshLbxItems();
            refreshCmbItems();
            refreshGoldDrop();
            _selectedLbxCreatureIndex = 0;
            _selectedLbxEncounterIndex = 0;
            refreshMap();
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadEncounters();
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            refreshListBoxEncounters();
        }

        #region Encounter Stuff
        public void refreshCmbItems()
        {
            cmbItems.BeginUpdate();
            cmbItems.DataSource = null;
            cmbItems.DataSource =  prntForm.itemsList;
            cmbItems.DisplayMember = "name";
            cmbItems.EndUpdate();
        }
        private void refreshLbxItems()
        {
            lbxItems.BeginUpdate();
            lbxItems.DataSource = null;
            lbxItems.DataSource = prntForm.encountersList[_selectedLbxEncounterIndex].encounterInventoryRefsList;
            lbxItems.DisplayMember = "name";
            lbxItems.EndUpdate();
        }
        public void refreshGoldDrop()
        {
            numGold.Value = (int)prntForm.encountersList[_selectedLbxEncounterIndex].goldDrop;
        }
        public void refreshListBoxEncounters()
        {
            lbxEncounters.BeginUpdate();
            lbxEncounters.DataSource = null;
            lbxEncounters.DataSource = prntForm.encountersList;
            lbxEncounters.DisplayMember = "encounterName";
            lbxEncounters.EndUpdate();
        }
        public void refreshListBoxCreatures()
        {
            lbxCreatures.BeginUpdate();
            lbxCreatures.DataSource = null;
            lbxCreatures.DataSource = prntForm.creaturesList;
            lbxCreatures.DisplayMember = "cr_name";
            lbxCreatures.EndUpdate();
        }
        private void numGold_ValueChanged(object sender, EventArgs e)
        {
            prntForm.encountersList[_selectedLbxEncounterIndex].goldDrop = (int)numGold.Value;
        }        
        private void txtEncounterName_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                //prntForm.encountersList.encounters[prntForm._selectedLbxEncounterIndex].EncounterName = txtEncounterName.Text;
                refreshListBoxEncounters();
            }
            catch { }
        }
        private void btnAddEncounter_Click_1(object sender, EventArgs e)
        {
            Encounter newEncounter = new Encounter();
            newEncounter.SetAllToGrass();
            newEncounter.encounterName = "new encounter";
            prntForm.encountersList.Add(newEncounter);
            refreshListBoxEncounters();
            // should I create a new file at this point?
        }
        private void btnRemoveEncounter_Click_1(object sender, EventArgs e)
        {
            if ((lbxEncounters.Items.Count > 0) && (lbxEncounters.SelectedIndex >= 0))
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxEncounters.SelectedIndex;
                    prntForm.encountersList.RemoveAt(selectedIndex);
                }
                catch { }
                _selectedLbxEncounterIndex = 0;
                lbxEncounters.SelectedIndex = 0;
                refreshListBoxEncounters();
            }
        }
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            /*if ((lbxEncounters.Items.Count > 0) && (lbxEncounters.SelectedIndex >= 0))
            {
                try
                {
                    Encounter newEncounter = new Encounter();
                    newEncounter = prntForm.encountersList.encounters[prntForm._selectedLbxEncounterIndex].DeepCopy();
                    newEncounter.EncounterName = prntForm.encountersList.encounters[prntForm._selectedLbxEncounterIndex].EncounterName + "-Copy";
                    newEncounter.passRefs(prntForm.game, prntForm);
                    prntForm.encountersList.Add(newEncounter);
                    refreshListBoxEncounters();
                }
                catch { }
            }*/
        }
        private void lbxEncounters_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lbxEncounters.SelectedIndex >= 0)
            {
                _selectedLbxEncounterIndex = lbxEncounters.SelectedIndex;
                lbxEncounters.SelectedIndex = _selectedLbxEncounterIndex;
                refreshMap();
                refreshLbxItems();
                refreshGoldDrop();
                propertyGrid1.SelectedObject = prntForm.encountersList[_selectedLbxEncounterIndex];
            }
        }
        private void lbxCreatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxCreatures.SelectedIndex >= 0)
            {
                tileSelected = false;
                PcSelected = false;
                CrtSelected = true;
                _selectedLbxCreatureIndex = lbxCreatures.SelectedIndex;
                lbxCreatures.SelectedIndex = _selectedLbxCreatureIndex;
                refreshMap();                
            }
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
        public List<Creature> loadCreaturesFile()
        {
            List<Creature> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(prntForm._mainDirectory + "\\data\\creatures.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Creature>)serializer.Deserialize(file, typeof(List<Creature>));
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
        #endregion

        private void btnAddItems_Click(object sender, EventArgs e)
        {
            try
            {
                Item it = prntForm.itemsList[cmbItems.SelectedIndex];
                ItemRefs newIR = prntForm.createItemRefsFromItem(it);
                prntForm.encountersList[_selectedLbxEncounterIndex].encounterInventoryRefsList.Add(newIR);
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
                        prntForm.encountersList[_selectedLbxEncounterIndex].encounterInventoryRefsList.RemoveAt(lbxItems.SelectedIndex);
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
            prntForm.encountersList[_selectedLbxEncounterIndex].encounterPcStartLocations.Clear();
            refreshMap();
        }
        private void btnDeleteCreatures_Click(object sender, EventArgs e)
        {
            prntForm.encountersList[_selectedLbxEncounterIndex].encounterCreatureRefsList.Clear();
            refreshMap();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            refreshMap();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            refreshMap();
        }

        private void rbtnInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnInfo.Checked)
            {
                tileSelected = false;
                PcSelected = false;
                CrtSelected = false;
                refreshMap();
            }
        }

        private void rbtnWalkable_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnWalkable.Checked)
            {
                tileSelected = false;
                PcSelected = false;
                CrtSelected = false;
                refreshMap();
            }
        }

        private void rbtnLoS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnLoS.Checked)
            {
                tileSelected = false;
                PcSelected = false;
                CrtSelected = false;
                refreshMap();
            }
        }

        
    }

    public class TileBitmapNamePair
    {
        public Bitmap bitmap = null;
        public string filename = "";

        public TileBitmapNamePair()
        {
        }
        public TileBitmapNamePair(Bitmap bm, string fname)
        {
            bitmap = bm;
            filename = fname;
        }
    }
}
