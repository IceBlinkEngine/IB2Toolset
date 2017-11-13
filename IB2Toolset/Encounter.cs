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
    public class Encounter
    {
        private string encName = "newEncounter";
        public int singleImageMapTileSize = 50;
        public char[,] encounterMapLayout = new char[7, 7];
        private string mapImage = "none";
        private bool useMapImage = false;
        private bool useDayNightCycle = false;
        private bool isRepeatable = true;
        public int MapSizeX = 7;
        public int MapSizeY = 7;
        public List<TileEnc> encounterTiles = new List<TileEnc>();
        public List<CreatureRefs> encounterCreatureRefsList = new List<CreatureRefs>();
        public List<string> encounterCreatureList = new List<string>();
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
        private string onSetupCombatIBScript = "none";
        private string onSetupCombatIBScriptParms = "";
        private string onStartCombatRoundIBScript = "none";
        private string onStartCombatRoundIBScriptParms = "";
        private string onStartCombatTurnIBScript = "none";
        private string onStartCombatTurnIBScriptParms = "";
        private string onEndCombatIBScript = "none";
        private string onEndCombatIBScriptParms = "";
        public List<Trigger> Triggers = new List<Trigger>();
        public List<Prop> propsList = new List<Prop>();
        private bool isFriendlyContest = false; 

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
        
        [CategoryAttribute("03 - Not used anymore"), DescriptionAttribute("When using a premade area graphic, this how large in px one tile is supposed to be; defaults to 50.")]
        public int SingleImageMapTileSize
        {
            get { return singleImageMapTileSize; }
            set { singleImageMapTileSize = value; }
        }
        
        
        [CategoryAttribute("03 - Not used anymore"), DescriptionAttribute("if True, the encounter will use the single image from the MapImage filename instead of using the tiles.")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Set to true if this encounter can be redone despite killing all its creatures beforehand")]
        public bool IsRepeatable
        {
            get { return isRepeatable; }
            set { isRepeatable = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Set to true if this encounter is just a friendly contest; characters will be revived at the end and have at least 1 hp; game continues even if losing the contest")]
        public bool IsFriendlyContest
        {
            get { return isFriendlyContest; }
            set {isFriendlyContest = value; }
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
        [CategoryAttribute("03 - Not used anymore"), DescriptionAttribute("LogicTree name to be run at the start of each combat round")]
        public string OnStartCombatRoundLogicTree
        {
            get { return onStartCombatRoundLogicTree; }
            set { onStartCombatRoundLogicTree = value; }
        }
        [CategoryAttribute("03 - Not used anymore"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnStartCombatRoundParms
        {
            get { return onStartCombatRoundParms; }
            set { onStartCombatRoundParms = value; }
        }
        [Browsable(true), TypeConverter(typeof(LogicTreeConverter))]
        [CategoryAttribute("03 - Not used anymore"), DescriptionAttribute("LogicTree name to be run at the start of each PC and Enemy turn in combat")]
        public string OnStartCombatTurnLogicTree
        {
            get { return onStartCombatTurnLogicTree; }
            set { onStartCombatTurnLogicTree = value; }
        }
        [CategoryAttribute("03 - Not used anymore"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnStartCombatTurnParms
        {
            get { return onStartCombatTurnParms; }
            set { onStartCombatTurnParms = value; }
        }
        [Browsable(true), TypeConverter(typeof(LogicTreeConverter))]
        [CategoryAttribute("03 - Not used anymore"), DescriptionAttribute("LogicTree name to be run at the end of combat")]
        public string OnEndCombatLogicTree
        {
            get { return onEndCombatLogicTree; }
            set { onEndCombatLogicTree = value; }
        }
        [CategoryAttribute("03 - Not used anymore"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnEndCombatParms
        {
            get { return onEndCombatParms; }
            set { onEndCombatParms = value; }
        }

        [Browsable(true), TypeConverter(typeof(IBScriptConverter))]
        [CategoryAttribute("02 - IBScript Hooks"), DescriptionAttribute("IBScript name to be run once at setup of combat, before first combat round")]
        public string OnSetupCombatIBScript
        {
            get { return onSetupCombatIBScript; }
            set { onSetupCombatIBScript = value; }
        }
        [CategoryAttribute("02 - IBScript Hooks"), DescriptionAttribute("Parameters to be used for this IBScript hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnSetupCombatIBScriptParms
        {
            get { return onSetupCombatIBScriptParms; }
            set { onSetupCombatIBScriptParms = value; }
        }

        [Browsable(true), TypeConverter(typeof(IBScriptConverter))]
        [CategoryAttribute("02 - IBScript Hooks"), DescriptionAttribute("IBScript name to be run at the start of each combat round")]
        public string OnStartCombatRoundIBScript
        {
            get { return onStartCombatRoundIBScript; }
            set { onStartCombatRoundIBScript = value; }
        }
        [CategoryAttribute("02 - IBScript Hooks"), DescriptionAttribute("Parameters to be used for this IBScript hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnStartCombatRoundIBScriptParms
        {
            get { return onStartCombatRoundIBScriptParms; }
            set { onStartCombatRoundIBScriptParms = value; }
        }
        [Browsable(true), TypeConverter(typeof(IBScriptConverter))]
        [CategoryAttribute("02 - IBScript Hooks"), DescriptionAttribute("IBScript name to be run at the start of each PC and Enemy turn in combat")]
        public string OnStartCombatTurnIBScript
        {
            get { return onStartCombatTurnIBScript; }
            set { onStartCombatTurnIBScript = value; }
        }
        [CategoryAttribute("02 - IBScript Hooks"), DescriptionAttribute("Parameters to be used for this IBScript hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnStartCombatTurnIBScriptParms
        {
            get { return onStartCombatTurnIBScriptParms; }
            set { onStartCombatTurnIBScriptParms = value; }
        }
        [Browsable(true), TypeConverter(typeof(IBScriptConverter))]
        [CategoryAttribute("02 - IBScript Hooks"), DescriptionAttribute("IBScript name to be run at the end of combat")]
        public string OnEndCombatIBScript
        {
            get { return onEndCombatIBScript; }
            set { onEndCombatIBScript = value; }
        }
        [CategoryAttribute("02 - IBScript Hooks"), DescriptionAttribute("Parameters to be used for this IBScript hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnEndCombatIBScriptParms
        {
            get { return onEndCombatIBScriptParms; }
            set { onEndCombatIBScriptParms = value; }
        }

        public Encounter()
        {
        }
        public Encounter DeepCopy()
        {
            Encounter copy = new Encounter();
            copy = (Encounter)this.MemberwiseClone();
            
            copy.encounterTiles = new List<TileEnc>();
            foreach (TileEnc s in this.encounterTiles)
            {
                TileEnc newTileEnc = new TileEnc();
                newTileEnc.Layer1Filename = s.Layer1Filename;
                newTileEnc.Layer2Filename = s.Layer2Filename;
                newTileEnc.Layer3Filename = s.Layer3Filename;
                newTileEnc.LoSBlocked = s.LoSBlocked;
                newTileEnc.Walkable = s.Walkable;
                copy.encounterTiles.Add(newTileEnc);
            }

            copy.encounterCreatureRefsList = new List<CreatureRefs>();
            foreach (CreatureRefs s in this.encounterCreatureRefsList)
            {
                CreatureRefs newCrtRef = new CreatureRefs();
                newCrtRef.creatureResRef = s.creatureResRef;
                newCrtRef.creatureTag = s.creatureTag;
                newCrtRef.creatureStartLocationX = s.creatureStartLocationX;
                newCrtRef.creatureStartLocationY = s.creatureStartLocationY;
                copy.encounterCreatureRefsList.Add(newCrtRef);
            }

            copy.encounterCreatureList = new List<string>();
            foreach (string s in this.encounterCreatureList)
            {
                copy.encounterCreatureList.Add(s);
            }

            copy.Triggers = new List<Trigger>();
            foreach (Trigger t in this.Triggers)
            {
                copy.Triggers.Add(t);
            }

            copy.propsList = new List<Prop>();
            foreach (Prop p in this.propsList)
            {
                copy.propsList.Add(p);
            }

            copy.encounterInventoryRefsList = new List<ItemRefs>();
            foreach (ItemRefs s in this.encounterInventoryRefsList)
            {
                ItemRefs newItRef = new ItemRefs();
                newItRef = s.DeepCopy();
                copy.encounterInventoryRefsList.Add(newItRef);
            }

            copy.encounterPcStartLocations = new List<Coordinate>();
            foreach (Coordinate s in this.encounterPcStartLocations)
            {
                Coordinate newCoor = new Coordinate();
                newCoor.X = s.X;
                newCoor.Y = s.Y;
                copy.encounterPcStartLocations.Add(newCoor);
            }

            return copy;
        }


        public void SetAllToGrass()
        {
            for (int x = 0; x < this.MapSizeX; x++)
            {
                for (int y = 0; y < this.MapSizeY; y++)
                {
                    TileEnc t = new TileEnc();
                    encounterTiles.Add(t);                                        
                }
            }
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
