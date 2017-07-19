using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using IceBlinkCore;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace IB2Toolset
{
    public partial class IBScriptForm : DockContent
    {
        public ParentForm prntForm;

        public IBScriptForm(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }

        #region IBScript Stuff
        public void refreshListBoxIBScripts()
        {
            lbxIBScripts.BeginUpdate();
            lbxIBScripts.DataSource = null;
            //try drawing from default module here already
            //caesar
            //************************************************
            try
            {
                List<string> tempList = new List<string>();

                string jobDir = prntForm._mainDirectory + "\\default\\" + "NewModule" + "\\ibscript";

                foreach (string f in Directory.GetFiles(jobDir, "*.ibs"))
                {
                    string filename = Path.GetFileNameWithoutExtension(f);
                    tempList.Add(filename);
                }

                foreach (string sTemp in tempList)
                {
                    if (!prntForm.mod.moduleIBScriptsList.Contains(sTemp))
                    {
                        prntForm.mod.moduleIBScriptsList.Add(sTemp);
                    }
                }
            }
            catch
            { }

                        //************************************************
                        lbxIBScripts.DataSource = prntForm.mod.moduleIBScriptsList;
            lbxIBScripts.EndUpdate();            

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            prntForm.mod.moduleIBScriptsList.Add("newIBScript");
            refreshListBoxIBScripts();
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if ((lbxIBScripts.Items.Count > 0) && (lbxIBScripts.SelectedIndex >= 0))
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxIBScripts.SelectedIndex;
                    prntForm.mod.moduleIBScriptsList.RemoveAt(selectedIndex);
                }
                catch { }
                prntForm._selectedLbxIBScriptIndex = 0;
                lbxIBScripts.SelectedIndex = 0;
                refreshListBoxIBScripts();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((lbxIBScripts.Items.Count > 0) && (lbxIBScripts.SelectedIndex >= 0))
                {
                    EditIBScript();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed: " + ex.ToString());
                //prntForm.game.errorLog("failed: " + ex.ToString());
            }
        }
        private void lbxIBScripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxIBScripts.SelectedIndex >= 0)
            {
                prntForm._selectedLbxIBScriptIndex = lbxIBScripts.SelectedIndex;
                lbxIBScripts.SelectedIndex = prntForm._selectedLbxIBScriptIndex;
            }
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            if ((lbxIBScripts.Items.Count > 0) && (lbxIBScripts.SelectedIndex >= 0))
            {
                string scriptname = prntForm.mod.moduleIBScriptsList[prntForm._selectedLbxIBScriptIndex];
                RenameDialog newName = new RenameDialog();
                DialogResult result = newName.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        #region New IB Script
                        if (scriptname == "newIBScript")
                        {
                            //if file exists, rename the file
                            string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscript";
                            if (File.Exists(filePath + "\\" + scriptname + ".ibs"))
                            {
                                try
                                {
                                    //rename file
                                    File.Move(filePath + "\\" + scriptname + ".ibs", filePath + "\\" + newName.RenameText + ".ibs");
                                    try
                                    {
                                        prntForm.mod.moduleIBScriptsList[prntForm._selectedLbxIBScriptIndex] = newName.RenameText;
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
                                prntForm.mod.moduleIBScriptsList[prntForm._selectedLbxIBScriptIndex] = newName.RenameText;
                            }
                            refreshListBoxIBScripts();
                        }
                        #endregion
                        #region Existing IB Script
                        else
                        {
                            DialogResult sure = MessageBox.Show("Are you sure you wish to change this IB Script's file name? (make sure to update any references to this LogicTree name such as script hooks and trigger events)", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            if (sure == System.Windows.Forms.DialogResult.Yes)
                            {
                                //if file exists, rename the file
                                string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscript";
                                if (File.Exists(filePath + "\\" + scriptname + ".ibs"))
                                {
                                    try
                                    {
                                        //rename file
                                        File.Move(filePath + "\\" + scriptname + ".ibs", filePath + "\\" + newName.RenameText + ".ibs");
                                        try
                                        {                                            
                                            prntForm.mod.moduleIBScriptsList[prntForm._selectedLbxIBScriptIndex] = newName.RenameText;
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
                                    prntForm.mod.moduleIBScriptsList[prntForm._selectedLbxIBScriptIndex] = newName.RenameText;
                                }
                                refreshListBoxIBScripts();
                            }
                        }
                        #endregion
                    }
                    catch { }
                }
            }
        }
        private void lbxIBScripts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbxIBScripts.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                //do your stuff here
                try
                {
                    if ((lbxIBScripts.Items.Count > 0) && (lbxIBScripts.SelectedIndex >= 0))
                    {
                        EditIBScript();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("failed: " + ex.ToString());
                }
            }
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            prntForm.mod.moduleIBScriptsList = prntForm.mod.moduleIBScriptsList.OrderBy(o => o).ToList();
            refreshListBoxIBScripts();
        }
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            /*TODO
            if ((lbxIBScripts.Items.Count > 0) && (lbxIBScripts.SelectedIndex >= 0))
            {
                //if file exists, rename the file
                string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscript";
                string filename = prntForm.mod.moduleIBScriptsList[prntForm._selectedLbxIBScriptIndex];
                if (File.Exists(filePath + "\\" + filename + ".ibs"))
                {
                    try
                    {
                        //rename file
                        File.Copy(filePath + "\\" + filename + ".ibs", filePath + "\\" + filename + "-Copy.ibs"); // Try to move
                        try
                        {
                            //load convo
                            LogicTree newConvo = new LogicTree();
                            newConvo = newConvo.GetLogicTree(filePath, "\\" + filename + "-Copy.json");
                            if (newConvo == null)
                            {
                                MessageBox.Show("returned a null LogicTree");
                            }
                            //change convo file name in convo file object properties
                            newConvo.Filename = filename + "-Copy";
                            newConvo.SaveLogicTree(filePath, "\\" + filename + "-Copy.json");
                            prntForm.mod.moduleLogicTreesList.Add(newConvo.Filename);
                            refreshListBoxIBScripts();
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
                    MessageBox.Show("File: " + filename + ".json does not exist in the dialog folder");
                }
                refreshListBoxIBScripts();
            }*/
        }
        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            try
            {
                string jobDir = "";
                jobDir = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscript";
                prntForm.mod.moduleIBScriptsList.Clear();
                foreach (string f in Directory.GetFiles(jobDir, "*.ibs"))
                {
                    string filename = Path.GetFileNameWithoutExtension(f);
                    prntForm.mod.moduleIBScriptsList.Add(filename);
                }

                //second routine for retrieving ibscripts from default folder and adding hem, too, if they do not alreday exist
                //Note: load and save actions for ibscripts will need to be reworked the same way
                try
                {
                    List<string> tempList = new List<string>();

                    jobDir = prntForm._mainDirectory + "\\default\\" + "NewModule" + "\\ibscript";

                    foreach (string f in Directory.GetFiles(jobDir, "*.ibs"))
                    {
                        string filename = Path.GetFileNameWithoutExtension(f);
                        tempList.Add(filename);
                    }

                    foreach (string sTemp in tempList)
                    {
                        if (!prntForm.mod.moduleIBScriptsList.Contains(sTemp))
                        {
                            prntForm.mod.moduleIBScriptsList.Add(sTemp);
                            //***************************************************
                            IBScriptEditor newChild = new IBScriptEditor(prntForm.mod, prntForm); //add new child
                            newChild.Text = prntForm.mod.moduleIBScriptsList[prntForm._selectedLbxIBScriptIndex];
                            newChild.Show(prntForm.dockPanel1);  //as new form created so that corresponding tab and child form is active
                            refreshListBoxIBScripts();
                            newChild.filename = prntForm.mod.moduleIBScriptsList[prntForm._selectedLbxIBScriptIndex] + ".ibs";
                            if (!File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscript\\" + newChild.filename))
                            {
                                newChild.SaveScript();
                            }


                            //***************************************************
                        }
                    }
                }
                catch
                {

                }

                refreshListBoxIBScripts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }      
        #endregion 

        private void EditIBScript()
        {
            IBScriptEditor newChild = new IBScriptEditor(prntForm.mod, prntForm); //add new child
            newChild.Text = prntForm.mod.moduleIBScriptsList[prntForm._selectedLbxIBScriptIndex];
            newChild.Show(prntForm.dockPanel1);  //as new form created so that corresponding tab and child form is active
            refreshListBoxIBScripts();
            newChild.filename = prntForm.mod.moduleIBScriptsList[prntForm._selectedLbxIBScriptIndex] + ".ibs";
            //try loading from default
            try
            {
                if (!File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscript\\" + newChild.filename))
                {
                    newChild.LoadScriptDefault();
                }
                else
                {
                    newChild.LoadScript();
                }
            }
            catch
            { }

            if (!File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\ibscript\\" + newChild.filename))
            {
                newChild.SaveScript();
            }

        }
    }
}
