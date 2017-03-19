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
    /*[Serializable]
    public class Shops
    {
        //[XmlArrayItem("Shops")]
        public List<Shop> shopsList = new List<Shop>();
        
        public Shops()
        {
        }
        public void saveShopsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public Shops loadShopsFile(string filename)
        {
            Shops toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (Shops)serializer.Deserialize(file, typeof(Shops));
            }
            return toReturn;
        }
        public Shop getShopByTag(string tag)
        {
            foreach (Shop shp in shopsList)
            {
                if (shp.shopTag == tag)
                {
                    return shp;
                }
            }
            return null;
        }
    }*/

    public class Shop
    {
        private string _shopTag = "newShopTag";
        private string _shopName = "newShopName";
        private int _buybackPercent = 70;  
        private int _sellPercent = 100;

        //private List<string> _shopItemTags = new List<string>();
        private List<ItemRefs> _shopItemRefs = new List<ItemRefs>();

        public string shopTag
        {
            get { return _shopTag; }
            set { _shopTag = value; }
        }
        public string shopName
        {
            get { return _shopName; }
            set { _shopName = value; }
        }
        public List<ItemRefs> shopItemRefs
        {
            get { return _shopItemRefs; }
            set { _shopItemRefs = value; }
        }
        public int buybackPercent  
        {  
             get { return _buybackPercent; }  
             set { _buybackPercent = value; }  
        }

        public int sellPercent
        {  
             get { return _sellPercent; }  
             set { _sellPercent = value; }  
        }  


        
        public Shop()
        {
        }
        public override string ToString()
        {
            return shopTag;
        }
        public Shop ShallowCopy()
        {
            return (Shop)this.MemberwiseClone();
        }
        public Shop DeepCopy()
        {
            Shop other = (Shop)this.MemberwiseClone();
            other.shopItemRefs = new List<ItemRefs>();
            for (int i = 0; i < this.shopItemRefs.Count; i++)
            {
                other.shopItemRefs.Add(this.shopItemRefs[i].DeepCopy());
            }            
            return other;
        }
    }
}
