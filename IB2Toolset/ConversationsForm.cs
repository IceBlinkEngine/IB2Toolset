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
    public partial class ConversationsForm : DockContent
    {
        public ParentForm prntForm;

        public ConversationsForm(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }

        #region Conversation Stuff
        public void refreshListBoxConvos()
        {
            //if (lbxAreas.Items.Count < 1)
            //{
            lbxConvos.BeginUpdate();
            lbxConvos.DataSource = null;
            lbxConvos.DataSource = prntForm.mod.moduleConvosList;
            lbxConvos.EndUpdate();
            //}           
            //DropdownStringLists.convoStringList = new List<string>();
            //DropdownStringLists.convoStringList.Add("none");
            //foreach (string s in prntForm.mod.ModuleConvosList)
            //{
            //    DropdownStringLists.convoStringList.Add(s);
            //}
        }
        private void txtConvoName_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] = txtConvoName.Text;
                refreshListBoxConvos();
                //containersList.containers[_selectedLbx1Index].containerName = txtName.Text;
                //refreshListBox1();
            }
            catch { }
        }
        private void btnAddConvo_Click_1(object sender, EventArgs e)
        {
            Convo newConvo = new Convo();
            newConvo.ConvoFileName = "new conversation";
            prntForm.mod.moduleConvosList.Add(newConvo.ConvoFileName);
            refreshListBoxConvos();
            // should I create a new file at this point?
        }
        private void btnRemoveConvo_Click_1(object sender, EventArgs e)
        {
            if ((lbxConvos.Items.Count > 0) && (lbxConvos.SelectedIndex >= 0))
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxConvos.SelectedIndex;
                    prntForm.mod.moduleConvosList.RemoveAt(selectedIndex);
                }
                catch { }
                prntForm._selectedLbxConvoIndex = 0;
                lbxConvos.SelectedIndex = 0;
                refreshListBoxConvos();
            }
        }
        private void btnEditConvo_Click_1(object sender, EventArgs e)
        {
            try
            {
                if ((lbxConvos.Items.Count > 0) && (lbxConvos.SelectedIndex >= 0))
                {
                    EditConvo();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed: " + ex.ToString());
                //prntForm.game.errorLog("failed: " + ex.ToString());
            }
        }
        private void lbxConvos_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            //MessageBox.Show("listBox selected index changed");
            if (lbxConvos.SelectedIndex >= 0)
            {
                prntForm._selectedLbxConvoIndex = lbxConvos.SelectedIndex;
                txtConvoName.Text = prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex];
                lbxConvos.SelectedIndex = prntForm._selectedLbxConvoIndex;
            }
            //refreshListBoxAreas();
            //refreshLbxItems();
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            if ((lbxConvos.Items.Count > 0) && (lbxConvos.SelectedIndex >= 0))
            {
                RenameDialog newName = new RenameDialog();
                DialogResult result = newName.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    /*try
                    {
                        prntForm.mod.ModuleConvosList[prntForm._selectedLbxConvoIndex] = newName.RenameText;
                        refreshListBoxConvos();
                    }
                    catch { }
                    */
                    try
                    {
                        #region New Convo
                        if (prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] == "new conversation")
                        {
                            //if file exists, rename the file
                            string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\dialog";
                            if (File.Exists(filePath + "\\" + prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] + ".json"))
                            {
                                try
                                {
                                    //rename file
                                    File.Move(filePath + "\\" + prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] + ".json", filePath + "\\" + newName.RenameText + ".json"); // Try to move
                                    try
                                    {
                                        //load area
                                        Convo newConvo = new Convo();
                                        newConvo = newConvo.GetConversation(filePath, "\\" + newName.RenameText + ".json");
                                        if (newConvo == null)
                                        {
                                            MessageBox.Show("returned a null convo");
                                        }
                                        //change area file name in area file object properties
                                        newConvo.ConvoFileName = newName.RenameText;
                                        newConvo.SaveContentConversation(filePath, "\\" + newName.RenameText + ".json");
                                        prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] = newName.RenameText;
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
                                prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] = newName.RenameText;
                            }
                            refreshListBoxConvos();
                        }
                        #endregion
                        #region Existing Convo
                        else
                        {
                            DialogResult sure = MessageBox.Show("Are you sure you wish to change the conversation name and the conversation file name? (make sure to update any references to this conversation name such as creature attached convo name and scripts)", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                            if (sure == System.Windows.Forms.DialogResult.Yes)
                            {
                                //if file exists, rename the file
                                string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\dialog";
                                if (File.Exists(filePath + "\\" + prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] + ".json"))
                                {
                                    try
                                    {
                                        //rename file
                                        File.Move(filePath + "\\" + prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] + ".json", filePath + "\\" + newName.RenameText + ".json"); // Try to move
                                        try
                                        {
                                            //load convo
                                            Convo newConvo = new Convo();
                                            newConvo = newConvo.GetConversation(filePath, "\\" + newName.RenameText + ".json");
                                            if (newConvo == null)
                                            {
                                                MessageBox.Show("returned a null convo");
                                            }
                                            //change convo file name in convo file object properties
                                            newConvo.ConvoFileName = newName.RenameText;
                                            newConvo.SaveContentConversation(filePath, "\\" + newName.RenameText + ".json");
                                            prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] = newName.RenameText;
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
                                    prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] = newName.RenameText;
                                }
                                refreshListBoxConvos();
                            }
                        }
                        #endregion
                    }
                    catch { }
                }
            }
        }
        private void lbxConvos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbxConvos.IndexFromPoint(e.Location);

            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                //MessageBox.Show(index.ToString());
                //do your stuff here
                //prntForm._selectedLbxConvoIndex = index;
                try
                {
                    if ((lbxConvos.Items.Count > 0) && (lbxConvos.SelectedIndex >= 0))
                    {
                        EditConvo();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("failed: " + ex.ToString());
                    //prntForm.game.errorLog("failed: " + ex.ToString());
                }
            }
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            prntForm.mod.moduleConvosList = prntForm.mod.moduleConvosList.OrderBy(o => o).ToList();
            refreshListBoxConvos();
        }
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if ((lbxConvos.Items.Count > 0) && (lbxConvos.SelectedIndex >= 0))
            {
                //if file exists, rename the file
                string filePath = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\dialog";
                string filename = prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex];
                if (File.Exists(filePath + "\\" + filename + ".json"))
                {
                    try
                    {
                        //rename file
                        File.Copy(filePath + "\\" + filename + ".json", filePath + "\\" + filename + "-Copy.json"); // Try to move
                        try
                        {
                            //load convo
                            Convo newConvo = new Convo();
                            newConvo = newConvo.GetConversation(filePath, "\\" + filename + "-Copy.json");
                            if (newConvo == null)
                            {
                                MessageBox.Show("returned a null convo");
                            }
                            //change convo file name in convo file object properties
                            newConvo.ConvoFileName = filename + "-Copy";
                            newConvo.SaveContentConversation(filePath, "\\" + filename + "-Copy.json");
                            prntForm.mod.moduleConvosList.Add(newConvo.ConvoFileName);
                            refreshListBoxConvos();
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
                refreshListBoxConvos();
            }
        }                
        #endregion 

        private void EditConvo()
        {
            ConvoEditor newChild = new ConvoEditor(prntForm.mod, prntForm); //add new child
            newChild.Text = prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex];
            newChild.Show(prntForm.dockPanel1);  //as new form created so that corresponding tab and child form is active
            refreshListBoxConvos();
            newChild.ce_filename = prntForm.mod.moduleConvosList[prntForm._selectedLbxConvoIndex] + ".json";
            newChild.saveConvoFile();
        }

        private void btnLoadAllConvo_Click(object sender, EventArgs e)
        {
            string jobDir = "";
            jobDir = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\dialog";
            prntForm.mod.moduleConvosList.Clear();
            foreach (string f in Directory.GetFiles(jobDir, "*.json"))
            {
                string filename = Path.GetFileNameWithoutExtension(f);
                prntForm.mod.moduleConvosList.Add(filename);
            }
            refreshListBoxConvos();
        }
    }
}
