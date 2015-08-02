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
    /*public partial class LogicTreeEditor : DockContent
    { 
        public LogicTree lt;
        public Module mod;
        public ParentForm prntForm;      
        int copyToClipboardNode = 1;
        int copyToClipboardNodeParentNode = 1;
        int pasteFromClipboardNode = 1;
        TreeNode currentSelectedNode = new TreeNode();
        public string ce_filename;
        private List<string> scriptList = new List<string>();
        public Stack<LogicTree> undoConvoStack = new Stack<LogicTree>();
        public Stack<LogicTree> redoConvoStack = new Stack<LogicTree>();
        public Stack<string> undoTextStack = new Stack<string>();
        public Stack<string> redoTextStack = new Stack<string>();
        public string lastTextStored = "";


        public LogicTreeEditor(Module m, ParentForm p)
        {
            InitializeComponent();
            lt = new LogicTree();
            mod = m;
            prntForm = p;
        }

        #region Handlers        
        private void LogicTreeEditor_Load(object sender, EventArgs e)
        {            
            // load the file that has the selected node name if it exists
            string filenameOnly = mod.moduleLogicTreesList[prntForm._selectedLbxLogicTreeIndex] + ".json";
            string dirFullPath = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\logictree";
            if (File.Exists(dirFullPath + "\\" + filenameOnly))
            {
                openFile(dirFullPath, filenameOnly);
                if (lt == null)
                {
                    lt = new LogicTree();
                    lt.Filename = filenameOnly;
                    LogicTreeNode contentNode = new LogicTreeNode();
                    contentNode.idNum = lt.NextIdNum;
                    contentNode.nodeText = "root";
                    lt.AddNodeToRoot(contentNode);
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
                lt.Filename = filenameOnly;
                LogicTreeNode contentNode = new LogicTreeNode();
                contentNode.idNum = lt.NextIdNum;
                contentNode.nodeText = "root";
                lt.AddNodeToRoot(contentNode);
                TreeNode mainNode = new TreeNode();
                mainNode.Name = "0";
                mainNode.Text = "root";
                treeView1.Nodes.Add(mainNode);
            }
            currentSelectedNode = null;
            SortLogicTree(lt);
            ResetOrderNumBasedOnIndex(lt.SubNodes[0]);
            prntForm.openLogicTreesList.Add(lt);
            fillScriptList();            
        }
        private void ConvoEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MessageBox.Show("closing editor and removing from openConvosList");
            prntForm.openLogicTreesList.Remove(lt);
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
        private void txtText_TextChanged(object sender, EventArgs e)
        {
            //check to ignore if selected node is root
            if (Convert.ToInt32(treeView1.SelectedNode.Name) != 0)
            {
                treeView1.SelectedNode.Text = txtText.Text;
                int pnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                lt.GetLogicTreeNodeById(pnod).nodeText = txtText.Text;
            }
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
                LogicTreeNode nod = lt.GetLogicTreeNodeById(Convert.ToInt32(e.Node.Name));
                if (nod != null)
                {
                    txtText.Text = nod.nodeText;
                    txtName.Text = treeView1.SelectedNode.Name.ToString();
                    txtIndex.Text = treeView1.SelectedNode.Index.ToString();
                    txtOrderNum.Text = nod.orderNum.ToString();
                    txtLinkTo.Text = nod.linkTo.ToString();

                    treeView1.SelectedNode.Text = nod.nodeText + "  ";

                    if (!chkShowTextOnly.Checked)
                    {
                        //create a string of all the conditions and actions and insert to beginning of treenode.Text
                        string condactText = "";
                        foreach (Condition c in nod.conditions)
                        {
                            condactText += "[" + c.c_script
                                + "(" + c.c_parameter_1 + ","
                                + c.c_parameter_2 + ","
                                + c.c_parameter_3 + ","
                                + c.c_parameter_4 + ")] ";
                        }
                        foreach (Action a in nod.actions)
                        {
                            condactText += "[" + a.a_script
                                + "(" + a.a_parameter_1 + ","
                                + a.a_parameter_2 + ","
                                + a.a_parameter_3 + ","
                                + a.a_parameter_4 + ")] ";
                        }
                        treeView1.SelectedNode.Text += condactText;
                    }

                    if (nod.subNodes.Count == 0)
                    {
                        treeView1.SelectedNode.Text += " [END LOGIC TREE]";
                    }

                    if (nod.isLink)
                    {
                        txtText.Enabled = false;                        
                    }
                    else
                    {
                        txtText.Enabled = true;
                    }                    
                    setupConditionsDataGridView();
                    setupActionsDataGridView();
                }
            }
            catch { }
        }
        private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            LogicTreeNode nod = lt.GetLogicTreeNodeById(Convert.ToInt32(e.Node.Name));
            if (nod != null)
            {
                nod.IsExpanded = false;
            }
        }
        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {
            LogicTreeNode nod = lt.GetLogicTreeNodeById(Convert.ToInt32(e.Node.Name));
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

            openFileDialog1.InitialDirectory = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\logictree";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string g_directory = prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\logictree";
                string g_filename = Path.GetFileName(openFileDialog1.FileName);
                openFile(g_directory, g_filename);
            }
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
                LogicTree newConvo = new LogicTree();
                newConvo = lt.Clone();
                redoConvoStack.Push(newConvo);
                lt = undoConvoStack.Pop();
                refreshTreeView();
            }
        }
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (redoConvoStack.Count > 0)
            {
                LogicTree newConvo = new LogicTree();
                newConvo = lt.Clone();
                undoConvoStack.Push(newConvo);
                lt = redoConvoStack.Pop();
                refreshTreeView();
            }
        }
        private void tsUndo_Click(object sender, EventArgs e)
        {
            if (undoConvoStack.Count > 0)
            {
                LogicTree newConvo = new LogicTree();
                newConvo = lt.Clone();
                redoConvoStack.Push(newConvo);
                lt = undoConvoStack.Pop();
                refreshTreeView();
            }
        }
        private void tsRedo_Click(object sender, EventArgs e)
        {
            if (redoConvoStack.Count > 0)
            {
                LogicTree newConvo = new LogicTree();
                newConvo = lt.Clone();
                undoConvoStack.Push(newConvo);
                lt = redoConvoStack.Pop();
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
        public void ResetOrderNumBasedOnIndex(LogicTreeNode node)
        {
            //ContentNode returnNode = new ContentNode();
            for (int i = 0; i < node.subNodes.Count; i++)
            {
                node.subNodes[i].orderNum = i;
            }
            foreach (LogicTreeNode n in node.subNodes)
            {
                ResetOrderNumBasedOnIndex(n);
            }
            //return returnNode;
        }
        public void PushToUndoStack()
        {
            LogicTree newConvo = new LogicTree();
            newConvo = lt.Clone();
            undoConvoStack.Push(newConvo);
        }
        public void AddNode()
        {
            try
            {
                LogicTreeNode chdnod = lt.GetLogicTreeNodeById(Convert.ToInt32(treeView1.SelectedNode.Name));
                if (!chdnod.isLink)
                {
                    PushToUndoStack();

                    lt.NextIdNum++;
                    
                    //ContentNode Stuff
                    LogicTreeNode newNode = new LogicTreeNode();
                    newNode = newNode.NewContentNode(lt.NextIdNum);
                    newNode.orderNum = treeView1.SelectedNode.Nodes.Count;
                    LogicTreeNode pnod = lt.GetLogicTreeNodeById(Convert.ToInt32(treeView1.SelectedNode.Name));
                    pnod.IsExpanded = true;
                    pnod.AddNodeToSubNode(newNode);
                                        
                    refreshTreeView();
                    TreeNode[] tn = treeView1.Nodes.Find(lt.NextIdNum.ToString(), true);
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
                if (Convert.ToInt32(treeView1.SelectedNode.Name) != 0)
                {
                    PushToUndoStack();

                    //find corresponding contentNode (same IdNum) and delete it and subNodes
                    int rnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                    int prnod = Convert.ToInt32(treeView1.SelectedNode.Parent.Name);
                    lt.GetLogicTreeNodeById(prnod).RemoveNodeFromSubNode(lt.GetLogicTreeNodeById(rnod));
                    //treeView1.SelectedNode.Remove();
                    refreshTreeView();
                }
            }
            catch { MessageBox.Show("remove node failed"); }
        }
        public void CopyNodes()
        {
            try
            {
                int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                int prnod = Convert.ToInt32(treeView1.SelectedNode.Parent.Name);
                LogicTreeNode chdnod = new LogicTreeNode();
                chdnod = lt.GetLogicTreeNodeById(cnod);
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
            try
            {
                int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                LogicTreeNode chdnod = new LogicTreeNode();
                chdnod = lt.GetLogicTreeNodeById(cnod);
                if (!chdnod.isLink)
                {
                    PushToUndoStack();
                    prntForm.logText("You selected idNum = " + pasteFromClipboardNode.ToString() + " to paste from the clipboard");
                    LogicTreeNode copy = duplicateNode(lt.GetLogicTreeNodeById(copyToClipboardNode));
                    lt.GetLogicTreeNodeById(pasteFromClipboardNode).AddNodeToSubNode(copy);
                    refreshTreeView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("paste node failed: " + ex.ToString());
            }            
        }
        public void PasteAsRelocatedNodes()
        {
            int relocateFromClipboardToThisNode = Convert.ToInt32(treeView1.SelectedNode.Name);
            try
            {
                //get the clipboardNode
                LogicTreeNode clipboardNode = lt.GetLogicTreeNodeById(copyToClipboardNode);
                LogicTreeNode clipboardNodeParentNode = lt.GetLogicTreeNodeById(copyToClipboardNodeParentNode);
                LogicTreeNode relocateToNode = lt.GetLogicTreeNodeById(relocateFromClipboardToThisNode);
                //add this node to the relocateToNode's subNodes list
                //remove 
                int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                LogicTreeNode chdnod = new LogicTreeNode();
                chdnod = lt.GetLogicTreeNodeById(cnod);
                if (!chdnod.isLink)
                {
                    PushToUndoStack();
                    prntForm.logText("You selected idNum = " + relocateFromClipboardToThisNode.ToString() + " to relocate from the clipboard");
                    LogicTreeNode copy = duplicateNodeKeepIds(clipboardNode);
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
        public void PasteAsLink()
        {
            pasteFromClipboardNode = Convert.ToInt32(treeView1.SelectedNode.Name);
            try
            {
                int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
                LogicTreeNode chdnod = new LogicTreeNode();
                chdnod = lt.GetLogicTreeNodeById(cnod);
                if (!chdnod.isLink)
                {
                    PushToUndoStack();
                    //MessageBox.Show("You selected idNum = " + pasteFromClipboardNode.ToString() + " to paste as link from the clipboard");
                    LogicTreeNode copy = createLinkNode(lt.GetLogicTreeNodeById(copyToClipboardNode));
                    copy.linkTo = copyToClipboardNode;
                    LogicTreeNode subcnt = lt.GetLogicTreeNodeById(pasteFromClipboardNode);
                    copy.orderNum = subcnt.subNodes.Count;
                    lt.GetLogicTreeNodeById(pasteFromClipboardNode).AddNodeToSubNode(copy);
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
                LogicTreeNode chdnod = new LogicTreeNode();
                chdnod = lt.GetLogicTreeNodeById(cnod);
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
            {
                PushToUndoStack();
                LogicTreeNode parnod = lt.GetLogicTreeNodeById(Convert.ToInt32(treeView1.SelectedNode.Parent.Name));
                LogicTreeNode chdnod = lt.GetLogicTreeNodeById(Convert.ToInt32(treeView1.SelectedNode.Name));
                if (chdnod.orderNum > 0)
                {
                    int tempNodeIndx = chdnod.orderNum;
                    parnod.subNodes[tempNodeIndx - 1].orderNum++;
                    chdnod.orderNum--;
                    SortLogicTree(lt);
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
            if ((treeView1.SelectedNode != treeView1.Nodes[0]) && (currentSelectedNode != null))
            {
                PushToUndoStack();
                LogicTreeNode parnod = lt.GetLogicTreeNodeById(Convert.ToInt32(treeView1.SelectedNode.Parent.Name));
                LogicTreeNode chdnod = lt.GetLogicTreeNodeById(Convert.ToInt32(treeView1.SelectedNode.Name));
                if (chdnod.orderNum < parnod.subNodes.Count - 1)
                {
                    int tempNodeIndx = chdnod.orderNum;
                    parnod.subNodes[tempNodeIndx + 1].orderNum--;
                    chdnod.orderNum++;
                    SortLogicTree(lt);
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
        public TreeNode PopulateTreeNode(LogicTreeNode node)
        {
            TreeNode treenode = new TreeNode(node.nodeText);  // Add the Text to the Node
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
                treenode.Text = lt.GetLogicTreeNodeById(node.linkTo).nodeText;
                node.nodeText = lt.GetLogicTreeNodeById(node.linkTo).nodeText;
            }
            treenode.ForeColor = Color.Blue;            

            if (!chkShowTextOnly.Checked)
            {
                //create a string of all the conditions and actions and insert to beginning of treenode.Text
                string condactText = "";
                foreach (Condition c in node.conditions)
                {
                    condactText += "[" + c.c_script
                        + "(" + c.c_parameter_1 + ","
                        + c.c_parameter_2 + ","
                        + c.c_parameter_3 + ","
                        + c.c_parameter_4 + ")] ";
                }
                foreach (Action a in node.actions)
                {
                    condactText += "[" + a.a_script
                        + "(" + a.a_parameter_1 + ","
                        + a.a_parameter_2 + ","
                        + a.a_parameter_3 + ","
                        + a.a_parameter_4 + ")] ";
                }
                treenode.Text += "  " + condactText;
            }

            if (node.subNodes.Count == 0)
            {
                treenode.Text += " [END LOGIC TREE]";
            }

            for (int i = 0; i < node.subNodes.Count; i++)
            {
                if (node.subNodes[i].subNodes.Count >= 0)
                {
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
            ResetOrderNumBasedOnIndex(lt.SubNodes[0]);
            treeView1.Nodes.Clear();
            TreeNode treeNodes = PopulateTreeNode(lt.GetLogicTreeNodeById(0));
            treeView1.Nodes.Add(treeNodes);            
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
                lt.Filename = filenameNoExt;
                lt.SaveLogicTree(prntForm._mainDirectory + "\\modules\\" + mod.moduleName + "\\logictree", ce_filename);
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
                lt = lt.GetLogicTree(path, filename);
                refreshTreeView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
            }
        }
        private LogicTreeNode createLinkNode(LogicTreeNode copiedNode)
        {
            lt.NextIdNum++;
            LogicTreeNode copy = new LogicTreeNode();
            copy.nodeText = copiedNode.nodeText;
            copy.idNum = lt.NextIdNum;
            return copy;
        }
        public LogicTreeNode duplicateNode(LogicTreeNode copiedNode)
        {
            lt.NextIdNum++;
            prntForm.logText("NextIdNum = " + lt.NextIdNum.ToString());
            LogicTreeNode copy = new LogicTreeNode();
            copy = copiedNode.DuplicateContentNode(lt.NextIdNum);
            //copy.conversationText = copiedNode.conversationText;
            copy.idNum = lt.NextIdNum;
            foreach (LogicTreeNode node in copiedNode.subNodes)
            {
                copy.subNodes.Add(duplicateNode(node));
            }
            return copy;
        }
        public LogicTreeNode duplicateNodeKeepIds(LogicTreeNode copiedNode)
        {
            LogicTreeNode copy = new LogicTreeNode();
            copy = copiedNode.DuplicateContentNode();
            foreach (LogicTreeNode node in copiedNode.subNodes)
            {
                copy.subNodes.Add(duplicateNodeKeepIds(node));
            }
            return copy;
        }
        public static List<LogicTreeNode> SortList(List<LogicTreeNode> thisList)
        {
            List<LogicTreeNode> returnList = new List<LogicTreeNode>();
            returnList = thisList.OrderBy(o => o.orderNum).ToList();
            return returnList;            
        }
        public static void SortSubNodes(LogicTreeNode myNode)
        {
            foreach (LogicTreeNode subNode in myNode.subNodes)
            {
                SortSubNodes(subNode);
            }
            myNode.subNodes = SortList(myNode.subNodes);
        }
        public static void SortLogicTree(LogicTree toSort)
        {
            int i;
            if (toSort.SubNodes.Count > 0)
            {
                for (i = 0; i < toSort.SubNodes.Count; i++)
                {
                    SortSubNodes(toSort.SubNodes[i]);
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
            LogicTreeNode chdnod = new LogicTreeNode();
            chdnod = lt.GetLogicTreeNodeById(cnod);

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
            LogicTreeNode chdnod = new LogicTreeNode();
            chdnod = lt.GetLogicTreeNodeById(cnod);

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
        private void loadAndOrButtonValues(LogicTreeNode cnod)
        {
            for (int i = 0; i < dgvCondition.Rows.Count; i++)
            {
                //AndOr column is index 1
                dgvCondition[1, i].Value = cnod.conditions[i].c_btnAndOr;
            }
        }
        private void loadValueForCondComboBoxes(LogicTreeNode cnod)
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
        private void loadValueForActComboBoxes(LogicTreeNode cnod)
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
                            lt.GetLogicTreeNodeById(node).conditions[e.RowIndex].c_and = false;
                        }
                        else
                        {
                            dgvCondition[e.ColumnIndex, e.RowIndex].Value = "and";
                            lt.GetLogicTreeNodeById(node).conditions[e.RowIndex].c_and = true;
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
                    lt.GetLogicTreeNodeById(node).conditions[e.RowIndex].c_script = (string)dgvCondition[e.ColumnIndex, e.RowIndex].Value;
                    loadScriptText(lt.GetLogicTreeNodeById(node).conditions[e.RowIndex].c_script);
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
            lt.GetLogicTreeNodeById(node).AddNodeToConditions(newCondition);
            RefreshGrid(lt.GetLogicTreeNodeById(node).conditions);
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
                        lt.GetLogicTreeNodeById(rnod).RemoveNodeFromConditions(rcond);
                    }
                    setupConditionsDataGridView();
                }
                catch { }
            }
        }
        private void btnCondMoveUp_Click(object sender, EventArgs e)
        {            
            int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
            LogicTreeNode chdnod = new LogicTreeNode();
            chdnod = lt.GetLogicTreeNodeById(cnod);
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
            LogicTreeNode chdnod = new LogicTreeNode();
            chdnod = lt.GetLogicTreeNodeById(cnod);
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
                    lt.GetLogicTreeNodeById(node).actions[e.RowIndex].a_script = (string)dgvAction[e.ColumnIndex, e.RowIndex].Value;
                    loadScriptText(lt.GetLogicTreeNodeById(node).actions[e.RowIndex].a_script);
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
            lt.GetLogicTreeNodeById(node).AddNodeToActions(newAction);
            RefreshGrid(lt.GetLogicTreeNodeById(node).actions);
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
                        lt.GetLogicTreeNodeById(rnod).RemoveNodeFromActions(rcond);
                    }
                    setupActionsDataGridView();
                }
                catch { }
            }
        }
        private void btnActMoveUp_Click(object sender, EventArgs e)
        {
            int cnod = Convert.ToInt32(treeView1.SelectedNode.Name);
            LogicTreeNode chdnod = new LogicTreeNode();
            chdnod = lt.GetLogicTreeNodeById(cnod);
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
            LogicTreeNode chdnod = new LogicTreeNode();
            chdnod = lt.GetLogicTreeNodeById(cnod);
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
        #endregion                       

        private void btnCondVariables_Click(object sender, EventArgs e)
        {
            //VariablesEditor varEditor = new VariablesEditor(ce_mod, prntForm);
            //varEditor.ShowDialog();
        }
        private void btnActVariables_Click(object sender, EventArgs e)
        {
            //VariablesEditor varEditor = new VariablesEditor(ce_mod, prntForm);
            //varEditor.ShowDialog();
        }

        private void chkShowTextOnly_CheckedChanged(object sender, EventArgs e)
        {
            refreshTreeView();
        }
    }
    */
}
