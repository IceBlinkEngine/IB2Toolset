using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
//using IceBlinkCore;
using WeifenLuo.WinFormsUI.Docking;

namespace IB2Toolset
{
    public struct selectionStruct
    {
        public int index;
        public int oldIndex;
        public int x, y;
    }

    public partial class LevelEditor : DockContent
    {
        public ParentForm prntForm;
        public const int tileSize = 50;
        public Module le_mod;
        //public Game le_game;
        public Bitmap drawArea;
        public Bitmap gameMapBitmap;
        public Bitmap selectedBitmap;
        //public int selectedBitmapSize = 1;
        public int mousex, mousey;
        public Graphics gfx;
        public int gridx, gridy;
        public Font fontArial;
        public string g_filename = "";
        public string g_directory = "";
        public Area area;
        public selectionStruct selectedTile;
        public List<string> spriteIconList = new List<string>();
        public List<string> scriptList = new List<string>();
        public bool showGrid = true;
        public Bitmap g_walkPass;
        public Bitmap g_walkBlock;
        public Bitmap g_LoSBlock;
        public Point lastSelectedCreaturePropIcon;
        public string lastSelectedObjectTag;
        public string lastSelectedObjectResRef;
        public Creature le_selectedCreature = new Creature();
        public Prop le_selectedProp = new Prop();
        public List<Bitmap> crtBitmapList = new List<Bitmap>(); //index will match AreaCreatureList index
        public List<Bitmap> propBitmapList = new List<Bitmap>(); //index will match AreaPropList index
        
        public LevelEditor(Module mod, ParentForm p)
        {
            InitializeComponent();
            le_mod = mod;
            //le_game = g;
            prntForm = p;
        }

        #region Methods
        private void loadAreaObjectBitmapLists()
        {
            foreach (Prop prp in area.Props)
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
        }
        private void openLevel(string g_dir, string g_fil, string g_filNoEx)
        {
            this.Cursor = Cursors.WaitCursor;
            
            try
            {
                area = area.loadAreaFile(g_dir + "\\" + g_fil + ".lvl");
                if (area == null)
                {
                    MessageBox.Show("returned a null area");
                    //le_game.errorLog("returned a null area");
                }
                //MessageBox.Show("open file success");
                loadAreaObjectBitmapLists();
                //loadAreaTriggerPassRefs();
                //area.passEventRefs(prntForm);
                //loadAreaObjectPassRefs();
                //loadAreaCreaturePropLists();                
                //redrawTilemap();
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open file: " + ex.ToString());
                //le_game.errorLog("failed to open file: " + ex.ToString());
            }

            // load JPG Map first
            try
            {
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".jpg"))
                {
                    gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".jpg");
                    drawArea = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".jpg");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName))
                {
                    gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName);
                    drawArea = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName);
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".png"))
                {
                    gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".png");
                    drawArea = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".png");
                }
                else
                {
                    gameMapBitmap = new Bitmap(area.MapSizeX * tileSize, area.MapSizeY * tileSize);
                    drawArea = new Bitmap(area.MapSizeX * tileSize, area.MapSizeY * tileSize);
                }
            }
            catch
            {
                gameMapBitmap = new Bitmap(area.MapSizeX * tileSize, area.MapSizeY * tileSize);
                drawArea = new Bitmap(area.MapSizeX * tileSize, area.MapSizeY * tileSize);
            }
            pictureBox1.Width = gameMapBitmap.Size.Width;
            pictureBox1.Height = gameMapBitmap.Size.Height;
            pictureBox1.Image = (Image)drawArea;
            gfx = Graphics.FromImage(drawArea);
            if (drawArea == null)
            {
                MessageBox.Show("returned a null Map bitmap");
                return;
            }
            refreshMap();
            this.Cursor = Cursors.Arrow;
        }
        private void saveTilemapFileAs()
        {
            //display the open file dialog
            saveFileDialog1.DefaultExt = ".lvl";
            saveFileDialog1.Filter = "Area Level Files|*.lvl";
            saveFileDialog1.Title = "Save Level File";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.OverwritePrompt = true;
            saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result != DialogResult.OK) return;
            if (saveFileDialog1.FileName.Length == 0) return;
            g_filename = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
            g_directory = Path.GetDirectoryName(saveFileDialog1.FileName);
            area.Filename = g_filename;
            area.ImageFileName = Path.GetFileNameWithoutExtension(saveFileDialog1.FileName);
            saveTilemapFile();
        }
        public void saveTilemapFile()
        {
            if (g_filename.Length == 0)
            {
                saveTilemapFileAs();
                return;
            }
            area.Filename = g_filename;
            //area.MapFileName = Path.GetFileNameWithoutExtension(g_filename) + ".jpg";
            area.saveAreaFile(g_directory + "\\" + g_filename + ".lvl");
        }
        private void createNewArea(int width, int height)
        {
            //create tilemap
            area = null;
            area = new Area();
            area.MapSizeX = width;
            area.MapSizeY = height;
            //area.MapSizeInPixels.Width = width * tileSize;
            //area.MapSizeInPixels.Height = height * tileSize;
            for (int index = 0; index < (width * height); index++)
            {
                Tile newTile = new Tile();
                newTile.Walkable = true;
                newTile.LoSBlocked = false;
                newTile.Visible = false;
                area.Tiles.Add(newTile);
            }
            try
            {
                gameMapBitmap = new Bitmap(area.MapSizeX * tileSize, area.MapSizeY * tileSize);
                drawArea = new Bitmap(area.MapSizeX * tileSize, area.MapSizeY * tileSize);
            }
            catch
            {
                gameMapBitmap = new Bitmap(800, 800);
                drawArea = new Bitmap(800, 800);
            }
            pictureBox1.Width = gameMapBitmap.Size.Width;
            pictureBox1.Height = gameMapBitmap.Size.Height;
            pictureBox1.Image = (Image)drawArea;
            gfx = Graphics.FromImage(drawArea);
            if (drawArea == null)
            {
                MessageBox.Show("returned a null Map bitmap");
                return;
            }
            refreshMap();
        }
        private void resetAreaTileValues(int width, int height)
        {
            //create tilemap
            //area = null;
            //area = new Area();
            area.MapSizeX = width;
            area.MapSizeY = height;
            //area.MapSizeInPixels.Width = width * tileSize;
            //area.MapSizeInPixels.Height = height * tileSize;
            for (int index = 0; index < (width * height); index++)
            {
                Tile newTile = new Tile();
                newTile.Walkable = true;
                newTile.LoSBlocked = false;
                newTile.Visible = false;
                area.Tiles.Add(newTile);
            }
        }
        public void refreshMap()
        {
            try
            {
                if (gameMapBitmap != null)
                {
                    gfx.DrawImage((Image)gameMapBitmap, 0, 0);
                }
                int cnt = 0;
                foreach (Prop prpRef in area.Props)
                {
                    int cspx = prpRef.LocationX;
                    int cspy = prpRef.LocationY;
                    spritePropDraw(cspx, cspy, cnt);
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
                //le_game.errorLog("failed on refresh map: " + ex.ToString());
            }
            drawTileSettings();
        }
        private void spritePropDraw(int cspx, int cspy, int spriteListIndex)
        {
            //source image
            Bitmap prpBitmap = propBitmapList[spriteListIndex];
            //Rectangle source = new Rectangle(0, 0, le_selectedProp.propBitmap.Width, le_selectedProp.propBitmap.Height);
            Rectangle source = new Rectangle(0, 0, prpBitmap.Width, prpBitmap.Height);
            //target location
            Rectangle target = new Rectangle(cspx * tileSize, cspy * tileSize, tileSize, tileSize);
            //draw sprite
            gfx.DrawImage((Image)prpBitmap, target, source, GraphicsUnit.Pixel);
        }
        private void drawTileSettings()
        {
            //for (int index = 0; index < area.MapSizeInSquares.Width * area.MapSizeInSquares.Height; index++)
            for (int x = 0; x < area.MapSizeX; x++)
            {
                for (int y = 0; y < area.MapSizeY; y++)
                {
                    if (showGrid) //if show grid is turned on, draw grid squares
                    {
                        if (area.Tiles[y * this.area.MapSizeX + x].LoSBlocked)
                        {
                            Rectangle src = new Rectangle(0, 0, tileSize, tileSize);
                            int dx = x * tileSize;
                            int dy = y * tileSize;
                            Rectangle target = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                            gfx.DrawImage(g_LoSBlock, target, src, GraphicsUnit.Pixel);
                        }
                        if (area.Tiles[y * this.area.MapSizeX + x].Walkable)
                        {
                            Rectangle src = new Rectangle(0, 0, tileSize, tileSize);
                            int dx = x * tileSize;
                            int dy = y * tileSize;
                            Rectangle target = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                            gfx.DrawImage(g_walkPass, target, src, GraphicsUnit.Pixel);
                        }
                        else
                        {
                            Rectangle src = new Rectangle(0, 0, tileSize, tileSize);
                            int dx = x * tileSize;
                            int dy = y * tileSize;
                            Rectangle target = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                            gfx.DrawImage(g_walkBlock, target, src, GraphicsUnit.Pixel);
                        }
                    }
                }
            }
            foreach (Trigger t in area.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    int dx = p.X * tileSize;
                    int dy = p.Y * tileSize;
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
                    Rectangle rect = new Rectangle(dx + 3, dy + 3, tileSize - 6, tileSize - 6);
                    gfx.DrawRectangle(pen, rect);
                }
            }
            pictureBox1.Image = drawArea;
        }
        public void drawSelectionBox(int gridx, int gridy)
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
        public void refreshLeftPanelInfo()
        {
            selectedTile.x = gridx;
            selectedTile.y = gridy;
            selectedTile.index = gridy * area.MapSizeX + gridx;
            drawSelectionBox(gridx, gridy);
        }
        private void clickDrawArea(MouseEventArgs e)
        {
            switch (e.Button)
            {
                #region Left Button
                case MouseButtons.Left:
                    refreshLeftPanelInfo();
                    prntForm.currentSelectedTrigger = null;
                    #region Creature Selected
                    /*if (prntForm.CreatureSelected)
                    {
                        string selectedCrt = prntForm.selectedLevelMapCreatureTag;
                        prntForm.logText(selectedCrt);
                        prntForm.logText(Environment.NewLine);

                        gridx = e.X / tileSize;
                        gridy = e.Y / tileSize;

                        prntForm.logText("gridx = " + gridx.ToString() + "gridy = " + gridy.ToString());
                        prntForm.logText(Environment.NewLine);
                        // verify that there is no creature, blocked, or PC already on this location
                        // add to a List<> a new item with the x,y coordinates
                        if (le_selectedCreature.SpriteFilename == "blank.spt")
                        {
                            return;
                        }
                        //check to see if a creature already exists on this square
                        foreach (CreatureRefs cr in area.AreaCreatureRefsList)
                        {
                            if (cr.CreatureStartLocation == new Point(gridx, gridy))
                            {
                                MessageBox.Show("Can only place one creature per square...aborting placement");
                                return;
                            }
                        }
                        CreatureRefs newCrtRef = new CreatureRefs();
                        newCrtRef.CreatureName = le_selectedCreature.Name;
                        newCrtRef.CreatureTag = le_selectedCreature.Tag + "_" + prntForm.mod.NextIdNumber;
                        newCrtRef.CreatureResRef = le_selectedCreature.ResRef;
                        newCrtRef.CreatureSize = le_selectedCreature.Size;
                        newCrtRef.MouseOverText = le_selectedCreature.MouseOverText;
                        newCrtRef.CreatureStartLocation = new Point(gridx, gridy);
                        area.AreaCreatureRefsList.Add(newCrtRef);                       
                        
                        // show the item on the map
                        if (le_mod.ModuleName != "NewModule")
                        {
                            Creature crt = le_selectedCreature.DeepCopy();
                            crt.LoadSpriteStuff(prntForm._mainDirectory + "\\modules\\" + le_mod.ModuleFolderName);
                            Bitmap newBitmap = (Bitmap)crt.CharSprite.Image.Clone();
                            //Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + le_mod.ModuleFolderName + "\\graphics\\sprites\\tokens\\module\\" + le_selectedCreature.CharSprite.SpriteSheetFilename);
                            crtBitmapList.Add(newBitmap);                            
                        }
                        else
                        {
                            Creature crt = le_selectedCreature.DeepCopy();
                            crt.LoadSpriteStuff(prntForm._mainDirectory + "\\data\\NewModule");
                            Bitmap newBitmap = (Bitmap)crt.CharSprite.Image.Clone();
                            //Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\NewModule\\graphics\\sprites\\tokens\\" + le_selectedCreature.CharSprite.SpriteSheetFilename);
                            crtBitmapList.Add(newBitmap);
                        }
                    }*/
                    #endregion
                    #region Prop Selected
                    if (prntForm.PropSelected)
                    {
                        string selectedProp = prntForm.selectedLevelMapPropTag;
                        prntForm.logText(selectedProp);
                        prntForm.logText(Environment.NewLine);

                        gridx = e.X / tileSize;
                        gridy = e.Y / tileSize;

                        prntForm.logText("gridx = " + gridx.ToString() + "gridy = " + gridy.ToString());
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
                        newProp.LocationX = gridx;
                        newProp.LocationY = gridy;
                        newProp.PostLocationX = gridx;
                        newProp.PostLocationY = gridy;
                        area.Props.Add(newProp);                        
                        /*Prop newProp = new Prop();
                        newProp = le_selectedProp.DeepCopy();
                        newProp.PropTag = le_selectedProp.PropTag + "_" + prntForm.mod.NextIdNumber;
                        newProp.Location = new Point(gridx, gridy);
                        area.AreaPropList.propsList.Add(newProp);*/
                        // show the item on the map
                        if (le_mod.moduleName != "NewModule")
                        {
                            //Prop prp = le_selectedProp.DeepCopy();
                            //prp.LoadPropSpriteStuffForTS(prntForm._mainDirectory + "\\modules\\" + le_mod.ModuleFolderName);
                            //Bitmap newBitmap = (Bitmap)prp.PropSprite.Image.Clone();
                            Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + le_mod.moduleName + "\\graphics\\" + le_selectedProp.ImageFileName + ".png");
                            propBitmapList.Add(newBitmap);
                        }
                        else
                        {
                            //Prop prp = le_selectedProp.DeepCopy();
                            //prp.LoadPropSpriteStuffForTS(prntForm._mainDirectory + "\\data\\NewModule");
                            //Bitmap newBitmap = (Bitmap)prp.PropSprite.Image.Clone();
                            //Bitmap newBitmap = new Bitmap(prntForm._mainDirectory + "\\data\\NewModule\\graphics\\sprites\\props\\" + le_selectedProp.PropSprite.SpriteSheetFilename);
                            //propBitmapList.Add(newBitmap);
                        }
                    }
                    #endregion
                    #region Paint New Trigger Selected
                    else if (rbtnPaintTrigger.Checked)
                    {                                                                  
                        string selectedTrigger = prntForm.selectedLevelMapTriggerTag;
                        prntForm.logText(selectedTrigger);
                        prntForm.logText(Environment.NewLine);

                        gridx = e.X / tileSize;
                        gridy = e.Y / tileSize;

                        prntForm.logText("gridx = " + gridx.ToString() + "gridy = " + gridy.ToString());
                        prntForm.logText(Environment.NewLine);
                        Point newPoint = new Point(gridx, gridy);
                        //add the selected square to the squareList if doesn't already exist
                        try
                        {
                            //check: if click square already exists, then erase from list                            
                            Trigger newTrigger = area.getTriggerByTag(selectedTrigger);
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
                        refreshMap();
                    }
                    #endregion
                    #region Edit Trigger Selected
                    else if (rbtnEditTrigger.Checked)
                    {
                        if (prntForm.selectedLevelMapTriggerTag != null)
                        {
                            string selectedTrigger = prntForm.selectedLevelMapTriggerTag;
                            prntForm.logText(selectedTrigger);
                            prntForm.logText(Environment.NewLine);

                            gridx = e.X / tileSize;
                            gridy = e.Y / tileSize;

                            prntForm.logText("gridx = " + gridx.ToString() + "gridy = " + gridy.ToString());
                            prntForm.logText(Environment.NewLine);
                            Point newPoint = new Point(gridx, gridy);
                            try
                            {
                                //check: if click square already exists, then erase from list  
                                Trigger newTrigger = area.getTriggerByTag(selectedTrigger);
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
                            refreshMap();
                        }
                    }
                    #endregion
                    #region Walkmesh Toggle Selected
                    else if (rbtnWalkable.Checked)
                    {
                        gridx = e.X / tileSize;
                        gridy = e.Y / tileSize;
                        selectedTile.index = gridy * area.MapSizeX + gridx;
                        prntForm.logText("gridx = " + gridx.ToString() + "gridy = " + gridy.ToString());
                        prntForm.logText(Environment.NewLine);
                        if (area.Tiles[selectedTile.index].Walkable == true)
                        {
                            area.Tiles[selectedTile.index].Walkable = false;
                        }
                        else
                        {
                            area.Tiles[selectedTile.index].Walkable = true;
                        }
                        refreshMap();
                    }
                    #endregion
                    #region LoS mesh Toggle Selected
                    else if (rbtnLoS.Checked)
                    {
                        gridx = e.X / tileSize;
                        gridy = e.Y / tileSize;
                        selectedTile.index = gridy * area.MapSizeX + gridx;
                        prntForm.logText("gridx = " + gridx.ToString() + "gridy = " + gridy.ToString());
                        prntForm.logText(Environment.NewLine);
                        if (area.Tiles[selectedTile.index].LoSBlocked == true)
                        {
                            area.Tiles[selectedTile.index].LoSBlocked = false;
                        }
                        else
                        {
                            area.Tiles[selectedTile.index].LoSBlocked = true;
                        }
                        refreshMap();
                    }
                    #endregion
                    #region None Selected
                    else // not placing, just getting info and possibly deleteing icon
                    {
                        contextMenuStrip1.Items.Clear();
                        //when left click, get location
                        gridx = e.X / tileSize;
                        gridy = e.Y / tileSize;
                        Point newPoint = new Point(gridx, gridy);                        
                        EventHandler handler = new EventHandler(HandleContextMenuClick);
                        //loop through all the objects
                        //if has that location, add the tag to the list                    
                        //draw selection box
                        drawSelectionBox(gridx, gridy);
                        txtSelectedIconInfo.Text = "";
                                                
                        foreach (Prop prp in area.Props)
                        {
                            if ((prp.LocationX == newPoint.X) && (prp.LocationY == newPoint.Y))
                            {
                                // if so then give details about that icon (name, tag, etc.)
                                txtSelectedIconInfo.Text = "name: " + prp.ImageFileName + Environment.NewLine
                                                            + "tag: " + prp.PropTag + Environment.NewLine;
                                lastSelectedObjectTag = prp.PropTag;
                                //prntForm.selectedLevelMapPropTag = prp.PropTag;
                                pictureBox1.ContextMenuStrip.Items.Add(prp.PropTag, null, handler); //string, image, handler
                                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = prp;
                            }
                        }
                        foreach (Trigger t in area.Triggers)
                        {
                            foreach (Coordinate p in t.TriggerSquaresList)
                            {
                                if ((p.X == newPoint.X) && (p.Y == newPoint.Y))
                                {
                                    txtSelectedIconInfo.Text = "Trigger Tag: " + Environment.NewLine + t.TriggerTag;
                                    lastSelectedObjectTag = t.TriggerTag;
                                    prntForm.currentSelectedTrigger = t;
                                    prntForm.frmTriggerEvents.refreshTriggers();
                                    pictureBox1.ContextMenuStrip.Items.Add(t.TriggerTag, null, handler); //string, image, handler
                                    //prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = t;
                                }
                            }
                        }
                        //if the list is less than 2, do nothing
                        if (pictureBox1.ContextMenuStrip.Items.Count > 1)
                        {
                            contextMenuStrip1.Show(pictureBox1, e.Location);
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
                    //prntForm.selectedEncounterCreatureTag = "";
                    prntForm.selectedLevelMapCreatureTag = "";
                    prntForm.selectedLevelMapPropTag = "";
                    prntForm.CreatureSelected = false;
                    prntForm.PropSelected = false;
                    prntForm.currentSelectedTrigger = null;
                    refreshMap();
                    pictureBox1.Image = drawArea;
                    rbtnInfo.Checked = true;
                    break;
                #endregion
            }
        }
        
        #endregion

        #region Event Handlers
        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            if (le_mod.moduleName != "NewModule")
                openFileDialog2.InitialDirectory = prntForm._mainDirectory + "\\modules\\" + le_mod.moduleName + "\\graphics";
            else
                openFileDialog2.InitialDirectory = prntForm._mainDirectory + "\\default\\NewModule";
            //openFileDialog2.InitialDirectory = Environment.CurrentDirectory;
            //Empty the FileName text box of the dialog
            openFileDialog2.FileName = String.Empty;
            openFileDialog2.Filter = "Map (*.jpg)|*.jpg";
            openFileDialog2.FilterIndex = 1;

            DialogResult result = openFileDialog2.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                Bitmap testSize = new Bitmap(Path.GetFullPath(openFileDialog2.FileName));
                if ((testSize.Width > 800) || (testSize.Height > 800))
                {
                    MessageBox.Show("Map images must be less than 800x800 pixels");
                    return;
                }
                string filename = Path.GetFullPath(openFileDialog2.FileName);
                area.ImageFileName = Path.GetFileNameWithoutExtension(openFileDialog2.FileName);
                //string directory = Path.GetDirectoryName(openFileDialog2.FileName);
                //MessageBox.Show("filename selected = " + filename);
                gameMapBitmap = new Bitmap(filename);
                drawArea = new Bitmap(filename);
                pictureBox1.Width = gameMapBitmap.Size.Width;
                pictureBox1.Height = gameMapBitmap.Size.Height;
                pictureBox1.Image = (Image)drawArea;
                gfx = Graphics.FromImage(drawArea);

                if (drawArea == null)
                {
                    MessageBox.Show("returned a null bitmap");
                    //le_game.errorLog("returned a null Map bitmap");
                }
                resetAreaTileValues(gameMapBitmap.Size.Width / tileSize, gameMapBitmap.Size.Height / tileSize);
                refreshMap();
            }
        }
        private void btnSaveArea_Click(object sender, EventArgs e)
        {
            saveTilemapFile();
            MessageBox.Show("Area Saved");
        }
        private void LevelEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("closing editor and removing from openAreaList");
            prntForm.openAreasList.Remove(area);
        }
        private void LevelEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            gfx.Dispose();
            drawArea.Dispose();          
            //gfxSelected.Dispose();
            //selectedBitmap.Dispose();
            //this.Close();
        }
        private void LevelEditor_Load(object sender, EventArgs e)
        {
            //prntForm = (ParentForm)this.ParentForm;
            try
            {
                g_walkPass = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_pass.png");
                g_walkBlock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_block.png");
                g_LoSBlock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\los_block.png");
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to load walkPass and walkBlock bitmaps: " + ex.ToString());
                //le_game.errorLog("failed to load walkPass and walkBlock bitmaps: " + ex.ToString());
            }
            area = new Area();
            area.MapSizeX = 16;
            area.MapSizeY = 16;
            //createNewArea(area.MapSizeInSquares.Width, area.MapSizeInSquares.Height);
            //set up level drawing surface
            drawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = drawArea;
            gfx = Graphics.FromImage(drawArea);

            //selected image
            //selectedBitmap = new Bitmap(picSelected.Size.Width, picSelected.Size.Height);
            //picSelected.Image = selectedBitmap;
            //gfxSelected = Graphics.FromImage(selectedBitmap);

            //create font
            //fontArial = new Font("Arial Narrow", 40);

            // try and load the file selected if it exists
            string g_filename = le_mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
            string g_directory = prntForm._mainDirectory + "\\modules\\" + le_mod.moduleName + "\\areas";
            string filenameNoExtension = Path.GetFileNameWithoutExtension(le_mod.moduleAreasList[prntForm._selectedLbxAreaIndex]);
            if (File.Exists(g_directory + "\\" + g_filename + ".lvl"))
            {
                openLevel(g_directory, g_filename, filenameNoExtension);
                if (area == null)
                {
                    createNewArea(area.MapSizeX, area.MapSizeY);
                    area.Filename = g_filename;
                }
            }
            else
            {
                createNewArea(area.MapSizeX, area.MapSizeY);
                area.Filename = g_filename;
            }
            //refreshCmbBoxes();
            prntForm.openAreasList.Add(area);
            rbtnInfo.Checked = true;            
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            clickDrawArea(e);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {            
            gridx = e.X / tileSize;
            gridy = e.Y / tileSize;
            mousex = e.X;
            mousey = e.Y;
            lblMouseInfo.Text = "CURSOR " + e.X.ToString() + "," + e.Y.ToString() + Environment.NewLine + "GRID " + gridx.ToString() + "," + gridy.ToString();
            if ((prntForm.CreatureSelected) || (prntForm.PropSelected))
            {
                refreshMap();
                try
                {
                    if (selectedBitmap != null)
                    {
                        //source image size
                        Rectangle frame = new Rectangle(0, 0, selectedBitmap.Width, selectedBitmap.Height);
                        //target location
                        Rectangle target = new Rectangle(gridx * tileSize, gridy * tileSize, tileSize, tileSize);
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
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Select();
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
                    /*string selectedCrt = prntForm.selectedLevelMapCreatureTag;
                    le_selectedCreature = prntForm.creaturesList.getCreatureByTag(selectedCrt);
                    if (le_selectedCreature != null)
                    {
                        selectedBitmap = le_selectedCreature.CharSprite.Image;
                        selectedBitmapSize = le_selectedCreature.Size;
                    }*/
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
        private void btnRemoveSelectedObject_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (Prop prp in area.Props)
            {
                if (prp.PropTag == lastSelectedObjectTag)
                {
                    // remove at index of matched tag
                    area.Props.RemoveAt(cnt);
                    propBitmapList.RemoveAt(cnt);
                    refreshMap();
                    return;
                }
                cnt++;
            }
            foreach (Trigger t in area.Triggers)
            {
                if (t.TriggerTag == lastSelectedObjectTag)
                {
                    // remove at index of matched tag
                    area.Triggers.Remove(t);
                    refreshMap();
                    return;
                }
            }
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
                refreshMap();
                pictureBox1.Image = drawArea;
            }
        }
        private void rbtnPaintTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPaintTrigger.Checked)
            {
                //create a new trigger object
                Trigger newTrigger = new Trigger();
                //increment the tag to something unique
                newTrigger.TriggerTag = "newTrigger_" + prntForm.mod.nextIdNumber;
                prntForm.selectedLevelMapTriggerTag = newTrigger.TriggerTag;
                area.Triggers.Add(newTrigger);
                //set propertygrid to the new object
                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = newTrigger;
                prntForm.logText("painting a new trigger");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                refreshMap();
                pictureBox1.Image = drawArea;
            }
        }
        private void rbtnEditTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnEditTrigger.Checked)
            {
                prntForm.logText("edit trigger: ");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                prntForm.selectedLevelMapTriggerTag = lastSelectedObjectTag;
                refreshMap();
                pictureBox1.Image = drawArea;
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
                refreshMap();
                pictureBox1.Image = drawArea;
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
                refreshMap();
                pictureBox1.Image = drawArea;
            }
        }
        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            showGrid = chkGrid.Checked;
            refreshMap();
        }
        private void LevelEditor_KeyDown(object sender, KeyEventArgs e)
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
                refreshMap();
                pictureBox1.Image = drawArea;
                rbtnInfo.Checked = true;
            }
        }
        public void HandleContextMenuClick(object sender, EventArgs e)
        {
            //else, handler returns the selected tag
            ToolStripMenuItem menuItm = (ToolStripMenuItem)sender;
            //get object by tag
            //Set propertygrid to that object
            //set last selected object...used for remove
            foreach (Prop prp in area.Props)
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
            }
            foreach (Trigger t in area.Triggers)
            {
                if (t.TriggerTag == menuItm.Text)
                {
                    txtSelectedIconInfo.Text = "Trigger Tag: " + Environment.NewLine + t.TriggerTag;
                    lastSelectedObjectTag = t.TriggerTag;
                    prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = t;
                    return;
                }
            }
        }
        #endregion  

        private void btnProperties_Click(object sender, EventArgs e)
        {
            prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = area;
        }

        private void pictureBox1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
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
                refreshMap();
                pictureBox1.Image = drawArea;
                rbtnInfo.Checked = true;
            }
        }        
    }
}
