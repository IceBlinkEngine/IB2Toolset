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
            openSpells(_mainDirectory + "\\default\\NewModule\\data\\spells.json");
            openTraits(_mainDirectory + "\\default\\NewModule\\data\\traits.json");
            openWeatherEffects(_mainDirectory + "\\default\\NewModule\\data\\weatherEffects.json");
            openWeathers(_mainDirectory + "\\default\\NewModule\\data\\weathers.json");
            openEffects(_mainDirectory + "\\default\\NewModule\\data\\effects.json");
            //game.errorLog("Starting IceBlink Toolset");
            saveAsTemp();

            //fill all lists
            DropdownStringLists.aiTypeStringList = new List<string> { "BasicAttacker", "GeneralCaster" };
            DropdownStringLists.damageTypeStringList = new List<string> { "Normal", "Acid", "Cold", "Electricity", "Fire", "Magic", "Poison" };
            DropdownStringLists.itemTypeStringList = new List<string> { "Head", "Neck", "Armor", "Ranged", "Melee", "General", "Ring", "Shield", "Feet", "Ammo" };
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
                openWeatherEffects(directory + "\\data\\weatherEffects.json");
                openWeathers(directory + "\\data\\weathers.json");
                openEffects(directory + "\\data\\effects.json");
                refreshDropDownLists();
                this.Text = "IceBlink 2 Toolset - " + mod.moduleLabelName;
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
                    try
                    {
                        a.saveAreaFile(this._mainDirectory + "\\modules\\" + mod.moduleName + "\\areas\\" + a.Filename + ".lvl");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not save area file to disk. Original error: " + ex.Message);
                    }
                }
            }
            catch { MessageBox.Show("failed to createFiles"); }
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
        }

        private void tsBtnDataCheck_Click(object sender, EventArgs e)
        {
            DataCheck dc = new DataCheck(mod, this);
            dc.CheckAllData();
            dc = null;
        }

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
