using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using IceBlinkCore;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace IBBToolset
{
    public partial class EncounterEditor : DockContent
    {        
        public ParentForm prntForm;
        public const int mapSize = 16;
        public const int tileSize = 64;
        public Module ee_mod = new Module();
        //public Game ee_game;
        public Creature ee_selectedCreature = new Creature();
        public Prop ee_selectedProp = new Prop();
        public Encounter ee_encounter = new Encounter();
        public Bitmap drawArea;
        public Bitmap gameMapBitmap;
        public Bitmap selectedBitmap;
        public int selectedBitmapSize = 1;
        public List<Bitmap> crtBitmapList = new List<Bitmap>(); //index will match EncounterCreatureList index
        public List<Bitmap> propBitmapList = new List<Bitmap>(); //index will match EncounterPropList index
        //public List<Bitmap> crtRefBitmapList = new List<Bitmap>(); //index will match ee_encounter.encounters index
        public List<Bitmap> PCBitmapList = new List<Bitmap>();
        public int mousex, mousey;
        public Graphics gfx;
        public int gridx, gridy;
        public Font fontArial;
        public string ee_filename = "";
        public string ee_directory = "";
        public Area area;
        public bool selected = false;
        public bool propSelected = false;
        public bool PCselected = false;
        public Point lastSelectedCreatureIcon;
        public string lastSelectedObjectTag;
        public string lastSelectedObjectResRef;
        public bool showGrid = true;
        public Bitmap g_walkPass;
        public Bitmap g_walkBlock;
        public Bitmap g_LoSBlock;

        public EncounterEditor(Module mod, Encounter encounter, ParentForm p)
        {
            InitializeComponent();
            ee_mod = mod;
            ee_encounter = encounter;            
            //ee_game = g;
            prntForm = p;
            //ee_encounter.passRefs(g, p);
        }
        private void EncounterEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            gfx.Dispose();
            drawArea.Dispose();
            //gfxSelected.Dispose();
            //selectedBitmap.Dispose();
            //this.Close();
        }
        private void EncounterEditor_Load(object sender, EventArgs e)
        {
            //prntForm = (ParentForm)this.ParentForm;
            try
            {
                g_walkPass = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\walkPass.png");
                g_walkBlock = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\walkBlock.png");
                g_LoSBlock = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\LoSBlock.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to load walkPass and walkBlock bitmaps: " + ex.ToString());
                ee_game.errorLog("failed to load walkPass and walkBlock bitmaps: " + ex.ToString());
            }
            area = new Area();
            area.MapSizeInSquares.Width = 16;
            area.MapSizeInSquares.Height = 16;
            //set up level drawing surface
            drawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = drawArea;
            gfx = Graphics.FromImage(drawArea);

            selectedBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\blank.png");

            createNewArea(area.MapSizeInSquares.Width, area.MapSizeInSquares.Height);

            for (int i = 1; i <= 6; i++)
            {
                Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc" + i.ToString() + ".png");
                PCBitmapList.Add(newBitmap);
            }

            cmbItems.DataSource = null;
            cmbItems.DataSource = prntForm.itemsList.itemsList;
            cmbItems.DisplayMember = "ItemName";
            refreshLbxItems();

            loadFileSelectedToEdit();

            //refreshPanel();
        }
        private void loadEncounterTriggerPassRefs()
        {
            foreach (Trigger t in area.AreaTriggerList.triggersList)
            {
                t.passRefs(ee_game, prntForm, area);
            }
        }
        private void convertObjectsToRefs()
        {
            //need to find resref of crt and get the tag
            foreach (Creature crt in ee_encounter.EncounterCreatureList.creatures)
            {
                Creature test = ee_encounter.EncounterCreatureList.getCreatureByResRef(crt.ResRef);
                if (test != null)
                {
                    CreatureRefs newCrtRef = new CreatureRefs();
                    newCrtRef.CreatureName = crt.Name;
                    newCrtRef.CreatureTag = crt.Tag;
                    newCrtRef.CreatureResRef = crt.ResRef;
                    newCrtRef.CreatureSize = crt.Size;
                    newCrtRef.CreatureStartLocation = new Point(crt.CombatLocation.X, crt.CombatLocation.Y);
                    newCrtRef.MouseOverText = crt.MouseOverText;
                    ee_encounter.EncounterCreatureRefsList.Add(newCrtRef);
                }
                else
                {
                    MessageBox.Show("Didn't find ResRef: " + crt.ResRef + " ...Failed to automatically convert object to a reference");
                }
            }
            ee_encounter.EncounterCreatureList.creatures.Clear();
        }
        private void loadEncounterObjectBitmapLists()
        {
            convertObjectsToRefs();
            foreach (CreatureRefs crtRef in ee_encounter.EncounterCreatureRefsList)
            {
                // get Creature by Tag and then get Icon filename, add Bitmap to list
                Creature newCrt = prntForm.creaturesList.getCreatureByResRef(crtRef.CreatureResRef);
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName + "\\graphics\\sprites\\tokens\\module\\" + newCrt.CharSprite.SpriteSheetFilename))
                {
                    Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName + "\\graphics\\sprites\\tokens\\module\\" + newCrt.CharSprite.SpriteSheetFilename);
                    crtBitmapList.Add(newBitmap);
                }
                else if (File.Exists(prntForm._mainDirectory + "\\data\\graphics\\sprites\\tokens\\" + newCrt.CharSprite.SpriteSheetFilename))
                {
                    Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\sprites\\tokens\\" + newCrt.CharSprite.SpriteSheetFilename);
                    crtBitmapList.Add(newBitmap);
                }
                else
                {
                    MessageBox.Show("Failed to find creature sprite (" + newCrt.CharSprite.SpriteSheetFilename + ") in data or module folders");
                }
            }
            foreach (PropRefs prpRef in ee_encounter.EncounterPropRefsList)
            {
                // get Creature by Tag and then get Icon filename, add Bitmap to list
                Prop newPrp = prntForm.propsList.getPropByResRef(prpRef.PropResRef);
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName + "\\graphics\\sprites\\props\\" + newPrp.PropSprite.SpriteSheetFilename))
                {
                    Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName + "\\graphics\\sprites\\props\\" + newPrp.PropSprite.SpriteSheetFilename);
                    propBitmapList.Add(newBitmap);
                }
                else if (File.Exists(prntForm._mainDirectory + "\\data\\graphics\\sprites\\props\\" + newPrp.PropSprite.SpriteSheetFilename))
                {
                    Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\sprites\\props\\" + newPrp.PropSprite.SpriteSheetFilename);
                    propBitmapList.Add(newBitmap);
                }
                else
                {
                    MessageBox.Show("Failed to find prop sprite (" + newPrp.PropSprite.SpriteSheetFilename + ") in data or module folders");
                }
            }
        }
        private void createNewArea(int width, int height)
        {
            area = null;
            area = new Area();
            area.MapSizeInSquares.Width = width;
            area.MapSizeInSquares.Height = height;
            area.MapSizeInPixels.Width = width * tileSize;
            area.MapSizeInPixels.Height = height * tileSize;
            for (int index = 0; index < (width * height); index++)
            {
                Tile newTile = new Tile();
                //newTile.tilenum = 0;
                newTile.collidable = false;
                area.TileMapList.Add(newTile);
            }
        }
        private void loadFileSelectedToEdit()
        {
            // try and load the file selected if it exists
            string g_filename = prntForm.encountersList.encounters[prntForm._selectedLbxEncounterIndex].EncounterLevelFilename;
            string g_directory = prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName + "\\areas";
            string filenameNoExtension = Path.GetFileNameWithoutExtension(g_filename);
            if (File.Exists(g_directory + "\\" + g_filename + ".level"))
            {
                openLevel(g_directory, g_filename, filenameNoExtension);
                if (area == null)
                {
                    createNewArea(area.MapSizeInSquares.Width, area.MapSizeInSquares.Height);
                }
            }
            else
            {
                createNewArea(area.MapSizeInSquares.Width, area.MapSizeInSquares.Height);
            }
        }
        private void openLevel(string g_dir, string g_fil, string g_filNoEx)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                area = area.loadAreaFile(g_dir + "\\" + g_fil + ".level");
                if (area == null)
                {
                    MessageBox.Show("returned a null area");
                    return;
                }
                loadEncounterObjectBitmapLists();
                loadEncounterTriggerPassRefs();
                //MessageBox.Show("open file success");
                // load Map next
                //ee_encounter.encounterPngMapName = filenameNoExtension + ".png";
                gameMapBitmap = new Bitmap(g_dir + "\\" + ee_encounter.EncounterMapFilename);
                drawArea = new Bitmap(g_dir + "\\" + ee_encounter.EncounterMapFilename);
                pictureBox1.Width = gameMapBitmap.Size.Width;
                pictureBox1.Height = gameMapBitmap.Size.Height;
                pictureBox1.Image = (Image)drawArea;
                gfx = Graphics.FromImage(drawArea);
                if (drawArea == null)
                {
                    MessageBox.Show("returned a null Map bitmap");
                }
                refreshMap();
                //redrawTilemap();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
                MessageBox.Show("failed to open file");
            }          

            this.Cursor = Cursors.Arrow;
        }
        /*private void btnLoadMap_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = Environment.CurrentDirectory;
            //Empty the FileName text box of the dialog
            openFileDialog2.FileName = String.Empty;
            openFileDialog2.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            openFileDialog2.FilterIndex = 1;

            DialogResult result = openFileDialog2.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string filename = Path.GetFullPath(openFileDialog2.FileName);
                string filenameOnly = openFileDialog2.SafeFileName;
                ee_encounter.EncounterMapName = filenameOnly;
                gameMapBitmap = new Bitmap(filename);
                drawArea = new Bitmap(filename);
                pictureBox1.Width = gameMapBitmap.Size.Width;
                pictureBox1.Height = gameMapBitmap.Size.Height;
                pictureBox1.Image = (Image)drawArea;
                gfx = Graphics.FromImage(drawArea);
                if (drawArea == null)
                {
                    MessageBox.Show("returned a null bitmap");
                }
                createNewArea(gameMapBitmap.Size.Width / tileSize, gameMapBitmap.Size.Height / tileSize);
            }
        }*/
        private void btnLoadEncounter_Click(object sender, EventArgs e)
        {
            //display the open file dialog
            openFileDialog1.DefaultExt = ".level";
            openFileDialog1.Filter = "Tilemap Files|*.level";
            openFileDialog1.Multiselect = false;
            openFileDialog1.Title = "Load Level File";
            openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName + "\\areas";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result != DialogResult.OK) return;
            //ee_directory = Path.GetDirectoryName(openFileDialog1.FileName);
            ee_directory = prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName + "\\areas";
            string filenameNoExtension = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
            ee_filename = openFileDialog1.SafeFileName;            
            this.Cursor = Cursors.WaitCursor;            

            try
            {
                area = area.loadAreaFile(ee_directory + "\\" + filenameNoExtension + ".level");
                if (area == null)
                {
                    MessageBox.Show("returned a null area");
                }                
                // load Map
                ee_encounter.EncounterMapFilename = area.MapFileName;
                ee_encounter.EncounterLevelFilename = area.AreaFileName;
                gameMapBitmap = new Bitmap(ee_directory + "\\" + area.MapFileName);
                drawArea = new Bitmap(ee_directory + "\\" + area.MapFileName);
                pictureBox1.Width = gameMapBitmap.Size.Width;
                pictureBox1.Height = gameMapBitmap.Size.Height;
                pictureBox1.Image = (Image)drawArea;
                gfx = Graphics.FromImage(drawArea);
                if (drawArea == null)
                {
                    MessageBox.Show("returned a null Map bitmap");
                }
                //MessageBox.Show("open file success");
                refreshMap();
                //redrawTilemap();
            }
            catch (Exception es)
            {
                MessageBox.Show(es.Message);
                MessageBox.Show("failed to open Level file");
            }

            this.Cursor = Cursors.Arrow;
        }
        /*private void btnSaveEncounter_Click(object sender, EventArgs e)
        {
            //display the open file dialog
            saveFileDialog1.DefaultExt = ".level";
            saveFileDialog1.Filter = "Tilemap Files|*.level";
            saveFileDialog1.Title = "Save Level File";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result != DialogResult.OK) return;
            if (saveFileDialog1.FileName.Length == 0) return;
            ee_filename = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
            ee_directory = Path.GetDirectoryName(saveFileDialog1.FileName);
            area.AreaFileName = ee_filename;
            area.MapFileName = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName) + ".jpg";
            area.saveAreaFile(ee_directory + "\\" + ee_filename + ".level");
        }*/
        private void btnPlacePC_Click(object sender, EventArgs e)
        {
            selected = false;
            PCselected = true;
        }
        private void btnDeleteAllPCStartPoints_Click(object sender, EventArgs e)
        {
            ee_encounter.EncounterPcStartLocations.Clear();
            PCBitmapList.Clear();
            refreshPcIcon();
            refreshMap();
        }
        /*private void refreshPanel()
        {
            //retreatX.Value = ee_encounter.encounterPcRetreatLocation.X;
            //retreatY.Value = ee_encounter.encounterPcRetreatLocation.Y;
        }*/
        private void refreshMap()
        {
            try
            {
                if (gameMapBitmap != null)
                {
                    gfx.DrawImage((Image)gameMapBitmap, 0, 0);
                }
                int cnt = 0;
                foreach (CreatureRefs crtRef in ee_encounter.EncounterCreatureRefsList)
                {
                    int cspx = crtRef.CreatureStartLocation.X;
                    int cspy = crtRef.CreatureStartLocation.Y;
                    spriteCreatureDraw(cspx, cspy, cnt, crtRef.CreatureSize);
                    cnt++;
                }
                cnt = 0;
                foreach (PropRefs prpRef in ee_encounter.EncounterPropRefsList)
                {
                    int cspx = prpRef.PropStartLocation.X;
                    int cspy = prpRef.PropStartLocation.Y;
                    spritePropDraw(cspx, cspy, cnt);
                    cnt++;
                }
                /*cnt = 0;
                foreach (Creature crt in ee_encounter.EncounterCreatureList.creatures)
                {
                    int cspx = crt.CombatLocation.X;
                    int cspy = crt.CombatLocation.Y;
                    spriteCreatureDraw(cspx, cspy, cnt);
                    cnt++;
                }*/
                cnt = 0;
                foreach (Point PCpoint in ee_encounter.EncounterPcStartLocations)
                {
                    spritePcCombatDraw(PCpoint.X, PCpoint.Y, cnt);
                    cnt++;
                }
                if (area != null)
                {
                    drawTileSettings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed on refresh map: " + ex.ToString());
                ee_game.errorLog("failed on refresh map: " + ex.ToString());
            }
        }
        private void refreshLbxItems()
        {
            lbxItems.BeginUpdate();
            lbxItems.DataSource = null;
            lbxItems.DataSource = ee_encounter.EncounterInventoryTagList;
            lbxItems.EndUpdate();
        }
        private void spriteCreatureDraw(int cspx, int cspy, int spriteListIndex, int size)
        {
            //source image
            Rectangle source = new Rectangle(0, 0, tileSize * size, tileSize * size);
            //target location
            Rectangle target = new Rectangle(cspx * tileSize, cspy * tileSize, tileSize * size, tileSize * size);
            //draw sprite
            gfx.DrawImage((Image)crtBitmapList[spriteListIndex], target, source, GraphicsUnit.Pixel);
        }
        private void spritePropDraw(int cspx, int cspy, int spriteListIndex)
        {
            //source image
            Rectangle source = new Rectangle(0, 0, ee_selectedProp.PropSprite.SpriteSize.Width, ee_selectedProp.PropSprite.SpriteSize.Height);
            //target location
            Rectangle target = new Rectangle(cspx * tileSize, cspy * tileSize, ee_selectedProp.PropSprite.SpriteSize.Width, ee_selectedProp.PropSprite.SpriteSize.Height);
            //draw sprite
            gfx.DrawImage((Image)propBitmapList[spriteListIndex], target, source, GraphicsUnit.Pixel);
        }
        /*private void spriteCombatDraw(int cspx, int cspy, int spriteListIndex)
        {
            //source image
            Rectangle source = new Rectangle(0, 0, tileSize, tileSize);
            //target location
            Rectangle target = new Rectangle(cspx * tileSize, cspy * tileSize, tileSize, tileSize);
            //draw sprite
            gfx.DrawImage((Image)crtBitmapList[spriteListIndex], target, source, GraphicsUnit.Pixel);
        }*/
        private void spritePcCombatDraw(int cspx, int cspy, int spriteListIndex)
        {
            //source image
            Rectangle source = new Rectangle(0, 0, tileSize, tileSize);
            //target location
            Rectangle target = new Rectangle(cspx * tileSize, cspy * tileSize, tileSize, tileSize);
            //draw sprite
            gfx.DrawImage((Image)PCBitmapList[spriteListIndex], target, source, GraphicsUnit.Pixel);
        }
        private void drawTileSettings()
        {
            //for (int index = 0; index < area.MapSizeInSquares.Width * mapSize; index++)
            for (int x = 0; x < area.MapSizeInSquares.Width; x++)
            {
                for (int y = 0; y < area.MapSizeInSquares.Height; y++)
                {
                    if (showGrid) //if show grid is turned on, draw grid squares
                    {
                        if (area.TileMapList[y * this.area.MapSizeInSquares.Width + x].LoSBlocked)
                        {
                            Rectangle src = new Rectangle(0, 0, tileSize, tileSize);
                            int dx = x * tileSize;
                            int dy = y * tileSize;
                            Rectangle target = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                            gfx.DrawImage(g_LoSBlock, target, src, GraphicsUnit.Pixel);
                        }
                        if (area.TileMapList[y * this.area.MapSizeInSquares.Width + x].collidable)
                        {
                            Rectangle src = new Rectangle(0, 0, tileSize, tileSize);
                            int dx = x * tileSize;
                            int dy = y * tileSize;
                            Rectangle target = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                            gfx.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                        }
                        else
                        {
                            Rectangle src = new Rectangle(0, 0, tileSize, tileSize);
                            int dx = x * tileSize;
                            int dy = y * tileSize;
                            Rectangle target = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                            gfx.DrawImage(g_walkPass, target, src, GraphicsUnit.Pixel);
                        }
                    }
                }
            }
            /*foreach (Trigger t in ee_encounter.EncounterTriggerList.triggersList)
            {
                foreach (Point p in t.TriggerSquaresList)
                {
                    int dx = p.X * tileSize;
                    int dy = p.Y * tileSize;
                    Pen pen = new Pen(t.TriggerColor, 2);
                    Rectangle rect = new Rectangle(dx + 3, dy + 3, tileSize - 6, tileSize - 6);
                    gfx.DrawRectangle(pen, rect);
                }
            }*/
            foreach (Trigger t in area.AreaTriggerList.triggersList)
            {
                foreach (Point p in t.TriggerSquaresList)
                {
                    int dx = p.X * tileSize;
                    int dy = p.Y * tileSize;
                    Pen pen = new Pen(t.TriggerColor, 2);
                    Rectangle rect = new Rectangle(dx + 3, dy + 3, tileSize - 6, tileSize - 6);
                    gfx.DrawRectangle(pen, rect);
                }
            }
            //save changes
            pictureBox1.Image = drawArea;
        }
        private void drawSelectionBox(int gridx, int gridy)
        {
            //hideSelectionBox();
            refreshMap();
            //remember current tile
            //selectedTile.oldIndex = selectedTile.index;

            //draw selection box around tile
            int dx = gridx * tileSize;
            int dy = gridy * tileSize;
            Pen pen = new Pen(Color.DarkMagenta, 2);
            Rectangle rect = new Rectangle(dx + 1, dy + 1, tileSize - 2, tileSize - 2);
            gfx.DrawRectangle(pen, rect);

            //save changes
            pictureBox1.Image = drawArea;
        }
        protected override System.Drawing.Point ScrollToControl(Control activeControl)
        {
            return DisplayRectangle.Location;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {            
            gridx = e.X / tileSize;
            gridy = e.Y / tileSize;
            mousex = e.X;
            mousey = e.Y;
            lblMouseInfo.Text = "CURSOR " + e.X.ToString() + "," + e.Y.ToString() + " - GRID " + gridx.ToString() + "," + gridy.ToString();
            if ((selected) || (PCselected) || (propSelected))
            {
                refreshMap();
                try
                {
                    if (selectedBitmap != null)
                    {
                        //source image size
                        Rectangle frame = new Rectangle(0, 0, tileSize * selectedBitmapSize, tileSize * selectedBitmapSize);
                        //target location
                        Rectangle target = new Rectangle(gridx * tileSize, gridy * tileSize, tileSize * selectedBitmapSize, tileSize * selectedBitmapSize);
                        //draw sprite
                        gfx.DrawImage((Image)selectedBitmap, target, frame, GraphicsUnit.Pixel);
                    }
                }
                catch (Exception ex) { MessageBox.Show("failed mouse move: " + ex.ToString()); }
                //save changes
                pictureBox1.Image = drawArea;
            }
        }
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                refreshMap();
                pictureBox1.Image = drawArea;
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed on mouse leave map: " + ex.ToString());
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                #region Left Button
                case MouseButtons.Left:
                    #region Creature Selected
                    if ((selected) && (ee_selectedCreature != null))
                    {
                        string selectedCrt = prntForm.selectedEncounterCreatureTag;
                        prntForm.logText(selectedCrt);
                        prntForm.logText(Environment.NewLine);

                        gridx = e.X / tileSize;
                        gridy = e.Y / tileSize;

                        prntForm.logText("gridx = " + gridx.ToString() + "gridy = " + gridy.ToString());
                        prntForm.logText(Environment.NewLine);
                        // verify that there is no creature, blocked, or PC already on this location
                        // add to a List<> a new item with the x,y coordinates
                        if (ee_selectedCreature.SpriteFilename == "blank.spt")
                        {
                            return;
                        }
                        //check to see if a creature already exists on this square
                        foreach (CreatureRefs cr in ee_encounter.EncounterCreatureRefsList)
                        {
                            if (cr.CreatureStartLocation == new Point(gridx, gridy))
                            {
                                MessageBox.Show("Can only place one creature per square...aborting placement");
                                return;
                            }
                        }
                        CreatureRefs newCrtRef = new CreatureRefs();
                        newCrtRef.CreatureName = ee_selectedCreature.Name;
                        newCrtRef.CreatureTag = ee_selectedCreature.Tag + "_" + prntForm.mod.NextIdNumber;
                        newCrtRef.CreatureResRef = ee_selectedCreature.ResRef;
                        newCrtRef.CreatureSize = ee_selectedCreature.Size;
                        newCrtRef.MouseOverText = ee_selectedCreature.MouseOverText;
                        newCrtRef.CreatureStartLocation = new Point(gridx, gridy);
                        ee_encounter.EncounterCreatureRefsList.Add(newCrtRef);
                        //Creature newCrt = new Creature();
                        //newCrt = ee_selectedCreature.DeepCopy();
                        //newCrt.Tag = ee_selectedCreature.Tag + "_" + prntForm.mod.NextIdNumber;
                        //newCrt.CombatLocation = new Point(gridx, gridy);
                        //ee_encounter.EncounterCreatureList.creatures.Add(newCrt);
                        //area.AreaCreatureList.creatures.Add(newCrt);                      
                        // show the item on the map
                        if (ee_mod.ModuleName != "NewModule")
                        {
                            Creature crt = ee_selectedCreature.DeepCopy();
                            crt.LoadSpriteStuff(prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName);
                            Bitmap newBitmap = (Bitmap)crt.CharSprite.Image.Clone();
                            //Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName + "\\graphics\\sprites\\tokens\\module\\" + ee_selectedCreature.CharSprite.SpriteSheetFilename);
                            crtBitmapList.Add(newBitmap);
                        }
                        else
                        {
                            Creature crt = ee_selectedCreature.DeepCopy();
                            crt.LoadSpriteStuff(prntForm._mainDirectory + "\\data\\NewModule");
                            Bitmap newBitmap = (Bitmap)crt.CharSprite.Image.Clone(); 
                            //Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\NewModule\\graphics\\sprites\\tokens\\" + ee_selectedCreature.CharSprite.SpriteSheetFilename);
                            crtBitmapList.Add(newBitmap);
                        }
                    }
                    #endregion
                    #region Prop Selected
                    else if ((propSelected) && (ee_selectedProp != null))
                    {
                        string selectedProp = prntForm.selectedEncounterPropTag;
                        prntForm.logText(selectedProp);
                        prntForm.logText(Environment.NewLine);

                        gridx = e.X / tileSize;
                        gridy = e.Y / tileSize;

                        prntForm.logText("gridx = " + gridx.ToString() + "gridy = " + gridy.ToString());
                        prntForm.logText(Environment.NewLine);
                        // verify that there is no creature, blocked, or PC already on this location
                        // add to a List<> a new item with the x,y coordinates
                        if (ee_selectedProp.PropSpriteFilename == "blank.spt")
                        {
                            return;
                        }
                        PropRefs newPropRef = new PropRefs();
                        newPropRef.PropName = ee_selectedProp.PropName;
                        newPropRef.PropTag = ee_selectedProp.PropTag + "_" + prntForm.mod.NextIdNumber;
                        newPropRef.PropResRef = ee_selectedProp.PropResRef;
                        newPropRef.PropContainerChk = ee_selectedProp.PropContainerChk;
                        newPropRef.PropContainerTag = ee_selectedProp.PropContainerTag;
                        newPropRef.PropTrappedChk = ee_selectedProp.PropTrapped;
                        newPropRef.PropLockedChk = ee_selectedProp.PropLocked;
                        newPropRef.PropKeyTag = ee_selectedProp.PropKeyTag;
                        newPropRef.MouseOverText = ee_selectedProp.MouseOverText;
                        newPropRef.PropStartLocation = new Point(gridx, gridy);
                        ee_encounter.EncounterPropRefsList.Add(newPropRef);                        
                        //Prop newProp = new Prop();
                        //newProp = ee_selectedProp.DeepCopy();
                        //newProp.PropTag = ee_selectedProp.PropTag + "_" + prntForm.mod.NextIdNumber;
                        //newProp.Location = new Point(gridx, gridy);
                        //ee_encounter.EncounterPropList.propsList.Add(newProp);
                        // show the item on the map
                        if (ee_mod.ModuleName != "NewModule")
                        {
                            Prop prp = ee_selectedProp.DeepCopy();
                            prp.LoadPropSpriteStuffForTS(prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName);
                            Bitmap newBitmap = (Bitmap)prp.PropSprite.Image.Clone();
                            //Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + ee_mod.ModuleFolderName + "\\graphics\\sprites\\props\\" + ee_selectedProp.PropSprite.SpriteSheetFilename);
                            propBitmapList.Add(newBitmap);
                        }
                        else
                        {
                            Prop prp = ee_selectedProp.DeepCopy();
                            prp.LoadPropSpriteStuffForTS(prntForm._mainDirectory + "\\data\\NewModule");
                            Bitmap newBitmap = (Bitmap)prp.PropSprite.Image.Clone();
                            //Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\NewModule\\graphics\\sprites\\props\\" + ee_selectedProp.PropSprite.SpriteSheetFilename);
                            propBitmapList.Add(newBitmap);
                        }
                    }
                    #endregion
                    #region PC Selected
                    else if (PCselected)
                    {
                        // place PC location, check if valid location and not used yet
                        gridx = e.X / tileSize;
                        gridy = e.Y / tileSize;

                        prntForm.logText("gridx = " + gridx.ToString() + "gridy = " + gridy.ToString());
                        prntForm.logText(Environment.NewLine);
                        // verify that there is no creature, blocked, or PC already on this location
                        // add to a List<> a new item with the x,y coordinates                        
                        Point newPoint = new Point(gridx, gridy);
                        ee_encounter.EncounterPcStartLocations.Add(newPoint);
                        Bitmap newBitmap;
                        if (ee_encounter.EncounterPcStartLocations.Count >= 6)
                            newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc6.png");
                        else if (ee_encounter.EncounterPcStartLocations.Count >= 5)
                            newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc5.png");
                        else if (ee_encounter.EncounterPcStartLocations.Count >= 4)
                            newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc4.png");
                        else if (ee_encounter.EncounterPcStartLocations.Count >= 3)
                            newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc3.png");
                        else if (ee_encounter.EncounterPcStartLocations.Count >= 2)
                            newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc2.png");
                        else
                            newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc1.png");
                        PCBitmapList.Add(newBitmap);
                        refreshPcIcon();
                    }
                    #endregion
                    #region None Selected
                    else // not placing, just getting info and possibly deleteing icon
                    {
                        gridx = e.X / tileSize;
                        gridy = e.Y / tileSize;
                        Point newPoint = new Point(gridx, gridy);
                        //draw selection box
                        drawSelectionBox(gridx, gridy);
                        txtSelectedIconInfo.Text = "";
                        // if click on item (and not placing a new item), should be able to
                        // get info about the item and remove if wanted

                        // iterate through all icons to see if one is at the selected location
                        foreach (Creature crt in ee_encounter.EncounterCreatureList.creatures)
                        {
                            if (crt.CombatLocation == newPoint)
                            {
                                // if so then give details about that icon (name, tag, etc.)
                                txtSelectedIconInfo.Text = "name: " + crt.Name + Environment.NewLine + "tag: " + crt.Tag;
                                lastSelectedObjectTag = crt.Tag;
                                //lastSelectedObjectResRef = crt.ResRef;
                                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = crt;
                            }
                        }
                        foreach (CreatureRefs crt in ee_encounter.EncounterCreatureRefsList)
                        {
                            if (crt.CreatureStartLocation == newPoint)
                            {
                                // if so then give details about that icon (name, tag, etc.)
                                txtSelectedIconInfo.Text = "name: " + crt.CreatureName + Environment.NewLine
                                                            + "tag: " + crt.CreatureTag + Environment.NewLine
                                                            + "resref: " + crt.CreatureResRef;
                                //lastSelectedObjectResRef = crt.CreatureResRef;
                                lastSelectedObjectTag = crt.CreatureTag;
                                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = crt;
                            }
                        }
                        foreach (PropRefs prp in ee_encounter.EncounterPropRefsList)
                        {
                            if (prp.PropStartLocation == newPoint)
                            {
                                // if so then give details about that icon (name, tag, etc.)
                                txtSelectedIconInfo.Text = "name: " + prp.PropName + Environment.NewLine
                                                            + "tag: " + prp.PropTag + Environment.NewLine
                                                            + "resref: " + prp.PropResRef;
                                //lastSelectedObjectResRef = prp.PropResRef;
                                lastSelectedObjectTag = prp.PropTag;
                                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = prp;
                            }
                        }
                        foreach (Point pc in ee_encounter.EncounterPcStartLocations)
                        {
                            if (pc == newPoint)
                            {
                                txtSelectedIconInfo.Text = "selected a PC marker";
                            }
                        }                        
                    }
                    #endregion
                    break;
                #endregion
                #region Right Button
                case MouseButtons.Right:
                    // exit by right click or ESC
                    prntForm.logText("entered esc");
                    prntForm.logText(Environment.NewLine);
                    prntForm.selectedEncounterCreatureTag = "";
                    prntForm.selectedEncounterPropTag = "";
                    selected = false;
                    PCselected = false;
                    propSelected = false;
                    refreshMap();            
                    pictureBox1.Image = drawArea;
                    break;
                #endregion
            }
        }
        private void EncounterEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                // exit by right click or ESC
                prntForm.logText("entered escape");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedEncounterCreatureTag = "";
                prntForm.selectedEncounterPropTag = "";
                selected = false;
                PCselected = false;
                refreshMap();
                pictureBox1.Image = drawArea;
            }
        }
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            try
            {
                if (prntForm.selectedEncounterCreatureTag != "")
                {
                    selected = true;
                }
                if (prntForm.selectedEncounterPropTag != "")
                {
                    propSelected = true;
                }
                if (selected)
                {
                    string selectedCrt = prntForm.selectedEncounterCreatureTag;
                    ee_selectedCreature = prntForm.creaturesList.getCreatureByTag(selectedCrt);
                    if (ee_selectedCreature != null)
                    {
                        selectedBitmap = ee_selectedCreature.CharSprite.Image;
                        selectedBitmapSize = ee_selectedCreature.Size;
                    }
                }
                //else if (prntForm.PropSelected)
                else if (propSelected)
                {
                    string selectedProp = prntForm.selectedLevelMapPropTag;
                    ee_selectedProp = prntForm.propsList.getPropByTag(selectedProp);
                    if (ee_selectedProp != null)
                    {
                        selectedBitmap = ee_selectedProp.PropSprite.Image;
                        selectedBitmapSize = ee_selectedProp.PropSprite.SpriteSize.Width / tileSize;
                    }
                }
                else if (PCselected)
                {
                    // determine how many PCs have been placed
                    refreshPcIcon();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed on mouse enter map: " + ex.ToString()); 
            }
        }
        private void refreshPcIcon()
        {
            try
            {
                if (ee_encounter.EncounterPcStartLocations.Count == 5)
                    selectedBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc6.png");
                else if (ee_encounter.EncounterPcStartLocations.Count == 4)
                    selectedBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc5.png");
                else if (ee_encounter.EncounterPcStartLocations.Count == 3)
                    selectedBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc4.png");
                else if (ee_encounter.EncounterPcStartLocations.Count == 2)
                    selectedBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc3.png");
                else if (ee_encounter.EncounterPcStartLocations.Count == 1)
                    selectedBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc2.png");
                else
                    selectedBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\graphics\\PCLoc1.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed on refresh PC Icons: " + ex.ToString());
            }

        }
        private void btnRemoveSelectedCreature_Click(object sender, EventArgs e)
        {
            // iterate through creature ref list and see if tag matches
            int cnt = 0;
            foreach (CreatureRefs crt in ee_encounter.EncounterCreatureRefsList)
            {
                //if (crt.CreatureResRef == lastSelectedObjectResRef)
                if (crt.CreatureTag == lastSelectedObjectTag)
                {
                    // remove at index of matched location
                    ee_encounter.EncounterCreatureRefsList.RemoveAt(cnt);
                    crtBitmapList.RemoveAt(cnt);
                    refreshMap();
                    return;
                }
                cnt++;
            }
            // iterate through creature list and see if tag matches
            /*cnt = 0;
            foreach (Creature crt in ee_encounter.EncounterCreatureList.creatures)
            {
                if (crt.Tag == lastSelectedObjectTag)
                {
                    // remove at index of matched tag
                    ee_encounter.EncounterCreatureList.creatures.RemoveAt(cnt);
                    crtBitmapList.RemoveAt(cnt);
                    refreshMap();
                    return;
                }
                cnt++;
            }*/
            cnt = 0;
            foreach (PropRefs prp in ee_encounter.EncounterPropRefsList)
            {
                //if (prp.PropResRef == lastSelectedObjectResRef)
                if (prp.PropTag == lastSelectedObjectTag)
                {
                    // remove at index of matched tag
                    ee_encounter.EncounterPropRefsList.RemoveAt(cnt);
                    propBitmapList.RemoveAt(cnt);
                    refreshMap();
                    return;
                }
                cnt++;
            }
            /*cnt = 0;
            foreach (Prop prp in ee_encounter.EncounterPropList.propsList)
            {
                if (prp.PropResRef == lastSelectedObjectResRef)
                {
                    // remove at index of matched tag
                    ee_encounter.EncounterPropList.propsList.RemoveAt(cnt);
                    propBitmapList.RemoveAt(cnt);
                    refreshMap();
                    return;
                }
                cnt++;
            }*/
        }
        private void btnAddItems_Click(object sender, EventArgs e)
        {
            try
            {
                string newTag;
                newTag = prntForm.itemsList.itemsList[cmbItems.SelectedIndex].ItemTag;
                ee_encounter.EncounterInventoryTagList.Add(newTag);
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
                        ee_encounter.EncounterInventoryTagList.RemoveAt(lbxItems.SelectedIndex);
                }
                catch { }
                refreshLbxItems();
            }
        }
        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            showGrid = chkGrid.Checked;
            refreshMap();
        }

        private void btnProperties_Click(object sender, EventArgs e)
        {
            prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = ee_encounter;
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                // exit by right click or ESC
                prntForm.logText("entered escape");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedEncounterCreatureTag = "";
                prntForm.selectedEncounterPropTag = "";
                selected = false;
                PCselected = false;
                refreshMap();
                pictureBox1.Image = drawArea;
            }
        }
    }
}
