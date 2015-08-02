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
    /*public partial class LogicTreeForm : DockContent
    {
        public ParentForm prntForm;

        public LogicTreeForm(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }

        #region LogicTree Stuff
        public void refreshListBoxLogicTrees()
        {
            lbxLogicTrees.BeginUpdate();
            lbxLogicTrees.DataSource = null;
            lbxLogicTrees.DataSource = prntForm.mod.moduleLogicTreesList;
            lbxLogicTrees.EndUpdate();            
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            LogicTree newConvo = new LogicTree();
            newConvo.Filename = "newLogicTree";
            prntForm.mod.moduleLogicTreesList.Add(newConvo.Filename);
            refreshListBoxLogicTrees();
            // should I create a new file at this point?
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if ((lbxLogicTrees.Items.Count > 0) && (lbxLogicTrees.SelectedIndex >= 0))
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxLogicTrees.SelectedIndex;
                    prntForm.mod.moduleLogicTreesList.RemoveAt(selectedIndex);
                }
                catch { }
                prntForm._selectedLbxLogicTreeIndex = 0;
                lbxLogicTrees.SelectedIndex = 0;
                refreshListBoxLogicTrees();
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if ((lbxLogicTrees.Items.Count > 0) && (lbxLogicTrees.SelectedIndex >= 0))
                {
                    EditLogicTree();                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed: " + ex.ToString());
                //prntForm.game.errorLog("failed: " + ex.ToString());
            }
        }
        private void lbxLogicTrees_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxLogicTrees.SelectedIndex >= 0)
            {
                prntForm._selectedLbxLogicTreeIndex = lbxLogicTrees.SelectedIndex;
                //txtConvoName.Text = prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex];
                lbxLogicTrees.SelectedIndex = prntForm._selectedLbxLogicTreeIndex;
            }
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            if ((lbxLogicTrees.Items.Count > 0) && (lbxLogicTrees.SelectedIndex >= 0))
            {
                RenameDialog newName = new RenameDialog();
                DialogResult result = newName.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        #region New Logic Tree
                        if (prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] == "newLogicTree")
                        {
                            //if file exists, rename the file
                            string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\logictree";
                            if (File.Exists(filePath + "\\" + prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] + ".json"))
                            {
                                try
                                {
                                    //rename file
                                    File.Move(filePath + "\\" + prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] + ".json", filePath + "\\" + newName.RenameText + ".json"); // Try to move
                                    try
                                    {
                                        //load area
                                        LogicTree newConvo = new LogicTree();
                                        newConvo = newConvo.GetLogicTree(filePath, "\\" + newName.RenameText + ".json");
                                        if (newConvo == null)
                                        {
                                            MessageBox.Show("returned a null LogicTree");
                                        }
                                        //change area file name in area file object properties
                                        newConvo.Filename = newName.RenameText;
                                        newConvo.SaveLogicTree(filePath, "\\" + newName.RenameText + ".json");
                                        prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] = newName.RenameText;
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
                                prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] = newName.RenameText;
                            }
                            refreshListBoxLogicTrees();
                        }
                        #endregion
                        #region Existing Logic Tree
                        else
                        {
                            DialogResult sure = MessageBox.Show("Are you sure you wish to change the Logic Tree name and the LogicTree file name? (make sure to update any references to this LogicTree name such as script hooks and trigger events)", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            if (sure == System.Windows.Forms.DialogResult.Yes)
                            {
                                //if file exists, rename the file
                                string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\logictree";
                                if (File.Exists(filePath + "\\" + prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] + ".json"))
                                {
                                    try
                                    {
                                        //rename file
                                        File.Move(filePath + "\\" + prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] + ".json", filePath + "\\" + newName.RenameText + ".json"); // Try to move
                                        try
                                        {
                                            //load convo
                                            LogicTree newConvo = new LogicTree();
                                            newConvo = newConvo.GetLogicTree(filePath, "\\" + newName.RenameText + ".json");
                                            if (newConvo == null)
                                            {
                                                MessageBox.Show("returned a null LogicTree");
                                            }
                                            //change convo file name in convo file object properties
                                            newConvo.Filename = newName.RenameText;
                                            newConvo.SaveLogicTree(filePath, "\\" + newName.RenameText + ".json");
                                            prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] = newName.RenameText;
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
                                    prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] = newName.RenameText;
                                }
                                refreshListBoxLogicTrees();
                            }
                        }
                        #endregion
                    }
                    catch { }
                }
            }
        }
        private void lbxLogicTrees_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbxLogicTrees.IndexFromPoint(e.Location);

            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                //do your stuff here
                try
                {
                    if ((lbxLogicTrees.Items.Count > 0) && (lbxLogicTrees.SelectedIndex >= 0))
                    {
                        EditLogicTree();
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
            prntForm.mod.moduleLogicTreesList = prntForm.mod.moduleLogicTreesList.OrderBy(o => o).ToList();
            refreshListBoxLogicTrees();
        }
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if ((lbxLogicTrees.Items.Count > 0) && (lbxLogicTrees.SelectedIndex >= 0))
            {
                //if file exists, rename the file
                string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\logictree";
                string filename = prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex];
                if (File.Exists(filePath + "\\" + filename + ".json"))
                {
                    try
                    {
                        //rename file
                        File.Copy(filePath + "\\" + filename + ".json", filePath + "\\" + filename + "-Copy.json"); // Try to move
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
                            refreshListBoxLogicTrees();
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
                refreshListBoxLogicTrees();
            }
        }
        private void btnLoadAll_Click(object sender, EventArgs e)
        {
            string jobDir = "";
            jobDir = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\logictree";
            prntForm.mod.moduleLogicTreesList.Clear();
            foreach (string f in Directory.GetFiles(jobDir, "*.json"))
            {
                string filename = Path.GetFileNameWithoutExtension(f);
                prntForm.mod.moduleLogicTreesList.Add(filename);
            }
            refreshListBoxLogicTrees();
        }      
        #endregion 

        private void EditLogicTree()
        {
            LogicTreeEditor newChild = new LogicTreeEditor(prntForm.mod, prntForm); //add new child
            newChild.Text = prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex];
            newChild.Show(prntForm.dockPanel1);  //as new form created so that corresponding tab and child form is active
            refreshListBoxLogicTrees();
            newChild.ce_filename = prntForm.mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] + ".json";
            newChild.saveConvoFile();
        }        
    }*/
}
