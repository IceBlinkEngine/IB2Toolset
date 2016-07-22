using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IB2Toolset
{
    [Serializable]
    public class ContentNode
    {        
        public int idNum = -1;
        public int orderNum = 0;
        public bool pcNode = true;
        public int linkTo = 0;
        public bool ShowOnlyOnce = false;
        public bool NodeIsActive = true;
        public string NodePortraitBitmap = "";
        public string NodeNpcName = "";
        public string NodeSound = "none";
        public string conversationText = "Continue";
        public bool IsExpanded = true;
        public List<ContentNode> subNodes = new List<ContentNode>();
        public List<Action> actions = new List<Action>();
        public List<Condition> conditions = new List<Condition>();
        public bool isLink
        {
            get
            {
                return (linkTo > 0);
            }
        }

        public ContentNode()
        {
        }
        public ContentNode NewContentNode(int nextIdNum)
        {
            ContentNode newNode = new ContentNode();
            //newNode.passRefs(game);
            newNode.idNum = nextIdNum;
            return newNode;
        }
        public void AddNodeToSubNode(ContentNode contentNode)
        {
            subNodes.Add(contentNode);
        }
        public void RemoveNodeFromSubNode(ContentNode contentNode)
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
            //MessageBox.Show("conditionNodeIndex = " + conditionNodeIndex.ToString());
            //MessageBox.Show("c_script = " + conditions[conditionNodeIndex].c_script);
            conditions.RemoveAt(conditionNodeIndex);
        }
        public ContentNode SearchContentNodeById(int checkIdNum)
        {
            ContentNode tempNode = null;
            if (idNum == checkIdNum)
            {
                return this;
            }
            foreach (ContentNode subNode in subNodes)
            {
                tempNode = subNode.SearchContentNodeById(checkIdNum);
                if (tempNode != null)
                {
                    return tempNode;
                }
            }
            return null;
        }
        public ContentNode GetContentNodeLinkedToGivenNode(int linkedToIdNum)
        {
            ContentNode tempNode = null;
            if (linkTo == linkedToIdNum)
            {
                return this;
            }
            foreach (ContentNode subNode in subNodes)
            {
                tempNode = subNode.GetContentNodeLinkedToGivenNode(linkedToIdNum);
                if (tempNode != null)
                {
                    return tempNode;
                }
            }
            return null;
        }
        public ContentNode DuplicateContentNode(int nextIdNum)
        {
            ContentNode newNode = new ContentNode();
            newNode.conversationText = this.conversationText;
            //newNode.idNum = nextIdNum;
            newNode.pcNode = this.pcNode;
            newNode.linkTo = this.linkTo;
            newNode.NodePortraitBitmap = this.NodePortraitBitmap;
            //newNode.NodeSound = this.NodeSound;
            newNode.IsExpanded = this.IsExpanded;
            newNode.ShowOnlyOnce = this.ShowOnlyOnce;
            newNode.NodeIsActive = this.NodeIsActive;
            newNode.NodeNpcName = this.NodeNpcName;

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
        public ContentNode DuplicateContentNode()
        {
            ContentNode newNode = new ContentNode();
            newNode.conversationText = this.conversationText;
            newNode.idNum = this.idNum;
            newNode.pcNode = this.pcNode;
            newNode.linkTo = this.linkTo;
            newNode.NodePortraitBitmap = this.NodePortraitBitmap;
            //newNode.NodeSound = this.NodeSound;
            newNode.IsExpanded = this.IsExpanded;
            newNode.ShowOnlyOnce = this.ShowOnlyOnce;
            newNode.NodeIsActive = this.NodeIsActive;
            newNode.NodeNpcName = this.NodeNpcName;

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
        public ContentNode Duplicate()
        {
            ContentNode copy = new ContentNode();
            copy = (ContentNode)this.MemberwiseClone();
            //copy.passRefs(game);
            //copy.conversationText = this.conversationText;
            //copy.idNum = this.idNum;
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
            foreach (ContentNode node in this.subNodes)
            {
                copy.subNodes.Add(node.Duplicate());
            }
            return copy;
        }
        public ContentNode NewContentNodeLink(int nextOrderNum)
        {
            ContentNode newNode = new ContentNode();
            //newNode.passRefs(game);
            newNode.orderNum = nextOrderNum;
            return newNode;
        }
    }

    [Serializable]
    public class Condition
    {
        private string script;
        private string btnAndOr;
        private bool and; //and = true   or = false
        private bool not; //checked = true   unchecked = false
        private string parameter_1;
        private string parameter_2;
        private string parameter_3;
        private string parameter_4;

        [XmlAttribute]
        public string c_script
        {
            get { return script; }
            set
            {
                script = value;
               
            }
        }
        [XmlAttribute]
        public string c_btnAndOr
        {
            get { return btnAndOr; }
            set
            {
                btnAndOr = value;
               
            }
        }
        [XmlAttribute]
        public bool c_and
        {
            get { return and; }
            set
            {
                and = value;
                
            }
        }
        [XmlAttribute]
        public bool c_not
        {
            get { return not; }
            set
            {
                not = value;
                
            }
        }
        [XmlAttribute]
        public string c_parameter_1
        {
            get { return parameter_1; }
            set
            {
                parameter_1 = value;
               
            }
        }
        [XmlAttribute]
        public string c_parameter_2
        {
            get { return parameter_2; }
            set
            {
                parameter_2 = value;
                
            }
        }
        [XmlAttribute]
        public string c_parameter_3
        {
            get { return parameter_3; }
            set
            {
                parameter_3 = value;
               
            }
        }
        [XmlAttribute]
        public string c_parameter_4
        {
            get { return parameter_4; }
            set
            {
                parameter_4 = value;
                
            }
        }

        public Condition()
        {
        }
        public Condition DeepCopy()
        {
            Condition other = (Condition)this.MemberwiseClone();
            return other;
        }
    }

    [Serializable]
    public class Action
    {
        private string script;
        private string parameter_1;
        private string parameter_2;
        private string parameter_3;
        private string parameter_4;

        [XmlAttribute]
        public string a_script
        {
            get { return script; }
            set
            {
                script = value;
                
            }
        }
        [XmlAttribute]
        public string a_parameter_1
        {
            get { return parameter_1; }
            set
            {
                parameter_1 = value;
                
            }
        }
        [XmlAttribute]
        public string a_parameter_2
        {
            get { return parameter_2; }
            set
            {
                parameter_2 = value;
                
            }
        }
        [XmlAttribute]
        public string a_parameter_3
        {
            get { return parameter_3; }
            set
            {
                parameter_3 = value;
                
            }
        }
        [XmlAttribute]
        public string a_parameter_4
        {
            get { return parameter_4; }
            set
            {
                parameter_4 = value;
                
            }
        }

        public Action()
        {
        }
        public Action DeepCopy()
        {
            Action other = (Action)this.MemberwiseClone();
            return other;
        }
    }
}
