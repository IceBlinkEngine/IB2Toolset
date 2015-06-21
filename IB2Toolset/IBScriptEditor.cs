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
        public string kwMain1 = "break continue else end endif for gosub goto if label msg next return subroutine %Mod[] %ModArea[] %ModEncounter[] %AreaProp[] %Player[] %Roster[] %PartyInventory[] #numModArea #numModEncounters #numAreaProps #numPlayers #numRosters #numPartyInventory";
        public string kwMain2 = "Mod currentArea currentEncounter ModArea ModEncounter AreaProp Player Roster PartyInventory";
        public string okwMod = "WorldTime PlayerLocationX PlayerLocationY partyGold showPartyToken partyTokenFilename selectedPartyLeader indexOfPCtoLastUseItem OnHeartBeatLogicTree OnHeartBeatParms";
        public string okwPlayer = "combatLocX combatLocY charStatus baseStr baseDex baseInt baseCha hp sp XP";
        public string okwProp = "LocationX LocationY isShown isActive ConversationWhenOnPartySquare EncounterWhenOnPartySquare isMover isChaser MoverType";


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
            scintilla1.SetKeywords(0, kwMain1);
            scintilla1.SetKeywords(1, kwMain2);

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

            //read in script file and create line numbered list
            //lines = File.ReadAllLines("script.txt");
            //List<string> converttolist = lines.ToList();
            //converttolist.Insert(0, "//line 0");
            //lines = converttolist.ToArray();
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
                        scintilla1.AutoCShow(lenEntered, okwPlayer);
                    }
                    else if (keyword.Contains("AreaProp["))
                    {
                        scintilla1.AutoCShow(lenEntered, okwProp);
                    }
                    else if (keyword.Contains("Mod["))
                    {
                        scintilla1.AutoCShow(lenEntered, okwMod);
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
                    scintilla1.AutoCShow(lenEntered, kwMain1);
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
    }
}
