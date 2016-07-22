using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using Newtonsoft.Json;
//using IceBlink;

namespace IB2Toolset
{
    /*public class Journal
    {        
        private int nextIdNum = 0;
        public int NextIdNum
        {
            get 
            {
                nextIdNum++;
                return nextIdNum;
            }
            set { nextIdNum = value; }
        }

        public List<JournalQuest> categories = new List<JournalQuest>();

        public Journal()
        {
        }
        public void saveJournalFile(string filename)
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public Journal loadJournalFile(string filename)
        {
            Journal toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (Journal)serializer.Deserialize(file, typeof(Journal));
            }
            return toReturn;
        }
        public JournalQuest getJournalCategoryByName(string name)
        {
            foreach (JournalQuest it in categories)
            {
                if (it.Name == name) return it;
            }
            return null;
        }
        public JournalQuest getJournalCategoryByTag(string tag)
        {
            foreach (JournalQuest it in categories)
            {
                if (it.Tag == tag) return it;
            }
            return null;
        }
        public Journal DeepCopy()
        {
            Journal other = (Journal)this.MemberwiseClone();
            other.categories = new List<JournalQuest>();
            foreach (JournalQuest jcat in this.categories)
            {
                JournalQuest j = jcat.DeepCopy();
                other.categories.Add(j);
            }
            return other;
        }
    }*/

    public class JournalQuest
    {        
        #region Fields
        //private int orderIndex = 0;
        private string name = "newCategory";
        private string tag = "tag";
        private List<JournalEntry> entries = new List<JournalEntry>();
        #endregion

        #region Properties
        /*[CategoryAttribute("JournalCategory"), DescriptionAttribute("index"), ReadOnly(true)]
        public int OrderIndex
        {
            get { return orderIndex; }
            set { orderIndex = value; }
        }*/
        [CategoryAttribute("JournalCategory"), DescriptionAttribute("Name of the quest. Will be used as the title of the quest in the player's journal.")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [CategoryAttribute("JournalCategory"), DescriptionAttribute("Tag of the Category (Must be unique)")]
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }        
        [CategoryAttribute("JournalCategory"), ReadOnly(true)]
        public List<JournalEntry> Entries
        {
            get { return entries; }
            set { entries = value; }
        }       
        #endregion

        public JournalQuest()
        {
        }
        public override string ToString()
        {
            return Name;
        }
        public JournalEntry getJournalEntryByTag(string tag)
        {
            foreach (JournalEntry it in entries)
            {
                if (it.Tag == tag) return it;
            }
            return null;
        }
        public JournalQuest ShallowCopy()
        {
            return (JournalQuest)this.MemberwiseClone();
        }
        public JournalQuest DeepCopy()
        {
            JournalQuest other = (JournalQuest)this.MemberwiseClone();
            other.Entries = new List<JournalEntry>();
            foreach (JournalEntry jent in this.entries)
            {
                JournalEntry j = jent.DeepCopy();
                other.Entries.Add(j);
            }
            return other;
        }        
    }

    public class JournalEntry
    {

        #region Fields
        //private int orderIndex;
        private string entryTitle = "newTitle";
        private string entryText = "quest entry text";
        private string tag = "tag";
        private int entryId = 0;
        private bool endPoint = false;
        #endregion

        #region Properties
        /*[CategoryAttribute("JournalEntry"), DescriptionAttribute("index"), ReadOnly(true)]
        public int OrderIndex
        {
            get { return orderIndex; }
            set { orderIndex = value; }
        }*/
        [CategoryAttribute("JournalEntry"), DescriptionAttribute("Journal entry title for this node of the quest. This is the entry's title that will show up in the player's Journal.")]
        public string EntryTitle
        {
            get { return entryTitle; }
            set { entryTitle = value; }
        }
        [CategoryAttribute("JournalEntry"), DescriptionAttribute("Journal entry text for this node of the quest. This is the quest information that will show up in the player's Journal.")]
        public string EntryText
        {
            get { return entryText; }
            set { entryText = value; }
        }
        [CategoryAttribute("JournalEntry"), DescriptionAttribute("Tag of the entry (Must be unique within this Journal Category...for example, can use '10' in all Journal Categories, but only once in each Category.)")]
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }
        [CategoryAttribute("JournalEntry"), DescriptionAttribute("Assign an integer that can be used to do comparison checks on the current state of the quest with the gcCheckJournalEntry script.")]
        public int EntryId
        {
            get { return entryId; }
            set { entryId = value; }
        }
        [CategoryAttribute("JournalEntry"), DescriptionAttribute("TRUE means that the quest is considered completed upon reaching this entry. FALSE means that this quest is still active upon reaching this entry.")]
        public bool EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }
        #endregion

        public JournalEntry()
        {
            //this.OrderIndex = index;
            //this.Tag = "EntryTag" + nextID.ToString();
        }
        public JournalEntry ShallowCopy()
        {
            return (JournalEntry)this.MemberwiseClone();
        }
        public JournalEntry DeepCopy()
        {
            JournalEntry other = (JournalEntry)this.MemberwiseClone();
            return other;
        }
    }
}
