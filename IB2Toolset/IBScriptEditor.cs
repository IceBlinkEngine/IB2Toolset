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
        public string keywordMain1 = "break continue else end endif for gosub goto if label msg debug next return subroutine %Mod[] %ModArea[] " +
                                            "%ModEncounter[] %AreaProp[] %Player[] %Roster[] %PartyInventoryItem[] #numModAreas #numModEncounters " +
                                            "#numAreaProps #numPlayers #numPlayersInRoster #numPartyInventoryItems";
        public string keywordMain2 = "Mod currentArea currentEncounter ModArea ModEncounter AreaProp Player Roster PartyInventoryItem";
        public string objKeywordMod = "WorldTime PlayerLocationX PlayerLocationY partyGold showPartyToken partyTokenFilename selectedPartyLeader " +
                                            "indexOfPCtoLastUseItem OnHeartBeatLogicTree OnHeartBeatParms";
        public string objKeywordPlayer = "combatLocX combatLocY charStatus baseStr baseDex baseInt baseCha hp sp XP";
        public string objKeywordProp = "LocationX LocationY isShown isActive ConversationWhenOnPartySquare EncounterWhenOnPartySquare isMover isChaser " +
                                            "MoverType";
        public List<string> basicCommands = new List<string>() { "break", "continue", "else", "end", "endif", "for", "gosub", "goto", "if", "label", "msg", "debug", "next", "return", "subroutine" };


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
                    string keyword = scintilla1.GetTextRange(currentPos - 10, 10);
                    if (keyword.Contains("Player["))
                    {
                        scintilla1.AutoCShow(lenEntered, objKeywordPlayer);
                    }
                    else if (keyword.Contains("AreaProp["))
                    {
                        scintilla1.AutoCShow(lenEntered, objKeywordProp);
                    }
                    else if (keyword.Contains("Mod["))
                    {
                        scintilla1.AutoCShow(lenEntered, objKeywordMod);
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
        private void refreshLbxFunctions()
        {
            /*~ga~gc~og~os
            BasicCommands
            ModuleProperties
            PlayerProperties
            PropProperties*/


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
            lbxFunctions.EndUpdate();
        }
        private void lbxFunctions_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string text = lbxFunctions.SelectedItem.ToString();
            if (cmbFunctions.SelectedIndex == 0) //a ga_ or gc_ etc.
            {
                //remove the .cs
                text = text.Substring(0, text.Length - 3);
                //add "~" at the beginning and "()" to the end
                text = "~" + text + "()";
            }
            scintilla1.InsertText(scintilla1.CurrentPosition, text);
        }

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
            else if (cmbFunctions.SelectedIndex == 1) 
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
