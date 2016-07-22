using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class LocalInt
    {
        private string g_key;
        private int g_value;

        public string Key
        {
            get { return g_key; }
            set { g_key = value; }
        }
        public int Value
        {
            get { return g_value; }
            set { g_value = value; }
        }

        public LocalInt()
        {
        }
    }
}
