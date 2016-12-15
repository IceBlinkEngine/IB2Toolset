using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using ScintillaNET;

namespace IB2Toolset
{
    public partial class IBScriptEditor : DockContent
    { 
        public Module mod;
        public ParentForm prntForm;
        public string filename = ""; //example: coolscript.ibs
       
        //this is used to define keywords for the initial dropdown on starting to type in anew line
        public string keywordMain1 = "break continue else end endif for gosub goto if label msg debug next return subroutine %Mod %Area " +
                                            "%Encounter %Prop %Player %CreatureResRef %CreatureInCurrentEncounter #numModAreas #numModEncounters " +
                                            "#numAreaProps #numPlayers #numPlayersInRoster #numPartyInventoryItems";
        
        //this is used to color the contained keywords red(?) 
        public string keywordMain2 = "Mod Area Encounter Prop Player CreatureResRef CreatureInCurrentEncounter";
        
        //DISABLED (using lists below for this, too); the keywords in this block are used for the drop down popping up after a "."
        //public string objKeywordMod = "WorldTime PlayerLocationX PlayerLocationY partyGold showPartyToken partyTokenFilename selectedPartyLeader " +
                                            //"indexOfPCtoLastUseItem OnHeartBeatLogicTree OnHeartBeatParms";
        //public string objKeywordPlayer = "combatLocX combatLocY charStatus baseStr baseDex baseInt baseCha hp sp XP";
        //public string objKeywordProp = "LocationX LocationY isShown isActive ConversationWhenOnPartySquare EncounterWhenOnPartySquare isMover isChaser " +
                                            //"MoverType";
        
        
        //these are used for available selection in the right hand side big drop down box choosing commands and properties by category
        public List<string> basicCommands = new List<string>() { "for", "break", "continue", "next", "if", "else", "endif", "gosub", "subroutine", "return", "goto", "label", "msg", "debug", "end" };
        public List<string> moduleProperties = new List<string>() { "WorldTime", "TimePerRound", "PlayerLocationX", "PlayerLocationY", "PlayerLastLocationX", "PlayerLastLocationY", "partyGold", "showPartyToken", "partyTokenFilename", "selectedPartyLeader", "indexOfPCtoLastUseItem", "combatAnimationSpeed", "OnHeartBeatIBScript", "OnHeartBeatIBScriptParms", "debugMode", "allowSave", "PlayerFacingLeft", "SizeOfModuleItemsList", "SizeOfModuleEncountersLists", "SizeOfModuleContainersList", "SizeOfModuleShopsList", "SizeOfModuleCreaturesList", "SizeOfModuleJournal", "SizeOfModulePlayerClassLists", "SizeOfModuleRacesList", "SizeOfModuleSpellsList", "SizeOfModuleTraitsList", "SizeOfModuleEffectsList", "SizeOfModuleAreasList", "SizeOfModuleConvosList", "SizeOfModuleAreasObjects", "SizeOfModuleGlobalInts", "SizeOfModuleGlobalStrings", "SizeOfModuleConvoSavedValuesList", "SizeOfPlayerList", "SizeOfPartyRosterList", "SizeOfPartyInventoryRefsList", "SizeOfPartyJournalQuests", "SizeOfPartyJournalCompleted"};
        public List<string> playerProperties = new List<string>() { "hp", "sp", "combatLocX", "combatLocY", "moveDistance", "moveOrder", "classLevel", "baseFortitude", "baseWill", "baseReflex", "fortitude", "will", "reflex", "strength", "dexterity", "intelligence", "charisma", "wisdom", "constitution", "baseStr", "baseDex", "baseInt", "baseCha", "baseWis", "baseCon", "ACBase", "AC", "baseAttBonus", "hpMax", "spMax", "XP", "XPNeeded", "hpRegenTimePassedCounter", "spRegenTimePassedCounter", "damageTypeResistanceTotalAcid", "damageTypeResistanceTotalCold", "damageTypeResistanceTotalNormal", "damageTypeResistanceTotalElectricity", "damageTypeResistanceTotalFire", "damageTypeResistanceTotalMagic", "damageTypeResistanceTotalPoison", "SizeOfKnownSpellsTags", "SizeOfKnownSpellsList", "SizeOfKnownTraitsTags", "SizeOfKnownTraitsList", "SizeOfEffectsList", "combatFacingLeft", "steathModeOn", "mainPc", "nonRemoveablePc", "isMale", "tokenFilename", "name", "tag", "raceTag", "classTag", "HeadRefsTag", "HeadRefsName", "HeadRefsResRef", "HeadRefsCanNotBeUnequipped", "HeadRefsQuantity", "NeckRefsTag", "NeckRefsName", "NeckRefsResRef", "NeckRefsCanNotBeUnequipped", "NeckRefsQuantity", "BodyRefsTag", "BodyRefsName", "BodyRefsResRef", "BodyRefsCanNotBeUnequipped", "BodyRefsQuantity", "MainHandRefsTag", "MainHandRefsName", "MainHandRefsResRef", "MainHandRefsCanNotBeUnequipped", "MainHandRefsQuantity", "OffHandRefsTag", "OffHandRefsName", "OffHandRefsResRef", "OffHandRefsCanNotBeUnequipped", "OffHandRefsQuantity", "RingRefsTag", "RingRefsName", "RingRefsResRef", "RingRefsCanNotBeUnequipped", "RingRefsQuantity", "Ring2RefsTag", "Ring2RefsName", "Ring2RefsResRef", "Ring2RefsCanNotBeUnequipped", "Ring2RefsQuantity", "FeetRefsTag", "FeetRefsName", "FeetRefsResRef", "FeetRefsCanNotBeUnequipped", "FeetRefsQuantity", "AmmoRefsTag", "AmmoRefsName", "AmmoRefsResRef", "AmmoRefsCanNotBeUnequipped", "AmmoRefsQuantity" };
        public List<string> propProperties = new List<string>() { "PropTag", "LocationX", "LocationY", "PropFacingLeft", "HasCollision", "isShown", "isActive", "DeletePropWhenThisEncounterIsWon", "PostLocationX", "PostLocationY", "WayPointListCurrentIndex", "isMover", "ChanceToMove2Squares", "ChanceToMove0Squares", "isChaser", "isCurrentlyChasing", "ChaserDetectRangeRadius", "ChaserGiveUpChasingRangeRadius", "ChaserChaseDuration", "ChaserStartChasingTime", "RandomMoverRadius", "ReturningToPost", "ImageFileName", "MouseOverText", "PropCategoryName", "ConversationWhenOnPartySquare", "EncounterWhenOnPartySquare", "MoverType", "SizeOfPropLocalInts", "SizeOfPropLocalStrings", "SizeOfWayPointList", "OnHeartBeatIBScript", "OnHeartBeatIBScriptParms", "unavoidableConversation" };
        public List<string> creatureInCurrentEncounterProperties = new List<string>() { "cr_tokenFilename", "combatFacingLeft", "combatLocX", "combatLocY", "moveDistance", "cr_name", "cr_tag", "cr_resref", "cr_desc", "cr_level", "hpMax", "sp", "hp", "cr_XP", "AC", "cr_status", "cr_att", "cr_attRange", "cr_damageNumDice", "cr_damageDie", "cr_damageAdder", "cr_category", "cr_projSpriteFilename", "cr_spriteEndingFilename", "cr_attackSound", "cr_numberOfAttacks", "cr_ai", "fortitude", "will", "reflex", "damageTypeResistanceValueAcid", "damageTypeResistanceValueNormal", "damageTypeResistanceValueCold", "damageTypeResistanceValueElectricity", "damageTypeResistanceValueFire", "damageTypeResistanceValueMagic", "damageTypeResistanceValuePoison", "cr_typeOfDamage", "onScoringHit", "onScoringHitParms", "onDeathIBScript", "onDeathIBScriptParms", "SizeOfKnownSpellsTags", "SizeOfCr_effectsList", "SizeOfCreatureLocalInts", "SizeOfCreatureLocalStrings" };
        public List<string> creatureResRefProperties = new List<string>() { "creatureResRef", "creatureTag", "creatureStartLocationX", "creatureStartLocationY"};
        public List<string> areaProperties = new List<string>() { "SizeOfProps", "Filename", "UseMiniMapFogOfWar", "areaDark", "UseDayNightCycle", "TimePerSquare", "MusicFileName", "ImageFileName", "MapSizeX", "MapSizeY", "AreaMusic", "AreaSounds", "SizeOfTriggers", "SizeOFAreaLocalInts", "SizeOfAreaLocalStrings", "OnHeartBeatIBScript", "OnHeartBeatIBScriptParms", "inGameAreaName" };
        public List<string> encounterProperties = new List<string>() { "encounterName", "MapImage", "UseMapImage", "UseDayNightCycle", "MapSizeX", "MapSizeY", "goldDrop", "AreaMusic", "AreaMusicDelay", "AreaMusicDelayRandomAdder", "OnSetupCombatIBScript", "OnSetupCombatIBScriptParms", "OnStartCombatRoundIBScript", "OnStartCombatRoundIBScriptParms", "OnStartCombatTurnIBScript", "OnStartCombatTurnIBScriptParms", "OnEndCombatIBScript", "OnEndCombatIBScriptParms", "SizeOfEncounterTiles", "SizeOfEncounterCreatureRefsList", "SizeOfEncounterCreatureList", "SizeOfEncounterInventoryRefsList", "SizeOfEncounterPcStartLocations"};
        //public List<string> currentEncounterProperties = new List<string>() { "encounterName", "MapImage", "UseMapImage", "UseDayNightCycle", "MapSizeX", "MapSizeY", "goldDrop", "AreaMusic", "AreaMusicDelay", "AreaMusicDelayRandomAdder", "OnSetupCombatIBScript", "OnSetupCombatIBScriptParms", "OnStartCombatRoundIBScript", "OnStartCombatRoundIBScriptParms", "OnStartCombatTurnIBScript", "OnStartCombatTurnIBScriptParms", "OnEndCombatIBScript", "OnEndCombatIBScriptParms", "SizeOfEncounterTiles", "SizeOfEncounterCreatureRefsList", "SizeOfEncounterCreatureList", "SizeOfEncounterInventoryRefsList", "SizeOfEncounterPcStartLocations" };
        public List<string> currentEncounterProperties = new List<string>() { "encounterName", "MapImage", "UseMapImage", "UseDayNightCycle", "MapSizeX", "MapSizeY", "goldDrop", "triggerScriptCalledFromSquareLocX", "triggerScriptCalledFromSquareLocY", "AreaMusic", "AreaMusicDelay", "AreaMusicDelayRandomAdder", "OnSetupCombatIBScript", "OnSetupCombatIBScriptParms", "OnStartCombatRoundIBScript", "OnStartCombatRoundIBScriptParms", "OnStartCombatTurnIBScript", "OnStartCombatTurnIBScriptParms", "OnEndCombatIBScript", "OnEndCombatIBScriptParms", "SizeOfEncounterTiles", "SizeOfEncounterCreatureRefsList", "SizeOfEncounterCreatureList", "SizeOfEncounterInventoryRefsList", "SizeOfEncounterPcStartLocations" };            




        public IBScriptEditor(Module m, ParentForm p)
        {
            InitializeComponent();
            mod = m;
            prntForm = p;

            // Set the lexer
            scintilla1.Lexer = Lexer.Cpp;

            // we have common to every lexer style saves time.
            scintilla1.StyleResetDefault();
            scintilla1.Styles[Style.Default].Font = "Courier";
            scintilla1.Styles[Style.Default].Size = 10;
            scintilla1.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            scintilla1.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            scintilla1.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            scintilla1.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
            scintilla1.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Cpp.Word2].ForeColor = Color.Red;
            scintilla1.Styles[Style.Cpp.String].ForeColor = Color.Teal;
            scintilla1.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
            scintilla1.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
            scintilla1.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
            scintilla1.Lexer = Lexer.Cpp;

            // Set the keywords
            scintilla1.SetKeywords(0, keywordMain1);
            scintilla1.SetKeywords(1, keywordMain2);

            scintilla1.Margins[0].Width = 35;

            // Display whitespace in orange
            scintilla1.WhitespaceSize = 2;
            scintilla1.ViewWhitespace = WhitespaceMode.VisibleAlways;
            scintilla1.SetWhitespaceForeColor(true, Color.Orange);

            //turn autosorting on of autocomplete lists (I think) 
            scintilla1.AutoCOrder = Order.PerformSort;

            #region Folding Settings
            // Instruct the lexer to calculate folding
            scintilla1.SetProperty("fold", "1");
            scintilla1.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            scintilla1.Margins[2].Type = MarginType.Symbol;
            scintilla1.Margins[2].Mask = Marker.MaskFolders;
            scintilla1.Margins[2].Sensitive = true;
            scintilla1.Margins[2].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                scintilla1.Markers[i].SetForeColor(SystemColors.ControlLightLight);
                scintilla1.Markers[i].SetBackColor(SystemColors.ControlDark);
            }

            // Configure folding markers with respective symbols
            scintilla1.Markers[Marker.Folder].Symbol = MarkerSymbol.BoxPlus;
            scintilla1.Markers[Marker.FolderOpen].Symbol = MarkerSymbol.BoxMinus;
            scintilla1.Markers[Marker.FolderEnd].Symbol = MarkerSymbol.BoxPlusConnected;
            scintilla1.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            scintilla1.Markers[Marker.FolderOpenMid].Symbol = MarkerSymbol.BoxMinusConnected;
            scintilla1.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            scintilla1.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            scintilla1.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
            #endregion

            cmbFunctions.SelectedIndex = 0;
        }
        
        public void LoadScript()
        {
            try
            {
                string path = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscript";
                string text = File.ReadAllText(path + "\\" + filename);
                scintilla1.AppendText(text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not load IBScript file " + filename + " from disk. Error: " + ex.Message);
            }
        }
        private void tsSaveScript_Click(object sender, EventArgs e)
        {
            SaveScript();
        }
        public void SaveScript()
        {
            try
            {
                string path = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscript";
                File.WriteAllText(path + "\\" + filename, scintilla1.Text, Encoding.ASCII);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not save IBScript file " + filename + " to disk. Error: " + ex.Message);
            }
        }
        
        //this calls the drop selection selction when a "." is typed in the script editor window
        private void scintilla1_CharAdded(object sender, CharAddedEventArgs e)
        {
            if (e.Char == '.')
            {
                var currentPos = scintilla1.CurrentPosition;
                var wordStartPos = scintilla1.WordStartPosition(currentPos, true);

                var lenEntered = currentPos - wordStartPos;
                //this.Text = "lenEntered = currentPos - wordStartPos: " + lenEntered + "=" + currentPos + "-" + wordStartPos;

                Timer t = new Timer();

                t.Interval = 10;
                t.Tag = scintilla1;
                t.Tick += new EventHandler((obj, ev) =>
                { 
                    string keyword = scintilla1.GetTextRange(currentPos - 40, 40);
                    string[] words = keyword.Split('%'); 
                    keyword = words[words.Length-1];
                    
                    //IMPORTANT: If one keyword is fully contaiend in another one, check the longer keyword first

                    if (keyword.StartsWith("CreatureResRef"))
                    {
                        //we add the full path here, including the name(s) of the list(s) that can be referenced in the for-loop header with SizeOfListName
                        List<string> extentedPropProperties = new List<string>();
                        extentedPropProperties = creatureResRefProperties.ConvertAll(d => ("{@j}.Encounter[@i]." + d));
                        string selection = "";
                        foreach (string s in extentedPropProperties)
                        {
                            selection = selection + s + " ";
                        }

                        scintilla1.AutoCShow(lenEntered, selection);
                    }
                    else if (keyword.StartsWith("CreatureInCurrentEncounter"))
                    {
                        //we add the full path here, including the name(s) of the list(s) that can be referenced in the for-loop header with SizeOfListName
                        List<string> extentedPropProperties = new List<string>();
                        extentedPropProperties = creatureInCurrentEncounterProperties.ConvertAll(d => ("[@i]." + d));
                        string selection = "";
                        foreach (string s in extentedPropProperties)
                        {
                            selection = selection + s + " ";
                        }

                        scintilla1.AutoCShow(lenEntered, selection);
                    }
                    else if (keyword.StartsWith("CurrentEncounter"))
                    {
                        //we add the full path here, including the name(s) of the list(s) that can be referenced in the for-loop header with SizeOfListName
                        List<string> extentedPropProperties = new List<string>();
                        extentedPropProperties = currentEncounterProperties.ConvertAll(d => ("[@i]." + d));
                        string selection = "";
                        foreach (string s in extentedPropProperties)
                        {
                            selection = selection + s + " ";
                        }

                        scintilla1.AutoCShow(lenEntered, selection);
                    }

                    else if (keyword.StartsWith("Encounter"))
                    {
                        //we add the full path here, including the name(s) of the list(s) that can be referenced in the for-loop header with SizeOfListName
                        List<string> extentedPropProperties = new List<string>();
                        extentedPropProperties = encounterProperties.ConvertAll(d => ("[@i]." + d));
                        string selection = "";
                        foreach (string s in extentedPropProperties)
                        {
                            selection = selection + s + " ";
                        }

                        scintilla1.AutoCShow(lenEntered, selection);
                    }
                    
                    else if (keyword.StartsWith("Mod"))
                    {
                        //we add the full path here, including the name(s) of the list(s) that can be referenced in the for-loop header with SizeOfListName
                        //List<string> extentedPropProperties = new List<string>();
                        //extentedPropProperties = moduleProperties.ConvertAll(d => ("" + d));
                        string selection = "";
                        foreach (string s in moduleProperties)
                        {
                            selection = selection + s + " ";
                        }
                        
                        scintilla1.AutoCShow(lenEntered, selection);
                    }
                    else if (keyword.StartsWith("Player"))
                    {
                        //we add the full path here, including the name(s) of the list(s) that can be referenced in the for-loop header with SizeOfListName
                        List<string> extentedPropProperties = new List<string>();
                        extentedPropProperties = playerProperties.ConvertAll(d => ("[@i]." + d));
                        string selection = "";
                        foreach (string s in extentedPropProperties)
                        {
                            selection = selection + s + " ";
                        }

                        scintilla1.AutoCShow(lenEntered, selection);
                    }
                    else if (keyword.StartsWith("Prop"))
                    {
                        //we add the full path here, including the name(s) of the list(s) that can be referenced in the for-loop header with SizeOfListName
                        List<string> extentedPropProperties = new List<string>();
                        extentedPropProperties = propProperties.ConvertAll(d => ("{@j}.Area[@i]." + d));
                        string selection ="";
                        foreach (string s in extentedPropProperties)
                        {
                            selection = selection + s + " ";
                        }

                        scintilla1.AutoCShow(lenEntered, selection);
                    }
                    else if (keyword.StartsWith("Area"))
                    {
                        //we add the full path here, including the name(s) of the list(s) that can be referenced in the for-loop header with SizeOfListName
                        List<string> extentedPropProperties = new List<string>();
                        extentedPropProperties = areaProperties.ConvertAll(d => ("[@i]." + d));
                        string selection = "";
                        foreach (string s in extentedPropProperties)
                        {
                            selection = selection + s + " ";
                        }

                        scintilla1.AutoCShow(lenEntered, selection);
                    }
                

                    t.Stop();
                    t.Enabled = false;
                    t.Dispose();
                });
                t.Start();

            }
            else
            {
                var currentPos = scintilla1.CurrentPosition;
                var wordStartPos = scintilla1.WordStartPosition(currentPos, false);

                var lenEntered = currentPos - wordStartPos;
                //this.Text = "lenEntered = currentPos - wordStartPos: " + lenEntered + "=" + currentPos + "-" + wordStartPos;


                if (lenEntered > 0)
                {
                    scintilla1.AutoCShow(lenEntered, keywordMain1);
                }
            }
        }
        private void tsShowWhiteSpace_Click(object sender, EventArgs e)
        {
            if (scintilla1.ViewWhitespace == WhitespaceMode.VisibleAlways)
            {
                scintilla1.ViewWhitespace = WhitespaceMode.Invisible;
            }
            else
            {
                scintilla1.ViewWhitespace = WhitespaceMode.VisibleAlways;
            }
        }
        private void loadScriptText(string scriptFilename)
        {
            //load script into rtxt for browsing
            string jobDir = "";
            if (prntForm.mod.moduleName != "NewModule")
            {
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\scripts\\" + scriptFilename))
                {
                    jobDir = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\scripts";
                }
                else if (File.Exists(prntForm._mainDirectory + "\\default\\NewModule\\scripts\\" + scriptFilename))
                {
                    jobDir = prntForm._mainDirectory + "\\default\\NewModule\\scripts";
                }
                else
                {
                    prntForm.logText("couldn't find the script file." + Environment.NewLine);
                    //prntForm.game.errorLog("couldn't find the script file.");
                }
            }
            else
            {
                //jobDir = prntForm._mainDirectory + "\\data\\scripts";
            }
            try
            {
                rtxtScript.LoadFile(jobDir + "\\" + scriptFilename, RichTextBoxStreamType.PlainText);
            }
            catch (Exception ex)
            {
                prntForm.logText("Failed to preview script of " + scriptFilename + Environment.NewLine);
                //prntForm.game.errorLog("failed to preview script of selected row: " + ex.ToString());
            }
        }
        private void loadExamplesText(string exampleFilename)
        {
            //load script into rtxt for browsing
            string jobDir = "";
            if (prntForm.mod.moduleName != "NewModule")
            {
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscriptexamples\\" + exampleFilename))
                {
                    jobDir = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscriptexamples";
                }
                else if (File.Exists(prntForm._mainDirectory + "\\default\\NewModule\\ibscriptexamples\\" + exampleFilename))
                {
                    jobDir = prntForm._mainDirectory + "\\default\\NewModule\\ibscriptexamples";
                }
                else
                {
                    prntForm.logText("couldn't find the example file." + Environment.NewLine);
                    //prntForm.game.errorLog("couldn't find the script file.");
                }
            }
            else
            {
                //jobDir = prntForm._mainDirectory + "\\data\\scripts";
            }
            try
            {
                rtxtScript.LoadFile(jobDir + "\\" + exampleFilename, RichTextBoxStreamType.PlainText);
            }
            catch (Exception ex)
            {
                prntForm.logText("Failed to preview example of " + exampleFilename + Environment.NewLine);
                //prntForm.game.errorLog("failed to preview script of selected row: " + ex.ToString());
            }
        }
        private void cmbFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshLbxFunctions();            
        }
        
        //this fills the left hand side drop down list with the properties (for each category)
        private void refreshLbxFunctions()
        {
            /*~ga~gc~og~os
            BasicCommands
            ModuleProperties
            PlayerProperties
            PropProperties
            CreatureInCurrentEncounter
            CreatureResRef
            Area
            Encounter
            CurrentEncounter
             */


            lbxFunctions.BeginUpdate();
            lbxFunctions.DataSource = null;
            if (cmbFunctions.SelectedIndex == 0)
            {
                lbxFunctions.DataSource = prntForm.scriptList;
                //lbxFunctions.DisplayMember = "name";
            }
            else if (cmbFunctions.SelectedIndex == 1)
            {
                lbxFunctions.DataSource = basicCommands;
                //lbxFunctions.DisplayMember = "name";
            }

            //module
            else if (cmbFunctions.SelectedIndex == 2)
            {
                lbxFunctions.DataSource = moduleProperties;
                //lbxFunctions.DisplayMember = "name";
            }
            
            //player
            else if (cmbFunctions.SelectedIndex == 3)
            {
                lbxFunctions.DataSource = playerProperties;
                //lbxFunctions.DisplayMember = "name";
            }

            //prop
            else if (cmbFunctions.SelectedIndex == 4)
            {
                lbxFunctions.DataSource = propProperties;
                //lbxFunctions.DisplayMember = "name";
            }

            //CreatureInCurrentEncounter
            else if (cmbFunctions.SelectedIndex == 5)
            {
                lbxFunctions.DataSource = creatureInCurrentEncounterProperties;
                //lbxFunctions.DisplayMember = "name";
            }

            //CreatureResRef
            else if (cmbFunctions.SelectedIndex == 6)
            {
                lbxFunctions.DataSource = creatureResRefProperties;
                //lbxFunctions.DisplayMember = "name";
            }

            //Area
            else if (cmbFunctions.SelectedIndex == 7)
            {
                lbxFunctions.DataSource = areaProperties;
                //lbxFunctions.DisplayMember = "name";
            }
            
            //Encounter
            else if (cmbFunctions.SelectedIndex == 8)
            {
                lbxFunctions.DataSource = encounterProperties;
                //lbxFunctions.DisplayMember = "name";
            }

            //CurrentEncounter
            else if (cmbFunctions.SelectedIndex == 9)
            {
                lbxFunctions.DataSource = currentEncounterProperties;
                //lbxFunctions.DisplayMember = "name";
            }

            lbxFunctions.EndUpdate();
        }

        private void lbxFunctions_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                //right clcik also selects the clicked line and uses its content
                lbxFunctions.SelectedIndex = lbxFunctions.IndexFromPoint(e.X, e.Y);
                
                string text = lbxFunctions.SelectedItem.ToString();
                int wantedPosition = scintilla1.CurrentPosition;

                
                //player
                if (cmbFunctions.SelectedIndex == 3)
                {
                    text = "\n" + "for (@i = 0; @i < %Mod.SizeOfPlayerList; @i ++)" + "\n" + "   %Player[@i]." + text + "\n" + "next" + "\n";
                    wantedPosition += text.Length;
                    
                }
                //prop
                else if (cmbFunctions.SelectedIndex == 4)
                {
                    text = "\n" + "for (@i = 0; @i < %Mod.SizeOfModuleAreasObjects; @i ++)" + "\n" + "   for (@j = 0; @j < %Area[@i].SizeOfProps; @j ++)" + "\n" + "      %Prop{@j}.Area[@i]." + text + "\n" + "   next" + "\n" + "next" + "\n";
                    wantedPosition += text.Length;
                }

                //CreatureInCurrentEncounter
                else if (cmbFunctions.SelectedIndex == 5)
                {
                    text = "\n" + "for (@i = 0; @i <  %CurrentEncounter.SizeOfEncounterCreatureList; @i ++)" + "\n" + "   %CreatureInCurrentEncounter[@i]." + text + "\n" + "next" + "\n";
                    wantedPosition += text.Length;
                }

                //CreatureResRef
                else if (cmbFunctions.SelectedIndex == 6)
                {
                    text = "\n" + "for (@i = 0; @i < %Mod.SizeOfModuleEncountersList; @i ++)" + "\n" + "   for (@j = 0; @j < %Encounter[@i].SizeOfEncounterCreatureRefsList; @j ++)" + "\n" + "      %CreatureResRef{@j}.Encounter[@i]." + text + "\n" + "   next" + "\n" + "next" + "\n";
                    wantedPosition += text.Length;
                }
                    
                //Area
                else if (cmbFunctions.SelectedIndex == 7)
                {
                    text = "\n" + "for (@i = 0; @i < %Mod.SizeOfModuleAreasObjects; @i ++)" + "\n" + "   %Area[@i]." + text + "\n" + "next" + "\n";
                    wantedPosition += text.Length;
                }

                //Encounter
                else if (cmbFunctions.SelectedIndex == 8)
                {
                    text = "\n" + "for (@i = 0; @i < %Mod.SizeOfModuleEncountersList; @i ++)" + "\n" + "   %Encounter[@i]." + text + "\n" + "next" + "\n";
                    wantedPosition += text.Length;
                }
                //CurrentEncounter
                else if (cmbFunctions.SelectedIndex == 9)
                {
                    text = "";
                }
                else
                {
                    text = "";
                }

                scintilla1.InsertText(scintilla1.CurrentPosition, text);
                scintilla1.GotoPosition(wantedPosition);
                scintilla1.Focus();
            }
        }

        //this defines what's copied into the script text on double click in the drop down list
        private void lbxFunctions_MouseDoubleClick(object sender, MouseEventArgs e)
        {

                string text = lbxFunctions.SelectedItem.ToString();
                int wantedPosition = scintilla1.CurrentPosition;

                if (cmbFunctions.SelectedIndex == 0) //a ga_ or gc_ etc.
                {
                    //remove the .cs
                    text = text.Substring(0, text.Length - 3);
                    //add "~" at the beginning and "()" to the end
                    text = "~" + text + "()";
                    wantedPosition += text.Length;
                }

                //basic commands
                else if (cmbFunctions.SelectedIndex == 1)
                {
                    if (text == "for")
                    {
                        text = "\n" + "for (@i = 0; @i < XXX; @i ++)" + "\n" + "\n" + "next" + "\n";
                        wantedPosition += text.Length;
                    }
                    else if (text == "if")
                    {
                        text = "\n" + "if (XXX)" + "\n" + "\n" + "endif" + "\n";
                        wantedPosition += text.Length;
                    }
                    else if (text == "msg")
                    {
                        text = "\n" + "msg (\"XXX\")" + "\n";
                        wantedPosition += text.Length;
                    }
                    else if (text == "goto")
                    {
                        text = "\n" + "goto XXX" + "\n" + "\n" + "label XXX" + "\n";
                        wantedPosition += text.Length;
                    }
                    else if (text == "gosub")
                    {
                        text = "\n" + "gosub XXX" + "\n" + "\n" + "subroutine XXX" + "\n" + "\n" + "return" + "\n";
                        wantedPosition += text.Length;
                    }
                    else if (text == "debug")
                    {
                        text = "\n" + "debug (\"XXX\")" + "\n";
                        wantedPosition += text.Length;
                    }
                }

                //module
                else if (cmbFunctions.SelectedIndex == 2)
                {
                    text = "%Mod." + text;
                    wantedPosition += text.Length;
                }

                //player
                else if (cmbFunctions.SelectedIndex == 3)
                {
                    text = "%Player[@i]." + text;
                    wantedPosition += text.Length;
                }

                //prop
                else if (cmbFunctions.SelectedIndex == 4)
                {
                    text = "%Prop{@j}.Area[@i]." + text;
                    wantedPosition += text.Length;
                }

                //CreatureInCurrentEncounter
                else if (cmbFunctions.SelectedIndex == 5)
                {
                    text = "%CreatureInCurrentEncounter[@i]." + text;
                    wantedPosition += text.Length;
                }

                //CreatureResRef
                else if (cmbFunctions.SelectedIndex == 6)
                {
                    text = "%CreatureResRef{@j}.Encounter[@i]." + text;
                    wantedPosition += text.Length;
                }

                //Area
                else if (cmbFunctions.SelectedIndex == 7)
                {
                    text = "%Area[@i]." + text;
                    wantedPosition += text.Length;
                }

                //Encounter
                else if (cmbFunctions.SelectedIndex == 8)
                {
                    text = "%Encounter[@i]." + text;
                    wantedPosition += text.Length;
                }
                //CurrentEncounter
                else if (cmbFunctions.SelectedIndex == 9)
                {
                    text = "%CurrentEncounter." + text;
                    wantedPosition += text.Length;
                }

                scintilla1.InsertText(scintilla1.CurrentPosition, text);
                scintilla1.GotoPosition(wantedPosition);
                scintilla1.Focus();
        }

        //this is for filling the info text box at tooslet bottom
        private void lbxFunctions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxFunctions.SelectedItem == null)
            {
                return;
            }
            if (cmbFunctions.SelectedIndex == 0) //a ga_ or gc_ etc.
            {
                loadScriptText(lbxFunctions.SelectedItem.ToString());
            }
            else if (cmbFunctions.SelectedIndex > 0)
            {
                loadExamplesText(lbxFunctions.SelectedItem.ToString() + ".txt");
            }
        }

        private void tsUndo_Click(object sender, EventArgs e)
        {
            scintilla1.Undo();            
        }

        private void tsRedo_Click(object sender, EventArgs e)
        {
            scintilla1.Redo();
        }
    }
}
