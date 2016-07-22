using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
//using IceBlinkCore;
using WeifenLuo.WinFormsUI.Docking;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace IB2Toolset
{
    public partial class ConvoEditor : DockContent
    { 
        public Convo f_convo;
        public Module ce_mod;
        public ParentForm prntForm;      
        int copyToClipboardNode = 1;
        int copyToClipboardNodeParentNode = 1;
        int pasteFromClipboardNode = 1;
        TreeNode currentSelectedNode = new TreeNode();
        public string ce_filename;
        private List<string> scriptList = new List<string>();
        public Stack<Convo> undoConvoStack = new Stack<Convo>();
        public Stack<Convo> redoConvoStack = new Stack<Convo>();
        public Stack<string> undoTextStack = new Stack<string>();
        public Stack<string> redoTextStack = new Stack<string>();
        public string lastTextStored = "";

        #region Handlers
        public ConvoEditor(Module mod, ParentForm p)
        {
            InitializeComponent();
            f_convo = new Convo();
            ce_mod = mod;
            prntForm = p;
        }
        private void ConvoEditor_Load(object sender, EventArgs e)
        {            
            // load the file that has the selected node name if it exists
            string filenameOnly = ce_mod.moduleConvosList[prntForm._selectedLbxConvoIndex] + ".json";
            string dirFullPath = prntForm._mainDirectory + "\\modules\\" + ce_mod.moduleName + "\\dialog";
            if (File.Exists(dirFullPath + "\\" + filenameOnly))
            {
                openFile(dirFullPath, filenameOnly);
                if (f_convo == null)
                {
                    f_convo = new Convo();
                    f_convo.ConvoFileName = filenameOnly;
                    //initializeConditionsTab();
                    //initializeActionsTab();
                    // start off by adding a base treeview node
                    ContentNode contentNode = new ContentNode();
                    contentNode.idNum = f_convo.NextIdNum;
                    contentNode.conversationText = "root";
                    f_convo.AddNodeToRoot(contentNode);
                    TreeNode mainNode = new TreeNode();
                    mainNode.Name = "0";
                    mainNode.Text = "root";
                    treeView1.Nodes.Add(mainNode);
                }
                else
                {                    
                    //initializeConditionsTab();
                    //initializeActionsTab();
                }                   
            }
            else
            {
                f_convo.ConvoFileName = filenameOnly;
                //initializeConditionsTab();
                //initializeActionsTab();
                // start off by adding a base treeview node
                ContentNode contentNode = new ContentNode();
                contentNode.idNum = f_convo.NextIdNum;
                contentNode.conversationText = "root";
                f_convo.AddNodeToRoot(contentNode);
                TreeNode mainNode = new TreeNode();
                mainNode.Name = "0";
                mainNode.Text = "root";
                treeView1.Nodes.Add(mainNode);
            }
            currentSelectedNode = null;
            txtImage.Text = f_convo.NpcPortraitBitmap;
            txtDefaultNpcName.Text = f_convo.DefaultNpcName;
            chkNarrator.Checked = f_convo.Narration;
            chkPartyChat.Checked = f_convo.PartyChat;
            chkMainPcOnly.Checked = f_convo.SpeakToMainPcOnly;
            SortConversation(f_convo);
            ResetOrderNumBasedOnIndex(f_convo.subNodes[0]);
            prntForm.openConvosList.Add(f_convo);
            fillScriptList();            
        }
        private void ConvoEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("closing editor and removing from openConvosList");
            prntForm.openConvosList.Remove(f_convo);
        }
        private void tsAddNode_Click(object sender, EventArgs e)
        {
            AddNode();            
        }        
        private void tsRemoveNode_Click(object sender, EventArgs e)
        {
            RemoveNode();            
        }        
        private void tsCopyNodes_Click(object sender, EventArgs e)
        {
            CopyNodes();            
        }        
        private void tsPasteNodes_Click(object sender, EventArgs e)
        {
            PasteNodes();            
        }        
        private void tsPasteAsLink_Click(object sender, EventArgs e)
        {
            PasteAsLink();            
        }
        private void tsRelocateCopyNodes_Click(object sender, EventArgs e)
        {
            PasteAsRelocatedNodes();
        }
        private void tsRefresh_Click(object sender, EventArgs e)
        {
            refreshTreeView();
        }        
        private void tsMoveUp_Click(object sender, EventArgs e)
        {
            MoveUp();            
        }        
        private void tsMoveDown_Click(object sender, EventArgs e)
        {
            MoveDown();            
        }
        private void addNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNode();
        }
        private void removeNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveNode();
        }
        private void copyNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyNodes();
        }
        private void relocateCopiedNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteAsRelocatedNodes();
        }
        private void pasteNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteNodes();
        }
        private void pasteAsLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteAsLink();
        }
        private void followLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FollowLink();
        }
        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveUp();
        }
        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveDown();
        }
        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }
        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }
        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshTreeView();
        }
        private void nodeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                // Set the tree view's PathSeparator property.
                treeView1.PathSeparator = "/";

                // Get the count of the child tree nodes contained in the SelectedNode.
                int myNodeCount = treeView1.SelectedNode.GetNodeCount(true);
                decimal myChildPercentage = ((decimal)myNodeCount / (decimal)treeView1.GetNodeCount(true)) * 100;
                // Display the tree node path and the number of child nodes it and the tree view have.
                prntForm.logText("The '" + treeView1.SelectedNode.FullPath + "' node has "
                  + myNodeCount.ToString() + " child nodes.\nThat is "
                  + string.Format("{0:###.##}", myChildPercentage)
                  + "% of the total tree nodes in the tree view control.");
            }
            catch { }
        }
        private void chkDoOnceOnly_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(treeView1.SelectedNode.Name) != 0)
            {
                int pnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                f_convo.GetContentNodeById(pnod).ShowOnlyOnce = chkDoOnceOnly.Checked;
            }
        }                        
        private void txtText_TextChanged(object sender, EventArgs e)
        {
            //check to ignore if selected node is root
            if (Convert.ToInt32(treeView1.SelectedNode.Name) != 0)
            {
                treeView1.SelectedNode.Text = txtText.Text;
                int pnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                f_convo.GetContentNodeById(pnod).conversationText = txtText.Text;
            }
        }
        private void txtNodeSound_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(treeView1.SelectedNode.Name) != 0)
            {
                int pnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                f_convo.GetContentNodeById(pnod).NodeSound = txtNodeSound.Text;
            }
        }
        private void txtNodeNpcName_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(treeView1.SelectedNode.Name) != 0)
            {
                int pnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                f_convo.GetContentNodeById(pnod).NodeNpcName = txtNodeNpcName.Text;
            }
        }
        private void txtDefaultNpcName_TextChanged(object sender, EventArgs e)
        {
            f_convo.DefaultNpcName = txtDefaultNpcName.Text;
        }
        private void btnOpenImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\modules\\" + ce_mod.moduleName + "\\graphics";
            //Empty the FileName text box of the dialog
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string filename = Path.GetFileName(openFileDialog1.FileName);
                f_convo.NpcPortraitBitmap = filename;
                txtImage.Text = filename;
            }
        }
        private void btnClearImage_Click(object sender, EventArgs e)
        {
            f_convo.NpcPortraitBitmap = "";
            txtImage.Text = "";
        }
        private void btnOpenNodeImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\modules\\" + ce_mod.moduleName + "\\graphics";
            //Empty the FileName text box of the dialog
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string filename = Path.GetFileName(openFileDialog1.FileName);
                int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                ContentNode chdnod = new ContentNode();
                chdnod = f_convo.GetContentNodeById(cnod);
                chdnod.NodePortraitBitmap = filename;
                txtNodeImage.Text = filename;
            }
        }
        private void btnClearNodeImage_Click(object sender, EventArgs e)
        {
            int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
            ContentNode chdnod = new ContentNode();
            chdnod = f_convo.GetContentNodeById(cnod);
            chdnod.NodePortraitBitmap = "";
            txtNodeImage.Text = "";
        }
        private void chkNarrator_CheckedChanged(object sender, EventArgs e)
        {
            f_convo.Narration = chkNarrator.Checked;
        }
        private void chkPartyChat_CheckedChanged(object sender, EventArgs e)
        {
            f_convo.PartyChat = chkPartyChat.Checked;
        }
        private void chkMainPcOnly_CheckedChanged(object sender, EventArgs e)
        {
            f_convo.SpeakToMainPcOnly = chkMainPcOnly.Checked;
        }
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            //happens anywhere in the TreeView when any mouse button is pressed down
            //order is MouseDown, NodeMouseClick, AfterSelect, MouseClick
            //MessageBox.Show("MouseDown");
            PushToUndoStack();
            currentSelectedNode = null;

        }
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //happens first if a node is clicked on
            //order is MouseDown, NodeMouseClick, AfterSelect, MouseClick
            //MessageBox.Show("NodeMouseClick");
            treeView1.SelectedNode = e.Node;
            currentSelectedNode = e.Node;
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //happens only after a new selection is made (change a node selected)
            //order is MouseDown, NodeMouseClick, AfterSelect, MouseClick
            undoTextStack.Clear();
            redoTextStack.Clear();
            try
            {
                //int pnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                ContentNode nod = f_convo.GetContentNodeById(Convert.ToInt32(e.Node.Name));
                if (nod != null)
                {
                    txtText.Text = nod.conversationText;
                    txtNodeSound.Text = nod.NodeSound;
                    chkDoOnceOnly.Checked = nod.ShowOnlyOnce;
                    txtNodeNpcName.Text = nod.NodeNpcName;
                    //txtText.Text = treeView1.SelectedNode.Text.ToString();
                    txtName.Text = treeView1.SelectedNode.Name.ToString();
                    //txtParentName.Text = treeView1.SelectedNode.Parent.Text.ToString();
                    txtIndex.Text = treeView1.SelectedNode.Index.ToString();
                    txtOrderNum.Text = nod.orderNum.ToString();
                    txtLinkTo.Text = nod.linkTo.ToString();
                    txtNodeImage.Text = nod.NodePortraitBitmap;
                    if (nod.subNodes.Count == 0)
                    {
                        treeView1.SelectedNode.Text = nod.conversationText + " [END DIALOG]";
                    }
                    
                    if (nod.isLink)
                    {
                        txtText.Enabled = false;
                        txtNodeSound.Enabled = false;
                        txtNodeNpcName.Enabled = false;
                        chkDoOnceOnly.Enabled = false;
                    }
                    else
                    {
                        txtText.Enabled = true;
                        txtNodeSound.Enabled = true;
                        txtNodeNpcName.Enabled = true;
                        if (!nod.pcNode) 
                        { 
                            chkDoOnceOnly.Enabled = true; 
                        }
                        else 
                        { 
                            chkDoOnceOnly.Enabled = false; 
                        }
                    }                    
                    setupConditionsDataGridView();
                    setupActionsDataGridView();
                    //refreshConditionsTab();
                    //refreshActionsTab();
                }
            }
            catch { }
        }
        private void treeView1_MouseClick(object sender, MouseEventArgs e)
        {
            //happens anywhere clicked
            //order is MouseDown, NodeMouseClick, AfterSelect, MouseClick
            //MessageBox.Show("MouseClick");
            //refreshProperties();
        }       
        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            ContentNode nod = f_convo.GetContentNodeById(Convert.ToInt32(e.Node.Name));
            if (nod != null)
            {
                nod.IsExpanded = false;
            }
        }
        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            ContentNode nod = f_convo.GetContentNodeById(Convert.ToInt32(e.Node.Name));
            if (nod != null)
            {
                nod.IsExpanded = true;
            }
        }
        private void saveFileDlg_Click(object sender, EventArgs e)
        {
            saveConvoFile();
        }        
        private void openFileDlg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\modules\\" + ce_mod.moduleName + "\\dialog";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string g_directory = prntForm._mainDirectory + "\\modules\\" + ce_mod.moduleName + "\\dialog";
                string g_filename = Path.GetFileName(openFileDialog1.FileName);
                openFile(g_directory, g_filename);
            }
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            txtText.Text = txtText.Text.Insert(txtText.SelectionStart, cmbTokens.Text);
        }
        private void cmbTokens_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ctxTreeView_Opening(object sender, CancelEventArgs e)
        {
            if (currentSelectedNode != null)
            {
                addNodeToolStripMenuItem.Enabled = true;
                removeNodeToolStripMenuItem.Enabled = true;
            }
            else //blank area, not a parent or child
            {
                addNodeToolStripMenuItem.Enabled = false;
                removeNodeToolStripMenuItem.Enabled = false;
            }
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (undoConvoStack.Count > 0)
            {
                Convo newConvo = new Convo();
                newConvo = f_convo.Clone();
                redoConvoStack.Push(newConvo);
                f_convo = undoConvoStack.Pop();
                refreshTreeView();
            }
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redoConvoStack.Count > 0)
            {
                Convo newConvo = new Convo();
                newConvo = f_convo.Clone();
                undoConvoStack.Push(newConvo);
                f_convo = redoConvoStack.Pop();
                refreshTreeView();
            }
        }
        private void tsUndo_Click(object sender, EventArgs e)
        {
            if (undoConvoStack.Count > 0)
            {
                Convo newConvo = new Convo();
                newConvo = f_convo.Clone();
                redoConvoStack.Push(newConvo);
                f_convo = undoConvoStack.Pop();
                refreshTreeView();
            }
        }
        private void tsRedo_Click(object sender, EventArgs e)
        {
            if (redoConvoStack.Count > 0)
            {
                Convo newConvo = new Convo();
                newConvo = f_convo.Clone();
                undoConvoStack.Push(newConvo);
                f_convo = redoConvoStack.Pop();
                refreshTreeView();
            }
        }
        private void btnUndoText_Click(object sender, EventArgs e)
        {
            if (undoTextStack.Count > 0)
            {
                redoTextStack.Push(txtText.Text);
                txtText.Text = undoTextStack.Pop();
            }
        }
        private void btnRedoText_Click(object sender, EventArgs e)
        {
            if (redoTextStack.Count > 0)
            {
                undoTextStack.Push(txtText.Text);
                txtText.Text = redoTextStack.Pop();
            }
        }
        private void txtText_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Space) || (e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                undoTextStack.Push(txtText.Text);
            }
        }
        #endregion

        #region Methods 
        public void ResetOrderNumBasedOnIndex(ContentNode node)
        {
            //ContentNode returnNode = new ContentNode();
            for (int i = 0; i < node.subNodes.Count; i++)
            {
                node.subNodes[i].orderNum = i;
            }
            foreach (ContentNode n in node.subNodes)
            {
                ResetOrderNumBasedOnIndex(n);
            }
            //return returnNode;
        }
        public void PushToUndoStack()
        {
            Convo newConvo = new Convo();
            newConvo = f_convo.Clone();
            undoConvoStack.Push(newConvo);
        }
        public void AddNode()
        {
            try
            {
                //int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                //int cnod = Convert.ToInt32(currentSelectedNode.Name);
                //ContentNode chdnod = new ContentNode();
                ContentNode chdnod = f_convo.GetContentNodeById(Convert.ToInt32(treeView1.SelectedNode.Name));
                if (!chdnod.isLink)
                {
                    PushToUndoStack();

                    f_convo.NextIdNum++;
                    //TreeView Stuff
                    //TreeNode nod = new TreeNode();
                    //nod.Name = f_convo.NextIdNum.ToString();
                    //nod.Text = "Continue";
                    //treeView1.SelectedNode.Nodes.Add(nod);                    
                    //treeView1.SelectedNode.ExpandAll();

                    //ContentNode Stuff
                    ContentNode newNode = new ContentNode();
                    newNode = newNode.NewContentNode(f_convo.NextIdNum);
                    //newNode.conversationText = nod.Text;
                    //newNode.orderNum = treeView1.SelectedNode.Nodes.Count - 1;
                    newNode.orderNum = treeView1.SelectedNode.Nodes.Count;
                    ContentNode pnod = f_convo.GetContentNodeById(Convert.ToInt32(treeView1.SelectedNode.Name));
                    //int pnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                    //f_convo.GetContentNodeById(pnod).IsExpanded = true;
                    pnod.IsExpanded = true;
                    if (pnod.pcNode == true)
                    {
                        newNode.pcNode = false;
                    }
                    else
                    {
                        newNode.pcNode = true;
                    }
                    pnod.AddNodeToSubNode(newNode);
                                        
                    refreshTreeView();
                    TreeNode[] tn = treeView1.Nodes.Find(f_convo.NextIdNum.ToString(), true);
                    if (tn[0] != null)
                    {
                        treeView1.SelectedNode = tn[0];
                        currentSelectedNode = tn[0];
                    }
                }
            }
            catch (Exception ex)
            { 
                MessageBox.Show("add node failed: " + ex.ToString()); 
            }
        }
        public void RemoveNode()
        {
            try
            {
                //check to ignore if selected node is root
                if (Convert.ToInt32(treeView1.SelectedNode.Name) == 0)
                {
                    return;
                }
                                
                PushToUndoStack();

                //do a check for linked nodes pointing to this node or subnodes

                //delete any linked nodes first before deleting this node

                //find corresponding contentNode (same IdNum) and delete it and subNodes
                int rnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                int prnod = Convert.ToInt32(treeView1.SelectedNode.Parent.Name);
                ContentNode node = f_convo.GetContentNodeById(rnod);
                removeAllLinksToNodeAndSubnodes(node);
                //prntForm.logText("rnod = " + rnod.ToString());
                //prntForm.logText("prnod = " + prnod.ToString());
                f_convo.GetContentNodeById(prnod).RemoveNodeFromSubNode(f_convo.GetContentNodeById(rnod));
                //treeView1.SelectedNode.Remove();
                refreshTreeView();
                
            }
            catch { MessageBox.Show("remove node failed...make sure there are no remaining links referring to this node or sub nodes of this node. A link node pointing to a deleted node will cause problems."); }
        }
        public List<int> foundLinkedNodesIdList = new List<int>();
        public void removeAllLinksToNodeAndSubnodes(ContentNode node)
        {
            //clear find list
            foundLinkedNodesIdList.Clear();
            //find all nodes that link to this node
            findAllLinkedNodesToGivenNodeId(f_convo.subNodes[0], node.idNum);
            //delete all nodes in found list
            foreach (int id in foundLinkedNodesIdList)
            {
                ContentNode n = getParentNodeById(f_convo.subNodes[0], id);
                if (n != null)
                {
                    foreach (ContentNode sn in n.subNodes)
                    {
                        if (sn.idNum == id)
                        {
                            n.subNodes.Remove(sn);
                            break;
                        }
                    }
                }
            }

            foreach (ContentNode subNode in node.subNodes)
            {
                removeAllLinksToNodeAndSubnodes(subNode);
            }
        }
        public void findAllLinkedNodesToGivenNodeId(ContentNode node, int idPointedTo)
        {
            //go through entire convo and find all nodes that linkTo the given node 'idPointedTo'
            if (node.linkTo == idPointedTo)
            {
                foundLinkedNodesIdList.Add(node.idNum);
            }
            foreach (ContentNode subNode in node.subNodes)
            {
                findAllLinkedNodesToGivenNodeId(subNode, idPointedTo);
            }
        }
        public ContentNode getParentNodeById(ContentNode node, int idOfChild)
        {
            foreach(ContentNode subn in node.subNodes)
            {
                if (subn.idNum == idOfChild)
                {
                    return node;
                }
            }
            foreach (ContentNode sn in node.subNodes)
            {
                ContentNode n = getParentNodeById(sn, idOfChild);
                if (n != null)
                {
                    return n;
                }
            }
            return null;
        }
        public void CopyNodes()
        {
            try
            {
                int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                int prnod = Convert.ToInt32(treeView1.SelectedNode.Parent.Name);
                ContentNode chdnod = new ContentNode();
                chdnod = f_convo.GetContentNodeById(cnod);
                if (!chdnod.isLink)
                {
                    copyToClipboardNode = Convert.ToInt32(treeView1.SelectedNode.Name);
                    copyToClipboardNodeParentNode = Convert.ToInt32(treeView1.SelectedNode.Parent.Name);
                    prntForm.logText("You selected idNum = " + copyToClipboardNode.ToString() + " to copy to the clipboard");
                }
            }
            catch { MessageBox.Show("copy node failed"); }
        }
        public void PasteNodes()
        {
            pasteFromClipboardNode = Convert.ToInt32(treeView1.SelectedNode.Name);
            if (f_convo.GetContentNodeById(copyToClipboardNode).pcNode == f_convo.GetContentNodeById(pasteFromClipboardNode).pcNode)
            {
                prntForm.logText("You can't paste a PC node to a PC node or a NPC node to a NPC node");
            }
            else
            {
                try
                {
                    int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                    ContentNode chdnod = new ContentNode();
                    chdnod = f_convo.GetContentNodeById(cnod);
                    if (!chdnod.isLink)
                    {
                        PushToUndoStack();
                        prntForm.logText("You selected idNum = " + pasteFromClipboardNode.ToString() + " to paste from the clipboard");
                        ContentNode copy = duplicateNode(f_convo.GetContentNodeById(copyToClipboardNode));
                        //ContentNode copy = f_convo.GetContentNodeById(copyToClipboardNode).Clone();
                        f_convo.GetContentNodeById(pasteFromClipboardNode).AddNodeToSubNode(copy);
                        refreshTreeView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("paste node failed: " + ex.ToString());
                }
            }
        }
        public void PasteAsRelocatedNodes()
        {
            int relocateFromClipboardToThisNode = Convert.ToInt32(treeView1.SelectedNode.Name);
            if (f_convo.GetContentNodeById(copyToClipboardNode).pcNode == f_convo.GetContentNodeById(relocateFromClipboardToThisNode).pcNode)
            {
                prntForm.logText("You can't paste a PC node to a PC node or a NPC node to a NPC node");
            }
            else
            {
                try
                {
                    //get the clipboardNode
                    ContentNode clipboardNode = f_convo.GetContentNodeById(copyToClipboardNode);
                    ContentNode clipboardNodeParentNode = f_convo.GetContentNodeById(copyToClipboardNodeParentNode);
                    ContentNode relocateToNode = f_convo.GetContentNodeById(relocateFromClipboardToThisNode);
                    //add this node to the relocateToNode's subNodes list
                    //remove 
                    int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                    ContentNode chdnod = new ContentNode();
                    chdnod = f_convo.GetContentNodeById(cnod);
                    if (!chdnod.isLink)
                    {
                        PushToUndoStack();
                        prntForm.logText("You selected idNum = " + relocateFromClipboardToThisNode.ToString() + " to relocate from the clipboard");
                        ContentNode copy = duplicateNodeKeepIds(clipboardNode);
                        //ContentNode copy = f_convo.GetContentNodeById(copyToClipboardNode).Clone();
                        relocateToNode.AddNodeToSubNode(copy);
                        clipboardNodeParentNode.RemoveNodeFromSubNode(clipboardNode);
                        //f_convo.GetContentNodeById(pasteFromClipboardNode).AddNodeToSubNode(copy);
                        //f_convo.GetContentNodeById(prnod).RemoveNodeFromSubNode(f_convo.GetContentNodeById(rnod));
                        refreshTreeView();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("paste node failed: " + ex.ToString());
                }
            }
        }
        public void PasteAsLink()
        {
            pasteFromClipboardNode = Convert.ToInt32(treeView1.SelectedNode.Name);
            try
            {
                int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                ContentNode chdnod = new ContentNode();
                chdnod = f_convo.GetContentNodeById(cnod);
                if (!chdnod.isLink)
                {
                    PushToUndoStack();
                    //MessageBox.Show("You selected idNum = " + pasteFromClipboardNode.ToString() + " to paste as link from the clipboard");
                    ContentNode copy = createLinkNode(f_convo.GetContentNodeById(copyToClipboardNode));
                    copy.linkTo = copyToClipboardNode;
                    ContentNode subcnt = f_convo.GetContentNodeById(pasteFromClipboardNode);
                    copy.orderNum = subcnt.subNodes.Count;
                    f_convo.GetContentNodeById(pasteFromClipboardNode).AddNodeToSubNode(copy);
                    refreshTreeView();
                }
            }
            catch { MessageBox.Show("paste as link node failed"); }
        }
        public void FollowLink()
        {
            try
            {
                int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                ContentNode chdnod = new ContentNode();
                chdnod = f_convo.GetContentNodeById(cnod);
                TreeNode[] tn = treeView1.Nodes.Find(chdnod.linkTo.ToString(), true);
                //TreeNode[] tn = treeView1.Nodes.Find(f_convo.NextIdNum.ToString(), true);
                if (tn[0] != null)
                {
                    treeView1.SelectedNode = tn[0];
                    currentSelectedNode = tn[0];
                }
            }
            catch { MessageBox.Show("follow link node failed"); }
        }
        public void MoveUp()
        {
            if ((treeView1.SelectedNode != treeView1.Nodes[0]) && (currentSelectedNode != null))
            //if (currentSelectedNode != treeView1.Nodes[0]) 
            {
                PushToUndoStack();
                //int pnod = Convert.ToInt32(treeView1.SelectedNode.Parent.Name);
                //int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                //ContentNode parnod = new ContentNode();
                //ContentNode chdnod = new ContentNode();
                //parnod = f_convo.GetContentNodeById(pnod);
                //chdnod = f_convo.GetContentNodeById(cnod);
                ContentNode parnod = f_convo.GetContentNodeById(Convert.ToInt32(treeView1.SelectedNode.Parent.Name));
                ContentNode chdnod = f_convo.GetContentNodeById(Convert.ToInt32(treeView1.SelectedNode.Name));
                if (chdnod.orderNum > 0)
                {
                    int tempNodeIndx = chdnod.orderNum;
                    parnod.subNodes[tempNodeIndx - 1].orderNum++;
                    chdnod.orderNum--;
                    SortConversation(f_convo);
                    refreshTreeView();
                }
                TreeNode[] tn = treeView1.Nodes.Find(currentSelectedNode.Name, true);
                if (tn[0] != null)
                {
                    treeView1.SelectedNode = tn[0];
                    currentSelectedNode = tn[0];
                }
            }
        }
        public void MoveDown()
        {
            //if (treeView1.SelectedNode != treeView1.Nodes[0])
            if ((treeView1.SelectedNode != treeView1.Nodes[0]) && (currentSelectedNode != null))
            {
                PushToUndoStack();
                //int pnod = Convert.ToInt32(treeView1.SelectedNode.Parent.Name);
                //int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                //ContentNode parnod = new ContentNode();
                //ContentNode chdnod = new ContentNode();
                //parnod = f_convo.GetContentNodeById(pnod);
                //chdnod = f_convo.GetContentNodeById(cnod);
                ContentNode parnod = f_convo.GetContentNodeById(Convert.ToInt32(treeView1.SelectedNode.Parent.Name));
                ContentNode chdnod = f_convo.GetContentNodeById(Convert.ToInt32(treeView1.SelectedNode.Name));
                if (chdnod.orderNum < parnod.subNodes.Count - 1)
                {
                    int tempNodeIndx = chdnod.orderNum;
                    parnod.subNodes[tempNodeIndx + 1].orderNum--;
                    chdnod.orderNum++;
                    SortConversation(f_convo);
                    refreshTreeView();
                }
                TreeNode[] tn = treeView1.Nodes.Find(currentSelectedNode.Name, true);
                if (tn[0] != null)
                {
                    treeView1.SelectedNode = tn[0];
                    currentSelectedNode = tn[0];
                }
            }
        }
        public TreeNode PopulateTreeNode(ContentNode node)
        {
            TreeNode treenode = new TreeNode(node.conversationText);  // Add the Text to the Node
            if (node.IsExpanded) { treenode.Expand(); }
            treenode.Name = node.idNum.ToString();
            if ((node.conditions.Count > 0) && (node.actions.Count > 0))
            {
                treenode.ImageIndex = 3;
                treenode.SelectedImageIndex = 3;
            }
            else if (node.conditions.Count > 0)
            {
                treenode.ImageIndex = 1;
                treenode.SelectedImageIndex = 1;
            }
            else if (node.actions.Count > 0)
            {
                treenode.ImageIndex = 2;
                treenode.SelectedImageIndex = 2;
            }
            else
            {
                treenode.ImageIndex = 0;
                treenode.SelectedImageIndex = 0;
            }

            if (node.isLink)
            {
                treenode.Text = f_convo.GetContentNodeById(node.linkTo).conversationText;
                node.conversationText = f_convo.GetContentNodeById(node.linkTo).conversationText;
            }
            if (node.pcNode == false)
            {
                treenode.ForeColor = Color.Red;
            }
            else
            {
                treenode.ForeColor = Color.Blue;
            }
            if (node.subNodes.Count == 0)
            {
                treenode.Text = treenode.Text + " [END DIALOG]";
            }
            /*
            prntForm.logText("node IdNum = " + node.idNum.ToString());
            prntForm.logText(Environment.NewLine);
            prntForm.logText("node.subNodes.Count = " + node.subNodes.Count.ToString());
            prntForm.logText(Environment.NewLine);
            */
            for (int i = 0; i < node.subNodes.Count; i++)
            {
                /*
                prntForm.logText("node.subNodes[" + i.ToString() + "].subNodes.Count = " + node.subNodes[i].subNodes.Count.ToString());
                prntForm.logText(Environment.NewLine);
                */
                if (node.subNodes[i].subNodes.Count >= 0)
                {
                    /*
                    prntForm.logText("node.subNodes[" + i.ToString() + "].idNum = " + node.subNodes[i].idNum.ToString());
                    prntForm.logText(Environment.NewLine);
                    */
                    treenode.Nodes.Add(PopulateTreeNode(node.subNodes[i]));
                }
            }
            if (node.isLink)
            {
                treenode.ForeColor = Color.Gray;
            }
            return treenode;
        }
        public void refreshTreeView()
        {
            ResetOrderNumBasedOnIndex(f_convo.subNodes[0]);
            treeView1.Nodes.Clear();
            TreeNode treeNodes = PopulateTreeNode(f_convo.GetContentNodeById(0));
            treeView1.Nodes.Add(treeNodes);            
            //treeView1.ExpandAll();
            //treeView1.SelectedNode = treeView1.Nodes[0];
            if (currentSelectedNode != null)
            {
                treeView1.SelectedNode = currentSelectedNode;
            }
            else
            {
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
        }
        public void saveConvoFile()
        {
            try
            {
                string filenameNoExt = Path.GetFileNameWithoutExtension(ce_filename);
                f_convo.ConvoFileName = filenameNoExt;
                f_convo.SaveContentConversation(prntForm._mainDirectory + "\\modules\\" + ce_mod.moduleName + "\\dialog", ce_filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not save Convo file to disk. Original error: " + ex.Message);
            }
        }
        private void openFile(string path, string filename)
        {
            try
            {
                f_convo = f_convo.GetConversation(path, filename);
                refreshTreeView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }
        private ContentNode createLinkNode(ContentNode copiedNode)
        {
            f_convo.NextIdNum++;
            ContentNode copy = new ContentNode();
            copy.conversationText = copiedNode.conversationText;
            copy.idNum = f_convo.NextIdNum;
            return copy;
        }
        public ContentNode duplicateNode(ContentNode copiedNode)
        {
            f_convo.NextIdNum++;
            prntForm.logText("NextIdNum = " + f_convo.NextIdNum.ToString());
            ContentNode copy = new ContentNode();
            copy = copiedNode.DuplicateContentNode(f_convo.NextIdNum);
            //copy.conversationText = copiedNode.conversationText;
            copy.idNum = f_convo.NextIdNum;
            foreach (ContentNode node in copiedNode.subNodes)
            {
                copy.subNodes.Add(duplicateNode(node));
            }
            return copy;
        }
        public ContentNode duplicateNodeKeepIds(ContentNode copiedNode)
        {
            ContentNode copy = new ContentNode();
            copy = copiedNode.DuplicateContentNode();
            foreach (ContentNode node in copiedNode.subNodes)
            {
                copy.subNodes.Add(duplicateNodeKeepIds(node));
            }
            return copy;
        }      
        public static List<ContentNode> SortList(List<ContentNode> thisList)
        {
            List<ContentNode> returnList = new List<ContentNode>();
            returnList = thisList.OrderBy(o => o.orderNum).ToList();
            return returnList;            
        }
        public static void SortSubNodes(ContentNode myNode)
        {
            foreach (ContentNode subNode in myNode.subNodes)
            {
                SortSubNodes(subNode);
            }
            myNode.subNodes = SortList(myNode.subNodes);
        }
        public static void SortConversation(Convo toSort)
        {
            int i;
            if (toSort.subNodes.Count > 0)
            {
                for (i = 0; i < toSort.subNodes.Count; i++)
                {
                    SortSubNodes(toSort.subNodes[i]);
                }
            }
        }
        private void RefreshGrid(object dataSource)
        {
            CurrencyManager myCurrencyManager = (CurrencyManager)this.BindingContext[dataSource];
            myCurrencyManager.Refresh();
        }
        private void setupConditionsDataGridView()
        {
            int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
            ContentNode chdnod = new ContentNode();
            chdnod = f_convo.GetContentNodeById(cnod);

            dgvCondition.DataSource = chdnod.conditions;
            dgvCondition.AutoGenerateColumns = false;

            DataGridViewColumn column0 = new DataGridViewCheckBoxColumn();
            //column0.DataPropertyName = "Select";
            column0.HeaderText = "Select";
            column0.Name = "select";
            column0.Width = 45;

            DataGridViewColumn columnA = new DataGridViewButtonColumn();
            columnA.DataPropertyName = "c_btnAndOr";
            columnA.HeaderText = "AndOr";
            columnA.Name = "c_btnAndOr";
            columnA.Width = 45;            

            DataGridViewColumn columnB = new DataGridViewCheckBoxColumn();
            columnB.DataPropertyName = "c_not";
            columnB.HeaderText = "Not";
            columnB.Name = "c_not";
            columnB.Width = 35;            

            DataGridViewComboBoxColumn selScript = new DataGridViewComboBoxColumn();
            selScript.Items.Add("none");
            foreach (string script in scriptList)
            {
                selScript.Items.Add(script);
            }
            selScript.DefaultCellStyle.NullValue = "none";
            selScript.Name = "selectScript";
            selScript.Width = 150;

            DataGridViewColumn columnD = new DataGridViewTextBoxColumn();
            columnD.DataPropertyName = "c_parameter_1";
            columnD.HeaderText = "Parameter 1";
            columnD.Name = "c_parameter_1";
            columnD.Width = 130;

            DataGridViewColumn columnE = new DataGridViewTextBoxColumn();
            columnE.DataPropertyName = "c_parameter_2";
            columnE.HeaderText = "Parameter 2";
            columnE.Name = "c_parameter_2";
            columnE.Width = 130;

            DataGridViewColumn columnF = new DataGridViewTextBoxColumn();
            columnF.DataPropertyName = "c_parameter_3";
            columnF.HeaderText = "Parameter 3";
            columnF.Name = "c_parameter_3";
            columnF.Width = 130;

            DataGridViewColumn columnG = new DataGridViewTextBoxColumn();
            columnG.DataPropertyName = "c_parameter_4";
            columnG.HeaderText = "Parameter 4";
            columnG.Name = "c_parameter_4";
            columnG.Width = 130;

            dgvCondition.Columns.Clear();
            dgvCondition.Columns.Add(column0);
            dgvCondition.Columns.Add(columnA);
            dgvCondition.Columns.Add(columnB);
            //dgvCondition.Columns.Add(columnC);
            dgvCondition.Columns.Add(selScript);
            dgvCondition.Columns.Add(columnD);
            dgvCondition.Columns.Add(columnE);
            dgvCondition.Columns.Add(columnF);
            dgvCondition.Columns.Add(columnG);
            loadAndOrButtonValues(chdnod);
            loadValueForCondComboBoxes(chdnod);
            //loadSelectScriptButtonValues();
        }
        private void setupActionsDataGridView()
        {
            int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
            ContentNode chdnod = new ContentNode();
            chdnod = f_convo.GetContentNodeById(cnod);

            dgvAction.DataSource = chdnod.actions;
            dgvAction.AutoGenerateColumns = false;

            DataGridViewColumn column0 = new DataGridViewCheckBoxColumn();
            //column0.DataPropertyName = "Select";
            column0.HeaderText = "Select";
            column0.Name = "select";
            column0.Width = 45;

            DataGridViewComboBoxColumn selScriptAct = new DataGridViewComboBoxColumn();
            selScriptAct.Items.Add("none");
            foreach (string script in scriptList)
            {
                selScriptAct.Items.Add(script);
            }
            selScriptAct.DefaultCellStyle.NullValue = "none";
            selScriptAct.Name = "selectScript";
            selScriptAct.Width = 150;

            DataGridViewColumn columnD = new DataGridViewTextBoxColumn();
            columnD.DataPropertyName = "a_parameter_1";
            columnD.HeaderText = "Parameter 1";
            columnD.Name = "a_parameter_1";
            columnD.Width = 130;

            DataGridViewColumn columnE = new DataGridViewTextBoxColumn();
            columnE.DataPropertyName = "a_parameter_2";
            columnE.HeaderText = "Parameter 2";
            columnE.Name = "a_parameter_2";
            columnE.Width = 130;

            DataGridViewColumn columnF = new DataGridViewTextBoxColumn();
            columnF.DataPropertyName = "a_parameter_3";
            columnF.HeaderText = "Parameter 3";
            columnF.Name = "a_parameter_3";
            columnF.Width = 130;

            DataGridViewColumn columnG = new DataGridViewTextBoxColumn();
            columnG.DataPropertyName = "a_parameter_4";
            columnG.HeaderText = "Parameter 4";
            columnG.Name = "a_parameter_4";
            columnG.Width = 130;

            dgvAction.Columns.Clear();
            dgvAction.Columns.Add(column0);
            dgvAction.Columns.Add(selScriptAct);
            //dgvAction.Columns.Add(columnC);
            dgvAction.Columns.Add(columnD);
            dgvAction.Columns.Add(columnE);
            dgvAction.Columns.Add(columnF);
            dgvAction.Columns.Add(columnG);
            loadValueForActComboBoxes(chdnod);
        }
        private void loadAndOrButtonValues(ContentNode cnod)
        {
            for (int i = 0; i < dgvCondition.Rows.Count; i++)
            {
                //AndOr column is index 1
                dgvCondition[1, i].Value = cnod.conditions[i].c_btnAndOr;
            }
        }
        private void loadValueForCondComboBoxes(ContentNode cnod)
        {
            for (int i = 0; i < dgvCondition.Rows.Count; i++)
            {
                //AndOr column is index 1
                if (scriptList.Contains(cnod.conditions[i].c_script))
                {
                    dgvCondition[3, i].Value = cnod.conditions[i].c_script;
                }
                else
                {
                    prntForm.logText("[ERROR] Can't find the script that was assigned to this node, choose one from the list." + Environment.NewLine);
                }
            }
        }
        private void loadValueForActComboBoxes(ContentNode cnod)
        {
            for (int i = 0; i < dgvAction.Rows.Count; i++)
            {
                //AndOr column is index 1
                if (scriptList.Contains(cnod.actions[i].a_script))
                {
                    dgvAction[1, i].Value = cnod.actions[i].a_script;
                }
                else
                {
                    prntForm.logText("[ERROR] Can't find the script that was assigned to this node, choose one from the list." + Environment.NewLine);
                }
            }
        }
        private void fillScriptList()
        {
            scriptList.Clear();
            string jobDir = prntForm._mainDirectory + "\\default\\NewModule\\scripts";
            foreach (string f in Directory.GetFiles(jobDir, "*.cs"))
            {
                string filename = Path.GetFileName(f);
                scriptList.Add(filename);
            }            
        }
        private void loadScriptText(string scriptFilename)
        {
            //load script into rtxt for browsing
            string jobDir = "";
            if (prntForm.mod.moduleName != "NewModule")
            {
                if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\scripts\\" + scriptFilename))
                {
                    jobDir = prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\scripts";
                }
                else if (File.Exists(prntForm._mainDirectory + "\\default\\NewModule\\scripts\\" + scriptFilename))
                {
                    jobDir = prntForm._mainDirectory + "\\default\\NewModule\\scripts";
                }
                else
                {
                    prntForm.logText("couldn't find the script file." + Environment.NewLine);
                    //prntForm.game.errorLog("couldn't find the script file.");
                }
            }
            else
            {
                //jobDir = prntForm._mainDirectory + "\\data\\scripts";
            }
            try
            {
                rtxtScript.LoadFile(jobDir + "\\" + scriptFilename, RichTextBoxStreamType.PlainText);
            }
            catch (Exception ex)
            {
                prntForm.logText("Failed to preview script of the selected row." + Environment.NewLine);
                //prntForm.game.errorLog("failed to preview script of selected row: " + ex.ToString());
            }
        }
        #endregion

        #region Condition Stuff
        private void dgvCondition_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }
        private void dgvCondition_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > 0)
                {
                    if ((e.ColumnIndex == 1) && (e.RowIndex != 0)) //clicked on the button "AndOr" column
                    {
                        prntForm.logText("andbutton changed index = " + e.RowIndex.ToString() + Environment.NewLine);
                        int node = Convert.ToInt32(treeView1.SelectedNode.Name);
                        if ((string)dgvCondition[e.ColumnIndex, e.RowIndex].Value == "and")
                        {
                            dgvCondition[e.ColumnIndex, e.RowIndex].Value = "or";
                            f_convo.GetContentNodeById(node).conditions[e.RowIndex].c_and = false;
                        }
                        else
                        {
                            dgvCondition[e.ColumnIndex, e.RowIndex].Value = "and";
                            f_convo.GetContentNodeById(node).conditions[e.RowIndex].c_and = true;
                        }
                    }
                    if (e.ColumnIndex == 3) //clicked on the script column
                    {
                        MessageBox.Show("clicked on dropdown");
                    }
                    if (dgvCondition[3, e.RowIndex].Value != null)
                    {
                        try
                        {
                            loadScriptText((string)dgvCondition[3, e.RowIndex].Value);
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("failed to select cell: " + ex.ToString());
            }
        }
        private void dgvCondition_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                if (dgvCondition[3, e.RowIndex].Value != null)
                {
                    try
                    {
                        loadScriptText((string)dgvCondition[3, e.RowIndex].Value);
                    }
                    catch { }
                }
            }
        }
        private void dgvCondition_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            }
        }
        private void dgvCondition_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // This event handler manually raises the CellValueChanged event
            // by calling the CommitEdit method.
            if (dgvCondition.IsCurrentCellDirty)
            {
                dgvCondition.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dgvCondition_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCondition.RowCount > 0)
            {
                if (e.ColumnIndex == 3) //clicked on the script column
                {
                    //MessageBox.Show("clicked on dropdown");
                    //MessageBox.Show("new value: " + dgvCondition[e.ColumnIndex, e.RowIndex].Value);
                    int node = Convert.ToInt32(treeView1.SelectedNode.Name);
                    f_convo.GetContentNodeById(node).conditions[e.RowIndex].c_script = (string)dgvCondition[e.ColumnIndex, e.RowIndex].Value;
                    loadScriptText(f_convo.GetContentNodeById(node).conditions[e.RowIndex].c_script);
                    //setupConditionsDataGridView();
                }
            }
        }        
        private void btnCondAdd_Click(object sender, EventArgs e)
        {
            int node = Convert.ToInt32(treeView1.SelectedNode.Name);
            Condition newCondition = new Condition();
            newCondition.c_and = true;
            newCondition.c_btnAndOr = "and";
            newCondition.c_not = false;
            newCondition.c_script = "none";
            newCondition.c_parameter_1 = null;
            newCondition.c_parameter_2 = null;
            newCondition.c_parameter_3 = null;
            newCondition.c_parameter_4 = null;
            f_convo.GetContentNodeById(node).AddNodeToConditions(newCondition);
            RefreshGrid(f_convo.GetContentNodeById(node).conditions);
            setupConditionsDataGridView();
        }
        private void btnCondRemove_Click(object sender, EventArgs e)
        {
            int node = Convert.ToInt32(treeView1.SelectedNode.Name);
            if (dgvCondition.Rows.Count > 0)
            {
                try
                {
                    int rnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                    int rcond = -1;
                    rcond = getIndexOfSelectedCondition();
                    prntForm.logText("selected index = " + rcond.ToString() + Environment.NewLine);
                    if (rcond >= 0)
                    {
                        f_convo.GetContentNodeById(rnod).RemoveNodeFromConditions(rcond);
                    }
                    setupConditionsDataGridView();
                }
                catch { }
            }
        }
        private void btnCondMoveUp_Click(object sender, EventArgs e)
        {            
            int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
            ContentNode chdnod = new ContentNode();
            chdnod = f_convo.GetContentNodeById(cnod);
            int oldIndex = getIndexOfSelectedCondition();
            int newIndex = oldIndex - 1;

            if (newIndex >= 0)
            {
                var item = chdnod.conditions[oldIndex];
                chdnod.conditions.RemoveAt(oldIndex);
                chdnod.conditions.Insert(newIndex, item);
                setupConditionsDataGridView();
            }
        }
        private void btnCondMoveDown_Click(object sender, EventArgs e)
        {
            int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
            ContentNode chdnod = new ContentNode();
            chdnod = f_convo.GetContentNodeById(cnod);
            int oldIndex = getIndexOfSelectedCondition();
            int newIndex = oldIndex + 1;

            if ((newIndex < chdnod.conditions.Count) && (oldIndex >= 0))
            {
                var item = chdnod.conditions[oldIndex];
                chdnod.conditions.RemoveAt(oldIndex);
                //newIndex--; // the actual index could have shifted due to the removal
                chdnod.conditions.Insert(newIndex, item);
                setupConditionsDataGridView();
            }
        }
        private void btnCondCopySelected_Click(object sender, EventArgs e)
        {
            prntForm.copiedConditionalsList.Clear();
            int node = Convert.ToInt32(treeView1.SelectedNode.Name);
            if (dgvCondition.Rows.Count > 0)
            {
                try
                {
                    int rnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                    List<int> list = getAllIndexOfSelectedConditions();
                    //prntForm.logText("selected index = " + rcond.ToString() + Environment.NewLine);
                    foreach (int index in list)
                    {
                        if (index >= 0)
                        {
                            prntForm.copiedConditionalsList.Add(f_convo.GetContentNodeById(rnod).conditions[index]);
                        }
                    }
                    setupConditionsDataGridView();
                }
                catch { }
            }
        }
        private void btnCondPaste_Click(object sender, EventArgs e)
        {
            int node = Convert.ToInt32(treeView1.SelectedNode.Name);
            foreach (Condition c in prntForm.copiedConditionalsList)
            {
                f_convo.GetContentNodeById(node).conditions.Add(c.DeepCopy());
            }
            RefreshGrid(f_convo.GetContentNodeById(node).conditions);
            setupConditionsDataGridView();
        }
        private void btnCondVariables_Click(object sender, EventArgs e)
        {
            VariablesEditor varEditor = new VariablesEditor(ce_mod, prntForm);
            varEditor.ShowDialog();
        }
        private int getIndexOfSelectedCondition()
        {
            foreach (DataGridViewRow row in dgvCondition.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    return row.Index;
                }
            }
            return -1;
        }
        private List<int> getAllIndexOfSelectedConditions()
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dgvCondition.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    list.Add(row.Index);
                }
            }
            return list;
        }
        #endregion

        #region Action Stuff
        private void dgvAction_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }
        private void dgvAction_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                if (dgvAction[1, e.RowIndex].Value != null)
                {
                    try
                    {
                        loadScriptText((string)dgvAction[1, e.RowIndex].Value);
                    }
                    catch { }
                }
            }
        }
        private void dgvAction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                if (dgvAction[1, e.RowIndex].Value != null)
                {
                    try
                    {
                        loadScriptText((string)dgvAction[1, e.RowIndex].Value);
                    }
                    catch { }
                }
            }
        }
        private void dgvAction_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ((ComboBox)e.Control).DropDownStyle = ComboBoxStyle.DropDown;
                ((ComboBox)e.Control).AutoCompleteSource = AutoCompleteSource.ListItems;
                ((ComboBox)e.Control).AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            }
        }
        private void dgvAction_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // This event handler manually raises the CellValueChanged event
            // by calling the CommitEdit method.
            if (dgvAction.IsCurrentCellDirty)
            {
                dgvAction.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void dgvAction_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvAction.RowCount > 0)
            {
                if (e.ColumnIndex == 1) //clicked on the script column
                {
                    //MessageBox.Show("clicked on dropdown");
                    //MessageBox.Show("new value: " + dgvCondition[e.ColumnIndex, e.RowIndex].Value);
                    int node = Convert.ToInt32(treeView1.SelectedNode.Name);
                    f_convo.GetContentNodeById(node).actions[e.RowIndex].a_script = (string)dgvAction[e.ColumnIndex, e.RowIndex].Value;
                    loadScriptText(f_convo.GetContentNodeById(node).actions[e.RowIndex].a_script);
                    //setupConditionsDataGridView();
                }
            }
        }
        private void btnActAdd_Click(object sender, EventArgs e)
        {
            int node = Convert.ToInt32(treeView1.SelectedNode.Name);
            IB2Toolset.Action newAction = new IB2Toolset.Action();
            newAction.a_script = "none";
            newAction.a_parameter_1 = null;
            newAction.a_parameter_2 = null;
            newAction.a_parameter_3 = null;
            newAction.a_parameter_4 = null;
            f_convo.GetContentNodeById(node).AddNodeToActions(newAction);
            RefreshGrid(f_convo.GetContentNodeById(node).actions);
            setupActionsDataGridView();
        }
        private void btnActRemove_Click(object sender, EventArgs e)
        {
            int node = Convert.ToInt32(treeView1.SelectedNode.Name);
            if (dgvAction.Rows.Count > 0)
            {
                try
                {
                    int rnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                    int rcond = -1;
                    rcond = getIndexOfSelectedAction();
                    prntForm.logText("selected index = " + rcond.ToString() + Environment.NewLine);
                    if (rcond >= 0)
                    {
                        f_convo.GetContentNodeById(rnod).RemoveNodeFromActions(rcond);
                    }
                    setupActionsDataGridView();
                }
                catch { }
            }
        }
        private void btnActMoveUp_Click(object sender, EventArgs e)
        {
            int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
            ContentNode chdnod = new ContentNode();
            chdnod = f_convo.GetContentNodeById(cnod);
            int oldIndex = getIndexOfSelectedAction();
            int newIndex = oldIndex - 1;

            if (newIndex >= 0)
            {
                var item = chdnod.actions[oldIndex];
                chdnod.actions.RemoveAt(oldIndex);
                chdnod.actions.Insert(newIndex, item);
                setupActionsDataGridView();
            }
        }
        private void btnActMoveDown_Click(object sender, EventArgs e)
        {
            int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
            ContentNode chdnod = new ContentNode();
            chdnod = f_convo.GetContentNodeById(cnod);
            int oldIndex = getIndexOfSelectedAction();
            int newIndex = oldIndex + 1;

            if ((newIndex < chdnod.actions.Count) && (oldIndex >= 0))
            {
                var item = chdnod.actions[oldIndex];
                chdnod.actions.RemoveAt(oldIndex);
                chdnod.actions.Insert(newIndex, item);
                setupActionsDataGridView();
            }
        }
        private void btnActCopySelected_Click(object sender, EventArgs e)
        {
            prntForm.copiedActionsList.Clear();
            int node = Convert.ToInt32(treeView1.SelectedNode.Name);
            if (dgvAction.Rows.Count > 0)
            {
                try
                {
                    int rnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                    List<int> list = getAllIndexOfSelectedActions();
                    foreach (int index in list)
                    {
                        if (index >= 0)
                        {
                            prntForm.copiedActionsList.Add(f_convo.GetContentNodeById(rnod).actions[index]);
                        }
                    }
                    setupActionsDataGridView();
                }
                catch { }
            }
        }
        private void btnActPaste_Click(object sender, EventArgs e)
        {
            int node = Convert.ToInt32(treeView1.SelectedNode.Name);
            foreach (IB2Toolset.Action c in prntForm.copiedActionsList)
            {
                f_convo.GetContentNodeById(node).actions.Add(c.DeepCopy());
            }
            RefreshGrid(f_convo.GetContentNodeById(node).actions);
            setupActionsDataGridView();
        }
        private void btnActVariables_Click(object sender, EventArgs e)
        {
            VariablesEditor varEditor = new VariablesEditor(ce_mod, prntForm);
            varEditor.ShowDialog();
        }
        private int getIndexOfSelectedAction()
        {
            foreach (DataGridViewRow row in dgvAction.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    return row.Index;
                }
            }
            return -1;
        }
        private List<int> getAllIndexOfSelectedActions()
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dgvAction.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    list.Add(row.Index);
                }
            }
            return list;
        }
        #endregion                       
    }
}
