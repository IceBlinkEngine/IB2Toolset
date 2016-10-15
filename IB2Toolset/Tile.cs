﻿/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Text;
using System.IO;
//using IceBlink;
using System.ComponentModel;
using Newtonsoft.Json;

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
        public int Layer1Rotate = 0;
        public int Layer2Rotate = 0;
        public int Layer3Rotate = 0;
        public int Layer4Rotate = 0;
        public int Layer5Rotate = 0;
        public int Layer1Xshift = 0;
        public int Layer2Xshift = 0;
        public int Layer3Xshift = 0;
        public int Layer4Xshift = 0;
        public int Layer5Xshift = 0;
        public int Layer1Yshift = 0;
        public int Layer2Yshift = 0;
        public int Layer3Yshift = 0;
        public int Layer4Yshift = 0;
        public int Layer5Yshift = 0;
        public bool Layer1Mirror = false;
        public bool Layer2Mirror = false;
        public bool Layer3Mirror = false;
        public bool Layer4Mirror = false;
        public bool Layer5Mirror = false;
        public int Layer1Xscale = 0;
        public int Layer2Xscale = 0;
        public int Layer3Xscale = 0;
        public int Layer4Xscale = 0;
        public int Layer5Xscale = 0;
        public int Layer1Yscale = 0;
        public int Layer2Yscale = 0;
        public int Layer3Yscale = 0;
        public int Layer4Yscale = 0;
        public int Layer5Yscale = 0;
        public bool Walkable = true;
        public bool LoSBlocked = false;
        public bool Visible = false;
        public int heightLevel = 0;
        public bool isRamp = false;
        public bool isEWBridge = false;
        public bool isNSBridge = false;
        public bool isShadowCaster = false;

        [CategoryAttribute("01 - Height Level System"), DescriptionAttribute("Tile is a ramp: you can leave this ramp towards one height level lower neighbouring tiles; note that the height level of the current tile is the top of the ramp, also ramps work from all directions")]
        public bool IsRamp
        {
            get { return isRamp; }
            set { isRamp = value; }
        }

        [CategoryAttribute("01 - Height Level System"), DescriptionAttribute("Tile is east-west bridge: it can be traversed east-west on same height level as the bridge itself (move on bridge) or north-south from one height level lower than bridge (under bridge); note the hight level of the current tile is the top of the bridge")]
        public bool IsEWBridge
        {
            get { return isEWBridge; }
            set { isEWBridge = value; }
        }

        [CategoryAttribute("01 - Height Level System"), DescriptionAttribute("Tile is north-south bridge: it can be traversed north-south on same height level as the bridge itself (move on bridge) or east-west from one height level lower than bridge (under bridge); note the hight level of the current tile is the top of the bridge")]
        public bool IsNSBridge
        {
            get { return isNSBridge; }
            set { isNSBridge = value; }
        }

        public Tile()
        {
        }
    }
}
