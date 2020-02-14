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

namespace IB2Toolset
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
            foreach (Creature crt in prntForm.creaturesList)
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
            var filteredItems = prntForm.creaturesList.Where(item => item.cr_parentNodeName == parentName);

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
            foreach (Creature crt in prntForm.creaturesList)
            {
                if (crt.cr_tag == _nodeTag)
                    return crt;
            }
            return null;
        }
        public int GetCreatureIndex(string _nodeTag)
        {
            int cnt = 0;
            foreach (Creature crt in prntForm.creaturesList)
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
                prntForm.selectedEncounterTriggerTag = "";
                prntForm.selectedLevelMapTriggerTag = "";
                prntForm.CreatureSelected = true;
                prntForm.TriggerSelected = false;
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
                prntForm.selectedEncounterTriggerTag = "";
                prntForm.selectedLevelMapTriggerTag = "";
                prntForm.CreatureSelected = true;
                prntForm.TriggerSelected = false;
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
            prntForm.creaturesList.Add(newCreature);
            UpdateTreeViewCreatures();
        }
        private void btnRemoveCreature_Click_1(object sender, EventArgs e)
        {
            if (tvCreatures.SelectedNode != null && tvCreatures.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvCreatures.SelectedNode.Name;
                    prntForm.creaturesList.RemoveAt(GetCreatureIndex(_nodeTag));
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
                    Creature newCreature = prntForm.creaturesList[GetCreatureIndex(_nodeTag)].DeepCopy();
                    newCreature.cr_tag = "newTag_" + prntForm.mod.nextIdNumber;
                    prntForm.creaturesList.Add(newCreature);
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
            prntForm.creaturesList = prntForm.creaturesList.OrderBy(o => o.cr_parentNodeName).ThenBy(o => o.cr_name).ToList();
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
            foreach (Item item in prntForm.itemsList)
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
            var filteredItems = prntForm.itemsList.Where(item => item.ItemCategoryName == parentName);

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
            foreach (Item item in prntForm.itemsList)
            {
                if (item.tag == _nodeTag)
                    return item;
            }
            return null;
        }
        public int GetItemIndex(string _nodeTag)
        {
            int cnt = 0;
            foreach (Item item in prntForm.itemsList)
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
            prntForm.itemsList.Add(newItem);
            UpdateTreeViewItems();
        }
        private void btnRemoveItem_Click_1(object sender, EventArgs e)
        {
            if (tvItems.SelectedNode != null && tvItems.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvItems.SelectedNode.Name;
                    prntForm.itemsList.RemoveAt(GetItemIndex(_nodeTag));
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
                    Item newItem = prntForm.itemsList[GetItemIndex(_nodeTag)].DeepCopy();
                    newItem.tag = "newTag_" + prntForm.mod.nextIdNumber;
                    prntForm.itemsList.Add(newItem);
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
            prntForm.itemsList = prntForm.itemsList.OrderBy(o => o.ItemCategoryName).ThenBy(o => o.name).ToList();
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
            foreach (Prop prp in prntForm.propsList)
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
            var filteredProps = prntForm.propsList.Where(prp => prp.PropCategoryName == parentName);

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
            foreach (Prop prp in prntForm.propsList)
            {
                if (prp.PropTag == _nodeTag)
                    return prp;
            }
            return null;
        }
        public int GetPropIndex(string _nodeTag)
        {
            int cnt = 0;
            foreach (Prop prp in prntForm.propsList)
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
                prntForm.selectedEncounterTriggerTag = "";
                prntForm.selectedLevelMapTriggerTag = "";
                prntForm.PropSelected = true;
                prntForm.TriggerSelected = false;
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
                prntForm.selectedEncounterTriggerTag = "";
                prntForm.selectedLevelMapTriggerTag = "";
                prntForm.PropSelected = true;
                prntForm.TriggerSelected = false;
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
            prntForm.propsList.Add(newProp);
            UpdateTreeViewProps();
        }
        private void btnRemoveProp_Click_1(object sender, EventArgs e)
        {
            if (tvProps.SelectedNode != null && tvProps.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvProps.SelectedNode.Name;
                    prntForm.propsList.RemoveAt(GetPropIndex(_nodeTag));
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
                    Prop newProp = prntForm.propsList[GetPropIndex(_nodeTag)].DeepCopy();
                    newProp.PropTag = "newPropTag_" + prntForm.mod.nextIdNumber;
                    prntForm.propsList.Add(newProp);
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
            prntForm.propsList = prntForm.propsList.OrderBy(o => o.PropCategoryName).ThenBy(o => o.PropName).ToList();
            UpdateTreeViewProps();
        }
        #endregion

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX

        #region Triggers Stuff
        #region UpdateTreeViewItems
        public void UpdateTreeViewTriggers()
        {
            Dictionary<string, bool> nodeStates = SaveTreeState(tvTriggers);
            tvTriggers.Nodes.Clear();
            prntForm.triggersParentNodeList.Clear();
            foreach (Trigger trg in prntForm.triggersList)
            {
                if (!CheckExistsTriggerCategory(trg.TriggerCategory))
                    prntForm.triggersParentNodeList.Add(trg.TriggerCategory);
            }
            foreach (string trg in prntForm.triggersParentNodeList)
            {
                TreeNode parentNode;
                parentNode = tvTriggers.Nodes.Add(trg);
                PopulateTreeViewTriggers(trg, parentNode);
            }
            RestoreTreeState(tvTriggers, nodeStates);
            //tvProps.ExpandAll();
            refreshPropertiesTriggers();
        }
        private bool CheckExistsTriggerCategory(string parentNodeName)
        {
            foreach (string par in prntForm.triggersParentNodeList)
            {
                if (parentNodeName == par)
                    return true;
            }
            return false;
        }
        private void PopulateTreeViewTriggers(string parentName, TreeNode parentNode)
        {
            var filteredTriggers = prntForm.triggersList.Where(trg => trg.TriggerCategory == parentName);

            TreeNode childNode;
            foreach (var pr in filteredTriggers.ToList())
            {
                childNode = parentNode.Nodes.Add(pr.TriggerName);
                childNode.Name = pr.TriggerTag;
            }
        }
        private void refreshPropertiesTriggers()
        {
            if (tvTriggers.SelectedNode != null)
            {
                string _nodeTag = tvTriggers.SelectedNode.Name;
                prntForm.frmIceBlinkProperties.propertyGrid1.SelectedObject = GetTrigger(_nodeTag);
                prntForm.currentSelectedTrigger = GetTrigger(_nodeTag);
            }
        }
        public Trigger GetTrigger(string _nodeTag)
        {
            foreach (Trigger trg in prntForm.triggersList)
            {
                if (trg.TriggerTag == _nodeTag)
                    return trg;
            }
            return null;
        }
        public int GetTriggerIndex(string _nodeTag)
        {
            int cnt = 0;
            foreach (Trigger trg in prntForm.triggersList)
            {
                if (trg.TriggerTag == _nodeTag)
                    return cnt;
                cnt++;
            }
            return -1;
        }
        #endregion
        private void tvTriggers_AfterSelect_1(object sender, TreeViewEventArgs e)
        {
            //prntForm.logText("afterselectProp");
            if (tvTriggers.SelectedNode != null && tvTriggers.Nodes.Count > 0)
            {
                prntForm.selectedEncounterTriggerTag = tvTriggers.SelectedNode.Name;
                prntForm.selectedLevelMapTriggerTag = tvTriggers.SelectedNode.Name;
                prntForm.selectedEncounterCreatureTag = "";
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.selectedEncounterPropTag = "";
                prntForm.selectedLevelMapPropTag = "";
                prntForm.TriggerSelected = true;
                prntForm.PropSelected = false;
                prntForm.CreatureSelected = false;
                prntForm.frmIconSprite.gbKnownSpells.Enabled = false;
                prntForm.lastSelectedTriggerNodeName = tvTriggers.SelectedNode.Name;
                prntForm.logText(prntForm.selectedEncounterTriggerTag);
                prntForm.logText(Environment.NewLine);

                Trigger t = new Trigger();
                t = GetTrigger((string)tvTriggers.SelectedNode.Tag);
                //lastSelectedObjectTag = t.TriggerTag;
                prntForm.currentSelectedTrigger = t;
                //prntForm.le_selectedTrigger = t;
            }
            refreshPropertiesTriggers();
            prntForm.frmTriggerEvents.refreshTriggers();
            //not needed?
            //prntForm.refreshIconTriggers();
        }
        private void tvTriggers_MouseClick_1(object sender, MouseEventArgs e)
        {
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXx
            /*
            txtSelectedIconInfo.Text = "Trigger Tag: " + Environment.NewLine + t.TriggerTag;
            lastSelectedObjectTag = t.TriggerTag;
            prntForm.currentSelectedTrigger = t;
            prntForm.frmTriggerEvents.refreshTriggers();
            panelView.ContextMenuStrip.Items.Add(t.TriggerTag, null, handler);
            */
            //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
            //prntForm.logText("mouseclickProp");
            if (tvTriggers.SelectedNode != null && tvTriggers.Nodes.Count > 0)
            {

                //Trigger t = new Trigger();
                //t = GetTrigger((string)tvTriggers.SelectedNode.Tag);
                //lastSelectedObjectTag = t.TriggerTag;
                //prntForm.currentSelectedTrigger = t;
                //prntForm.frmTriggerEvents.refreshTriggers();

                prntForm.selectedEncounterTriggerTag = tvTriggers.SelectedNode.Name;
                prntForm.selectedLevelMapTriggerTag = tvTriggers.SelectedNode.Name;
                prntForm.selectedEncounterCreatureTag = "";
                prntForm.selectedLevelMapCreatureTag = "";
                prntForm.TriggerSelected = true;
                prntForm.CreatureSelected = false;
                prntForm.PropSelected = false;
                prntForm.frmIconSprite.gbKnownSpells.Enabled = false;
                prntForm.lastSelectedTriggerNodeName = tvTriggers.SelectedNode.Name;
                prntForm.logText(prntForm.selectedEncounterTriggerTag);
                prntForm.logText(Environment.NewLine);
            }
            refreshPropertiesTriggers();
            prntForm.frmTriggerEvents.refreshTriggers();
            //not needed?
            //prntForm.refreshIconProps();
        }
        private void btnAddTrigger_Click_1(object sender, EventArgs e)
        {
            Trigger newTrigger = new Trigger();
            newTrigger.TriggerName = "newTrigger";
            newTrigger.TriggerCategory = "New Category";
            newTrigger.TriggerTag = "newTriggerTag_" + prntForm.mod.nextIdNumber;
            prntForm.nodeCount++;
            prntForm.triggersList.Add(newTrigger);
            UpdateTreeViewTriggers();
        }
        private void btnRemoveTrigger_Click_1(object sender, EventArgs e)
        {
            if (tvTriggers.SelectedNode != null && tvTriggers.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvTriggers.SelectedNode.Name;
                    prntForm.triggersList.RemoveAt(GetTriggerIndex(_nodeTag));
                    tvTriggers.Nodes.RemoveAt(tvTriggers.SelectedNode.Index);
                    UpdateTreeViewTriggers();
                }
                catch
                {
                    prntForm.logText("Failed to remove trigger");
                    prntForm.logText(Environment.NewLine);
                }
            }
        }
        private void btnDuplicateTrigger_Click_1(object sender, EventArgs e)
        {
            if (tvTriggers.SelectedNode != null && tvTriggers.Nodes.Count > 0)
            {
                try
                {
                    string _nodeTag = tvTriggers.SelectedNode.Name;
                    Trigger newTrigger = prntForm.triggersList[GetTriggerIndex(_nodeTag)].DeepCopy();
                    newTrigger.TriggerTag = "newTriggerTag_" + prntForm.mod.nextIdNumber;
                    prntForm.triggersList.Add(newTrigger);
                    UpdateTreeViewTriggers();
                }
                catch
                {
                    prntForm.logText("Failed to duplicate trigger");
                    prntForm.logText(Environment.NewLine);
                }
            }
        }
        private void btnSortTriggers_Click(object sender, EventArgs e)
        {
            prntForm.triggersList = prntForm.triggersList.OrderBy(o => o.TriggerCategory).ThenBy(o => o.TriggerName).ToList();
            UpdateTreeViewTriggers();
        }
        #endregion            


        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX


    }
}
