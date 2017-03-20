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
using SharpDX.Direct2D1;
using Bitmap = System.Drawing.Bitmap;
using SharpDX.DXGI;
using SharpDX.DirectWrite;

namespace IB2Toolset
{
    public struct selectionStruct
    {
        public int index;
        public int oldIndex;
        public int x, y;
    }
    public struct tilepropToBePlacedSettings
    {
        public int angle;
        public bool mirror;
        public int xshift;
        public int yshift;
        public int xscale;
        public int yscale;
        public float opacity;
    }

    public partial class WorldMapEditor : DockContent
    {
        public ParentForm prntForm;
        public Module mod;

        private List<TileBitmapNamePair> tileList = new List<TileBitmapNamePair>();
        //GDI private Graphics device;
        //GDI private Bitmap surface;
        //GDI public Bitmap gameMapBitmap;
        public SharpDX.Direct2D1.Bitmap gameMapBitmapD2D;
        private Bitmap selectedBitmap;
        public string selectedBitmapFilename = "";
        //GDI public Bitmap g_walkPass;
        //GDI public Bitmap g_walkBlock;
        //GDI public Bitmap g_LoSBlock;
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
        public tilepropToBePlacedSettings tileToBePlaced;
        public bool tileSelected = true;
        //GDI Pen blackPen = new Pen(Color.Black, 1);
        public Point currentSquareClicked = new Point(0, 0);
        public Point lastSquareClicked = new Point(0, 0);
        //public Font fontArial;
        public string g_filename = "";
        public string g_directory = "";
        public Area area;
        public selectionStruct selectedTile;
        public Point lastSelectedCreaturePropIcon;
        public string lastSelectedObjectTag;
        public string lastSelectedObjectResRef;
        public Prop le_selectedProp = new Prop();
        //GDI public List<Bitmap> crtBitmapList = new List<Bitmap>(); //index will match AreaCreatureList index
        //GDI public List<Bitmap> propBitmapList = new List<Bitmap>(); //index will match AreaPropList index

        public Bitmap sourceBitmap;
        //public string sourceBitmapName = "";
        string[] args = new string[2];
        string xCells = "";
        string yCells = "";

        bool calledFromLoadButton = false;
        bool firstTimeLoadingMap = true;

        #region Direct2D Stuff
        public bool useDirect2D = true;
        public SharpDX.Direct2D1.Factory Factory2D { get; private set; }
        public SharpDX.DirectWrite.Factory FactoryDWrite { get; private set; }
        public WindowRenderTarget RenderTarget2D { get; private set; }
        public SolidColorBrush SceneColorBrush { get; private set; }
        public Dictionary<string, SharpDX.Direct2D1.Bitmap> commonBitmapList = new Dictionary<string, SharpDX.Direct2D1.Bitmap>();
        public Point selectionBoxLocation = new Point(-1, -1);
        public TextFormat textFormat;
        public TextLayout textLayout;

        public bool firstElementHasBeenPlaced = false;
        public int stairRotationCounter = 1; //1=N, 2=E, 3= S, 4 = W
        public int bridgeRotationCounter = 1; //1=EW-Bridge, 2=NS-Bridge
        public int transitionToMasterRotationCounter = 1; //1=N, 2=E, 3= S, 4 = W
        #endregion

        public WorldMapEditor(Module m, ParentForm p)
        {
            InitializeComponent(p);
            
            mod = m;
            prntForm = p;
            resetTileToBePlacedSettings();
            //createTileImageButtons();
            createTileImageButtons("t_");

            //prntForm._mainDirectory = Directory.GetCurrentDirectory();
            if (useDirect2D)
            {
                //TODO add D2D stuff                
            }
            else
            {
                /*//GDI surface = new Bitmap(mSizeW, mSizeH);
                panelView.BackgroundImage = surface;
                device = Graphics.FromImage(surface);

                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_pass.png"))
                {
                    g_walkPass = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_pass.png");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\walk_pass.png"))
                {
                    g_walkPass = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\walk_pass.png");
                }
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_block.png"))
                {
                    g_walkBlock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_block.png");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\walk_block.png"))
                {
                    g_walkBlock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\walk_block.png");
                }
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\los_block.png"))
                {
                    g_LoSBlock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\los_block.png");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\los_block.png"))
                {
                    g_LoSBlock = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\los_block.png");
                }
                if ((g_walkBlock == null) || (g_walkPass == null) || (g_LoSBlock == null))
                {
                    try
                    {
                        g_walkPass = new Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\ui\\walk_pass.png");
                        g_walkBlock = new Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\ui\\walk_block.png");
                        g_LoSBlock = new Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\ui\\los_block.png");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("failed to load walk_pass, los_block, and walk_block bitmaps: " + ex.ToString());
                        //le_game.errorLog("failed to load walkPass and walkBlock bitmaps: " + ex.ToString());
                    }
                }//GDI */
            }
        }

        //Synch
        private void btnSynchArea_Click(object sender, EventArgs e)
        {
            synchLevel();
        }


        private void btnTileFilter_Click(object sender, EventArgs e)
        {  
             Button selectBtn = (Button)sender;  
             createTileImageButtons(selectBtn.Text);  
        }


    private void createTileImageButtons(string filter)
        {
            try
            {

                //create list of tile prefixes  
                //loop through list and create buttons using name of prefix for btn.Text  
                this.flTileFilters.Controls.Clear();
                foreach (string f in prntForm.tilePrefixFilterList)
                {
                    Button btnTileFilter = new Button();
                    btnTileFilter.FlatAppearance.BorderColor = System.Drawing.Color.Black;
                    btnTileFilter.FlatAppearance.BorderSize = 2;
                    btnTileFilter.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
                    btnTileFilter.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Green;
                    btnTileFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btnTileFilter.Size = new System.Drawing.Size(52, 25);
                    btnTileFilter.Text = f;
                    btnTileFilter.UseVisualStyleBackColor = true;
                    btnTileFilter.Click += new System.EventHandler(this.btnTileFilter_Click);
                    this.flTileFilters.Controls.Add(btnTileFilter);
                }

                this.flPanelTab1.Controls.Clear();
                tileList.Clear();
                foreach (string f in Directory.GetFiles(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\", "*.png"))
                {
                    if (!Path.GetFileName(f).StartsWith(filter))
                    {
                        continue;
                    }

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

                foreach (string f in Directory.GetFiles(prntForm._mainDirectory + "\\default\\" + "NewModule" + "\\tiles\\", "*.png"))
                {
                    if (!Path.GetFileName(f).StartsWith(filter))
                    {
                        continue;
                    }

                    bool skip = false;
                    foreach (TileBitmapNamePair t in tileList)
                    {
                        if (Path.GetFileNameWithoutExtension(f) == t.filename)
                        {
                            skip = true;
                            break;
                        }
                    }

                    if (skip)
                    {
                        continue;
                    }

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


        //LINKED
        private void btnCreateLinkedArea_Click(object sender, EventArgs e)
        {
            //works only on non-link areas, ie areas without master
            if (area.masterOfThisArea == "none")
            {

                //check if linked area with to-be-raised-counter already exists 
                bool alreadyExists = true;
                string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas";
                string filename = area.Filename;
                //filename + "_" + area.linkedAreasCounter.ToString() + "_.lvl");

                while (alreadyExists)
                {
                    if ((File.Exists(filePath + "\\" + filename + "_" + area.linkedAreasCounter.ToString() + "_.lvl")))
                    {
                        //alreadyExists = true;
                        area.linkedAreasCounter++;
                    }
                    else
                    {
                        alreadyExists = false;
                    }
                }
                //area.linkedAreasCounter++;

                //if file exists, rename the file
                //string filename = area.Filename;
                if ((File.Exists(filePath + "\\" + filename + ".lvl")) && (!alreadyExists))
                {
                    try
                    {
                        //if (area.linkedAreasCounter == 0)
                        //{
                            //area.linkedAreasCounter = 1;
                        //}
                        area.linkNumbers.Add(area.linkedAreasCounter);
                        mod.masterAreasList.Add(area.Filename);
                        //prntForm.mod.linkedAreasCounter++;
                        //rename file
                        File.Copy(filePath + "\\" + filename + ".lvl", filePath + "\\" + filename + "_" + area.linkedAreasCounter.ToString() + "_.lvl");
                        try
                        {
                            //load area
                            Area newArea = new Area();
                            newArea = newArea.loadAreaFile(filePath + "\\" + filename + "_" + area.linkedAreasCounter.ToString() + "_.lvl");
                            if (newArea == null)
                            {
                                MessageBox.Show("returned a null area");
                            }
                            //change area file name in area file object properties
                            newArea.Filename = filename + "_" + area.linkedAreasCounter.ToString() + "_";
                            //newArea.saveAreaFile(filePath + "\\" + filename + "_" + prntForm.mod.linkedAreasCounter.ToString() + "_.lvl");
                            foreach (Tile t in newArea.Tiles)
                            {
                                t.linkedToMasterMap = true;
                            }
                            newArea.masterOfThisArea = area.Filename;
                            newArea.linkNumberOfThisArea = area.linkedAreasCounter;
                            area.linkedAreas.Add(newArea.Filename);
                            newArea.saveAreaFile(filePath + "\\" + filename + "_" + area.linkedAreasCounter.ToString() + "_.lvl");
                            prntForm.mod.moduleAreasList.Add(newArea.Filename);
                            prntForm.mod.moduleAreasList.Sort();
                            prntForm.frmAreas.refreshListBoxAreas();
                            
                            /*
                            Area newArea = new Area();
                            newArea.Filename = "new area";
                            prntForm.mod.moduleAreasList.Add(newArea.Filename);
                            refreshListBoxAreas();
                            */
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("failed to open file: " + ex.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString()); // Write error
                    }
                }
                else
                {
                    MessageBox.Show("File: " + filename + ".lvl does not exist in the areas folder");
                }
                //refreshListBoxAreas();
                
            }

            prntForm.frmAreas.refreshListBoxAreas();
            //refreshLeftPanelInfo();
        }        


        private void WorldMapEditor_Load(object sender, EventArgs e)
        {
            //LoadEncounters();
            radioButton1.Checked = true;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            //createTileImageButtons(); 

            area = new Area();
            area.MapSizeX = 16;
            area.MapSizeY = 16;

            // try and load the file selected if it exists
            string g_filename = mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
            string g_directory = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas";
            string filenameNoExtension = Path.GetFileNameWithoutExtension(mod.moduleAreasList[prntForm._selectedLbxAreaIndex]);
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
                if (useDirect2D)
                {
                    //TODO add D2D stuff
                    InitDirect2DAndDirectWrite();
                }
            }
            lblMapSizeX.Text = area.MapSizeX.ToString();
            lblMapSizeY.Text = area.MapSizeY.ToString();
            //set up level drawing surface
            
            if (useDirect2D)
            {
                //TODO add D2D stuff
                /*try
                {
                    InitDirect2DAndDirectWrite();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to use DirectX, DirectX option will not be available. Error: " + ex.ToString());
                    prntForm.errorLog(ex.ToString());
                }*/
            }
            else
            {
                //GDI panelView.Width = area.MapSizeX * sqr;
                //GDI panelView.Height = area.MapSizeY * sqr;
                //GDI surface = new Bitmap(panelView.Size.Width, panelView.Size.Height);
                //GDI device = Graphics.FromImage(surface);
                //GDI panelView.BackgroundImage = (Image)surface;
            }

            //refreshCmbBoxes();
            prntForm.openAreasList.Add(area);
            rbtnInfo.Checked = true;
            rbtnZoom1x.Checked = true;
            //refreshMap(true);
            numBGLocX.Value = area.backgroundImageStartLocX;
            numBGLocY.Value = area.backgroundImageStartLocY;
            //Set this map to be a WORLD MAP
            //area.IsWorldMap = true;
        }
        public void resetTileToBePlacedSettings()
        {
            tileToBePlaced.angle = 0;
            tileToBePlaced.mirror = false;
            tileToBePlaced.xshift = 0;
            tileToBePlaced.yshift = 0;
            tileToBePlaced.xscale = 0;
            tileToBePlaced.yscale = 0;
            tileToBePlaced.opacity = 1;
            
        }
        private void resetPanelAndDeviceSize()
        {
            
            if (useDirect2D)
            {
                //TODO add D2D stuff
            }
            else
            {
                //GDI panelView.Width = area.MapSizeX * sqr;
                //GDI panelView.Height = area.MapSizeY * sqr;
                //GDI surface = new Bitmap(area.MapSizeX * sqr, area.MapSizeY * sqr);
                //GDI device = Graphics.FromImage(surface);
            }
        }
        private void createTileImageButtons()
        {
            try
            {
                this.flPanelTab1.Controls.Clear();
                tileList.Clear();
                foreach (string f in Directory.GetFiles(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\", "*.png"))
                {
                    //foreach (string g in Directory.GetFileSystemEntries(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\", "*.png"))
                    //{
                    //}
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

                        //add the layer0 tiles on top

                        //TileBitmapNamePair u = new TileBitmapNamePair((Bitmap)bit.Clone(), Path.GetFileNameWithoutExtension(f));
                        //tileList.Add(u);
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
        private void refreshMap(bool refreshAll)
        {
            /*//GDI        
            if (area == null) { return; }
            //if area is small, always do a full redraw (refreshAll == true)
            if ((area.MapSizeX < 20) && (area.MapSizeY < 20))
            {
                refreshAll = true;
            }
            if (refreshAll)
            {
                device.Clear(Color.Gainsboro);
            }
            try
            {
                //draw background image first if using one
                if ((!area.ImageFileName.Equals("none")) && (gameMapBitmap != null))
                {
                    Rectangle srcBG = new Rectangle(0, 0, gameMapBitmap.Width, gameMapBitmap.Height);
                    Rectangle dstBG = new Rectangle(area.backgroundImageStartLocX * sqr, area.backgroundImageStartLocY * sqr, sqr * (gameMapBitmap.Width / 50), sqr * (gameMapBitmap.Height / 50));
                    device.DrawImage(gameMapBitmap, dstBG, srcBG, GraphicsUnit.Pixel);
                }
                //draw map en block
                //new code for drawing layer 0, aka puzzle pieces of hand drawn map
                //if (calledFromLoadButton == true)
                //{
                calledFromLoadButton = false;
                if (mod.useAllTileSystem)
                {
                    #region Draw Layer 0
                    if (area.sourceBitmapName != "")
                    {
                        int tileCounter = 0;
                        for (int y = 0; y < area.MapSizeY; y++)
                        {
                            for (int x = 0; x < area.MapSizeX; x++)
                            {
                                if ((refreshAll) || (currentSquareClicked == new Point(x, y)) || (lastSquareClicked == new Point(x, y)))
                                {
                                    Tile tile = area.Tiles[y * area.MapSizeX + x];
                                    Bitmap lyr0 = null;
                                    try
                                    {
                                        tile.Layer0Filename = area.sourceBitmapName + tileCounter.ToString();
                                    }
                                    catch { }
                                    if ((tile.Layer0Filename != null) && (tile.Layer0Filename != "") && tile.Layer0Filename != "t_blank")
                                    {
                                        if (area.isPNGMap)
                                        {
                                            string bitMapPath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + tile.Layer0Filename + ".png";
                                        }
                                        if (area.isJPGMap)
                                        {
                                            string bitMapPath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + tile.Layer0Filename + ".jpg";
                                        }
                                        tileCounter++;
                                        //Rectangle src1 = new Rectangle(0, 0, 100, 100);

                                        //if (tile.Layer0Filename != null)
                                        //{

                                        //lyr0 = new Bitmap(Path.GetFullPath(tile.Layer0Filename + ".png"));
                                        //g_walkPass = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\walk_pass.png");
                                        //lyr0 = getTileByName(tile.Layer0Filename).bitmap;
                                        try
                                        {
                                            if (area.isPNGMap)
                                            {
                                                lyr0 = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + tile.Layer0Filename + ".png");
                                                //int block = 3;
                                            }
                                            if (area.isJPGMap)
                                            {
                                                lyr0 = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + tile.Layer0Filename + ".jpg");
                                                //int block = 3;
                                            }
                                        }
                                        catch
                                        {

                                        }
                                        //lyr1 = (Bitmap)getTileByName(tile.Layer1Filename).bitmap.Clone();
                                        //flip about y-axis layer
                                        //lyr1 = Flip(lyr1, tile.Layer1Flip);
                                        //rotate layer
                                        //lyr1 = Rotate(lyr1, tile.Layer1Rotate);
                                        //src1 = new Rectangle(0, 0, lyr1.Width, lyr1.Height);
                                        //}
                                        //Rectangle src = new Rectangle(0, 0, 100, 100);
                                        //Rectangle dst = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                                        //draw layer 1 first


                                        if (lyr0 != null)
                                        {
                                            //float scalerX = lyr0.Width / 100;
                                            //float scalerY = lyr0.Height / 100;
                                            float scalerX = 1;
                                            float scalerY = 1;
                                            Rectangle src = new Rectangle(0, 0, lyr0.Width, lyr0.Height);
                                            Rectangle dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                            device.DrawImage(lyr0, dst, src, GraphicsUnit.Pixel);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }
                #region Draw Layer 1
                if (checkBox1.Checked)
                {
                    for (int y = 0; y < area.MapSizeY; y++)
                    {
                        for (int x = 0; x < area.MapSizeX; x++)
                        {
                            if ((refreshAll) || (currentSquareClicked == new Point(x, y)) || (lastSquareClicked == new Point(x, y)))
                            {
                                Tile tile = area.Tiles[y * area.MapSizeX + x];
                                Bitmap lyr1 = null;
                                if (getTileByName(tile.Layer1Filename) != null)
                                {
                                    lyr1 = getTileByName(tile.Layer1Filename).bitmap;
                                }
                                //draw layer 1 first
                                if (lyr1 != null)
                                {
                                    float scalerX = lyr1.Width / 100;
                                    float scalerY = lyr1.Height / 100;
                                    Rectangle src = new Rectangle(0, 0, lyr1.Width, lyr1.Height);
                                    Rectangle dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr1, dst, src, GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region Draw Layer 2
                if (checkBox2.Checked)
                {
                    for (int y = 0; y < area.MapSizeY; y++)
                    {
                        for (int x = 0; x < area.MapSizeX; x++)
                        {
                            if ((refreshAll) || (currentSquareClicked == new Point(x, y)) || (lastSquareClicked == new Point(x, y)))
                            {
                                Tile tile = area.Tiles[y * area.MapSizeX + x];
                                Bitmap lyr1 = null;
                                if (getTileByName(tile.Layer2Filename) != null)
                                {
                                    lyr1 = getTileByName(tile.Layer2Filename).bitmap;
                                }
                                if (lyr1 != null)
                                {
                                    float scalerX = lyr1.Width / 100;
                                    float scalerY = lyr1.Height / 100;
                                    Rectangle src = new Rectangle(0, 0, lyr1.Width, lyr1.Height);
                                    Rectangle dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr1, dst, src, GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region Draw Layer 3
                if (checkBox3.Checked)
                {
                    for (int y = 0; y < area.MapSizeY; y++)
                    {
                        for (int x = 0; x < area.MapSizeX; x++)
                        {
                            if ((refreshAll) || (currentSquareClicked == new Point(x, y)) || (lastSquareClicked == new Point(x, y)))
                            {
                                Tile tile = area.Tiles[y * area.MapSizeX + x];
                                Bitmap lyr1 = null;
                                if (getTileByName(tile.Layer3Filename) != null)
                                {
                                    lyr1 = getTileByName(tile.Layer3Filename).bitmap;
                                }
                                if (lyr1 != null)
                                {
                                    float scalerX = lyr1.Width / 100;
                                    float scalerY = lyr1.Height / 100;
                                    Rectangle src = new Rectangle(0, 0, lyr1.Width, lyr1.Height);
                                    Rectangle dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr1, dst, src, GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region Draw Layer 4
                if (checkBox4.Checked)
                {
                    for (int y = 0; y < area.MapSizeY; y++)
                    {
                        for (int x = 0; x < area.MapSizeX; x++)
                        {
                            if ((refreshAll) || (currentSquareClicked == new Point(x, y)) || (lastSquareClicked == new Point(x, y)))
                            {
                                Tile tile = area.Tiles[y * area.MapSizeX + x];
                                Bitmap lyr1 = null;
                                if (getTileByName(tile.Layer4Filename) != null)
                                {
                                    lyr1 = getTileByName(tile.Layer4Filename).bitmap;
                                }
                                if (lyr1 != null)
                                {
                                    float scalerX = lyr1.Width / 100;
                                    float scalerY = lyr1.Height / 100;
                                    Rectangle src = new Rectangle(0, 0, lyr1.Width, lyr1.Height);
                                    Rectangle dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr1, dst, src, GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }
                }
                #endregion
                #region Draw Layer 5
                if (checkBox5.Checked)
                {
                    for (int y = 0; y < area.MapSizeY; y++)
                    {
                        for (int x = 0; x < area.MapSizeX; x++)
                        {
                            if ((refreshAll) || (currentSquareClicked == new Point(x, y)) || (lastSquareClicked == new Point(x, y)))
                            {
                                Tile tile = area.Tiles[y * area.MapSizeX + x];
                                Bitmap lyr1 = null;
                                if (getTileByName(tile.Layer5Filename) != null)
                                {
                                    lyr1 = getTileByName(tile.Layer5Filename).bitmap;
                                }
                                if (lyr1 != null)
                                {
                                    float scalerX = lyr1.Width / 100;
                                    float scalerY = lyr1.Height / 100;
                                    Rectangle src = new Rectangle(0, 0, lyr1.Width, lyr1.Height);
                                    Rectangle dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr1, dst, src, GraphicsUnit.Pixel);
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Draw Grid
                for (int y = 0; y < area.MapSizeY; y++)
                {
                    for (int x = 0; x < area.MapSizeX; x++)
                    {
                        if ((refreshAll) || (currentSquareClicked == new Point(x, y)) || (lastSquareClicked == new Point(x, y)))
                        {
                            Tile tile = area.Tiles[y * area.MapSizeX + x];
                            //draw square walkmesh and LoS stuff
                            Rectangle src = new Rectangle(0, 0, g_walkPass.Width, g_walkPass.Height);
                            Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
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
                #endregion
                #region Old stuff for reference (to be deleted later once fully tested)
                /*for (int y = 0; y < area.MapSizeY; y++)
                {
                    for (int x = 0; x < area.MapSizeX; x++)
                    {
                        if ((refreshAll) || (currentSquareClicked == new Point(x, y)) || (lastSquareClicked == new Point(x, y)))
                        {
                            Tile tile = area.Tiles[y * area.MapSizeX + x];
                            Bitmap lyr1 = null;
                            Bitmap lyr2 = null;
                            Bitmap lyr3 = null;
                            Bitmap lyr4 = null;
                            Bitmap lyr5 = null;
                            Rectangle src1 = new Rectangle(0, 0, 100, 100);
                            Rectangle src2 = new Rectangle(0, 0, 100, 100);
                            Rectangle src3 = new Rectangle(0, 0, 100, 100);
                            Rectangle src4 = new Rectangle(0, 0, 100, 100);
                            Rectangle src5 = new Rectangle(0, 0, 100, 100);
                            if (getTileByName(tile.Layer1Filename) != null)
                            {
                                lyr1 = getTileByName(tile.Layer1Filename).bitmap;
                                //lyr1 = (Bitmap)getTileByName(tile.Layer1Filename).bitmap.Clone();
                                //flip about y-axis layer
                                //lyr1 = Flip(lyr1, tile.Layer1Flip);
                                //rotate layer
                                //lyr1 = Rotate(lyr1, tile.Layer1Rotate);
                                //src1 = new Rectangle(0, 0, lyr1.Width, lyr1.Height);
                            }
                            if (getTileByName(tile.Layer2Filename) != null)
                            {
                                lyr2 = getTileByName(tile.Layer2Filename).bitmap;
                                //flip about y-axis layer
                                //lyr2 = Flip(lyr2, tile.Layer2Flip);
                                //rotate layer
                                //lyr2 = Rotate(lyr2, tile.Layer2Rotate);
                                //src2 = new Rectangle(0, 0, lyr2.Width, lyr2.Height);
                            }
                            if (getTileByName(tile.Layer3Filename) != null)
                            {
                                lyr3 = getTileByName(tile.Layer3Filename).bitmap;
                                //flip about y-axis layer
                                //lyr3 = Flip(lyr3, tile.Layer3Flip);
                                //rotate layer
                                //lyr3 = Rotate(lyr3, tile.Layer3Rotate);
                                //src3 = new Rectangle(0, 0, lyr3.Width, lyr3.Height);
                            }
                            if (getTileByName(tile.Layer4Filename) != null)
                            {
                                lyr4 = getTileByName(tile.Layer4Filename).bitmap;
                                //flip about y-axis layer
                                //lyr4 = Flip(lyr4, tile.Layer4Flip);
                                //rotate layer
                                //lyr4 = Rotate(lyr4, tile.Layer4Rotate);
                                //src4 = new Rectangle(0, 0, lyr4.Width, lyr4.Height);
                            }
                            if (getTileByName(tile.Layer5Filename) != null)
                            {
                                lyr5 = getTileByName(tile.Layer5Filename).bitmap;
                                //flip about y-axis layer
                                //lyr5 = Flip(lyr5, tile.Layer5Flip);
                                //rotate layer
                                //lyr5 = Rotate(lyr5, tile.Layer5Rotate);
                                //src5 = new Rectangle(0, 0, lyr5.Width, lyr5.Height);
                            }

                            Rectangle src = new Rectangle(0, 0, 100, 100);
                            Rectangle dst = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                            //draw layer 1 first
                            if (checkBox1.Checked)
                            {
                                if (lyr1 != null)
                                {
                                    float scalerX = lyr1.Width / 100;
                                    float scalerY = lyr1.Height / 100;
                                    src = new Rectangle(0, 0, lyr1.Width, lyr1.Height);
                                    dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr1, dst, src, GraphicsUnit.Pixel);
                                }
                            }
                            //draw layer 2
                            if (checkBox2.Checked)
                            {
                                if (lyr2 != null)
                                {
                                    float scalerX = lyr2.Width / 100;
                                    float scalerY = lyr2.Height / 100;
                                    src = new Rectangle(0, 0, lyr2.Width, lyr2.Height);
                                    dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr2, dst, src, GraphicsUnit.Pixel);
                                }
                            }
                            //draw layer 3
                            if (checkBox3.Checked)
                            {
                                if (lyr3 != null)
                                {
                                    float scalerX = lyr3.Width / 100;
                                    float scalerY = lyr3.Height / 100;
                                    src = new Rectangle(0, 0, lyr3.Width, lyr3.Height);
                                    dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr3, dst, src, GraphicsUnit.Pixel);
                                }
                            }
                            //draw layer 4
                            if (checkBox4.Checked)
                            {
                                if (lyr4 != null)
                                {
                                    float scalerX = lyr4.Width / 100;
                                    float scalerY = lyr4.Height / 100;
                                    src = new Rectangle(0, 0, lyr4.Width, lyr4.Height);
                                    dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr4, dst, src, GraphicsUnit.Pixel);
                                }
                            }
                            //draw layer 5
                            if (checkBox5.Checked)
                            {
                                if (lyr5 != null)
                                {
                                    float scalerX = lyr5.Width / 100;
                                    float scalerY = lyr5.Height / 100;
                                    src = new Rectangle(0, 0, lyr5.Width, lyr5.Height);
                                    dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr5, dst, src, GraphicsUnit.Pixel);
                                }
                            }
                            //draw square walkmesh and LoS stuff
                            src = new Rectangle(0, 0, g_walkPass.Width, g_walkPass.Height);
                            Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
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
                }*/
            /*//GDI #endregion

                        int cnt = 0;
                            foreach (Prop prpRef in area.Props)
                            {
                                int cspx = prpRef.LocationX;
                                int cspy = prpRef.LocationY;
                                if ((refreshAll) || (currentSquareClicked == new Point(cspx, cspy)) || (lastSquareClicked == new Point(cspx, cspy)))
                                {
                                    spritePropDraw(cspx, cspy, cnt);
                                }
                                cnt++;
                            }
                            foreach (Trigger t in area.Triggers)
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
                            }
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
                    private void drawTileSettings()
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
        }
        public void drawSelectionBox(int gridx, int gridy)
        {
            //GDI int dx = gridx * sqr;
            //GDI int dy = gridy * sqr;
            //draw selection box around tile                
            //GDI Pen pen = new Pen(Color.DarkMagenta, 2);
            //GDI Rectangle rect = new Rectangle(dx + 1, dy + 1, sqr - 2, sqr - 2);
            //GDI device.DrawRectangle(pen, rect);
            //save changes
            //GDI UpdatePB();            
        }
        public void UpdatePB()
        {
            //this.Cursor = Cursors.Default;
            //GDI panelView.BackgroundImage = surface;
            //GDI panelView.Invalidate();
        }

        private void panelView_MouseMove(object sender, MouseEventArgs e)
        {
            gridX = e.X / sqr;
            gridY = e.Y / sqr;
            if (!mouseInMapArea(gridX, gridY)) { return; }
            lblMouseInfo.Text = "gridX = " + gridX.ToString() + " : gridY = " + gridY.ToString();
            panelView.Focus();
            if (prntForm.PropSelected)
            {
                // TODO re-implement continuous drawing of props once converted to use Direct2D
                /*refreshMap(true);
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
                */
            }
            else if (currentPoint != new Point(gridX, gridY))
            {
                //if painting tiles or walkable or Line-of-sight squares, allow multiple square painting if left mouse button down and move
                if (prntForm.CreatureSelected)
                {
                    return; //don't allow painting multiple creatures by mouse down and move
                }
                if (e.Button == MouseButtons.Left)
                {
                    //if painting tiles or walkable or Line-of-sight squares, allow multiple square painting if LEFT mouse button down and move
                    if ((rbtnPaintTile.Checked) || (rbtnWalkable.Checked) || (rbtnLoS.Checked) || (rbtnChangeLinkState.Checked))
                    {
                        clickDrawArea(e);
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    //if painting walkable or Line-of-sight squares, allow multiple square painting if RIGHT mouse button down and move
                    if ((rbtnWalkable.Checked) || (rbtnLoS.Checked) || (rbtnChangeLinkState.Checked))
                    {
                        clickDrawArea(e);
                    }
                }
            }//GDI             
        }
        private void panelView_MouseEnter(object sender, EventArgs e)
        {
            panelView.Select();
            try
            {
                if (prntForm.selectedLevelMapPropTag != "")
                {
                    prntForm.PropSelected = true;
                }
                if (prntForm.PropSelected)
                {
                    string selectedProp = prntForm.selectedLevelMapPropTag;
                    le_selectedProp = prntForm.getPropByTag(selectedProp);
                    if (le_selectedProp != null)
                    {
                        selectedBitmap = le_selectedProp.propBitmap;
                        selectedBitmapFilename = le_selectedProp.ImageFileName;
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
        public bool mouseInMapArea(int gridX, int gridY)
        {
            if (gridX < 0) { return false; }
            if (gridY < 0) { return false; }
            if (gridX > area.MapSizeX - 1) { return false; }
            if (gridY > area.MapSizeY - 1) { return false; }
            return true;
        }
        private void clickDrawArea(MouseEventArgs e)
        {
            gridX = e.X / sqr;
            gridY = e.Y / sqr;
            lastSquareClicked.X = currentSquareClicked.X;
            lastSquareClicked.Y = currentSquareClicked.Y;
            currentSquareClicked.X = gridX;
            currentSquareClicked.Y = gridY;
            if (!mouseInMapArea(gridX, gridY))
            {
                return;
            }
            switch (e.Button)
            {
                #region Left Button
                case MouseButtons.Left:
                    refreshLeftPanelInfo();
                    prntForm.currentSelectedTrigger = null;
                    #region Tile Selected
                    if (rbtnPaintTile.Checked)
                    {
                        if (currentTileFilename == "")
                        {
                            if (radioButton1.Checked)
                            {
                                currentTileFilename = area.Tiles[selectedTile.index].Layer1Filename;
                            }
                            if (radioButton2.Checked)
                            {
                                currentTileFilename = area.Tiles[selectedTile.index].Layer2Filename;
                            }
                            if (radioButton3.Checked)
                            {
                                currentTileFilename = area.Tiles[selectedTile.index].Layer3Filename;
                            }
                            if (radioButton4.Checked)
                            {
                                currentTileFilename = area.Tiles[selectedTile.index].Layer4Filename;
                            }
                            if (radioButton5.Checked)
                            {
                                currentTileFilename = area.Tiles[selectedTile.index].Layer5Filename;
                            }
                        }

                        else
                        {
                            //gridX = e.X / sqr;
                            //gridY = e.Y / sqr;
                            selectedTile.index = gridY * area.MapSizeX + gridX;
                            prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                            prntForm.logText(Environment.NewLine);
                            #region Layer 1
                            if (radioButton1.Checked)
                            {
                                area.Tiles[selectedTile.index].Layer1Filename = currentTileFilename;
                                area.Tiles[selectedTile.index].Layer1Rotate = tileToBePlaced.angle;
                                area.Tiles[selectedTile.index].Layer1Mirror = tileToBePlaced.mirror;
                                area.Tiles[selectedTile.index].Layer1Xshift = tileToBePlaced.xshift;
                                area.Tiles[selectedTile.index].Layer1Yshift = tileToBePlaced.yshift;
                                area.Tiles[selectedTile.index].Layer1Xscale = tileToBePlaced.xscale;
                                area.Tiles[selectedTile.index].Layer1Yscale = tileToBePlaced.yscale;
                                area.Tiles[selectedTile.index].Layer1Opacity = tileToBePlaced.opacity;
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
                                            area.Tiles[y * area.MapSizeX + x].Layer1Filename = currentTileFilename;
                                            area.Tiles[y * area.MapSizeX + x].Layer1Rotate = tileToBePlaced.angle;
                                            area.Tiles[y * area.MapSizeX + x].Layer1Mirror = tileToBePlaced.mirror;
                                            area.Tiles[y * area.MapSizeX + x].Layer1Xshift = tileToBePlaced.xshift;
                                            area.Tiles[y * area.MapSizeX + x].Layer1Yshift = tileToBePlaced.yshift;
                                            area.Tiles[y * area.MapSizeX + x].Layer1Xscale = tileToBePlaced.xscale;
                                            area.Tiles[y * area.MapSizeX + x].Layer1Yscale = tileToBePlaced.yscale;
                                            area.Tiles[y * area.MapSizeX + x].Layer1Opacity = tileToBePlaced.opacity;
                                            currentSquareClicked = new Point(x, y);
                                            //GDI refreshMap(false);
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region Layer 2
                            else if (radioButton2.Checked)
                            {
                                area.Tiles[selectedTile.index].Layer2Filename = currentTileFilename;
                                area.Tiles[selectedTile.index].Layer2Rotate = tileToBePlaced.angle;
                                area.Tiles[selectedTile.index].Layer2Mirror = tileToBePlaced.mirror;
                                area.Tiles[selectedTile.index].Layer2Xshift = tileToBePlaced.xshift;
                                area.Tiles[selectedTile.index].Layer2Yshift = tileToBePlaced.yshift;
                                area.Tiles[selectedTile.index].Layer2Xscale = tileToBePlaced.xscale;
                                area.Tiles[selectedTile.index].Layer2Yscale = tileToBePlaced.yscale;
                                area.Tiles[selectedTile.index].Layer2Opacity = tileToBePlaced.opacity;
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
                                            area.Tiles[y * area.MapSizeX + x].Layer2Filename = currentTileFilename;
                                            area.Tiles[y * area.MapSizeX + x].Layer2Rotate = tileToBePlaced.angle;
                                            area.Tiles[y * area.MapSizeX + x].Layer2Mirror = tileToBePlaced.mirror;
                                            area.Tiles[y * area.MapSizeX + x].Layer2Xshift = tileToBePlaced.xshift;
                                            area.Tiles[y * area.MapSizeX + x].Layer2Yshift = tileToBePlaced.yshift;
                                            area.Tiles[y * area.MapSizeX + x].Layer2Xscale = tileToBePlaced.xscale;
                                            area.Tiles[y * area.MapSizeX + x].Layer2Yscale = tileToBePlaced.yscale;
                                            area.Tiles[y * area.MapSizeX + x].Layer2Opacity = tileToBePlaced.opacity;
                                            currentSquareClicked = new Point(x, y);
                                            //GDI refreshMap(false);
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region Layer 3
                            else if (radioButton3.Checked)
                            {
                                area.Tiles[selectedTile.index].Layer3Filename = currentTileFilename;
                                area.Tiles[selectedTile.index].Layer3Rotate = tileToBePlaced.angle;
                                area.Tiles[selectedTile.index].Layer3Mirror = tileToBePlaced.mirror;
                                area.Tiles[selectedTile.index].Layer3Xshift = tileToBePlaced.xshift;
                                area.Tiles[selectedTile.index].Layer3Yshift = tileToBePlaced.yshift;
                                area.Tiles[selectedTile.index].Layer3Xscale = tileToBePlaced.xscale;
                                area.Tiles[selectedTile.index].Layer3Yscale = tileToBePlaced.yscale;
                                area.Tiles[selectedTile.index].Layer3Opacity = tileToBePlaced.opacity;
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
                                            area.Tiles[y * area.MapSizeX + x].Layer3Filename = currentTileFilename;
                                            area.Tiles[y * area.MapSizeX + x].Layer3Rotate = tileToBePlaced.angle;
                                            area.Tiles[y * area.MapSizeX + x].Layer3Mirror = tileToBePlaced.mirror;
                                            area.Tiles[y * area.MapSizeX + x].Layer3Xshift = tileToBePlaced.xshift;
                                            area.Tiles[y * area.MapSizeX + x].Layer3Yshift = tileToBePlaced.yshift;
                                            area.Tiles[y * area.MapSizeX + x].Layer3Xscale = tileToBePlaced.xscale;
                                            area.Tiles[y * area.MapSizeX + x].Layer3Yscale = tileToBePlaced.yscale;
                                            area.Tiles[y * area.MapSizeX + x].Layer3Opacity = tileToBePlaced.opacity;
                                            currentSquareClicked = new Point(x, y);
                                            //GDI refreshMap(false);
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region Layer 4
                            else if (radioButton4.Checked)
                            {
                                area.Tiles[selectedTile.index].Layer4Filename = currentTileFilename;
                                area.Tiles[selectedTile.index].Layer4Rotate = tileToBePlaced.angle;
                                area.Tiles[selectedTile.index].Layer4Mirror = tileToBePlaced.mirror;
                                area.Tiles[selectedTile.index].Layer4Xshift = tileToBePlaced.xshift;
                                area.Tiles[selectedTile.index].Layer4Yshift = tileToBePlaced.yshift;
                                area.Tiles[selectedTile.index].Layer4Xscale = tileToBePlaced.xscale;
                                area.Tiles[selectedTile.index].Layer4Yscale = tileToBePlaced.yscale;
                                area.Tiles[selectedTile.index].Layer4Opacity = tileToBePlaced.opacity;
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
                                            area.Tiles[y * area.MapSizeX + x].Layer4Filename = currentTileFilename;
                                            area.Tiles[y * area.MapSizeX + x].Layer4Rotate = tileToBePlaced.angle;
                                            area.Tiles[y * area.MapSizeX + x].Layer4Mirror = tileToBePlaced.mirror;
                                            area.Tiles[y * area.MapSizeX + x].Layer4Xshift = tileToBePlaced.xshift;
                                            area.Tiles[y * area.MapSizeX + x].Layer4Yshift = tileToBePlaced.yshift;
                                            area.Tiles[y * area.MapSizeX + x].Layer4Xscale = tileToBePlaced.xscale;
                                            area.Tiles[y * area.MapSizeX + x].Layer4Yscale = tileToBePlaced.yscale;
                                            area.Tiles[y * area.MapSizeX + x].Layer4Opacity = tileToBePlaced.opacity;
                                            currentSquareClicked = new Point(x, y);
                                            //GDI refreshMap(false);
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region Layer 5
                            else if (radioButton5.Checked)
                            {
                                area.Tiles[selectedTile.index].Layer5Filename = currentTileFilename;
                                area.Tiles[selectedTile.index].Layer5Rotate = tileToBePlaced.angle;
                                area.Tiles[selectedTile.index].Layer5Mirror = tileToBePlaced.mirror;
                                area.Tiles[selectedTile.index].Layer5Xshift = tileToBePlaced.xshift;
                                area.Tiles[selectedTile.index].Layer5Yshift = tileToBePlaced.yshift;
                                area.Tiles[selectedTile.index].Layer5Xscale = tileToBePlaced.xscale;
                                area.Tiles[selectedTile.index].Layer5Yscale = tileToBePlaced.yscale;
                                area.Tiles[selectedTile.index].Layer5Opacity = tileToBePlaced.opacity;
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
                                            area.Tiles[y * area.MapSizeX + x].Layer5Filename = currentTileFilename;
                                            area.Tiles[y * area.MapSizeX + x].Layer5Rotate = tileToBePlaced.angle;
                                            area.Tiles[y * area.MapSizeX + x].Layer5Mirror = tileToBePlaced.mirror;
                                            area.Tiles[y * area.MapSizeX + x].Layer5Xshift = tileToBePlaced.xshift;
                                            area.Tiles[y * area.MapSizeX + x].Layer5Yshift = tileToBePlaced.yshift;
                                            area.Tiles[y * area.MapSizeX + x].Layer5Xscale = tileToBePlaced.xscale;
                                            area.Tiles[y * area.MapSizeX + x].Layer5Yscale = tileToBePlaced.yscale;
                                            area.Tiles[y * area.MapSizeX + x].Layer5Opacity = tileToBePlaced.opacity;
                                            currentSquareClicked = new Point(x, y);
                                            //GDI refreshMap(false);
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                        //GDI refreshMap(false);
                    }
                    #endregion
                    #region Prop Selected
                    else if (prntForm.PropSelected)
                    {
                        string selectedProp = prntForm.selectedLevelMapPropTag;
                        prntForm.logText(selectedProp);
                        prntForm.logText(Environment.NewLine);

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
                        area.Props.Add(newProp);

                    }
                    #endregion
                    #region Paint New Trigger Selected
                    else if (rbtnPaintTrigger.Checked)
                    {
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
                            //patrick4
                            if (!exists) //doesn't exist so is a new point, add to list
                            {
                                Coordinate newCoor = new Coordinate();
                                newCoor.X = newPoint.X;
                                newCoor.Y = newPoint.Y;
                                newTrigger.TriggerSquaresList.Add(newCoor);
                            }
                            prntForm.currentSelectedTrigger = newTrigger;
                            prntForm.frmTriggerEvents.refreshTriggers();
                        }
                        catch
                        {
                            MessageBox.Show("The tag of the selected Trigger was not found in the area's trigger list");
                        }
                        //update the map to show colored squares    
                        //GDI refreshMap(false);
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

                            //gridX = e.X / sqr;
                            //gridY = e.Y / sqr;

                            prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                            prntForm.logText(Environment.NewLine);
                            Point newPoint = new Point(gridX, gridY);
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
                            //GDI refreshMap(false);
                        }
                    }
                    #endregion
                    #region Walkmesh Toggle Selected (Make Non-Walkable)
                    //height level system:
                    else if (rbtnHeightLevel.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        if (!isOrthogonalNeighbourOfBridge(gridX, gridY) && !area.Tiles[gridY * area.MapSizeX + gridX].isEWBridge && !area.Tiles[gridY * area.MapSizeX + gridX].isNSBridge)
                        {
                            if (area.allowLevelDesignWithMoreThan2HeightLevelsDifference)
                            {
                                area.Tiles[selectedTile.index].heightLevel += 1;
                            }
                            else
                            {
                                bool changeAllowed = true;
                                try
                                {
                                    for (int xCheck = gridX - 1; xCheck <= gridX + 1; xCheck++)
                                    {
                                        for (int yCheck = gridY - 1; yCheck <= gridY + 1; yCheck++)
                                        {
                                            /*
                                            //the checked tile is downstairs east ramp that...
                                            if (area.Tiles[yCheck * area.MapSizeX + xCheck].hasDownStairShadowE)
                                            {
                                                //... is located north of current tile 
                                                if (yCheck == gridY -1 && xCheck == gridX)
                                                {//normal 2-level rule

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel < area.Tiles[selectedTile.index].heightLevel - 1)
                                                    {
                                                        changeAllowed = false;
                                                        break;
                                                    }
                                                }
                                                //... is located south of current tile 
                                                if (yCheck == gridY + 1 && xCheck == gridX)
                                                {//normal 2-level rule

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel < area.Tiles[selectedTile.index].heightLevel - 1)
                                                    {
                                                        changeAllowed = false;
                                                        break;
                                                    }
                                                }
                                                //... is located east of current tile 
                                                if (yCheck == gridY && xCheck == gridX + 1)
                                                {//current tile must be one level lower than checked tile, ie current tile is ramp bottom

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel != area.Tiles[selectedTile.index].heightLevel + 2)
                                                    {
                                                        changeAllowed = false;
                                                        break;
                                                    }
                                                }
                                                //... is located west of current tile
                                                if (yCheck == gridY && xCheck == gridX - 1)
                                                {//current tile must be same level as checked tile, ie current tile is ramp top

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel != area.Tiles[selectedTile.index].heightLevel + 1)
                                                    {
                                                        if ((area.Tiles[selectedTile.index].hasDownStairShadowE) && (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel == area.Tiles[selectedTile.index].heightLevel))
                                                        {
                                                            ///allow connected ramps in same direction
                                                        }
                                                        else
                                                        {
                                                            changeAllowed = false;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }

                                            //the checked tile is downstairs south ramp that...
                                            else if (area.Tiles[yCheck * area.MapSizeX + xCheck].hasDownStairShadowS)
                                            {
                                                //... is located west of current tile 
                                                if (yCheck == gridY && xCheck == gridX - 1)
                                                {//normal 2-level rule

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel < area.Tiles[selectedTile.index].heightLevel - 1)
                                                    {
                                                        changeAllowed = false;
                                                        break;
                                                    }
                                                }
                                                //... is located east of current tile 
                                                if (yCheck == gridY  && xCheck == gridX + 1)
                                                {//normal 2-level rule

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel < area.Tiles[selectedTile.index].heightLevel - 1)
                                                    {
                                                        changeAllowed = false;
                                                        break;
                                                    }
                                                }
                                                //... is located south of current tile 
                                                if (yCheck == gridY + 1 && xCheck == gridX)
                                                {//current tile must be one level lower than checked tile, ie current tile is ramp bottom

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel != area.Tiles[selectedTile.index].heightLevel + 2)
                                                    {
                                                        changeAllowed = false;
                                                        break;
                                                    }
                                                }
                                                //... is located north of current tile
                                                if (yCheck == gridY - 1 && xCheck == gridX)
                                                {//current tile must be same level as checked tile, ie current tile is ramp top

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel != area.Tiles[selectedTile.index].heightLevel + 1)
                                                    {
                                                        if ((area.Tiles[selectedTile.index].hasDownStairShadowS) && (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel == area.Tiles[selectedTile.index].heightLevel))
                                                        {
                                                            ///allow connected ramps in same direction
                                                        }
                                                        else
                                                        {
                                                            changeAllowed = false;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }

                                            //the checked tile is downstairs north ramp that...
                                            else if (area.Tiles[yCheck * area.MapSizeX + xCheck].hasDownStairShadowN)
                                            {
                                                //... is located west of current tile 
                                                if (yCheck == gridY && xCheck == gridX - 1)
                                                {//normal 2-level rule

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel < area.Tiles[selectedTile.index].heightLevel - 1)
                                                    {
                                                        changeAllowed = false;
                                                        break;
                                                    }
                                                }
                                                //... is located east of current tile 
                                                if (yCheck == gridY && xCheck == gridX + 1)
                                                {//normal 2-level rule

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel < area.Tiles[selectedTile.index].heightLevel - 1)
                                                    {
                                                        changeAllowed = false;
                                                        break;
                                                    }
                                                }
                                                //... is located south of current tile 
                                                if (yCheck == gridY + 1 && xCheck == gridX)
                                                {//current tile must be one level lower than checked tile, ie current tile is ramp bottom

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel != area.Tiles[selectedTile.index].heightLevel + 2)
                                                    {
                                                        changeAllowed = false;
                                                        break;
                                                    }
                                                }
                                                //... is located north of current tile
                                                if (yCheck == gridY - 1 && xCheck == gridX)
                                                {//current tile must be same level as checked tile, ie current tile is ramp top

                                                    if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel != area.Tiles[selectedTile.index].heightLevel + 1)
                                                    {
                                                        if ((area.Tiles[selectedTile.index].hasDownStairShadowS) && (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel == area.Tiles[selectedTile.index].heightLevel))
                                                        {
                                                            ///allow connected ramps in same direction
                                                        }
                                                        else
                                                        {
                                                            changeAllowed = false;
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                            */

                                            //the checked tile is a normal square
                                            if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel < area.Tiles[selectedTile.index].heightLevel - 1)
                                            {
                                                //restriction does not apply to diagonal squares
                                                if (((yCheck == gridY) && (xCheck != gridX)) || ((yCheck != gridY) && (xCheck == gridX)))
                                                {
                                                    changeAllowed = false;
                                                    break;
                                                }
                                            }

                                        }
                                    }
                                }
                                catch
                                {

                                }
                                if (changeAllowed)
                                {
                                    area.Tiles[selectedTile.index].heightLevel += 1;
                                }
                            }
                        }
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    else if (rbtnBridgeEW.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        //area.Tiles[selectedTile.index].isEWBridge = true;
                        if (!isOrthogonalNeighbourOfBridge(gridX, gridY) && !isDiagonalNeighbourOfBridge(gridX, gridY))
                        {
                            if ((gridY > 0) && (gridY < (area.MapSizeY - 1)) && (gridX > 0) && (gridY < (area.MapSizeX - 1)))
                            {
                                rotateBridge();
                            }
                        }
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    /*
                    else if (rbtnBridgeNS.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].isNSBridge = true;
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    */
                    else if (rbtnDownToN.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        if (!isOrthogonalNeighbourOfBridge(gridX, gridY) && !area.Tiles[gridY * area.MapSizeX + gridX].isEWBridge && !area.Tiles[gridY * area.MapSizeX + gridX].isNSBridge)
                        {
                            area.Tiles[selectedTile.index].isRamp = true;
                            //area.Tiles[selectedTile.index].hasDownStairShadowN = true;
                            rotateStair();
                        }
                        /*
                        if (!area.allowFreePlacementOfRamps)
                        {
                            bool allowPlacement = true;
                            if (area.Tiles[(gridY-1) * area.MapSizeX + gridX].heightLevel + 1 != area.Tiles[selectedTile.index].heightLevel)
                            {
                                allowPlacement = false;
                            }
                            if (area.Tiles[(gridY + 1) * area.MapSizeX + gridX].heightLevel != area.Tiles[selectedTile.index].heightLevel)
                            {
                                if (!area.Tiles[(gridY + 1) * area.MapSizeX + gridX].hasDownStairShadowN)
                                {
                                    allowPlacement = false;
                                }
                            }

                            if (allowPlacement)
                            {
                                area.Tiles[selectedTile.index].isRamp = true;
                                area.Tiles[selectedTile.index].hasDownStairShadowN = true;
                            }
                        }

                        else
                        {
                            area.Tiles[selectedTile.index].isRamp = true;
                            area.Tiles[selectedTile.index].hasDownStairShadowN = true;
                        }
                        */
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    /*
                    else if (rbtnDownToE.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].isRamp = true;
                        rotateStair();
                        //firstElementHasBeenPlaced = true;
                        /*
                        if (!area.Tiles[selectedTile.index].hasDownStairShadowE)
                        {
                            area.Tiles[selectedTile.index].hasDownStairShadowE = true;
                            area.Tiles[selectedTile.index].hasDownStairShadowS = false;
                            area.Tiles[selectedTile.index].hasDownStairShadowW = false;
                            area.Tiles[selectedTile.index].hasDownStairShadowN = false;
                        }
                        else
                             if (!area.Tiles[selectedTile.index].hasDownStairShadowS)
                        {
                            area.Tiles[selectedTile.index].hasDownStairShadowE = false;
                            area.Tiles[selectedTile.index].hasDownStairShadowS = true;
                            area.Tiles[selectedTile.index].hasDownStairShadowW = false;
                            area.Tiles[selectedTile.index].hasDownStairShadowN = false;
                        }
                        else
                             if (!area.Tiles[selectedTile.index].hasDownStairShadowW)
                        {
                            area.Tiles[selectedTile.index].hasDownStairShadowE = false;
                            area.Tiles[selectedTile.index].hasDownStairShadowS = false;
                            area.Tiles[selectedTile.index].hasDownStairShadowW = true;
                            area.Tiles[selectedTile.index].hasDownStairShadowN = false;
                        }
                        else
                             if (!area.Tiles[selectedTile.index].hasDownStairShadowN)
                        {
                            area.Tiles[selectedTile.index].hasDownStairShadowE = false;
                            area.Tiles[selectedTile.index].hasDownStairShadowS = false;
                            area.Tiles[selectedTile.index].hasDownStairShadowW = false;
                            area.Tiles[selectedTile.index].hasDownStairShadowN = true;
                        }
                       

                            //area.Tiles[selectedTile.index].hasDownStairShadowE = true;
                            calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    else if (rbtnDownToS.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].isRamp = true;
                        //area.Tiles[selectedTile.index].hasDownStairShadowS = true;
                        rotateStair();
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    else if (rbtnDownToW.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].isRamp = true;
                        //area.Tiles[selectedTile.index].hasDownStairShadowW = true;
                        rotateStair();
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    */
                    else if (rbtnWalkable.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].Walkable = false;
                        //GDI refreshMap(false);
                    }

                    else if (rbtnToMaster.Checked)
                    {
                        if (area.masterOfThisArea != "none")
                        {
                            /*
                             else if (rbtnPaintTrigger.Checked)
                        {
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
                                prntForm.currentSelectedTrigger = newTrigger;
                                prntForm.frmTriggerEvents.refreshTriggers();
                            }
                            catch
                            {
                                MessageBox.Show("The tag of the selected Trigger was not found in the area's trigger list");
                            }
                            //update the map to show colored squares    
                            //GDI refreshMap(false);
                        }
                            */
                            selectedTile.index = gridY * area.MapSizeX + gridX;

                            //if selected square already contains a link to master, then switch prntForm.selectedLevelMapTriggerTag) to that one
                            /*
                            //bool blockCreation
                            foreach (Trigger newTrigger in area.Triggers)
                            {

                                foreach (Trigger newTrigger in area.Triggers)
                                {
                                    if (newTrigger.TriggerTag == prntForm.selectedLevelMapTriggerTag)
                                    {

                                        rotateTransitionToMaster();
                                        */
                            foreach (Trigger existingTrigger in area.Triggers)
                            {
                                if (existingTrigger.TriggerSquaresList.Count > 0)
                                {
                                    if (existingTrigger.TriggerSquaresList[0].X == gridX && existingTrigger.TriggerSquaresList[0].Y == gridY)
                                    {
                                        if (existingTrigger.isLinkToMaster)
                                        {
                                            prntForm.selectedLevelMapTriggerTag = existingTrigger.TriggerTag;
                                        }
                                    }
                                }
                            }

                            rotateTransitionToMaster();

                            prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                            prntForm.logText(Environment.NewLine);

                            //rotation satet will do belwo 2-steps in 4 variants
                            //patrick3
                            //create a new trigger object on linked map (ie current map)
                            //Trigger newTrigger = new Trigger();
                            //newTrigger.TriggerTag = "newTrigger_" + prntForm.mod.nextIdNumber;
                            //areaOrg.Triggers.Add(newTriggerMaster);
                            //prntForm.selectedLevelMapTriggerTag = newTrigger.TriggerTag;
                            

                            foreach (Trigger newTrigger in area.Triggers)
                            {
                                if (newTrigger.TriggerTag == prntForm.selectedLevelMapTriggerTag)
                                {
                                    newTrigger.transitionToMasterRotationCounter = transitionToMasterRotationCounter;
                                    newTrigger.isLinkToMaster = true;
                                    newTrigger.tagOfLink = area.Filename;
                                    newTrigger.tagOfLinkedMaster = area.masterOfThisArea;
                                    newTrigger.Enabled = true;
                                    newTrigger.EnabledEvent1 = true;
                                    newTrigger.Event1Type = "transition";
                                    newTrigger.Event1FilenameOrTag = area.masterOfThisArea;
                                    newTrigger.Event1TransPointX = gridX;
                                    newTrigger.Event1TransPointY = gridY;
                                    Coordinate newCoor = new Coordinate();
                                    newCoor.X = gridX;
                                    newCoor.Y = gridY;
                                    if (newTrigger.TriggerSquaresList.Count <= 1)
                                    {
                                        if (newTrigger.TriggerSquaresList.Count == 1)
                                        {
                                            newTrigger.TriggerSquaresList.RemoveAt(0);
                                        }
                                        newTrigger.TriggerSquaresList.Add(newCoor);
                                    }
                                }
                            }

                            Area areaOrg = new Area();
                            bool orgIsStillOpen = false;
                            for (int a = 0; a < prntForm.openAreasList.Count; a++)
                            {
                                if (prntForm.openAreasList[a].Filename == area.masterOfThisArea)
                                {
                                    areaOrg = prntForm.openAreasList[a];
                                    orgIsStillOpen = true;
                                    break;
                                }
                            }

                            if (orgIsStillOpen)
                            {

                                //string g_dir = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas";
                                //if (area.masterOfThisArea != "none")
                                //{
                                //areaOrg = areaOrg.loadAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                                //}

                                //example for entrance ligths on link pointing towards east (placed on west side of their square): 
                                //Link:    A(x-1,y), DE(x,y)
                                //Master:  D(x-1,y), AE(x,y)

                                //example for entrance ligths on link pointing towards south (placed on north side of their square): 
                                //Link:    A(x,y-1), DE(x,y)
                                //Master:  D(x,y-1), AE(x,y)

                                //example for entrance ligths on link pointing towards west (placed on east side of their square): 
                                //Link:    A(x+1,y), DE(x,y)
                                //Master:  D(x+1,y), AE(x,y)

                                //example for entrance ligths on link pointing towards north (placed on south side of their square): 
                                //Link:    A(x,y+1), DE(x,y)
                                //Master:  D(x,y+1), AE(x,y)

                                //pointing towars north
                                if (transitionToMasterRotationCounter == 1)
                                {
                                    foreach (Trigger newTrigger in areaOrg.Triggers)
                                    {
                                        if (newTrigger.TriggerTag == (prntForm.selectedLevelMapTriggerTag + "_" + area.masterOfThisArea))
                                        {
                                            //patrick100
                                            newTrigger.isLinkToMaster = true;
                                            newTrigger.tagOfLink = area.Filename;
                                            newTrigger.tagOfLinkedMaster = area.masterOfThisArea;
                                            newTrigger.Enabled = true;
                                            newTrigger.EnabledEvent1 = true;
                                            newTrigger.Event1Type = "transition";
                                            newTrigger.Event1FilenameOrTag = area.Filename;
                                            newTrigger.Event1TransPointX = gridX;
                                            newTrigger.Event1TransPointY = gridY + 1;
                                            Coordinate newCoor = new Coordinate();
                                            newCoor.X = gridX;
                                            newCoor.Y = gridY + 1;
                                            if (newTrigger.TriggerSquaresList.Count <= 1)
                                            {
                                                if (newTrigger.TriggerSquaresList.Count == 1)
                                                {
                                                    newTrigger.TriggerSquaresList.RemoveAt(0);
                                                }
                                                newTrigger.TriggerSquaresList.Add(newCoor);
                                            }
                                            areaOrg.Tiles[(gridY+1) * areaOrg.MapSizeX + gridX].transitionToMasterDirection = area.Tiles[gridY * areaOrg.MapSizeX + gridX].transitionToMasterDirection;
                                            areaOrg.Tiles[(gridY+1) * areaOrg.MapSizeX + gridX].numberOfLinkedAreaToTransitionTo = area.linkNumberOfThisArea;
                                            //if (mod.formerDirection == "E")
                                                //areaOrg.Tiles[(gridY + 1) * areaOrg.MapSizeX + gridX].transitionToMasterDirection = area.Tiles[gridY * areaOrg.MapSizeX + gridX].transitionToMasterDirection;
                                                //areaOrg.Tiles[(gridY + 1) * areaOrg.MapSizeX + gridX].numberOfLinkedAreaToTransitionTo = area.linkNumberOfThisArea;
                                            //{

                                            //}
                                            //areaOrg.saveAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                                        }
                                    }
                                }

                                //pointing towards east
                                if (transitionToMasterRotationCounter == 2)
                                {
                                    foreach (Trigger newTrigger in areaOrg.Triggers)
                                    {
                                        if (newTrigger.TriggerTag == (prntForm.selectedLevelMapTriggerTag + "_" + area.masterOfThisArea))
                                        {
                                            newTrigger.isLinkToMaster = true;
                                            newTrigger.tagOfLink = area.Filename;
                                            newTrigger.tagOfLinkedMaster = area.masterOfThisArea;
                                            newTrigger.Enabled = true;
                                            newTrigger.EnabledEvent1 = true;
                                            newTrigger.Event1Type = "transition";
                                            newTrigger.Event1FilenameOrTag = area.Filename;
                                            newTrigger.Event1TransPointX = gridX - 1;
                                            newTrigger.Event1TransPointY = gridY;
                                            Coordinate newCoor = new Coordinate();
                                            newCoor.X = gridX - 1;
                                            newCoor.Y = gridY;
                                            if (newTrigger.TriggerSquaresList.Count <= 1)
                                            {
                                                if (newTrigger.TriggerSquaresList.Count == 1)
                                                {
                                                    newTrigger.TriggerSquaresList.RemoveAt(0);
                                                }
                                                newTrigger.TriggerSquaresList.Add(newCoor);
                                            }
                                            areaOrg.Tiles[gridY * areaOrg.MapSizeX + gridX-1].transitionToMasterDirection = area.Tiles[gridY * areaOrg.MapSizeX + gridX].transitionToMasterDirection;
                                            areaOrg.Tiles[gridY * areaOrg.MapSizeX + gridX-1].numberOfLinkedAreaToTransitionTo = area.linkNumberOfThisArea;
                                            //areaOrg.saveAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                                        }
                                    }
                                }

                                //pointing towards south
                                if (transitionToMasterRotationCounter == 3)
                                {
                                    foreach (Trigger newTrigger in areaOrg.Triggers)
                                    {
                                        if (newTrigger.TriggerTag == (prntForm.selectedLevelMapTriggerTag + "_" + area.masterOfThisArea))
                                        {
                                            newTrigger.isLinkToMaster = true;
                                            newTrigger.tagOfLink = area.Filename;
                                            newTrigger.tagOfLinkedMaster = area.masterOfThisArea;
                                            newTrigger.Enabled = true;
                                            newTrigger.EnabledEvent1 = true;
                                            newTrigger.Event1Type = "transition";
                                            newTrigger.Event1FilenameOrTag = area.Filename;
                                            newTrigger.Event1TransPointX = gridX;
                                            newTrigger.Event1TransPointY = gridY - 1;
                                            Coordinate newCoor = new Coordinate();
                                            newCoor.X = gridX;
                                            newCoor.Y = gridY - 1;
                                            if (newTrigger.TriggerSquaresList.Count <= 1)
                                            {
                                                if (newTrigger.TriggerSquaresList.Count == 1)
                                                {
                                                    newTrigger.TriggerSquaresList.RemoveAt(0);
                                                }
                                                newTrigger.TriggerSquaresList.Add(newCoor);
                                            }
                                            areaOrg.Tiles[(gridY-1) * areaOrg.MapSizeX + gridX].transitionToMasterDirection = area.Tiles[gridY * areaOrg.MapSizeX + gridX].transitionToMasterDirection;
                                            areaOrg.Tiles[(gridY-1) * areaOrg.MapSizeX + gridX].numberOfLinkedAreaToTransitionTo = area.linkNumberOfThisArea;
                                            //areaOrg.saveAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                                        }
                                    }
                                }

                                //pointing towards west
                                if (transitionToMasterRotationCounter == 4)
                                {
                                    foreach (Trigger newTrigger in areaOrg.Triggers)
                                    {
                                        if (newTrigger.TriggerTag == (prntForm.selectedLevelMapTriggerTag + "_" + area.masterOfThisArea))
                                        {
                                            newTrigger.isLinkToMaster = true;
                                            newTrigger.tagOfLink = area.Filename;
                                            newTrigger.tagOfLinkedMaster = area.masterOfThisArea;
                                            newTrigger.Enabled = true;
                                            newTrigger.EnabledEvent1 = true;
                                            newTrigger.Event1Type = "transition";
                                            newTrigger.Event1FilenameOrTag = area.Filename;
                                            newTrigger.Event1TransPointX = gridX + 1;
                                            newTrigger.Event1TransPointY = gridY;
                                            Coordinate newCoor = new Coordinate();
                                            newCoor.X = gridX + 1;
                                            newCoor.Y = gridY;
                                            if (newTrigger.TriggerSquaresList.Count <= 1)
                                            {
                                                if (newTrigger.TriggerSquaresList.Count == 1)
                                                {
                                                    newTrigger.TriggerSquaresList.RemoveAt(0);
                                                }
                                                newTrigger.TriggerSquaresList.Add(newCoor);
                                            }
                                            areaOrg.Tiles[gridY * areaOrg.MapSizeX + gridX+1].transitionToMasterDirection = area.Tiles[gridY * areaOrg.MapSizeX + gridX].transitionToMasterDirection;
                                            areaOrg.Tiles[gridY * areaOrg.MapSizeX + gridX+1].numberOfLinkedAreaToTransitionTo = area.linkNumberOfThisArea;
                                            //areaOrg.saveAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                                        }
                                    }
                                }
                                //clean up: remove all tile info for link to master without correpsonding trigger in that square
                                //will be neededfor openLevel and createFiels , too

                                for (int i = 0; i < areaOrg.Tiles.Count(); i++)
                                {
                                    bool resetTile = true;
                                    int TileLocX = i % areaOrg.MapSizeY;
                                    int TileLocY = i / areaOrg.MapSizeX;

                                    foreach (Trigger trig in areaOrg.Triggers)
                                    {
                                        if (trig.isLinkToMaster)
                                        {
                                            if (trig.TriggerSquaresList[0].X == TileLocX && trig.TriggerSquaresList[0].Y == TileLocY)
                                            {
                                                resetTile = false;
                                                break;
                                            }
                                        }
                                    }

                                    if (resetTile)
                                    {
                                        areaOrg.Tiles[i].transitionToMasterDirection = "none";
                                        areaOrg.Tiles[i].numberOfLinkedAreaToTransitionTo = -1;
                                    }
                                }
                            }

                            //set propertygrid to the new object
                            //hopefully not neeeded
                            //prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = newTrigger;

                            //area.Tiles[selectedTile.index].transitionToMasterDirection = "none";
                            //area.Tiles[selectedTile.index].numberOfLinkedAreaToTransitionTo = -1;

                            //GDI refreshMap(false);
                        }
                    }

                    else if (rbtnChangeLinkState.Checked)
                    {
                        if (area.masterOfThisArea != "none")
                        {
                            selectedTile.index = gridY * area.MapSizeX + gridX;
                            prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                            prntForm.logText(Environment.NewLine);
                            area.Tiles[selectedTile.index].linkedToMasterMap = true;
                            area.Tiles[selectedTile.index].LoSBlocked = true;
                            //GDI refreshMap(false);
                        }
                    }


                    #endregion
                    #region LoS mesh Toggle Selected (Make LoS Blocked)
                    else if (rbtnLoS.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].LoSBlocked = true;
                        //GDI refreshMap(false);
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
                        //GDI refreshMap(false);
                        //GDI drawSelectionBox(gridX, gridY);
                        selectionBoxLocation = new Point(gridX, gridY);
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
                                panelView.ContextMenuStrip.Items.Add(prp.PropTag, null, handler); //string, image, handler
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
                                    panelView.ContextMenuStrip.Items.Add(t.TriggerTag, null, handler); //string, image, handler
                                    prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = t;
                                }
                            }
                        }

                        //height level system: add tile property info
                        int tileCounter = 0;
                        foreach (Tile t in area.Tiles)
                        {
                            int locationX = tileCounter % area.MapSizeX;
                            int locationY = tileCounter / area.MapSizeX;
                            tileCounter++;
                            if ((locationX == newPoint.X) && (locationY == newPoint.Y))
                            {
                                txtSelectedIconInfo.Text = "Tile (x" + locationX.ToString() + " / y" + locationY.ToString() + ")" + Environment.NewLine;
                                lastSelectedObjectTag = tileCounter.ToString();
                                //lastSelectedObjectTag = "Tile (x" + locationX.ToString() + "/ y" + locationY.ToString() + ")";
                                //panelView.ContextMenuStrip.Items.Add(lastSelectedObjectTag, null, handler); //string, image, handler
                                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = t;
                            }
                        }

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
                    #region Walkmesh Toggle Selected (Make Walkable)
                    if (rbtnWalkable.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].Walkable = true;
                        //GDI refreshMap(false);
                    }
                    else if (rbtnToMaster.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        string tagOfRemovedTrigger = "void";

                        //allow removing only from links
                        if (area.masterOfThisArea != "none")
                        {
                            if (area.Triggers.Count >= 1)
                            {
                                for (int i = area.Triggers.Count - 1; i >= 0; i--)
                                {
                                    if (area.Triggers[i].isLinkToMaster)
                                    {
                                        if (area.Triggers[i].TriggerSquaresList.Count >= 1)
                                        {
                                            if (area.Triggers[i].TriggerSquaresList[0].X == gridX && area.Triggers[i].TriggerSquaresList[0].Y == gridY)
                                            {
                                                area.Tiles[gridY * area.MapSizeX + gridX].transitionToMasterDirection = "none";
                                                area.Tiles[gridY * area.MapSizeX + gridX].numberOfLinkedAreaToTransitionTo = -1;
                                                tagOfRemovedTrigger = area.Triggers[i].TriggerTag;
                                                area.Triggers.RemoveAt(i);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }


                            Area areaOrg = new Area();
                            bool orgIsStillOpen = false;
                            for (int a = 0; a < prntForm.openAreasList.Count; a++)
                            {
                                if (prntForm.openAreasList[a].Filename == area.masterOfThisArea)
                                {
                                    areaOrg = prntForm.openAreasList[a];
                                    orgIsStillOpen = true;
                                    break;
                                }
                            }

                            if (orgIsStillOpen)
                            {

                                //now the same plus special tile attributes for the master
                                //Area areaOrg = new Area();
                                //string g_dir = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas";
                                //if (area.masterOfThisArea != "none")
                                //{
                                //areaOrg = areaOrg.loadAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                                //}

                                if (areaOrg.Triggers.Count >= 1)
                                {
                                    for (int i = areaOrg.Triggers.Count - 1; i >= 0; i--)
                                    {
                                        if (areaOrg.Triggers[i].isLinkToMaster)
                                        {
                                            if ((tagOfRemovedTrigger + "_" + areaOrg.Filename) == (areaOrg.Triggers[i].TriggerTag))
                                            {
                                                //tagchange1
                                                //newTriggerMaster.TriggerTag = "TransitionOn_" + area.Filename + "_" + (prntForm.mod.nextIdNumber - 1) + "_" + area.masterOfThisArea;

                                                //if (areaOrg.Triggers[i].TriggerSquaresList[0].X == gridX && areaOrg.Triggers[i].TriggerSquaresList[0].Y == gridY)
                                                //{
                                                areaOrg.Tiles[areaOrg.Triggers[i].TriggerSquaresList[0].Y * areaOrg.MapSizeX + areaOrg.Triggers[i].TriggerSquaresList[0].X].transitionToMasterDirection = "none";
                                                areaOrg.Tiles[areaOrg.Triggers[i].TriggerSquaresList[0].Y * areaOrg.MapSizeX + areaOrg.Triggers[i].TriggerSquaresList[0].X].numberOfLinkedAreaToTransitionTo = -1;
                                                areaOrg.Triggers.RemoveAt(i);
                                                break;
                                                //}
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //patrick5
                        /*
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        rotateTransitionToMaster();

                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);

                        //rotation satet will do belwo 2-steps in 4 variants
                        //patrick3
                        //create a new trigger object on linked map (ie current map)
                        //Trigger newTrigger = new Trigger();
                        //newTrigger.TriggerTag = "newTrigger_" + prntForm.mod.nextIdNumber;
                        //areaOrg.Triggers.Add(newTriggerMaster);
                        //prntForm.selectedLevelMapTriggerTag = newTrigger.TriggerTag;

                        foreach (Trigger newTrigger in area.Triggers)
                        {
                            if (newTrigger.TriggerTag == prntForm.selectedLevelMapTriggerTag)
                            {
                                newTrigger.Enabled = true;
                                newTrigger.EnabledEvent1 = true;
                                newTrigger.Event1Type = "transition";
                                newTrigger.Event1FilenameOrTag = area.masterOfThisArea;
                                newTrigger.Event1TransPointX = gridX;
                                newTrigger.Event1TransPointY = gridY;
                                Coordinate newCoor = new Coordinate();
                                newCoor.X = gridX;
                                newCoor.Y = gridY;
                                newTrigger.TriggerSquaresList.Add(newCoor);
                            }
                        }

                        Area areaOrg = new Area();
                        string g_dir = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas";
                        if (area.masterOfThisArea != "none")
                        {
                            areaOrg = areaOrg.loadAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                        }

                        //patrick4
                        foreach (Trigger newTrigger in areaOrg.Triggers)
                        {
                            if (newTrigger.TriggerTag == (prntForm.selectedLevelMapTriggerTag + "_" + area.masterOfThisArea))
                            {
                                newTrigger.Enabled = true;
                                newTrigger.EnabledEvent1 = true;
                                newTrigger.Event1Type = "transition";
                                newTrigger.Event1FilenameOrTag = area.Filename;
                                newTrigger.Event1TransPointX = gridX;
                                newTrigger.Event1TransPointY = gridY;
                                Coordinate newCoor = new Coordinate();
                                newCoor.X = gridX;
                                newCoor.Y = gridY;
                                newTrigger.TriggerSquaresList.Add(newCoor);
                                areaOrg.Tiles[gridY * areaOrg.MapSizeX + gridX].transitionToMasterDirection = area.Tiles[gridY * areaOrg.MapSizeX + gridX].transitionToMasterDirection];
                                areaOrg.Tiles[gridY * areaOrg.MapSizeX + gridX].numberOfLinkedAreaToTransitionTo = area.linkNumberOfThisArea;
                            }
                        }
                        */
                    }
                    else if (rbtnChangeLinkState.Checked)
                    {
                        if (area.masterOfThisArea != "none")
                        {
                            selectedTile.index = gridY * area.MapSizeX + gridX;
                            prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                            prntForm.logText(Environment.NewLine);
                            area.Tiles[selectedTile.index].linkedToMasterMap = false;
                            area.Tiles[selectedTile.index].LoSBlocked = false;
                            //GDI refreshMap(false);
                        }
                    }
                    else if (rbtnHeightLevel.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        //check taht square is not a brdige right now, aDD, to do
                        if (!isOrthogonalNeighbourOfBridge(gridX, gridY) && !area.Tiles[gridY * area.MapSizeX + gridX].isEWBridge && !area.Tiles[gridY * area.MapSizeX + gridX].isNSBridge)
                        {
                            if (area.allowLevelDesignWithMoreThan2HeightLevelsDifference)
                            {
                                area.Tiles[selectedTile.index].heightLevel -= 1;
                            }
                            else
                            {
                                bool changeAllowed = true;
                                try
                                {
                                    for (int xCheck = gridX - 1; xCheck <= gridX + 1; xCheck++)
                                    {
                                        for (int yCheck = gridY - 1; yCheck <= gridY + 1; yCheck++)
                                        {

                                            if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel > area.Tiles[selectedTile.index].heightLevel + 1)
                                            {
                                                if (((yCheck == gridY) && (xCheck != gridX)) || ((yCheck != gridY) && (xCheck == gridX)))
                                                {
                                                    changeAllowed = false;
                                                    break;
                                                }
                                            }


                                            /*
                                            if (area.Tiles[yCheck * area.MapSizeX + xCheck].heightLevel < area.Tiles[selectedTile.index].heightLevel - 1)
                                            {
                                                changeAllowed = false;
                                                break;
                                            }
                                            */

                                        }
                                    }
                                }
                                catch
                                {

                                }
                                if (changeAllowed)
                                {
                                    area.Tiles[selectedTile.index].heightLevel -= 1;
                                }
                            }
                        }
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                        //area.Tiles[selectedTile.index].heightLevel -= 1;
                        //calculateHeightShadows();
                        //break;
                        //ContextMenuStrip.Items.Clear();
                        //contextMenuStrip1.Items.Clear();
                        //ContextMenu.MenuItems.Clear();
                        //to do
                        /*
                        contextMenuStrip1.Items.Clear();
                        lastSelectedObjectTag = "";
                        panelView.ContextMenuStrip.Items.Clear();

                        //contextMenuStrip1.Items.Clear();
                        //when left click, get location
                        //gridX = e.X / sqr;
                        //gridY = e.Y / sqr;
                        Point newPoint = new Point(gridX, gridY);
                        EventHandler handler = new EventHandler(HandleContextMenuClick);
                        //loop through all the objects
                        //if has that location, add the tag to the list                    
                        //draw selection box
                        //GDI refreshMap(false);
                        //GDI drawSelectionBox(gridX, gridY);
                        selectionBoxLocation = new Point(gridX, gridY);
                        txtSelectedIconInfo.Text = "";
                        //GDI refreshMap(false);
                        */
                    }
                    else if (rbtnBridgeEW.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].isEWBridge = false;
                        area.Tiles[selectedTile.index].isNSBridge = false;
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    //REMOVE THESE
                    //to do
                    /*
                    else if (rbtnBridgeNS.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].isNSBridge = false;
                        area.Tiles[selectedTile.index].isEWBridge = false;
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    */
                    else if (rbtnDownToN.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].isRamp = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowE = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowS = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowW = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowN = false;
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    /*
                    else if (rbtnDownToE.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].isRamp = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowE = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowS = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowW = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowN = false;
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    */
                    /*
                    else if (rbtnDownToS.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].isRamp = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowE = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowS = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowW = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowN = false;
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    */
                    /*
                    else if (rbtnDownToW.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].isRamp = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowE = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowS = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowW = false;
                        area.Tiles[selectedTile.index].hasDownStairShadowN = false;
                        calculateHeightShadows();
                        //GDI refreshMap(false);
                    }
                    */
                    #endregion
                    #region LoS mesh Toggle Selected (Make LoS Visible)
                    else if (rbtnLoS.Checked)
                    {
                        selectedTile.index = gridY * area.MapSizeX + gridX;
                        prntForm.logText("gridx = " + gridX.ToString() + "gridy = " + gridY.ToString());
                        prntForm.logText(Environment.NewLine);
                        area.Tiles[selectedTile.index].LoSBlocked = false;
                        //GDI refreshMap(false);
                    }
                    else if (rbtnPaintTile.Checked)
                    {
                        //pipetto
                        currentTileFilename = "";
                    }
                    #endregion
                    else
                    {
                        // exit by right click or ESC
                        prntForm.logText("entered right-click");
                        prntForm.logText(Environment.NewLine);
                        prntForm.selectedLevelMapCreatureTag = "";
                        prntForm.selectedLevelMapPropTag = "";
                        //
                        //lastSelectedObjectTag = "";
                        //panelView.ContextMenuStrip.Items.Clear();
                        //
                        prntForm.CreatureSelected = false;
                        prntForm.PropSelected = false;
                        prntForm.currentSelectedTrigger = null;
                        //GDI refreshMap(true);
                        //GDI UpdatePB();
                        rbtnInfo.Checked = true;
                        resetTileToBePlacedSettings();
                    }
                    break;
                #endregion
            }
        }

        private void rotateStair()
        {
            if (stairRotationCounter == 1)
            {
                area.Tiles[selectedTile.index].hasDownStairShadowE = false;
                area.Tiles[selectedTile.index].hasDownStairShadowS = false;
                area.Tiles[selectedTile.index].hasDownStairShadowW = false;
                area.Tiles[selectedTile.index].hasDownStairShadowN = true;
            }
            else
            if (stairRotationCounter == 2)
            {
                area.Tiles[selectedTile.index].hasDownStairShadowE = true;
                area.Tiles[selectedTile.index].hasDownStairShadowS = false;
                area.Tiles[selectedTile.index].hasDownStairShadowW = false;
                area.Tiles[selectedTile.index].hasDownStairShadowN = false;
            }
            else
               if (stairRotationCounter == 3)
            {
                area.Tiles[selectedTile.index].hasDownStairShadowE = false;
                area.Tiles[selectedTile.index].hasDownStairShadowS = true;
                area.Tiles[selectedTile.index].hasDownStairShadowW = false;
                area.Tiles[selectedTile.index].hasDownStairShadowN = false;
            }
            else
               if (stairRotationCounter == 4)
            {
                area.Tiles[selectedTile.index].hasDownStairShadowE = false;
                area.Tiles[selectedTile.index].hasDownStairShadowS = false;
                area.Tiles[selectedTile.index].hasDownStairShadowW = true;
                area.Tiles[selectedTile.index].hasDownStairShadowN = false;
            }

            stairRotationCounter++;

            if(stairRotationCounter > 4)
            {
                stairRotationCounter = 1;
            }
        }

        private void rotateTransitionToMaster()
        {
            //changela1
            //transitionToMasterRotationCounter
            if (transitionToMasterRotationCounter == 1)
            {
                area.Tiles[selectedTile.index].transitionToMasterDirection = "E";
                mod.formerDirection = "N";
            }
            else
            if (transitionToMasterRotationCounter == 2)
            {
                area.Tiles[selectedTile.index].transitionToMasterDirection = "S";
                mod.formerDirection = "E";
            }
            else
               if (transitionToMasterRotationCounter == 3)
            {
                area.Tiles[selectedTile.index].transitionToMasterDirection = "W";
                mod.formerDirection = "S";
            }
            else
               if (transitionToMasterRotationCounter == 4)
            {
                area.Tiles[selectedTile.index].transitionToMasterDirection = "N";
                mod.formerDirection = "W";
            }

            transitionToMasterRotationCounter++;

            if (transitionToMasterRotationCounter > 4)
            {
                transitionToMasterRotationCounter = 1;
            }
        }

        private void rotateBridge()
        {
            if (bridgeRotationCounter == 1)
            {
                area.Tiles[selectedTile.index].isEWBridge = true;
                if ((selectedTile.index + 1) < (area.MapSizeX * area.MapSizeY))
                {
                    //east to same height
                    area.Tiles[selectedTile.index + 1].heightLevel = area.Tiles[selectedTile.index].heightLevel;
                    area.Tiles[selectedTile.index + 1].isRamp = false;
                    area.Tiles[selectedTile.index + 1].hasDownStairShadowE = false;
                    area.Tiles[selectedTile.index + 1].hasDownStairShadowW = false;
                    area.Tiles[selectedTile.index + 1].hasDownStairShadowN = false;
                    area.Tiles[selectedTile.index + 1].hasDownStairShadowS = false;
                    area.Tiles[selectedTile.index + 1].isEWBridge = false;
                    area.Tiles[selectedTile.index + 1].isNSBridge = false;

                }

                if ((selectedTile.index - 1) >= 0)
                {
                    //west to same height
                    area.Tiles[selectedTile.index - 1].heightLevel = area.Tiles[selectedTile.index].heightLevel;
                    area.Tiles[selectedTile.index - 1].isRamp = false;
                    area.Tiles[selectedTile.index - 1].hasDownStairShadowE = false;
                    area.Tiles[selectedTile.index - 1].hasDownStairShadowW = false;
                    area.Tiles[selectedTile.index - 1].hasDownStairShadowN = false;
                    area.Tiles[selectedTile.index - 1].hasDownStairShadowS = false;
                    area.Tiles[selectedTile.index - 1].isEWBridge = false;
                    area.Tiles[selectedTile.index - 1].isNSBridge = false;
                }

                if ((selectedTile.index + area.MapSizeX) < (area.MapSizeX * area.MapSizeY))
                {
                    //south to lower height
                    area.Tiles[selectedTile.index + area.MapSizeX].heightLevel = area.Tiles[selectedTile.index].heightLevel - 1;
                    area.Tiles[selectedTile.index + area.MapSizeX].isRamp = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].hasDownStairShadowE = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].hasDownStairShadowW = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].hasDownStairShadowN = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].hasDownStairShadowS = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].isEWBridge = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].isNSBridge = false;
                }

                if ((selectedTile.index - area.MapSizeX) >= 0)
                {
                    //north to lower height
                    area.Tiles[selectedTile.index - area.MapSizeX].heightLevel = area.Tiles[selectedTile.index].heightLevel - 1;
                    area.Tiles[selectedTile.index - area.MapSizeX].isRamp = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].hasDownStairShadowE = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].hasDownStairShadowW = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].hasDownStairShadowN = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].hasDownStairShadowS = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].isEWBridge = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].isNSBridge = false;
                }

                area.Tiles[selectedTile.index].isNSBridge = false;
            }
            else
            if (bridgeRotationCounter == 2)
            {
                area.Tiles[selectedTile.index].isEWBridge = false;
                area.Tiles[selectedTile.index].isNSBridge = true;

                if ((selectedTile.index + 1) < (area.MapSizeX * area.MapSizeY))
                {
                    //east to lower height
                    area.Tiles[selectedTile.index + 1].heightLevel = area.Tiles[selectedTile.index].heightLevel - 1;
                    area.Tiles[selectedTile.index + 1].isRamp = false;
                    area.Tiles[selectedTile.index + 1].hasDownStairShadowE = false;
                    area.Tiles[selectedTile.index + 1].hasDownStairShadowW = false;
                    area.Tiles[selectedTile.index + 1].hasDownStairShadowN = false;
                    area.Tiles[selectedTile.index + 1].hasDownStairShadowS = false;
                    area.Tiles[selectedTile.index + 1].isEWBridge = false;
                    area.Tiles[selectedTile.index + 1].isNSBridge = false;
                }

                if ((selectedTile.index - 1) >= 0)
                {
                    //west to lower height
                    area.Tiles[selectedTile.index - 1].heightLevel = area.Tiles[selectedTile.index].heightLevel - 1;
                    area.Tiles[selectedTile.index - 1].isRamp = false;
                    area.Tiles[selectedTile.index - 1].hasDownStairShadowE = false;
                    area.Tiles[selectedTile.index - 1].hasDownStairShadowW = false;
                    area.Tiles[selectedTile.index - 1].hasDownStairShadowN = false;
                    area.Tiles[selectedTile.index - 1].hasDownStairShadowS = false;
                    area.Tiles[selectedTile.index - 1].isEWBridge = false;
                    area.Tiles[selectedTile.index - 1].isNSBridge = false;


                }

                if ((selectedTile.index + area.MapSizeX) < (area.MapSizeX * area.MapSizeY))
                {
                    //south to same height
                    area.Tiles[selectedTile.index + area.MapSizeX].heightLevel = area.Tiles[selectedTile.index].heightLevel;
                    area.Tiles[selectedTile.index + area.MapSizeX].isRamp = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].hasDownStairShadowE = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].hasDownStairShadowW = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].hasDownStairShadowN = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].hasDownStairShadowS = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].isEWBridge = false;
                    area.Tiles[selectedTile.index + area.MapSizeX].isNSBridge = false;
                }

                if ((selectedTile.index - area.MapSizeX) >= 0)
                {
                    //north to same height
                    area.Tiles[selectedTile.index - area.MapSizeX].heightLevel = area.Tiles[selectedTile.index].heightLevel;
                    area.Tiles[selectedTile.index - area.MapSizeX].isRamp = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].hasDownStairShadowE = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].hasDownStairShadowW = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].hasDownStairShadowN = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].hasDownStairShadowS = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].isEWBridge = false;
                    area.Tiles[selectedTile.index - area.MapSizeX].isNSBridge = false;
                }
            }
           
            bridgeRotationCounter++;

            if (bridgeRotationCounter > 2)
            {
                bridgeRotationCounter = 1;
            }
        }

        private void btnFillWithSelected_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < area.MapSizeX; x++)
            {
                for (int y = 0; y < area.MapSizeY; y++)
                {
                    selectedTile.index = y * area.MapSizeX + x;
                    if (radioButton1.Checked)
                    {
                        area.Tiles[selectedTile.index].Layer1Filename = currentTileFilename;
                    }
                    else if (radioButton2.Checked)
                    {
                        area.Tiles[selectedTile.index].Layer2Filename = currentTileFilename;
                    }
                    else if (radioButton3.Checked)
                    {
                        area.Tiles[selectedTile.index].Layer3Filename = currentTileFilename;
                    }
                    else if (radioButton4.Checked)
                    {
                        area.Tiles[selectedTile.index].Layer4Filename = currentTileFilename;
                    }
                    else if (radioButton5.Checked)
                    {
                        area.Tiles[selectedTile.index].Layer5Filename = currentTileFilename;
                    }
                }
            }
            //GDI refreshMap(true);
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //GDI refreshMap(true);
        }
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //GDI refreshMap(true);
        }
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            //GDI refreshMap(true);
        }
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            //GDI refreshMap(true);
        }
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            //GDI refreshMap(true);
        }

        #region DIRECT2D STUFF
        private void InitDirect2DAndDirectWrite()
        {
            Factory2D = new SharpDX.Direct2D1.Factory();
            FactoryDWrite = new SharpDX.DirectWrite.Factory();

            var properties = new HwndRenderTargetProperties();
            properties.Hwnd = panelView.Handle;
            properties.PixelSize = new SharpDX.Size2(panelView.ClientSize.Width, panelView.ClientSize.Height);
            properties.PresentOptions = PresentOptions.None;

            RenderTarget2D = new WindowRenderTarget(Factory2D, new RenderTargetProperties(new SharpDX.Direct2D1.PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied)), properties);
            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;
            RenderTarget2D.TextAntialiasMode = TextAntialiasMode.Cleartype;

            SceneColorBrush = new SolidColorBrush(RenderTarget2D, SharpDX.Color.Black);
            //TODO dispose of all of these objects upon closing this editor
            try
            {
                gameMapBitmapD2D = GetFromBitmapList(area.ImageFileName);
            }
            catch (Exception ex)
            {
                prntForm.errorLog(ex.ToString());
            }
            timerRenderLoop.Start();
        }
        public void DrawD2DBitmap(SharpDX.Direct2D1.Bitmap bitmap, SharpDX.RectangleF source, SharpDX.RectangleF target, int angleInDegrees, bool mirror, int Xshift, int Yshift)
        {
            DrawD2DBitmap(bitmap, source, target, angleInDegrees, mirror, Xshift, Yshift, 0, 0);
        }
        public void DrawD2DBitmap(SharpDX.Direct2D1.Bitmap bitmap, SharpDX.RectangleF source, SharpDX.RectangleF target, int angleInDegrees, bool mirror, int Xshift, int Yshift, int Xscale, int Yscale)
        {
            int mir = 1;
            if (mirror) { mir = -1; }
            //convert degrees to radians
            float angle = (float)(Math.PI * 2 * (float)angleInDegrees / (float)360);
            float xshf = (float)Xshift * (float)sqr / 50f;
            float yshf = (float)Yshift * (float)sqr / 50f;
            float xscl = 1f + (((float)Xscale * (float)sqr / 50f) / 50f);
            float yscl = 1f + (((float)Yscale * (float)sqr / 50f) / 50f);
            SharpDX.Vector2 center = new SharpDX.Vector2(target.Left + (target.Width / 2), target.Top + (target.Height / 2));
            RenderTarget2D.Transform = SharpDX.Matrix.Transformation2D(center, 0, new SharpDX.Vector2(mir * xscl, yscl), center, angle, new SharpDX.Vector2(xshf, yshf));
            SharpDX.RectangleF trg = new SharpDX.RectangleF(target.Left, target.Top, target.Width, target.Height);
            SharpDX.RectangleF src = new SharpDX.RectangleF(source.Left, source.Top, source.Width, source.Height);
            RenderTarget2D.DrawBitmap(bitmap, trg, 1.0f, BitmapInterpolationMode.Linear, src);
            RenderTarget2D.Transform = SharpDX.Matrix3x2.Identity;
        }

        /*
        public void DrawD2DBitmap(SharpDX.Direct2D1.Bitmap bitmap, SharpDX.RectangleF source, SharpDX.RectangleF target, int angleInDegrees, bool mirror, int Xshift, int Yshift, int Xscale, int Yscale, float opacity)
        {
            int mir = 1;
            if (mirror) { mir = -1; }
            //convert degrees to radians
            float angle = (float)(Math.PI * 2 * (float)angleInDegrees / (float)360);
            float xshf = (float)Xshift * (float)sqr / 50f;
            float yshf = (float)Yshift * (float)sqr / 50f;
            float xscl = 1f + (((float)Xscale * (float)sqr / 50f) / 50f);
            float yscl = 1f + (((float)Yscale * (float)sqr / 50f) / 50f);
            SharpDX.Vector2 center = new SharpDX.Vector2(target.Left + (target.Width / 2), target.Top + (target.Height / 2));
            RenderTarget2D.Transform = SharpDX.Matrix.Transformation2D(center, 0, new SharpDX.Vector2(mir * xscl, yscl), center, angle, new SharpDX.Vector2(xshf, yshf));
            SharpDX.RectangleF trg = new SharpDX.RectangleF(target.Left, target.Top, target.Width, target.Height);
            SharpDX.RectangleF src = new SharpDX.RectangleF(source.Left, source.Top, source.Width, source.Height);
            RenderTarget2D.DrawBitmap(bitmap, trg, opacity, BitmapInterpolationMode.Linear, src);
            RenderTarget2D.Transform = SharpDX.Matrix3x2.Identity;
        }
        */

        public void DrawD2DBitmap(SharpDX.Direct2D1.Bitmap bitmap, SharpDX.RectangleF source, SharpDX.RectangleF target, int angleInDegrees, bool mirror, int Xshift, int Yshift, int Xscale, int Yscale, float opac)
        {
            int mir = 1;
            if (mirror) { mir = -1; }
            //convert degrees to radians
            float angle = (float)(Math.PI * 2 * (float)angleInDegrees / (float)360);
            float xshf = (float)Xshift * (float)sqr / 50f;
            float yshf = (float)Yshift * (float)sqr / 50f;
            float xscl = 1f + (((float)Xscale * (float)sqr / 50f) / 50f);
            float yscl = 1f + (((float)Yscale * (float)sqr / 50f) / 50f);
            SharpDX.Vector2 center = new SharpDX.Vector2(target.Left + (target.Width / 2), target.Top + (target.Height / 2));
            RenderTarget2D.Transform = SharpDX.Matrix.Transformation2D(center, 0, new SharpDX.Vector2(mir * xscl, yscl), center, angle, new SharpDX.Vector2(xshf, yshf));
            SharpDX.RectangleF trg = new SharpDX.RectangleF(target.Left, target.Top, target.Width, target.Height);
            SharpDX.RectangleF src = new SharpDX.RectangleF(source.Left, source.Top, source.Width, source.Height);
            RenderTarget2D.DrawBitmap(bitmap, trg, opac, BitmapInterpolationMode.Linear, src);
            RenderTarget2D.Transform = SharpDX.Matrix3x2.Identity;
        }
        public void DrawRectangle(SharpDX.RectangleF rect, SharpDX.Color penColor, int penWidth)
        {
            using (SolidColorBrush scb = new SolidColorBrush(RenderTarget2D, penColor))
            {
                RenderTarget2D.DrawRectangle(rect, scb, penWidth);
            }
        }
        public System.Drawing.Bitmap LoadBitmapGDI(string filename)
        {
            System.Drawing.Bitmap bm = null;
            try
            {
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\" + filename + ".png"))
                {
                    bm = new System.Drawing.Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\" + filename + ".png");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\" + filename))
                {
                    bm = new System.Drawing.Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\" + filename);
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + filename + ".png"))
                {
                    bm = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + filename + ".png");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + filename + ".jpg"))
                {
                    bm = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + filename + ".jpg");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + filename))
                {
                    bm = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + filename);
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\" + filename + ".png"))
                {
                    bm = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\" + filename + ".png");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\" + filename + ".jpg"))
                {
                    bm = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\" + filename + ".jpg");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\" + filename))
                {
                    bm = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ui\\" + filename);
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + filename + ".png"))
                {
                    bm = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + filename + ".png");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + filename + ".jpg"))
                {
                    bm = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + filename + ".jpg");
                }
                else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + filename))
                {
                    bm = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + filename);
                }
                else
                {
                    bm = new System.Drawing.Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\missingtexture.png");
                }		
            }
            catch (Exception ex)
            {
                prntForm.errorLog(ex.ToString());
                if (bm == null)
                {
                    bm = new System.Drawing.Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\missingtexture.png");
                    return bm;
                }                
            }
            return bm;
        }
        public SharpDX.Direct2D1.Bitmap LoadBitmap(string file) //change this to LoadBitmap
        {
            // Loads from file using System.Drawing.Image
            using (var bitmap = LoadBitmapGDI(file)) //change this to LoadBitmapGDI
            {
                var sourceArea = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                var bitmapProperties = new BitmapProperties(new SharpDX.Direct2D1.PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Premultiplied));
                var size = new SharpDX.Size2(bitmap.Width, bitmap.Height);

                // Transform pixels from BGRA to RGBA
                int stride = bitmap.Width * sizeof(int);
                using (var tempStream = new SharpDX.DataStream(bitmap.Height * stride, true, true))
                {
                    // Lock System.Drawing.Bitmap
                    var bitmapData = bitmap.LockBits(sourceArea, ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

                    // Convert all pixels 
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        int offset = bitmapData.Stride * y;
                        for (int x = 0; x < bitmap.Width; x++)
                        {
                            // Not optimized 
                            byte B = System.Runtime.InteropServices.Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte G = System.Runtime.InteropServices.Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte R = System.Runtime.InteropServices.Marshal.ReadByte(bitmapData.Scan0, offset++);
                            byte A = System.Runtime.InteropServices.Marshal.ReadByte(bitmapData.Scan0, offset++);
                            int rgba = R | (G << 8) | (B << 16) | (A << 24);
                            tempStream.Write(rgba);
                        }
                    }
                    bitmap.UnlockBits(bitmapData);
                    tempStream.Position = 0;
                    return new SharpDX.Direct2D1.Bitmap(RenderTarget2D, size, tempStream, stride, bitmapProperties);
                }
            }
        }
        public SharpDX.Direct2D1.Bitmap GetFromBitmapList(string fileNameWithOutExt)
        {
            //check to see if in list already and return bitmap if it is found
            if (commonBitmapList.ContainsKey(fileNameWithOutExt))
            {
                return commonBitmapList[fileNameWithOutExt];
            }
            //try loading and adding to list and return bitmap
            else
            {
                commonBitmapList.Add(fileNameWithOutExt, LoadBitmap(fileNameWithOutExt));
                return commonBitmapList[fileNameWithOutExt];
            }
        }
        public void DisposeAllD2D()
        {
            //dispose of all bitmaps
            foreach(SharpDX.Direct2D1.Bitmap bm in commonBitmapList.Values)
            {
                if (bm != null)
                {
                    bm.Dispose();
                }
            }
            commonBitmapList.Clear();
            DisposeOfBitmap(ref gameMapBitmapD2D);
            SceneColorBrush.Dispose();
            RenderTarget2D.Dispose();
            FactoryDWrite.Dispose();
            Factory2D.Dispose();
        }
        public void DisposeOfBitmap(ref SharpDX.Direct2D1.Bitmap bmp)
        {
            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null;
            }
        }
        public void Render()
        {
            try
            {
                RenderTarget2D.BeginDraw();
                RenderTarget2D.Clear(SharpDX.Color.Black);
                redrawMain();
                drawLinkedShade();
                RenderTarget2D.EndDraw();
            }
            catch (Exception ex)
            {
                prntForm.errorLog(ex.ToString());
            }
        }

        public void drawLinkedShade()
        {
            #region Draw Linked state
            for (int y = 0; y < area.MapSizeY; y++)
            {
                for (int x = 0; x < area.MapSizeX; x++)
                {
                    Tile tile = area.Tiles[y * area.MapSizeX + x];
                    if (tile.linkedToMasterMap)
                    {
                        //LINKED
                        //draw square walkmesh and LoS stuff
                        //SharpDX.Direct2D1.Bitmap bm = GetFromBitmapList("walk_pass");
                        //Rectangle src = new Rectangle(0, 0, g_walkPass.Width, g_walkPass.Height);
                        //Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                        SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList("black_tile").PixelSize.Width, GetFromBitmapList("black_tile").PixelSize.Height);
                        SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, sqr, sqr);

                        DrawD2DBitmap(GetFromBitmapList("black_tile"), src, dst, 0, false, 0, 0, 0, 0, 0.75f);
                        //bear
                        if (chkGrid.Checked) //if show grid is turned on, draw grid squares
                        {
                            float scaler = 1.0f;
                            if (sqr == 50) { scaler = 15.0f; }
                            else if (sqr == 25) { scaler = 7.5f; }
                            else if (sqr == 10) { scaler = 3.0f; }
                            DrawText("L", dst.X + (sqr / 4), dst.Y + (sqr / 4), scaler * 1.5f, SharpDX.Color.Red);
                        }
                    }

                    if ((tile.transitionToMasterDirection != "none") && (area.masterOfThisArea != "none"))
                    {
                        SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList("black_tile").PixelSize.Width, GetFromBitmapList("black_tile").PixelSize.Height);
                        SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, sqr, sqr);

                        //DrawD2DBitmap(GetFromBitmapList("black_tile"), src, dst, 0, false, 0, 0, 0, 0, 0.75f);
                        //bear
                        if (chkGrid.Checked) //if show grid is turned on, draw grid squares
                        {
                            float scaler = 1.0f;
                            if (sqr == 50) { scaler = 15.0f; }
                            else if (sqr == 25) { scaler = 7.5f; }
                            else if (sqr == 10) { scaler = 3.0f; }
                            DrawText(tile.transitionToMasterDirection, dst.X + (sqr / 4), dst.Y + (sqr / 4), scaler * 1.5f, SharpDX.Color.LimeGreen);
                        }
                    }

                    if ((tile.numberOfLinkedAreaToTransitionTo != -1) && (area.masterOfThisArea == "none"))
                    {
                        SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList("black_tile").PixelSize.Width, GetFromBitmapList("black_tile").PixelSize.Height);
                        SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, sqr, sqr);

                        //DrawD2DBitmap(GetFromBitmapList("black_tile"), src, dst, 0, false, 0, 0, 0, 0, 0.75f);
                        //bear
                        if (chkGrid.Checked) //if show grid is turned on, draw grid squares
                        {
                            float scaler = 1.0f;
                            if (sqr == 50) { scaler = 15.0f; }
                            else if (sqr == 25) { scaler = 7.5f; }
                            else if (sqr == 10) { scaler = 3.0f; }
                            DrawText(tile.numberOfLinkedAreaToTransitionTo.ToString(), dst.X + (sqr / 4), dst.Y + (sqr / 4), scaler * 1.5f, SharpDX.Color.LimeGreen);
                        }
                    }
                }
            }
            #endregion
        }
        public bool isOrthogonalNeighbourOfBridge(int tileX, int tileY)
        {
            //check north
            if (tileY - 1 >= 0)
            {
                if (area.Tiles[(tileY-1) * area.MapSizeX + tileX].isEWBridge || area.Tiles[(tileY - 1) * area.MapSizeX + tileX].isNSBridge)
                {
                    return true;
                }
            }

            //check east
            if (tileX + 1 < area.MapSizeX)
            {
                if (area.Tiles[(tileY) * area.MapSizeX + tileX + 1].isEWBridge || area.Tiles[(tileY) * area.MapSizeX + tileX + 1].isNSBridge)
                {
                    return true;
                }
            }

            //check south
            if (tileY + 1 < area.MapSizeY)
            {
                if (area.Tiles[(tileY+1) * area.MapSizeX + tileX].isEWBridge || area.Tiles[(tileY+1) * area.MapSizeX + tileX].isNSBridge)
                {
                    return true;
                }
            }

            //check west
            if (tileX - 1 >= 0)
            {
                if (area.Tiles[(tileY) * area.MapSizeX + tileX - 1].isEWBridge || area.Tiles[(tileY) * area.MapSizeX + tileX -1].isNSBridge)
                {
                    return true;
                }
            }

            return false;
        }


        public bool isDiagonalNeighbourOfBridge(int tileX, int tileY)
        {
            //check northeast
            if ((tileY - 1 >= 0) && (tileX + 1 < area.MapSizeX))
            {
                if (area.Tiles[(tileY - 1) * area.MapSizeX + tileX + 1].isEWBridge || area.Tiles[(tileY - 1) * area.MapSizeX + tileX +1].isNSBridge)
                {
                    return true;
                }
            }

            //check southeast
            if ((tileY + 1 < area.MapSizeY) && (tileX + 1 < area.MapSizeX))
            {
                if (area.Tiles[(tileY+1) * area.MapSizeX + tileX + 1].isEWBridge || area.Tiles[(tileY+1) * area.MapSizeX + tileX + 1].isNSBridge)
                {
                    return true;
                }
            }

            //check southwest
            if ((tileY + 1 < area.MapSizeY) && (tileX - 1 >= 0))
            {
                if (area.Tiles[(tileY + 1) * area.MapSizeX + tileX - 1].isEWBridge || area.Tiles[(tileY + 1) * area.MapSizeX + tileX -1].isNSBridge)
                {
                    return true;
                }
            }

            //check northwest
            if ((tileY - 1 >= 0) && (tileX - 1 >= 0))
            {
                if (area.Tiles[(tileY-1) * area.MapSizeX + tileX - 1].isEWBridge || area.Tiles[(tileY-1) * area.MapSizeX + tileX - 1].isNSBridge)
                {
                    return true;
                }
            }

            return false;
        }

        public void redrawMain()
        {
            if ((!area.ImageFileName.Equals("none")) && (gameMapBitmapD2D != null))
            {
                SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, gameMapBitmapD2D.PixelSize.Width, gameMapBitmapD2D.PixelSize.Height);
                SharpDX.RectangleF dst = new SharpDX.RectangleF(area.backgroundImageStartLocX * sqr, area.backgroundImageStartLocY * sqr, sqr * (gameMapBitmapD2D.PixelSize.Width / 50), sqr * (gameMapBitmapD2D.PixelSize.Height / 50));
                DrawD2DBitmap(gameMapBitmapD2D, src, dst, 0, false, 0, 0, 0, 0);
            }
            if (mod.useAllTileSystem)
            {
                //calculate shadow state of a shaded tile alredy on toolset level
                //two advantages: faster at runtime and enjoy correct shaodw graphics while building in toolset
                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                /*

                public void drawHeightLevelShadows()
        {
            if (mod.useAllTileSystem)
            {
                #region new system
                gv.mod.indexOfNorthernNeighbour = -1;
                gv.mod.indexOfSouthernNeighbour = -1;
                gv.mod.indexOfEasternNeighbour = -1;
                gv.mod.indexOfWesternNeighbour = -1;
                gv.mod.indexOfNorthEasternNeighbour = -1;
                gv.mod.indexOfNorthWesternNeighbour = -1;
                gv.mod.indexOfSouthEasternNeighbour = -1;
                gv.mod.indexOfSouthWesternNeighbour = -1;

                gv.mod.seamlessModififierMinX = 0;
                mod.seamlessModififierMaxX = 0;
                gv.mod.seamlessModififierMinY = 0;
                gv.mod.seamlessModififierMaxY = 0;

                #region neighbours
                if ((gv.mod.currentArea.northernNeighbourArea != "") && (gv.mod.PlayerLocationY <= gv.playerOffsetY))
                {
                    gv.mod.seamlessModififierMinY = gv.playerOffsetY - gv.mod.PlayerLocationY;
                    for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                    {
                        if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.currentArea.northernNeighbourArea)
                        {
                            gv.mod.indexOfNorthernNeighbour = i;
                        }
                    }

                    if (gv.mod.moduleAreasObjects[gv.mod.indexOfNorthernNeighbour].easternNeighbourArea != "")
                    {
                        if (gv.mod.PlayerLocationX > (gv.mod.currentArea.MapSizeX - gv.playerOffsetX - 1))
                        {
                            gv.mod.seamlessModififierMaxX = gv.mod.PlayerLocationX - (gv.mod.currentArea.MapSizeX - gv.playerOffsetX - 1);
                        }
                        for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                        {
                            if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.moduleAreasObjects[gv.mod.indexOfNorthernNeighbour].easternNeighbourArea)
                            {
                                gv.mod.indexOfNorthEasternNeighbour = i;
                            }
                        }
                    }

                    if (gv.mod.moduleAreasObjects[gv.mod.indexOfNorthernNeighbour].westernNeighbourArea != "")
                    {
                        if (gv.mod.PlayerLocationX <= gv.playerOffsetX) 
                        {
                        gv.mod.seamlessModififierMinX = gv.playerOffsetX - gv.mod.PlayerLocationX;
                        }

                        for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                        {
                            if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.moduleAreasObjects[gv.mod.indexOfNorthernNeighbour].westernNeighbourArea)
                            {
                                gv.mod.indexOfNorthWesternNeighbour = i;
                            }
                        }
                    }
                }

                if ((gv.mod.currentArea.southernNeighbourArea != "") && (gv.mod.PlayerLocationY > (gv.mod.currentArea.MapSizeY - gv.playerOffsetY - 1)))
                {

                    gv.mod.seamlessModififierMaxY = gv.mod.PlayerLocationY - (gv.mod.currentArea.MapSizeY - gv.playerOffsetY - 1);
                    for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                    {
                        if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.currentArea.southernNeighbourArea)
                        {
                            gv.mod.indexOfSouthernNeighbour = i;
                        }
                    }

                    if (gv.mod.moduleAreasObjects[gv.mod.indexOfSouthernNeighbour].easternNeighbourArea != "")
                    {
                        if (gv.mod.PlayerLocationX > (gv.mod.currentArea.MapSizeX - gv.playerOffsetX - 1))
                        {
                            gv.mod.seamlessModififierMaxX = gv.mod.PlayerLocationX - (gv.mod.currentArea.MapSizeX - gv.playerOffsetX - 1);
                        }
                        for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                        {
                            if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.moduleAreasObjects[gv.mod.indexOfSouthernNeighbour].easternNeighbourArea)
                            {
                                gv.mod.indexOfSouthEasternNeighbour = i;
                            }
                        }
                    }

                    if (gv.mod.moduleAreasObjects[gv.mod.indexOfSouthernNeighbour].westernNeighbourArea != "")
                    {
                        if (gv.mod.PlayerLocationX <= gv.playerOffsetX)
                        {
                            gv.mod.seamlessModififierMinX = gv.playerOffsetX - gv.mod.PlayerLocationX;
                        }
                        for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                        {
                            if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.moduleAreasObjects[gv.mod.indexOfSouthernNeighbour].westernNeighbourArea)
                            {
                                gv.mod.indexOfSouthWesternNeighbour = i;
                            }
                        }
                    }
                }

                if ((gv.mod.currentArea.westernNeighbourArea != "") && (gv.mod.PlayerLocationX <= gv.playerOffsetX))
                {
                    gv.mod.seamlessModififierMinX = gv.playerOffsetX - gv.mod.PlayerLocationX;
                    for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                    {
                        if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.currentArea.westernNeighbourArea)
                        {
                            gv.mod.indexOfWesternNeighbour = i;
                        }
                    }

                    if (gv.mod.moduleAreasObjects[gv.mod.indexOfWesternNeighbour].northernNeighbourArea != "")
                    {
                        
                        if (gv.mod.PlayerLocationY <= gv.playerOffsetY)
                        {
                            gv.mod.seamlessModififierMinY = gv.playerOffsetY - gv.mod.PlayerLocationY;
                        }
                        
                        for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                        {
                            if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.moduleAreasObjects[gv.mod.indexOfWesternNeighbour].northernNeighbourArea)
                            {
                                gv.mod.indexOfNorthWesternNeighbour = i;
                            }
                        }
                    }

                    if (gv.mod.moduleAreasObjects[gv.mod.indexOfWesternNeighbour].southernNeighbourArea != "")
                    {

                        if (gv.mod.PlayerLocationY > (gv.mod.currentArea.MapSizeY - gv.playerOffsetY - 1))
                        {
                            gv.mod.seamlessModififierMaxY = gv.mod.PlayerLocationY - (gv.mod.currentArea.MapSizeY - gv.playerOffsetY - 1);
                        }
                        
                        for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                        {
                            if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.moduleAreasObjects[gv.mod.indexOfWesternNeighbour].southernNeighbourArea)
                            {
                                gv.mod.indexOfSouthWesternNeighbour = i;
                            }
                        }
                    }
                }

                if ((gv.mod.currentArea.easternNeighbourArea != "") && (gv.mod.PlayerLocationX > (gv.mod.currentArea.MapSizeX - gv.playerOffsetX - 1)))
                {
                    gv.mod.seamlessModififierMaxX = gv.mod.PlayerLocationX - (gv.mod.currentArea.MapSizeX - gv.playerOffsetX - 1);
                    for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                    {
                        if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.currentArea.easternNeighbourArea)
                        {
                            gv.mod.indexOfEasternNeighbour = i;
                        }
                    }

                    if (gv.mod.moduleAreasObjects[gv.mod.indexOfEasternNeighbour].northernNeighbourArea != "")
                    {
                        if (gv.mod.PlayerLocationY <= gv.playerOffsetY)
                        {
                            gv.mod.seamlessModififierMinY = gv.playerOffsetY - gv.mod.PlayerLocationY;
                        }
                        
                        for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                        {
                            if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.moduleAreasObjects[gv.mod.indexOfEasternNeighbour].northernNeighbourArea)
                            {
                                gv.mod.indexOfNorthEasternNeighbour = i;
                            }
                        }
                    }

                    if (gv.mod.moduleAreasObjects[gv.mod.indexOfEasternNeighbour].southernNeighbourArea != "")
                    {
                        if (gv.mod.PlayerLocationY > (gv.mod.currentArea.MapSizeY - gv.playerOffsetY - 1))
                        {
                            gv.mod.seamlessModififierMaxY = gv.mod.PlayerLocationY - (gv.mod.currentArea.MapSizeY - gv.playerOffsetY - 1);
                        }
                        for (int i = 0; i < gv.mod.moduleAreasObjects.Count; i++)
                        {
                            if (gv.mod.moduleAreasObjects[i].Filename == gv.mod.moduleAreasObjects[gv.mod.indexOfEasternNeighbour].southernNeighbourArea)
                            {
                                gv.mod.indexOfSouthEasternNeighbour = i;
                            }
                        }
                    }
                }
                #endregion
                //foreach (Area a in gv.mod.moduleAreasObjects)

                int minX = mod.PlayerLocationX - gv.playerOffsetX - 3; //using -2 in case a large tile (3x3) needs to start off the visible map space to be seen
                if (minX < -gv.mod.seamlessModififierMinX -1) { minX = -gv.mod.seamlessModififierMinX - 1; }
                int minY = mod.PlayerLocationY - gv.playerOffsetY - 3; //using -2 in case a large tile (3x3) needs to start off the visible map space to be seen
                if (minY < -gv.mod.seamlessModififierMinY - 1) { minY = -gv.mod.seamlessModififierMinY - 1; }

                int maxX = mod.PlayerLocationX + gv.playerOffsetX + 1;
                if (maxX > this.mod.currentArea.MapSizeX + gv.mod.seamlessModififierMaxX) { maxX = this.mod.currentArea.MapSizeX + gv.mod.seamlessModififierMaxX; }
                int maxY = mod.PlayerLocationY + gv.playerOffsetY +1;
                if (maxY > this.mod.currentArea.MapSizeY + gv.mod.seamlessModififierMaxY) { maxY = this.mod.currentArea.MapSizeY + gv.mod.seamlessModififierMaxY; }

                #region Draw Layer 1
                for (int x = minX; x < maxX; x++)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        //Tile tile = mod.currentArea.Tiles[y * mod.currentArea.MapSizeX + x];
                        bool situationFound = false;
                        bool drawTile = true;
                        int index = -1;
                        Tile tile = new Tile();

                        //nine situations where a tile can be:
                        //tile on north-western map (diagonal situation)
                        if ((x < 0) && (y < 0) && (!situationFound))
                        {
                            situationFound = true;
                            if (gv.mod.indexOfNorthWesternNeighbour != -1)
                            {
                                int transformedX = mod.moduleAreasObjects[gv.mod.indexOfNorthWesternNeighbour].MapSizeX + x;
                                int transformedY = mod.moduleAreasObjects[gv.mod.indexOfNorthWesternNeighbour].MapSizeY + y;
                                tile = mod.moduleAreasObjects[gv.mod.indexOfNorthWesternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfNorthWesternNeighbour].MapSizeX + transformedX];
                                index = gv.mod.indexOfNorthWesternNeighbour;

                                //now we know the potentially shaded tile, we need information on how it is shaded
                                //this requires information on the height levels of all neighbouring tiles, relative to the current tile's height
                                //this requires a loop checking the tiles neighbouring the current tile
                                //main difficulty is that the neighbouring tiles can very well be on different maps than the currrent tile
                                //consequence is we will need to nest the above mechanism again 
                                for (int nx = -1; nx < 2; nx++)
                                {
                                    for (int ny = -1; ny < 2; ny++)
                                    {
                                        //nine situations where a shadow granting tile can be:
                                        //shadow granting tile on north-western map (diagonal situation)
                                        if ((x + nx < 0) && (y + ny < 0) && (!situationFound))
                                        {

                                        }
                                    }
                                }
                            }
                            else
                            {
                                drawTile = false;
                            }
                        }
                        //tile on south-westernmap (diagonal situation)
                        if ((x < 0) && (y > (gv.mod.currentArea.MapSizeY - 1)) && (!situationFound))
                        {
                            situationFound = true;
                            if (gv.mod.indexOfSouthWesternNeighbour != -1)
                            {
                                int transformedX = mod.moduleAreasObjects[gv.mod.indexOfSouthWesternNeighbour].MapSizeX + x;
                                int transformedY = y - gv.mod.currentArea.MapSizeY;
                                tile = mod.moduleAreasObjects[gv.mod.indexOfSouthWesternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfSouthWesternNeighbour].MapSizeX + transformedX];
                                index = gv.mod.indexOfSouthWesternNeighbour;
                            }
                            else
                            {
                                drawTile = false;
                            }
                        }
                        //tile on south-easternmap (diagonal situation)
                        if ((x > (gv.mod.currentArea.MapSizeX - 1)) && (y > (gv.mod.currentArea.MapSizeY - 1)) && (!situationFound))
                        {
                            situationFound = true;
                            if (gv.mod.indexOfSouthEasternNeighbour != -1)
                            {
                                int transformedX = x - gv.mod.currentArea.MapSizeX;
                                int transformedY = y - gv.mod.currentArea.MapSizeY;
                                tile = mod.moduleAreasObjects[gv.mod.indexOfSouthEasternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfSouthEasternNeighbour].MapSizeX + transformedX];
                                index = gv.mod.indexOfSouthEasternNeighbour;
                            }
                            else
                            {
                                drawTile = false;
                            }
                        }
                        //tile on north-easternmap (diagonal situation)
                        if ((x > (gv.mod.currentArea.MapSizeX - 1)) && (y < 0) && (!situationFound))
                        {
                            situationFound = true;
                            if (gv.mod.indexOfNorthEasternNeighbour != -1)
                            {
                                int transformedX = x - gv.mod.currentArea.MapSizeX;
                                int transformedY = mod.moduleAreasObjects[gv.mod.indexOfNorthEasternNeighbour].MapSizeY + y;
                                tile = mod.moduleAreasObjects[gv.mod.indexOfNorthEasternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfNorthEasternNeighbour].MapSizeX + transformedX];
                                index = gv.mod.indexOfNorthEasternNeighbour;
                            }
                            else
                            {
                                drawTile = false;
                            }
                        }
                        //tile on western map
                        if ((x < 0) && (!situationFound))
                        {
                            situationFound = true;
                            if (gv.mod.indexOfWesternNeighbour != -1)
                            {
                                int transformedX = mod.moduleAreasObjects[gv.mod.indexOfWesternNeighbour].MapSizeX + x;
                                int transformedY = y;
                                tile = mod.moduleAreasObjects[gv.mod.indexOfWesternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfWesternNeighbour].MapSizeX + transformedX];
                                index = gv.mod.indexOfWesternNeighbour;
                            }
                            else
                            {
                                drawTile = false;
                            }
                        }
                        //tile on southern map
                        if ((y > (gv.mod.currentArea.MapSizeY - 1)) && (!situationFound))
                        {
                            situationFound = true;
                            if (gv.mod.indexOfSouthernNeighbour != -1)
                            {
                                int transformedX = x;
                                int transformedY = y - gv.mod.currentArea.MapSizeY;
                                tile = mod.moduleAreasObjects[gv.mod.indexOfSouthernNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfSouthernNeighbour].MapSizeX + transformedX];
                                index = gv.mod.indexOfSouthernNeighbour;
                            }
                            else
                            {
                                drawTile = false;
                            }
                        }
                        //tile on eastern map
                        if ((x > (gv.mod.currentArea.MapSizeX - 1)) && (!situationFound))
                        {
                            situationFound = true;
                            if (gv.mod.indexOfEasternNeighbour != -1)
                            {
                                int transformedX = x - gv.mod.currentArea.MapSizeX;
                                int transformedY = y;
                                tile = mod.moduleAreasObjects[gv.mod.indexOfEasternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfEasternNeighbour].MapSizeX + transformedX];
                                index = gv.mod.indexOfEasternNeighbour;
                            }
                            else
                            {
                                drawTile = false;
                            }
                        }
                        //tile on northern map
                        if ((y < 0) && (!situationFound))
                        {
                            situationFound = true;
                            if (gv.mod.indexOfNorthernNeighbour != -1)
                            {
                                int transformedX = x;
                                int transformedY = mod.moduleAreasObjects[gv.mod.indexOfNorthernNeighbour].MapSizeY + y;
                                tile = mod.moduleAreasObjects[gv.mod.indexOfNorthernNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfNorthernNeighbour].MapSizeX + transformedX];
                                index = gv.mod.indexOfNorthernNeighbour;
                            }
                            else
                            {
                                drawTile = false;
                            }
                        }
                        //tile is on current map
                        if (!situationFound)
                        {
                            tile = mod.currentArea.Tiles[y * mod.currentArea.MapSizeX + x];
                        }

                        if (drawTile)
                        {
                            try
                            {

                                bool tileBitmapIsLoadedAlready = false;
                                int indexOfLoadedTile = -1;
                                for (int i = 0; i < gv.mod.loadedTileBitmapsNames.Count; i++)
                                {
                                    if (gv.mod.loadedTileBitmapsNames[i] == tile.Layer1Filename)
                                    {
                                        tileBitmapIsLoadedAlready = true;
                                        indexOfLoadedTile = i;
                                        break;
                                    }
                                }

                                //hurghx
                                if (!tileBitmapIsLoadedAlready)
                                {
                                    gv.mod.loadedTileBitmapsNames.Add(tile.Layer1Filename);
                                    string backup = mod.currentArea.sourceBitmapName;
                                    if (index != -1)
                                    {
                                        mod.currentArea.sourceBitmapName = mod.moduleAreasObjects[index].sourceBitmapName;
                                    }
                                    tile.tileBitmap1 = gv.cc.LoadBitmap(tile.Layer1Filename);
                                    //tile.tileBitmap0 = gv.cc.LoadBitmapSubdirectory(tile.Layer0Filename, gv.mod.currentArea);
                                    mod.currentArea.sourceBitmapName = backup;

                                    //tile.tileBitmap1 = gv.cc.LoadBitmap(tile.Layer1Filename);

                                    int tlX = (x - mod.PlayerLocationX + gv.playerOffsetX) * gv.squareSize;
                                    int tlY = (y - mod.PlayerLocationY + gv.playerOffsetY) * gv.squareSize;
                                    float scalerX = tile.tileBitmap1.PixelSize.Width / 100;
                                    float scalerY = tile.tileBitmap1.PixelSize.Height / 100;
                                    int brX = (int)(gv.squareSize * scalerX);
                                    int brY = (int)(gv.squareSize * scalerY);
                                    IbRect src = new IbRect(0, 0, tile.tileBitmap1.PixelSize.Width, tile.tileBitmap1.PixelSize.Height);
                                    IbRect dst = new IbRect(tlX + gv.oXshift + mapStartLocXinPixels, tlY, brX, brY);

                                    gv.mod.loadedTileBitmaps.Add(tile.tileBitmap1);
                                    gv.DrawBitmap(tile.tileBitmap1, src, dst);
                                }
                                else
                                {
                                    int tlX = (x - mod.PlayerLocationX + gv.playerOffsetX) * gv.squareSize;
                                    int tlY = (y - mod.PlayerLocationY + gv.playerOffsetY) * gv.squareSize;
                                    float scalerX = gv.mod.loadedTileBitmaps[indexOfLoadedTile].PixelSize.Width / 100;
                                    float scalerY = gv.mod.loadedTileBitmaps[indexOfLoadedTile].PixelSize.Height / 100;
                                    int brX = (int)(gv.squareSize * scalerX);
                                    int brY = (int)(gv.squareSize * scalerY);
                                    IbRect src = new IbRect(0, 0, gv.mod.loadedTileBitmaps[indexOfLoadedTile].PixelSize.Width, gv.mod.loadedTileBitmaps[indexOfLoadedTile].PixelSize.Height);
                                    IbRect dst = new IbRect(tlX + gv.oXshift + mapStartLocXinPixels, tlY, brX, brY);

                                    gv.DrawBitmap(gv.mod.loadedTileBitmaps[indexOfLoadedTile], src, dst);
                                }

                                //gv.DrawBitmap(gv.cc.tileBitmapList[tile.Layer1Filename], src, dst);
                            }
                            catch { }
                        }
                    }
                }
                #endregion
                
                #endregion
            }
            else //old system using single image background and no load tile images on demand
            {
                #region old system
                int minX = mod.PlayerLocationX - gv.playerOffsetX - 2; //using -2 in case a large tile (3x3) needs to start off the visible map space to be seen
                if (minX < 0) { minX = 0; }
                int minY = mod.PlayerLocationY - gv.playerOffsetY - 2; //using -2 in case a large tile (3x3) needs to start off the visible map space to be seen
                if (minY < 0) { minY = 0; }

                int maxX = mod.PlayerLocationX + gv.playerOffsetX + 1;
                if (maxX > this.mod.currentArea.MapSizeX) { maxX = this.mod.currentArea.MapSizeX; }
                int maxY = mod.PlayerLocationY + gv.playerOffsetY + 2; // use 2 so that extends down to bottom of screen
                if (maxY > this.mod.currentArea.MapSizeY) { maxY = this.mod.currentArea.MapSizeY; }

                #region Draw Layer 1
                for (int x = minX; x < maxX; x++)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        Tile tile = mod.currentArea.Tiles[y * mod.currentArea.MapSizeX + x];
                        int tlX = (x - mod.PlayerLocationX + gv.playerOffsetX) * gv.squareSize;
                        int tlY = (y - mod.PlayerLocationY + gv.playerOffsetY) * gv.squareSize;
                        float scalerX = gv.cc.GetFromTileBitmapList(tile.Layer1Filename).PixelSize.Width / 100;
                        float scalerY = gv.cc.GetFromTileBitmapList(tile.Layer1Filename).PixelSize.Height / 100;
                        int brX = (int)(gv.squareSize * scalerX);
                        int brY = (int)(gv.squareSize * scalerY);

                        try
                        {
                            IbRect src = new IbRect(0, 0, gv.cc.GetFromTileBitmapList(tile.Layer1Filename).PixelSize.Width, gv.cc.GetFromTileBitmapList(tile.Layer1Filename).PixelSize.Height);
                            IbRect dst = new IbRect(tlX + gv.oXshift + mapStartLocXinPixels, tlY, brX, brY);
                            gv.DrawBitmap(gv.cc.GetFromTileBitmapList(tile.Layer1Filename), src, dst, tile.Layer1Rotate, tile.Layer1Mirror, tile.Layer1Xshift, tile.Layer1Yshift, tile.Layer1Xscale, tile.Layer1Yscale);
                        }
                        catch { }
                    }
                }
                #endregion
                #region Draw Layer 2
                for (int x = minX; x < maxX; x++)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        Tile tile = mod.currentArea.Tiles[y * mod.currentArea.MapSizeX + x];
                        int tlX = (x - mod.PlayerLocationX + gv.playerOffsetX) * gv.squareSize;
                        int tlY = (y - mod.PlayerLocationY + gv.playerOffsetY) * gv.squareSize;
                        float scalerX = gv.cc.GetFromTileBitmapList(tile.Layer2Filename).PixelSize.Width / 100;
                        float scalerY = gv.cc.GetFromTileBitmapList(tile.Layer2Filename).PixelSize.Height / 100;
                        int brX = (int)(gv.squareSize * scalerX);
                        int brY = (int)(gv.squareSize * scalerY);

                        try
                        {
                            IbRect src = new IbRect(0, 0, gv.cc.GetFromTileBitmapList(tile.Layer2Filename).PixelSize.Width, gv.cc.GetFromTileBitmapList(tile.Layer2Filename).PixelSize.Height);
                            IbRect dst = new IbRect(tlX + gv.oXshift + mapStartLocXinPixels, tlY, brX, brY);
                            gv.DrawBitmap(gv.cc.GetFromTileBitmapList(tile.Layer2Filename), src, dst, tile.Layer2Rotate, tile.Layer2Mirror, tile.Layer2Xshift, tile.Layer2Yshift, tile.Layer2Xscale, tile.Layer2Yscale);
                        }
                        catch { }
                    }
                }
                #endregion
                #region Draw Layer 3
                for (int x = minX; x < maxX; x++)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        Tile tile = mod.currentArea.Tiles[y * mod.currentArea.MapSizeX + x];
                        int tlX = (x - mod.PlayerLocationX + gv.playerOffsetX) * gv.squareSize;
                        int tlY = (y - mod.PlayerLocationY + gv.playerOffsetY) * gv.squareSize;
                        float scalerX = gv.cc.GetFromTileBitmapList(tile.Layer3Filename).PixelSize.Width / 100;
                        float scalerY = gv.cc.GetFromTileBitmapList(tile.Layer3Filename).PixelSize.Height / 100;
                        int brX = (int)(gv.squareSize * scalerX);
                        int brY = (int)(gv.squareSize * scalerY);

                        try
                        {
                            IbRect src = new IbRect(0, 0, gv.cc.GetFromTileBitmapList(tile.Layer3Filename).PixelSize.Width, gv.cc.GetFromTileBitmapList(tile.Layer3Filename).PixelSize.Height);
                            IbRect dst = new IbRect(tlX + gv.oXshift + mapStartLocXinPixels, tlY, brX, brY);
                            gv.DrawBitmap(gv.cc.GetFromTileBitmapList(tile.Layer3Filename), src, dst, tile.Layer3Rotate, tile.Layer3Mirror, tile.Layer3Xshift, tile.Layer3Yshift, tile.Layer3Xscale, tile.Layer3Yscale);
                        }
                        catch { }
                    }
                }
                #endregion
                #region Draw Layer 4
                for (int x = minX; x < maxX; x++)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        Tile tile = mod.currentArea.Tiles[y * mod.currentArea.MapSizeX + x];
                        int tlX = (x - mod.PlayerLocationX + gv.playerOffsetX) * gv.squareSize;
                        int tlY = (y - mod.PlayerLocationY + gv.playerOffsetY) * gv.squareSize;
                        float scalerX = gv.cc.GetFromTileBitmapList(tile.Layer4Filename).PixelSize.Width / 100;
                        float scalerY = gv.cc.GetFromTileBitmapList(tile.Layer4Filename).PixelSize.Height / 100;
                        int brX = (int)(gv.squareSize * scalerX);
                        int brY = (int)(gv.squareSize * scalerY);

                        try
                        {
                            IbRect src = new IbRect(0, 0, gv.cc.GetFromTileBitmapList(tile.Layer4Filename).PixelSize.Width, gv.cc.GetFromTileBitmapList(tile.Layer4Filename).PixelSize.Height);
                            IbRect dst = new IbRect(tlX + gv.oXshift + mapStartLocXinPixels, tlY, brX, brY);
                            gv.DrawBitmap(gv.cc.GetFromTileBitmapList(tile.Layer4Filename), src, dst, tile.Layer4Rotate, tile.Layer4Mirror, tile.Layer4Xshift, tile.Layer4Yshift, tile.Layer4Xscale, tile.Layer4Yscale);
                        }
                        catch { }
                    }
                }
                #endregion
                #region Draw Layer 5
                for (int x = minX; x < maxX; x++)
                {
                    for (int y = minY; y < maxY; y++)
                    {
                        Tile tile = mod.currentArea.Tiles[y * mod.currentArea.MapSizeX + x];
                        int tlX = (x - mod.PlayerLocationX + gv.playerOffsetX) * gv.squareSize;
                        int tlY = (y - mod.PlayerLocationY + gv.playerOffsetY) * gv.squareSize;
                        float scalerX = gv.cc.GetFromTileBitmapList(tile.Layer5Filename).PixelSize.Width / 100;
                        float scalerY = gv.cc.GetFromTileBitmapList(tile.Layer5Filename).PixelSize.Height / 100;
                        int brX = (int)(gv.squareSize * scalerX);
                        int brY = (int)(gv.squareSize * scalerY);

                        try
                        {
                            IbRect src = new IbRect(0, 0, gv.cc.GetFromTileBitmapList(tile.Layer5Filename).PixelSize.Width, gv.cc.GetFromTileBitmapList(tile.Layer5Filename).PixelSize.Height);
                            IbRect dst = new IbRect(tlX + gv.oXshift + mapStartLocXinPixels, tlY, brX, brY);
                            gv.DrawBitmap(gv.cc.GetFromTileBitmapList(tile.Layer5Filename), src, dst, tile.Layer5Rotate, tile.Layer5Mirror, tile.Layer5Xshift, tile.Layer5Yshift, tile.Layer5Xscale, tile.Layer5Yscale);
                        }
                        catch { }
                    }
                }
                #endregion

                #endregion
            }
        }

                */
                //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


                #region Draw Layer 0
                if (area.sourceBitmapName != "")
                {
                    if (firstTimeLoadingMap)
                    {
                        this.Cursor = Cursors.WaitCursor;
                    }
                    int tileCounter = 0;
                    for (int y = 0; y < area.MapSizeY; y++)
                    {
                        for (int x = 0; x < area.MapSizeX; x++)
                        {                            
                            Tile tile = area.Tiles[y * area.MapSizeX + x];
                            //Bitmap lyr0 = null;
                            try
                            {
                                tile.Layer0Filename = area.sourceBitmapName + tileCounter.ToString();
                            }
                            catch { }
                            if ((tile.Layer0Filename != null) && (tile.Layer0Filename != "") && tile.Layer0Filename != "t_blank")
                            {
                                /*if (area.isPNGMap)
                                {
                                    string bitMapPath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + tile.Layer0Filename + ".png";
                                }
                                if (area.isJPGMap)
                                {
                                    string bitMapPath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + tile.Layer0Filename + ".jpg";
                                }*/
                                tileCounter++;
                                /*try
                                {
                                    if (area.isPNGMap)
                                    {
                                        lyr0 = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + tile.Layer0Filename + ".png");
                                        //int block = 3;
                                    }
                                    if (area.isJPGMap)
                                    {
                                        lyr0 = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.sourceBitmapName + "\\" + tile.Layer0Filename + ".jpg");
                                        //int block = 3;
                                    }
                                }
                                catch
                                {

                                }*/

                                /*if (lyr0 != null)
                                {
                                    float scalerX = 1;
                                    float scalerY = 1;
                                    Rectangle src = new Rectangle(0, 0, lyr0.Width, lyr0.Height);
                                    Rectangle dst = new Rectangle(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                    device.DrawImage(lyr0, dst, src, GraphicsUnit.Pixel);
                                }*/
                                SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList(tile.Layer0Filename).PixelSize.Width, GetFromBitmapList(tile.Layer0Filename).PixelSize.Height);
                                SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, sqr, sqr);
                                DrawD2DBitmap(GetFromBitmapList(tile.Layer0Filename), src, dst, 0, false, 0, 0);
                            }                            
                        }
                    }
                    if (firstTimeLoadingMap)
                    {
                        this.Cursor = Cursors.Default;
                        firstTimeLoadingMap = false;
                    }                    
                }
                #endregion
            }
            #region Draw Layer 1
            if (checkBox1.Checked)
            {
                for (int y = 0; y < area.MapSizeY; y++)
                {
                    for (int x = 0; x < area.MapSizeX; x++)
                    {                        
                        Tile tile = area.Tiles[y * area.MapSizeX + x];
                        if (!tile.Layer1Filename.Equals("t_blank"))
                        {
                            float scalerX = GetFromBitmapList(tile.Layer1Filename).PixelSize.Width / 100;
                            float scalerY = GetFromBitmapList(tile.Layer1Filename).PixelSize.Height / 100;
                            SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList(tile.Layer1Filename).PixelSize.Width, GetFromBitmapList(tile.Layer1Filename).PixelSize.Height);
                            SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                            DrawD2DBitmap(GetFromBitmapList(tile.Layer1Filename), src, dst, tile.Layer1Rotate, tile.Layer1Mirror, tile.Layer1Xshift, tile.Layer1Yshift, tile.Layer1Xscale, tile.Layer1Yscale, tile.Layer1Opacity);
                        }
                    }
                }
            }
            #endregion
            #region Draw Layer 2
            if (checkBox2.Checked)
            {
                for (int y = 0; y < area.MapSizeY; y++)
                {
                    for (int x = 0; x < area.MapSizeX; x++)
                    {
                        Tile tile = area.Tiles[y * area.MapSizeX + x];
                        if (!tile.Layer2Filename.Equals("t_blank"))
                        {
                            float scalerX = GetFromBitmapList(tile.Layer2Filename).PixelSize.Width / 100;
                            float scalerY = GetFromBitmapList(tile.Layer2Filename).PixelSize.Height / 100;
                            SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList(tile.Layer2Filename).PixelSize.Width, GetFromBitmapList(tile.Layer2Filename).PixelSize.Height);
                            SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                            DrawD2DBitmap(GetFromBitmapList(tile.Layer2Filename), src, dst, tile.Layer2Rotate, tile.Layer2Mirror, tile.Layer2Xshift, tile.Layer2Yshift, tile.Layer2Xscale, tile.Layer2Yscale, tile.Layer2Opacity);
                        }
                    }
                }
            }
            #endregion
            #region Draw Layer 3
            if (checkBox3.Checked)
            {
                for (int y = 0; y < area.MapSizeY; y++)
                {
                    for (int x = 0; x < area.MapSizeX; x++)
                    {
                        Tile tile = area.Tiles[y * area.MapSizeX + x];
                        if (!tile.Layer3Filename.Equals("t_blank"))
                        {
                            float scalerX = GetFromBitmapList(tile.Layer3Filename).PixelSize.Width / 100;
                            float scalerY = GetFromBitmapList(tile.Layer3Filename).PixelSize.Height / 100;
                            SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList(tile.Layer3Filename).PixelSize.Width, GetFromBitmapList(tile.Layer3Filename).PixelSize.Height);
                            SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                            DrawD2DBitmap(GetFromBitmapList(tile.Layer3Filename), src, dst, tile.Layer3Rotate, tile.Layer3Mirror, tile.Layer3Xshift, tile.Layer3Yshift, tile.Layer3Xscale, tile.Layer3Yscale, tile.Layer3Opacity);

                        }
                    }
                }
            }
            #endregion
            #region Draw Layer 4
            if (checkBox4.Checked)
            {
                for (int y = 0; y < area.MapSizeY; y++)
                {
                    for (int x = 0; x < area.MapSizeX; x++)
                    {
                        Tile tile = area.Tiles[y * area.MapSizeX + x];
                        if (!tile.Layer4Filename.Equals("t_blank"))
                        {
                            float scalerX = GetFromBitmapList(tile.Layer4Filename).PixelSize.Width / 100;
                            float scalerY = GetFromBitmapList(tile.Layer4Filename).PixelSize.Height / 100;
                            SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList(tile.Layer4Filename).PixelSize.Width, GetFromBitmapList(tile.Layer4Filename).PixelSize.Height);
                            SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                            DrawD2DBitmap(GetFromBitmapList(tile.Layer4Filename), src, dst, tile.Layer4Rotate, tile.Layer4Mirror, tile.Layer4Xshift, tile.Layer4Yshift, tile.Layer4Xscale, tile.Layer4Yscale, tile.Layer4Opacity);
                        }
                    }
                }
            }
            #endregion

            #region Draw Entrance lights
            for (int y = 0; y < area.MapSizeY; y++)
            {
                for (int x = 0; x < area.MapSizeX; x++)
                {
                    Tile tile = area.Tiles[y * area.MapSizeX + x];
                    SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList("longShadow").PixelSize.Width, GetFromBitmapList("longShadow").PixelSize.Height);
                    SharpDX.RectangleF dstNorth = new SharpDX.RectangleF(x * sqr, (y - 1) * sqr, sqr, sqr);
                    SharpDX.RectangleF dstEast = new SharpDX.RectangleF((x + 1) * sqr, y * sqr, sqr, sqr);
                    SharpDX.RectangleF dstSouth = new SharpDX.RectangleF(x * sqr, (y + 1) * sqr, sqr, sqr);
                    SharpDX.RectangleF dstWest = new SharpDX.RectangleF((x - 1) * sqr, y * sqr, sqr, sqr);
                    SharpDX.RectangleF dst = new SharpDX.RectangleF((x) * sqr, y * sqr, sqr, sqr);

                    //areaTempMaster.Tiles[triggerOnLink.TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + triggerOnLink.TriggerSquaresList[0].X].transitionToMasterDirection

                    //our entrance light is an exit light and is painted right n top of trigger
                    //if (area.masterOfThisArea != "none")
                    //{

                    //changela2
                        if (tile.transitionToMasterDirection == "E")
                        {
                        if (area.masterOfThisArea != "none")
                        {
                            //krah
                            DrawD2DBitmap(GetFromBitmapList("black_tile"), src, dst, 0, false, 0, 0, 0, 0, 0.5f);

                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dst, 90, false, 0, 0, 1, 1, 0.3f);
                        }
                        else
                        {
                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstEast, 90, false, 0, 0, 1, 1, 0.3f);
                        }

                        }
                        if (tile.transitionToMasterDirection == "W")
                        {
                        if (area.masterOfThisArea != "none")
                        {
                            //krah
                            DrawD2DBitmap(GetFromBitmapList("black_tile"), src, dst, 0, false, 0, 0, 0, 0, 0.5f);

                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dst, 270, false, 0, 0, 1, 1, 0.3f);
                        }
                        else
                        {
                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstWest, 270, false, 0, 0, 1, 1, 0.3f);
                        }
                        }
                        if (tile.transitionToMasterDirection == "N")
                        {
                        if (area.masterOfThisArea != "none")
                        {
                            //krah
                            DrawD2DBitmap(GetFromBitmapList("black_tile"), src, dst, 0, false, 0, 0, 0, 0, 0.5f);

                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dst, 0, false, 0, 0, 1, 1, 0.3f);
                        }
                        else
                        {
                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstNorth, 0, false, 0, 0, 1, 1, 0.3f);
                        }
                        }
                        if (tile.transitionToMasterDirection == "S")
                        {
                        if (area.masterOfThisArea != "none")
                        {
                            //krah
                            DrawD2DBitmap(GetFromBitmapList("black_tile"), src, dst, 0, false, 0, 0, 0, 0, 0.5f);

                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dst, 180, false, 0, 0, 1, 1, 0.3f);
                        }
                        else
                        {
                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstSouth, 180, false, 0, 0, 1, 1, 0.3f);
                        }
                        }
                    //}

                    /*
                    //our entrance light is an really an entrance light and is painted on neighbouring square of trigger
                    if (area.masterOfThisArea == "none")
                    {
                        if (tile.transitionToMasterDirection == "N")
                        {
                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstNorth, 0, false, 0, 0, 1, 1, 0.25f);
                        }
                        if (tile.transitionToMasterDirection == "S")
                        {
                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstSouth, 180, false, 0, 0, 1, 1, 0.25f);
                        }
                        if (tile.transitionToMasterDirection == "W")
                        {
                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstWest, 270, false, 0, 0, 1, 1, 0.25f);
                        }
                        if (tile.transitionToMasterDirection == "E")
                        {
                            DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstEast, 90, false, 0, 0, 1, 1, 0.25f);
                        }
                    }
                    */

                    if (tile.isEWBridge)
                    {
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstNorth, 0, false, 0, 0,1,1,0.3f);
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstSouth, 180, false, 0, 0,1,1,0.3f);
                        //DrawD2DBitmap(GetFromBitmapList("shortShadow"), src, dst, 180, false, 0, 0, 1, 1, 0.6f);
                    }

                    if (tile.isNSBridge)
                    {
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstWest, 270, false, 0, 0,1,1,0.3f);
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth2"), src, dstEast, 90, false, 0, 0,1,1,0.3f);
                    }
                }
            }

            #endregion
            #region Draw Layer 5
            if (checkBox5.Checked)
            {
                for (int y = 0; y < area.MapSizeY; y++)
                {
                    for (int x = 0; x < area.MapSizeX; x++)
                    {
                        Tile tile = area.Tiles[y * area.MapSizeX + x];
                        if (!tile.Layer5Filename.Equals("t_blank"))
                        {
                            float scalerX = GetFromBitmapList(tile.Layer5Filename).PixelSize.Width / 100;
                            float scalerY = GetFromBitmapList(tile.Layer5Filename).PixelSize.Height / 100;
                            SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList(tile.Layer5Filename).PixelSize.Width, GetFromBitmapList(tile.Layer5Filename).PixelSize.Height);
                            SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                            DrawD2DBitmap(GetFromBitmapList(tile.Layer5Filename), src, dst, tile.Layer5Rotate, tile.Layer5Mirror, tile.Layer5Xshift, tile.Layer5Yshift, tile.Layer5Xscale, tile.Layer5Yscale, tile.Layer5Opacity);
                        }
                    }
                }
            }
            #endregion

            #region Draw Grid
            for (int y = 0; y < area.MapSizeY; y++)
            {
                for (int x = 0; x < area.MapSizeX; x++)
                {                    
                    Tile tile = area.Tiles[y * area.MapSizeX + x];                    
                    //draw square walkmesh and LoS stuff
                    //SharpDX.Direct2D1.Bitmap bm = GetFromBitmapList("walk_pass");
                    //Rectangle src = new Rectangle(0, 0, g_walkPass.Width, g_walkPass.Height);
                    //Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                    SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList("walk_pass").PixelSize.Width, GetFromBitmapList("walk_pass").PixelSize.Height);
                    SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, sqr, sqr);
                    if (chkGrid.Checked) //if show grid is turned on, draw grid squares
                    {
                        if (tile.LoSBlocked)
                        {
                            DrawD2DBitmap(GetFromBitmapList("los_block"), src, dst, 0, false, 0, 0);
                        }
                        if (tile.Walkable)
                        {
                            DrawD2DBitmap(GetFromBitmapList("walk_pass"), src, dst, 0, false, 0, 0);
                        }
                        else
                        {
                            DrawD2DBitmap(GetFromBitmapList("walk_block"), src, dst, 0, false, 0, 0);
                            
                        }

                        //DrawText((cnt + 1).ToString(), cspx + 5, cspy, scaler, SharpDX.Color.Yellow);


                        //height level system: toDO draw text for height level here
                        float scaler = 1.0f;
                        if (sqr == 50) { scaler = 15.0f; }
                        else if (sqr == 25) { scaler = 7.5f; }
                        else if (sqr == 10) { scaler = 3.0f; }
                        DrawText(tile.heightLevel.ToString(), dst.X, dst.Y, scaler, SharpDX.Color.Yellow);

                        if(tile.isRamp)
                        {
                            DrawText("R", dst.X + (sqr/4), dst.Y + (sqr/4), scaler * 1.1f, SharpDX.Color.Yellow);
                        }

                        if (tile.isEWBridge)
                        {
                            DrawText("EW", dst.X + (sqr / 4), dst.Y + (sqr / 4), scaler * 1.1f, SharpDX.Color.Yellow);
                        }

                        if (tile.isNSBridge)
                        {
                            DrawText("NS", dst.X + (sqr / 4), dst.Y + (sqr / 4), scaler * 1.1f, SharpDX.Color.Yellow);
                        }
                    }
                }
            }
            #endregion

            #region Draw Props
            foreach (Prop prpRef in area.Props)
            {
                SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList(prpRef.ImageFileName).PixelSize.Width, GetFromBitmapList(prpRef.ImageFileName).PixelSize.Height);
                SharpDX.RectangleF dst = new SharpDX.RectangleF(prpRef.LocationX * sqr, prpRef.LocationY * sqr, sqr, sqr);
                DrawD2DBitmap(GetFromBitmapList(prpRef.ImageFileName), src, dst, 0, !prpRef.PropFacingLeft, 0, 0);
            }
            #endregion
            #region Draw Triggers
            foreach (Trigger t in area.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    int dx = p.X * sqr;
                    int dy = p.Y * sqr;
                    //Pen pen = new Pen(Color.Orange, 2);
                    SharpDX.Color clr = SharpDX.Color.Orange;
                    if ((t.Event1Type == "encounter") || (t.Event2Type == "encounter") || (t.Event3Type == "encounter"))
                    {
                        clr = SharpDX.Color.Red;
                    }
                    else if (t.Event1Type == "conversation")
                    {
                        clr = SharpDX.Color.Yellow;
                    }
                    else if (t.Event1Type == "script")
                    {
                        clr = SharpDX.Color.Blue;
                    }
                    else if (t.Event1Type == "transition")
                    {
                        clr = SharpDX.Color.Lime;
                    }
                    SharpDX.RectangleF rect = new SharpDX.RectangleF(dx + 3, dy + 3, sqr - 6, sqr - 6);
                    DrawRectangle(rect, clr, 2);
                }
            }
            #endregion
            #region Draw Selection Box
            if (selectionBoxLocation.X != -1)
            {
                int dx = selectionBoxLocation.X * sqr;
                int dy = selectionBoxLocation.Y * sqr;
                SharpDX.RectangleF rect = new SharpDX.RectangleF(dx + 1, dy + 1, sqr - 2, sqr - 2);
                DrawRectangle(rect, SharpDX.Color.Magenta, 2);
            }
            #endregion
            #region Draw To Be Placed Prop/Tile
            if (prntForm.PropSelected)
            {
                try
                {
                    if (!selectedBitmapFilename.Equals(""))
                    {
                        SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList(selectedBitmapFilename).PixelSize.Width, GetFromBitmapList(selectedBitmapFilename).PixelSize.Height);
                        SharpDX.RectangleF dst = new SharpDX.RectangleF(gridX * sqr, gridY * sqr, sqr, sqr);
                        DrawD2DBitmap(GetFromBitmapList(selectedBitmapFilename), src, dst, 0, false, 0, 0, 0, 0);
                    }
                }
                catch (Exception ex) { MessageBox.Show("failed mouse move update to be placed prop: " + ex.ToString()); }
            }
            else if (rbtnPaintTile.Checked)
            {
                try
                {
                    if (!currentTileFilename.Equals(""))
                    {
                        float scalerX = GetFromBitmapList(currentTileFilename).PixelSize.Width / 100;
                        float scalerY = GetFromBitmapList(currentTileFilename).PixelSize.Height / 100;
                        SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList(currentTileFilename).PixelSize.Width, GetFromBitmapList(currentTileFilename).PixelSize.Height);
                        SharpDX.RectangleF dst = new SharpDX.RectangleF(gridX * sqr, gridY * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                        DrawD2DBitmap(GetFromBitmapList(currentTileFilename), src, dst, tileToBePlaced.angle, tileToBePlaced.mirror, tileToBePlaced.xshift, tileToBePlaced.yshift, tileToBePlaced.xscale, tileToBePlaced.yscale, tileToBePlaced.opacity);
                    }
                }
                catch (Exception ex) { MessageBox.Show("failed mouse move update to be placed tile: " + ex.ToString()); }
            }
            #endregion


            #region draw Height Shadows

            for (int y = 0; y < area.MapSizeY; y++)
            {
                for (int x = 0; x < area.MapSizeX; x++)
                {
                    Tile tile = area.Tiles[y * area.MapSizeX + x];
                    SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList("longShadow").PixelSize.Width, GetFromBitmapList("longShadow").PixelSize.Height);
                    SharpDX.RectangleF dst = new SharpDX.RectangleF(x * (sqr), y * sqr, (sqr), (sqr));
                    SharpDX.RectangleF dst2 = new SharpDX.RectangleF(x * (sqr), y * sqr, (sqr), (sqr));
                    SharpDX.RectangleF dst3 = new SharpDX.RectangleF(x * (sqr), y * sqr, sqr, sqr);
                    SharpDX.RectangleF dst4 = new SharpDX.RectangleF(x * (sqr), y * (sqr), sqr, sqr);
                    SharpDX.RectangleF dstNorth = new SharpDX.RectangleF(x * sqr, (y-1) * sqr, sqr, sqr);
                    SharpDX.RectangleF dstEast = new SharpDX.RectangleF((x+1) * sqr, y * sqr, sqr, sqr);
                    SharpDX.RectangleF dstSouth = new SharpDX.RectangleF(x * sqr, (y+1) * sqr, sqr, sqr);
                    SharpDX.RectangleF dstWest = new SharpDX.RectangleF((x-1) * sqr, y * sqr, sqr, sqr);

                    float relativeTileHeight = tile.heightLevel - area.averageHeightOnThisMap;
                    if (relativeTileHeight > 4)
                    {
                        relativeTileHeight = 4;
                    }
                    if (relativeTileHeight < -4)
                    {
                        relativeTileHeight = -4;
                    }
                    relativeTileHeight = relativeTileHeight / 10;

                    //bool allowHighLight = true;
                    //highlights
                    //tile.allowOppositeHighlight = true;
                    /*
                    if (area.masterOfThisArea == "none")
                    {
                        if (tile.transitionToMasterDirection  == "N")
                        {
                            DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dst4, 90, false, 0, -1, 1, 1, 1f);
                            //allowHighLight = false;
                        }
                        if (tile.transitionToMasterDirection == "E")
                        {
                            DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dst, 180, false, 0, -1, 1, 1, 1f);
                            //allowHighLight = false;
                        }
                        if (tile.transitionToMasterDirection == "S")
                        {
                            DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dst, 270, false, 0, -1, 1, 1, 1f);
                            //allowHighLight = false;
                        }
                        if (tile.transitionToMasterDirection == "W")
                        {
                            DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dst3, 0, false, 0, -1, 1, 1, 1f);
                            //allowHighLight = false;
                        }
                    }
                    */

                    //if (area.masterOfThisArea == "none")
                    //{
                    //changela
                        int xAdder = 1;
                        int yAdder = 1;
                        int xSubstractor = 1;
                        int ySubstractor = 1;

                        if (x == 0)
                        {
                            xSubstractor = 0;
                        }
                        if (y == 0)
                        {
                            ySubstractor = 0;
                        }

                        if (x == area.MapSizeX-1)
                        {
                            xAdder = 0;
                        }

                        if (y == area.MapSizeY - 1)
                        {
                            yAdder = 0;
                        }

                        if (area.Tiles[(y+yAdder) * area.MapSizeX + x].transitionToMasterDirection != "S")
                        {
                            if (tile.hasHighlightS)
                            {
                                if (tile.isEWBridge)
                                {
                                    DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dst4, 0, false, 0, -1, 1, 1, 1f);
                                }
                                else
                                {
                                DrawD2DBitmap(GetFromBitmapList("highlight90"), src, dst, 0, false, 0, 0, 1, 1, 0.5f + relativeTileHeight);
                                //DrawD2DBitmap(GetFromBitmapList("highlight90"), src, dst, 0, false, 0, 1, 1, 1, 0.5f + relativeTileHeight);

                            }
                            //DrawD2DBitmap
                        }
                        }

                        if (area.Tiles[(y - ySubstractor) * area.MapSizeX + x].transitionToMasterDirection != "N")
                        {
                            if (tile.hasHighlightN)
                            {
                                if (tile.isEWBridge)
                                {
                                    DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dst, 180, false, 0, 0, 1, 1, 1f);
                                }
                                else
                                {
                                    DrawD2DBitmap(GetFromBitmapList("highlight90"), src, dst, 180, false, 0, 1, 1, 1, 0.5f + relativeTileHeight);
                                }
                            }
                        }

                        if (area.Tiles[(y) * area.MapSizeX + x-xSubstractor].transitionToMasterDirection != "W")
                        {
                            if (tile.hasHighlightW)
                            {
                                if (tile.isNSBridge)
                                {
                                    DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dst, 90, false, 0, 0, 1, 1, 1f);
                                }
                                else
                                {
                                    DrawD2DBitmap(GetFromBitmapList("highlight90"), src, dst, 90, false, 1, 0, 1, 1, 0.5f + relativeTileHeight);
                                }
                            }
                        }

                        if (area.Tiles[(y) * area.MapSizeX + x + xAdder].transitionToMasterDirection != "E")
                        {
                            if (tile.hasHighlightE)
                            {
                                if (tile.isNSBridge)
                                {
                                    DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dst3, 270, false, -1, 0, 1, 1, 1f);
                                }
                                else
                                {
                                DrawD2DBitmap(GetFromBitmapList("highlight90"), src, dst, 270, false, 0, 0, 1, 1, 0.5f + relativeTileHeight);
                                //DrawD2DBitmap(GetFromBitmapList("highlight90"), src, dst, 270, false, 1, 0, 1, 1, 0.5f + relativeTileHeight);

                            }
                        }
                        }
                    //}

                    if (area.masterOfThisArea == "none")
                    {
                        //changela
                        if (tile.transitionToMasterDirection == "E")
                        {
                            DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstEast, 90, false, 0, 0, 1, 1, 1f);
                            //DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstEast, 90, false, 0, 1, 1, 1, 1f);
                            //DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstEast, 90, false, 0, -1, 1, 1, 1f);
                            //allowHighLight = false;
                        }
                        if (tile.transitionToMasterDirection == "S")
                        {
                            DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstSouth, 180, false, 0, 0, 1, 1, 1f);
                            //DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstSouth, 180, false, 0, 1, 1, 1, 1f);
                            //DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstSouth, 180, false, 0, -1, 1, 1, 1f);
                            //allowHighLight = false;
                        }
                        if (tile.transitionToMasterDirection == "W")
                        {
                            DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstWest, 270, false, 0, 0, 1, 1, 1f);
                            //DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstWest, 270, false, 0, 1, 1, 1, 1f);
                            //DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstWest, 270, false, 0, -1, 1, 1, 1f);

                            //allowHighLight = false;0
                        }
                        if (tile.transitionToMasterDirection == "N")
                        {
                            //DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstNorth, 0, false, 0, 0, 1, 1, 1f);
                            DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstNorth, 0, false, 0, 0, 1, 1, 1f);
                            //DrawD2DBitmap(GetFromBitmapList("highLightGreen"), src, dstNorth, 0, false, 0, 0, -1, 1, 1f);
                            //allowHighLight = false;
                        }
                    }

                    //urfeld
                    //stair shadows
                    if (tile.hasDownStairShadowN)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairShadow"), src, dst, 180, false, 0, 0);
                        if (!tile.isInShortShadeE && !tile.isInLongShadeE && !tile.isInMaxShadeE)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE == 0)
                            {
                                //dst = new SharpDX.RectangleF(x * sqr, y * sqr, sqr*0.5f, sqr);
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 180, false, 0, 0);
                                //dst = new SharpDX.RectangleF(x * sqr, y * sqr, sqr * 2f, sqr);
                            }
                        }
                        if (!tile.isInShortShadeW && !tile.isInLongShadeW && !tile.isInMaxShadeW)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW == 0)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 180, false, 0, 0);
                            }
                        }
                        //freedom, WIP
                        
                        if (!tile.isInShortShadeN && !tile.isInLongShadeN && !tile.isInMaxShadeN)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN == -2)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 90, false, 0, 0);
                            }
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN == -3)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 270, false, 0, 0);
                            }
                        }                       
                    }
                    if (tile.hasDownStairShadowE)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairShadow"), src, dst, 270, false, 0, 0);
                        if (!tile.isInShortShadeS && !tile.isInLongShadeS && !tile.isInMaxShadeS)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS == 0)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 270, false, 0, 0);
                            }
                        }
                        if (!tile.isInShortShadeN && !tile.isInLongShadeN && !tile.isInMaxShadeN)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN == 0)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 270, false, 0, 0);
                            }
                        }
                        if (!tile.isInShortShadeE && !tile.isInLongShadeE && !tile.isInMaxShadeE)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE == -2)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 0, false, 0, 0);
                            }
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE == -3)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 180, false, 0, 0);
                            }
                        }

                    }
                    if (tile.hasDownStairShadowS)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairShadow"), src, dst, 0, false, 0, 0);
                        if (!tile.isInShortShadeW && !tile.isInLongShadeW && !tile.isInMaxShadeW)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW == 0)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 0, false, 0, 0);
                            }
                        }
                        if (!tile.isInShortShadeE && !tile.isInLongShadeE && !tile.isInMaxShadeE)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE == 0)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 0, false, 0, 0);
                            }
                        }
                        if (!tile.isInShortShadeS && !tile.isInLongShadeS && !tile.isInMaxShadeS)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS == -2)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 90, false, 0, 0);
                            }
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS == -3)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 270, false, 0, 0);
                            }
                        }
                    }
                    if (tile.hasDownStairShadowW)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairShadow"), src, dst, 90, false, 0, 0);
                        if (!tile.isInShortShadeN && !tile.isInLongShadeN && !tile.isInMaxShadeN)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN == 0)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 90, false, 0, 0);
                            }
                        }
                        if (!tile.isInShortShadeS && !tile.isInLongShadeS && !tile.isInMaxShadeS)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS == 0)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 90, false, 0, 0);
                            }
                        }

                        if (!tile.isInShortShadeW && !tile.isInLongShadeW && !tile.isInMaxShadeW)
                        {
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW == -2)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 0, false, 0, 0);
                            }
                            if (tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW == -3)
                            {
                                DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 180, false, 0, 0);
                            }
                        }
                    }

                    if (tile.inRampShadowWest1Long)
                    {
                        DrawD2DBitmap(GetFromBitmapList("rightCurtain"), src, dst, 180, true, 0, 0);
                    }
                    if (tile.inRampShadowWest1Short)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 0, true, 0, 0);
                    }

                    if (tile.inRampShadowWest2Long)
                    {
                        DrawD2DBitmap(GetFromBitmapList("rightCurtain"), src, dst, 0, false, 0, 0);
                    }
                    if (tile.inRampShadowWest2Short)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 180, false, 0, 0);
                    }

                    if (tile.inRampShadowEast3Long)
                    {
                        DrawD2DBitmap(GetFromBitmapList("leftCurtain"), src, dst, 180, false, 0, 0);
                    }
                    if (tile.inRampShadowEast3Short)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 0, true, 0, 0);
                    }

                    if (tile.inRampShadowEast4Long)
                    {
                        DrawD2DBitmap(GetFromBitmapList("rightCurtain"), src, dst, 0, true, 0, 0);
                    }
                    if (tile.inRampShadowEast4Short)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 180, true, 0, 0);
                    }

                    if (tile.inRampShadowNorth5Long)
                    {
                        DrawD2DBitmap(GetFromBitmapList("rightCurtain"), src, dst, 270, true, 0, 0);
                    }
                    if (tile.inRampShadowNorth5Short)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 90, true, 0, 0);
                    }

                    if (tile.inRampShadowNorth6Long)
                    {
                        DrawD2DBitmap(GetFromBitmapList("leftCurtain"), src, dst, 90, false, 0, 0);
                    }
                    if (tile.inRampShadowNorth6Short)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 270, true, 0, 0);
                    }

                    if (tile.inRampShadowSouth7Long)
                    {
                        DrawD2DBitmap(GetFromBitmapList("leftCurtain"), src, dst, 270, false, 0, 0);
                    }
                    if (tile.inRampShadowSouth7Short)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowLeft"), src, dst2, 90, true, 0, 0);
                    }

                    if (tile.inRampShadowSouth8Long)
                    {
                        DrawD2DBitmap(GetFromBitmapList("rightCurtain"), src, dst, 90, true, 0, 0);
                    }
                    if (tile.inRampShadowSouth8Short)
                    {
                        DrawD2DBitmap(GetFromBitmapList("downStairFlankShadowRight"), src, dst2, 270, true, 0, 0);
                    }

                    if (tile.inSmallStairNEVertical)
                    {
                        DrawD2DBitmap(GetFromBitmapList("smallStairNEMirror"), src, dst2, 0, true, 0, 0);
                    }
                    if (tile.inSmallStairNEHorizontal)
                    {
                        DrawD2DBitmap(GetFromBitmapList("smallStairNENormal"), src, dst2, 90, true, 0, 0);
                    }
                    if (tile.inSmallStairSEVertical)
                    {
                        DrawD2DBitmap(GetFromBitmapList("smallStairNENormal"), src, dst2, 180, true, 0, 0);
                    }
                    if (tile.inSmallStairSEHorizontal)
                    {
                        DrawD2DBitmap(GetFromBitmapList("smallStairNEMirror"), src, dst2, 90, true, 0, 0);
                    }
                    if (tile.inSmallStairSWVertical)
                    {
                        DrawD2DBitmap(GetFromBitmapList("smallStairNEMirror"), src, dst2, 180, true, 0, 0);
                    }
                    if (tile.inSmallStairSWHorizontal)
                    {
                        DrawD2DBitmap(GetFromBitmapList("smallStairNENormal"), src, dst2, 270, true, 0, 0);
                    }
                    if (tile.inSmallStairNWVertical)
                    {
                        DrawD2DBitmap(GetFromBitmapList("smallStairNENormal"), src, dst2, 0, true, 0, 0);
                    }
                    if (tile.inSmallStairNWHorizontal)
                    {
                        DrawD2DBitmap(GetFromBitmapList("smallStairNEMirror"), src, dst2, 270, true, 0, 0);
                    }

                    //height shadows
                    if (tile.isInLongShadeN)
                    {
                        DrawD2DBitmap(GetFromBitmapList("longShadow"), src, dst, 180, false, 0, 0);
                    }
                    else if (tile.isInShortShadeN)
                    {
                        if (y - 1 >= 0)
                        {
                            //changela
                            if (!area.Tiles[(y - 1) * area.MapSizeX + x].isEWBridge && area.Tiles[(y-1) * area.MapSizeX + x].transitionToMasterDirection != "S")
                            {
                                DrawD2DBitmap(GetFromBitmapList("shortShadow"), src, dst, 180, false, 0, 0);
                            }
                            else
                            {
                                DrawD2DBitmap(GetFromBitmapList("bridgeShadow"), src, dst, 180, false, 0, 0, 1, 1, 1f);
                            }
                        }
                        else
                        {
                            DrawD2DBitmap(GetFromBitmapList("shortShadow"), src, dst, 180, false, 0, 0);
                        } 
                    }

                    if (tile.isInLongShadeE)
                    {
                            DrawD2DBitmap(GetFromBitmapList("longShadow"), src, dst, 270, false, 0, 0);
                    }
                    else if (tile.isInShortShadeE)
                    {
                        if (x + 1 <= area.MapSizeX - 1)
                        {
                            if (!area.Tiles[(y) * area.MapSizeX + (x + 1)].isNSBridge && area.Tiles[(y) * area.MapSizeX + x+1].transitionToMasterDirection != "W")
                            {
                                DrawD2DBitmap(GetFromBitmapList("shortShadow"), src, dst, 270, false, 0, 0);
                            }
                            else
                            {
                                DrawD2DBitmap(GetFromBitmapList("bridgeShadow"), src, dst, 270, false, 0, 0, 1, 1, 1f);
                            }
                        }
                        else
                        {
                            DrawD2DBitmap(GetFromBitmapList("shortShadow"), src, dst, 270, false, 0, 0);
                        }

                    }

                    if (tile.isInLongShadeS)
                    {
                        DrawD2DBitmap(GetFromBitmapList("longShadow"), src, dst, 0, false, 0, 0);
                    }
                    else if (tile.isInShortShadeS)
                    {
                        if (y + 1 <= area.MapSizeY - 1)
                        {
                            if (!area.Tiles[(y + 1) * area.MapSizeX + (x)].isEWBridge && area.Tiles[(y + 1) * area.MapSizeX + x].transitionToMasterDirection != "N")
                            {
                                DrawD2DBitmap(GetFromBitmapList("shortShadow"), src, dst, 0, false, 0, 0);
                            }
                            else
                            {
                                DrawD2DBitmap(GetFromBitmapList("bridgeShadow"), src, dst, 0, false, 0, 0, 1, 1, 1f);
                            }
                        }
                        else
                        {
                            DrawD2DBitmap(GetFromBitmapList("shortShadow"), src, dst, 0, false, 0, 0);
                        }
                    }
                    
                    if (tile.isInLongShadeW)
                    {
                        DrawD2DBitmap(GetFromBitmapList("longShadow"), src, dst, 90, false, 0, 0);
                    }
                    else if (tile.isInShortShadeW)
                    {
                        if (x - 1 >= 0)
                        {
                            if (!area.Tiles[(y) * area.MapSizeX + (x - 1)].isNSBridge && area.Tiles[(y) * area.MapSizeX + x-1].transitionToMasterDirection != "E")
                            {
                                DrawD2DBitmap(GetFromBitmapList("shortShadow"), src, dst, 90, false, 0, 0);
                            }
                            else
                            {
                                DrawD2DBitmap(GetFromBitmapList("bridgeShadow"), src, dst, 90, false, 0, 0, 1, 1, 1f);
                            }
                        }
                        else
                        {
                            DrawD2DBitmap(GetFromBitmapList("shortShadow"), src, dst, 90, false, 0, 0);
                        }
                    }

                    //if (!tile.isRamp)
                    //{
                        //if (!tile.inRampShadowWest1Short && !tile.inRampShadowWest1Long && !tile.inRampShadowWest2Short && !tile.inRampShadowWest2Long && !tile.inRampShadowNorth5Long && !tile.inRampShadowNorth5Short && !tile.inRampShadowNorth6Long && !tile.inRampShadowNorth6Short)
                        //{
                            if (!tile.isInLongShadeN && !tile.isInShortShadeN && !tile.isInLongShadeW && !tile.isInShortShadeW)
                            {
                                if (tile.isInMaxShadeNW)
                                {
                                    DrawD2DBitmap(GetFromBitmapList("corner3"), src, dst, 180, false, 0, 0);
                                }        
                                else
                                if (tile.isInLongShadeNW)
                                {
                                    if ((!tile.inRampShadowNorth5Long) && (!tile.inRampShadowWest2Long))
                                    {
                                        DrawD2DBitmap(GetFromBitmapList("longShadowCorner"), src, dst, 180, false, 0, 0);
                                    }
                                }
                                else if ((tile.isInShortShadeNW) && (!tile.inRampShadowNorth5Short) && (!tile.inRampShadowWest2Short) && (!tile.inRampShadowNorth5Long) && (!tile.inRampShadowWest2Long))
                                {
                                    DrawD2DBitmap(GetFromBitmapList("shortShadowCorner"), src, dst, 180, false, 0, 0);
                                }
                            }

                            else if (!tile.isInLongShadeN && !tile.isInLongShadeW)
                            {

                                if (tile.isInMaxShadeNW)
                                {
                                    DrawD2DBitmap(GetFromBitmapList("corner3"), src, dst, 180, false, 0, 0);
                                }
                                else
                                if (tile.isInLongShadeNW)
                                {
                                    if ((!tile.inRampShadowNorth5Long) && (!tile.inRampShadowWest2Long))
                                    {
                                        DrawD2DBitmap(GetFromBitmapList("longShadowCorner"), src, dst, 180, false, 0, 0);
                                    }
                                }
                            }
                        //}

                        //if (!tile.inRampShadowEast3Long && !tile.inRampShadowEast3Short && !tile.inRampShadowEast4Short && !tile.inRampShadowEast4Long && !tile.inRampShadowNorth5Long && !tile.inRampShadowNorth5Short && !tile.inRampShadowNorth6Long && !tile.inRampShadowNorth6Short)
                        //{
                            if (!tile.isInLongShadeN && !tile.isInShortShadeN && !tile.isInLongShadeE && !tile.isInShortShadeE)
                            {
                                if (tile.isInMaxShadeNE)
                                {
                                    DrawD2DBitmap(GetFromBitmapList("corner3"), src, dst, 270, false, 0, 0);
                                }
                                else
                                if (tile.isInLongShadeNE)
                                {
                                    if ((!tile.inRampShadowNorth6Long) && (!tile.inRampShadowEast4Long))
                                    {
                                        DrawD2DBitmap(GetFromBitmapList("longShadowCorner"), src, dst, 270, false, 0, 0);
                                    }
                                }
                                else if ((tile.isInShortShadeNE) && (!tile.inRampShadowEast4Short) && (!tile.inRampShadowNorth6Short) && (!tile.inRampShadowEast4Long) && (!tile.inRampShadowNorth6Long))
                                {
                                    DrawD2DBitmap(GetFromBitmapList("shortShadowCorner"), src, dst, 270, false, 0, 0);
                                }
                            }

                            else if (!tile.isInLongShadeN && !tile.isInLongShadeE)
                            {
                                if (tile.isInMaxShadeNE)
                                {
                                    DrawD2DBitmap(GetFromBitmapList("corner3"), src, dst, 270, false, 0, 0);
                                }
                                else
                                if (tile.isInLongShadeNE)
                                {
                                    if ((!tile.inRampShadowNorth6Long) && (!tile.inRampShadowEast4Long))
                                    {
                                        DrawD2DBitmap(GetFromBitmapList("longShadowCorner"), src, dst, 270, false, 0, 0);
                                    }
                                }
                            }
                        //}

                        //if (!tile.inRampShadowEast3Short && !tile.inRampShadowEast3Long && !tile.inRampShadowEast4Short && !tile.inRampShadowEast4Long && !tile.inRampShadowSouth7Long && !tile.inRampShadowSouth7Short && !tile.inRampShadowSouth8Long && !tile.inRampShadowSouth8Short)
                        //{
                            if (!tile.isInLongShadeS && !tile.isInShortShadeS && !tile.isInLongShadeE && !tile.isInShortShadeE)
                            {
                        if (tile.isInMaxShadeSE)
                        {
                            DrawD2DBitmap(GetFromBitmapList("corner3"), src, dst, 0, false, 0, 0);
                        }
                        else
                                if (tile.isInLongShadeSE)
                                {
                                    if ((!tile.inRampShadowSouth8Long) && (!tile.inRampShadowEast3Long))
                                    {
                                        DrawD2DBitmap(GetFromBitmapList("longShadowCorner"), src, dst, 0, false, 0, 0);
                                    }
                                }
                                else if ((tile.isInShortShadeSE) && (!tile.inRampShadowEast3Short) && (!tile.inRampShadowSouth8Short) && (!tile.inRampShadowEast3Long) && (!tile.inRampShadowSouth8Long))
                                {
                                    DrawD2DBitmap(GetFromBitmapList("shortShadowCorner"), src, dst, 0, false, 0, 0);
                                }
                            }

                            else if (!tile.isInLongShadeS && !tile.isInLongShadeE)
                            {
                        if (tile.isInMaxShadeSE)
                        {
                            DrawD2DBitmap(GetFromBitmapList("corner3"), src, dst, 0, false, 0, 0);
                        }
                        else
                        if(tile.isInLongShadeSE)
                        {
                            if ((!tile.inRampShadowSouth8Long) && (!tile.inRampShadowEast3Long))
                            {
                                DrawD2DBitmap(GetFromBitmapList("longShadowCorner"), src, dst, 0, false, 0, 0);
                            }
                         }
                     }
                        //}

                        //if (!tile.inRampShadowWest1Short && !tile.inRampShadowWest1Long && !tile.inRampShadowWest2Short && !tile.inRampShadowWest2Long && !tile.inRampShadowSouth7Long && !tile.inRampShadowSouth7Short && !tile.inRampShadowSouth8Long && !tile.inRampShadowSouth8Short)
                        //{
                            if (!tile.isInLongShadeS && !tile.isInShortShadeS && !tile.isInLongShadeW && !tile.isInShortShadeW)
                            {
                        if (tile.isInMaxShadeSW)
                        {
                            DrawD2DBitmap(GetFromBitmapList("corner3"), src, dst, 90, false, 0, 0);
                        }
                        else
                        if (tile.isInLongShadeSW)
                        {
                            if ((!tile.inRampShadowSouth7Long) && (!tile.inRampShadowWest1Long))
                            {
                                DrawD2DBitmap(GetFromBitmapList("longShadowCorner"), src, dst, 90, false, 0, 0);
                            }
                                }
                                else if ((tile.isInShortShadeSW) && (!tile.inRampShadowWest1Short) && (!tile.inRampShadowSouth7Short) && (!tile.inRampShadowSouth7Long) && (!tile.inRampShadowWest1Long))
                                {
                                    DrawD2DBitmap(GetFromBitmapList("shortShadowCorner"), src, dst, 90, false, 0, 0);
                                }
                            }

                            else if (!tile.isInLongShadeS && !tile.isInLongShadeW)
                            {
                        if (tile.isInMaxShadeSW)
                        {
                            DrawD2DBitmap(GetFromBitmapList("corner3"), src, dst, 90, false, 0, 0);
                        }
                        else
                        if (tile.isInLongShadeSW)
                                {
                            if ((!tile.inRampShadowSouth7Long) && (!tile.inRampShadowWest1Long))
                            {
                                DrawD2DBitmap(GetFromBitmapList("longShadowCorner"), src, dst, 90, false, 0, 0);
                            }
                                }
                            }
                    //entrance lights
                    /*
                    if (tile.hasEntranceLightNorth)
                    {
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth"), src, dst, 0, false, 0, 0);
                    }
                    if (tile.hasEntranceLightEast)
                    {
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth"), src, dst, 90, false, 0, 0);
                    }
                    if (tile.hasEntranceLightSouth)
                    {
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth"), src, dst, 180, false, 0, 0);
                    }
                    if (tile.hasEntranceLightWest)
                    {
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth"), src, dst, 270, false, 0, 0);
                    }
                    */
                    /*
                    if (tile.isEWBridge)
                    {
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth"), src, dstNorth, 0, false, 0, 0);
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth"), src, dstSouth, 180, false, 0, 0);
                    }

                    if (tile.isNSBridge)
                    {
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth"), src, dstWest, 270, false, 0, 0);
                        DrawD2DBitmap(GetFromBitmapList("entranceLightNorth"), src, dstEast, 90, false, 0, 0);
                    }
                    */
                }
            }


                        #endregion
            /*
            #region Draw Linked state
            for (int y = 0; y < area.MapSizeY; y++)
            {
                for (int x = 0; x < area.MapSizeX; x++)
                {
                    Tile tile = area.Tiles[y * area.MapSizeX + x];
                    if (tile.linkedToMasterMap)
                    {
                        //LINKED
                        //draw square walkmesh and LoS stuff
                        //SharpDX.Direct2D1.Bitmap bm = GetFromBitmapList("walk_pass");
                        //Rectangle src = new Rectangle(0, 0, g_walkPass.Width, g_walkPass.Height);
                        //Rectangle target = new Rectangle(x * sqr, y * sqr, sqr, sqr);
                        SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList("black_tile").PixelSize.Width, GetFromBitmapList("black_tile").PixelSize.Height);
                        SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, sqr, sqr);
                                                  
                        DrawD2DBitmap(GetFromBitmapList("black_tile"), src, dst, 0, false, 0, 0, 0, 0, 0.75f);

                        if (chkGrid.Checked) //if show grid is turned on, draw grid squares
                        {
                            float scaler = 1.0f;
                            if (sqr == 50) { scaler = 15.0f; }
                            else if (sqr == 25) { scaler = 7.5f; }
                            else if (sqr == 10) { scaler = 3.0f; }
                            DrawText("L", dst.X + (sqr / 4), dst.Y + (sqr / 4), scaler * 1.5f, SharpDX.Color.Red);
                        }
                    }
                }
            }
            #endregion
            */
                        /*
                        #region Draw Height Shadows

                        int indexOfNorthernNeighbour = -1;
                        int indexOfSouthernNeighbour = -1;
                        int indexOfEasternNeighbour = -1;
                        int indexOfWesternNeighbour = -1;
                        int indexOfNorthEasternNeighbour = -1;
                        int indexOfNorthWesternNeighbour = -1;
                        int indexOfSouthEasternNeighbour = -1;
                        int indexOfSouthWesternNeighbour = -1;

                        #region neighbours
                        if ((area.northernNeighbourArea != ""))
                        {
                            for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                            {
                                if (mod.moduleAreasObjects[i].Filename == area.northernNeighbourArea)
                                {
                                   indexOfNorthernNeighbour = i;
                                }
                            }

                            if (mod.moduleAreasObjects[indexOfNorthernNeighbour].easternNeighbourArea != "")
                            {
                                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                                {
                                    if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfNorthernNeighbour].easternNeighbourArea)
                                    {
                                        indexOfNorthEasternNeighbour = i;
                                    }
                                }
                            }

                            if (mod.moduleAreasObjects[indexOfNorthernNeighbour].westernNeighbourArea != "")
                            {
                                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                                {
                                    if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfNorthernNeighbour].westernNeighbourArea)
                                    {
                                        indexOfNorthWesternNeighbour = i;
                                    }
                                }
                            }
                        }

                        if ((area.southernNeighbourArea != ""))
                        {
                            for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                            {
                                if (mod.moduleAreasObjects[i].Filename == area.southernNeighbourArea)
                                {
                                    indexOfSouthernNeighbour = i;
                                }
                            }

                            if (mod.moduleAreasObjects[indexOfSouthernNeighbour].easternNeighbourArea != "")
                            {
                                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                                {
                                    if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfSouthernNeighbour].easternNeighbourArea)
                                    {
                                        indexOfSouthEasternNeighbour = i;
                                    }
                                }
                            }

                            if (mod.moduleAreasObjects[indexOfSouthernNeighbour].westernNeighbourArea != "")
                            {

                                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                                {
                                    if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfSouthernNeighbour].westernNeighbourArea)
                                    {
                                        indexOfSouthWesternNeighbour = i;
                                    }
                                }
                            }
                        }

                        if ((area.westernNeighbourArea != ""))
                        {              
                            for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                            {
                                if (mod.moduleAreasObjects[i].Filename == area.westernNeighbourArea)
                                {
                                    indexOfWesternNeighbour = i;
                                }
                            }

                            if (mod.moduleAreasObjects[indexOfWesternNeighbour].northernNeighbourArea != "")
                            {
                                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                                {
                                    if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfWesternNeighbour].northernNeighbourArea)
                                    {
                                        indexOfNorthWesternNeighbour = i;
                                    }
                                }
                            }

                            if (mod.moduleAreasObjects[indexOfWesternNeighbour].southernNeighbourArea != "")
                            {

                                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                                {
                                    if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfWesternNeighbour].southernNeighbourArea)
                                    {
                                        indexOfSouthWesternNeighbour = i;
                                    }
                                }
                            }
                        }

                        if ((area.easternNeighbourArea != ""))
                        {
                            for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                            {
                                if (mod.moduleAreasObjects[i].Filename == area.easternNeighbourArea)
                                {
                                    indexOfEasternNeighbour = i;
                                }
                            }

                            if (mod.moduleAreasObjects[indexOfEasternNeighbour].northernNeighbourArea != "")
                            {
                                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                                {
                                    if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfEasternNeighbour].northernNeighbourArea)
                                    {
                                        indexOfNorthEasternNeighbour = i;
                                    }
                                }
                            }

                            if (mod.moduleAreasObjects[indexOfEasternNeighbour].southernNeighbourArea != "")
                            {
                                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                                {
                                    if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfEasternNeighbour].southernNeighbourArea)
                                    {
                                        indexOfSouthEasternNeighbour = i;
                                    }
                                }
                            }
                        }
                        #endregion

                        for (int y = 0; y < area.MapSizeY; y++)
                        {
                            for (int x = 0; x < area.MapSizeX; x++)
                            {
                                Tile tile = area.Tiles[y * area.MapSizeX + x];

                                for (int yS = -1; yS < 2; yS++)
                                {
                                    for (int xS = -1; xS < 2; xS++)
                                    {
                                        //**********************************************************************************
                                        //int index = -1;
                                        Tile tileCaster = new Tile();

                                        //nine situations where a caster tile can be:
                                        //caster tile on north-western map (diagonal situation)
                                        if ((x + xS < 0) && (y + yS < 0))
                                        {
                                            if (indexOfNorthWesternNeighbour != -1)
                                            {
                                                int transformedX = mod.moduleAreasObjects[indexOfNorthWesternNeighbour].MapSizeX + x + xS;
                                                int transformedY = mod.moduleAreasObjects[indexOfNorthWesternNeighbour].MapSizeY + y + yS;
                                                tileCaster = mod.moduleAreasObjects[indexOfNorthWesternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfNorthWesternNeighbour].MapSizeX + transformedX];

                                                //casts shadow and is no ramp
                                                if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                                {
                                                    //check for long shadows
                                                    if (tileCaster.heightLevel == tile.heightLevel + 2)
                                                    {
                                                        tile.isInLongShadeNW = true;
                                                    }

                                                    //check for short shadows
                                                    else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                                    {
                                                        tile.isInShortShadeNW = true;
                                                    }
                                                    else
                                                    {
                                                        tile.isInShortShadeNW = false;
                                                        tile.isInLongShadeNW = false;
                                                    }
                                                }
                                                else
                                                {
                                                    tile.isInShortShadeNW = false;
                                                    tile.isInLongShadeNW = false;
                                                }
                                            }
                                        }

                                        //caster tile on south-western map (diagonal situation)
                                        if ((x + xS < 0) && (y + yS > area.MapSizeY - 1))
                                        {
                                            if (indexOfSouthWesternNeighbour != -1)
                                            {
                                                int transformedX = mod.moduleAreasObjects[indexOfSouthWesternNeighbour].MapSizeX + x + xS;
                                                int transformedY = y + yS - area.MapSizeY;
                                                tileCaster = mod.moduleAreasObjects[indexOfSouthWesternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfSouthWesternNeighbour].MapSizeX + transformedX];

                                                //casts shadow and is no ramp
                                                if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                                {
                                                    //check for long shadows
                                                    if (tileCaster.heightLevel == tile.heightLevel + 2)
                                                    {
                                                        tile.isInLongShadeSW = true;
                                                    }

                                                    //check for short shadows
                                                    else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                                    {
                                                        tile.isInShortShadeSW = true;
                                                    }
                                                    else
                                                    {
                                                        tile.isInShortShadeSW = false;
                                                        tile.isInLongShadeSW = false;
                                                    }
                                                }
                                                else
                                                {
                                                    tile.isInShortShadeSW = false;
                                                    tile.isInLongShadeSW = false;
                                                }


                                            }

                                        }


                                        //***********************
                                        //caster tile on south-eastern map (diagonal situation)
                                        if ((x + xS > area.MapSizeX - 1) && (y + yS > area.MapSizeY - 1))
                                        {
                                            if (indexOfSouthEasternNeighbour != -1)
                                            {
                                                int transformedX = x + xS - area.MapSizeX;
                                                int transformedY = y + yS - area.MapSizeY;
                                                tileCaster = mod.moduleAreasObjects[indexOfSouthEasternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfSouthEasternNeighbour].MapSizeX + transformedX];

                                                //casts shadow and is no ramp
                                                if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                                {
                                                    //check for long shadows
                                                    if (tileCaster.heightLevel == tile.heightLevel + 2)
                                                    {
                                                        tile.isInLongShadeSE = true;
                                                    }

                                                    //check for short shadows
                                                    else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                                    {
                                                        tile.isInShortShadeSE = true;
                                                    }
                                                    else
                                                    {
                                                        tile.isInShortShadeSE = false;
                                                        tile.isInLongShadeSE = false;
                                                    }
                                                }
                                                else
                                                {
                                                    tile.isInShortShadeSE = false;
                                                    tile.isInLongShadeSE = false;
                                                }
                                            }
                                        }

                                        //caster tile on north-eastern map (diagonal situation)
                                        if ((x + xS > area.MapSizeX - 1) && (y + yS < 0))
                                        {
                                            if (indexOfNorthEasternNeighbour != -1)
                                            {
                                                int transformedX = x + xS - area.MapSizeX;
                                                int transformedY = mod.moduleAreasObjects[indexOfNorthEasternNeighbour].MapSizeY + y + yS; ;
                                                tileCaster = mod.moduleAreasObjects[indexOfNorthEasternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfNorthEasternNeighbour].MapSizeX + transformedX];

                                                //casts shadow and is no ramp
                                                if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                                {
                                                    //check for long shadows
                                                    if (tileCaster.heightLevel == tile.heightLevel + 2)
                                                    {
                                                        tile.isInLongShadeNE = true;
                                                    }

                                                    //check for short shadows
                                                    else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                                    {
                                                        tile.isInShortShadeNE = true;
                                                    }
                                                    else
                                                    {
                                                        tile.isInShortShadeNE = false;
                                                        tile.isInLongShadeNE = false;
                                                    }
                                                }
                                                else
                                                {
                                                    tile.isInShortShadeNE = false;
                                                    tile.isInLongShadeNE = false;
                                                }
                                            }
                                        }

                                        //caster tile on western map (non-diagonal)
                                        if ((x + xS < 0) && (y + yS >= 0) && (y + yS < area.MapSizeY))
                                        {
                                            if (indexOfWesternNeighbour != -1)
                                            {
                                                int transformedX = mod.moduleAreasObjects[indexOfWesternNeighbour].MapSizeX + x + xS;
                                                int transformedY = y + yS;
                                                tileCaster = mod.moduleAreasObjects[indexOfWesternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfWesternNeighbour].MapSizeX + transformedX];

                                                //casts shadow and is no ramp
                                                if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                                {
                                                    //check for long shadows
                                                    if (tileCaster.heightLevel == tile.heightLevel + 2)
                                                    {
                                                        if (yS == -1)
                                                        {
                                                            tile.isInLongShadeNW = true;
                                                        }
                                                        if (yS == 0)
                                                        {
                                                            tile.isInLongShadeW = true;
                                                        }
                                                        if (yS == 1)
                                                        {
                                                            tile.isInLongShadeSW = true;
                                                        }
                                                    }

                                                    //check for short shadows
                                                    else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                                    {
                                                        if (yS == -1)
                                                        {
                                                            tile.isInShortShadeNW = true;
                                                        }
                                                        if (yS == 0)
                                                        {
                                                            tile.isInShortShadeW = true;
                                                        }
                                                        if (yS == 1)
                                                        {
                                                            tile.isInShortShadeSW = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        tile.isInShortShadeNW = false;
                                                        tile.isInLongShadeNW = false;
                                                        tile.isInShortShadeW = false;
                                                        tile.isInLongShadeW = false;
                                                        tile.isInShortShadeSW = false;
                                                        tile.isInLongShadeSW = false;
                                                    }
                                                }
                                                else
                                                {
                                                    tile.isInShortShadeNW = false;
                                                    tile.isInLongShadeNW = false;
                                                    tile.isInShortShadeW = false;
                                                    tile.isInLongShadeW = false;
                                                    tile.isInShortShadeSW = false;
                                                    tile.isInLongShadeSW = false;
                                                }
                                            }
                                        }

                                        //caster tile on eastern map (non-diagonal)
                                        if ((x + xS >= area.MapSizeX) && (y + yS >= 0) && (y + yS < area.MapSizeY))
                                        {
                                            if (indexOfEasternNeighbour != -1)
                                            {
                                                int transformedX = x + xS - area.MapSizeX;
                                                int transformedY = y + yS;
                                                tileCaster = mod.moduleAreasObjects[indexOfEasternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfEasternNeighbour].MapSizeX + transformedX];

                                                //casts shadow and is no ramp
                                                if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                                {
                                                    //check for long shadows
                                                    if (tileCaster.heightLevel == tile.heightLevel + 2)
                                                    {
                                                        if (yS == -1)
                                                        {
                                                            tile.isInLongShadeNE = true;
                                                        }
                                                        if (yS == 0)
                                                        {
                                                            tile.isInLongShadeE = true;
                                                        }
                                                        if (yS == 1)
                                                        {
                                                            tile.isInLongShadeSE = true;
                                                        }
                                                    }

                                                    //check for short shadows
                                                    else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                                    {
                                                        if (yS == -1)
                                                        {
                                                            tile.isInShortShadeNE = true;
                                                        }
                                                        if (yS == 0)
                                                        {
                                                            tile.isInShortShadeE = true;
                                                        }
                                                        if (yS == 1)
                                                        {
                                                            tile.isInShortShadeSE = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        tile.isInShortShadeNE = false;
                                                        tile.isInLongShadeNE = false;
                                                        tile.isInShortShadeE = false;
                                                        tile.isInLongShadeE = false;
                                                        tile.isInShortShadeSE = false;
                                                        tile.isInLongShadeSE = false;
                                                    }
                                                }
                                                else
                                                {
                                                    tile.isInShortShadeNE = false;
                                                    tile.isInLongShadeNE = false;
                                                    tile.isInShortShadeE = false;
                                                    tile.isInLongShadeE = false;
                                                    tile.isInShortShadeSE = false;
                                                    tile.isInLongShadeSE = false;
                                                }
                                            }
                                        }


                                        //caster tile on southern map (non-diagonal)
                                        if ((y + yS >= area.MapSizeY) && (x + xS >= 0) && (x + xS < area.MapSizeX))
                                        {
                                            if (indexOfSouthernNeighbour != -1)
                                            {
                                                int transformedX = x + xS;
                                                int transformedY = y + yS - area.MapSizeY;
                                                tileCaster = mod.moduleAreasObjects[indexOfSouthernNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfSouthernNeighbour].MapSizeX + transformedX];

                                                //casts shadow and is no ramp
                                                if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                                {
                                                    //check for long shadows
                                                    if (tileCaster.heightLevel == tile.heightLevel + 2)
                                                    {
                                                        if (xS == -1)
                                                        {
                                                            tile.isInLongShadeSW = true;
                                                        }
                                                        if (xS == 0)
                                                        {
                                                            tile.isInLongShadeS = true;
                                                        }
                                                        if (xS == 1)
                                                        {
                                                            tile.isInLongShadeSE = true;
                                                        }
                                                    }

                                                    //check for short shadows
                                                    else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                                    {
                                                        if (xS == -1)
                                                        {
                                                            tile.isInShortShadeSW = true;
                                                        }
                                                        if (xS == 0)
                                                        {
                                                            tile.isInShortShadeS = true;
                                                        }
                                                        if (xS == 1)
                                                        {
                                                            tile.isInShortShadeSE = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        tile.isInShortShadeSE = false;
                                                        tile.isInLongShadeSE = false;
                                                        tile.isInShortShadeS = false;
                                                        tile.isInLongShadeS = false;
                                                        tile.isInShortShadeSW = false;
                                                        tile.isInLongShadeSW = false;
                                                    }
                                                }
                                                else
                                                {
                                                    tile.isInShortShadeSE = false;
                                                    tile.isInLongShadeSE = false;
                                                    tile.isInShortShadeS = false;
                                                    tile.isInLongShadeS = false;
                                                    tile.isInShortShadeSW = false;
                                                    tile.isInLongShadeSW = false;
                                                }
                                            }
                                        }

                                        //caster tile on northern map (non-diagonal)
                                        if ((y + yS < 0) && (x + xS >= 0) && (x + xS < area.MapSizeX))
                                        {
                                            if (indexOfNorthernNeighbour != -1)
                                            {
                                                int transformedX = x + xS;
                                                int transformedY = mod.moduleAreasObjects[indexOfNorthernNeighbour].MapSizeY + y + yS; ;
                                                tileCaster = mod.moduleAreasObjects[indexOfNorthernNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfNorthernNeighbour].MapSizeX + transformedX];

                                                //casts shadow and is no ramp
                                                if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                                {
                                                    //check for long shadows
                                                    if (tileCaster.heightLevel == tile.heightLevel + 2)
                                                    {
                                                        if (xS == -1)
                                                        {
                                                            tile.isInLongShadeNW = true;
                                                            tile.isInShortShadeNW = false;
                                                        }
                                                        if (xS == 0)
                                                        {
                                                            tile.isInLongShadeN = true;
                                                            tile.isInShortShadeN = false;
                                                        }
                                                        if (xS == 1)
                                                        {
                                                            tile.isInLongShadeNE = true;
                                                            tile.isInShortShadeNE = false;
                                                        }
                                                    }

                                                    //check for short shadows
                                                    else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                                    {
                                                        if (xS == -1)
                                                        {
                                                            tile.isInShortShadeNW = true;
                                                            tile.isInLongShadeNW = false;
                                                        }
                                                        if (xS == 0)
                                                        {
                                                            tile.isInShortShadeN = true;
                                                            tile.isInLongShadeN = false;
                                                        }
                                                        if (xS == 1)
                                                        {
                                                            tile.isInShortShadeNE = true;
                                                            tile.isInLongShadeNE = false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (xS == -1)
                                                        {
                                                            tile.isInLongShadeNW = false;
                                                            tile.isInShortShadeNW = false;
                                                        }
                                                        if (xS == 0)
                                                        {
                                                            tile.isInLongShadeN = false;
                                                            tile.isInShortShadeN = false;
                                                        }
                                                        if (xS == 1)
                                                        {
                                                            tile.isInLongShadeNE = false;
                                                            tile.isInShortShadeNE = false;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (xS == -1)
                                                    {
                                                        tile.isInLongShadeNW = false;
                                                        tile.isInShortShadeNW = false;
                                                    }
                                                    if (xS == 0)
                                                    {
                                                        tile.isInLongShadeN = false;
                                                        tile.isInShortShadeN = false;
                                                    }
                                                    if (xS == 1)
                                                    {
                                                        tile.isInLongShadeNE = false;
                                                        tile.isInShortShadeNE = false;
                                                    }
                                                }
                                            }
                                        }

                                        //caster tile is on this map
                                        if ((y + yS >= 0) && (y + yS < area.MapSizeY) && (x + xS >= 0) && (x + xS < area.MapSizeX))
                                        {
                                                int transformedX = x + xS;
                                                int transformedY = y + yS; ;
                                                tileCaster = area.Tiles[transformedY * area.MapSizeX + transformedX];

                                                //casts shadow and is no ramp
                                                if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                                {
                                                    //check for long shadows
                                                    if (tileCaster.heightLevel == tile.heightLevel + 2)
                                                    {
                                                        if ((xS == -1) &&  (yS == -1))
                                                        {
                                                            tile.isInLongShadeNW = true;
                                                        }

                                                    }

                                                    //check for short shadows
                                                    else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                                    {
                                                        if (xS == -1)
                                                        {
                                                            tile.isInShortShadeNW = true;
                                                        }
                                                        if (xS == 0)
                                                        {
                                                            tile.isInShortShadeN = true;
                                                        }
                                                        if (xS == 1)
                                                        {
                                                            tile.isInShortShadeNE = true;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        tile.isInShortShadeNE = false;
                                                        tile.isInLongShadeNE = false;
                                                        tile.isInShortShadeN = false;
                                                        tile.isInLongShadeN = false;
                                                        tile.isInShortShadeNW = false;
                                                        tile.isInLongShadeNW = false;
                                                    }
                                                }
                                                else
                                                {
                                                    tile.isInShortShadeNE = false;
                                                    tile.isInLongShadeNE = false;
                                                    tile.isInShortShadeN = false;
                                                    tile.isInLongShadeN = false;
                                                    tile.isInShortShadeNW = false;
                                                    tile.isInLongShadeNW = false;
                                                }
                                        }

                                        //**********************



                                        //tile on southern map
                                        if ((y > (gv.mod.currentArea.MapSizeY - 1)) && (!situationFound))
                                        {
                                            situationFound = true;
                                            if (gv.mod.indexOfSouthernNeighbour != -1)
                                            {
                                                int transformedX = x;
                                                int transformedY = y - gv.mod.currentArea.MapSizeY;
                                                tile = mod.moduleAreasObjects[gv.mod.indexOfSouthernNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfSouthernNeighbour].MapSizeX + transformedX];
                                                index = gv.mod.indexOfSouthernNeighbour;
                                            }
                                            else
                                            {
                                                drawTile = false;
                                            }
                                        }
                                        //tile on eastern map
                                        if ((x > (gv.mod.currentArea.MapSizeX - 1)) && (!situationFound))
                                        {
                                            situationFound = true;
                                            if (gv.mod.indexOfEasternNeighbour != -1)
                                            {
                                                int transformedX = x - gv.mod.currentArea.MapSizeX;
                                                int transformedY = y;
                                                tile = mod.moduleAreasObjects[gv.mod.indexOfEasternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfEasternNeighbour].MapSizeX + transformedX];
                                                index = gv.mod.indexOfEasternNeighbour;
                                            }
                                            else
                                            {
                                                drawTile = false;
                                            }
                                        }
                                        //tile on northern map
                                        if ((y < 0) && (!situationFound))
                                        {
                                            situationFound = true;
                                            if (gv.mod.indexOfNorthernNeighbour != -1)
                                            {
                                                int transformedX = x;
                                                int transformedY = mod.moduleAreasObjects[gv.mod.indexOfNorthernNeighbour].MapSizeY + y;
                                                tile = mod.moduleAreasObjects[gv.mod.indexOfNorthernNeighbour].Tiles[transformedY * mod.moduleAreasObjects[gv.mod.indexOfNorthernNeighbour].MapSizeX + transformedX];
                                                index = gv.mod.indexOfNorthernNeighbour;
                                            }
                                            else
                                            {
                                                drawTile = false;
                                            }
                                        }
                                        //tile is on current map
                                        if (!situationFound)
                                        {
                                            tile = mod.currentArea.Tiles[y * mod.currentArea.MapSizeX + x];
                                        }




                                        //****************************************************************************************

                                    }//close inner y loop
                                }//close inner x loop


                                float scalerX = GetFromBitmapList(tile.Layer1Filename).PixelSize.Width / 100;
                                float scalerY = GetFromBitmapList(tile.Layer1Filename).PixelSize.Height / 100;
                                SharpDX.RectangleF src = new SharpDX.RectangleF(0, 0, GetFromBitmapList(tile.Layer1Filename).PixelSize.Width, GetFromBitmapList(tile.Layer1Filename).PixelSize.Height);
                                SharpDX.RectangleF dst = new SharpDX.RectangleF(x * sqr, y * sqr, (int)(sqr * scalerX), (int)(sqr * scalerY));
                                DrawD2DBitmap(GetFromBitmapList(tile.Layer1Filename), src, dst, tile.Layer1Rotate, tile.Layer1Mirror, tile.Layer1Xshift, tile.Layer1Yshift, tile.Layer1Xscale, tile.Layer1Yscale);
                                }//closer outer y loop 
                            }//close outer x loop

                        #endregion
                        */
                    }
        #endregion

        #region Methods

        public void calculateHeightShadows()
        {
            string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas\\";
            mod.loadAreas(filePath, area);

            #region Calculate Height Shadows

            int indexOfNorthernNeighbour = -1;
            int indexOfSouthernNeighbour = -1;
            int indexOfEasternNeighbour = -1;
            int indexOfWesternNeighbour = -1;
            int indexOfNorthEasternNeighbour = -1;
            int indexOfNorthWesternNeighbour = -1;
            int indexOfSouthEasternNeighbour = -1;
            int indexOfSouthWesternNeighbour = -1;

            #region neighbours
            if ((area.northernNeighbourArea != ""))
            {
                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                {
                    if (mod.moduleAreasObjects[i].Filename == area.northernNeighbourArea)
                    {
                        indexOfNorthernNeighbour = i;
                    }
                }

                if (mod.moduleAreasObjects[indexOfNorthernNeighbour].easternNeighbourArea != "")
                {
                    for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                    {
                        if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfNorthernNeighbour].easternNeighbourArea)
                        {
                            indexOfNorthEasternNeighbour = i;
                        }
                    }
                }

                if (mod.moduleAreasObjects[indexOfNorthernNeighbour].westernNeighbourArea != "")
                {
                    for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                    {
                        if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfNorthernNeighbour].westernNeighbourArea)
                        {
                            indexOfNorthWesternNeighbour = i;
                        }
                    }
                }
            }

            if ((area.southernNeighbourArea != ""))
            {
                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                {
                    if (mod.moduleAreasObjects[i].Filename == area.southernNeighbourArea)
                    {
                        indexOfSouthernNeighbour = i;
                    }
                }

                if (mod.moduleAreasObjects[indexOfSouthernNeighbour].easternNeighbourArea != "")
                {
                    for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                    {
                        if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfSouthernNeighbour].easternNeighbourArea)
                        {
                            indexOfSouthEasternNeighbour = i;
                        }
                    }
                }

                if (mod.moduleAreasObjects[indexOfSouthernNeighbour].westernNeighbourArea != "")
                {

                    for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                    {
                        if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfSouthernNeighbour].westernNeighbourArea)
                        {
                            indexOfSouthWesternNeighbour = i;
                        }
                    }
                }
            }

            if ((area.westernNeighbourArea != ""))
            {
                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                {
                    if (mod.moduleAreasObjects[i].Filename == area.westernNeighbourArea)
                    {
                        indexOfWesternNeighbour = i;
                    }
                }

                if (mod.moduleAreasObjects[indexOfWesternNeighbour].northernNeighbourArea != "")
                {
                    for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                    {
                        if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfWesternNeighbour].northernNeighbourArea)
                        {
                            indexOfNorthWesternNeighbour = i;
                        }
                    }
                }

                if (mod.moduleAreasObjects[indexOfWesternNeighbour].southernNeighbourArea != "")
                {

                    for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                    {
                        if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfWesternNeighbour].southernNeighbourArea)
                        {
                            indexOfSouthWesternNeighbour = i;
                        }
                    }
                }
            }

            if ((area.easternNeighbourArea != ""))
            {
                for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                {
                    if (mod.moduleAreasObjects[i].Filename == area.easternNeighbourArea)
                    {
                        indexOfEasternNeighbour = i;
                    }
                }

                if (mod.moduleAreasObjects[indexOfEasternNeighbour].northernNeighbourArea != "")
                {
                    for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                    {
                        if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfEasternNeighbour].northernNeighbourArea)
                        {
                            indexOfNorthEasternNeighbour = i;
                        }
                    }
                }

                if (mod.moduleAreasObjects[indexOfEasternNeighbour].southernNeighbourArea != "")
                {
                    for (int i = 0; i < mod.moduleAreasObjects.Count; i++)
                    {
                        if (mod.moduleAreasObjects[i].Filename == mod.moduleAreasObjects[indexOfEasternNeighbour].southernNeighbourArea)
                        {
                            indexOfSouthEasternNeighbour = i;
                        }
                    }
                }
            }
            #endregion

            //reset the highlights on this map
            for (int y = 0; y < area.MapSizeY; y++)
            {
                for (int x = 0; x < area.MapSizeX; x++)
                {
                    Tile tile = area.Tiles[y * area.MapSizeX + x];
                    tile.hasHighlightN = false;
                    tile.hasHighlightE = false;
                    tile.hasHighlightS = false;
                    tile.hasHighlightW = false;
                }
            }

            if (indexOfNorthernNeighbour != -1)
            {
                for (int y = 0; y < mod.moduleAreasObjects[indexOfNorthernNeighbour].MapSizeY; y++)
                {
                    for (int x = 0; x < mod.moduleAreasObjects[indexOfNorthernNeighbour].MapSizeX; x++)
                    {
                        if (y == mod.moduleAreasObjects[indexOfNorthernNeighbour].MapSizeY - 1)
                        {
                            Tile tile = area.Tiles[y * area.MapSizeX + x];
                            tile.hasHighlightS = false;
                        }
                    }
                }
            }

            if (indexOfSouthernNeighbour != -1)
            {
                for (int y = 0; y < mod.moduleAreasObjects[indexOfSouthernNeighbour].MapSizeY; y++)
                {
                    for (int x = 0; x < mod.moduleAreasObjects[indexOfSouthernNeighbour].MapSizeX; x++)
                    {
                        if (y == 0)
                        {
                            Tile tile = area.Tiles[y * area.MapSizeX + x];
                            tile.hasHighlightN = false;
                        }
                    }
                }
            }

            if (indexOfEasternNeighbour != -1)
            {
                for (int y = 0; y < mod.moduleAreasObjects[indexOfEasternNeighbour].MapSizeY; y++)
                {
                    for (int x = 0; x < mod.moduleAreasObjects[indexOfEasternNeighbour].MapSizeX; x++)
                    {
                        if (x == 0)
                        {
                            Tile tile = area.Tiles[y * area.MapSizeX + x];
                            tile.hasHighlightW = false;
                        }
                    }
                }
            }

            if (indexOfWesternNeighbour != -1)
            {
                for (int y = 0; y < mod.moduleAreasObjects[indexOfWesternNeighbour].MapSizeY; y++)
                {
                    for (int x = 0; x < mod.moduleAreasObjects[indexOfWesternNeighbour].MapSizeX; x++)
                    {
                        if (x == mod.moduleAreasObjects[indexOfWesternNeighbour].MapSizeX-1)
                        {
                            Tile tile = area.Tiles[y * area.MapSizeX + x];
                            tile.hasHighlightE = false;
                        }
                    }
                }
            }
            /*
            //calculate height level differences
            for (int y = 0; y < area.MapSizeY; y++)
            {
                for (int x = 0; x < area.MapSizeX; x++)
                {
                    Tile tile = area.Tiles[y * area.MapSizeX + x];
                    for (int yS = -1; yS < 2; yS++)
                    {
                        for (int xS = -1; xS < 2; xS++)
                        {


                        }
                    }
                }
            }
            */
            int heightSum = 0;
            //go through tiles potentially in shade
            for (int y = 0; y < area.MapSizeY; y++)
            {
                for (int x = 0; x < area.MapSizeX; x++)
                {
                    Tile tile = area.Tiles[y * area.MapSizeX + x];

                    heightSum += tile.heightLevel;
                    if (!tile.linkedToMasterMap || tile.linkedToMasterMap)
                    { 
                    tile.isInShortShadeN = false;
                    tile.isInShortShadeE = false;
                    tile.isInShortShadeS = false;
                    tile.isInShortShadeW = false;
                    tile.isInShortShadeNE = false;
                    tile.isInShortShadeSE = false;
                    tile.isInShortShadeSW = false;
                    tile.isInShortShadeNW = false;
                    tile.isInLongShadeN = false;
                    tile.isInLongShadeE = false;
                    tile.isInLongShadeS = false;
                    tile.isInLongShadeW = false;
                    tile.isInLongShadeNE = false;
                    tile.isInLongShadeSE = false;
                    tile.isInLongShadeSW = false;
                    tile.isInLongShadeNW = false;

                    /*
                    tile.hasEntranceLightNorth = false;
                    tile.hasEntranceLightEast = false;
                    tile.hasEntranceLightSouth = false;
                    tile.hasEntranceLightWest = false;
                    */

                    tile.isInMaxShadeN = false;
                    tile.isInMaxShadeE = false;
                    tile.isInMaxShadeS = false;
                    tile.isInMaxShadeW = false;
                    tile.isInMaxShadeNE = false;
                    tile.isInMaxShadeSE = false;
                    tile.isInMaxShadeSW = false;
                    tile.isInMaxShadeNW = false;

                    tile.inRampShadowWest1Long = false;
                    tile.inRampShadowWest1Short = false;
                    tile.inRampShadowWest2Long = false;
                    tile.inRampShadowWest2Short = false;
                    tile.inRampShadowEast3Long = false;
                    tile.inRampShadowEast3Short = false;
                    tile.inRampShadowEast4Long = false;
                    tile.inRampShadowEast4Short = false;
                    tile.inRampShadowNorth5Long = false;
                    tile.inRampShadowNorth5Short = false;
                    tile.inRampShadowNorth6Long = false;
                    tile.inRampShadowNorth6Short = false;
                    tile.inRampShadowSouth7Long = false;
                    tile.inRampShadowSouth7Short = false;
                    tile.inRampShadowSouth8Long = false;
                    tile.inRampShadowSouth8Short = false;

                    tile.inSmallStairNEVertical = false;
                    tile.inSmallStairNEHorizontal = false;
                    tile.inSmallStairSEVertical = false;
                    tile.inSmallStairSEHorizontal = false;
                    tile.inSmallStairSWVertical = false;
                    tile.inSmallStairSWHorizontal = false;
                    tile.inSmallStairNWVertical = false;
                    tile.inSmallStairNWHorizontal = false;


                    //go through each potential shadow caster tile surrounding the potentially shaded tile
                    for (int yS = -1; yS < 2; yS++)
                    {
                        for (int xS = -1; xS < 2; xS++)
                        {
                            //**********************************************************************************
                            //int index = -1;
                            Tile tileCaster = new Tile();

                            //nine situations where a caster tile can be:
                            //caster tile on north-western map (diagonal situation)
                            if ((x + xS < 0) && (y + yS < 0))
                            {
                                if (indexOfNorthWesternNeighbour != -1)
                                {
                                    int transformedX = mod.moduleAreasObjects[indexOfNorthWesternNeighbour].MapSizeX + x + xS;
                                    int transformedY = mod.moduleAreasObjects[indexOfNorthWesternNeighbour].MapSizeY + y + yS;
                                    tileCaster = mod.moduleAreasObjects[indexOfNorthWesternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfNorthWesternNeighbour].MapSizeX + transformedX];
                                    /*
                                    //casts shadow and is no ramp
                                    if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                    {
                                        //check for long shadows
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            tile.isInLongShadeNW = true;
                                        }

                                        //check for short shadows
                                        else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            tile.isInShortShadeNW = true;
                                        }

                                    }
                                    else if ((tileCaster.isShadowCaster) && (tileCaster.isRamp))
                                    {
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            tile.isInShortShadeNW = true;
                                        }
                                    }
                                    */
                                }
                            }

                            //caster tile on south-western map (diagonal situation)
                            if ((x + xS < 0) && (y + yS > area.MapSizeY - 1))
                            {
                                if (indexOfSouthWesternNeighbour != -1)
                                {
                                    int transformedX = mod.moduleAreasObjects[indexOfSouthWesternNeighbour].MapSizeX + x + xS;
                                    int transformedY = y + yS - area.MapSizeY;
                                    tileCaster = mod.moduleAreasObjects[indexOfSouthWesternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfSouthWesternNeighbour].MapSizeX + transformedX];
                                    /*
                                    //casts shadow and is no ramp
                                    if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                    {
                                        //check for long shadows
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            tile.isInLongShadeSW = true;
                                        }

                                        //check for short shadows
                                        else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            tile.isInShortShadeSW = true;
                                        }
                                    }
                                    else if ((tileCaster.isShadowCaster) && (tileCaster.isRamp))
                                    {
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            tile.isInShortShadeSW = true;
                                        }
                                    }
                                    /*
                                    else if ((tileCaster.isShadowCaster) && (tileCaster.hasDownStairShadowE || tileCaster.hasDownStairShadowN))
                                    {
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            tile.isInShortShadeSW = true;
                                        }
                                    }
                                    else if ((tileCaster.isShadowCaster) && (tileCaster.hasDownStairShadowE || tileCaster.hasDownStairShadowN))
                                    {
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            tile.isInLongShadeSW = true;
                                        }
                                        if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            tile.isInShortShadeSW = true;
                                        }
                                    }
                                    */
                                }
                            }


                            //***********************
                            //caster tile on south-eastern map (diagonal situation)
                            if ((x + xS > area.MapSizeX - 1) && (y + yS > area.MapSizeY - 1))
                            {
                                if (indexOfSouthEasternNeighbour != -1)
                                {
                                    int transformedX = x + xS - area.MapSizeX;
                                    int transformedY = y + yS - area.MapSizeY;
                                    tileCaster = mod.moduleAreasObjects[indexOfSouthEasternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfSouthEasternNeighbour].MapSizeX + transformedX];
                                    /*
                                    //casts shadow and is no ramp
                                    if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                    {
                                        //check for long shadows
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            tile.isInLongShadeSE = true;
                                        }

                                        //check for short shadows
                                        else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            tile.isInShortShadeSE = true;
                                        }

                                    }
                                    else if ((tileCaster.isShadowCaster) && (tileCaster.isRamp))
                                    {
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            tile.isInShortShadeSE = true;
                                        }
                                    }
                                    */
                                }
                            }

                            //caster tile on north-eastern map (diagonal situation)
                            if ((x + xS > area.MapSizeX - 1) && (y + yS < 0))
                            {
                                if (indexOfNorthEasternNeighbour != -1)
                                {
                                    int transformedX = x + xS - area.MapSizeX;
                                    int transformedY = mod.moduleAreasObjects[indexOfNorthEasternNeighbour].MapSizeY + y + yS; ;
                                    tileCaster = mod.moduleAreasObjects[indexOfNorthEasternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfNorthEasternNeighbour].MapSizeX + transformedX];
                                    /*
                                    //casts shadow and is no ramp
                                    if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                    {
                                        //check for long shadows
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            tile.isInLongShadeNE = true;
                                        }

                                        //check for short shadows
                                        else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            tile.isInShortShadeNE = true;
                                        }

                                    }
                                    else if ((tileCaster.isShadowCaster) && (tileCaster.isRamp))
                                    {
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            tile.isInShortShadeNE = true;
                                        }
                                    }
                                    */
                                }
                            }

                            //caster tile on western map (non-diagonal)
                            if ((x + xS < 0) && (y + yS >= 0) && (y + yS < area.MapSizeY))
                            {
                                if (indexOfWesternNeighbour != -1)
                                {
                                    int transformedX = mod.moduleAreasObjects[indexOfWesternNeighbour].MapSizeX + x + xS;
                                    int transformedY = y + yS;
                                    tileCaster = mod.moduleAreasObjects[indexOfWesternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfWesternNeighbour].MapSizeX + transformedX];

                                    //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE = tileCaster.heightLevel - tile.heightLevel;
                                    //casts shadow and is no ramp
                                    /*
                                    if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                    {
                                        //check for long shadows
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            if (yS == -1)
                                            {
                                                tile.isInLongShadeNW = true;
                                            }
                                            if (yS == 0)
                                            {
                                                tile.isInLongShadeW = true;
                                                tileCaster.hasHighlightE = true;
                                            }
                                            if (yS == 1)
                                            {
                                                tile.isInLongShadeSW = true;
                                            }
                                        }

                                        //check for short shadows
                                        else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            if (yS == -1)
                                            {
                                                tile.isInShortShadeNW = true;
                                            }
                                            if (yS == 0)
                                            {
                                                tile.isInShortShadeW = true;
                                                tileCaster.hasHighlightE = true;
                                            }
                                            if (yS == 1)
                                            {
                                                tile.isInShortShadeSW = true;
                                            }
                                        }

                                        /*
                                        //check if caster tile is bottom of this ramp/tile
                                        else if ((tileCaster.heightLevel == tile.heightLevel - 1) && (tile.isRamp))
                                        {
                                            if (yS == 0)
                                            {
                                                tile.hasDownStairShadowW = true;
                                            }
                                        }
                                        

                                    }
                                    else if ((tileCaster.isShadowCaster) && (tileCaster.isRamp))
                                    {
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            if (yS == -1)
                                            {
                                                tile.isInShortShadeNW = true;
                                            }
                                            if (yS == 0)
                                            {
                                                //casting ramp is on western map here
                                                //so ramp has to be to either north or south
                                                if (tileCaster.hasDownStairShadowN)
                                                {
                                                    tile.inRampShadowWest1Long = true;
                                                }
                                                if (tileCaster.hasDownStairShadowS)
                                                {
                                                    tile.inRampShadowWest2Long = true;
                                                }
                                                if (tileCaster.hasDownStairShadowE)
                                                {
                                                    tile.isInShortShadeW = true;
                                                }
                                                if (tileCaster.hasDownStairShadowW)
                                                {
                                                    tile.isInLongShadeW = true;
                                                }
                                                tileCaster.hasHighlightE = true;
                                            }
                                            if (yS == 1)
                                            {
                                                tile.isInShortShadeSW = true;
                                            }
                                        }

                                        if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            if (yS == 0)
                                            {
                                                //casting ramp is on western map here
                                                //so ramp has to be to either north or south
                                                if (tileCaster.hasDownStairShadowN)
                                                {
                                                    tile.inRampShadowWest1Short = true;
                                                }
                                                if (tileCaster.hasDownStairShadowS)
                                                {
                                                    tile.inRampShadowWest2Short = true;
                                                }
                                                tileCaster.hasHighlightE = true;
                                                //below constellation should not be allowed to build with new diretional ramps
                                                /*
                                                if (tileCaster.hasDownStairShadowW)
                                                {
                                                    tile.isInShortShadeW = true;
                                                    tileCaster.hasHighlightE = true;
                                                }
                                                

                                            }
                                        }
                                    }
                                    */
                                }
                            }

                            //caster tile on eastern map (non-diagonal)
                            if ((x + xS >= area.MapSizeX) && (y + yS >= 0) && (y + yS < area.MapSizeY))
                            {
                                if (indexOfEasternNeighbour != -1)
                                {
                                    int transformedX = x + xS - area.MapSizeX;
                                    int transformedY = y + yS;
                                    tileCaster = mod.moduleAreasObjects[indexOfEasternNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfEasternNeighbour].MapSizeX + transformedX];

                                    //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW = tileCaster.heightLevel - tile.heightLevel;
                                    /*
                                    //casts shadow and is no ramp
                                    if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                    {
                                        //check for long shadows
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            if (yS == -1)
                                            {
                                                tile.isInLongShadeNE = true;
                                            }
                                            if (yS == 0)
                                            {
                                                tile.isInLongShadeE = true;
                                                tileCaster.hasHighlightW = true;
                                            }
                                            if (yS == 1)
                                            {
                                                tile.isInLongShadeSE = true;
                                            }
                                        }

                                        //check for short shadows
                                        else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            if (yS == -1)
                                            {
                                                tile.isInShortShadeNE = true;
                                            }
                                            if (yS == 0)
                                            {
                                                tile.isInShortShadeE = true;
                                                tileCaster.hasHighlightW = true;
                                            }
                                            if (yS == 1)
                                            {
                                                tile.isInShortShadeSE = true;
                                            }
                                        }
                                        
                                        /*
                                        //check if caster tile is bottom of this ramp/tile
                                        else if ((tileCaster.heightLevel == tile.heightLevel - 1) && (tile.isRamp))
                                        {
                                            if (yS == 0)
                                            {
                                                tile.hasDownStairShadowE = true;
                                            }
                                        }
                                        

                                    }
                                    else if ((tileCaster.isShadowCaster) && (tileCaster.isRamp))
                                    {
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            if (yS == -1)
                                            {
                                                tile.isInShortShadeNE = true;
                                            }
                                            if (yS == 0)
                                            {
                                                //casting ramp is on eastern map here
                                                //so ramp has to be to either north or south
                                                if (tileCaster.hasDownStairShadowN)
                                                {
                                                    tile.inRampShadowEast3Long = true;
                                                }
                                                if (tileCaster.hasDownStairShadowS)
                                                {
                                                    tile.inRampShadowEast4Long = true;
                                                }
                                                if (tileCaster.hasDownStairShadowW)
                                                {
                                                    tile.isInShortShadeE = true;
                                                }
                                                if (tileCaster.hasDownStairShadowE)
                                                {
                                                    tile.isInLongShadeE = true;
                                                }
                                                tileCaster.hasHighlightW = true;
                                            }

                                            if (yS == 1)
                                            {
                                                tile.isInShortShadeSE = true;
                                            }
                                        }
                                    }

                                    if (tileCaster.heightLevel == tile.heightLevel + 1)
                                    {
                                        if (yS == 0)
                                        {
                                            //casting ramp is on western map here
                                            //so ramp has to be to either north or south
                                            if (tileCaster.hasDownStairShadowN)
                                            {
                                                tile.inRampShadowEast3Short = true;
                                            }
                                            if (tileCaster.hasDownStairShadowS)
                                            {
                                                tile.inRampShadowEast4Short = true;
                                            }
                                            tileCaster.hasHighlightW = true;
                                        }
                                    }
                                    */
                                }
                            }

                            //caster tile on southern map (non-diagonal)
                            if ((y + yS >= area.MapSizeY) && (x + xS >= 0) && (x + xS < area.MapSizeX))
                            {
                                if (indexOfSouthernNeighbour != -1)
                                {
                                    int transformedX = x + xS;
                                    int transformedY = y + yS - area.MapSizeY;
                                    tileCaster = mod.moduleAreasObjects[indexOfSouthernNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfSouthernNeighbour].MapSizeX + transformedX];

                                    /*
                                    //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tileCaster.heightLevel - tile.heightLevel;
                                    //casts shadow and is no ramp
                                    if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                    {
                                        //check for long shadows
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            if (xS == -1)
                                            {
                                                tile.isInLongShadeSW = true;
                                            }
                                            if (yS == 0)
                                            {
                                                tile.isInLongShadeS = true;
                                                tileCaster.hasHighlightN = true;
                                            }
                                            if (xS == 1)
                                            {
                                                tile.isInLongShadeSE = true;
                                            }
                                        }

                                        //check for short shadows
                                        else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            if (xS == -1)
                                            {
                                                tile.isInShortShadeSW = true;
                                            }
                                            if (xS == 0)
                                            {
                                                tile.isInShortShadeS = true;
                                                tileCaster.hasHighlightN = true;
                                            }
                                            if (xS == 1)
                                            {
                                                tile.isInShortShadeSE = true;
                                            }
                                        }

                                        /*
                                        //check if caster tile is bottom of this ramp/tile
                                        else if ((tileCaster.heightLevel == tile.heightLevel - 1) && (tile.isRamp))
                                        {
                                            if (xS == 0)
                                            {
                                                tile.hasDownStairShadowS = true;
                                            }
                                        }
                                        

                                    }
                                    else if ((tileCaster.isShadowCaster) && (tileCaster.isRamp))
                                    {
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            if (xS == -1)
                                            {
                                                tile.isInShortShadeSW = true;
                                            }
                                            if (xS == 0)
                                            {
                                                //casting ramp is on southern map here
                                                //so ramp has to be to either east or west
                                                if (tileCaster.hasDownStairShadowE)
                                                {
                                                    tile.inRampShadowSouth7Long = true;
                                                }
                                                if (tileCaster.hasDownStairShadowW)
                                                {
                                                    tile.inRampShadowSouth8Long = true;
                                                }
                                                if (tileCaster.hasDownStairShadowN)
                                                {
                                                    tile.isInShortShadeS = true;
                                                }
                                                if (tileCaster.hasDownStairShadowS)
                                                {
                                                    tile.isInLongShadeS = true;
                                                }
                                                tileCaster.hasHighlightN = true;
                                            }
                                            if (xS == 1)
                                            {
                                                tile.isInShortShadeSE = true;
                                            }
                                        }

                                        if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            if (xS == 0)
                                            {
                                                //casting ramp is on western map here
                                                //so ramp has to be to either north or south
                                                if (tileCaster.hasDownStairShadowE)
                                                {
                                                    tile.inRampShadowSouth7Short = true;
                                                }
                                                if (tileCaster.hasDownStairShadowW)
                                                {
                                                    tile.inRampShadowSouth8Short = true;
                                                }
                                                tileCaster.hasHighlightN = true;
                                            }
                                        }
                                    }

                                */
                                }
                            }

                            //caster tile on northern map (non-diagonal)
                            if ((y + yS < 0) && (x + xS >= 0) && (x + xS < area.MapSizeX))
                            {
                                if (indexOfNorthernNeighbour != -1)
                                {
                                    int transformedX = x + xS;
                                    int transformedY = mod.moduleAreasObjects[indexOfNorthernNeighbour].MapSizeY + y + yS; ;
                                    tileCaster = mod.moduleAreasObjects[indexOfNorthernNeighbour].Tiles[transformedY * mod.moduleAreasObjects[indexOfNorthernNeighbour].MapSizeX + transformedX];
                                    //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS = tileCaster.heightLevel - tile.heightLevel;
                                    /*
                                    //casts shadow and is no ramp
                                    if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                    {
                                        //check for long shadows
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            if (xS == -1)
                                            {
                                                tile.isInLongShadeNW = true;
                                                //tile.isInShortShadeNW = false;
                                            }
                                            if (xS == 0)
                                            {
                                                tile.isInLongShadeN = true;
                                                tileCaster.hasHighlightS = true;
                                                //tile.isInShortShadeN = false;
                                            }
                                            if (xS == 1)
                                            {
                                                tile.isInLongShadeNE = true;
                                                //tile.isInShortShadeNE = false;
                                            }
                                        }

                                        //check for short shadows
                                        else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            if (xS == -1)
                                            {
                                                tile.isInShortShadeNW = true;
                                                //tile.isInLongShadeNW = false;
                                            }
                                            if (xS == 0)
                                            {
                                                tile.isInShortShadeN = true;
                                                tileCaster.hasHighlightS = true;
                                                //tile.isInLongShadeN = false;
                                            }
                                            if (xS == 1)
                                            {
                                                tile.isInShortShadeNE = true;
                                                //tile.isInLongShadeNE = false;
                                            }
                                        }

                                        /*
                                        //check if caster tile is bottom of this ramp/tile
                                        else if ((tileCaster.heightLevel == tile.heightLevel - 1) && (tile.isRamp))
                                        {
                                            if (xS == 0)
                                            {
                                                tile.hasDownStairShadowN = true;
                                            }
                                        }
                                        

                                    }
                                    else if ((tileCaster.isShadowCaster) && (tileCaster.isRamp))
                                    {
                                        if (tileCaster.heightLevel == tile.heightLevel + 2)
                                        {
                                            if (xS == -1)
                                            {
                                                tile.isInShortShadeNW = true;
                                                //tile.isInShortShadeNW = false;
                                            }
                                            if (xS == 0)
                                            {
                                                //casting ramp is on northern map here
                                                //so ramp has to be to either east or west
                                                if (tileCaster.hasDownStairShadowE)
                                                {
                                                    tile.inRampShadowNorth5Long = true;
                                                }
                                                if (tileCaster.hasDownStairShadowW)
                                                {
                                                    tile.inRampShadowNorth6Long = true;
                                                }
                                                if (tileCaster.hasDownStairShadowS)
                                                {
                                                    tile.isInShortShadeN = true;
                                                }
                                                if (tileCaster.hasDownStairShadowN)
                                                {
                                                    tile.isInLongShadeN = true;
                                                }

                                                tileCaster.hasHighlightS = true;
                                            }
                                            if (xS == 1)
                                            {
                                                tile.isInShortShadeNE = true;
                                                //tile.isInShortShadeNE = false;
                                            }
                                        }

                                        if (tileCaster.heightLevel == tile.heightLevel + 1)
                                        {
                                            if (xS == 0)
                                            {
                                                //casting ramp is on western map here
                                                //so ramp has to be to either north or south
                                                if (tileCaster.hasDownStairShadowE)
                                                {
                                                    tile.inRampShadowNorth5Short = true;
                                                }
                                                if (tileCaster.hasDownStairShadowW)
                                                {
                                                    tile.inRampShadowNorth6Short = true;
                                                }
                                                tileCaster.hasHighlightS = true;
                                            }
                                        }
                                    }
                                    */
                                }
                            }

                            //caster tile is on this map
                            //godorf

                            if ((y + yS >= 0) && (y + yS < area.MapSizeY) && (x + xS >= 0) && (x + xS < area.MapSizeX))
                            {
                                int transformedX = x + xS;
                                int transformedY = y + yS; ;
                                tileCaster = area.Tiles[transformedY * area.MapSizeX + transformedX];
                            }

                            int placebo = 0;
                            if (placebo == 0)
                            {
                                //int transformedX = x + xS;
                                //int transformedY = y + yS; ;
                                //tileCaster = area.Tiles[transformedY * area.MapSizeX + transformedX];

                                //get height level difference
                                if ((xS == 0) && (yS == -1))
                                {

                                    tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                }

                                if ((xS == 1) && (yS == 0))
                                {

                                    tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE = tile.heightLevel - tileCaster.heightLevel;
                                }

                                if ((xS == 0) && (yS == 1))
                                {

                                    tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS = tile.heightLevel - tileCaster.heightLevel;
                                }

                                if ((xS == -1) && (yS == 0))
                                {

                                    tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW = tile.heightLevel - tileCaster.heightLevel;
                                }

                                //entrancelights: bridges, same height indoors
                                /*
                                if (tile.isEWBridge)
                                {
                                    //north
                                    if ((xS == 0) && (yS == -1))
                                    {
                                        tile.hasEntranceLightNorth = true;
                                    }

                                    //south
                                    if ((xS == 0) && (yS == 1))
                                    {
                                        tile.hasEntranceLightSouth = true;
                                    }
                                }

                                if (tile.isNSBridge)
                                {
                                    //west
                                    if ((xS == -1) && (yS == 0))
                                    {
                                        tile.hasEntranceLightWest = true;
                                    }

                                    //east
                                    if ((xS == 1) && (yS == 0))
                                    {
                                        tile.hasEntranceLightEast = true;
                                    }
                                }
                                */

                                //TODO: Add maxShade
                                //check max shades for all
                                if (tileCaster.heightLevel > tile.heightLevel + 2)
                                {
                                    if ((xS == -1) && (yS == -1))
                                    {
                                        tile.isInMaxShadeNW = true;
                                    }
                                    if ((xS == 0) && (yS == -1))
                                    {
                                        tile.isInMaxShadeN = true;
                                        tileCaster.hasHighlightS = true;
                                        //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                    }
                                    if ((xS == 1) && (yS == -1))
                                    {
                                        tile.isInMaxShadeNE = true;
                                    }
                                    if ((xS == 1) && (yS == 0))
                                    {
                                        tile.isInMaxShadeE = true;
                                        tileCaster.hasHighlightW = true;
                                        //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE = tile.heightLevel - tileCaster.heightLevel;
                                    }
                                    if ((xS == 1) && (yS == 1))
                                    {
                                        tile.isInMaxShadeSE = true;
                                    }
                                    if ((xS == 0) && (yS == 1))
                                    {
                                        tile.isInMaxShadeS = true;
                                        tileCaster.hasHighlightN = true;
                                        //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS = tile.heightLevel - tileCaster.heightLevel;
                                    }
                                    if ((xS == -1) && (yS == 1))
                                    {
                                        tile.isInMaxShadeSW = true;
                                    }
                                    if ((xS == -1) && (yS == 0))
                                    {
                                        tile.isInMaxShadeW = true;
                                        tileCaster.hasHighlightE = true;
                                        //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW = tile.heightLevel - tileCaster.heightLevel;
                                    }
                                }


                                //casts shadow and is no ramp
                                if ((tileCaster.isShadowCaster) && (!tileCaster.isRamp))
                                {
                                        //check for linked map and allow deep shadows even if height level difference is greater than 2
                                        bool linkedMapException = false;

                                        if ((tileCaster.heightLevel >= tile.heightLevel + 2) && area.masterOfThisArea != "none")
                                        {
                                            linkedMapException = true;
                                        }
                                        
                                        //check for long shadows
                                    if (tileCaster.heightLevel == tile.heightLevel + 2 || linkedMapException)
                                    {
                                        if ((xS == -1) && (yS == -1))
                                        {
                                            tile.isInLongShadeNW = true;
                                        }
                                        if ((xS == 0) && (yS == -1))
                                        {
                                            tile.isInLongShadeN = true;
                                            tileCaster.hasHighlightS = true;
                                            //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                        }
                                        if ((xS == 1) && (yS == -1))
                                        {
                                            tile.isInLongShadeNE = true;
                                        }
                                        if ((xS == 1) && (yS == 0))
                                        {
                                            tile.isInLongShadeE = true;
                                            tileCaster.hasHighlightW = true;
                                            //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE = tile.heightLevel - tileCaster.heightLevel;
                                        }
                                        if ((xS == 1) && (yS == 1))
                                        {
                                            tile.isInLongShadeSE = true;
                                        }
                                        if ((xS == 0) && (yS == 1))
                                        {
                                            tile.isInLongShadeS = true;
                                            tileCaster.hasHighlightN = true;
                                            //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS = tile.heightLevel - tileCaster.heightLevel;
                                        }
                                        if ((xS == -1) && (yS == 1))
                                        {
                                            tile.isInLongShadeSW = true;
                                        }
                                        if ((xS == -1) && (yS == 0))
                                        {
                                            tile.isInLongShadeW = true;
                                            tileCaster.hasHighlightE = true;
                                            //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW = tile.heightLevel - tileCaster.heightLevel;
                                        }
                                    }

                                    //check for short shadows
                                    else if (tileCaster.heightLevel == tile.heightLevel + 1)
                                    {
                                        if ((xS == -1) && (yS == -1))
                                        {
                                            if ((tile.hasDownStairShadowN) || (tile.hasDownStairShadowW))
                                            {
                                                tile.isInLongShadeNW = true;
                                                //tileCaster.hasHighlightS = true;
                                                //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                            }
                                            else
                                            {
                                                //if ((tile.hasDownStairShadowN) || (tile.hasDownStairShadowW))
                                                //{
                                                tile.isInShortShadeNW = true;
                                                //}
                                            }
                                        }
                                        if ((xS == 0) && (yS == -1))
                                        {
                                            if (tile.hasDownStairShadowN)
                                            {
                                                tile.isInLongShadeN = true;
                                                tileCaster.hasHighlightS = true;
                                                //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeN = true;
                                                tileCaster.hasHighlightS = true;
                                            }
                                        }
                                        if ((xS == 1) && (yS == -1))
                                        {
                                            if ((tile.hasDownStairShadowN) || (tile.hasDownStairShadowE))
                                            {
                                                tile.isInLongShadeNE = true;
                                                //tileCaster.hasHighlightS = true;
                                                //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeNE = true;
                                            }
                                        }
                                        if ((xS == 1) && (yS == 0))
                                        {
                                            if (tile.hasDownStairShadowE)
                                            {
                                                tile.isInLongShadeE = true;
                                                tileCaster.hasHighlightW = true;
                                                //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeE = true;
                                                tileCaster.hasHighlightW = true;
                                            }
                                            //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE = tile.heightLevel - tileCaster.heightLevel;
                                        }
                                        if ((xS == 1) && (yS == 1))
                                        {
                                            if ((tile.hasDownStairShadowS) || (tile.hasDownStairShadowE))
                                            {
                                                tile.isInLongShadeSE = true;
                                                //tileCaster.hasHighlightS = true;
                                                //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeSE = true;
                                            }
                                        }
                                        if ((xS == 0) && (yS == 1))
                                        {
                                            if (tile.hasDownStairShadowS)
                                            {
                                                tile.isInLongShadeS = true;
                                                tileCaster.hasHighlightN = true;
                                                //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeS = true;
                                                tileCaster.hasHighlightN = true;
                                            }
                                            //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS = tile.heightLevel - tileCaster.heightLevel;
                                        }
                                        if ((xS == -1) && (yS == 1))
                                        {
                                            if ((tile.hasDownStairShadowS) || (tile.hasDownStairShadowW))
                                            {
                                                tile.isInLongShadeSW = true;
                                                //tileCaster.hasHighlightS = true;
                                                //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeSW = true;
                                            }
                                        }
                                        if ((xS == -1) && (yS == 0))
                                        {
                                            if (tile.hasDownStairShadowW)
                                            {
                                                tile.isInLongShadeW = true;
                                                tileCaster.hasHighlightE = true;
                                                //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeW = true;
                                                tileCaster.hasHighlightE = true;
                                            }
                                            //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW = tile.heightLevel - tileCaster.heightLevel;
                                        }
                                    }

                                    //check for shadows on low ramp parts of same height
                                    else if (tileCaster.heightLevel == tile.heightLevel)
                                    {
                                        if ((xS == -1) && (yS == -1))
                                        {
                                            //tile.isInShortShadeNW = true;
                                            //AddingNewEventArgs HERE
                                            //breslauer
                                            if (!tileCaster.isRamp)
                                            {
                                                if (tile.hasDownStairShadowN || tile.hasDownStairShadowW)
                                                {
                                                    tile.isInShortShadeNW = true;
                                                }
                                            }
                                        }
                                        if ((xS == 0) && (yS == -1))
                                        {
                                            if (tile.hasDownStairShadowN)
                                            {
                                                tile.isInShortShadeN = true;
                                                tileCaster.hasHighlightS = true;
                                            }

                                            if (!tileCaster.isRamp)
                                            {
                                                if (tile.hasDownStairShadowN || tile.hasDownStairShadowW)
                                                {
                                                    tile.isInShortShadeNW = true;
                                                }
                                            }
                                            //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tile.heightLevel - tileCaster.heightLevel;
                                        }
                                        if ((xS == 1) && (yS == -1))
                                        {
                                            if (!tileCaster.isRamp)
                                            {
                                                if (tile.hasDownStairShadowN || tile.hasDownStairShadowE)
                                                {
                                                    tile.isInShortShadeNE = true;
                                                }
                                            }
                                            //tile.isInShortShadeNE = true;
                                        }
                                        if ((xS == 1) && (yS == 0))
                                        {
                                            if (tile.hasDownStairShadowE)
                                            {
                                                tile.isInShortShadeE = true;
                                                tileCaster.hasHighlightW = true;
                                            }
                                            //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE = tile.heightLevel - tileCaster.heightLevel;
                                        }
                                        if ((xS == 1) && (yS == 1))
                                        {
                                            //tile.isInShortShadeSE = true;
                                            if (!tileCaster.isRamp)
                                            {
                                                if (tile.hasDownStairShadowS || tile.hasDownStairShadowE)
                                                {
                                                    tile.isInShortShadeSE = true;
                                                }
                                            }
                                        }
                                        if ((xS == 0) && (yS == 1))
                                        {
                                            if (tile.hasDownStairShadowS)
                                            {
                                                tile.isInShortShadeS = true;
                                                tileCaster.hasHighlightN = true;
                                                //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS = tile.heightLevel - tileCaster.heightLevel;
                                            }
                                        }
                                        if ((xS == -1) && (yS == 1))
                                        {
                                            //tile.isInShortShadeSW = true;
                                            if (!tileCaster.isRamp)
                                            {
                                                if (tile.hasDownStairShadowS || tile.hasDownStairShadowW)
                                                {
                                                    tile.isInShortShadeSW = true;
                                                }
                                            }
                                        }
                                        if ((xS == -1) && (yS == 0))
                                        {
                                            if (tile.hasDownStairShadowW)
                                            {
                                                tile.isInShortShadeW = true;
                                                tileCaster.hasHighlightE = true;
                                                //tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW = tile.heightLevel - tileCaster.heightLevel;
                                            }
                                        }
                                    }




                                    /*
                                    //check if caster tile is bottom of this ramp/tile
                                    else if ((tileCaster.heightLevel == tile.heightLevel - 1) && (tile.isRamp))
                                    {
                                        if ((xS == 0) && (yS == -1))
                                        {
                                            tile.hasDownStairShadowN = true;
                                        }
                                        if ((xS == 1) && (yS == 0))
                                        {
                                            tile.hasDownStairShadowE = true;
                                        }
                                        if ((xS == 0) && (yS == 1))
                                        {
                                            tile.hasDownStairShadowS = true;
                                        }
                                        if ((xS == -1) && (yS == 0))
                                        {
                                            tile.hasDownStairShadowW = true;
                                        }
                                    }
                                    */
                                }

                                //this is for the ramp casting shadow
                                //harmonie
                                else if ((tileCaster.isShadowCaster) && (tileCaster.isRamp))
                                {

                                        //check for linked map and allow deep shadows even if height level difference is greater than 2
                                        bool linkedMapException = false;

                                        if ((tileCaster.heightLevel >= tile.heightLevel + 2) && area.masterOfThisArea != "none")
                                        {
                                            linkedMapException = true;
                                        }

                                        if (tileCaster.heightLevel == tile.heightLevel + 2 || linkedMapException)
                                    {
                                        if ((xS == -1) && (yS == -1))
                                        {
                                            if (tileCaster.hasDownStairShadowN || tileCaster.hasDownStairShadowW)
                                            {
                                                tile.isInLongShadeNW = true;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeNW = true;
                                            }
                                        }
                                        if ((xS == 0) && (yS == -1))
                                        {
                                            //tile.isInShortShadeN = true;
                                            tileCaster.hasHighlightS = true;
                                            tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourS = tileCaster.heightLevel - tile.heightLevel;
                                            //enter northern map code from above here: 
                                            if (tileCaster.hasDownStairShadowE)
                                            {
                                                tile.inRampShadowNorth5Long = true;
                                            }
                                            if (tileCaster.hasDownStairShadowW)
                                            {
                                                tile.inRampShadowNorth6Long = true;
                                            }
                                            if (tileCaster.hasDownStairShadowS)
                                            {
                                                tile.isInShortShadeN = true;
                                            }
                                            if (tileCaster.hasDownStairShadowN)
                                            {
                                                tile.isInLongShadeN = true;
                                            }

                                        }
                                        if ((xS == 1) && (yS == -1))
                                        {
                                            if (tileCaster.hasDownStairShadowN || tileCaster.hasDownStairShadowE)
                                            {
                                                tile.isInLongShadeNE = true;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeNE = true;
                                            }
                                        }
                                        if ((xS == 1) && (yS == 0))
                                        {
                                            //tile.isInShortShadeE = true;
                                            tileCaster.hasHighlightW = true;
                                            tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourW = tileCaster.heightLevel - tile.heightLevel;
                                            //look for eastern map code above
                                            if (tileCaster.hasDownStairShadowN)
                                            {
                                                tile.inRampShadowEast3Long = true;
                                            }
                                            if (tileCaster.hasDownStairShadowS)
                                            {
                                                tile.inRampShadowEast4Long = true;
                                            }
                                            if (tileCaster.hasDownStairShadowW)
                                            {
                                                tile.isInShortShadeE = true;
                                            }
                                            if (tileCaster.hasDownStairShadowE)
                                            {
                                                tile.isInLongShadeE = true;
                                            }
                                        }
                                        if ((xS == 1) && (yS == 1))
                                        {
                                            if (tileCaster.hasDownStairShadowS || tileCaster.hasDownStairShadowE)
                                            {
                                                tile.isInLongShadeSE = true;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeSE = true;
                                            }
                                        }
                                        if ((xS == 0) && (yS == 1))
                                        {
                                            //tile.isInShortShadeS = true;
                                            tileCaster.hasHighlightN = true;
                                            tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tileCaster.heightLevel - tile.heightLevel;
                                            //add southern map code here
                                            if (tileCaster.hasDownStairShadowE)
                                            {
                                                tile.inRampShadowSouth7Long = true;
                                            }
                                            if (tileCaster.hasDownStairShadowW)
                                            {
                                                tile.inRampShadowSouth8Long = true;
                                            }
                                            if (tileCaster.hasDownStairShadowN)
                                            {
                                                tile.isInShortShadeS = true;
                                            }
                                            if (tileCaster.hasDownStairShadowS)
                                            {
                                                tile.isInLongShadeS = true;
                                            }
                                        }
                                        if ((xS == -1) && (yS == 1))
                                        {
                                            if (tileCaster.hasDownStairShadowS || tileCaster.hasDownStairShadowW)
                                            {
                                                tile.isInLongShadeSW = true;
                                            }
                                            else
                                            {
                                                tile.isInShortShadeSW = true;
                                            }
                                        }
                                        if ((xS == -1) && (yS == 0))
                                        {
                                            //tile.isInShortShadeW = true;
                                            tileCaster.hasHighlightE = true;
                                            tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourE = tileCaster.heightLevel - tile.heightLevel;
                                            //add western map code here
                                            if (tileCaster.hasDownStairShadowN)
                                            {
                                                tile.inRampShadowWest1Long = true;
                                            }
                                            if (tileCaster.hasDownStairShadowS)
                                            {
                                                tile.inRampShadowWest2Long = true;
                                            }
                                            if (tileCaster.hasDownStairShadowE)
                                            {
                                                tile.isInShortShadeW = true;
                                            }
                                            if (tileCaster.hasDownStairShadowW)
                                            {
                                                tile.isInLongShadeW = true;
                                            }
                                        }
                                    }

                                    if (tileCaster.heightLevel == tile.heightLevel + 1)
                                    {
                                        if ((xS == -1) && (yS == -1))
                                        {
                                            if (tileCaster.hasDownStairShadowN || tileCaster.hasDownStairShadowW)
                                            {
                                                tile.isInShortShadeNW = true;
                                            }
                                        }
                                        if ((xS == 0) && (yS == -1))
                                        {
                                            //tile.isInShortShadeN = true;
                                            //tileCaster.hasHighlightS = true;
                                            tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourS = tileCaster.heightLevel - tile.heightLevel;
                                            //enter southern map code from above here: 
                                            if (tileCaster.hasDownStairShadowE)
                                            {
                                                tile.inRampShadowNorth5Short = true;
                                            }
                                            if (tileCaster.hasDownStairShadowW)
                                            {
                                                tile.inRampShadowNorth6Short = true;
                                            }
                                            if (tileCaster.hasDownStairShadowN)
                                            {
                                                tile.isInShortShadeN = true;
                                                tileCaster.hasHighlightS = true;
                                            }
                                            if (tileCaster.hasDownStairShadowS)
                                            {
                                                if (tile.hasDownStairShadowN)
                                                {
                                                    tile.isInShortShadeN = true;
                                                }
                                                if (tile.hasDownStairShadowE)
                                                {
                                                    tile.inRampShadowNorth6Short = true;
                                                }
                                                if (tile.hasDownStairShadowW)
                                                {
                                                    tile.inRampShadowNorth5Short = true;
                                                }
                                            }
                                        }
                                        if ((xS == 1) && (yS == -1))
                                        {
                                            if (tileCaster.hasDownStairShadowN || tileCaster.hasDownStairShadowE)
                                            {
                                                tile.isInShortShadeNE = true;
                                            }
                                        }
                                        if ((xS == 1) && (yS == 0))
                                        {
                                            //tile.isInShortShadeE = true;
                                            //tileCaster.hasHighlightW = true;
                                            tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourW = tileCaster.heightLevel - tile.heightLevel;
                                            //look for eastern map code above

                                            if (tileCaster.hasDownStairShadowN)
                                            {
                                                if (tile.hasDownStairShadowE)
                                                {
                                                    tile.inRampShadowEast3Long = true;
                                                }
                                                else
                                                {
                                                    tile.inRampShadowEast3Short = true;
                                                }
                                            }

                                            if (tileCaster.hasDownStairShadowS)
                                            {
                                                tile.inRampShadowEast4Short = true;
                                            }
                                            if (tileCaster.hasDownStairShadowE)
                                            {
                                                tile.isInShortShadeE = true;
                                                tileCaster.hasHighlightW = true;
                                            }

                                            if (tileCaster.hasDownStairShadowW)
                                            {
                                                if (tile.hasDownStairShadowE)
                                                {
                                                    tile.isInShortShadeE = true;
                                                }
                                                if (tile.hasDownStairShadowN)
                                                {
                                                    tile.inRampShadowEast4Short = true;
                                                }
                                                if (tile.hasDownStairShadowS)
                                                {
                                                    tile.inRampShadowEast3Short = true;
                                                }
                                            }

                                            /*
                                        //tile is ramp and 1 level higher
                                        else
                                        {
                                            if (tileCaster.hasDownStairShadowN)
                                            {
                                                if (tile.hasDownStairShadowE)
                                                {
                                                    tile.inRampShadowEast3Long = true;
                                                }
                                                else
                                                {
                                                    tile.inRampShadowEast3Short = true;
                                                }
                                            }


                                            if (tileCaster.hasDownStairShadowS)
                                            {
                                                tile.inRampShadowEast4Short = true;
                                            }
                                            if (tileCaster.hasDownStairShadowE)
                                            {
                                                tile.isInShortShadeE = true;
                                            }
                                        }
                                        //gtx1080 add tile is ramp sitauations here, smae likely for two height levels difference an same height, too
                                        */
                                        }
                                        if ((xS == 1) && (yS == 1))
                                        {
                                            if (tileCaster.hasDownStairShadowS || tileCaster.hasDownStairShadowE)
                                            {
                                                tile.isInShortShadeSE = true;
                                            }
                                        }
                                        if ((xS == 0) && (yS == 1))
                                        {
                                            //tile.isInShortShadeS = true;
                                            //tileCaster.hasHighlightN = true;
                                            tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tileCaster.heightLevel - tile.heightLevel;
                                            //add southern map code here
                                            //azraeli
                                            if (tileCaster.hasDownStairShadowE)
                                            {
                                                tile.inRampShadowSouth7Short = true;
                                            }
                                            if (tileCaster.hasDownStairShadowW)
                                            {
                                                tile.inRampShadowSouth8Short = true;
                                            }
                                            if (tileCaster.hasDownStairShadowS)
                                            {
                                                tile.isInShortShadeS = true;
                                                tileCaster.hasHighlightN = true;
                                            }
                                            if (tileCaster.hasDownStairShadowN)
                                            {
                                                if (tile.hasDownStairShadowS)
                                                {
                                                    tile.isInShortShadeS = true;
                                                }
                                                if (tile.hasDownStairShadowW)
                                                {
                                                    tile.inRampShadowSouth7Short = true;
                                                }
                                                if (tile.hasDownStairShadowE)
                                                {
                                                    tile.inRampShadowSouth8Short = true;
                                                }
                                            }
                                        }
                                        if ((xS == -1) && (yS == 1))
                                        {
                                            if (tileCaster.hasDownStairShadowS || tileCaster.hasDownStairShadowW)
                                            {
                                                tile.isInShortShadeSW = true;
                                            }
                                        }
                                        if ((xS == -1) && (yS == 0))
                                        {
                                            //tile.isInShortShadeW = true;
                                            //tileCaster.hasHighlightE = true;
                                            tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourE = tileCaster.heightLevel - tile.heightLevel;
                                            //add western map code here
                                            if (tileCaster.hasDownStairShadowN)
                                            {
                                                tile.inRampShadowWest1Short = true;
                                            }
                                            if (tileCaster.hasDownStairShadowS)
                                            {
                                                tile.inRampShadowWest2Short = true;
                                            }
                                            if (tileCaster.hasDownStairShadowW)
                                            {
                                                tile.isInShortShadeW = true;
                                                tileCaster.hasHighlightE = true;
                                            }
                                            if (tileCaster.hasDownStairShadowE)
                                            {
                                                if (tile.hasDownStairShadowW)
                                                {
                                                    tile.isInShortShadeW = true;
                                                }
                                                if (tile.hasDownStairShadowN)
                                                {
                                                    tile.inRampShadowWest2Short = true;
                                                }
                                                if (tile.hasDownStairShadowS)
                                                {
                                                    tile.inRampShadowWest1Short = true;
                                                }
                                            }
                                        }
                                    }

                                    //upper RAMP part of neighbouring square casting on same level lower RAMP part of this square
                                    //adding also other ramp to ramp shadows now
                                    if (tileCaster.heightLevel == tile.heightLevel)
                                    {
                                        if (tile.isRamp)
                                        {
                                            //caster from the northwest
                                            if ((xS == -1) && (yS == -1))
                                            {

                                                if (tileCaster.hasDownStairShadowN || tileCaster.hasDownStairShadowW)
                                                {
                                                    if (tile.hasDownStairShadowN || tile.hasDownStairShadowW)
                                                    {
                                                        tile.isInShortShadeNW = true;
                                                    }
                                                }
                                            }
                                            //north
                                            if ((xS == 0) && (yS == -1))
                                            {
                                                //freedom
                                                //tile.isInShortShadeN = true;
                                                //tileCaster.hasHighlightS = true;
                                                //tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourS = tileCaster.heightLevel - tile.heightLevel + 1;
                                                tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = -1;

                                                if (tileCaster.hasDownStairShadowN)
                                                {
                                                    if (tile.hasDownStairShadowN)
                                                    {
                                                        tile.isInShortShadeN = true;
                                                        tileCaster.hasHighlightS = true;
                                                    }
                                                    if (tile.hasDownStairShadowE || tile.hasDownStairShadowW)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = 0;
                                                    }
                                                }

                                                if (tileCaster.hasDownStairShadowE)
                                                {
                                                    if (tile.hasDownStairShadowN)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = -2;
                                                    }

                                                    if (tile.hasDownStairShadowW)
                                                    {
                                                        tile.inSmallStairNWHorizontal = true;
                                                    }
                                                }
                                                if (tileCaster.hasDownStairShadowW)
                                                {
                                                    if (tile.hasDownStairShadowN)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourN = -3;
                                                    }

                                                    if (tile.hasDownStairShadowE)
                                                    {
                                                        tile.inSmallStairNEHorizontal = true;
                                                    }
                                                }

                                            }

                                            //NE
                                            if ((xS == 1) && (yS == -1))
                                            {
                                                if (tileCaster.hasDownStairShadowE || tileCaster.hasDownStairShadowN)
                                                {
                                                    if (tile.hasDownStairShadowE || tile.hasDownStairShadowN)
                                                    {
                                                        tile.isInShortShadeNE = true;
                                                    }
                                                }



                                                /*
                                                if (tileCaster.hasDownStairShadowE || tileCaster.hasDownStairShadowN)
                                                {
                                                    if (tile.hasDownStairShadowN || tileCaster.hasDownStairShadowE)
                                                    {
                                                        tile.isInLongShadeNE = true;
                                                    }
                                                }
                                                */

                                            }

                                            //E
                                            if ((xS == 1) && (yS == 0))
                                            {
                                                //tile.isInShortShadeE = true;
                                                //tileCaster.hasHighlightW = true;
                                                //tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourW = tileCaster.heightLevel - tile.heightLevel + 1;
                                                tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE = -1;
                                                if (tileCaster.hasDownStairShadowE)
                                                {
                                                    if (tile.hasDownStairShadowE)
                                                    {
                                                        tile.isInShortShadeE = true;
                                                        tileCaster.hasHighlightW = true;
                                                    }
                                                    if (tile.hasDownStairShadowN || tile.hasDownStairShadowS)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE = 0;
                                                    }
                                                }

                                                if (tileCaster.hasDownStairShadowN)
                                                {
                                                    if (tile.hasDownStairShadowE)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE = -2;
                                                    }

                                                    if (tile.hasDownStairShadowS)
                                                    {
                                                        tile.inSmallStairSEVertical = true;
                                                    }
                                                }

                                                if (tileCaster.hasDownStairShadowS)
                                                {
                                                    if (tile.hasDownStairShadowE)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourE = -3;
                                                    }

                                                    if (tile.hasDownStairShadowN)
                                                    {
                                                        tile.inSmallStairNEVertical = true;
                                                    }

                                                }
                                            }
                                            //SE
                                            if ((xS == 1) && (yS == 1))
                                            {
                                                if (tileCaster.hasDownStairShadowE || tileCaster.hasDownStairShadowS)
                                                {
                                                    if (tile.hasDownStairShadowE || tile.hasDownStairShadowS)
                                                    {
                                                        tile.isInShortShadeSE = true;
                                                    }
                                                }

                                                /*
                                                if (tileCaster.hasDownStairShadowE || tileCaster.hasDownStairShadowS)
                                                {
                                                    if (tile.hasDownStairShadowS || tileCaster.hasDownStairShadowE)
                                                    {
                                                        tile.isInLongShadeNE = true;
                                                    }
                                                }
                                                */
                                                //tile.isInShortShadeSE = true;
                                            }

                                            //S
                                            if ((xS == 0) && (yS == 1))
                                            {
                                                //tile.isInShortShadeS = true;
                                                //tileCaster.hasHighlightN = true;
                                                //tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourN = tileCaster.heightLevel - tile.heightLevel+1;
                                                tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS = -1;
                                                if (tileCaster.hasDownStairShadowS)
                                                {
                                                    if (tile.hasDownStairShadowS)
                                                    {
                                                        tile.isInShortShadeS = true;
                                                        tileCaster.hasHighlightN = true;
                                                    }
                                                    if (tile.hasDownStairShadowE || tile.hasDownStairShadowW)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS = 0;
                                                    }
                                                }
                                                if (tileCaster.hasDownStairShadowE)
                                                {
                                                    if (tile.hasDownStairShadowS)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS = -2;
                                                    }
                                                    if (tile.hasDownStairShadowW)
                                                    {
                                                        tile.inSmallStairSWHorizontal = true;
                                                    }
                                                }

                                                if (tileCaster.hasDownStairShadowW)
                                                {
                                                    if (tile.hasDownStairShadowS)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourS = -3;
                                                    }
                                                    if (tile.hasDownStairShadowE)
                                                    {
                                                        tile.inSmallStairSEHorizontal = true;
                                                    }
                                                }
                                            }

                                            //SW
                                            if ((xS == -1) && (yS == 1))
                                            {
                                                if (tileCaster.hasDownStairShadowW || tileCaster.hasDownStairShadowS)
                                                {
                                                    if (tile.hasDownStairShadowW || tile.hasDownStairShadowS)
                                                    {
                                                        tile.isInShortShadeSW = true;
                                                    }
                                                }

                                                /*
                                                if (tileCaster.hasDownStairShadowW || tileCaster.hasDownStairShadowS)
                                                {
                                                    if (tile.hasDownStairShadowS || tileCaster.hasDownStairShadowW)
                                                    {
                                                        tile.isInLongShadeNE = true;
                                                    }
                                                }
                                                */
                                                //tile.isInShortShadeSW = true;
                                            }

                                            //W
                                            if ((xS == -1) && (yS == 0))
                                            {
                                                //tile.isInShortShadeW = true;
                                                //tileCaster.hasHighlightE = true;
                                                //tileCaster.numberOfHeightLevelsThisTileisHigherThanNeighbourE = tileCaster.heightLevel - tile.heightLevel + 1;
                                                tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW = -1;
                                                if (tileCaster.hasDownStairShadowW)
                                                {
                                                    if (tile.hasDownStairShadowW)
                                                    {
                                                        tile.isInShortShadeW = true;
                                                        tileCaster.hasHighlightE = true;
                                                    }
                                                    if (tile.hasDownStairShadowN || tile.hasDownStairShadowS)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW = 0;
                                                    }
                                                }

                                                if (tileCaster.hasDownStairShadowN)
                                                {
                                                    if (tile.hasDownStairShadowW)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW = -2;
                                                    }

                                                    if (tile.hasDownStairShadowS)
                                                    {
                                                        tile.inSmallStairSWVertical = true;
                                                    }
                                                }

                                                if (tileCaster.hasDownStairShadowS)
                                                {
                                                    if (tile.hasDownStairShadowW)
                                                    {
                                                        tile.numberOfHeightLevelsThisTileisHigherThanNeighbourW = -3;
                                                    }

                                                    if (tile.hasDownStairShadowN)
                                                    {
                                                        tile.inSmallStairNWVertical = true;
                                                    }
                                                }
                                            }
                                        }

                                    }//end

                                }

                            }

                        }
                    }//try  
                    }
                }
            }
            #endregion
            area.averageHeightOnThisMap = heightSum / (area.Tiles.Count);

            mod.moduleAreasObjects.Clear();
        }

        public void CleanUpDrawTextResources()
        {
            if (textFormat != null)
            {
                textFormat.Dispose();
                textFormat = null;
            }
            if (textLayout != null)
            {
                textLayout.Dispose();
                textLayout = null;
            }
        }
        public void DrawText(string text, float xLoc, float yLoc, float scaler)
        {
            DrawText(text, xLoc, yLoc, scaler, SharpDX.Color.White);
        }
        public void DrawText(string text, float x, float y, float scaler, SharpDX.Color fontColor)
        {
            CleanUpDrawTextResources();
            using (SolidColorBrush scb = new SolidColorBrush(RenderTarget2D, fontColor))
            {
                textFormat = new TextFormat(FactoryDWrite, "Arial", scaler);
                //textFormat = new TextFormat(factoryDWrite, thisFont.FontFamily.Name, fw, fs, FontStretch.Normal, thisFont.Height) { TextAlignment = TextAlignment.Leading, ParagraphAlignment = ParagraphAlignment.Near };
                //textFormat = new TextFormat(factoryDWrite, FontFamilyName, CurrentFontCollection, fw, fs, FontStretch.Normal, scaler) { TextAlignment = TextAlignment.Leading, ParagraphAlignment = ParagraphAlignment.Near };
                textLayout = new TextLayout(FactoryDWrite, text, textFormat, this.Width, this.Height);
                RenderTarget2D.DrawTextLayout(new SharpDX.Vector2(x, y), textLayout, scb, DrawTextOptions.None);
            }
        }


        private void loadAreaObjectBitmapLists()
        {
            /*//GDI foreach (Prop prp in area.Props)
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
            }*/
        }
        private void openLevel(string g_dir, string g_fil, string g_filNoEx)
        {
            //this.Cursor = Cursors.WaitCursor;
            /*
             Area areaOrg = new Area();
                        bool orgIsStillOpen = false;
                        for (int a = 0; a < prntForm.openAreasList.Count; a++)
                        {
                            if (prntForm.openAreasList[a].Filename == area.masterOfThisArea)
                            {
                                areaOrg = prntForm.openAreasList[a];
                                orgIsStillOpen = true;
                                break;
                            }
                        }

                        if (orgIsStillOpen)
                        {

            */
            //looks like 4 constellations to consider
            //1. what is currently being opened - master or link?
            //2. is link(s) or master of this to be opened area already open?


            try
            {
                area = area.loadAreaFile(g_dir + "\\" + g_fil + ".lvl");
                //Area areaOrg = new Area();

                if (area == null)
                {
                    MessageBox.Show("returned a null area");
                }

                //get shadows right while opening already
                //calculateHeightShadows();


                if (area.masterOfThisArea != "none")
                {
                    Area areaOrg = new Area();
                    //1. link to be opened, master is open
                    bool orgIsStillOpen = false;
                    for (int a = 0; a < prntForm.openAreasList.Count; a++)
                    {
                        if (prntForm.openAreasList[a].Filename == area.masterOfThisArea)
                        {
                            areaOrg = prntForm.openAreasList[a];
                            orgIsStillOpen = true;
                            break;
                        }
                    }
                    //2. link to be opened, master not open
                    if (!orgIsStillOpen)
                    {
                        areaOrg = areaOrg.loadAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                    }

                    //roterZwerg
                    for (int i = 0; i < areaOrg.Tiles.Count(); i++)
                    {
                        bool resetTile = true;
                        int TileLocX = i % areaOrg.MapSizeY;
                        int TileLocY = i / areaOrg.MapSizeX;

                        foreach (Trigger trig in areaOrg.Triggers)
                        {
                            if (trig.isLinkToMaster)
                            {
                                if (trig.TriggerSquaresList[0].X == TileLocX && trig.TriggerSquaresList[0].Y == TileLocY)
                                {
                                    resetTile = false;
                                    break;
                                }
                            }
                        }

                        if (resetTile)
                        {
                            areaOrg.Tiles[i].transitionToMasterDirection = "none";
                            areaOrg.Tiles[i].numberOfLinkedAreaToTransitionTo = -1;
                        }
                    }

                    for (int j = 0; j < area.Tiles.Count; j++)
                    {
                        bool temp = area.Tiles[j].linkedToMasterMap;
                        string temp2 = area.Tiles[j].transitionToMasterDirection;
                        int temp3 = area.Tiles[j].numberOfLinkedAreaToTransitionTo;
                        if (area.Tiles[j].linkedToMasterMap)
                        {
                            area.Tiles[j] = areaOrg.Tiles[j].ShallowCopy();
                        }
                        //newArea = newArea.loadAreaFile(path + areaName + ".lvl");
                        area.Tiles[j].linkedToMasterMap = temp;
                        area.Tiles[j].transitionToMasterDirection = temp2;
                        area.Tiles[j].numberOfLinkedAreaToTransitionTo = temp3;
                        if (area.Tiles[j].linkedToMasterMap)
                        {
                            area.Tiles[j].LoSBlocked = true;
                        }
                    }

                    //add new props (placed after creation of clone)
                    for (int j = 0; j < areaOrg.Props.Count; j++)
                    {
                        bool allowAdding = false;
                        if (!areaOrg.Props[j].isMover)
                        {
                            allowAdding = true;
                            for (int k = 0; k < area.Props.Count; k++)
                            {
                                if (areaOrg.Props[j].PropTag == area.Props[k].PropTag)
                                {
                                    allowAdding = false;
                                    break;
                                }
                            }
                            if (allowAdding)
                            {
                                area.Props.Add(areaOrg.Props[j]);
                            }
                        }
                    }

                    //remove props that dont exist on master any more
                    for (int i = area.Props.Count - 1; i >= 0; i--)
                    {
                        bool removeThisProp = true;
                        foreach (Prop p in areaOrg.Props)
                        {
                            if (p.PropTag == area.Props[i].PropTag)
                            {
                                removeThisProp = false;
                                break;
                            }
                        }

                        if (removeThisProp)
                        {
                            area.Props.Remove(area.Props[i]);
                        }
                    }
                }


                /*
                int index = -1;
                for (int i = 0; i < mod.moduleAreasList.Count; i++)
                {
                    if (area.masterOfThisArea ==  mod.moduleAreasList[i])
                    {
                        index = i;
                        break;
                    }
                }
                */

                
                //patrick1000
                //3. master to be opened
                else
                {
                    Area areaLink = new Area();
                    //go through all links and check for each whwter open or not
                    for (int l = 0; l < area.linkedAreas.Count; l++)
                   {
                        bool linkIsStillOpen = false;
                        for (int a = 0; a < prntForm.openAreasList.Count; a++)
                        {
                            if (prntForm.openAreasList[a].Filename == area.linkedAreas[l])
                            {
                                areaLink = prntForm.openAreasList[a];
                                linkIsStillOpen = true;
                                break;
                            }
                        }

                        if(!linkIsStillOpen)
                        {
                            areaLink = areaLink.loadAreaFile(g_dir + "\\" + area.linkedAreas[l] + ".lvl");
                        }

                        //now the code for adding triggers
                        foreach (Trigger t in areaLink.Triggers)
                        {
                            if (t.isLinkToMaster)
                            {
                                //need a tag or position comparison in roder to preevnt adding triggers double time
                                bool triggerDoesNotExistAlready = true;
                                foreach (Trigger t2 in area.Triggers)
                                {
                                    //if ((t2.TriggerSquaresList[0].X == t.TriggerSquaresList[0].X) && (t2.TriggerSquaresList[0].Y == t.TriggerSquaresList[0].Y))
                                    if (t2.TriggerTag == (t.TriggerTag + "_" + area.Filename))
                                    {
                                        triggerDoesNotExistAlready = false;
                                        break;
                                    }
                                }
                                if (triggerDoesNotExistAlready)
                                {
                                    area.Triggers.Add(t);
                                }
                            }
                        }

                        //need code for removing deleted triggers
                        //foreach (Trigger t in area.Triggers)
                        //need to check that we only remove triggrs tat have a dead connection to the linked area currently checked
                        for (int tIndex = area.Triggers.Count -1; tIndex >= 0; tIndex--)
                        {
                            if ((area.Triggers[tIndex].isLinkToMaster) && (area.Triggers[tIndex].tagOfLink == areaLink.Filename))
                            {
                                //need a tag or position comparison in roder to preevnt adding triggers double time
                                bool triggerHasDeadEnd = true;
                                foreach (Trigger t2 in areaLink.Triggers)
                                {
                                    if (area.Triggers[tIndex].TriggerTag == (t2.TriggerTag + "_" + area.Filename))
                                    {
                                        //if (t2.TriggerSquaresList.Count > 0 && area.Triggers[tIndex].TriggerSquaresList.Count > 0)
                                        //{
                                        //if ((t2.TriggerSquaresList[0].X == area.Triggers[tIndex].TriggerSquaresList[0].X) && (t2.TriggerSquaresList[0].Y == area.Triggers[tIndex].TriggerSquaresList[0].Y))
                                        //{
                                        triggerHasDeadEnd = false;
                                        break;
                                        //}
                                        //}
                                    }
                                }
                                if (triggerHasDeadEnd)
                                {
                                    area.Triggers.RemoveAt(tIndex);
                                }
                            }
                        }
                    }
                }

                //get shadows right while opening already
                calculateHeightShadows();

                loadAreaObjectBitmapLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open file: " + ex.ToString());
            }
            // load JPG Map first
            /*//GDI try
            {
                if (!area.ImageFileName.Equals("none"))
                {
                    if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".jpg"))
                    {
                        gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".jpg");
                    }
                    else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName))
                    {
                        gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName);
                    }
                    else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".png"))
                    {
                        gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".png");
                    }
                    else
                    {
                        gameMapBitmap = null;
                    }
                }
            }
            catch
            {
                gameMapBitmap = null;
            }*/


            if (useDirect2D)
            {
                //TODO add D2D stuff
                InitDirect2DAndDirectWrite();
            }
            /*//GDI else
            {
                panelView.Width = area.MapSizeX * sqr;
                panelView.Height = area.MapSizeY * sqr;
                panelView.BackgroundImage = (Image)surface;
                device = Graphics.FromImage(surface);
                if (surface == null)
                {
                    MessageBox.Show("returned a null Map bitmap");
                    return;
                }
            }*/
            refreshLeftPanelInfo();
            //GDI refreshMap(true);
            //this.Cursor = Cursors.Arrow;
        }

        private void synchLevel()
        {

            int backupIndex = 0;

            for (int i = 0; i < prntForm.mod.moduleAreasList.Count; i++)
            {
                if (prntForm.mod.moduleAreasList[i] == area.Filename)
                {
                    backupIndex = i;
                }
            }

            //this.Cursor = Cursors.WaitCursor;

            //try simpler
            //area.saveAreaFile(prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas\\" + area.Filename + ".lvl");
            //this.Close();
            
            //area.saveAreaFile();
            string g_dir = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas";
            string g_fil = mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
            try
            {
                //try simpler
                //area = area.loadAreaFile(g_dir + "\\" + g_fil + ".lvl");

                //prntForm.mod.moduleAreasList;
                //prntForm.frmAreas.lbxAreas.Items.
                //lbxAreas.Items.Count

                /*
                lbxAreas.Items.Count
                 if (lbxAreas.SelectedIndex >= 0)
            {
                prntForm._selectedLbxAreaIndex = lbxAreas.SelectedIndex;
                txtAreaName.Text = prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
                lbxAreas.SelectedIndex = prntForm._selectedLbxAreaIndex;                
            }
                */
                Area areaOrg = new Area();

                if (area == null)
                {
                    MessageBox.Show("returned a null area");
                }

                //current area is a linked area, so we pull data for it from the master
                if (area.masterOfThisArea != "none")
                {
                    /*
                    int index = -1;
                    for (int i = 0; i < mod.moduleAreasList.Count; i++)
                    {
                        if (area.masterOfThisArea ==  mod.moduleAreasList[i])
                        {
                            index = i;
                            break;
                        }
                    }
                    */

                    //prntForm.openAreasList.Remove(area);
                    bool orgIsStillOpen = false;
                    for (int a = 0; a < prntForm.openAreasList.Count; a++)
                    {
                        if (prntForm.openAreasList[a].Filename == area.masterOfThisArea)
                        {
                            areaOrg = prntForm.openAreasList[a];
                            orgIsStillOpen = true;
                            break;
                        }
                    }

                    if (!orgIsStillOpen)
                    {

                        areaOrg = areaOrg.loadAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                    }
                    //patrick10
                    //if (index != -1)
                    //{
                    for (int j = 0; j < area.Tiles.Count; j++)
                    {
                        bool temp = area.Tiles[j].linkedToMasterMap;
                        string temp2 = area.Tiles[j].transitionToMasterDirection;
                        int temp3 = area.Tiles[j].numberOfLinkedAreaToTransitionTo ;
                        if (area.Tiles[j].linkedToMasterMap)
                        {
                            area.Tiles[j] = areaOrg.Tiles[j].ShallowCopy();
                        }
                        //newArea = newArea.loadAreaFile(path + areaName + ".lvl");
                        area.Tiles[j].linkedToMasterMap = temp;
                        area.Tiles[j].transitionToMasterDirection = temp2;
                        area.Tiles[j].numberOfLinkedAreaToTransitionTo = temp3;
                        if (area.Tiles[j].linkedToMasterMap)
                        {
                            area.Tiles[j].LoSBlocked = true;
                        }
                    }

                    //add new props (placed after creation of clone)
                    for (int j = 0; j < areaOrg.Props.Count; j++)
                    {
                        bool allowAdding = false;
                        if (!areaOrg.Props[j].isMover)
                        {
                            allowAdding = true;
                            for (int k = 0; k < area.Props.Count; k++)
                            {
                                if (areaOrg.Props[j].PropTag == area.Props[k].PropTag)
                                {
                                    allowAdding = false;
                                    break;
                                }
                            }
                            if (allowAdding)
                            {
                                if (area.Tiles[areaOrg.Props[j].LocationY * area.MapSizeX + areaOrg.Props[j].LocationX].linkedToMasterMap)
                                {
                                    area.Props.Add(areaOrg.Props[j]);
                                }
                            }
                        }
                    }

                    //remove props that dont exist on master any more
                for (int i = area.Props.Count - 1; i >= 0; i--)
                {
                        bool removeThisProp = true;
                        foreach ( Prop p in areaOrg.Props)
                        {
                            if (p.PropTag == area.Props[i].PropTag)
                            {
                                removeThisProp = false;
                                break;
                            }
                        }

                        if (removeThisProp)
                        {
                            area.Props.Remove(area.Props[i]);
                        }
                }    


             }

                //current area is a master, we need to force a fresh somehow, it alreday has all the dat stored, but nor drawn
                //patrick20
                //else if (area.linkedAreas.Count > 0)
                //{

                //}
                //try simpler
                //prntForm._selectedLbxAreaIndex = backupIndex;
                //prntForm.frmAreas.lbxAreas.SelectedIndex = backupIndex;
                //prntForm.frmAreas.EditArea();
                loadAreaObjectBitmapLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open file: " + ex.ToString());
            }
            // load JPG Map first
            /*//GDI try
            {
                if (!area.ImageFileName.Equals("none"))
                {
                    if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".jpg"))
                    {
                        gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".jpg");
                    }
                    else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName))
                    {
                        gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName);
                    }
                    else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".png"))
                    {
                        gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".png");
                    }
                    else
                    {
                        gameMapBitmap = null;
                    }
                }
            }
            catch
            {
                gameMapBitmap = null;
            }*/


            if (useDirect2D)
            {
                //TODO add D2D stuff
                //InitDirect2DAndDirectWrite();
            }
            /*//GDI else
            {
                panelView.Width = area.MapSizeX * sqr;
                panelView.Height = area.MapSizeY * sqr;
                panelView.BackgroundImage = (Image)surface;
                device = Graphics.FromImage(surface);
                if (surface == null)
                {
                    MessageBox.Show("returned a null Map bitmap");
                    return;
                }
            }*/
            refreshLeftPanelInfo();
            calculateHeightShadows();
            //GDI refreshMap(true);
            //this.Cursor = Cursors.Arrow;
        }

        private void synchAllLevels()
        {
            //we will need to call areas taht a re master or a link
            int backupIndex = 0;

            for (int i = 0; i < prntForm.mod.moduleAreasList.Count; i++)
            {
                if (prntForm.mod.moduleAreasList[i] == area.Filename)
                {
                    backupIndex = i;
                }
            }

            //this.Cursor = Cursors.WaitCursor;

            //try simpler
            //area.saveAreaFile(prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas\\" + area.Filename + ".lvl");
            //this.Close();

            //area.saveAreaFile();
            string g_dir = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas";
            string g_fil = mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
            try
            {
                //try simpler
                //area = area.loadAreaFile(g_dir + "\\" + g_fil + ".lvl");

                //prntForm.mod.moduleAreasList;
                //prntForm.frmAreas.lbxAreas.Items.
                //lbxAreas.Items.Count

                /*
                lbxAreas.Items.Count
                 if (lbxAreas.SelectedIndex >= 0)
            {
                prntForm._selectedLbxAreaIndex = lbxAreas.SelectedIndex;
                txtAreaName.Text = prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
                lbxAreas.SelectedIndex = prntForm._selectedLbxAreaIndex;                
            }
                */
                Area areaOrg = new Area();

                if (area == null)
                {
                    MessageBox.Show("returned a null area");
                }

                //current area is a linked area, so we pull data for it from the master
                if (area.masterOfThisArea != "none")
                {
                    /*
                    int index = -1;
                    for (int i = 0; i < mod.moduleAreasList.Count; i++)
                    {
                        if (area.masterOfThisArea ==  mod.moduleAreasList[i])
                        {
                            index = i;
                            break;
                        }
                    }
                    */

                    //prntForm.openAreasList.Remove(area);
                    bool orgIsStillOpen = false;
                    for (int a = 0; a < prntForm.openAreasList.Count; a++)
                    {
                        if (prntForm.openAreasList[a].Filename == area.masterOfThisArea)
                        {
                            areaOrg = prntForm.openAreasList[a];
                            orgIsStillOpen = true;
                            break;
                        }
                    }

                    if (!orgIsStillOpen)
                    {

                        areaOrg = areaOrg.loadAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                    }
                    //patrick10
                    //if (index != -1)
                    //{
                    for (int j = 0; j < area.Tiles.Count; j++)
                    {
                        bool temp = area.Tiles[j].linkedToMasterMap;
                        if (area.Tiles[j].linkedToMasterMap)
                        {
                            area.Tiles[j] = areaOrg.Tiles[j].ShallowCopy();
                        }
                        //newArea = newArea.loadAreaFile(path + areaName + ".lvl");
                        area.Tiles[j].linkedToMasterMap = temp;
                        if (area.Tiles[j].linkedToMasterMap)
                        {
                            area.Tiles[j].LoSBlocked = true;
                        }
                    }
                    //}
                }

                //current area is a master, we need to force a fresh somehow, it alreday has all the dat stored, but nor drawn
                //patrick20
                else if (area.linkedAreas.Count > 0)
                {

                }
                //try simpler
                //prntForm._selectedLbxAreaIndex = backupIndex;
                //prntForm.frmAreas.lbxAreas.SelectedIndex = backupIndex;
                //prntForm.frmAreas.EditArea();
                loadAreaObjectBitmapLists();
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to open file: " + ex.ToString());
            }
            // load JPG Map first
            /*//GDI try
            {
                if (!area.ImageFileName.Equals("none"))
                {
                    if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".jpg"))
                    {
                        gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".jpg");
                    }
                    else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName))
                    {
                        gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName);
                    }
                    else if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".png"))
                    {
                        gameMapBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + area.ImageFileName + ".png");
                    }
                    else
                    {
                        gameMapBitmap = null;
                    }
                }
            }
            catch
            {
                gameMapBitmap = null;
            }*/


            if (useDirect2D)
            {
                //TODO add D2D stuff
                //InitDirect2DAndDirectWrite();
            }
            /*//GDI else
            {
                panelView.Width = area.MapSizeX * sqr;
                panelView.Height = area.MapSizeY * sqr;
                panelView.BackgroundImage = (Image)surface;
                device = Graphics.FromImage(surface);
                if (surface == null)
                {
                    MessageBox.Show("returned a null Map bitmap");
                    return;
                }
            }*/
            refreshLeftPanelInfo();
            calculateHeightShadows();
            //GDI refreshMap(true);
            //this.Cursor = Cursors.Arrow;
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
            for (int index = 0; index < (width * height); index++)
            {
                Tile newTile = new Tile();
                newTile.Walkable = true;
                newTile.LoSBlocked = false;
                newTile.Visible = false;
                area.Tiles.Add(newTile);
            }
            //GDI gameMapBitmap = null;
            refreshLeftPanelInfo();
            
            if (useDirect2D)
            {
                //TODO set panel to use D2D
            }
            /*//GDI else
            {
                panelView.Width = area.MapSizeX * sqr;
                panelView.Height = area.MapSizeY * sqr;
                panelView.BackgroundImage = (Image)surface;
                device = Graphics.FromImage(surface);
                if (surface == null)
                {
                    MessageBox.Show("returned a null Map bitmap");
                    return;
                }
            }*/
            //GDI refreshMap(true);
        }
        private void resetAreaTileValues(int width, int height)
        {
            //create tilemap
            area.MapSizeX = width;
            area.MapSizeY = height;
            for (int index = 0; index < (width * height); index++)
            {
                Tile newTile = new Tile();
                newTile.Walkable = true;
                newTile.LoSBlocked = false;
                newTile.Visible = false;
                area.Tiles.Add(newTile);
            }
        }
        public void refreshLeftPanelInfo()
        {
            lblMapSizeX.Text = area.MapSizeX.ToString();
            lblMapSizeY.Text = area.MapSizeY.ToString();
            numBGLocX.Value = area.backgroundImageStartLocX;
            numBGLocY.Value = area.backgroundImageStartLocY;
            selectedTile.x = gridX;
            selectedTile.y = gridY;
            selectedTile.index = gridY * area.MapSizeX + gridX;
            //GDI drawSelectionBox(gridX, gridY);
            selectionBoxLocation = new Point(gridX, gridY);
        }
        private void mapSizeChangeStuff()
        {
            lblMapSizeX.Text = area.MapSizeX.ToString();
            lblMapSizeY.Text = area.MapSizeY.ToString();
            //GDI resetPanelAndDeviceSize();
        }
        #endregion

        #region Event Handlers
        private void btnLoadMap_Click(object sender, EventArgs e)
        {
            if (mod.useAllTileSystem)
            {
                #region new tile cutting system
                if (mod.moduleName != "NewModule")
                {
                    openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics";
                }
                else
                {
                    openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\default\\NewModule";
                }
                bool isPng = false;
                bool isJpg = false;
                openFileDialog1.FileName = String.Empty;
                //allow .png maps, too (their transparency allows to show bottom layer full screen effects, like the sea)
                openFileDialog1.Filter = "Map (*.jpg)|*.jpg|Map (*.png)|*.png";
                openFileDialog1.FilterIndex = 1;

                DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.  
                {
                    string pictureType = Path.GetFullPath(openFileDialog1.FileName);

                    if (pictureType.Contains("png"))
                    {
                        isPng = true;
                        area.isPNGMap = true;
                    }
                    if (pictureType.Contains("jpg"))
                    {
                        isJpg = true;
                        area.isJPGMap = true;
                    }

                    Bitmap testSize = new Bitmap(Path.GetFullPath(openFileDialog1.FileName));
                    if ((testSize.Width > 5000) || (testSize.Height > 5000))
                    {
                        MessageBox.Show("Map images must be no more than 5000x5000 pixels, i.e. 100x100 squares");
                        return;
                    }

                    //openLevel and refreshMap methods need area.ImageFileName != "none" to fire
                    //let us "none" and null here for starters to circumvent the reular load and draw of a background map en block
                    string filename = Path.GetFullPath(openFileDialog1.FileName);
                    //area.ImageFileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    //gameMapBitmap = new Bitmap(filename);
                    area.ImageFileName = "none";
                    //GDI gameMapBitmap = null;

                    //from here on we have hijacked the load button, we know the path and filename (together: filename)
                    //to our target map that is to be cut int 50x50 pieces
                    //these tiles will be uniquley named, numbered and stored in an own subfolder
                    //a new layer 0 will draw them like all the other tiles on screen
                    //if (gameMapBitmap == null)
                    //{
                    //MessageBox.Show("returned a null bitmap");
                    //}

                    //this is our source bitmap, ready for the cutting
                    //also name of the bitmap later ot be used for directory of its tiles as well as prefix in every of its tiles names (swamp0, swamp1, swamp2, etc.)


                    area.sourceBitmapName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    sourceBitmap = new Bitmap(filename);

                    //we need to:
                    //1. likely load it? Or is this only for the sharpdx
                    //2. Set up a loop running in increments of 50 through x and y, starting from zero up to x/y size of bitmap-1
                    //maybe just copy this from the normal tile draw routine 

                    //Rectangle srcBG = new Rectangle(0, 0, gameMapBitmap.Width, gameMapBitmap.Height);
                    //Rectangle dstBG = new Rectangle(area.backgroundImageStartLocX * sqr, area.backgroundImageStartLocY * sqr, sqr * (gameMapBitmap.Width / 50), sqr * (gameMapBitmap.Height / 50));
                    //device.DrawImage(gameMapBitmap, dstBG, srcBG, GraphicsUnit.Pixel);

                    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                    //trying to implemnt solution from codeproject

                    //xCells = (sourceBitmap.Width / 50).ToString();
                    //yCells = (sourceBitmap.Height / 50).ToString();

                    //args[0] = "50";
                    //args[1] = "50";

                    //ushort width = 50;
                    //ushort height = 50;

                    string dir = "";
                    try
                    {
                        dir = Path.GetDirectoryName(filename) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(filename);
                        if (!Directory.Exists(dir))
                        {
                            Directory.CreateDirectory(dir);
                        }
                        //else
                        //{
                        //Directory.
                        //}
                    }
                    catch
                    {
                        //Console.WriteLine("Error 6: Cannot create directory for cells.");
                        //return -6;
                    }
                    //Console.WriteLine();
                    //Console.WriteLine("About to split image in {0} rows x {1} columns = {2} cells...", rows, columns, cells);
                    //Console.WriteLine();
                    //int cellpadding = (cells - 1).ToString().Length;
                    //int rowpadding = (rows - 1).ToString().Length;
                    string cellfile;
                    string cellpath;
                    int squarePixSize = 50;
                    if (area.use100pixSquares)
                    {
                        squarePixSize = 100;
                    }
                    else
                    {
                        squarePixSize = 50;
                    }
                    int columns = sourceBitmap.Width / squarePixSize;
                    int rows = sourceBitmap.Height / squarePixSize;
                    int cells = columns * rows;
                    try
                    {
                        for (int row = 0; row < rows; row++)
                        {
                            //Console.Write("Row " + row.ToString().PadLeft(rowpadding, ' ') + ":  ");
                            for (int column = 0; column < columns; column++)
                            {
                                cellfile = area.sourceBitmapName + (row * columns + column).ToString();
                                if (isPng)
                                {
                                    cellpath = dir + Path.DirectorySeparatorChar + cellfile + ".png";
                                    sourceBitmap.Clone(new Rectangle(column * squarePixSize, row * squarePixSize, squarePixSize, squarePixSize), sourceBitmap.PixelFormat).Save(cellpath);
                                }

                                if (isJpg)
                                {
                                    cellpath = dir + Path.DirectorySeparatorChar + cellfile + ".jpg";
                                    sourceBitmap.Clone(new Rectangle(column * squarePixSize, row * squarePixSize, squarePixSize, squarePixSize), sourceBitmap.PixelFormat).Save(cellpath);
                                }



                                //foreach (string f in Directory.GetFiles(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\", "*.png"))
                                //{
                                //TileBitmapNamePair t = new TileBitmapNamePair((Bitmap)bit.Clone(), Path.GetFileNameWithoutExtension(f));
                                //tileList.Add(t);
                                //}

                                //foreach (string f in Directory.GetFiles(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\tiles\\", "*.png"))
                                //{
                                //TileBitmapNamePair t = new TileBitmapNamePair((Bitmap)bit.Clone(), Path.GetFileNameWithoutExtension(f));
                                //tileList.Add(t);
                                //}


                                //Console.Write(cellfile + "  ");
                            }
                            //Console.WriteLine();
                        }
                        //Console.WriteLine();
                        //Console.WriteLine("{0} files written to disk.", cells);
                        //Console.WriteLine();
                    }
                    catch (Exception ex)
                    {
                        //Console.WriteLine("Error 7: " + ex.Message);
                        //return -7;
                    }
                    //return cells;
                    //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
                    calledFromLoadButton = true;
                    //GDI refreshMap(true);
                }
                #endregion
            }
            else
            {
                #region old system
                if (mod.moduleName != "NewModule")
                {
                    openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics";
                }
                else
                {
                    openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\default\\NewModule";
                }
                openFileDialog1.FileName = String.Empty;
                openFileDialog1.Filter = "Map (*.jpg)|*.jpg";
                openFileDialog1.FilterIndex = 1;

                DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK) // Test result.
                {
                    Bitmap testSize = new Bitmap(Path.GetFullPath(openFileDialog1.FileName));
                    if ((testSize.Width > 800) || (testSize.Height > 800))
                    {
                        MessageBox.Show("Map images must be less than 800x800 pixels");
                        return;
                    }
                    string filename = Path.GetFullPath(openFileDialog1.FileName);
                    area.ImageFileName = Path.GetFileNameWithoutExtension(openFileDialog1.FileName);
                    gameMapBitmapD2D = GetFromBitmapList(area.ImageFileName);
                    //GDI gameMapBitmap = new Bitmap(filename);

                    //GDI if (gameMapBitmap == null)
                    //GDI {
                    //GDI     MessageBox.Show("returned a null bitmap");
                    //GDI }
                    //GDI refreshMap(true);
                }
                #endregion
            }
        }
        private void btnRemoveMap_Click(object sender, EventArgs e)
        {
            area.ImageFileName = "none";
            //GDI refreshMap(true);
        }
        private void numBGLocX_ValueChanged(object sender, EventArgs e)
        {
            area.backgroundImageStartLocX = (int)numBGLocX.Value;
        }
        private void numBGLocY_ValueChanged(object sender, EventArgs e)
        {
            area.backgroundImageStartLocY = (int)numBGLocY.Value;
        }
        private void WorldMapEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("closing editor and removing from openAreaList");
            prntForm.openAreasList.Remove(area);
        }
        private void WorldMapEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (useDirect2D)
            {
                DisposeAllD2D();
            }
            else
            {
                //device.Dispose();
                //surface.Dispose();
            }
            //gfxSelected.Dispose();
            //selectedBitmap.Dispose();
            //this.Close();
        }
        private void btnSelectedTerrain_Click(object sender, EventArgs e)
        {
            Button selectBtn = (Button)sender;
            currentTileFilename = selectBtn.Text;
            selectedBitmap = (Bitmap)selectBtn.BackgroundImage.Clone();
            panel1.BackgroundImage = selectedBitmap;
            rbtnPaintTile.Checked = true;
            prntForm.selectedLevelMapCreatureTag = "";
            prntForm.selectedLevelMapPropTag = "";
            prntForm.CreatureSelected = false;
            prntForm.PropSelected = false;
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
        private void rbtnPaintTile_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnPaintTile.Checked)
            {
                prntForm.logText("painting tiles" + Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                //pipetto
                currentTileFilename = "";
            }
        }
        private void rbtnChangeLinkState_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnChangeLinkState.Checked)
            {
                prntForm.logText("editing link to master state");
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

        private void rbtnToMaster_CheckedChanged(object sender, EventArgs e)
        {
            //patrick
            if (rbtnToMaster.Checked)
            {
                //patrick2

                //reset rotation counter
                transitionToMasterRotationCounter = 1;

                //create a new trigger object for current area (linked area)
                Trigger newTrigger = new Trigger();
                //increment the tag to something unique
                newTrigger.TriggerTag = "TransitionOn_" + area.Filename + "_" + prntForm.mod.nextIdNumber;
                prntForm.selectedLevelMapTriggerTag = newTrigger.TriggerTag;
                area.Triggers.Add(newTrigger);

                //create a new trigger object for master area
                Area areaOrg = new Area();
                
                bool orgIsStillOpen = false;
                    for (int a = 0; a < prntForm.openAreasList.Count; a++)
                    {
                        if (prntForm.openAreasList[a].Filename == area.masterOfThisArea)
                        {
                            areaOrg = prntForm.openAreasList[a];
                            orgIsStillOpen = true;
                            break;
                        }
                    }

                if (orgIsStillOpen)
                {

                    //get and load master area

                    //string g_dir = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas";
                    //if (area.masterOfThisArea != "none")
                    //{
                        //areaOrg = areaOrg.loadAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                    //}
                    Trigger newTriggerMaster = new Trigger();
                    //increment the tag to something unique
                    newTriggerMaster.TriggerTag = "TransitionOn_" + area.Filename + "_" + (prntForm.mod.nextIdNumber - 1) + "_" + area.masterOfThisArea;
                    //hopefully not need for teh paralley placed trigger on master
                    //prntForm.selectedLevelMapTriggerTag = newTrigger.TriggerTag;
                    areaOrg.Triggers.Add(newTriggerMaster);
                    //areaOrg.saveAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                }
                //set propertygrid to the new object
                //trying to disable the properties as they are not needed
                //prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = newTrigger;

                prntForm.logText("painting a new trigger");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                //refreshMap(true);
                //UpdatePB();

            }
        }

        private void rbtnHeightLevel_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnHeightLevel.Checked)
            {
                prntForm.logText("editing height level");
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
        /*
        private System.Windows.Forms.RadioButton rbtnHeightLevel;
        private System.Windows.Forms.RadioButton rbtnBridgeEW;
        private System.Windows.Forms.RadioButton rbtnBridgeNS;
        private System.Windows.Forms.RadioButton rbtnDownToN;
        private System.Windows.Forms.RadioButton rbtnDownToE;
        private System.Windows.Forms.RadioButton rbtnDownToS;
        private System.Windows.Forms.RadioButton rbtnDownToW;
        */

        private void rbtnBridgeEW_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnBridgeEW.Checked)
            {
                prntForm.logText("setting East-West bridge");
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
        /*
        private void rbtnBridgeNS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnBridgeNS.Checked)
            {
                prntForm.logText("setting North-South bridge");
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
        */

        private void rbtnDownToN_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDownToN.Checked)
            {
                prntForm.logText("setting downstairs to N");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.selectedLevelMapTriggerTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                stairRotationCounter = 1;
                //refreshMap(true);
                //UpdatePB();
            }
        }
        /*
        private void rbtnDownToS_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDownToS.Checked)
            {
                prntForm.logText("setting downstairs to S");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.selectedLevelMapTriggerTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                stairRotationCounter = 3;
                //refreshMap(true);
                //UpdatePB();
            }
        }
        **/

        /*
        private void rbtnDownToE_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDownToE.Checked)
            {
                prntForm.logText("setting downstairs to E");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.selectedLevelMapTriggerTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                stairRotationCounter = 2;
                //refreshMap(true);
                //UpdatePB();
            }
        }
        */

        /*
        private void rbtnDownToW_CheckedChanged(object sender, EventArgs e)
        {
            
            if (rbtnDownToW.Checked)
            {
                prntForm.logText("setting downstairs to W");
                prntForm.logText(Environment.NewLine);
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.selectedLevelMapTriggerTag = "";
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                stairRotationCounter = 4;
                //refreshMap(true);
                //UpdatePB();
            }
            
        }
        */
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
            foreach (Prop prp in area.Props)
            {
                if (prp.PropTag == lastSelectedObjectTag)
                {
                    // remove at index of matched tag
                    area.Props.RemoveAt(cnt);
                    //GDI propBitmapList.RemoveAt(cnt);
                    //GDI refreshMap(true);
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
                    //GDI refreshMap(true);
                    return;
                }
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
                //refreshMap(true);
                //UpdatePB();
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
                //refreshMap(true);
                //UpdatePB();
            }
        }
        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            //GDI refreshMap(true);
        }
        public void HandleContextMenuClick(object sender, EventArgs e)
        {
            //else, handler returns the selected tag
            ToolStripMenuItem menuItm = (ToolStripMenuItem)sender;
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

            int tileCounter = 0;
            foreach (Tile t in area.Tiles)
            {
                if (tileCounter.ToString() == menuItm.Text)
                {
                    int locationX = tileCounter % area.MapSizeX;
                    int locationY = tileCounter / area.MapSizeX;
                    txtSelectedIconInfo.Text = "Tile (x" + locationX.ToString() + " / y" + locationY.ToString() + ")" + Environment.NewLine;
                    lastSelectedObjectTag = tileCounter.ToString();
                    prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = t;
                    return;
                }
                tileCounter++;
            }
            }
        private void rbtnZoom1x_CheckedChanged(object sender, EventArgs e)
        {
            sqr = 50;
            resetPanelAndDeviceSize();
            //GDI refreshMap(true);
        }
        private void rbtnZoom2x_CheckedChanged(object sender, EventArgs e)
        {
            sqr = 25;
            resetPanelAndDeviceSize();
            //GDI refreshMap(true);
        }
        private void rbtnZoom5x_CheckedChanged(object sender, EventArgs e)
        {
            sqr = 10;
            resetPanelAndDeviceSize();
            //GDI refreshMap(true);
        }
        private void btnPlusLeftX_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            int oldX = area.MapSizeX;
            for (int i = area.Tiles.Count - oldX; i >= 0; i -= oldX)
            {
                Tile newTile = new Tile();
                area.Tiles.Insert(i, newTile);
            }
            foreach (Prop prpRef in area.Props)
            {
                prpRef.LocationX++;
            }
            foreach (Trigger t in area.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    p.X++;
                }
            }
            area.MapSizeX++;
            mapSizeChangeStuff();
        }
        private void btnMinusLeftX_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            int oldX = area.MapSizeX;
            for (int i = area.Tiles.Count - oldX; i >= 0; i -= oldX)
            {
                area.Tiles.RemoveAt(i);
            }
            foreach (Prop prpRef in area.Props)
            {
                prpRef.LocationX--;
            }
            foreach (Trigger t in area.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    p.X--;
                }
            }
            area.MapSizeX--;
            mapSizeChangeStuff();
        }
        private void btnPlusRightX_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            int oldX = area.MapSizeX;
            for (int i = area.Tiles.Count - 1; i >= 0; i -= oldX)
            {
                Tile newTile = new Tile();
                area.Tiles.Insert(i + 1, newTile);
            }
            area.MapSizeX++;
            mapSizeChangeStuff();
        }
        private void btnMinusRightX_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            int oldX = area.MapSizeX;
            for (int i = area.Tiles.Count - 1; i >= 0; i -= oldX)
            {
                area.Tiles.RemoveAt(i);
            }
            area.MapSizeX--;
            mapSizeChangeStuff();
        }
        private void btnPlusTopY_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            for (int i = 0; i < area.MapSizeX; i++)
            {
                Tile newTile = new Tile();
                area.Tiles.Insert(0, newTile);
            }
            foreach (Prop prpRef in area.Props)
            {
                prpRef.LocationY++;
            }
            foreach (Trigger t in area.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    p.Y++;
                }
            }
            area.MapSizeY++;
            mapSizeChangeStuff();
        }
        private void btnMinusTopY_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            for (int i = 0; i < area.MapSizeX; i++)
            {
                area.Tiles.RemoveAt(0);
            }
            foreach (Prop prpRef in area.Props)
            {
                prpRef.LocationY--;
            }
            foreach (Trigger t in area.Triggers)
            {
                foreach (Coordinate p in t.TriggerSquaresList)
                {
                    p.Y--;
                }
            }
            area.MapSizeY--;
            mapSizeChangeStuff();
        }
        private void btnPlusBottumY_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            for (int i = 0; i < area.MapSizeX; i++)
            {
                Tile newTile = new Tile();
                area.Tiles.Add(newTile);
            }
            area.MapSizeY++;
            mapSizeChangeStuff();
        }
        private void btnMinusBottumY_Click(object sender, EventArgs e)
        {
            //y * area.MapSizeX + x
            for (int i = 0; i < area.MapSizeX; i++)
            {
                area.Tiles.RemoveAt(area.Tiles.Count - 1);
            }
            area.MapSizeY--;
            mapSizeChangeStuff();
        }
        private void btnProperties_Click(object sender, EventArgs e)
        {
            prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = area;
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
                //GDI refreshMap(true);
                //GDI UpdatePB();
                rbtnInfo.Checked = true;
                resetTileToBePlacedSettings();
            }
            else if (e.KeyCode == Keys.R)
            {
                if (rbtnPaintTile.Checked)
                {
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        tileToBePlaced.angle -= 1;
                    }
                    else
                    {
                        tileToBePlaced.angle -= 90;
                    }
                    if (tileToBePlaced.angle > 360)
                    {
                        tileToBePlaced.angle -= 360;
                    }
                    if (tileToBePlaced.angle < 0)
                    {
                        tileToBePlaced.angle += 360;
                    }
                }
            }
            else if (e.KeyCode == Keys.T)
            {
                if (rbtnPaintTile.Checked)
                {
                    if (Control.ModifierKeys == Keys.Shift)
                    {
                        tileToBePlaced.angle += 1;
                    }
                    else
                    {
                        tileToBePlaced.angle += 90;
                    }
                    if (tileToBePlaced.angle > 360)
                    {
                        tileToBePlaced.angle -= 360;
                    }
                    if (tileToBePlaced.angle < 0)
                    {
                        tileToBePlaced.angle += 360;
                    }
                }
            }
            else if (e.KeyCode == Keys.Q)
            {
                if (rbtnPaintTile.Checked)
                {
                    tileToBePlaced.xscale--;
                    tileToBePlaced.yscale--;
                }
            }
            else if (e.KeyCode == Keys.E)
            {
                if (rbtnPaintTile.Checked)
                {
                    tileToBePlaced.xscale++;
                    tileToBePlaced.yscale++;
                }
            }
            else if (e.KeyCode == Keys.W)
            {
                if (rbtnPaintTile.Checked)
                {
                    tileToBePlaced.yshift--;
                }
            }
            else if (e.KeyCode == Keys.S)
            {
                if (rbtnPaintTile.Checked)
                {
                    tileToBePlaced.yshift++;
                }
            }
            else if (e.KeyCode == Keys.A)
            {
                if (rbtnPaintTile.Checked)
                {
                    tileToBePlaced.xshift--;
                }
            }
            else if (e.KeyCode == Keys.D)
            {
                if (rbtnPaintTile.Checked)
                {
                    tileToBePlaced.xshift++;
                }
            }
            else if (e.KeyCode == Keys.F)
            {
                if (rbtnPaintTile.Checked)
                {
                    if (tileToBePlaced.opacity > 0)
                    {
                        tileToBePlaced.opacity = tileToBePlaced.opacity - 0.01f;
                    }
                }
            }
            else if (e.KeyCode == Keys.G)
            {
                if (rbtnPaintTile.Checked)
                {
                    if (tileToBePlaced.opacity < 1)
                    {
                        tileToBePlaced.opacity = tileToBePlaced.opacity + 0.01f;
                    }
                }
            }
            else if (e.KeyCode == Keys.M)
            {
                if (rbtnPaintTile.Checked)
                {
                    tileToBePlaced.mirror = !tileToBePlaced.mirror;
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                int cnt = 0;
                foreach (Prop prp in area.Props)
                {
                    if (prp.PropTag == lastSelectedObjectTag)
                    {
                        // remove at index of matched tag
                        area.Props.RemoveAt(cnt);
                        //GDI propBitmapList.RemoveAt(cnt);
                        //GDI refreshMap(true);
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
                        //GDI refreshMap(true);
                        return;
                    }
                }
            }
            panelView.Focus();
        }
        #endregion

        private void timerRenderLoop_Tick(object sender, EventArgs e)
        {
            Render();
        }              
    }
}
