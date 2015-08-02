using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IB2Toolset
{
    /*[Serializable]
    public class LogicTreeNode
    {        
        public int idNum = -1;
        public int orderNum = 0;
        public int linkTo = 0;
        public string nodeText = "Continue";
        public bool IsExpanded = true;
        public List<LogicTreeNode> subNodes = new List<LogicTreeNode>();
        public List<Action> actions = new List<Action>();
        public List<Condition> conditions = new List<Condition>();
        public bool isLink
        {
            get
            {
                return (linkTo > 0);
            }
        }

        public LogicTreeNode()
        {
        }
        public LogicTreeNode NewContentNode(int nextIdNum)
        {
            LogicTreeNode newNode = new LogicTreeNode();
            //newNode.passRefs(game);
            newNode.idNum = nextIdNum;
            return newNode;
        }
        public void AddNodeToSubNode(LogicTreeNode contentNode)
        {
            subNodes.Add(contentNode);
        }
        public void RemoveNodeFromSubNode(LogicTreeNode contentNode)
        {
            bool returnvalue = subNodes.Remove(contentNode);
        }
        public void AddNodeToActions(Action actionNode)
        {
            actions.Add(actionNode);
        }
        public void RemoveNodeFromActions(int actionNodeIndex)
        {
            actions.RemoveAt(actionNodeIndex);
        }
        public void AddNodeToConditions(Condition conditionNode)
        {
            conditions.Add(conditionNode);
        }
        public void RemoveNodeFromConditions(int conditionNodeIndex)
        {
            conditions.RemoveAt(conditionNodeIndex);
        }
        public LogicTreeNode SearchContentNodeById(int checkIdNum)
        {
            LogicTreeNode tempNode = null;
            if (idNum == checkIdNum)
            {
                return this;
            }
            foreach (LogicTreeNode subNode in subNodes)
            {
                tempNode = subNode.SearchContentNodeById(checkIdNum);
                if (tempNode != null)
                {
                    return tempNode;
                }
            }
            return null;
        }
        public LogicTreeNode DuplicateContentNode(int nextIdNum)
        {
            LogicTreeNode newNode = new LogicTreeNode();
            newNode.nodeText = this.nodeText;
            newNode.linkTo = this.linkTo;
            newNode.IsExpanded = this.IsExpanded;
            
            newNode.actions = new List<Action>();
            foreach (Action a in this.actions)
            {
                Action ac = a.DeepCopy();
                newNode.actions.Add(ac);
            }
            newNode.conditions = new List<Condition>();
            foreach (Condition c in this.conditions)
            {
                Condition cc = c.DeepCopy();
                newNode.conditions.Add(cc);
            }

            return newNode;
        }
        public LogicTreeNode DuplicateContentNode()
        {
            LogicTreeNode newNode = new LogicTreeNode();
            newNode.nodeText = this.nodeText;
            newNode.idNum = this.idNum;
            newNode.linkTo = this.linkTo;
            newNode.IsExpanded = this.IsExpanded;
            
            newNode.actions = new List<Action>();
            foreach (Action a in this.actions)
            {
                Action ac = a.DeepCopy();
                newNode.actions.Add(ac);
            }
            newNode.conditions = new List<Condition>();
            foreach (Condition c in this.conditions)
            {
                Condition cc = c.DeepCopy();
                newNode.conditions.Add(cc);
            }

            return newNode;
        }
        public LogicTreeNode Duplicate()
        {
            LogicTreeNode copy = new LogicTreeNode();
            copy = (LogicTreeNode)this.MemberwiseClone();
            
            copy.actions = new List<Action>();
            foreach (Action a in this.actions)
            {
                Action ac = a.DeepCopy();
                copy.actions.Add(ac);
            }
            copy.conditions = new List<Condition>();
            foreach (Condition c in this.conditions)
            {
                Condition cc = c.DeepCopy();
                copy.conditions.Add(cc);
            }
            foreach (LogicTreeNode node in this.subNodes)
            {
                copy.subNodes.Add(node.Duplicate());
            }
            return copy;
        }
        public LogicTreeNode NewContentNodeLink(int nextOrderNum)
        {
            LogicTreeNode newNode = new LogicTreeNode();
            newNode.orderNum = nextOrderNum;
            return newNode;
        }
    }
    */
}
