using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class Tile
    {
        public string Layer1Filename = "t_grass";
        public string Layer2Filename = "t_blank";
        public bool Walkable = true;
        public bool LoSBlocked = false;
        public bool Visible = false;

        public Tile()
        {
        }
    }
}
