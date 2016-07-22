using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IB2miniToolset
{
    public class LocalListItem
    {
        private string name;
        private string notes;

        public string LocalName
        {
            get { return name; }
            set { name = value; }
        }

        public string LocalNotes
        {
            get { return notes; }
            set { notes = value; }
        }

        public LocalListItem()
        {
        }
    }
}
