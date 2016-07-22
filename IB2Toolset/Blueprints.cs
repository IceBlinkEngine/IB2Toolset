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
    public partial class Blueprints : DockContent
    {
        public ParentForm prntForm;

        public Blueprints(ParentForm pf)
        {
            InitializeComponent();
            prntForm = pf;
        }
        private Dictionary<string, bool> SaveTreeState(TreeView tree)
        {
            Dictionary<string, bool> nodeStates = new Dictionary<string, bool>();
            for (int i = 0; i < tree.Nodes.Count; i++)
            {
                if (tree.Nodes[i].Nodes.Count > 0)
                {
                    nodeStates.Add(tree.Nodes[i].Text, tree.Nodes[i].IsExpanded);
                }
            }
            return nodeStates;
        }
        private void RestoreTreeState(TreeView tree, Dictionary<string, bool> treeState)
        {
            for (int i = 0; i < tree.Nodes.Count; i++)
            {
                if (treeState.ContainsKey(tree.Nodes[i].Text))
                {
                    if (treeState[tree.Nodes[i].Text])
                        tree.Nodes[i].Expand();
                    else
                        tree.Nodes[i].Collapse();
                }
            }
        }

        #region Creature Stuff
        #region UpdateTreeViewCreatures
        public void UpdateTreeViewCreatures() //creature
        {
            Dictionary<string, bool> nodeStates = SaveTreeState(tvCreatures);
            tvCreatures.Nodes.Clear();
            prntForm.creaturesParentNodeList.Clear();
            foreach (Creature crt in prntForm.mod.moduleCreaturesList)
            {
                if (!CheckExistsCreatureCategory(crt.cr_parentNodeName))
                    prntForm.creaturesParentNodeList.Add(crt.cr_parentNodeName);
            }
            foreach (string crt in prntForm.creaturesParentNodeList)
            {
                TreeNode parentNode;
                parentNode = tvCreatures.Nodes.Add(crt);
                PopulateTreeViewCreatures(crt, parentNode);
            }
            RestoreTreeState(tvCreatures, nodeStates);
            //tvCreatures.ExpandAll();
            refreshPropertiesCreatures();
        }
        private bool CheckExistsCreatureCategory(string parentNodeName) //creature
        {
            foreach (string par in prntForm.creaturesParentNodeList)
            {
                if (parentNodeName == par)
                    return true;
            }
            return false;
        }
        private void PopulateTreeViewCreatures(string parentName, TreeNode parentNode)
        {
            var filteredItems = prntForm.mod.moduleCreaturesList.Where(item => item.cr_parentNodeName == parentName);

            TreeNode childNode;
            foreach (var i in filteredItems.ToList())
            {
                childNode = parentNode.Nodes.Add(i.cr_name);
                childNode.Name = i.cr_tag;
            }
        }
        private void refreshPropertiesCreatures()
        {
            if (tvCreatures.SelectedNode != null)
            {
                string _nodeTag = tvCreatures.SelectedNode.Name;
                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = GetCreature(_nodeTag);
            }
        }
        public Creature GetCreature(string _nodeTag)
        {
            foreach (Creature crt in prntForm.mod.moduleCreaturesList)
            {
                if (crt.cr_tag == _nodeTag)
                    return crt;
            }
            return null;
        }
        public int GetCreatureIndex(string _nodeTag)
        {
            int cnt = 0;
            foreach (Creature crt in prntForm.mod.moduleCreaturesList)
            {
                if (crt.cr_tag == _nodeTag)
                    return cnt;
                cnt++;
            }
            return -1;
        }
        #endregion
        private void tvCreatures_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //prntForm.logText("afterselectCreature");
            if ((tvCreatures.SelectedNode != null) && (tvCreatures.Nodes.Count > 0))
            {
                prntForm.selectedEncounterCreatureTag = tvCreatures.SelectedNode.Name;
                prntForm.selectedLevelMapCreatureTag = tvCreatures.SelectedNode.Name;
                prntForm.selectedEncounterPropTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.PropSelected = false;
                prntForm.lastSelectedCreatureNodeName = tvCreatures.SelectedNode.Name;
                prntForm.logText(prntForm.selectedEncounterCreatureTag);
                prntForm.logText(Environment.NewLine);
                refreshPropertiesCreatures();
                prntForm.refreshIconCreatures();
                //prntForm.frmIconSprite.refreshTraitsKnown();
                prntForm.frmIconSprite.gbKnownSpells.Enabled = true;
                prntForm.frmIconSprite.refreshSpellsKnown();
            }            
        }
        private void tvCreatures_MouseClick_1(object sender, MouseEventArgs e)
        {
            //prntForm.logText("mouseclickCreature");
            if ((tvCreatures.SelectedNode != null) && (tvCreatures.Nodes.Count > 0))
            {
                prntForm.selectedEncounterCreatureTag = tvCreatures.SelectedNode.Name;
                prntForm.selectedLevelMapCreatureTag = tvCreatures.SelectedNode.Name;
                prntForm.selectedEncounterPropTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.PropSelected = false;
                prntForm.lastSelectedCreatureNodeName = tvCreatures.SelectedNode.Name;
                prntForm.logText(prntForm.selectedEncounterCreatureTag);
                prntForm.logText(Environment.NewLine);
                refreshPropertiesCreatures();
                prntForm.refreshIconCreatures();
                prntForm.frmIconSprite.gbKnownSpells.Enabled = true;
                //prntForm.frmIconSprite.refreshTraitsKnown();
                prntForm.frmIconSprite.refreshSpellsKnown();
            }            
        }
        private void btnAddCreature_Click_1(object sender, EventArgs e)
        {
            Creature newCreature = new Creature();
            newCreature.cr_parentNodeName = "New Category";
            newCreature.cr_tag = "newTag_" + prntForm.mod.nextIdNumber;
            prntForm.nodeCount++;
            prntForm.mod.moduleCreaturesList.Add(newCreature);
            UpdateTreeViewCreatures();
        }
        private void btnRemoveCreature_Click_1(object sender, EventArgs e)
        {
            if (tvCreatures.SelectedNode != null && tvCreatures.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvCreatures.SelectedNode.Name;
                    prntForm.mod.moduleCreaturesList.RemoveAt(GetCreatureIndex(_nodeTag));
                    tvCreatures.Nodes.RemoveAt(tvCreatures.SelectedNode.Index);
                    UpdateTreeViewCreatures();
                    // The Remove button was clicked.
                    //int selectedIndex = tvCreatures.SelectedIndex;
                    //creaturesList.creatures.RemoveAt(selectedIndex);
                }
                catch
                {
                    prntForm.logText("Failed to remove creature");
                    prntForm.logText(Environment.NewLine);
                }
                //_selectedLbxCreaturesIndex = 0;
                //lbxCreatures.SelectedIndex = 0;
                //refreshListBoxCreatures();
            }
        }
        private void btnDuplicateCreature_Click_1(object sender, EventArgs e)
        {
            if (tvCreatures.SelectedNode != null && tvCreatures.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvCreatures.SelectedNode.Name;
                    Creature newCreature = prntForm.mod.moduleCreaturesList[GetCreatureIndex(_nodeTag)].DeepCopy();
                    newCreature.cr_tag = "newTag_" + prntForm.mod.nextIdNumber;
                    prntForm.mod.moduleCreaturesList.Add(newCreature);
                    UpdateTreeViewCreatures();
                }
                catch
                {
                    prntForm.logText("Failed to duplicate creature");
                    prntForm.logText(Environment.NewLine);
                }
            }
        }
        private void btnSortCreatures_Click(object sender, EventArgs e)
        {
            prntForm.mod.moduleCreaturesList = prntForm.mod.moduleCreaturesList.OrderBy(o => o.cr_parentNodeName).ThenBy(o => o.cr_name).ToList();
            UpdateTreeViewCreatures();
        }
        #endregion
        
        #region Item Stuff
        #region UpdateTreeViewItems
        public void UpdateTreeViewItems()
        {
            Dictionary<string, bool> nodeStates = SaveTreeState(tvItems);
            tvItems.Nodes.Clear();
            prntForm.itemsParentNodeList.Clear();
            foreach (Item item in prntForm.mod.moduleItemsList)
            {
                if (!CheckExistsItemCategory(item.ItemCategoryName))
                    prntForm.itemsParentNodeList.Add(item.ItemCategoryName);
            }
            foreach (string item in prntForm.itemsParentNodeList)
            {
                TreeNode parentNode;
                parentNode = tvItems.Nodes.Add(item);
                PopulateTreeViewItems(item, parentNode);
            }
            //tvItems.ExpandAll();
            RestoreTreeState(tvItems, nodeStates);
            refreshPropertiesItems();
        }
        private bool CheckExistsItemCategory(string parentNodeName)
        {
            foreach (string par in prntForm.itemsParentNodeList)
            {
                if (parentNodeName == par)
                    return true;
            }
            return false;
        }
        private void PopulateTreeViewItems(string parentName, TreeNode parentNode)
        {
            var filteredItems = prntForm.mod.moduleItemsList.Where(item => item.ItemCategoryName == parentName);

            TreeNode childNode;
            foreach (var i in filteredItems.ToList())
            {
                childNode = parentNode.Nodes.Add(i.name);
                childNode.Name = i.tag;
            }
        }
        private void refreshPropertiesItems()
        {
            if (tvItems.SelectedNode != null)
            {
                string _nodeTag = tvItems.SelectedNode.Name;
                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = GetItem(_nodeTag);
            }
        }
        public Item GetItem(string _nodeTag)
        {
            foreach (Item item in prntForm.mod.moduleItemsList)
            {
                if (item.tag == _nodeTag)
                    return item;
            }
            return null;
        }
        public int GetItemIndex(string _nodeTag)
        {
            int cnt = 0;
            foreach (Item item in prntForm.mod.moduleItemsList)
            {
                if (item.tag == _nodeTag)
                    return cnt;
                cnt++;
            }
            return -1;
        }
        #endregion
        private void tvItems_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            //prntForm.logText("afterselectItem");
            prntForm.lastSelectedItemNodeName = tvItems.SelectedNode.Name;
            prntForm.frmIconSprite.gbKnownSpells.Enabled = false;
            refreshPropertiesItems();
            prntForm.refreshIconItems();
        }
        private void tvItems_MouseClick_1(object sender, MouseEventArgs e)
        {
            if (tvItems.SelectedNode != null)
            {
                //prntForm.logText("mouseclickItem");
                prntForm.lastSelectedItemNodeName = tvItems.SelectedNode.Name;
                prntForm.frmIconSprite.gbKnownSpells.Enabled = false;
                refreshPropertiesItems();
                prntForm.refreshIconItems();
            }
        }
        private void btnAddItem_Click_1(object sender, EventArgs e)
        {
            Item newItem = new Item();
            newItem.name = "new item";
            newItem.ItemCategoryName = "New Category";
            newItem.tag = "newTag_" + prntForm.mod.nextIdNumber;
            prntForm.nodeCount++;
            prntForm.mod.moduleItemsList.Add(newItem);
            UpdateTreeViewItems();
        }
        private void btnRemoveItem_Click_1(object sender, EventArgs e)
        {
            if (tvItems.SelectedNode != null && tvItems.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvItems.SelectedNode.Name;
                    prntForm.mod.moduleItemsList.RemoveAt(GetItemIndex(_nodeTag));
                    tvItems.Nodes.RemoveAt(tvItems.SelectedNode.Index);
                    UpdateTreeViewItems();
                }
                catch
                {
                    prntForm.logText("Failed to remove item");
                    prntForm.logText(Environment.NewLine);
                }
            }
        }
        private void btnDuplicateItem_Click_1(object sender, EventArgs e)
        {
            if (tvItems.SelectedNode != null && tvItems.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvItems.SelectedNode.Name;
                    Item newItem = prntForm.mod.moduleItemsList[GetItemIndex(_nodeTag)].DeepCopy();
                    newItem.tag = "newTag_" + prntForm.mod.nextIdNumber;
                    prntForm.mod.moduleItemsList.Add(newItem);
                    UpdateTreeViewItems();
                }
                catch
                {
                    prntForm.logText("Failed to duplicate item");
                    prntForm.logText(Environment.NewLine);
                }
            }            
        }
        private void btnSortItems_Click(object sender, EventArgs e)
        {
            prntForm.mod.moduleItemsList = prntForm.mod.moduleItemsList.OrderBy(o => o.ItemCategoryName).ThenBy(o => o.name).ToList();
            UpdateTreeViewItems();
        }
        #endregion

        #region Props Stuff
        #region UpdateTreeViewItems
        public void UpdateTreeViewProps()
        {
            Dictionary<string, bool> nodeStates = SaveTreeState(tvProps);
            tvProps.Nodes.Clear();
            prntForm.propsParentNodeList.Clear();
            foreach (Prop prp in prntForm.mod.modulePropsList)
            {
                if (!CheckExistsPropCategory(prp.PropCategoryName))
                    prntForm.propsParentNodeList.Add(prp.PropCategoryName);
            }
            foreach (string prp in prntForm.propsParentNodeList)
            {
                TreeNode parentNode;
                parentNode = tvProps.Nodes.Add(prp);
                PopulateTreeViewProps(prp, parentNode);
            }
            RestoreTreeState(tvProps, nodeStates);
            //tvProps.ExpandAll();
            refreshPropertiesProps();
        }
        private bool CheckExistsPropCategory(string parentNodeName)
        {
            foreach (string par in prntForm.propsParentNodeList)
            {
                if (parentNodeName == par)
                    return true;
            }
            return false;
        }
        private void PopulateTreeViewProps(string parentName, TreeNode parentNode)
        {
            var filteredProps = prntForm.mod.modulePropsList.Where(prp => prp.PropCategoryName == parentName);

            TreeNode childNode;
            foreach (var pr in filteredProps.ToList())
            {
                childNode = parentNode.Nodes.Add(pr.PropName);
                childNode.Name = pr.PropTag;
            }
        }
        private void refreshPropertiesProps()
        {
            if (tvProps.SelectedNode != null)
            {
                string _nodeTag = tvProps.SelectedNode.Name;
                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = GetProp(_nodeTag);
            }
        }
        public Prop GetProp(string _nodeTag)
        {
            foreach (Prop prp in prntForm.mod.modulePropsList)
            {
                if (prp.PropTag == _nodeTag)
                    return prp;
            }
            return null;
        }
        public int GetPropIndex(string _nodeTag)
        {
            int cnt = 0;
            foreach (Prop prp in prntForm.mod.modulePropsList)
            {
                if (prp.PropTag == _nodeTag)
                    return cnt;
                cnt++;
            }
            return -1;
        }
        #endregion
        private void tvProps_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            //prntForm.logText("afterselectProp");
            if (tvProps.SelectedNode != null && tvProps.Nodes.Count > 0)
            {
                prntForm.selectedEncounterPropTag = tvProps.SelectedNode.Name;
                prntForm.selectedLevelMapPropTag = tvProps.SelectedNode.Name;
                prntForm.selectedEncounterCreatureTag = "";
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.CreatureSelected = false;
                prntForm.frmIconSprite.gbKnownSpells.Enabled = false;
                prntForm.lastSelectedPropNodeName = tvProps.SelectedNode.Name;
                prntForm.logText(prntForm.selectedEncounterPropTag);
                prntForm.logText(Environment.NewLine);
            }
            refreshPropertiesProps();
            prntForm.refreshIconProps();
        }
        private void tvProps_MouseClick_1(object sender, MouseEventArgs e)
        {
            //prntForm.logText("mouseclickProp");
            if (tvProps.SelectedNode != null && tvProps.Nodes.Count > 0)
            {
                prntForm.selectedEncounterPropTag = tvProps.SelectedNode.Name;
                prntForm.selectedLevelMapPropTag = tvProps.SelectedNode.Name;
                prntForm.selectedEncounterCreatureTag = "";
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.CreatureSelected = false;
                prntForm.frmIconSprite.gbKnownSpells.Enabled = false;
                prntForm.lastSelectedPropNodeName = tvProps.SelectedNode.Name;
                prntForm.logText(prntForm.selectedEncounterPropTag);
                prntForm.logText(Environment.NewLine);
            }
            refreshPropertiesProps();
            prntForm.refreshIconProps();
        }
        private void btnAddProp_Click_1(object sender, EventArgs e)
        {
            Prop newProp = new Prop();
            newProp.PropName = "newProp";
            newProp.PropCategoryName = "New Category";
            newProp.PropTag = "newPropTag_" + prntForm.mod.nextIdNumber;
            prntForm.nodeCount++;
            prntForm.mod.modulePropsList.Add(newProp);
            UpdateTreeViewProps();
        }
        private void btnRemoveProp_Click_1(object sender, EventArgs e)
        {
            if (tvProps.SelectedNode != null && tvProps.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvProps.SelectedNode.Name;
                    prntForm.mod.modulePropsList.RemoveAt(GetPropIndex(_nodeTag));
                    tvProps.Nodes.RemoveAt(tvProps.SelectedNode.Index);
                    UpdateTreeViewProps();
                }
                catch
                {
                    prntForm.logText("Failed to remove prop");
                    prntForm.logText(Environment.NewLine);
                }
            }
        }
        private void btnDuplicateProp_Click_1(object sender, EventArgs e)
        {
            if (tvProps.SelectedNode != null && tvProps.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvProps.SelectedNode.Name;
                    Prop newProp = prntForm.mod.modulePropsList[GetPropIndex(_nodeTag)].DeepCopy();
                    newProp.PropTag = "newPropTag_" + prntForm.mod.nextIdNumber;
                    prntForm.mod.modulePropsList.Add(newProp);
                    UpdateTreeViewProps();
                }
                catch
                {
                    prntForm.logText("Failed to duplicate prop");
                    prntForm.logText(Environment.NewLine);
                }
            }
        }
        private void btnSortProps_Click(object sender, EventArgs e)
        {
            prntForm.mod.modulePropsList = prntForm.mod.modulePropsList.OrderBy(o => o.PropCategoryName).ThenBy(o => o.PropName).ToList();
            UpdateTreeViewProps();
        }
        #endregion            
    }
}
