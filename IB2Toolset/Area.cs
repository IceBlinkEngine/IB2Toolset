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
    public class Area
    {
        #region Fields
        private string filename = "newArea";
        private int areaVisibleDistance = 2;
        private bool restingAllowed = false;
        private string imageFileName = "none";
        public int backgroundImageStartLocX = 0;
        public int backgroundImageStartLocY = 0;
        private int mapSizeX = 16;
        private int mapSizeY = 16;
        //private bool isWorldMap = false;
        private bool useMiniMapFogOfWar = false;
        private bool _areaDark = false;
        private bool useDayNightCycle = false;
        private int timePerSquare = 6;              //currently in minutes
        private string areaMusic = "none";
        private int areaMusicDelay = 0;
        private int areaMusicDelayRandomAdder = 0;
        private string areaSounds = "none";
        private int areaSoundsDelay = 0;
        private int areaSoundsDelayRandomAdder = 0;
        private List<Tile> tiles = new List<Tile>();
        private List<Prop> props = new List<Prop>();
        private List<Trigger> triggers = new List<Trigger>();
        //private string musicFileName = "forest.mp3";
        private int nextIdNumber = 100;
        private string onHeartBeatLogicTree = "none";
        private string onHeartBeatParms = "";
        private string onHeartBeatIBScript = "none";
        private string onHeartBeatIBScriptParms = "";
        private List<LocalInt> areaLocalInts = new List<LocalInt>();
        private List<LocalString> areaLocalStrings = new List<LocalString>();
        private string _inGameAreaName = "newArea";
        private bool _useMiniProps = false;
        private bool _useSuperTinyProps = false;

        private string _areaWeatherScript = "";
        private string _effectChannelScript1 = "deactivateChannel1";
        private string _effectChannelScript2 = "deactivateChannel2";
        private string _effectChannelScript3 = "deactivateChannel3";
        private string _effectChannelScript4 = "deactivateChannel4";
        private string _areaWeatherTag = "";

        private string _easternNeighbourArea = "";
        private string _westernNeighbourArea = "";
        private string _northernNeighbourArea = "";
        private string _southernNeighbourArea = "";

        public string sourceBitmapName = "";
        public bool isJPGMap = false;
        public bool isPNGMap = false;

        private bool _drawWithLessVisibleSeamsButMorePixelated = false;
        private bool _use100pixSquares = false;

        #endregion

        #region Properties
        [CategoryAttribute("01 - Main"), ReadOnly(true)]
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
        /*[CategoryAttribute("01 - Main"), ReadOnly(true)]
        public bool IsWorldMap
        {
            get { return isWorldMap; }
            set { isWorldMap = value; }
        }*/
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
        public string areaWeatherScript
        {
            get { return _areaWeatherScript; }
            set { _areaWeatherScript = value; }
        }
        [CategoryAttribute("01 - Main")]
        public string easternNeighbourArea
        {
            get { return _easternNeighbourArea; }
            set { _easternNeighbourArea = value; }
        }
        [CategoryAttribute("01 - Main")]
        public string westernNeighbourArea
        {
            get { return _westernNeighbourArea; }
            set { _westernNeighbourArea = value; }
        }
        [CategoryAttribute("01 - Main")]
        public string northernNeighbourArea
        {
            get { return _northernNeighbourArea; }
            set { _northernNeighbourArea = value; }
        }
        [CategoryAttribute("01 - Main")]
        public string southernNeighbourArea
        {
            get { return _southernNeighbourArea; }
            set { _southernNeighbourArea = value; }
        }

        [CategoryAttribute("01 - Main")]
        public string effectChannelScript1
        {
            get { return _effectChannelScript1; }
            set { _effectChannelScript1 = value; }
        }
        [CategoryAttribute("01 - Main")]
        public string effectChannelScript2
        {
            get { return _effectChannelScript2; }
            set { _effectChannelScript2 = value; }
        }
        [CategoryAttribute("01 - Main")]
        public string effectChannelScript3
        {
            get { return _effectChannelScript3; }
            set { _effectChannelScript3 = value; }
        }
        [CategoryAttribute("01 - Main")]
        public string effectChannelScript4
        {
            get { return _effectChannelScript4; }
            set { _effectChannelScript4 = value; }
        }
        [CategoryAttribute("01 - Main")]
        public bool useMiniProps
        {
            get { return _useMiniProps; }
            set { _useMiniProps = value; }
        }
        [CategoryAttribute("01 - Main")]
        public bool useSuperTinyProps
        {
            get { return _useSuperTinyProps; }
            set { _useSuperTinyProps = value; }
        }

        [CategoryAttribute("01 - Main")]
        public bool drawWithLessVisibleSeamsButMorePixelated
        {
            get { return _drawWithLessVisibleSeamsButMorePixelated; }
            set { _drawWithLessVisibleSeamsButMorePixelated = value; }
        }

        [CategoryAttribute("01 - Main")]
        public bool use100pixSquares
        {
            get { return _use100pixSquares; }
            set { _use100pixSquares = value; }
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
        [CategoryAttribute("01 - Main"), ReadOnly(true)]
        public string ImageFileName
        {
            get { return imageFileName; }
            set { imageFileName = value; }
        }
        [CategoryAttribute("01 - Main"), ReadOnly(true)]
        public int MapSizeX
        {
            get { return mapSizeX; }
            set { mapSizeX = value; }
        }
        [CategoryAttribute("01 - Main"), ReadOnly(true)]
        public int MapSizeY
        {
            get { return mapSizeY; }
            set { mapSizeY = value; }
        }
        [CategoryAttribute("Misc"), ReadOnly(true), Browsable(false)]
        public List<Tile> Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }
        [CategoryAttribute("Misc"), ReadOnly(true)]
        public List<Prop> Props
        {
            get { return props; }
            set { props = value; }
        }
        [CategoryAttribute("Misc"), ReadOnly(true)]
        public List<Trigger> Triggers
        {
            get { return triggers; }
            set { triggers = value; }
        }
        [Browsable(true), TypeConverter(typeof(MusicConverter))]
        [CategoryAttribute("02 - Music/Sounds"), DescriptionAttribute("Filename of music for the area (include extension)")]
        public string AreaMusic
        {
            get { return areaMusic; }
            set { areaMusic = value; }
        }
        [CategoryAttribute("02 - Music/Sounds"), DescriptionAttribute("Delay between replaying music (in milliseconds)")]
        public int AreaMusicDelay
        {
            get { return areaMusicDelay; }
            set { areaMusicDelay = value; }
        }
        [CategoryAttribute("02 - Music/Sounds"), DescriptionAttribute("Add a random amount of delay (between 0 and this value) to the AreaMusicDelay value (in milliseconds)")]
        public int AreaMusicDelayRandomAdder
        {
            get { return areaMusicDelayRandomAdder; }
            set { areaMusicDelayRandomAdder = value; }
        }
        [Browsable(true), TypeConverter(typeof(MusicConverter))]
        [CategoryAttribute("02 - Music/Sounds"), DescriptionAttribute("Filename of sounds for the area (include extension)")]
        public string AreaSounds
        {
            get { return areaSounds; }
            set { areaSounds = value; }
        }
        [CategoryAttribute("02 - Music/Sounds"), DescriptionAttribute("Delay between replaying area sounds (in milliseconds)")]
        public int AreaSoundsDelay
        {
            get { return areaSoundsDelay; }
            set { areaSoundsDelay = value; }
        }
        [CategoryAttribute("02 - Music/Sounds"), DescriptionAttribute("Add a random amount of delay (between 0 and this value) to the AreaSoundsDelay value (in milliseconds)")]
        public int AreaSoundsDelayRandomAdder
        {
            get { return areaSoundsDelayRandomAdder; }
            set { areaSoundsDelayRandomAdder = value; }
        }
        [CategoryAttribute("Misc"), ReadOnly(true)]
        public int NextIdNumber
        {
            get
            {
                nextIdNumber++;
                return nextIdNumber;
            }
            set { nextIdNumber = value; }
        }

        [Browsable(true), TypeConverter(typeof(LogicTreeConverter))]
        [CategoryAttribute("03 - LogicTree Hooks"), DescriptionAttribute("LogicTree name to be run at the end of each move on this area map (not combat)")]
        public string OnHeartBeatLogicTree
        {
            get { return onHeartBeatLogicTree; }
            set { onHeartBeatLogicTree = value; }
        }
        [CategoryAttribute("03 - LogicTree Hooks"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnHeartBeatParms
        {
            get { return onHeartBeatParms; }
            set { onHeartBeatParms = value; }
        }
        [Browsable(true), TypeConverter(typeof(IBScriptConverter))]
        [CategoryAttribute("03 - IBScript Hooks"), DescriptionAttribute("IBScript name to be run at the end of each move on this area map (not combat)")]
        public string OnHeartBeatIBScript
        {
            get { return onHeartBeatIBScript; }
            set { onHeartBeatIBScript = value; }
        }
        [CategoryAttribute("03 - IBScript Hooks"), DescriptionAttribute("Parameters to be used for this IBScript hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnHeartBeatIBScriptParms
        {
            get { return onHeartBeatIBScriptParms; }
            set { onHeartBeatIBScriptParms = value; }
        }
        [CategoryAttribute("04 - Locals"), DescriptionAttribute("These Local Variables will be saved in the save games")]
        public List<LocalInt> AreaLocalInts
        {
            get { return areaLocalInts; }
            set { areaLocalInts = value; }
        }
        [CategoryAttribute("04 - Locals"), DescriptionAttribute("These Local Variables will be saved in the save games")]
        public List<LocalString> AreaLocalStrings
        {
            get { return areaLocalStrings; }
            set { areaLocalStrings = value; }
        }
        //add converter and cmb functionality later
        [CategoryAttribute("05 - Full Screen Effects"), DescriptionAttribute("tag of weather object for this area"), Browsable(false)]
        public string areaWeatherTag
        {
            get { return _areaWeatherTag; }
            set { _areaWeatherTag = value; }
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
