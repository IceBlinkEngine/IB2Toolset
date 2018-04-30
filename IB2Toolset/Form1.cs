/* IBB Toolset by Jeremy Smith, copyright 2014 */

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
using Newtonsoft.Json;

namespace IB2Toolset
{
    public partial class ParentForm : Form
    {
        //added comment here to push a change
        public string _mainDirectory;
        public Module mod = new Module();
        public List<JournalQuest> journal = new List<JournalQuest>();
        public List<Creature> creaturesList = new List<Creature>();
        public List<Container> containersList = new List<Container>();
        public List<Shop> shopsList = new List<Shop>();
        public List<Encounter> encountersList = new List<Encounter>();
        public List<Item> itemsList = new List<Item>();
        public List<Prop> propsList = new List<Prop>();
        public List<PlayerClass> playerClassesList = new List<PlayerClass>();
        public List<Race> racesList = new List<Race>();
        public List<Spell> spellsList = new List<Spell>();
        public List<Trait> traitsList = new List<Trait>();
        public List<Faction> factionsList = new List<Faction>();
        public List<WeatherEffect> weatherEffectsList = new List<WeatherEffect>();
        public List<Weather> weathersList = new List<Weather>();
        public List<Effect> effectsList = new List<Effect>();
        public List<string> itemsParentNodeList = new List<string>();
        public List<string> creaturesParentNodeList = new List<string>();
        public List<string> propsParentNodeList = new List<string>();
        public List<Area> openAreasList = new List<Area>();
        public List<Convo> openConvosList = new List<Convo>();
        public List<string> scriptList = new List<string>();
        public List<Condition> copiedConditionalsList = new List<Condition>();
        public List<Action> copiedActionsList = new List<Action>();
        public List<string> tilePrefixFilterList = new List<string>();
        public string selectedEncounterCreatureTag = "";
        public string selectedEncounterPropTag = "";
        public string selectedEncounterTriggerTag = "";
        public string selectedLevelMapCreatureTag = "";
        public string selectedLevelMapPropTag = "";
        public string selectedLevelMapTriggerTag = "";
        public bool CreatureSelected = false;
        public bool PropSelected = false;
        public int nodeCount = 1;
        public int createdTab = 0;
        public int _selectedLbxAreaIndex;
        public int _selectedLbxConvoIndex;
        public int _selectedLbxLogicTreeIndex;
        public int _selectedLbxIBScriptIndex;
        public int _selectedLbxContainerIndex;
        public int _selectedLbxEncounterIndex;
        public string lastSelectedCreatureNodeName = "";
        public string lastSelectedItemNodeName = "";
        public string lastSelectedPropNodeName = "";
        public Trigger currentSelectedTrigger = null;
        public Bitmap iconBitmap;
        public string lastModuleFullPath;
        public string versionMessage = "IceBlink 2 RPG Toolset for creating adventure modules for the PC and Android.\r\n\r\n IceBlink 2 RPG Toolset ver 1.00";
        
        private DeserializeDockContent m_deserializeDockContent;
        public IceBlinkProperties frmIceBlinkProperties;
        public IconSprite frmIconSprite;
        public TriggerEventsForm frmTriggerEvents;
        public Blueprints frmBlueprints;
        public AreaForm frmAreas;
        public ConversationsForm frmConversations;
        public IBScriptForm frmIBScript;
        public EncountersForm frmEncounters;
        public ContainersForm frmContainers;
        public LogForm frmLog;
        public bool m_bSaveLayout = true;


        public ParentForm()
        {
            InitializeComponent();
            _mainDirectory = Directory.GetCurrentDirectory();
            dockPanel1.Dock = DockStyle.Fill;
            dockPanel1.BackColor = Color.Beige;
            dockPanel1.BringToFront();
            frmIceBlinkProperties = new IceBlinkProperties(this);
            frmIconSprite = new IconSprite(this);
            frmTriggerEvents = new TriggerEventsForm(this);
            frmBlueprints = new Blueprints(this);
            frmAreas = new AreaForm(this);
            frmConversations = new ConversationsForm(this);
            //REMOVEfrmLogicTree = new LogicTreeForm(this);
            frmIBScript = new IBScriptForm(this);
            frmEncounters = new EncountersForm(this);
            frmContainers = new ContainersForm(this);
            frmLog = new LogForm(this);
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }        
        private void ParentForm_Load(object sender, EventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            string configDefaultFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DefaultLayout.config");

            if (File.Exists(configFile))
            {
                dockPanel1.LoadFromXml(configFile, m_deserializeDockContent);
            }
            else if (File.Exists(configDefaultFile))
            {
                dockPanel1.LoadFromXml(configDefaultFile, m_deserializeDockContent);
            }
            else
            {
                //do nothing
            }

            //game = new Game();
            //game.mainDirectory = Directory.GetCurrentDirectory();
            openModule(_mainDirectory + "\\default\\NewModule\\NewModule.mod");
            openCreatures(_mainDirectory + "\\default\\NewModule\\data\\creatures.json");
            openItems(_mainDirectory + "\\default\\NewModule\\data\\items.json");
            openProps(_mainDirectory + "\\default\\NewModule\\data\\props.json");
            openJournal(_mainDirectory + "\\default\\NewModule\\data\\journal.json");
            openPlayerClasses(_mainDirectory + "\\default\\NewModule\\data\\playerClasses.json");
            openRaces(_mainDirectory + "\\default\\NewModule\\data\\races.json");
            //openSkills(_mainDirectory + "\\data\\NewModule\\data\\" + mod.SkillsFileName);
            openSpellsDefault();
            openTraitsDefault();
            openFactionsDefault();
            openWeatherEffects(_mainDirectory + "\\default\\NewModule\\data\\weatherEffects.json");
            openWeathers(_mainDirectory + "\\default\\NewModule\\data\\weathers.json");
            openEffectsDefault();
            //game.errorLog("Starting IceBlink Toolset");
            saveAsTemp();

            //hope this fits for IB2, too
            Text = "IceBlink 2 Toolset - " + mod.moduleLabelName;
            createTilePrefixFilterList();

            //fill all lists
            DropdownStringLists.aiTypeStringList = new List<string> { "BasicAttacker", "GeneralCaster","bloodHunter","mindHunter","softTargetHunter"};
            DropdownStringLists.damageTypeStringList = new List<string> { "Normal", "Acid", "Cold", "Electricity", "Fire", "Magic", "Poison" };
            DropdownStringLists.itemTypeStringList = new List<string> { "Head", "Neck", "Armor", "Ranged", "Melee", "General", "Ring", "Shield", "Feet", "Ammo", "Gloves" };
            DropdownStringLists.useableWhenStringList = new List<string> { "InCombat", "OutOfCombat", "Always", "Passive" };
            DropdownStringLists.weaponTypeStringList = new List<string> { "Ranged", "Melee" };
            DropdownStringLists.moverTypeStringList = new List<string> { "post", "random", "patrol", "daily", "weekly", "monthly", "yearly"};


            this.WindowState = FormWindowState.Maximized;
        }
        private void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //close all open tab documents first
            CloseAllDocuments();
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            if (m_bSaveLayout)
                dockPanel1.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);
        }

        //******************************************************************
          
         private void tsBtnChangePrefix_Click(object sender, EventArgs e)
         {  
             readPrefixChangeFile();
            MessageBox.Show("Finished swapping names.");
        }  
   
         public void readPrefixChangeFile()
         {  
             string[] lines = File.ReadAllLines(_mainDirectory + "\\prefix_change.txt");  
             foreach (string line in lines)  
             {  
                 if (line.Trim(' ').StartsWith("//"))  
                 {  
                     continue;  
                 }  
                 string[] name = line.Split(',');  
                 if (name.Length > 1)  
                 {  
                     switchGraphicsFilenames(name[0].Trim(' '), name[1].Trim(' '));  
                 }                  
             }  
        }

        public void switchGraphicsFilenames(string oldname, string newname)
         {  
             //iterate through each line and get old name and new name  
             foreach (Creature crt in mod.moduleCreaturesList)  
             {  
                 if (crt.cr_projSpriteFilename.Equals(oldname)) { crt.cr_projSpriteFilename = newname; }  
                 if (crt.cr_spriteEndingFilename.Equals(oldname)) { crt.cr_spriteEndingFilename = newname; }  
                 if (crt.cr_tokenFilename.Equals(oldname)) { crt.cr_tokenFilename = newname; }  
             }  
             foreach (Item it in mod.moduleItemsList)  
             {  
                 if (it.projectileSpriteFilename.Equals(oldname)) { it.projectileSpriteFilename = newname; }  
                 if (it.spriteEndingFilename.Equals(oldname)) { it.spriteEndingFilename = newname; }  
                 if (it.itemImage.Equals(oldname)) { it.itemImage = newname; }  
             }

            //add prop images to the graphics needed list  
            foreach (Area ar in mod.moduleAreasObjects)
            {
                foreach (Prop prp in ar.Props)
                {
                    if (prp.ImageFileName.Equals(oldname)) { prp.ImageFileName = newname; }
                }
            }

            //tiles world map
            foreach (Area ar in mod.moduleAreasObjects)
            {
                foreach (Tile t in ar.Tiles)
                {
                    if (t.Layer1Filename.Equals(oldname))
                    {
                        t.Layer1Filename = newname;
                    }
                    if (t.Layer2Filename.Equals(oldname))
                    {
                        t.Layer2Filename = newname;
                    }
                    if (t.Layer3Filename.Equals(oldname))
                    {
                        t.Layer3Filename = newname;
                    }
                    if (t.Layer4Filename.Equals(oldname))
                    {
                        t.Layer4Filename = newname;
                    }
                    if (t.Layer5Filename.Equals(oldname))
                    {
                        t.Layer5Filename = newname;
                    }
                }
            }
                        
             foreach (Encounter enc in mod.moduleEncountersList)  
             {
                foreach (TileEnc enct in enc.encounterTiles)
                {
                    if (enct.Layer1Filename.Equals(oldname))
                    {
                        enct.Layer1Filename = newname;
                    }
                    if (enct.Layer2Filename.Equals(oldname))
                    {
                        enct.Layer2Filename = newname;
                    }
                    if (enct.Layer3Filename.Equals(oldname))
                    {
                        enct.Layer3Filename = newname;
                    }
                }
                /*
                for (int i = 0; i<enc.Layer1Filename.Count; i++)  
                 {  
                     if (enc.Layer1Filename[i].Equals(oldname)) { enc.Layer1Filename[i] = newname; }  
                 }  
                 for (int i = 0; i<enc.Layer2Filename.Count; i++)  
                 {  
                     if (enc.Layer2Filename[i].Equals(oldname)) { enc.Layer2Filename[i] = newname; }  
                 }  
                 for (int i = 0; i<enc.Layer3Filename.Count; i++)  
                 {  
                     if (enc.Layer3Filename[i].Equals(oldname)) { enc.Layer3Filename[i] = newname; }  
                 }  
                 foreach (Prop prp in enc.propsList)  
                 {  
                     if (prp.ImageFileName.Equals(oldname)) { prp.ImageFileName = newname; }  
                 }
                 */
            }  

             //skipping adjustng convo graphics for now a sthe cnovo are not listed (?)
             /*
             foreach (String convoname in mod.conv)  
             {
                //getCreature("a");
                if (cnv.NpcPortraitBitmap.Equals(oldname)) { cnv.NpcPortraitBitmap = newname; }  
                 foreach (ContentNode subNode in cnv.subNodes)  
                 {  
                     switchAllNodePortraits(subNode, oldname, newname);  
                     if (subNode.NodePortraitBitmap.Equals(oldname)) { subNode.NodePortraitBitmap = newname; }  
                 }  
             } 
             */ 
             //go through convos, items, area tiles, encounter tiles,   
         }
          
        //not used so far in IB2(?)
         public ContentNode switchAllNodePortraits(ContentNode node, string oldname, string newname)
         {  
             ContentNode tempNode = null;  
             if (node != null)  
             {  
                 if (node.NodePortraitBitmap.Equals(oldname)) { node.NodePortraitBitmap = newname; }  
             }  
             foreach (ContentNode subNode in node.subNodes)  
             {  
                 tempNode = switchAllNodePortraits(subNode, oldname, newname);  
                 if (tempNode != null)  
                 {  
                     return tempNode;  
                 }  
             }  
             return tempNode;  
         }  

        //********************************************************************
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(IceBlinkProperties).ToString())
                return frmIceBlinkProperties;
            else if (persistString == typeof(IconSprite).ToString())
                return frmIconSprite;
            else if (persistString == typeof(Blueprints).ToString())
                return frmBlueprints;   
            else if (persistString == typeof(AreaForm).ToString())
                return frmAreas;
            else if (persistString == typeof(TriggerEventsForm).ToString())
                return frmTriggerEvents;
            else if (persistString == typeof(ConversationsForm).ToString())
                return frmConversations;
            //REMOVEelse if (persistString == typeof(LogicTreeForm).ToString())
            //REMOVE    return frmLogicTree;
            else if (persistString == typeof(IBScriptForm).ToString())
                return frmIBScript;
            else if (persistString == typeof(EncountersForm).ToString())
                return frmEncounters;
            else if (persistString == typeof(ContainersForm).ToString())
                return frmContainers;
            else //(persistString == typeof(LogForm).ToString())
                return frmLog;
        }
        private void CloseAllDocuments()
        {            
            for (int index = dockPanel1.Contents.Count - 1; index >= 0; index--)
            {
                if (dockPanel1.Contents[index] is IDockContent)
                {                    
                    IDockContent content = (IDockContent)dockPanel1.Contents[index];
                    if ((content.DockHandler.TabText == "Areas") ||
                        (content.DockHandler.TabText == "Conversations") ||
                        (content.DockHandler.TabText == "Containers") ||
                        (content.DockHandler.TabText == "Encounters") ||
                        (content.DockHandler.TabText == "TriggerEvents") ||
                        (content.DockHandler.TabText == "IBScripts") ||
                        (content.DockHandler.TabText == "LogForm") ||
                        (content.DockHandler.TabText == "Blueprints") ||
                        (content.DockHandler.TabText == "Properties") ||
                        (content.DockHandler.TabText == "IconSprite"))
                    {
                        //skip these, do not close them
                    }
                    else
                    {
                        content.DockHandler.Close();
                    }
                }
            }
        }
        private void loadAllDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");            
        }

        public void errorLog(string text)
        {
            if (_mainDirectory == null)
            {
                _mainDirectory = Directory.GetCurrentDirectory();
            }
            using (StreamWriter writer = new StreamWriter(_mainDirectory + "//IB2ToolsetErrorLog.txt", true))
            {
                writer.Write(DateTime.Now + ": ");
                writer.WriteLine(text);
            }
        }

        #region File Handling
        private void openModule(string filename)
        {            
            mod = mod.loadModuleFile(filename);
            if (mod == null)
            {
                MessageBox.Show("returned a null module");
            }
            frmAreas.lbxAreas.DataSource = null;
            frmAreas.lbxAreas.DataSource = mod.moduleAreasList;
            frmAreas.refreshListBoxAreas();            
            frmConversations.refreshListBoxConvos();
            //REMOVEfrmLogicTree.refreshListBoxLogicTrees();
            frmIBScript.refreshListBoxIBScripts();
        }
        private void openCreatures(string filename)
        {
            if (File.Exists(filename))
            {
                creaturesList.Clear();
                creaturesList = loadCreaturesFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find creatures.json file. Will create a new one upon saving module.");
            }
            frmBlueprints.UpdateTreeViewCreatures();
            loadCreatureSprites();
        }
        private void loadCreatureSprites()
        {
            foreach (Creature crt in creaturesList)
            {
                crt.LoadCreatureBitmap(this);                
            }     
        }
        private void openItems(string filename)
        {
            if (File.Exists(filename))
            {
                itemsList.Clear();
                itemsList = loadItemsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find items.json file. Will create a new one upon saving module.");
            }
            frmBlueprints.UpdateTreeViewItems();
            loadItemSprites();
        }
        private void loadItemSprites()
        {
            foreach (Item it in itemsList)
            {
                it.LoadItemBitmap(this);
            }
        }
        private void openProps(string filename)
        {
            if (File.Exists(filename))
            {
                propsList.Clear();
                propsList = loadPropsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find props.json file. Will create a new one upon saving module.");
            }
            frmBlueprints.UpdateTreeViewProps();
            loadPropSprites();
        }
        private void loadPropSprites()
        {
            foreach (Prop prp in propsList)
            {
                prp.LoadPropBitmap(this);
            }
        }
        private void openShops(string filename)
        {
            if (File.Exists(filename))
            {
                shopsList.Clear();
                shopsList = loadShopsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find shops.json file. Will create a new one upon saving module.");
            }
        }
        private void openContainers(string filename)
        {
            if (File.Exists(filename))
            {
                containersList.Clear();
                containersList = loadContainersFile(filename);                
            }
            else
            {
                MessageBox.Show("Couldn't find containers.json file. Will create a new one upon saving module.");
            }
            frmContainers.refreshListBoxContainers();
        }
        private void openEncounters(string filename)
        {            
            if (File.Exists(filename))
            {
                encountersList.Clear();
                encountersList = loadEncountersFile(filename);                
            }
            else
            {
                MessageBox.Show("Couldn't find encounters.json file. Will create a new one upon saving module.");
            }
            frmEncounters.refreshListBoxEncounters();
        }
        private void openJournal(string filename)
        {
            if (File.Exists(filename))
            {
                journal.Clear();
                journal = loadJournalFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find journal.json file. Will create a new one upon saving module.");
            }
        }

        private void openEffects(string filename)
        {
            if (File.Exists(filename))
            {
                effectsList.Clear();
                effectsList = loadEffectsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find effects.json file. Will create a new one upon saving module.");
            }

            List<Effect> effectsListDefault = new List<Effect>();
            effectsListDefault = loadEffectsFile(_mainDirectory + "\\default\\NewModule\\data\\effects.json");

            foreach (Effect eD in effectsListDefault)
            {
                bool allowAdding = true;
                foreach (Effect eM in effectsList)
                {
                    if (eM.name == eD.name)
                    {
                        allowAdding = false;
                        break;
                    }
                }

                if (allowAdding)
                {
                    effectsList.Add(eD);
                }
            }
            effectsListDefault.Clear();
        }

        private void openEffectsDefault()
        {
            string filename = _mainDirectory + "\\default\\NewModule\\data\\effects.json";

            if (File.Exists(filename))
            {
                effectsList.Clear();
                effectsList = loadEffectsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find effects.json file. Will create a new one upon saving module.");
            }
        }
        private void openPlayerClasses(string filename)
        {
            if (File.Exists(filename))
            {
                playerClassesList.Clear();
                playerClassesList = loadPlayerClassesFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find playerClasses.json file. Will create a new one upon saving module.");
            }
        }
        private void openRaces(string filename)
        {
            if (File.Exists(filename))
            {
                racesList.Clear();
                racesList = loadRacesFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find races.json file. Will create a new one upon saving module.");
            }
        }
        private void openSpells(string filename)
        {
            if (File.Exists(filename))
            {
                spellsList.Clear();
                spellsList = loadSpellsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find spells.json file. Will create a new one upon saving module.");
            }

            List<Spell> spellsListDefault = new List<Spell>();
            spellsListDefault = loadSpellsFile(_mainDirectory + "\\default\\NewModule\\data\\spells.json");
            
            foreach (Spell sD in spellsListDefault)
            {
                bool allowAdding = true;
                foreach (Spell sM in spellsList)
                {
                    if (sM.name == sD.name)
                    {
                        allowAdding = false;
                        break;
                    }
                }

                if (allowAdding)
                {
                    spellsList.Add(sD);
                }
            }

            spellsListDefault.Clear();            
        }
        private void openSpellsDefault()
        {
            string filename = _mainDirectory + "\\default\\NewModule\\data\\spells.json";
            if (File.Exists(filename))
            {
                spellsList.Clear();
                spellsList = loadSpellsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find spells.json file. Will create a new one upon saving module.");
            }
        }
        private void openTraits(string filename)
        {
            if (File.Exists(filename))
            {
                traitsList.Clear();
                traitsList = loadTraitsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find traits.json file. Will create a new one upon saving module.");
            }

            List<Trait> TraitsListDefault = new List<Trait>();
            TraitsListDefault = loadTraitsFile(_mainDirectory + "\\default\\NewModule\\data\\Traits.json");

            foreach (Trait tD in TraitsListDefault)
            {
                bool allowAdding = true;
                foreach (Trait tM in traitsList)
                {
                    if (tM.name == tD.name)
                    {
                        allowAdding = false;
                        break;
                    }
                }

                if (allowAdding)
                {
                    traitsList.Add(tD);
                }
            }

            TraitsListDefault.Clear();
        }

        private void openFactions(string filename)
        {
            if (File.Exists(filename))
            {
                factionsList.Clear();
                factionsList = loadFactionsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find factions.json file. Will create a new one upon saving module.");
            }

            List<Faction> FactionsListDefault = new List<Faction>();
            FactionsListDefault = loadFactionsFile(_mainDirectory + "\\default\\NewModule\\data\\Factions.json");

            foreach (Faction tD in FactionsListDefault)
            {
                bool allowAdding = true;
                foreach (Faction tM in factionsList)
                {
                    if (tM.name == tD.name)
                    {
                        allowAdding = false;
                        break;
                    }
                }

                if (allowAdding)
                {
                    factionsList.Add(tD);
                }
            }

            FactionsListDefault.Clear();
        }

        private void openTraitsDefault()
        {
            string filename = _mainDirectory + "\\default\\NewModule\\data\\traits.json";

            if (File.Exists(filename))
            {
                traitsList.Clear();
                traitsList = loadTraitsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find traits.json file. Will create a new one upon saving module.");
            }
        }

        private void openFactionsDefault()
        {
            string filename = _mainDirectory + "\\default\\NewModule\\data\\factions.json";

            if (File.Exists(filename))
            {
                factionsList.Clear();
                factionsList = loadFactionsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find factions.json file. Will create a new one upon saving module.");
            }
        }

        private void openWeatherEffects(string filename)
        {
            if (File.Exists(filename))
            {
                weatherEffectsList.Clear();
                weatherEffectsList = loadWeatherEffectsFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find weatherEffects.json file. Will create a new one upon saving module.");
            }
        }

        private void openWeathers(string filename)
        {
            if (File.Exists(filename))
            {
                weathersList.Clear();
                weathersList = loadWeathersFile(filename);
            }
            else
            {
                MessageBox.Show("Couldn't find weathers.json file. Will create a new one upon saving module.");
            }
        }

        private void openFiles()
        {
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory + "\\modules";
            //Empty the FileName text box of the dialog
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "Module files (*.mod)|*.mod|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string filename = Path.GetFullPath(openFileDialog1.FileName);
                string directory = Path.GetDirectoryName(openFileDialog1.FileName);
                openModule(filename);
                foreach (Item it in mod.moduleItemsList)
                {
                     if (it.attackRange == 0)
                     {
                         it.attackRange = 1;
                     }
                }

                openCreatures(directory + "\\data\\creatures.json");
                openItems(directory + "\\data\\items.json");
                openContainers(directory + "\\data\\containers.json");
                openShops(directory + "\\data\\shops.json");
                openEncounters(directory + "\\data\\encounters.json");
                openProps(directory + "\\data\\props.json");
                openJournal(directory + "\\data\\journal.json");
                openPlayerClasses(directory + "\\data\\playerClasses.json");
                openRaces(directory + "\\data\\races.json");
                openSpells(directory + "\\data\\spells.json");
                openTraits(directory + "\\data\\traits.json");
                openFactions(directory + "\\data\\factions.json");
                openWeatherEffects(directory + "\\data\\weatherEffects.json");
                openWeathers(directory + "\\data\\weathers.json");
                openEffects(directory + "\\data\\effects.json");
                refreshDropDownLists();
                this.Text = "IceBlink 2 Toolset - " + mod.moduleLabelName;
                createTilePrefixFilterList();
            }
        }

        public void createTilePrefixFilterList()
         {
            //t_f_ for floors, t_n_ for nature, t_m_ for manmade and t_w_ for walls
        
            tilePrefixFilterList.Clear();
            //tilePrefixFilterList.Add("t_");
            if (mod.usePredefinedTileCategories)
            {
                tilePrefixFilterList.Add("All");
                tilePrefixFilterList.Add("OnMap");
                tilePrefixFilterList.Add("t_m_");
                tilePrefixFilterList.Add("t_n_");
                tilePrefixFilterList.Add("t_f_");
                tilePrefixFilterList.Add("t_w_");
                tilePrefixFilterList.Add("Rest");
            }
            else if (!mod.usePredefinedTileCategories)
            {
                tilePrefixFilterList.Add("t_");
                try  
                 {

                     foreach (string f in Directory.GetFiles(_mainDirectory + "\\default\\NewModule\\tiles\\", "*.png"))  
                     {  
                         if (!Path.GetFileName(f).StartsWith("t_"))  
                         {  
                             continue;  
                         }  
                         string filename = Path.GetFileNameWithoutExtension(f);  
                         string[] split = filename.Split('_');  
                         if (split.Length > 2)  
                         {  
                             string s = "t_" + split[1] + "_";
                            if (!tilePrefixFilterList.Contains(s))  
                             {  
                                 tilePrefixFilterList.Add(s);  
                             }  
                         }  
                     }

                    foreach (string f in Directory.GetFiles(_mainDirectory + "\\modules\\" + mod.moduleName +"\\tiles\\", "*.png"))
                    {
                        if (!Path.GetFileName(f).StartsWith("t_"))
                        {
                            continue;
                        }
                        string filename = Path.GetFileNameWithoutExtension(f);
                        string[] split = filename.Split('_');
                        if (split.Length > 2)
                        {
                            string s = "t_" + split[1] + "_";
                            if (!tilePrefixFilterList.Contains(s))
                            {
                                tilePrefixFilterList.Add(s);
                            }
                        }
                    }

                }  
                 catch (Exception ex)  
                 {  
                     MessageBox.Show("error: " + ex.ToString());
                } 
            }
        }

        public void refreshDropDownLists()
        {
            fillScriptList();
            loadSpriteDropdownList();
            loadSoundDropdownList();
            loadScriptDropdownList();
            loadIBScriptDropdownList();
            loadMusicDropdownList();
            loadConversationDropdownList();
            loadEncounterDropdownList();
            loadPlayerClassesTagsList();
            loadRacesTagsList();
            loadSpellTagsList();
            loadTraitTagsList();
            loadEffectTagsList();
            loadWeatherEffectsTagsList();
            loadWeatherEffectsNamesList();
        }

        public void loadScriptDropdownList()
        {  
            DropdownStringLists.scriptStringList = new List<string>();  
            DropdownStringLists.scriptStringList.Add("none");  
            string jobDir = "";
            jobDir = this._mainDirectory + "\\default\\NewModule\\scripts";  
            foreach (string f in Directory.GetFiles(jobDir, "*.cs"))  
            {  
                string filename = Path.GetFileName(f);  
                DropdownStringLists.scriptStringList.Add(filename);  
            }  
         }

public void loadSpriteDropdownList()
        {
            DropdownStringLists.spriteStringList = new List<string>();
            DropdownStringLists.spriteStringList.Add("none");
            string jobDir = "";
            jobDir = this._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics";
            foreach (string f in Directory.GetFiles(jobDir, "fx_*.png"))
            {
                string filename = Path.GetFileNameWithoutExtension(f);
                DropdownStringLists.spriteStringList.Add(filename);
            }            
        }
        public void loadSoundDropdownList()
        {
            DropdownStringLists.soundStringList = new List<string>();
            DropdownStringLists.soundStringList.Add("none");
            string jobDir = "";
            jobDir = this._mainDirectory + "\\modules\\" + mod.moduleName + "\\sounds";
            foreach (string f in Directory.GetFiles(jobDir, "*.*", SearchOption.AllDirectories))
            {
                string filename = Path.GetFileNameWithoutExtension(f);
                DropdownStringLists.soundStringList.Add(filename);
            }            
        }
        public void loadLogicTreeDropdownList()
        {
            DropdownStringLists.logicTreeStringList = new List<string>();
            DropdownStringLists.logicTreeStringList.Add("none");
            string jobDir = "";
            jobDir = this._mainDirectory + "\\modules\\" + mod.moduleName + "\\logictree";
            foreach (string f in Directory.GetFiles(jobDir, "*.*", SearchOption.AllDirectories))
            {
                string filename = Path.GetFileNameWithoutExtension(f);
                DropdownStringLists.logicTreeStringList.Add(filename);
            }
        }
        public void loadIBScriptDropdownList()
        {
            DropdownStringLists.ibScriptStringList = new List<string>();
            DropdownStringLists.ibScriptStringList.Add("none");
            string jobDir = "";
            jobDir = this._mainDirectory + "\\modules\\" + mod.moduleName + "\\ibscript";
            foreach (string f in Directory.GetFiles(jobDir, "*.*", SearchOption.AllDirectories))
            {
                string filename = Path.GetFileNameWithoutExtension(f);
                DropdownStringLists.ibScriptStringList.Add(filename);
            }
        }
        public void loadConversationDropdownList()
        {
            DropdownStringLists.conversationTypeStringList = new List<string>();
            DropdownStringLists.conversationTypeStringList.Add("none");
            foreach (string conv in mod.moduleConvosList)
            {
                DropdownStringLists.conversationTypeStringList.Add(conv);
            }            
        }
        public void loadEncounterDropdownList()
        {
            DropdownStringLists.encounterTypeStringList = new List<string>();
            DropdownStringLists.encounterTypeStringList.Add("none");
            foreach (Encounter enc in this.encountersList)
            {
                DropdownStringLists.encounterTypeStringList.Add(enc.encounterName);
            }
        }
        public void loadMusicDropdownList()
        {
            DropdownStringLists.musicStringList = new List<string>();
            DropdownStringLists.musicStringList.Add("none");
            string jobDir = "";
            jobDir = this._mainDirectory + "\\modules\\" + mod.moduleName + "\\music";
            foreach (string f in Directory.GetFiles(jobDir, "*.mp3", SearchOption.AllDirectories))
            {
                string filename = Path.GetFileNameWithoutExtension(f);
                DropdownStringLists.musicStringList.Add(filename);
            }
        }
        public void loadPlayerClassesTagsList()
        {
            DropdownStringLists.playerClassTagsTypeStringList = new List<string>();
            foreach (PlayerClass pcl in this.playerClassesList)
            {
                DropdownStringLists.playerClassTagsTypeStringList.Add(pcl.tag);
            }
        }
        public void loadRacesTagsList()
        {
            DropdownStringLists.raceTagsTypeStringList = new List<string>();
            foreach (Race rc in this.racesList)
            {
                DropdownStringLists.raceTagsTypeStringList.Add(rc.tag);
            }
        }
        public void loadSpellTagsList()
        {
            DropdownStringLists.spellTagsTypeStringList = new List<string>();
            DropdownStringLists.spellTagsTypeStringList.Add("none");
            foreach (Spell sp in this.spellsList)
            {
                DropdownStringLists.spellTagsTypeStringList.Add(sp.tag);
            }
        }

        public void loadTraitTagsList()
        {
            DropdownStringLists.traitsTagsTypeStringList = new List<string>();
            DropdownStringLists.traitsTagsTypeStringList.Add("none");
            foreach (Trait t in this.traitsList)
            {
                DropdownStringLists.traitsTagsTypeStringList.Add(t.tag);
            }
        }
        public void loadFactionTagsList()
        {
            DropdownStringLists.factionsTagsTypeStringList = new List<string>();
            DropdownStringLists.factionsTagsTypeStringList.Add("none");
            foreach (Faction t in this.factionsList)
            {
                DropdownStringLists.factionsTagsTypeStringList.Add(t.tag);
            }
        }
        public void loadEffectTagsList()
        {
            DropdownStringLists.effectTagsTypeStringList = new List<string>();
            DropdownStringLists.effectTagsTypeStringList.Add("none");
            foreach (Effect ef in this.effectsList)
            {
                DropdownStringLists.effectTagsTypeStringList.Add(ef.tag);
            }
        }
        public void loadWeatherEffectsTagsList()
        {
            DropdownStringLists.weatherEffectsTagsTypeStringList = new List<string>();
            DropdownStringLists.weatherEffectsTagsTypeStringList.Add("none");
            foreach (WeatherEffect ef in this.weatherEffectsList)
            {
                DropdownStringLists.weatherEffectsTagsTypeStringList.Add(ef.tag);
            }
        }
        public void loadWeatherEffectsNamesList()
        {
            DropdownStringLists.weatherEffectsNamesTypeStringList = new List<string>();
            DropdownStringLists.weatherEffectsNamesTypeStringList.Add("none");
            foreach (WeatherEffect ef in this.weatherEffectsList)
            {
                DropdownStringLists.weatherEffectsNamesTypeStringList.Add(ef.name);
            }
        }

        public void loadWeatherTagsList()
        {
            DropdownStringLists.weathersTagsTypeStringList = new List<string>();
            DropdownStringLists.weathersTagsTypeStringList.Add("none");
            foreach (Weather ef in this.weathersList)
            {
                DropdownStringLists.weathersTagsTypeStringList.Add(ef.tag);
            }
        }

        public class EffectTagTypeConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            //true means show a combobox
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            //true will limit to list. false will show the list, but allow free-form entry
            return true;
        }
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(DropdownStringLists.effectTagsTypeStringList);
        }
    }

        public class WeatherEffectsTagTypeConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                //true means show a combobox
                return true;
            }
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                //true will limit to list. false will show the list, but allow free-form entry
                return true;
            }
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(DropdownStringLists.weatherEffectsTagsTypeStringList);
            }
        }

        public class WeatherEffectsNameTypeConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                //true means show a combobox
                return true;
            }
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                //true will limit to list. false will show the list, but allow free-form entry
                return true;
            }
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(DropdownStringLists.weatherEffectsNamesTypeStringList);
            }
        }

        public class WeathersTagTypeConverter : StringConverter
        {
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                //true means show a combobox
                return true;
            }
            public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
            {
                //true will limit to list. false will show the list, but allow free-form entry
                return true;
            }
            public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return new StandardValuesCollection(DropdownStringLists.weathersTagsTypeStringList);
            }
        }

        private void fillScriptList()
        {
            scriptList.Clear();
            string jobDir = this._mainDirectory + "\\default\\NewModule\\scripts";            
            foreach (string f in Directory.GetFiles(jobDir, "*.cs"))
            {
                string filename = Path.GetFileName(f);
                scriptList.Add(filename);
            }            
        }
        private void saveAsTemp()
        {
            lastModuleFullPath = _mainDirectory + "\\default\\NewModule";
            mod.moduleName = "temp01";
            string directory = _mainDirectory + "\\modules\\" + mod.moduleName;
            try
            {
                if (!Directory.Exists(directory)) // if folder does not exist, create it and copy contents from previous folder
                {
                    createDirectory(directory);
                    createDirectory(directory + "//data");
                    DirectoryCopy(lastModuleFullPath, directory, true); // needs to copy contents from previous folder into new folder and overwrite files with new updates
                    createFiles(directory);
                }
                else
                {
                    createDirectory(directory + "//data");
                    createFiles(directory); // if folder exists, then overwrite all files in folder
                }
                //MessageBox.Show("temp01 module saved");
                refreshDropDownLists();
            }
            catch (Exception e)
            {
                MessageBox.Show("failed to save temp01 module: " + e.ToString());
            }
        }
        private void saveFiles()
        {
            /*
            area = area.loadAreaFile(g_dir + "\\" + g_fil + ".lvl");
            Area areaOrg = new Area();

            if (area == null)
            {
                MessageBox.Show("returned a null area");
            }

            if (area.masterOfThisArea != "none")
            {
               
                areaOrg = areaOrg.loadAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
            
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
                }
                //}
            }
            */

            if ((mod.startingArea == null) || (mod.startingArea == ""))
            {
                MessageBox.Show("Starting area was not detected, please type in the starting area's name in module properties (Edit/Modules Properties). Your module will not work without a starting area defined.");
                //return;
            }
            if ((mod.moduleName.Length == 0) || (mod.moduleName == "NewModule"))
            {
                saveAsFiles();
                return;
            }
            string directory = _mainDirectory + "\\modules\\" + mod.moduleName;
            try
            {
                if (!Directory.Exists(directory)) // if folder does not exist, create it and copy contents from previous folder
                {
                    createDirectory(directory);
                    DirectoryCopy(lastModuleFullPath, directory, true); // needs to copy contents from previous folder into new folder and overwrite files with new updates
                    createFiles(directory);
                }
                else
                {
                    createFiles(directory); // if folder exists, then overwrite all files in folder
                }
                MessageBox.Show("Moduled saved");
                refreshDropDownLists();
                this.Text = "IceBlinkRPG Toolset - " + mod.moduleLabelName;
            }
            catch (Exception e)
            {
                MessageBox.Show("failed to save module: " + e.ToString());
            }
        }
        private void saveAsFiles()
        {
            if ((mod.startingArea == null) || (mod.startingArea == ""))
            {
                //MessageBox.Show("Starting area was not detected, please type in the starting area's name in module properties (Edit/Modules Properties). Your module will not work without a starting area defined.");
                //return;
            }
            //if (mod.ModuleName != "NewModule")
            //{
                lastModuleFullPath = _mainDirectory + "\\modules\\" + mod.moduleName;
            //}
            //else
            //{
            //    lastModuleFullPath = _mainDirectory + "\\data\\" + mod.ModuleFolderName;
            //}
            ModuleNameDialog mnd = new ModuleNameDialog();
            mnd.ShowDialog();
            mod.moduleName = mnd.ModText;
            saveFiles();
        }
        private void incrementalSave() //incremental save option
        {
            if ((mod.startingArea == null) || (mod.startingArea == ""))
            {
                MessageBox.Show("Starting area was not detected, please type in the starting area's name in module properties (Edit/Modules Properties). Your module will not work without a starting area defined.");
                //return;
            }
            else
            {
                // save a backup with a incremental folder name
                string lastDir = mod.moduleName;
                string workingDir = _mainDirectory + "\\modules";
                string backupDir = _mainDirectory + "\\module_backups";
                string fileName = mod.moduleName;
                string incrementDirName = "";
                for (int i = 0; i < 999; i++) // add an incremental save option (uses directoryName plus number for folder name)
                {
                    if (!Directory.Exists(backupDir + "\\" + fileName + "(" + i.ToString() + ")"))
                    {
                        incrementDirName = fileName + "(" + i.ToString() + ")";
                        createDirectory(backupDir + "\\" + incrementDirName);
                        DirectoryCopy(workingDir + "\\" + lastDir, backupDir + "\\" + incrementDirName, true); // needs to copy contents from previous folder into new folder and overwrite files with new updates
                        //DirectoryInfo dir = Directory.CreateDirectory(workingDir + "\\" + incrementDirName);
                        createFiles(backupDir + "\\" + incrementDirName);
                        break;
                    }
                    else
                    {
                        //lastDir = workingDir + "\\" + fileName + "(" + i.ToString() + ")";
                    }
                }
                MessageBox.Show("Moduled backup " + incrementDirName + " was saved");

                // save over original module
                string directory = _mainDirectory + "\\modules\\" + mod.moduleName;
                try
                {
                    if (!Directory.Exists(directory)) // if folder exists, then overwrite all files in folder
                    {
                        createDirectory(directory);
                    }
                    createFiles(directory);
                    MessageBox.Show("Moduled saved");
                }
                catch
                {
                    MessageBox.Show("failed to save module");
                }
            }
        }

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //own method for height shadow calculation, expecting an area as input
        public void calculateHeightShadows(Area area)
        {
            //string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas\\";
            //mod.loadAreas(filePath, area);

            #region Calculate Height Shadows

            int indexOfNorthernNeighbour = -1;
            int indexOfSouthernNeighbour = -1;
            int indexOfEasternNeighbour = -1;
            int indexOfWesternNeighbour = -1;
            int indexOfNorthEasternNeighbour = -1;
            int indexOfNorthWesternNeighbour = -1;
            int indexOfSouthEasternNeighbour = -1;
            int indexOfSouthWesternNeighbour = -1;
            /*
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
            */

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
                        if (x == mod.moduleAreasObjects[indexOfWesternNeighbour].MapSizeX - 1)
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

            //mod.moduleAreasObjects.Clear();
        }

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX



        private void createFiles(string fullPathDirectory)
        {
            try
            {
                //clean up the spellsAllowed and traitsAllowed
                foreach (PlayerClass pcls in playerClassesList)
                {
                    for (int i = pcls.spellsAllowed.Count - 1; i >= 0; i--)
                    {
                        if (!pcls.spellsAllowed[i].allow)
                        {
                            pcls.spellsAllowed.RemoveAt(i);
                        }
                    }
                    for (int i = pcls.traitsAllowed.Count - 1; i >= 0; i--)
                    {
                        if (!pcls.traitsAllowed[i].allow)
                        {
                            pcls.traitsAllowed.RemoveAt(i);
                        }
                    }
                }
                //mod.VersionIB = game.IBVersion;
                mod.saveModuleFile(fullPathDirectory + "\\" + mod.moduleName + ".mod");
                saveCreaturesFile(fullPathDirectory + "\\data\\creatures.json");
                saveItemsFile(fullPathDirectory + "\\data\\items.json");
                saveContainersFile(fullPathDirectory + "\\data\\containers.json");
                saveShopsFile(fullPathDirectory + "\\data\\shops.json");
                saveEncountersFile(fullPathDirectory + "\\data\\encounters.json");
                savePropsFile(fullPathDirectory + "\\data\\props.json");
                saveJournalFile(fullPathDirectory + "\\data\\journal.json");
                savePlayerClassesFile(fullPathDirectory + "\\data\\playerClasses.json");
                saveRacesFile(fullPathDirectory + "\\data\\races.json");
                saveSpellsFile(fullPathDirectory + "\\data\\spells.json");
                saveTraitsFile(fullPathDirectory + "\\data\\traits.json");
                saveFactionsFile(fullPathDirectory + "\\data\\factions.json");
                saveWeatherEffectsFile(fullPathDirectory + "\\data\\weatherEffects.json");
                saveWeathersFile(fullPathDirectory + "\\data\\weathers.json");
                saveEffectsFile(fullPathDirectory + "\\data\\effects.json");
                // save convos that are open
                foreach (Convo convo in openConvosList)
                {
                    try
                    {
                        convo.SaveContentConversation(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\dialog", convo.ConvoFileName + ".json");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not save Convo file to disk. Original error: " + ex.Message);
                    }
                }
                // save logic trees that are open
                /*//REMOVEforeach (LogicTree logtre in openLogicTreesList)
                {
                    try
                    {
                        logtre.SaveLogicTree(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\logictree", logtre.Filename + ".json");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not save Logic Tree file to disk. Original error: " + ex.Message);
                    }
                }*/
                // save areas that are open
                foreach (Area a in openAreasList)
                {
                    foreach (Prop p in a.Props)
                    {
                        int test = 1;
                        p.spawnArea = a.Filename;
                        p.spawnLocationX = p.LocationX;
                        p.spawnLocationY = p.LocationY;
                        p.spawnLocationZ = p.LocationZ;
                    }

                    try
                    {
                        a.saveAreaFile(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas\\" + a.Filename + ".lvl");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not save area file to disk. Original error: " + ex.Message);
                    }
                }

                Area areaTempMaster = new Area();
                Area areaTempLink = new Area();

                foreach (String masterAreaString in mod.masterAreasList)
                {
                    bool areaMasterExists = false;
                    foreach (String areaDoubleCheck in mod.moduleAreasList)
                    {
                        if (masterAreaString == areaDoubleCheck)
                        {
                            areaMasterExists = true;
                            break;
                        }
                    }

                    if (areaMasterExists)
                    {
                        areaTempMaster = areaTempMaster.loadAreaFile(_mainDirectory + "\\modules\\" + mod.moduleName + "\\areas" + "\\" + masterAreaString + ".lvl");

                        for (int i = 0; i < areaTempMaster.Tiles.Count(); i++)
                        {
                            bool resetTile = true;
                            int TileLocX = i % areaTempMaster.MapSizeY;
                            int TileLocY = i / areaTempMaster.MapSizeX;

                            foreach (Trigger trig in areaTempMaster.Triggers)
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
                                areaTempMaster.Tiles[i].transitionToMasterDirection = "none";
                                areaTempMaster.Tiles[i].numberOfLinkedAreaToTransitionTo = -1;
                            }
                        }


                        foreach (String linkedAreaString in areaTempMaster.linkedAreas)
                        {
                            bool areaLinkExists = false;
                            foreach (String areaDoubleCheck in mod.moduleAreasList)
                            {
                                if (linkedAreaString == areaDoubleCheck)
                                {
                                    areaLinkExists = true;
                                    break;
                                }
                            }

                            if (areaLinkExists)
                            {
                                areaTempLink = areaTempLink.loadAreaFile(_mainDirectory + "\\modules\\" + mod.moduleName + "\\areas" + "\\" + linkedAreaString + ".lvl");

                                //finalBlow1
                                //1. we need to synch tiles on this link with its master
                                for (int j = 0; j < areaTempLink.Tiles.Count; j++)
                                {
                                    bool temp = areaTempLink.Tiles[j].linkedToMasterMap;
                                    if (areaTempLink.Tiles[j].linkedToMasterMap)
                                    {
                                        areaTempLink.Tiles[j] = areaTempMaster.Tiles[j].ShallowCopy();
                                    }
                                    //newArea = newArea.loadAreaFile(path + areaName + ".lvl");
                                    areaTempLink.Tiles[j].linkedToMasterMap = temp;
                                    //linked tiles block line of sight always (no uncovering fog of war through cave walls)
                                    if (areaTempLink.Tiles[j].linkedToMasterMap)
                                    {
                                        areaTempLink.Tiles[j].LoSBlocked = true;
                                    }
                                }

                                //TODO: calculate height shadows here
                                calculateHeightShadows(areaTempLink);


                                //1b. let us synch the props of this link with its master
                                //add new props (placed after creation of clone)
                                for (int j = 0; j < areaTempMaster.Props.Count; j++)
                                {
                                    bool allowAdding = false;
                                    if (!areaTempMaster.Props[j].isMover)
                                    {
                                        allowAdding = true;
                                        for (int k = 0; k < areaTempLink.Props.Count; k++)
                                        {
                                            if (areaTempMaster.Props[j].PropTag == areaTempLink.Props[k].PropTag)
                                            {
                                                allowAdding = false;
                                                break;
                                            }
                                        }
                                        if (allowAdding)
                                        {
                                            areaTempLink.Props.Add(areaTempMaster.Props[j]);
                                        }
                                    }
                                }

                                //remove props that dont exist on master any more
                                for (int i = areaTempLink.Props.Count - 1; i >= 0; i--)
                                {
                                    bool removeThisProp = true;
                                    foreach (Prop p in areaTempMaster.Props)
                                    {
                                        if (p.PropTag == areaTempLink.Props[i].PropTag)
                                        {
                                            removeThisProp = false;
                                            break;
                                        }
                                    }

                                    if (removeThisProp)
                                    {
                                        areaTempLink.Props.Remove(areaTempLink.Props[i]);
                                    }
                                }


                                //2. we need to remove transition triggers to this link from master if those do not exist on this link (check tagOfLink)
                                for (int triggerIndexOnMaster = areaTempMaster.Triggers.Count -1; triggerIndexOnMaster >= 0; triggerIndexOnMaster--)
                                {
                                    bool deleteThisTrigger = false;
                                    //link is a link from master to this linked area
                                    if (areaTempMaster.Triggers[triggerIndexOnMaster].tagOfLink == areaTempLink.Filename)
                                    {
                                        deleteThisTrigger = true;
                                        foreach (Trigger triggerOnLink in areaTempLink.Triggers)
                                        {
                                            if ((triggerOnLink.TriggerTag + "_" + areaTempMaster.Filename) == areaTempMaster.Triggers[triggerIndexOnMaster].TriggerTag)
                                            {
                                                deleteThisTrigger = false;
                                                //actually also rotation might have changed, so we better replace anyway
                                                //areaTempMaster.Triggers[triggerIndexOnMaster] = triggerOnLink;
                                                if(triggerOnLink.transitionToMasterRotationCounter == 1)
                                                {
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].isLinkToMaster = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].tagOfLink = areaTempLink.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].tagOfLinkedMaster = areaTempMaster.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Enabled = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].EnabledEvent1 = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1Type = "transition";
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1FilenameOrTag = areaTempLink.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1TransPointX = triggerOnLink.TriggerSquaresList[0].X;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1TransPointY = triggerOnLink.TriggerSquaresList[0].Y + 1;
                                                    Coordinate newCoor = new Coordinate();
                                                    newCoor.X = triggerOnLink.TriggerSquaresList[0].X;
                                                    newCoor.Y = triggerOnLink.TriggerSquaresList[0].Y + 1;
                                                    if (areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Count <= 1)
                                                    {
                                                        if (areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Count == 1)
                                                        {
                                                            areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.RemoveAt(0);
                                                        }
                                                        areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Add(newCoor);
                                                    }
                                                    areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].transitionToMasterDirection = areaTempLink.Tiles[triggerOnLink.TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + triggerOnLink.TriggerSquaresList[0].X].transitionToMasterDirection;
                                                    areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].numberOfLinkedAreaToTransitionTo = areaTempLink.linkNumberOfThisArea;
                                                }
                                                if (triggerOnLink.transitionToMasterRotationCounter == 2)
                                                {
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].isLinkToMaster = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].tagOfLink = areaTempLink.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].tagOfLinkedMaster = areaTempMaster.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Enabled = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].EnabledEvent1 = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1Type = "transition";
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1FilenameOrTag = areaTempLink.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1TransPointX = triggerOnLink.TriggerSquaresList[0].X - 1;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1TransPointY = triggerOnLink.TriggerSquaresList[0].Y;
                                                    Coordinate newCoor = new Coordinate();
                                                    newCoor.X = triggerOnLink.TriggerSquaresList[0].X - 1;
                                                    newCoor.Y = triggerOnLink.TriggerSquaresList[0].Y;
                                                    if (areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Count <= 1)
                                                    {
                                                        if (areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Count == 1)
                                                        {
                                                            areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.RemoveAt(0);
                                                        }
                                                        areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Add(newCoor);
                                                    }
                                                    areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].transitionToMasterDirection = areaTempLink.Tiles[triggerOnLink.TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + triggerOnLink.TriggerSquaresList[0].X].transitionToMasterDirection;
                                                    areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].numberOfLinkedAreaToTransitionTo = areaTempLink.linkNumberOfThisArea;
                                                }
                                                if (triggerOnLink.transitionToMasterRotationCounter == 3)
                                                {
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].isLinkToMaster = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].tagOfLink = areaTempLink.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].tagOfLinkedMaster = areaTempMaster.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Enabled = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].EnabledEvent1 = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1Type = "transition";
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1FilenameOrTag = areaTempLink.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1TransPointX = triggerOnLink.TriggerSquaresList[0].X;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1TransPointY = triggerOnLink.TriggerSquaresList[0].Y - 1;
                                                    Coordinate newCoor = new Coordinate();
                                                    newCoor.X = triggerOnLink.TriggerSquaresList[0].X;
                                                    newCoor.Y = triggerOnLink.TriggerSquaresList[0].Y - 1;
                                                    if (areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Count <= 1)
                                                    {
                                                        if (areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Count == 1)
                                                        {
                                                            areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.RemoveAt(0);
                                                        }
                                                        areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Add(newCoor);
                                                    }
                                                    areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].transitionToMasterDirection = areaTempLink.Tiles[triggerOnLink.TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + triggerOnLink.TriggerSquaresList[0].X].transitionToMasterDirection;
                                                    areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].numberOfLinkedAreaToTransitionTo = areaTempLink.linkNumberOfThisArea;
                                                }
                                                if (triggerOnLink.transitionToMasterRotationCounter == 4)
                                                {
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].isLinkToMaster = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].tagOfLink = areaTempLink.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].tagOfLinkedMaster = areaTempMaster.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Enabled = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].EnabledEvent1 = true;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1Type = "transition";
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1FilenameOrTag = areaTempLink.Filename;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1TransPointX = triggerOnLink.TriggerSquaresList[0].X + 1;
                                                    areaTempMaster.Triggers[triggerIndexOnMaster].Event1TransPointY = triggerOnLink.TriggerSquaresList[0].Y;
                                                    Coordinate newCoor = new Coordinate();
                                                    newCoor.X = triggerOnLink.TriggerSquaresList[0].X + 1;
                                                    newCoor.Y = triggerOnLink.TriggerSquaresList[0].Y;
                                                    if (areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Count <= 1)
                                                    {
                                                        if (areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Count == 1)
                                                        {
                                                            areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.RemoveAt(0);
                                                        }
                                                        areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList.Add(newCoor);
                                                    }
                                                    areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].transitionToMasterDirection = areaTempLink.Tiles[triggerOnLink.TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + triggerOnLink.TriggerSquaresList[0].X].transitionToMasterDirection;
                                                    areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].numberOfLinkedAreaToTransitionTo = areaTempLink.linkNumberOfThisArea;
                                                }
                                                break;
                                            }
                                        }
                                    }

                                    if (deleteThisTrigger)
                                    {
                                        //kirche
                                        areaTempMaster.Tiles[areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList[0].X].transitionToMasterDirection = "none";
                                        areaTempMaster.Tiles[areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + areaTempMaster.Triggers[triggerIndexOnMaster].TriggerSquaresList[0].X].numberOfLinkedAreaToTransitionTo = -1;
                                        areaTempMaster.Triggers.RemoveAt(triggerIndexOnMaster);
                                    }
                                }

                                //3. we need to add transistion triggers from this link to master if those do not already exist on master
                                foreach (Trigger triggerOnLink in areaTempLink.Triggers)
                                {
                                    if (triggerOnLink.isLinkToMaster)
                                    {
                                        bool addTransitionTrigger = true;
                                        foreach (Trigger triggerOnMaster in areaTempMaster.Triggers)
                                        {
                                            if (triggerOnMaster.TriggerTag == triggerOnLink.TriggerTag + "_" + areaTempMaster.Filename)
                                            {
                                                addTransitionTrigger = false;
                                                break;
                                            }
                                        }

                                        if (addTransitionTrigger)
                                        {
                                            //not good, just references to the same thingie
                                            /*
                                            string placeholder = triggerOnLink.TriggerTag;
                                            triggerOnLink.TriggerTag += "_" + areaTempMaster.Filename;
                                            areaTempMaster.Triggers.Add(triggerOnLink);
                                            triggerOnLink.TriggerTag = placeholder;
                                            */

                                            Trigger newTriggerForMaster = new Trigger();

                                            if (triggerOnLink.transitionToMasterRotationCounter == 1)
                                            {
                                                newTriggerForMaster.isLinkToMaster = true;
                                                newTriggerForMaster.tagOfLink = areaTempLink.Filename;
                                                newTriggerForMaster.tagOfLinkedMaster = areaTempMaster.Filename;
                                                newTriggerForMaster.Enabled = true;
                                                newTriggerForMaster.EnabledEvent1 = true;
                                                newTriggerForMaster.Event1Type = "transition";
                                                newTriggerForMaster.Event1FilenameOrTag = areaTempLink.Filename;
                                                newTriggerForMaster.Event1TransPointX = triggerOnLink.TriggerSquaresList[0].X;
                                                newTriggerForMaster.Event1TransPointY = triggerOnLink.TriggerSquaresList[0].Y + 1;
                                                Coordinate newCoor = new Coordinate();
                                                newCoor.X = triggerOnLink.TriggerSquaresList[0].X;
                                                newCoor.Y = triggerOnLink.TriggerSquaresList[0].Y + 1;
                                                if (newTriggerForMaster.TriggerSquaresList.Count <= 1)
                                                {
                                                    if (newTriggerForMaster.TriggerSquaresList.Count == 1)
                                                    {
                                                        newTriggerForMaster.TriggerSquaresList.RemoveAt(0);
                                                    }
                                                    newTriggerForMaster.TriggerSquaresList.Add(newCoor);
                                                }
                                                areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].transitionToMasterDirection = areaTempLink.Tiles[triggerOnLink.TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + triggerOnLink.TriggerSquaresList[0].X].transitionToMasterDirection;
                                                areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].numberOfLinkedAreaToTransitionTo = areaTempLink.linkNumberOfThisArea;
                                            }
                                            if (triggerOnLink.transitionToMasterRotationCounter == 2)
                                            {
                                                newTriggerForMaster.isLinkToMaster = true;
                                                newTriggerForMaster.tagOfLink = areaTempLink.Filename;
                                                newTriggerForMaster.tagOfLinkedMaster = areaTempMaster.Filename;
                                                newTriggerForMaster.Enabled = true;
                                                newTriggerForMaster.EnabledEvent1 = true;
                                                newTriggerForMaster.Event1Type = "transition";
                                                newTriggerForMaster.Event1FilenameOrTag = areaTempLink.Filename;
                                                newTriggerForMaster.Event1TransPointX = triggerOnLink.TriggerSquaresList[0].X - 1;
                                                newTriggerForMaster.Event1TransPointY = triggerOnLink.TriggerSquaresList[0].Y;
                                                Coordinate newCoor = new Coordinate();
                                                newCoor.X = triggerOnLink.TriggerSquaresList[0].X - 1;
                                                newCoor.Y = triggerOnLink.TriggerSquaresList[0].Y;
                                                if (newTriggerForMaster.TriggerSquaresList.Count <= 1)
                                                {
                                                    if (newTriggerForMaster.TriggerSquaresList.Count == 1)
                                                    {
                                                        newTriggerForMaster.TriggerSquaresList.RemoveAt(0);
                                                    }
                                                    newTriggerForMaster.TriggerSquaresList.Add(newCoor);
                                                }
                                                areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].transitionToMasterDirection = areaTempLink.Tiles[triggerOnLink.TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + triggerOnLink.TriggerSquaresList[0].X].transitionToMasterDirection;
                                                areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].numberOfLinkedAreaToTransitionTo = areaTempLink.linkNumberOfThisArea;
                                            }
                                            if (triggerOnLink.transitionToMasterRotationCounter == 3)
                                            {
                                                newTriggerForMaster.isLinkToMaster = true;
                                                newTriggerForMaster.tagOfLink = areaTempLink.Filename;
                                                newTriggerForMaster.tagOfLinkedMaster = areaTempMaster.Filename;
                                                newTriggerForMaster.Enabled = true;
                                                newTriggerForMaster.EnabledEvent1 = true;
                                                newTriggerForMaster.Event1Type = "transition";
                                                newTriggerForMaster.Event1FilenameOrTag = areaTempLink.Filename;
                                                newTriggerForMaster.Event1TransPointX = triggerOnLink.TriggerSquaresList[0].X;
                                                newTriggerForMaster.Event1TransPointY = triggerOnLink.TriggerSquaresList[0].Y - 1;
                                                Coordinate newCoor = new Coordinate();
                                                newCoor.X = triggerOnLink.TriggerSquaresList[0].X;
                                                newCoor.Y = triggerOnLink.TriggerSquaresList[0].Y - 1;
                                                if (newTriggerForMaster.TriggerSquaresList.Count <= 1)
                                                {
                                                    if (newTriggerForMaster.TriggerSquaresList.Count == 1)
                                                    {
                                                        newTriggerForMaster.TriggerSquaresList.RemoveAt(0);
                                                    }
                                                    newTriggerForMaster.TriggerSquaresList.Add(newCoor);
                                                }
                                                areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].transitionToMasterDirection = areaTempLink.Tiles[triggerOnLink.TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + triggerOnLink.TriggerSquaresList[0].X].transitionToMasterDirection;
                                                areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].numberOfLinkedAreaToTransitionTo = areaTempLink.linkNumberOfThisArea;
                                            }
                                            if (triggerOnLink.transitionToMasterRotationCounter == 4)
                                            {
                                                newTriggerForMaster.isLinkToMaster = true;
                                                newTriggerForMaster.tagOfLink = areaTempLink.Filename;
                                                newTriggerForMaster.tagOfLinkedMaster = areaTempMaster.Filename;
                                                newTriggerForMaster.Enabled = true;
                                                newTriggerForMaster.EnabledEvent1 = true;
                                                newTriggerForMaster.Event1Type = "transition";
                                                newTriggerForMaster.Event1FilenameOrTag = areaTempLink.Filename;
                                                newTriggerForMaster.Event1TransPointX = triggerOnLink.TriggerSquaresList[0].X + 1;
                                                newTriggerForMaster.Event1TransPointY = triggerOnLink.TriggerSquaresList[0].Y;
                                                Coordinate newCoor = new Coordinate();
                                                newCoor.X = triggerOnLink.TriggerSquaresList[0].X + 1;
                                                newCoor.Y = triggerOnLink.TriggerSquaresList[0].Y;
                                                if (newTriggerForMaster.TriggerSquaresList.Count <= 1)
                                                {
                                                    if (newTriggerForMaster.TriggerSquaresList.Count == 1)
                                                    {
                                                        newTriggerForMaster.TriggerSquaresList.RemoveAt(0);
                                                    }
                                                    newTriggerForMaster.TriggerSquaresList.Add(newCoor);
                                                }
                                                areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].transitionToMasterDirection = areaTempLink.Tiles[triggerOnLink.TriggerSquaresList[0].Y * areaTempMaster.MapSizeX + triggerOnLink.TriggerSquaresList[0].X].transitionToMasterDirection;
                                                areaTempMaster.Tiles[newCoor.Y * areaTempMaster.MapSizeX + newCoor.X].numberOfLinkedAreaToTransitionTo = areaTempLink.linkNumberOfThisArea;
                                            }

                                            //when it is set up, we add we finally name it
                                            newTriggerForMaster.TriggerTag = triggerOnLink.TriggerTag + "_" + areaTempMaster.Filename;

                                            //then add it
                                            areaTempMaster.Triggers.Add(newTriggerForMaster);
                                        }   
                                    }
                                }

                                //we need to save the current link area to file here
                                areaTempLink.saveAreaFile(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas\\" + areaTempLink.Filename + ".lvl");
                            }//done with current link area (can be one of many for this master)

                        }//end of the strings loop for linked areas

                        // wee need to save the current master area to file here
                        areaTempMaster.saveAreaFile(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas\\" + areaTempMaster.Filename + ".lvl");
                    }//done with current master area

                }//end of the strings loop for the master areas

                    /*
                    Area areaTemp = new Area();
                    foreach (String areaString in mod.moduleAreasList)
                    {
                        areaTemp = areaTemp.loadAreaFile(_mainDirectory + "\\modules\\" + mod.moduleName + "\\areas" + "\\" + areaString + ".lvl");

                        if (areaTemp.m)
                    }
                    */
                    //areaOrg = areaOrg.loadAreaFile(g_dir + "\\" + area.masterOfThisArea + ".lvl");
                    //string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas";
                    //newAreaLink = newAreaLink.loadAreaFile(filePath + "\\" + prntForm.mod.moduleAreasList[selectedIndex] + ".lvl");
                }
            catch
            {
                MessageBox.Show("failed to createFiles");
            }
        }
        private void newModule()
        {
            openModule(_mainDirectory + "\\default\\NewModule\\NewModule.mod");
            openCreatures(_mainDirectory + "\\default\\NewModule\\data\\creatures.json");
            openItems(_mainDirectory + "\\default\\NewModule\\data\\items.json");
        }
        private void createDirectory(string fullPath)
        {
            try
            {
                DirectoryInfo dir = Directory.CreateDirectory(fullPath);
            }
            catch { MessageBox.Show("failed to create the directory: " + fullPath); }
        }
        private void DirectoryCopy(string sourceDirPath, string destDirPath, bool copySubDirs)
        {
            try
            {
                //string _currentDir = System.IO.Directory.GetCurrentDirectory();
                DirectoryInfo dir = new DirectoryInfo(sourceDirPath);
                DirectoryInfo[] dirs = dir.GetDirectories();

                FileInfo[] files = dir.GetFiles(); // Get the file contents of the directory to copy.
                foreach (FileInfo file in files)
                {
                    try
                    {
                        if (file.Name != "NewModule.mod")
                        {
                            string temppath = Path.Combine(destDirPath, file.Name); // Create the path to the new copy of the file.
                            file.CopyTo(temppath, false); // Copy the file.
                        }
                    }
                    catch (Exception ex)
                    { MessageBox.Show("failed to copy file: " + ex.ToString()); }
                }

                if (copySubDirs) // If copySubDirs is true, copy the subdirectories.
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        try
                        {
                            string temppath = Path.Combine(destDirPath, subdir.Name); // Create the subdirectory.
                            createDirectory(temppath);
                            DirectoryCopy(subdir.FullName, temppath, copySubDirs); // Copy the subdirectories.
                        }
                        catch (Exception ex)
                        { MessageBox.Show("failed to copy sub folders: " + ex.ToString()); }
                    }
                }
            }
            catch (Exception ex) 
            { MessageBox.Show("failed to copy the directory: " + ex.ToString()); }
        }
        private void refreshIcon()
        {
            if (frmBlueprints.tabCreatureItem.SelectedIndex == 0) //creature
            {
                refreshIconCreatures();
            }
            else if (frmBlueprints.tabCreatureItem.SelectedIndex == 1) //item
            {
                refreshIconItems();
            }
            else //prop
            {
                refreshIconProps();
            }
        }
        public void logText(string text)
        {
            frmLog.rtxtLog.AppendText(text);
            frmLog.rtxtLog.ScrollToCaret();
        }
        #endregion

        #region Event Handlers
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFiles();
        }
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openFiles();
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFiles();
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsFiles();
        }
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveFiles();
        }
        private void tsbSaveIncremental_Click(object sender, EventArgs e)
        {
            incrementalSave();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
            //newModule();
        }
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
            //newModule();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // maybe should ask to save first if any changes have been made
            this.Close();
        }
        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIceBlinkProperties.Show(dockPanel1);
        }
        private void spriteIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIconSprite.Show(dockPanel1);
        }
        private void blueprintsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBlueprints.Show(dockPanel1);
        }
        private void triggerEventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTriggerEvents.Show(dockPanel1);
        }
        private void areasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAreas.Show(dockPanel1);
        }
        /*//REMOVEprivate void logicTreesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogicTree.Show(dockPanel1);
        }*/
        private void iBScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIBScript.Show(dockPanel1);
        }
        private void conversationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConversations.Show(dockPanel1);
        }
        private void encountersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEncounters.Show(dockPanel1);
        }
        private void containersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmContainers.Show(dockPanel1);
        }
        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLog.Show(dockPanel1);
        }
        private void modulePropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmIceBlinkProperties.propertyGrid1.SelectedObject = mod;
        }
        private void journalEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JournalEditor journalEdit = new JournalEditor(mod, this);
            journalEdit.ShowDialog();
        }
        private void shopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShopEditor shopEdit = new ShopEditor(mod, this);
            shopEdit.ShowDialog();
        }
        private void playerClassEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayerClassEditor playerClassEdit = new PlayerClassEditor(mod, this);
            playerClassEdit.ShowDialog();
        }
        private void raceEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RacesEditor raceEdit = new RacesEditor(mod, this);
            raceEdit.ShowDialog();
        }
        private void spellEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpellEditor sEdit = new SpellEditor(mod, this);
            sEdit.ShowDialog();
        }
        private void traitEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TraitEditor tEdit = new TraitEditor(mod, this);
            tEdit.ShowDialog();
        }
        private void factionEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FactionEditor tEdit = new FactionEditor(mod, this);
            tEdit.ShowDialog();
        }
        private void weatherEffectsEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeatherEffectsEditor weEdit = new WeatherEffectsEditor(mod, this);
            weEdit.ShowDialog();
        }
        private void weatherEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WeatherEditor weEdit = new WeatherEditor(mod, this);
            weEdit.ShowDialog();
        }
        private void effectEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EffectEditor eEdit = new EffectEditor(mod, this);
            eEdit.ShowDialog();
        }
        private void playerEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayerEditor pcEdit = new PlayerEditor(this);
            pcEdit.ShowDialog();
        }
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(versionMessage);
            /*MessageBox.Show(test.EventTag1.TagOrFilename + " " +
                test.EventTag1.Parm1 + " " +
                test.EventTag1.Parm2 + " " +
                test.EventTag1.Parm3 + " " +
                test.EventTag1.Parm4 + " " +
                test.EventTag1.TransPoint.X.ToString() + " " +
                test.EventTag1.TransPoint.Y.ToString());*/
        }
        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("not implemented yet");
        }
        private void tabCreatureItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("changed tab");
            if (frmBlueprints.tabCreatureItem.SelectedIndex == 0)
            {
                //show sprite for currently selected creature
                refreshIcon();
            }
            else if (frmBlueprints.tabCreatureItem.SelectedIndex == 1) //item
            {
                //show icon for currently selected item
                refreshIcon();
            }
            else //prop
            {
                //show sprite for currently selected prop
                refreshIcon();
            }
        }
        private void btnSelectIcon_Click(object sender, EventArgs e)
        {
            if (frmBlueprints.tabCreatureItem.SelectedIndex == 0) //creature
            {
                LoadCreatureSprite();
            }
            else if (frmBlueprints.tabCreatureItem.SelectedIndex == 1) //item
            {
                LoadItemIcon();
            }
            else //prop
            {
                LoadPropSprite();
            }
        }
        #endregion        

        public string GetImageFilename(string filter)
        {
            if (mod.moduleName != "NewModule")
            {
                openFileDialog2.InitialDirectory = this._mainDirectory + "\\modules\\" + mod.moduleName + "\\graphics";
            }
            else
            {
                openFileDialog2.InitialDirectory = this._mainDirectory + "\\default\\NewModule\\graphics";
            }
            //Empty the FileName text box of the dialog
            openFileDialog2.FileName = filter + "*";
            openFileDialog2.Filter = "Image (*.png)|*.png|All Files (*.*)|*.*";
            openFileDialog2.FilterIndex = 1;

            DialogResult result = openFileDialog2.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                return Path.GetFileNameWithoutExtension(openFileDialog2.FileName);
            }
            return "none";
        }
        #region Creature Stuff
        public void refreshIconCreatures()
        {
            if (frmBlueprints.tvCreatures.SelectedNode != null)
            {
                try
                {
                    string _nodeTag = frmBlueprints.tvCreatures.SelectedNode.Name;
                    iconBitmap = (Bitmap)creaturesList[frmBlueprints.GetCreatureIndex(_nodeTag)].creatureIconBitmap.Clone();
                    frmIconSprite.pbIcon.BackgroundImage = (Image)iconBitmap;
                    if (iconBitmap == null) { MessageBox.Show("returned a null icon bitmap"); }
                }
                catch { }
            }
        }
        public void LoadCreatureSprite()
        {
            string _nodeTag = frmBlueprints.tvCreatures.SelectedNode.Name;
            string name = GetImageFilename("tkn_");
            if (name != "none")
            {
                creaturesList[frmBlueprints.GetCreatureIndex(_nodeTag)].cr_tokenFilename = name;
            }
            creaturesList[frmBlueprints.GetCreatureIndex(_nodeTag)].LoadCreatureBitmap(this);
            refreshIconCreatures();
            /*using (var sel = new SpriteSelector(game, mod))
            {
                var result = sel.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    string _nodeTag = lastSelectedCreatureNodeName;
                    string filename = game.returnSpriteFilename;
                    //IBMessageBox.Show(game, "filename selected = " + filename);
                    try
                    {
                        Creature crt = creaturesList.creatures[frmBlueprints.GetCreatureIndex(_nodeTag)];
                        crt.SpriteFilename = filename;
                        //creaturesList.creatures[frmBlueprints.GetCreatureIndex(_nodeTag)].LoadCharacterSprite(directory, filename);
                        crt.LoadSpriteStuff(_mainDirectory + "\\modules\\" + mod.ModuleFolderName);

                        //thisPC.SpriteFilename = filename;
                        //thisPC.LoadSpriteStuff(ccr_game.mainDirectory + "\\modules\\" + ccr_game.module.ModuleFolderName);
                        iconBitmap = (Bitmap)crt.CharSprite.Image.Clone();
                        //iconGameMap = new Bitmap(ccr_game.mainDirectory + "\\modules\\" + ccr_game.module.ModuleFolderName + "\\graphics\\sprites\\tokens\\player\\" + thisPC.CharSprite.SpriteSheetFilename);
                        frmIconSprite.pbIcon.BackgroundImage = (Image)iconBitmap;
                        if (iconBitmap == null) { MessageBox.Show("returned a null icon bitmap"); }
                        //iconBitmap.Image = (Image)iconGameMap;

                        //if (iconGameMap == null)
                        //{
                        //    IBMessageBox.Show(ccr_game, "returned a null icon bitmap");
                        //}
                    }
                    catch
                    {
                        MessageBox.Show("failed to load image...make sure to select a creature from the list first");
                    }
                }
            }
            loadCreatureSprites();      
            */
        }
        #endregion

        #region Item Stuff
        public void refreshIconItems()
        {
            if (frmBlueprints.tvItems.SelectedNode != null)
            {
                try
                {
                    string _nodeTag = frmBlueprints.tvItems.SelectedNode.Name;
                    iconBitmap = (Bitmap)itemsList[frmBlueprints.GetItemIndex(_nodeTag)].itemIconBitmap.Clone();
                    frmIconSprite.pbIcon.BackgroundImage = (Image)iconBitmap;
                    if (iconBitmap == null) { MessageBox.Show("returned a null icon bitmap"); }
                }
                catch { }
            }
        }
        public void LoadItemIcon()
        {
            string _nodeTag = frmBlueprints.tvItems.SelectedNode.Name;
            string name = GetImageFilename("it_");
            if (name != "none")
            {
                itemsList[frmBlueprints.GetItemIndex(_nodeTag)].itemImage = name;
            }
            itemsList[frmBlueprints.GetItemIndex(_nodeTag)].LoadItemBitmap(this);
            refreshIconItems();
            /*using (var sel = new ItemImageSelector(game, mod))
            {
                var result = sel.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    string filename = game.returnSpriteFilename;
                    string _nodeTag = lastSelectedItemNodeName;
                    try
                    {
                        itemsList.itemsList[frmBlueprints.GetItemIndex(_nodeTag)].ItemIconFilename = filename;
                        if (File.Exists(_mainDirectory + "\\modules\\" + mod.ModuleFolderName + "\\graphics\\sprites\\items\\" + filename))
                        {
                            iconBitmap = new Bitmap(_mainDirectory + "\\modules\\" + mod.ModuleFolderName + "\\graphics\\sprites\\items\\" + filename);
                        }
                        else if (File.Exists(_mainDirectory + "\\modules\\" + mod.ModuleFolderName + "\\graphics\\sprites\\" + filename))
                        {
                            iconBitmap = new Bitmap(_mainDirectory + "\\modules\\" + mod.ModuleFolderName + "\\graphics\\sprites\\" + filename);
                        }
                        else if (File.Exists(_mainDirectory + "\\data\\graphics\\sprites\\items\\" + filename))
                        {
                            iconBitmap = new Bitmap(_mainDirectory + "\\data\\graphics\\sprites\\items\\" + filename);
                        }
                        else if (File.Exists(_mainDirectory + "\\data\\graphics\\sprites\\" + filename))
                        {
                            iconBitmap = new Bitmap(_mainDirectory + "\\data\\graphics\\sprites\\" + filename);
                        }
                        else
                        {
                            MessageBox.Show("The image selected is not from one of the designated items folder locations...will use blank.png");
                            iconBitmap = new Bitmap(_mainDirectory + "\\data\\graphics\\blank.png");
                        }
                        frmIconSprite.pbIcon.BackgroundImage = (Image)iconBitmap;
                        if (iconBitmap == null) { MessageBox.Show("returned a null icon bitmap"); }
                    }
                    catch
                    {
                        MessageBox.Show("failed to load image...make sure to select an item from the list first");
                    }
                }
            }*/

            
        }
        public ItemRefs createItemRefsFromItem(Item it)
        {
            ItemRefs newIR = new ItemRefs();
            newIR.tag = it.tag + "_" + mod.nextIdNumber;
            newIR.name = it.name;
            newIR.resref = it.resref;
            newIR.quantity = it.quantity;
            newIR.canNotBeUnequipped = it.canNotBeUnequipped;
            newIR.isLightSource = it.isLightSource;
            newIR.isRation = it.isRation;

            return newIR;
        }
        #endregion

        #region Props Stuff
        public void refreshIconProps()
        {
            if (frmBlueprints.tvProps.SelectedNode != null)
            {
                try
                {
                    string _nodeTag = frmBlueprints.tvProps.SelectedNode.Name;
                    iconBitmap = (Bitmap)propsList[frmBlueprints.GetPropIndex(_nodeTag)].propBitmap.Clone();
                    frmIconSprite.pbIcon.BackgroundImage = (Image)iconBitmap;
                    if (iconBitmap == null) { MessageBox.Show("returned a null icon bitmap"); }
                }
                catch { }
            }
        }
        public void LoadPropSprite()
        {
            string _nodeTag = frmBlueprints.tvProps.SelectedNode.Name;
            string name = GetImageFilename("prp_");
            if (name != "none")
            {
                propsList[frmBlueprints.GetPropIndex(_nodeTag)].ImageFileName = name;
            }
            propsList[frmBlueprints.GetPropIndex(_nodeTag)].LoadPropBitmap(this);
            refreshIconProps();
            /*using (var sel = new PropSpriteSelector(game, mod))
            {
                var result = sel.ShowDialog();
                if (result == DialogResult.OK) // Test result.
                {
                    string _nodeTag = lastSelectedPropNodeName;
                    string filename = game.returnSpriteFilename;
                    try
                    {
                        Prop prp = propsList.propsList[frmBlueprints.GetPropIndex(_nodeTag)];
                        prp.PropSpriteFilename = filename;
                        prp.LoadPropSpriteStuffForTS(_mainDirectory + "\\modules\\" + mod.ModuleFolderName);

                        iconBitmap = (Bitmap)prp.PropSprite.Image.Clone();
                        frmIconSprite.pbIcon.BackgroundImage = (Image)iconBitmap;
                        if (iconBitmap == null) { MessageBox.Show("returned a null icon bitmap"); }
                    }
                    catch
                    {
                        MessageBox.Show("failed to load image...make sure to select a prop from the list first");
                    }
                }
            }
            loadPropSprites();
            */
            
        }
        #endregion                                                                        

        #region Save, Load and Get module data files                
        public void saveCreaturesFile(string filename)
        {
            string json = JsonConvert.SerializeObject(creaturesList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<Creature> loadCreaturesFile(string filename)
        {
            List<Creature> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Creature>)serializer.Deserialize(file, typeof(List<Creature>));
            }
            return toReturn;
        }
        public Creature getCreature(string name)
        {
            foreach (Creature cr in creaturesList)
            {
                if (cr.cr_name == name) return cr;
            }
            return null;
        }
        public Creature getCreatureByTag(string tag)
        {
            foreach (Creature crtag in creaturesList)
            {
                if (crtag.cr_tag == tag) return crtag;
            }
            return null;
        }
        public Creature getCreatureByResRef(string resref)
        {
            foreach (Creature crt in creaturesList)
            {
                if (crt.cr_resref == resref) return crt;
            }
            return null;
        }

        public void saveItemsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(itemsList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<Item> loadItemsFile(string filename)
        {
            List<Item> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Item>)serializer.Deserialize(file, typeof(List<Item>));
            }

            //try to also add all props from NewModule, but only if their tags are not already existing
            //openProps(_mainDirectory + "\\default\\NewModule\\data\\props.json");

            List<Item> fromNewModule = null;
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(_mainDirectory + "\\default\\NewModule\\data\\items.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                fromNewModule = (List<Item>)serializer.Deserialize(file, typeof(List<Item>));
            }

            foreach (Item iFNM in fromNewModule)
            {
                bool doAdd = true;
                foreach (Item iCurrent in toReturn)
                {
                    if (iFNM.resref == iCurrent.resref)
                    {
                        doAdd = false;
                        break;
                    }
                }
                if (doAdd)
                {
                    toReturn.Add(iFNM);
                }
            }
            return toReturn;
        }
        public Item getItem(string name)
        {
            foreach (Item it in itemsList)
            {
                if (it.name == name) return it;
            }
            return null;
        }
        public Item getItemByTag(string tag)
        {
            foreach (Item it in itemsList)
            {
                if (it.tag == tag) return it;
            }
            return null;
        }

        public void saveContainersFile(string filename)
        {
            string json = JsonConvert.SerializeObject(containersList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<Container> loadContainersFile(string filename)
        {
            List<Container> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Container>)serializer.Deserialize(file, typeof(List<Container>));
            }
            return toReturn;
        }
        public Container getContainer(string tag)
        {
            foreach (Container cont in containersList)
            {
                if (cont.containerTag == tag) return cont;
            }
            return null;
        }

        public void saveShopsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(shopsList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<Shop> loadShopsFile(string filename)
        {
            List<Shop> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Shop>)serializer.Deserialize(file, typeof(List<Shop>));
            }
            return toReturn;
        }
        public Shop getShopByTag(string tag)
        {
            foreach (Shop shp in shopsList)
            {
                if (shp.shopTag == tag)
                {
                    return shp;
                }
            }
            return null;
        }

        public void saveEncountersFile(string filename)
        {
            string json = JsonConvert.SerializeObject(encountersList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<Encounter> loadEncountersFile(string filename)
        {
            List<Encounter> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Encounter>)serializer.Deserialize(file, typeof(List<Encounter>));
            }
            return toReturn;
        }
        public Encounter getEncounter(string name)
        {
            foreach (Encounter encounter in encountersList)
            {
                if (encounter.encounterName == name) return encounter;
            }
            return null;
        }

        public void savePropsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(propsList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<Prop> loadPropsFile(string filename)
        {
            List<Prop> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Prop>)serializer.Deserialize(file, typeof(List<Prop>));
            }

            //try to also add all props from NewModule, but only if their tags are not already existing
            //openProps(_mainDirectory + "\\default\\NewModule\\data\\props.json");

            List<Prop> fromNewModule = null;
            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(_mainDirectory + "\\default\\NewModule\\data\\props.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                fromNewModule = (List<Prop>)serializer.Deserialize(file, typeof(List<Prop>));
            }

            foreach (Prop pFNM in fromNewModule)
            {
                bool doAdd = true;
                foreach (Prop pCurrent in toReturn)
                {
                    if (pFNM.PropTag == pCurrent.PropTag)
                    {
                        doAdd = false;
                        break;
                    }
                }
                if(doAdd)
                {
                    toReturn.Add(pFNM);
                }
            }

            return toReturn;
        }
        public Prop getPropByTag(string tag)
        {
            foreach (Prop it in propsList)
            {
                if (it.PropTag == tag) return it;
            }
            return null;
        }

        public void saveJournalFile(string filename)
        {
            string json = JsonConvert.SerializeObject(journal, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<JournalQuest> loadJournalFile(string filename)
        {
            List<JournalQuest> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<JournalQuest>)serializer.Deserialize(file, typeof(List<JournalQuest>));
            }
            return toReturn;
        }
        public JournalQuest getJournalCategoryByName(string name)
        {
            foreach (JournalQuest it in journal)
            {
                if (it.Name == name) return it;
            }
            return null;
        }
        public JournalQuest getJournalCategoryByTag(string tag)
        {
            foreach (JournalQuest it in journal)
            {
                if (it.Tag == tag) return it;
            }
            return null;
        }

        public void savePlayerClassesFile(string filename)
        {
            string json = JsonConvert.SerializeObject(playerClassesList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<PlayerClass> loadPlayerClassesFile(string filename)
        {
            List<PlayerClass> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<PlayerClass>)serializer.Deserialize(file, typeof(List<PlayerClass>));
            }
            return toReturn;
        }
        public PlayerClass getPlayerClassByTag(string tag)
        {
            foreach (PlayerClass ts in playerClassesList)
            {
                if (ts.tag == tag) return ts;
            }
            return null;
        }

        public void saveRacesFile(string filename)
        {
            string json = JsonConvert.SerializeObject(racesList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<Race> loadRacesFile(string filename)
        {
            List<Race> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Race>)serializer.Deserialize(file, typeof(List<Race>));
            }
            return toReturn;
        }
        public Race getRaceByTag(string tag)
        {
            foreach (Race ts in racesList)
            {
                if (ts.tag == tag) return ts;
            }
            return null;
        }

        public void saveSpellsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(spellsList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<Spell> loadSpellsFile(string filename)
        {
            List<Spell> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Spell>)serializer.Deserialize(file, typeof(List<Spell>));
            }
            return toReturn;
        }
        public Spell getSpellByTag(string tag)
        {
            foreach (Spell s in spellsList)
            {
                if (s.tag == tag) return s;
            }
            return null;
        }
        public Spell getSpellByName(string name)
        {
            foreach (Spell s in spellsList)
            {
                if (s.name == name) return s;
            }
            return null;
        }

        public void saveTraitsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(traitsList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }

        public void saveFactionsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(factionsList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }

        public List<Trait> loadTraitsFile(string filename)
        {
            List<Trait> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Trait>)serializer.Deserialize(file, typeof(List<Trait>));
            }
            return toReturn;
        }

        public List<Faction> loadFactionsFile(string filename)
        {
            List<Faction> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Faction>)serializer.Deserialize(file, typeof(List<Faction>));
            }
            return toReturn;
        }

        public Trait getTraitByTag(string tag)
        {
            foreach (Trait ts in traitsList)
            {
                if (ts.tag == tag) return ts;
            }
            return null;
        }
        public Trait getTraitByName(string name)
        {
            foreach (Trait ts in traitsList)
            {
                if (ts.name == name) return ts;
            }
            return null;
        }

        public Faction getFactionByTag(string tag)
        {
            foreach (Faction ts in factionsList)
            {
                if (ts.tag == tag) return ts;
            }
            return null;
        }
        public Faction getFactionByName(string name)
        {
            foreach (Faction ts in factionsList)
            {
                if (ts.name == name) return ts;
            }
            return null;
        }


        public void saveWeatherEffectsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(weatherEffectsList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<WeatherEffect> loadWeatherEffectsFile(string filename)
        {
            List<WeatherEffect> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<WeatherEffect>)serializer.Deserialize(file, typeof(List<WeatherEffect>));
            }
            return toReturn;
        }
        public WeatherEffect getWeatherEffectByTag(string tag)
        {
            foreach (WeatherEffect ts in weatherEffectsList)
            {
                if (ts.tag == tag) return ts;
            }
            return null;
        }
        public WeatherEffect getWeatherEffectByName(string name)
        {
            foreach (WeatherEffect ts in weatherEffectsList)
            {
                if (ts.name == name) return ts;
            }
            return null;
        }


        public void saveWeathersFile(string filename)
        {
            string json = JsonConvert.SerializeObject(weathersList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<Weather> loadWeathersFile(string filename)
        {
            List<Weather> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Weather>)serializer.Deserialize(file, typeof(List<Weather>));
            }
            return toReturn;
        }
        public Weather getWeatherByTag(string tag)
        {
            foreach (Weather ts in weathersList)
            {
                if (ts.tag == tag) return ts;
            }
            return null;
        }
        public Weather getWeatherByName(string name)
        {
            foreach (Weather ts in weathersList)
            {
                if (ts.name == name) return ts;
            }
            return null;
        }



        public void saveEffectsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(effectsList, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public List<Effect> loadEffectsFile(string filename)
        {
            List<Effect> toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (List<Effect>)serializer.Deserialize(file, typeof(List<Effect>));
            }
            return toReturn;
        }
        public Effect getEffectByTag(string tag)
        {
            foreach (Effect ef in effectsList)
            {
                if (ef.tag == tag) return ef;
            }
            return null;
        }
        #endregion

        //can delete this after the next update...use only once per module.
        private void copyTagsToResrefsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Creature crt in creaturesList)
            {
                crt.cr_resref = crt.cr_tag;
            }
            foreach (Encounter enc in encountersList)
            {
                foreach (CreatureRefs crtRef in enc.encounterCreatureRefsList)
                {
                    Creature crt = getCreatureByResRef(crtRef.creatureTag); //use tag because the old saves only had tag which is not the resref
                    crtRef.creatureTag = crt.cr_tag + "_" + this.mod.nextIdNumber;
                    crtRef.creatureResRef = crt.cr_resref;
                }
            }
        }
        private void copyItemTagsToResrefsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Item it in itemsList)
            {
                it.resref = it.tag;
            }
        }

        private void tsBtnResetDropDowns_Click(object sender, EventArgs e)
        {
            refreshDropDownLists();
            //note: this does nothing right now, must check later
            //migth be pure conversion routines?
            addPrefix();
        }

        private void tsBtnDataCheck_Click(object sender, EventArgs e)
        {
            DataCheck dc = new DataCheck(mod, this);
            dc.CheckAllData();
            dc = null;
        }

        //***********************************
        //note: this does nothing right now, must check later

        public void addPrefixToConvoNodeImage(ContentNode node)
        {  
             if ((node.NodePortraitBitmap != "") && (!node.NodePortraitBitmap.StartsWith("ptr_")))  
             {                  
                 string summaryReportPath = _mainDirectory + "\\modules\\" + mod.moduleName + "_convos.txt";  
                 File.AppendAllText(summaryReportPath, node.NodePortraitBitmap + Environment.NewLine);  
                 node.NodePortraitBitmap = "ptr_" + node.NodePortraitBitmap;  
             }  
            /*if (node.NodePortraitBitmap.StartsWith("ptr_"))  
 1900 +            {  
 1901 +                foundLinkedNodesIdList.Add(node.idNum);  
 1902 +            }*/  
             foreach (ContentNode subNode in node.subNodes)  
             {  
                 addPrefixToConvoNodeImage(subNode);  
             }  
               
         }  

         public void addPrefix()
         {  
             /*foreach (Convo c in mod.moduleConvoList)  
 1912 +            {  
 1913 +                if ((c.NpcPortraitBitmap != "") && (!c.NpcPortraitBitmap.StartsWith("ptr_")))  
 1914 +                {  
 1915 +                    string summaryReportPath = _mainDirectory + "\\modules\\" + mod.moduleName + "_convos.txt";  
 1916 +                    File.AppendAllText(summaryReportPath, c.NpcPortraitBitmap + Environment.NewLine);  
 1917 +                    c.NpcPortraitBitmap = "ptr_" + c.NpcPortraitBitmap;  
 1918 +                }  
 1919 +                addPrefixToConvoNodeImage(c.subNodes[0]);  
 1920 +            }*/  
               
             /*foreach (Area ar in mod.moduleAreasObjects)  
 1923 +            {  
 1924 +                for (int i = 0; i < ar.Layer1Filename.Count; i++)  
 1925 +                {  
 1926 +                    if (!ar.Layer1Filename[i].StartsWith("t_"))  
 1927 +                    {  
 1928 +                        ar.Layer1Filename[i] = "t_es_" + ar.Layer1Filename[i];  
 1929 +                    }  
 1930 +                }  
 1931 +                for (int i = 0; i < ar.Layer2Filename.Count; i++)  
 1932 +                {  
 1933 +                    if (!ar.Layer2Filename[i].StartsWith("t_"))  
 1934 +                    {  
 1935 +                        ar.Layer2Filename[i] = "t_es_" + ar.Layer2Filename[i];  
 1936 +                    }  
 1937 +                }  
 1938 +                for (int i = 0; i < ar.Layer3Filename.Count; i++)  
 1939 +                {  
 1940 +                    if (!ar.Layer3Filename[i].StartsWith("t_"))  
 1941 +                    {  
 1942 +                        ar.Layer3Filename[i] = "t_es_" + ar.Layer3Filename[i];  
 1943 +                    }  
 1944 +                }  
 1945 +            }  
 1946 +              
 1947 +            foreach (Encounter enc in mod.moduleEncountersList)  
 1948 +            {  
 1949 +                for (int i = 0; i < enc.Layer1Filename.Count; i++)  
 1950 +                {  
 1951 +                    if (!enc.Layer1Filename[i].StartsWith("t_"))  
 1952 +                    {  
 1953 +                        enc.Layer1Filename[i] = "t_es_" + enc.Layer1Filename[i];  
 1954 +                    }  
 1955 +                }  
 1956 +                for (int i = 0; i < enc.Layer2Filename.Count; i++)  
 1957 +                {  
 1958 +                    if (!enc.Layer2Filename[i].StartsWith("t_"))  
 1959 +                    {  
 1960 +                        enc.Layer2Filename[i] = "t_es_" + enc.Layer2Filename[i];  
 1961 +                    }  
 1962 +                }  
 1963 +                for (int i = 0; i < enc.Layer3Filename.Count; i++)  
 1964 +                {  
 1965 +                    if (!enc.Layer3Filename[i].StartsWith("t_"))  
 1966 +                    {  
 1967 +                        enc.Layer3Filename[i] = "t_es_" + enc.Layer3Filename[i];  
 1968 +                    }  
 1969 +                }  
 1970 +            }*/  
         }  


        //***************************************

        private void mergerEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergerEditor mergerEdit = new MergerEditor(mod, this);
            mergerEdit.ShowDialog();
        }

        private void tsBtnIBScriptEditor_Click(object sender, EventArgs e)
        {

        }

        private void rulesEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RulesEditor rulesEdit = new RulesEditor(mod, this);
            rulesEdit.ShowDialog();
        }

        private void tilesUsedInModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Area areaObj = new Area();
            List<string> tilenames = new List<string>();
            //iterate over each area and add tile names to tilenames if not already contained
            foreach (string ar in mod.moduleAreasList)
            {
                // try and load the file selected if it exists
                string g_directory = this._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas";
                if (File.Exists(g_directory + "\\" + ar + ".lvl"))
                {
                    try
                    {
                        areaObj = areaObj.loadAreaFile(g_directory + "\\" + ar + ".lvl");
                        foreach (Tile t in areaObj.Tiles)
                        {
                            if (!tilenames.Contains(t.Layer1Filename)) { tilenames.Add(t.Layer1Filename); }
                            if (!tilenames.Contains(t.Layer2Filename)) { tilenames.Add(t.Layer2Filename); }
                            if (!tilenames.Contains(t.Layer3Filename)) { tilenames.Add(t.Layer3Filename); }
                            if (!tilenames.Contains(t.Layer4Filename)) { tilenames.Add(t.Layer4Filename); }
                            if (!tilenames.Contains(t.Layer5Filename)) { tilenames.Add(t.Layer5Filename); }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("failed to open file: " + ex.ToString());
                    }                    
                }                
            }
            
            //iterate over each encounter and add tile names to List<string> if not already contained
            foreach (Encounter enc in encountersList)
            {
                foreach (TileEnc t in enc.encounterTiles)
                {
                    if (!tilenames.Contains(t.Layer1Filename)) { tilenames.Add(t.Layer1Filename); }
                    if (!tilenames.Contains(t.Layer2Filename)) { tilenames.Add(t.Layer2Filename); }
                    if (!tilenames.Contains(t.Layer3Filename)) { tilenames.Add(t.Layer3Filename); }
                }
            }
            //write out list to a file 'tiles_used.txt', one tile per line
            if (Directory.Exists(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\tiles\\tiles_used"))
            {
                try
                {
                    //delete folder and all contents first then create a clean folder to fill
                    Directory.Delete(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\tiles\\tiles_used", true);
                    createDirectory(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\tiles\\tiles_used");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete 'tiles_used' folder and then create a new version: " + ex.ToString());
                }
            }  
            else
            {
                try
                {
                    createDirectory(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\tiles\\tiles_used");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to create the folder 'tiles_used': " + ex.ToString());
                }
            }
            try
            {
                tilenames.Sort();
                File.WriteAllLines(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\tiles\\tiles_used\\00_tiles_used_list.txt", tilenames);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create a text file with list of tiles used: " + ex.ToString());
            }
            //create a folder 'tiles_used' and copy the tiles into it
            foreach (string s in tilenames)
            {
                try
                {
                    File.Copy(
                        this._mainDirectory + "\\modules\\" + mod.moduleName + "\\tiles\\" + s + ".png",
                        this._mainDirectory + "\\modules\\" + mod.moduleName + "\\tiles\\tiles_used\\" + s + ".png",
                        true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to copy file: s  " + ex.ToString());
                }
            }

            MessageBox.Show("A list of tiles and the tile files have been copied to a folder called 'tiles_used' in your " +
                            "module's 'tiles' folder. If the 'tiles_used' folder existed before, it was deleted first and then updated " + 
                            "to the currently used tiles in your module.");
        }
    }
}
