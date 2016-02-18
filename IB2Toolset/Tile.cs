using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class Tile
    {
        public string Layer0Filename = "t_blank";
        public string Layer1Filename = "t_blank";
        public string Layer2Filename = "t_blank";
        public string Layer3Filename = "t_blank";
        public string Layer4Filename = "t_blank";
        public string Layer5Filename = "t_blank";
        public float Layer1Rotate = 0;
        public float Layer2Rotate = 0;
        public float Layer3Rotate = 0;
        public float Layer4Rotate = 0;
        public float Layer5Rotate = 0;
        public bool Layer1Mirror = false;
        public bool Layer2Mirror = false;
        public bool Layer3Mirror = false;
        public bool Layer4Mirror = false;
        public bool Layer5Mirror = false;
        public bool Walkable = true;
        public bool LoSBlocked = false;
        public bool Visible = false;

        public Tile()
        {
        }
    }
}
