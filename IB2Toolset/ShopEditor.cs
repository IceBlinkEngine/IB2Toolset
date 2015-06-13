using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using IceBlinkCore;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;

namespace IB2Toolset
{
    public partial class ShopEditor : Form
    {
        private ParentForm prntForm;
        private Module mod;
        //private Game game;
        private int selectedLbxIndex = 0;

        public ShopEditor(Module m, ParentForm p)
        {
            InitializeComponent();
            mod = m;
            //game = g;
            prntForm = p;
        }

        #region Handlers
        private void ShopEditor_Load(object sender, EventArgs e)
        {
            cmbItems.DataSource = null;
            cmbItems.DataSource = prntForm.itemsList;
            cmbItems.DisplayMember = "name";
            refreshLbxItems();
            refreshListBox();
        }
        private void lbxShops_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((lbxShops.SelectedIndex >= 0) && (prntForm.shopsList != null))
            {
                selectedLbxIndex = lbxShops.SelectedIndex;
                lbxShops.SelectedIndex = selectedLbxIndex;
                propertyGrid1.SelectedObject = prntForm.shopsList[selectedLbxIndex];
                refreshLbxItems();
                refreshListBox();
            }
        }
        private void btnAddShop_Click(object sender, EventArgs e)
        {

            Shop newShop = new Shop();
            newShop.shopTag = "newShopTag" + prntForm.mod.nextIdNumber.ToString();
            prntForm.shopsList.Add(newShop);
            refreshListBox();
        }
        private void btnRemoveShop_Click(object sender, EventArgs e)
        {
            if (prntForm.shopsList.Count > 0)
            {
                if (lbxShops.Items.Count > 0)
                {
                    try
                    {
                        int selectedIndex = lbxShops.SelectedIndex;
                        prntForm.shopsList.RemoveAt(selectedIndex);
                    }
                    catch { }
                    prntForm._selectedLbxContainerIndex = 0;
                    lbxShops.SelectedIndex = 0;
                    refreshLbxItems();
                    refreshListBox();                    
                }
            }
        }
        private void btnDuplicateShop_Click(object sender, EventArgs e)
        {
            if (prntForm.shopsList.Count > 0)
            {
                Shop newCopy = prntForm.shopsList[selectedLbxIndex].DeepCopy();
                newCopy.shopTag = "newCopiedShopTag_" + prntForm.mod.nextIdNumber.ToString();
                prntForm.shopsList.Add(newCopy);
                refreshListBox();
                refreshLbxItems();
            }
        }
        private void btnAddItems_Click(object sender, EventArgs e)
        {
            if (prntForm.shopsList.Count > 0)
            {
                try
                {
                    Item it = prntForm.itemsList[cmbItems.SelectedIndex];
                    ItemRefs newIR = prntForm.createItemRefsFromItem(it);
                    prntForm.shopsList[selectedLbxIndex].shopItemRefs.Add(newIR);
                }
                catch { }
            }
            refreshLbxItems();
            refreshListBox();
        }
        private void btnRemoveItems_Click(object sender, EventArgs e)
        {
            if (lbxItems.Items.Count > 0)
            {
                try
                {
                    if (lbxItems.SelectedIndex >= 0)
                    {
                        prntForm.shopsList[selectedLbxIndex].shopItemRefs.RemoveAt(lbxItems.SelectedIndex);
                    }
                }
                catch { }
                refreshLbxItems();
                refreshListBox();
            }
        }        
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            refreshListBox();
        }
        #endregion

        #region Methods
        private void refreshListBox()
        {
            if (prntForm.shopsList.Count > 0)
            {
                lbxShops.BeginUpdate();
                lbxShops.DataSource = null;
                lbxShops.DataSource = prntForm.shopsList;
                lbxShops.DisplayMember = "shopTag";
                lbxShops.EndUpdate();
            }
            else
            {
                lbxShops.BeginUpdate();
                lbxShops.DataSource = null;
                lbxShops.EndUpdate();
            }
        }
        public void refreshLbxItems()
        {
            if (prntForm.shopsList.Count > 0)
            {
                if (prntForm.shopsList[selectedLbxIndex].shopItemRefs.Count > 0)
                {
                    lbxItems.BeginUpdate();
                    lbxItems.DataSource = null;
                    lbxItems.DataSource = prntForm.shopsList[selectedLbxIndex].shopItemRefs;
                    lbxItems.DisplayMember = "name";
                    lbxItems.EndUpdate();
                }
                else
                {
                    lbxItems.BeginUpdate();
                    lbxItems.DataSource = null;
                    lbxItems.EndUpdate();
                }
            }
            else
            {
                lbxItems.BeginUpdate();
                lbxItems.DataSource = null;
                lbxItems.EndUpdate();
            }
        }
        #endregion
    }
}
