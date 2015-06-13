using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IB2Toolset
{
    public class GlobalListItem
    {
        private string name;
        private string notes;

        public string GlobalName
        {
            get { return name; }
            set { name = value; }
        }

        public string GlobalNotes
        {
            get { return notes; }
            set { notes = value; }
        }

        public GlobalListItem()
        {
        }
    }
}
