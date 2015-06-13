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
    [Serializable]
    public class Convo
    {
        private string convoFileName = "";
        private bool narration = false;
        private bool partyChat = false;
        private bool speakToMainPcOnly = false;
        private string portraitBitmap = "";
        private string defaultNpcName = "";
        private int f_nextIdNum = 0;

        public string ConvoFileName
        {
            get
            {
                return convoFileName;
            }
            set
            {
                convoFileName = value;
            }
        }
        public bool Narration
        {
            get
            {
                return narration;
            }
            set
            {
                narration = value;
            }
        }
        public bool PartyChat
        {
            get
            {
                return partyChat;
            }
            set
            {
                partyChat = value;
            }
        }
        public bool SpeakToMainPcOnly
        {
            get
            {
                return speakToMainPcOnly;
            }
            set
            {
                speakToMainPcOnly = value;
            }
        }
        public string NpcPortraitBitmap
        {
            get
            {
                return portraitBitmap;
            }
            set
            {
                portraitBitmap = value;
            }
        }
        public string DefaultNpcName
        {
            get
            {
                return defaultNpcName;
            }
            set
            {
                defaultNpcName = value;
            }
        }
        public List<ContentNode> subNodes = new List<ContentNode>();
        public int NextIdNum
        {
            get
            {
                return f_nextIdNum;
            }
            set
            {
                f_nextIdNum = value;
            }
        }

        public Convo()
        {
        }
        public Convo GetConversation(string path, string FileName)
        {
            Convo toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(path + "\\" + FileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (Convo)serializer.Deserialize(file, typeof(Convo));
            }
            return toReturn;  
        }
        public void SaveContentConversation(string path, string FileName)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(path + "\\" + FileName))
            {
                sw.Write(json.ToString());
            }            
        }
        public ContentNode GetContentNodeById(int idNum)
        {
            ContentNode tempNode = null;
            foreach (ContentNode subNode in subNodes)
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
            ContentNode tempNode = GetContentNodeById(idNum);
            return tempNode.conversationText;
        }
        public void AddNodeToRoot(ContentNode contentNode)
        {
            subNodes.Add(contentNode);
        }
        public void RemoveNodeFromRoot(ContentNode contentNode)
        {
            subNodes.Remove(contentNode);
        }
    }

    [Serializable]
    public class ConvoSavedValues
    {
        private string convoFileName = "";
        private int nodeNotActiveIdNum = -1;

        [XmlElement]
        public string ConvoFileName
        {
            get { return convoFileName; }
            set { convoFileName = value; }
        }
        [XmlElement]
        public int NodeNotActiveIdNum
        {
            get { return nodeNotActiveIdNum; }
            set { nodeNotActiveIdNum = value; }
        }

        public ConvoSavedValues()
        {
        }
    }    
}
