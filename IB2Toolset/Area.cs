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

namespace IB2miniToolset
{
    public class Area
    {
        #region Fields
        private string filename = "newArea";
        private int areaVisibleDistance = 2;
        private bool restingAllowed = false;
        //private string imageFileName = "none";
        //public int backgroundImageStartLocX = 0;
        //public int backgroundImageStartLocY = 0;
        private int mapSizeX = 16;
        private int mapSizeY = 16;
        private bool useMiniMapFogOfWar = false;
        private bool _areaDark = false;
        private bool useDayNightCycle = false;
        private int timePerSquare = 6;              //currently in minutes
        //private string areaMusic = "none";
        //private int areaMusicDelay = 0;
        //private int areaMusicDelayRandomAdder = 0;
        //private string areaSounds = "none";
        //private int areaSoundsDelay = 0;
        //private int areaSoundsDelayRandomAdder = 0;
        //private List<Tile> tiles = new List<Tile>();
        public List<string> Layer1Filename = new List<string>();
        public List<int> Layer1Rotate = new List<int>();
        public List<int> Layer1Mirror = new List<int>();
        public List<string> Layer2Filename = new List<string>();
        public List<int> Layer2Rotate = new List<int>();
        public List<int> Layer2Mirror = new List<int>();
        public List<int> Walkable = new List<int>();
        public List<int> LoSBlocked = new List<int>();
        public List<int> Visible = new List<int>();
        private List<Prop> props = new List<Prop>();
        private List<Trigger> triggers = new List<Trigger>();
        private int nextIdNumber = 100;
        //private string musicFileName = "forest.mp3";
        //private string onHeartBeatLogicTree = "none";
        //private string onHeartBeatParms = "";
        private string onHeartBeatIBScript = "none";
        private string onHeartBeatIBScriptParms = "";
        //private List<LocalInt> areaLocalInts = new List<LocalInt>();
        //private List<LocalString> areaLocalStrings = new List<LocalString>();
        private string _inGameAreaName = "newArea";
        //private bool _useMiniProps = false;
        //private bool _useSuperTinyProps = false;

        //private string _areaWeatherScript = "";
        //private string _effectChannelScript1 = "deactivateChannel1";
        //private string _effectChannelScript2 = "deactivateChannel2";
        //private string _effectChannelScript3 = "deactivateChannel3";
        //private string _effectChannelScript4 = "deactivateChannel4";
        //private string _areaWeatherTag = "";
        //private string _areaWeatherName = "";

        //private string _easternNeighbourArea = "";
        //private string _westernNeighbourArea = "";
        //private string _northernNeighbourArea = "";
        //private string _southernNeighbourArea = "";

        //public string sourceBitmapName = "";
        //public bool isJPGMap = false;
        //public bool isPNGMap = false;

        //private bool _drawWithLessVisibleSeamsButMorePixelated = false;
        //private bool _use100pixSquares = false;

        #endregion

        #region Properties
        [CategoryAttribute("08 - Properties that are ony readable"), DescriptionAttribute("These properteis cannot be changed in toolset; you can read them in and use them via IBScript though"), ReadOnly(true)]
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("How far you can normally see in this area (radius in squares)")]
        public int AreaVisibleDistance
        {
            get { return areaVisibleDistance; }
            set { areaVisibleDistance = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("true = Can rest anywhere in this area, false = Can only rest in designated squares if they exist")]
        public bool RestingAllowed
        {
            get { return restingAllowed; }
            set { restingAllowed = value; }
        }
        [CategoryAttribute("01 - Main")]
        public bool UseMiniMapFogOfWar
        {
            get { return useMiniMapFogOfWar; }
            set { useMiniMapFogOfWar = value; }
        }
        [CategoryAttribute("01 - Main")]
        public string inGameAreaName
        {
            get { return _inGameAreaName; }
            set { _inGameAreaName = value; }
        }
        [CategoryAttribute("01 - Main")]
        public bool areaDark
        {
            get { return _areaDark; }
            set { _areaDark = value; }
        }
        [CategoryAttribute("01 - Main")]
        public bool UseDayNightCycle
        {
            get { return useDayNightCycle; }
            set { useDayNightCycle = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("How much game world time passes with each square move (in minutes)")]
        public int TimePerSquare
        {
            get { return timePerSquare; }
            set { timePerSquare = value; }
        }
        [CategoryAttribute("08 - Properties that are ony readable"), DescriptionAttribute("These properties cannot be changed in toolset; you can read them in and use them via IBScript though"), ReadOnly(true)]
        public int MapSizeX
        {
            get { return mapSizeX; }
            set { mapSizeX = value; }
        }
        [CategoryAttribute("08 - Properties that are ony readable"), DescriptionAttribute("These properties cannot be changed in toolset; you can read them in and use them via IBScript though"), ReadOnly(true)]
        public int MapSizeY
        {
            get { return mapSizeY; }
            set { mapSizeY = value; }
        }
        /*[CategoryAttribute("07 - Lists with objects contained in this area"), ReadOnly(true), Browsable(false)]
        public List<Tile> Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }*/
        [CategoryAttribute("07 - Lists with objects contained in this area"), ReadOnly(true)]
        public List<Prop> Props
        {
            get { return props; }
            set { props = value; }
        }
        [CategoryAttribute("07 - Lists with objects contained in this area"), ReadOnly(true)]
        public List<Trigger> Triggers
        {
            get { return triggers; }
            set { triggers = value; }
        }
        [CategoryAttribute("08 - Properties that are ony readable"), DescriptionAttribute("These properties cannot be changed in toolset; you can read them in and use them via IBScript though"), ReadOnly(true)]
        public int NextIdNumber
        {
            get
            {
                nextIdNumber++;
                return nextIdNumber;
            }
            set { nextIdNumber = value; }
        }
        [Browsable(true), TypeConverter(typeof(IBScriptConverter))]
        [CategoryAttribute("05 - IBScript Hooks"), DescriptionAttribute("IBScript name to be run at the end of each move on this area map (not combat)")]
        public string OnHeartBeatIBScript
        {
            get { return onHeartBeatIBScript; }
            set { onHeartBeatIBScript = value; }
        }
        [CategoryAttribute("05 - IBScript Hooks"), DescriptionAttribute("Parameters to be used for this IBScript hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnHeartBeatIBScriptParms
        {
            get { return onHeartBeatIBScriptParms; }
            set { onHeartBeatIBScriptParms = value; }
        }
        #endregion

        public Area()
        {
        }

        public void saveAreaFile(string path)
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(json.ToString());
            }
        }
        public Area loadAreaFile(string path)
        {
            Area toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(path))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (Area)serializer.Deserialize(file, typeof(Area));
            }
            return toReturn;
        }
        public Trigger getTriggerByTag(string tag)
        {
            foreach (Trigger it in Triggers)
            {
                if (it.TriggerTag == tag) return it;
            }
            return null;
        }
    }
}
