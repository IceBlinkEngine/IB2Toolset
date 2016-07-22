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

namespace IB2miniToolset
{
    public class Shop
    {
        private string _shopTag = "newShopTag";
        private string _shopName = "newShopName";
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
