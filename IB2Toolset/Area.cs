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

        private string _zoneMotherAreaName = "none";
        private int _zoneMotherAreaX = 0;
        private int _zoneMotherAreaY = 0;

        private string _zoneName = "none";
        private int _zoneX = 0;
        private int _zoneY = 0;
        private int _zoneFloorLevel = 0;

        public int linkedAreasCounter = 0;
        public string masterOfThisArea = "none";
        public List<string> linkedAreas = new List<string>();
        public List<int> linkNumbers = new List<int>();
        public int linkNumberOfThisArea = -1;
        private string filename = "newArea";
        public int averageHeightOnThisMap = 0;
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
        private string _areaWeatherName = "";

        private string _easternNeighbourArea = "";
        private string _westernNeighbourArea = "";
        private string _northernNeighbourArea = "";
        private string _southernNeighbourArea = "";

        public string sourceBitmapName = "";
        public bool isJPGMap = false;
        public bool isPNGMap = false;

        private bool _drawWithLessVisibleSeamsButMorePixelated = false;
        private bool _use100pixSquares = false;
        private bool _useLightSystem = false;

        private float _flickerSlowDownFactor = 1f;
        private float _shifterSlowDownFactor = 1f;
        private bool _noFlicker = false;
        private bool _noPositionShift = false;
        private float _minimumDarkness = 12;
        private float _maxLightMultiplier = 1;

        public bool allowLevelDesignWithMoreThan2HeightLevelsDifference = false;
        //public bool allowFreePlacementOfBridges = false;
        //public bool allowFreePlacementOfRamps = false;

        #endregion

        #region Properties
        [CategoryAttribute("01 - Main"), DescriptionAttribute("When set to true, you can build with more than 2 height levels difference which breaks the default height shadowing; best turn off isShadowCaster in the tile's properties and place custom shadow graphics as tiles in layer 5")]
        public bool AllowLevelDesignWithMoreThan2HeightLevelsDifference
        {
            get { return allowLevelDesignWithMoreThan2HeightLevelsDifference; }
            set { allowLevelDesignWithMoreThan2HeightLevelsDifference = value; }
        }

        /*
        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("When set to true, you can build bridges anywhere you like (normaly they require height a on both their topsides as well as the bridge itslef. And height a - 1 on their flank sides. Ramps would allow top sides a height of a+1, too. Be careful with freely buidling bridges as this might not work out for pathfinding AI and player movement.")]
        public bool AllowFreePlacementOfBridges
        {
            get { return allowFreePlacementOfBridges; }
            set { allowFreePlacementOfBridges = value; }
        }

        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("When set to true, you can build ramps/downstairs anywhere you like (normaly they require height a on their upper side as well as the on the ramp square itself. And height a - 1 on their bottom side. Further Ramps would allow upper side of this ramp a height of a+1, too. Be careful with freely buidling ramps as this might not work out for pathfinding AI and player movement.")]
        public bool AllowFreePlacementOfRamps
        {
            get { return allowFreePlacementOfRamps; }
            set { allowFreePlacementOfRamps = value; }
        }
        */

        [CategoryAttribute("08 - Properties that are ony readable"), DescriptionAttribute("These properteis cannot be changed in toolset; you can read them in and use them via IBScript though"), ReadOnly(true)]
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("How far you can normally see in this area (radius in squares)")]
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

        /*
        [CategoryAttribute("01 - Main")]
        public string areaWeatherScript
        {
            get { return _areaWeatherScript; }
            set { _areaWeatherScript = value; }
        }
        */

        [CategoryAttribute("03 - Scale and neighbouring areas"), DescriptionAttribute("The filename (NOT ingame name) of the more zoomed out area where the entrance to the zone of this area is located in; also called mother area")]
        public string zoneMotherAreaName
        {
            get { return _zoneMotherAreaName; }
            set { _zoneMotherAreaName = value; }
        }

        [CategoryAttribute("03 - Scale and neighbouring areas"), DescriptionAttribute("The x coordinate where the entrance to this area's zone can be found on the more zoomed out mother area.")]
        public int zoneMotherAreaX
        {
            get { return _zoneMotherAreaX; }
            set { _zoneMotherAreaX = value; }
        }


        [CategoryAttribute("03 - Scale and neighbouring areas"), DescriptionAttribute("The y coordinate where the entrance to this area's zone can be found on the more zoomed out mother area.")]
        public int zoneMotherAreaY
        {
            get { return _zoneMotherAreaY; }
            set { _zoneMotherAreaY = value; }
        }

        [CategoryAttribute("03 - Scale and neighbouring areas"), DescriptionAttribute("The ingame name of this area's zone: a zone typically consists of all horizontally or vertically connected areas of the same zoome level, you can freely define this as you like though")]
        public string zoneName
        {
            get { return _zoneName; }
            set { _zoneName = value; }
        }

        [CategoryAttribute("03 - Scale and neighbouring areas"), DescriptionAttribute("The x coordinate that this area has within its zone (of areas stitched together).")]
        public int zoneX
        {
            get { return _zoneX; }
             set { _zoneX = value; }
        }

        [CategoryAttribute("03 - Scale and neighbouring areas"), DescriptionAttribute("The floor level - as positive or negative number, with 0 being ground floor - that this area has within its zone (of areas stitched together).")]
        public int zoneFloorLevel
        {
            get { return _zoneFloorLevel; }
            set { _zoneFloorLevel = value; }
        }

        [CategoryAttribute("03 - Scale and neighbouring areas"), DescriptionAttribute("The y coordinate that this area has within its zone (of areas stitched together).")]
        public int zoneY
        {
            get { return _zoneY; }
            set { _zoneY = value; }
        }

        [CategoryAttribute("03 - Scale and neighbouring areas")]
        public string easternNeighbourArea
        {
            get { return _easternNeighbourArea; }
            set { _easternNeighbourArea = value; }
        }
        [CategoryAttribute("03 - Scale and neighbouring areas")]
        public string westernNeighbourArea
        {
            get { return _westernNeighbourArea; }
            set { _westernNeighbourArea = value; }
        }
       [CategoryAttribute("03 - Scale and neighbouring areas")]
        public string northernNeighbourArea
        {
            get { return _northernNeighbourArea; }
            set { _northernNeighbourArea = value; }
        }
       [CategoryAttribute("03 - Scale and neighbouring areas")]
        public string southernNeighbourArea
        {
            get { return _southernNeighbourArea; }
            set { _southernNeighbourArea = value; }
        }
        /*
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
        */

        [CategoryAttribute("03 - Scale and neighbouring areas")]
        public bool useMiniProps
        {
            get { return _useMiniProps; }
            set { _useMiniProps = value; }
        }
        [CategoryAttribute("03 - Scale and neighbouring areas")]
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

        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("Light related - true: Activates the light system, as of now NEEDS useAllTileSystem property of module set to true: makes all areas that dont use day&nigth cycle turn into pitch black darkness; you need to place props with isLight property set to true to lighten them up; also in nights, such props spend extra light.")]
        public bool useLightSystem
        {
            get { return _useLightSystem; }
            set { _useLightSystem = value; }
        }

        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("Light related - true: Lights on this map do not osciliate, ie the brightness/darkness of the lit squares does not change over time.")]
        public bool noFlicker
        {
            get { return _noFlicker; }
            set { _noFlicker = value; }
        }

        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("Light related - true: Lights on this map do not dance, ie the shadows and lit areas only move when the party moves or a prop that is also a light source moves.")]
        public bool noPositionShift
        {
            get { return _noPositionShift; }
            set { _noPositionShift = value; }
        }

        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("Light related: This is multiplied with the default flicker speed (osciliation); values above 1, like 1.1f, increase it therefore while values below 1, like 0.9f, decrease it; Try to stay between 0.1f and 1.9f.")]
        public float flickerSlowDownFactor
        {
            get { return _flickerSlowDownFactor; }
            set { _flickerSlowDownFactor = value; }
        }

        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("Light related: This is multiplied with the default shifter speed (dancing shadows); values above 1, like 1.1f, increase it therefore while values below 1, like 0.9f, decrease it; Try to stay between 0.1f and 1.9f.")]
        public float shifterSlowDownFactor
        {
            get { return _shifterSlowDownFactor; }
            set { _shifterSlowDownFactor = value; }
        }

        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("Light related: Defaults to 12. The higher, the more and the deeper shadows stay even at full light. Try to stay between 0f and 50f.")]
        public float minimumDarkness
        {
            get { return _minimumDarkness; }
            set { _minimumDarkness = value; }
        }

        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("Light related: Defaults to 1. This is multiplied with the current brightness level: the higher, the more shadows vanish when light osciliates into brightness. Try to stay between 0.1f and 1.9f.")]
        public float maxLightMultiplier
        {
            get { return _maxLightMultiplier; }
            set { _maxLightMultiplier = value; }
        }

        [CategoryAttribute("04 - Light and visibility"), DescriptionAttribute("A older value from the time before the light system. Leave at false unless you already use it for your module. Areas with useLightSystem set to true and UseDayNightCycle set to false, get dark automatically now.")]
        public bool areaDark
        {
            get { return _areaDark; }
            set { _areaDark = value; }
        }
        [CategoryAttribute("04 - Light and visibility")]
        public bool UseDayNightCycle
        {
            get { return useDayNightCycle; }
            set { useDayNightCycle = value; }
        }
        
        [CategoryAttribute("03 - Scale and neighbouring areas"), DescriptionAttribute("How much game world time passes with each square move (in minutes)")]
        public int TimePerSquare
        {
            get { return timePerSquare; }
            set { timePerSquare = value; }
        }
        [CategoryAttribute("08 - Properties that are ony readable"), DescriptionAttribute("These properties cannot be changed in toolset; you can read them in and use them via IBScript though"), ReadOnly(true)]
        public string ImageFileName
        {
            get { return imageFileName; }
            set { imageFileName = value; }
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
        [CategoryAttribute("07 - Lists with objects contained in this area"), ReadOnly(true), Browsable(false)]
        public List<Tile> Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }
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

        /*
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
        */

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
        [CategoryAttribute("06 - Locals"), DescriptionAttribute("These Local Variables will be saved in the save games")]
        public List<LocalInt> AreaLocalInts
        {
            get { return areaLocalInts; }
            set { areaLocalInts = value; }
        }
        [CategoryAttribute("06 - Locals"), DescriptionAttribute("These Local Variables will be saved in the save games")]
        public List<LocalString> AreaLocalStrings
        {
            get { return areaLocalStrings; }
            set { areaLocalStrings = value; }
        }
        //add converter and cmb functionality later
        [CategoryAttribute("01 - Main"), DescriptionAttribute("tag of weather object for this area"), Browsable(false)]
        public string areaWeatherTag
        {
            get { return _areaWeatherTag; }
            set { _areaWeatherTag = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("name of weather object for this area")]
        public string areaWeatherName
        {
            get { return _areaWeatherName; }
            set { _areaWeatherName = value; }
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
