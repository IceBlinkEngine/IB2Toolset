using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace IB2Toolset
{
    public partial class AreaForm : DockContent
    {
        public ParentForm prntForm;

        public AreaForm(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }

        #region Area Stuff
        public void refreshListBoxAreas()
        {
            //if (lbxAreas.Items.Count < 1)
            //{
            lbxAreas.BeginUpdate();
            lbxAreas.DataSource = null;
            lbxAreas.DataSource = prntForm.mod.moduleAreasList;
            lbxAreas.EndUpdate();
            //}
        }
        private void lbxAreas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if ((lbxAreas.Items.Count > 0) && (lbxAreas.SelectedIndex >= 0))
            {
                int index = this.lbxAreas.IndexFromPoint(e.Location);

                if (index != System.Windows.Forms.ListBox.NoMatches)
                {

                    //MessageBox.Show(index.ToString());
                    //do your stuff here
                    //prntForm._selectedLbxAreaIndex = index;
                    EditArea();
                }
            }
        }
        private void lbxAreas_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //MessageBox.Show("listBox selected index changed");
            if (lbxAreas.SelectedIndex >= 0)
            {
                prntForm._selectedLbxAreaIndex = lbxAreas.SelectedIndex;
                txtAreaName.Text = prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
                lbxAreas.SelectedIndex = prntForm._selectedLbxAreaIndex;                
            }
            //refreshListBoxAreas();
            //refreshLbxItems();
        }
        private void txtAreaName_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex] = txtAreaName.Text;
                refreshListBoxAreas();
            }
            catch { }
        }
        private void btnAddArea_Click_1(object sender, EventArgs e)
        {
            //thinktank1
            Area newArea = new Area();
            newArea.Filename = "new area";
            prntForm.mod.moduleAreasList.Add(newArea.Filename);
            refreshListBoxAreas();
            // should I create a new file at this point?
            
        }
        private void btnRemoveArea_Click_1(object sender, EventArgs e)
        {
            if ((lbxAreas.Items.Count > 0) && (lbxAreas.SelectedIndex >= 0))
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxAreas.SelectedIndex;

                    //remove linked areas from master area numbers and areas lists
                    Area newAreaLink = new Area();
                    Area newAreaMaster = new Area();
                    string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas";
                    newAreaLink = newAreaLink.loadAreaFile(filePath + "\\" + prntForm.mod.moduleAreasList[selectedIndex] + ".lvl");
                    foreach (string areaName in prntForm.mod.moduleAreasList)
                    {
                        if (areaName == newAreaLink.masterOfThisArea)
                        {
                            //find master area index
                            int index = -1;
                            for (int k = 0; k < prntForm.mod.moduleAreasList.Count; k++)
                            {
                                if (prntForm.mod.moduleAreasList[k] == newAreaLink.masterOfThisArea)
                                {
                                    index = k;
                                    break;
                                }
                            }

                            //get the master area
                            newAreaMaster = newAreaMaster.loadAreaFile(filePath + "\\" + prntForm.mod.moduleAreasList[index] + ".lvl");

                            //modify its link lists
                            foreach (int linkNumber in newAreaMaster.linkNumbers)
                            {
                                if (linkNumber == newAreaLink.linkNumberOfThisArea)
                                {
                                    newAreaMaster.linkNumbers.Remove(linkNumber);
                                }
                            }  

                            for (int l = newAreaMaster.linkedAreas.Count-1; l >= 0; l--)
                            //foreach (string linkName in newAreaMaster.linkedAreas)
                            {
                                if (newAreaMaster.linkedAreas[l] == newAreaLink.Filename)
                                {
                                    newAreaMaster.linkedAreas.RemoveAt(l);
                                }
                            }
                            break;
                        }
                    }

                    for (int m = prntForm.mod.masterAreasList.Count -1; m >= 0; m--)
                    {
                        if (prntForm.mod.masterAreasList[m] == prntForm.mod.moduleAreasList[selectedIndex])
                        {
                            prntForm.mod.masterAreasList.RemoveAt(m);
                            break;
                        }
                    }

                    prntForm.mod.moduleAreasList.RemoveAt(selectedIndex);
                }
                catch
                {
                    int c = 1;
                }
                prntForm._selectedLbxAreaIndex = 0;
                lbxAreas.SelectedIndex = 0;
                refreshListBoxAreas();
            }
        }
        private void btnEditArea_Click_1(object sender, EventArgs e)
        {
            if ((lbxAreas.Items.Count > 0) && (lbxAreas.SelectedIndex >= 0))
            {
                EditArea();                
            }
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            if ((lbxAreas.Items.Count > 0) && (lbxAreas.SelectedIndex >= 0))
            {
                RenameDialog newName = new RenameDialog();
                DialogResult result = newName.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        #region New Area
                        if (prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex] == "new area")
                        {
                            //if file exists, rename the file
                            string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas";
                            if (File.Exists(filePath + "\\" + prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex] + ".lvl"))
                            {
                                try
                                {
                                    //rename file
                                    File.Move(filePath + "\\" + prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex] + ".lvl", filePath + "\\" + newName.RenameText + ".lvl"); // Try to move
                                    try
                                    {
                                        //load area
                                        Area newArea = new Area();
                                        newArea = newArea.loadAreaFile(filePath + "\\" + newName.RenameText + ".lvl");
                                        if (newArea == null)
                                        {
                                            MessageBox.Show("returned a null area");
                                        }
                                        //change area file name in area file object properties
                                        newArea.Filename = newName.RenameText;
                                        newArea.saveAreaFile(filePath + "\\" + newName.RenameText + ".lvl");
                                        prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex] = newName.RenameText;
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
                                prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex] = newName.RenameText;
                            }
                            refreshListBoxAreas();
                        }
                        #endregion
                        #region Existing Area
                        else
                        {
                            DialogResult sure = MessageBox.Show("Are you sure you wish to change the area name and the area file name? (make sure to update any references to this area name such as transitions and scripts)", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            if (sure == System.Windows.Forms.DialogResult.Yes)
                            {
                                //if file exists, rename the file
                                string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas";
                                if (File.Exists(filePath + "\\" + prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex] + ".lvl"))
                                {
                                    try
                                    {
                                        //rename file
                                        File.Move(filePath + "\\" + prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex] + ".lvl", filePath + "\\" + newName.RenameText + ".lvl"); // Try to move
                                        try
                                        {
                                            //load area
                                            Area newArea = new Area();
                                            newArea = newArea.loadAreaFile(filePath + "\\" + newName.RenameText + ".lvl");
                                            if (newArea == null)
                                            {
                                                MessageBox.Show("returned a null area");
                                            }
                                            //change area file name in area file object properties
                                            newArea.Filename = newName.RenameText;
                                            newArea.saveAreaFile(filePath + "\\" + newName.RenameText + ".lvl");
                                            prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex] = newName.RenameText;
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
                                    prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex] = newName.RenameText;
                                }
                                refreshListBoxAreas();
                            }                            
                        }
                        #endregion
                    }
                    catch { }
                }
            }
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            prntForm.mod.moduleAreasList = prntForm.mod.moduleAreasList.OrderBy(o => o).ToList();
            refreshListBoxAreas();
        }
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if ((lbxAreas.Items.Count > 0) && (lbxAreas.SelectedIndex >= 0))
            {
                //if file exists, rename the file
                string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas";
                string filename = prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
                if (File.Exists(filePath + "\\" + filename + ".lvl"))
                {
                    try
                    {
                        //rename file
                        File.Copy(filePath + "\\" + filename + ".lvl", filePath + "\\" + filename + "-Copy.lvl");
                        try
                        {
                            //load area
                            Area newArea = new Area();
                            newArea = newArea.loadAreaFile(filePath + "\\" + filename + "-Copy.lvl");
                            if (newArea == null)
                            {
                                MessageBox.Show("returned a null area");
                            }
                            //change area file name in area file object properties
                            newArea.Filename = filename + "-Copy";
                            newArea.saveAreaFile(filePath + "\\" + filename + "-Copy.lvl");

                            prntForm.mod.moduleAreasList.Add(newArea.Filename);
                            refreshListBoxAreas();
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
                refreshListBoxAreas();
            }
        }        
        #endregion

        public void EditArea()
        {
            //if (prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex].StartsWith("wm_"))
            //{
                WorldMapEditor newChild = new WorldMapEditor(prntForm.mod, prntForm); //add new child
                newChild.Text = prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
                newChild.Show(prntForm.dockPanel1); //as new form created so that corresponding tab and child form is active
                refreshListBoxAreas();
                newChild.g_directory = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas";
                newChild.g_filename = prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
            /*}
            else
            {
                LevelEditor newChild = new LevelEditor(prntForm.mod, prntForm); //add new child
                newChild.Text = prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
                newChild.Show(prntForm.dockPanel1); //as new form created so that corresponding tab and child form is active
                refreshListBoxAreas();
                newChild.g_directory = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas";
                newChild.g_filename = prntForm.mod.moduleAreasList[prntForm._selectedLbxAreaIndex];
            }*/
        }

        private void btnLoadAllArea_Click(object sender, EventArgs e)
        {
            string jobDir = "";
            jobDir = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\areas";
            prntForm.mod.moduleAreasList.Clear();
            foreach (string f in Directory.GetFiles(jobDir, "*.lvl"))
            {
                string filename = Path.GetFileNameWithoutExtension(f);
                prntForm.mod.moduleAreasList.Add(filename);
            }
            refreshListBoxAreas();
        }
    }
}
