using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
//using IceBlink;
using System.ComponentModel;
using Newtonsoft.Json;

namespace IB2Toolset
{
    /*public class Encounters
    {
        //[XmlArrayItem("Encounters")]
        public List<Encounter> encounters = new List<Encounter>();
        
        public Encounters()
        {
        }
        public void saveEncountersFile(string filename)
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public Encounters loadEncountersFile(string filename)
        {
            Encounters toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (Encounters)serializer.Deserialize(file, typeof(Encounters));
            }
            return toReturn;
        }
        public Encounter getEncounter(string name)
        {
            foreach (Encounter encounter in encounters)
            {
                if (encounter.encounterName == name) return encounter;
            }
            return null;
        }
    }*/

    public class Encounter
    {
        private string encName = "newEncounter";
        public char[,] encounterMapLayout = new char[7, 7];
        private string mapImage = "none";
        private bool useMapImage = false;
        private bool useDayNightCycle = false;
        public List<TileEnc> encounterTiles = new List<TileEnc>();
        //m=mud, b=black, g=grass, t=tree, r=rock, w=stone wall, s=stone floor
        public List<CreatureRefs> encounterCreatureRefsList = new List<CreatureRefs>();
        public List<string> encounterCreatureList = new List<string>();
        //public List<string> encounterInventoryTagList = new List<string>();
        public List<ItemRefs> encounterInventoryRefsList = new List<ItemRefs>();
        public List<Coordinate> encounterPcStartLocations = new List<Coordinate>();
        public int goldDrop = 0;
        private string areaMusic = "none";
        private int areaMusicDelay = 0;
        private int areaMusicDelayRandomAdder = 0;
        private string onStartCombatRoundLogicTree = "none";
        private string onStartCombatRoundParms = "";
        private string onStartCombatTurnLogicTree = "none";
        private string onStartCombatTurnParms = "";
        private string onEndCombatLogicTree = "none";
        private string onEndCombatParms = "";

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Name of Encounter, must be unique")]
        public string encounterName
        {
            get { return encName; }
            set { encName = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Map image file name (do NOT include file extension). Image must be 350x350 pixels.")]
        public string MapImage
        {
            get { return mapImage; }
            set { mapImage = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("if True, the encounter will use the single image from the MapImage filename instead of using the tiles.")]
        public bool UseMapImage
        {
            get { return useMapImage; }
            set { useMapImage = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Set to true if this encounter will use the day/night cycle tinting.")]
        public bool UseDayNightCycle
        {
            get { return useDayNightCycle; }
            set { useDayNightCycle = value; }
        }
        [Browsable(true), TypeConverter(typeof(MusicConverter))]
        [CategoryAttribute("02 - Music"), DescriptionAttribute("Filename of music for the area (no extension)")]
        public string AreaMusic
        {
            get { return areaMusic; }
            set { areaMusic = value; }
        }
        [CategoryAttribute("02 - Music"), DescriptionAttribute("Delay between replaying music (in milliseconds)")]
        public int AreaMusicDelay
        {
            get { return areaMusicDelay; }
            set { areaMusicDelay = value; }
        }
        [CategoryAttribute("02 - Music"), DescriptionAttribute("Add a random amount of delay (between 0 and this value) to the AreaMusicDelay value (in milliseconds)")]
        public int AreaMusicDelayRandomAdder
        {
            get { return areaMusicDelayRandomAdder; }
            set { areaMusicDelayRandomAdder = value; }
        }
        [Browsable(true), TypeConverter(typeof(LogicTreeConverter))]
        [CategoryAttribute("02 - LogicTree Hooks"), DescriptionAttribute("LogicTree name to be run at the start of each combat round")]
        public string OnStartCombatRoundLogicTree
        {
            get { return onStartCombatRoundLogicTree; }
            set { onStartCombatRoundLogicTree = value; }
        }
        [CategoryAttribute("02 - LogicTree Hooks"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnStartCombatRoundParms
        {
            get { return onStartCombatRoundParms; }
            set { onStartCombatRoundParms = value; }
        }
        [Browsable(true), TypeConverter(typeof(LogicTreeConverter))]
        [CategoryAttribute("02 - LogicTree Hooks"), DescriptionAttribute("LogicTree name to be run at the start of each PC and Enemy turn in combat")]
        public string OnStartCombatTurnLogicTree
        {
            get { return onStartCombatTurnLogicTree; }
            set { onStartCombatTurnLogicTree = value; }
        }
        [CategoryAttribute("02 - LogicTree Hooks"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnStartCombatTurnParms
        {
            get { return onStartCombatTurnParms; }
            set { onStartCombatTurnParms = value; }
        }
        [Browsable(true), TypeConverter(typeof(LogicTreeConverter))]
        [CategoryAttribute("02 - LogicTree Hooks"), DescriptionAttribute("LogicTree name to be run at the end of combat")]
        public string OnEndCombatLogicTree
        {
            get { return onEndCombatLogicTree; }
            set { onEndCombatLogicTree = value; }
        }
        [CategoryAttribute("02 - LogicTree Hooks"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnEndCombatParms
        {
            get { return onEndCombatParms; }
            set { onEndCombatParms = value; }
        }

        public Encounter()
        {
        }
        public void SetAllToGrass()
        {
            for (int x = 0; x < 7; x++)
            {
                for (int y = 0; y < 7; y++)
                {
                    TileEnc t = new TileEnc();
                    encounterTiles.Add(t);                                        
                }
            }
        }
    }  
  
    public class TileEnc 
    {
	    public string Layer1Filename = "t_grass";
	    public string Layer2Filename = "t_blank";
	    public bool Walkable = true;
	    public bool LoSBlocked = false;
    
	    public TileEnc()
	    {
	
	    }
    }
}
