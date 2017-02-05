/*
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
        public float Layer1Opacity = 1;
        public float Layer2Opacity = 1;
        public float Layer3Opacity = 1;
        public float Layer4Opacity = 1;
        public float Layer5Opacity = 1;
        public bool Walkable = true;
        public bool LoSBlocked = false;
        public bool Visible = false;

        public int heightLevel = 0;
        public bool isRamp = false;
        public bool isEWBridge = false;
        public bool isNSBridge = false;
        public bool isShadowCaster = true;
        public bool drawDownStairShadows = true;

        public bool isInShortShadeN = false;
        public bool isInShortShadeE = false;
        public bool isInShortShadeS = false;
        public bool isInShortShadeW = false;
        public bool isInShortShadeNE = false;
        public bool isInShortShadeSE = false;
        public bool isInShortShadeSW = false;
        public bool isInShortShadeNW = false;

        public bool isInLongShadeN = false;
        public bool isInLongShadeE = false;
        public bool isInLongShadeS = false;
        public bool isInLongShadeW = false;
        public bool isInLongShadeNE = false;
        public bool isInLongShadeSE = false;
        public bool isInLongShadeSW = false;
        public bool isInLongShadeNW = false;

        public bool isInMaxShadeN = false;
        public bool isInMaxShadeE = false;
        public bool isInMaxShadeS = false;
        public bool isInMaxShadeW = false;
        public bool isInMaxShadeNE = false;
        public bool isInMaxShadeSE = false;
        public bool isInMaxShadeSW = false;
        public bool isInMaxShadeNW = false;

        public bool hasHighlightN = false;
        public int highlightStrengthN = 100;
        public bool hasHighlightE = false;
        public int highlightStrengthE = 100;
        public bool hasHighlightS = false;
        public int highlightStrengthS = 100;
        public bool hasHighlightW = false;
        public int highlightStrengthW = 100;

        public bool hasDownStairShadowN = false;
        public bool hasDownStairShadowE = false;
        public bool hasDownStairShadowS = false;
        public bool hasDownStairShadowW = false;

        public int numberOfHeightLevelsThisTileisHigherThanNeighbourN = 0;
        public int numberOfHeightLevelsThisTileisHigherThanNeighbourE = 0;
        public int numberOfHeightLevelsThisTileisHigherThanNeighbourS = 0;
        public int numberOfHeightLevelsThisTileisHigherThanNeighbourW = 0;

        public bool inSmallStairNEHorizontal = false;
        public bool inSmallStairNEVertical = false;
        public bool inSmallStairSEHorizontal = false;
        public bool inSmallStairSEVertical = false;
        public bool inSmallStairSWHorizontal = false;
        public bool inSmallStairSWVertical = false;
        public bool inSmallStairNWHorizontal = false;
        public bool inSmallStairNWVertical = false;

        public bool hasEntranceLightNorth = false;
        public bool hasEntranceLightEast = false;
        public bool hasEntranceLightSouth = false;
        public bool hasEntranceLightWest = false;

        public bool inRampShadowWest1Short = false;
        public bool inRampShadowWest1Long = false;
        public bool inRampShadowWest2Short = false;
        public bool inRampShadowWest2Long = false;

        public bool inRampShadowEast3Short = false;
        public bool inRampShadowEast3Long = false;
        public bool inRampShadowEast4Short = false;
        public bool inRampShadowEast4Long = false;

        public bool inRampShadowNorth5Short = false;
        public bool inRampShadowNorth5Long = false;
        public bool inRampShadowNorth6Short = false;
        public bool inRampShadowNorth6Long = false;

        public bool inRampShadowSouth7Short = false;
        public bool inRampShadowSouth7Long = false;
        public bool inRampShadowSouth8Short = false;
        public bool inRampShadowSouth8Long = false;

        /*
        [CategoryAttribute("01 - Height Level System"), DescriptionAttribute("Tile is a ramp: you can leave this ramp towards one height level lower neighbouring tiles; note that the height level of the current tile is the top of the ramp, also ramps work from all directions")]
        public bool IsRamp
        {
            get { return isRamp; }
            set { isRamp = value; }
        }
        */
        /*
        [CategoryAttribute("01 - Height Level System"), DescriptionAttribute("Tile is east-west bridge: it can be traversed east-west on same height level as the bridge itself (move on bridge) or north-south from one height level lower than bridge (under bridge); note the hight level of the current tile is the top of the bridge")]
        public bool IsEWBridge
        {
            get { return isEWBridge; }
            set { isEWBridge = value; }
        }
        */
        /*
        [CategoryAttribute("01 - Height Level System"), DescriptionAttribute("Tile is north-south bridge: it can be traversed north-south on same height level as the bridge itself (move on bridge) or east-west from one height level lower than bridge (under bridge); note the hight level of the current tile is the top of the bridge")]
        public bool IsNSBridge
        {
            get { return isNSBridge; }
            set { isNSBridge = value; }
        }
        */
        /*
        [CategoryAttribute("01 - Height Level System"), DescriptionAttribute("Height level of the currently selcted tile.")]
        public int HeightLevel
        {
            get { return heightLevel; }
            set { heightLevel = value; }
        }
        */

        [CategoryAttribute("01 - Height Level System"), DescriptionAttribute("When set to true, the tile casts autoshados on lower tiles.")]
        public bool IsShadowCaster
        {
            get { return isShadowCaster; }
            set { isShadowCaster = value; }
        }

        [CategoryAttribute("01 - Height Level System"), DescriptionAttribute("When set to true, the tile has an automatic downstairs shadow on top of it in correct direction. Turn off if you want to use custom stair prop or tile graphics for the stairs/ramp.")]
        public bool DrawDownStairShadows
        {
            get { return drawDownStairShadows; }
            set { drawDownStairShadows = value; }
        }

        public Tile()
        {
        }
    }
}
