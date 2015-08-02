using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace IB2Toolset
{
    /*[Serializable]
    public class LogicTree
    {
        private string filename = "";
        private int f_nextIdNum = 0;

        public string Filename
        {
            get{return filename;}
            set{filename = value;}
        }
        public List<LogicTreeNode> SubNodes = new List<LogicTreeNode>();
        public int NextIdNum
        {
            get{return f_nextIdNum;}
            set{f_nextIdNum = value;}
        }

        public LogicTree()
        {
        }
        public LogicTree GetLogicTree(string path, string FileName)
        {
            LogicTree toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(path + "\\" + FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (LogicTree)serializer.Deserialize(file, typeof(LogicTree));
            }
            return toReturn;  
        }
        public void SaveLogicTree(string path, string FileName)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(path + "\\" + FileName))
            {
                sw.Write(json.ToString());
            }            
        }
        public LogicTreeNode GetLogicTreeNodeById(int idNum)
        {
            LogicTreeNode tempNode = null;
            foreach (LogicTreeNode subNode in SubNodes)
            {
                tempNode = subNode.SearchContentNodeById(idNum);
                if (tempNode != null)
                {
                    return tempNode;
                }
            }
            return null;
        }
        public string GetTextById(int idNum)
        {
            LogicTreeNode tempNode = GetLogicTreeNodeById(idNum);
            return tempNode.nodeText;
        }
        public void AddNodeToRoot(LogicTreeNode contentNode)
        {
            SubNodes.Add(contentNode);
        }
        public void RemoveNodeFromRoot(LogicTreeNode contentNode)
        {
            SubNodes.Remove(contentNode);
        }
    }        
    */
}
