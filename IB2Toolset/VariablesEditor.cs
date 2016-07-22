using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using IceBlinkCore;

namespace IB2miniToolset
{
    public partial class VariablesEditor : Form
    {
        private Module mod = new Module();
        public ParentForm prntForm;
        private int selectedGlobalLbxIndex = 0;
        private int selectedLocalLbxIndex = 0;

        public VariablesEditor(Module m, ParentForm p)
        {
            InitializeComponent();
            mod = m;
            prntForm = p;
            refreshGlobalListBox();
            refreshLocalListBox();
        }

        #region GLOBALS
        private void refreshGlobalListBox()
        {
            lbxGlobals.BeginUpdate();
            lbxGlobals.DataSource = null;
            lbxGlobals.DataSource = mod.ModuleGlobalListItems;
            lbxGlobals.DisplayMember = "GlobalName";
            lbxGlobals.EndUpdate();
        }
        private void btnGlobalAdd_Click(object sender, EventArgs e)
        {
            if (txtGlobalAdd.Text != "")
            {
                GlobalListItem newGli = new GlobalListItem();
                newGli.GlobalName = txtGlobalAdd.Text;
                mod.ModuleGlobalListItems.Add(newGli);
                refreshGlobalListBox();
            }
        }
        private void btnSortGlobals_Click(object sender, EventArgs e)
        {
            mod.ModuleGlobalListItems = mod.ModuleGlobalListItems.OrderBy(o => o.GlobalName).ToList();
            refreshGlobalListBox();
        }
        private void lbxGlobals_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lbxGlobals.SelectedIndex >= 0) && (mod.ModuleGlobalListItems != null))
            {
                selectedGlobalLbxIndex = lbxGlobals.SelectedIndex;
                lbxGlobals.SelectedIndex = selectedGlobalLbxIndex;
                txtGlobalNotes.Text = mod.ModuleGlobalListItems[lbxGlobals.SelectedIndex].GlobalNotes;
            }
        }
        private void btnGlobalClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(mod.ModuleGlobalListItems[lbxGlobals.SelectedIndex].GlobalName);
                this.Close();
            }
            catch { }
        }
        private void txtGlobalNotes_TextChanged(object sender, EventArgs e)
        {
            if (lbxGlobals.Items.Count > 0)
            {
                mod.ModuleGlobalListItems[lbxGlobals.SelectedIndex].GlobalNotes = txtGlobalNotes.Text;
            }
        }
        private void btnRemoveGlobal_Click(object sender, EventArgs e)
        {
            if (lbxGlobals.Items.Count > 0)
            {
                try
                {
                    int selectedIndex = lbxGlobals.SelectedIndex;
                    mod.ModuleGlobalListItems.RemoveAt(selectedIndex);
                }
                catch { }
                selectedGlobalLbxIndex = 0;
                lbxGlobals.SelectedIndex = 0;
                refreshGlobalListBox();
            }
        }      
        #endregion

        #region LOCALS
        private void refreshLocalListBox()
        {
            lbxLocals.BeginUpdate();
            lbxLocals.DataSource = null;
            lbxLocals.DataSource = mod.ModuleLocalListItems;
            lbxLocals.DisplayMember = "LocalName";
            lbxLocals.EndUpdate();
        }
        private void btnLocalAdd_Click(object sender, EventArgs e)
        {
            if (txtLocalAdd.Text != "")
            {
                LocalListItem newLli = new LocalListItem();
                newLli.LocalName = txtLocalAdd.Text;
                mod.ModuleLocalListItems.Add(newLli);
                refreshLocalListBox();
            }
        }
        private void btnSortLocals_Click(object sender, EventArgs e)
        {
            mod.ModuleLocalListItems = mod.ModuleLocalListItems.OrderBy(o => o.LocalName).ToList();
            refreshLocalListBox();
        }        
        private void lbxLocals_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lbxLocals.SelectedIndex >= 0) && (mod.ModuleLocalListItems != null))
            {
                selectedLocalLbxIndex = lbxLocals.SelectedIndex;
                lbxLocals.SelectedIndex = selectedLocalLbxIndex;
                txtLocalNotes.Text = mod.ModuleLocalListItems[lbxLocals.SelectedIndex].LocalNotes;
            }
        }
        private void btnLocalClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(mod.ModuleLocalListItems[lbxLocals.SelectedIndex].LocalName);
                this.Close();
            }
            catch { }
        }
        private void txtLocalNotes_TextChanged(object sender, EventArgs e)
        {
            if (lbxLocals.Items.Count > 0)
            {
                mod.ModuleLocalListItems[lbxLocals.SelectedIndex].LocalNotes = txtLocalNotes.Text;
            }
        }
        private void btnRemoveLocal_Click(object sender, EventArgs e)
        {
            if (lbxLocals.Items.Count > 0)
            {
                try
                {
                    int selectedIndex = lbxLocals.SelectedIndex;
                    mod.ModuleLocalListItems.RemoveAt(selectedIndex);
                }
                catch { }
                selectedLocalLbxIndex = 0;
                lbxLocals.SelectedIndex = 0;
                refreshLocalListBox();
            }
        }
        private bool IsNotInList(string variableName)
        {
            foreach (LocalListItem l in mod.ModuleLocalListItems)
            {
                if (l.LocalName == variableName)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
    }
}
