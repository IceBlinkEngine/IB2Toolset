using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace IB2Toolset
{
    public partial class EncountersForm : DockContent
    {
        public ParentForm prntForm;
        public Encounter encToCopy;

        public EncountersForm(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }

        #region Encounter Stuff
        public void refreshListBoxEncounters()
        {
            lbxEncounters.BeginUpdate();
            lbxEncounters.DataSource = null;
            lbxEncounters.DataSource = prntForm.encountersList;
            lbxEncounters.DisplayMember = "EncounterName";
            lbxEncounters.EndUpdate();
        }
        private void txtEncounterName_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                prntForm.encountersList[prntForm._selectedLbxEncounterIndex].encounterName = txtEncounterName.Text;
                refreshListBoxEncounters();
            }
            catch { }
        }
        private void btnAddEncounter_Click_1(object sender, EventArgs e)
        {
            Encounter newEncounter = new Encounter();
            newEncounter.encounterName = "new encounter";
            newEncounter.SetAllToGrass();
            //newEncounter.passRefs(prntForm.game, prntForm);
            prntForm.encountersList.Add(newEncounter);
            refreshListBoxEncounters();
            // should I create a new file at this point?
        }
        private void btnRemoveEncounter_Click_1(object sender, EventArgs e)
        {
            if ((lbxEncounters.Items.Count > 0) && (lbxEncounters.SelectedIndex >= 0))
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxEncounters.SelectedIndex;
                    prntForm.encountersList.RemoveAt(selectedIndex);
                }
                catch { }
                prntForm._selectedLbxEncounterIndex = 0;
                lbxEncounters.SelectedIndex = 0;
                refreshListBoxEncounters();
            }
        }
        private void btnEditEncounter_Click_1(object sender, EventArgs e)
        {
            if ((lbxEncounters.Items.Count > 0) && (lbxEncounters.SelectedIndex >= 0))
            {
                EditEncounter();
                /*
                Encounter enc = prntForm.encountersList.encounters[prntForm._selectedLbxEncounterIndex];
                EncounterEditor newChild = new EncounterEditor(prntForm.mod, enc, prntForm.game, prntForm);      //add new child
                //         TabPage childTab = new TabPage();         //create new tab page
                //         newChild.MdiParent = this;                       //set as child of this form
                //newChild.Name = "Child" + createdTab.ToString();

                // should use the file name from selected area in lbxAreas
                // should automatically load area image and .level file upon clicking on the edit button

                newChild.Text = prntForm.encountersList.encounters[prntForm._selectedLbxEncounterIndex].EncounterName;
                //childTab.Name = newChild.Name;           //make sure name and text are same
                //          childTab.Text = newChild.Text;                  //this is for syncrhonize later
                //          childTab.Tag = "EncounterEditor";
                //               tabControl1.TabPages.Add(childTab);     //add new tab
                //          newChild.EcounterEditorPanel.Parent = childTab;     //attach to tab
                //             tabControl1.SelectTab(childTab);     //this is to make sure that tab page is selected in the same time
                newChild.Show(prntForm.dockPanel1);                                 //as new form created so that corresponding tab and child form is active
                //           prntForm.createdTab++;   //increment of course
                //mod.ModuleAreasList.Add("New Area");
                refreshListBoxEncounters();
                */
            }
        }
        private void lbxEncounters_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (lbxEncounters.SelectedIndex >= 0)
            {
                prntForm._selectedLbxEncounterIndex = lbxEncounters.SelectedIndex;
                txtEncounterName.Text = prntForm.encountersList[prntForm._selectedLbxEncounterIndex].encounterName;
                lbxEncounters.SelectedIndex = prntForm._selectedLbxEncounterIndex;
            }
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            if ((lbxEncounters.Items.Count > 0) && (lbxEncounters.SelectedIndex >= 0))
            {
                RenameDialog newName = new RenameDialog();
                DialogResult result = newName.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        prntForm.encountersList[prntForm._selectedLbxEncounterIndex].encounterName = newName.RenameText;
                        refreshListBoxEncounters();
                    }
                    catch { }
                }
            }
        }
        private void lbxEncounters_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbxEncounters.IndexFromPoint(e.Location);

            if (index != System.Windows.Forms.ListBox.NoMatches)
            {

                //MessageBox.Show(index.ToString());
                //do your stuff here
                //prntForm._selectedLbxEncounterIndex = index;
                if ((lbxEncounters.Items.Count > 0) && (lbxEncounters.SelectedIndex >= 0))
                {
                    EditEncounter();
                }
            }
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            prntForm.encountersList = prntForm.encountersList.OrderBy(o => o.encounterName).ToList();
            refreshListBoxEncounters();
        }
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if ((lbxEncounters.Items.Count > 0) && (lbxEncounters.SelectedIndex >= 0))
            {
                try
                {
                    Encounter newEncounter = new Encounter();
                    newEncounter = prntForm.encountersList[prntForm._selectedLbxEncounterIndex].DeepCopy();
                    newEncounter.encounterName = prntForm.encountersList[prntForm._selectedLbxEncounterIndex].encounterName + "-Copy";
                    //newEncounter.passRefs(prntForm.game, prntForm);
                    prntForm.encountersList.Add(newEncounter);
                    refreshListBoxEncounters();
                }
                catch { }
            }
        } 
        #endregion 
       
        private void EditEncounter()
        {
            Encounter enc = prntForm.encountersList[prntForm._selectedLbxEncounterIndex];
            EncounterEditor newChild = new EncounterEditor(prntForm.mod, prntForm); //add new child
            newChild.Text = prntForm.encountersList[prntForm._selectedLbxEncounterIndex].encounterName;
            newChild.Show(prntForm.dockPanel1); //as new form created so that corresponding tab and child form is active
            refreshListBoxEncounters();
        }
    }
}
