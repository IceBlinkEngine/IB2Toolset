using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
//using IceBlink;

namespace IB2Toolset
{
    /*public class Containers
    {
        //[XmlArrayItem("Containers")]
        public List<Container> containers = new List<Container>();
        public Containers()
        {
        }
        public void saveContainersFile(string filename)
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public Containers loadContainersFile(string filename)
        {
            Containers toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (Containers)serializer.Deserialize(file, typeof(Containers));
            }
            return toReturn;
        }
        public Container getContainer(string tag)
        {
            foreach (Container cont in containers)
            {
                if (cont.containerTag == tag) return cont;
            }
            return null;
        }
    }*/

    public class Container
    {
        private string _containerTag = ""; //container tag
        private List<ItemRefs> _containerItemRefs = new List<ItemRefs>();

        public string containerTag
        {
            get { return _containerTag; }
            set { _containerTag = value; }
        }
        public List<ItemRefs> containerItemRefs
        {
            get { return _containerItemRefs; }
            set { _containerItemRefs = value; }
        }

        public Container()
        {
        }
        public override string ToString()
        {
            return containerTag;
        }
        public Container DeepCopy()
        {
            Container other = (Container)this.MemberwiseClone();
            other.containerItemRefs = new List<ItemRefs>();
            for (int i = 0; i < this.containerItemRefs.Count; i++)
            {
                other.containerItemRefs.Add(this.containerItemRefs[i].DeepCopy());
            }
            return other;
        }
    }
}
