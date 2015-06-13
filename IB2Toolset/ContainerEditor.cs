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
    public partial class ContainerEditor : DockContent
    {
        List<Item> cte_itemsList = new List<Item>();
        IB2Toolset.Container cte_container = new IB2Toolset.Container();
        public ParentForm prntForm;

        public ContainerEditor(List<Item> items, IB2Toolset.Container container, ParentForm p)
        {
            InitializeComponent();
            cte_itemsList = items;
            cte_container = container;
            prntForm = p;
        }

        private void ContainerEditor_Load(object sender, EventArgs e)
        {
            cmbItems.DataSource = null;
            cmbItems.DataSource = cte_itemsList;
            cmbItems.DisplayMember = "ItemName";
            refreshLbxItems();
        }

        public void refreshLbxItems()
        {
            lbxItems.BeginUpdate();
            lbxItems.DataSource = null;
            lbxItems.DataSource = cte_container.containerItemRefs;
            lbxItems.EndUpdate();
        }

        private void btnAddItems_Click(object sender, EventArgs e)
        {
            try
            {
                Item it = cte_itemsList[cmbItems.SelectedIndex];
                //ItemRefs newItemRefs = new ItemRefs();
                ItemRefs newIR = prntForm.createItemRefsFromItem(it);
                //newItemRefs.name = it.name;
                //newItemRefs.resref = it.resref;
                //newItemRefs.canNotBeUnequipped = it.canNotBeUnequipped;
                //newItemRefs.tag = it.tag + "_" + prntForm.mod.nextIdNumber;
                //newItemRefs.quantity = it.quantity;
                cte_container.containerItemRefs.Add(newIR);
                //string newItemTag = cte_itemsList[cmbItems.SelectedIndex].tag;
                //cte_container.containerItemTags.Add(newItemTag);
                refreshLbxItems();
            }
            catch { }
        }

        private void btnRemoveItems_Click(object sender, EventArgs e)
        {
            if (lbxItems.Items.Count > 0)
            {
                try
                {
                    if (lbxItems.SelectedIndex >= 0)
                    {
                        cte_container.containerItemRefs.RemoveAt(lbxItems.SelectedIndex);
                    }
                }
                catch { }
                refreshLbxItems();
            }
        }
    }
}
