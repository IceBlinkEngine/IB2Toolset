﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class TileEnc
    {
        public string Layer1Filename = "t_blank";
        public string Layer2Filename = "t_blank";
        public string Layer3Filename = "t_blank";
        public int Layer1Rotate = 0;
        public int Layer2Rotate = 0;
        public int Layer3Rotate = 0;
        public int Layer1Xshift = 0;
        public int Layer2Xshift = 0;
        public int Layer3Xshift = 0;
        public int Layer1Yshift = 0;
        public int Layer2Yshift = 0;
        public int Layer3Yshift = 0;
        public bool Layer1Mirror = false;
        public bool Layer2Mirror = false;
        public bool Layer3Mirror = false;
        public int Layer1Xscale = 0;
        public int Layer2Xscale = 0;
        public int Layer3Xscale = 0;
        public int Layer1Yscale = 0;
        public int Layer2Yscale = 0;
        public int Layer3Yscale = 0;
        public bool Walkable = true;
        public bool LoSBlocked = false;

        public TileEnc()
        {

        }
    }
}
