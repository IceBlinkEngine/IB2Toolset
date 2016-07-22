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

namespace IB2miniToolset
{
    public partial class ContainersForm : DockContent
    {
        public ParentForm prntForm;

        public ContainersForm(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }

        #region Container Stuff
        public void refreshListBoxContainers()
        {
            //if (lbxAreas.Items.Count < 1)
            //{
            lbxContainers.BeginUpdate();
            lbxContainers.DataSource = null;
            //lbxContainers.DataSource = mod.ModuleContainersList.containers;
            lbxContainers.DataSource = prntForm.mod.moduleContainersList;
            lbxContainers.DisplayMember = "ContainerTag";
            lbxContainers.EndUpdate();
            //}
        }
        private void txtContainerName_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                //mod.ModuleContainersList.containers[_selectedLbxContainerIndex].containerName = txtContainerName.Text;
                prntForm.mod.moduleContainersList[prntForm._selectedLbxContainerIndex].containerTag = txtContainerName.Text;
                refreshListBoxContainers();
                //containersList.containers[_selectedLbx1Index].containerName = txtName.Text;
                //refreshListBox1();
            }
            catch { }
        }
        private void btnAddContainer_Click_1(object sender, EventArgs e)
        {
            IB2miniToolset.Container newContainer = new IB2miniToolset.Container();
            newContainer.containerTag = "new Container";
            //mod.ModuleContainersList.containers.Add(newContainer);
            prntForm.mod.moduleContainersList.Add(newContainer);
            refreshListBoxContainers();
            //refreshListBox1();
            //refreshPanelInfo();
        }
        private void btnRemoveContainer_Click_1(object sender, EventArgs e)
        {
            if ((lbxContainers.Items.Count > 0) && (lbxContainers.SelectedIndex >= 0))
            {
                try
                {
                    // The Remove button was clicked.
                    int selectedIndex = lbxContainers.SelectedIndex;
                    //mod.ModuleContainersList.containers.RemoveAt(selectedIndex);
                    prntForm.mod.moduleContainersList.RemoveAt(selectedIndex);
                }
                catch { }
                prntForm._selectedLbxContainerIndex = 0;
                lbxContainers.SelectedIndex = 0;
                refreshListBoxContainers();
            }
        }
        private void btnEditContainer_Click_1(object sender, EventArgs e)
        {
            if ((lbxContainers.Items.Count > 0) && (lbxContainers.SelectedIndex >= 0))
            {
                EditContainer();
                //IceBlinkCore.Container selectedCont = mod.ModuleContainersList.containers[_selectedLbxContainerIndex];
                //IceBlinkCore.Container selectedCont = prntForm.containersList.containers[prntForm._selectedLbxContainerIndex];
                //ContainerEditor cont = new ContainerEditor(prntForm.itemsList, selectedCont);
                //cont.ShowDialog();
            }
        }
        private void lbxContainers_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if ((lbxContainers.SelectedIndex >= 0) && (prntForm.mod.moduleContainersList != null))
            {
                prntForm._selectedLbxContainerIndex = lbxContainers.SelectedIndex;
                //txtContainerName.Text = mod.ModuleContainersList.containers[_selectedLbxContainerIndex].ContainerName;
                txtContainerName.Text = prntForm.mod.moduleContainersList[prntForm._selectedLbxContainerIndex].containerTag;
                lbxContainers.SelectedIndex = prntForm._selectedLbxContainerIndex;
            }
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            if ((lbxContainers.Items.Count > 0) && (lbxContainers.SelectedIndex >= 0))
            {
                RenameDialog newName = new RenameDialog();
                DialogResult result = newName.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        prntForm.mod.moduleContainersList[prntForm._selectedLbxContainerIndex].containerTag = newName.RenameText;
                        refreshListBoxContainers();
                    }
                    catch { }
                }
            }
        }
        private void lbxContainers_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbxContainers.IndexFromPoint(e.Location);

            if (index != System.Windows.Forms.ListBox.NoMatches)
            {

                //MessageBox.Show(index.ToString());
                //do your stuff here
                //prntForm._selectedLbxContainerIndex = index;
                if ((lbxContainers.Items.Count > 0) && (lbxContainers.SelectedIndex >= 0))
                {
                    EditContainer();
                }
            }
        }
        private void btnSort_Click(object sender, EventArgs e)
        {
            prntForm.mod.moduleContainersList = prntForm.mod.moduleContainersList.OrderBy(o => o.containerTag).ToList();
            refreshListBoxContainers();
        }
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if ((lbxContainers.Items.Count > 0) && (lbxContainers.SelectedIndex >= 0))
            {
                try
                {
                    IB2miniToolset.Container newContainer = new IB2miniToolset.Container();
                    newContainer = prntForm.mod.moduleContainersList[prntForm._selectedLbxContainerIndex].DeepCopy();
                    newContainer.containerTag = prntForm.mod.moduleContainersList[prntForm._selectedLbxContainerIndex].containerTag + "-Copy";
                    prntForm.mod.moduleContainersList.Add(newContainer);
                    refreshListBoxContainers();
                }
                catch { }
            }
        }
        #endregion

        private void EditContainer()
        {
            IB2miniToolset.Container selectedCont = prntForm.mod.moduleContainersList[prntForm._selectedLbxContainerIndex];
            ContainerEditor cont = new ContainerEditor(prntForm.mod.moduleItemsList, selectedCont, prntForm);
            cont.ShowDialog();
        }
    }
}
