using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class GlobalString
    {
        private string g_key;
        private string g_value;

        public string Key
        {
            get { return g_key; }
            set { g_key = value; }
        }        
        public string Value
        {
            get { return g_value; }
            set { g_value = value; }
        }

        public GlobalString()
        {
        }
    }
}
