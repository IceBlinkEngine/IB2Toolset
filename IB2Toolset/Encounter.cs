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



        private int challengeLevel = 1;
        private bool challengeHidden = false;

        //texts at start and ned (win) of battle
        private bool showDefaultMessageBoxAtStartOfEncounter = true;
        private bool showDefaultMessageBoxAtEndOfEncounter = true;
        private bool showDefaultLogTextAtStartOfEncounter = true;
        private bool showDefaultLogTextAtEndOfEncounter = true;
        private string customTextforMessageBoxAtStartOfEncounter = "none";
        private string customTextforMessageBoxAtEndOfEncounter = "none";
        private string customTextforLogTextAtStartOfEncounter = "none";
        private string customTextforLogTextAtEndOfEncounter = "none";

        private bool assassinationVictory = false;
        private string assassinationTargetTag = "none";

        private bool standGroundVictory = false;
        private int standGroundTimer = 10;

        private bool conquerVictory = false;
        private bool conquerTargetsCumulative = true;
        private int conquerTarget1X = -1;
        private int conquerTarget1Y = -1;
        private int conquerTarget2X = -1;
        private int conquerTarget2Y = -1;
        private int conquerTarget3X = -1;
        private  int conquerTarget3Y = -1;

        private bool noSpellCastModifier = false;
        private bool noTraitUseModifier = false;
        private bool noItemUseModifier = false;
        private bool onlyOneMoveModifier = false;

        //private bool timeLimitVictory = false;
        //private int timeLimitVictoryRoundGoal = 0;

        private bool timeLimitDefeat = false;
        private int timeLimitTimer = 10;

        private bool protectionDefeat = false;
        private string protectionTargetName = "none";

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

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Challenge rating of the encounter, use expected average player level in party; skull icons on props show difficulty ingame: 3 Skulls = 6+ levels diff, 2 Skulls = 4+ levels diff, 1 Skull = 2+ levels diff. Uses challengeSkull.png as graphic.")]
        public int ChallengeLevel
        {
            get { return challengeLevel; }
            set { challengeLevel = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("When true, a single challengeHidden.png is shown instaed of the challenge rating (number of challengeSkull.png).")]
        public bool ChallengeHidden
        {
            get { return challengeHidden; }
            set { challengeHidden = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("If true, the autogenerated text MessageBox (Victory & Loss conditions, battlefield modifiers) at start of an encounter is shown.")]
        public bool ShowDefaultMessageBoxAtStartOfEncounter
        {
            get { return showDefaultMessageBoxAtStartOfEncounter; }
            set { showDefaultMessageBoxAtStartOfEncounter = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("If true, the autogenerated text MessageBox (Victory & Loss conditions, battlefield modifiers) at end of an encounter is shown.")]
        public bool ShowDefaultMessageBoxAEndOfEncounter
        {
            get { return showDefaultMessageBoxAtEndOfEncounter; }
            set { showDefaultMessageBoxAtEndOfEncounter = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("If true, the autogenerated log text (Victory & Loss conditions, battlefield modifiers) at start of an encounter is shown.")]
        public bool ShowDefaultLogTextAtStartOfEncounter
        {
            get { return showDefaultLogTextAtStartOfEncounter; }
            set { showDefaultLogTextAtStartOfEncounter = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("If true, the autogenerated log text (Victory & Loss conditions, battlefield modifiers) at end of an encounter is shown.")]
        public bool ShowDefaultLogTextAtEndOfEncounter
        {
            get { return showDefaultLogTextAtEndOfEncounter; }
            set { showDefaultLogTextAtEndOfEncounter = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("If you enter text here, this is used instead of default text.")]
        public string CustomTextforMessageBoxAtStartOfEncounter
        {
            get { return customTextforMessageBoxAtStartOfEncounter; }
            set { customTextforMessageBoxAtStartOfEncounter = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("If you enter text here, this is used instead of default text.")]
        public string CustomTextforMessageBoxAEndOfEncounter
        {
            get { return customTextforMessageBoxAtEndOfEncounter; }
            set { customTextforMessageBoxAtEndOfEncounter = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("If you enter text here, this is used instead of default text.")]
        public string CustomTextforLogTextAtStartOfEncounter
        {
            get { return customTextforLogTextAtStartOfEncounter; }
            set { customTextforLogTextAtStartOfEncounter = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("If you enter text here, this is used instead of default text.")]
        public string CustomTextforLogTextAtEndOfEncounter
        {
            get { return customTextforLogTextAtEndOfEncounter; }
            set { customTextforLogTextAtEndOfEncounter = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Map image file name (do NOT include file extension). Image must be 350x350 pixels.")]
        public string MapImage
        {
            get { return mapImage; }
            set { mapImage = value; }
        }
        
        [CategoryAttribute("07 - Not used anymore"), DescriptionAttribute("When using a premade area graphic, this how large in px one tile is supposed to be; defaults to 50.")]
        public int SingleImageMapTileSize
        {
            get { return singleImageMapTileSize; }
            set { singleImageMapTileSize = value; }
        }

        //private bool assassinationVictory = false;
        //private string assassinationTargetTag = "none";

        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("When true, this battle is instantly won once the player slays a creature with this tag.")]
        public bool AssassinationVictory
        {
            get { return assassinationVictory; }
            set { assassinationVictory = value; }
        }

        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("Tag of the creature that has to be slain in case an assassination victory is enabled.")]
        public string AssassinationTargetTag
        {
            get { return assassinationTargetTag; }
            set { assassinationTargetTag = value; }
        }

        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("When true, this battle is instantly won once the party has survived the defined number of rounds")]
        public bool StandGroundVictory
        {
            get { return standGroundVictory; }
            set { standGroundVictory = value; }
        }

        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("Number of rounds to survive for fullfilling the stand ground victory condition")]
        public int StandGroundTimer
        {
            get { return standGroundTimer; }
            set { standGroundTimer = value; }
        }
        /*
        private bool conquerVictory = true;
        private bool conquerTargetsCumulative = true;
        private int conquerTarget1X = 8;
        private int conquerTarget1Y = 8;
        private int conquerTarget2X = -1;
        private int conquerTarget2Y = -1;
        private int conquerTarget3X = -1;
        private int conquerTarget3Y = -1;
        */
        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("When true, this battle is instantly once the party has occupied all required conquer locations with a living pc at the start of a round.")]
        public bool ConquerVictory
        {
            get { return conquerVictory; }
            set { conquerVictory = value; }
        }
        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("When true, all of the required conquer locations must be occupied at the same time. When false, it is sufficient if only one of the (alternatively) required conquer locations is occupied.")]
         public bool ConquerTargetsCumulative
        {
            get { return conquerTargetsCumulative; }
            set { conquerTargetsCumulative = value; }
        }
        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("X coordinate for first conquer location, use -1 for x and y to disable")]
        public int ConquerTarget1X
        {
            get { return conquerTarget1X; }
            set { conquerTarget1X = value; }
        }
        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("X coordinate for second conquer location, use -1 for x and y to disable")]
        public int ConquerTarget2X
        {
            get { return conquerTarget2X; }
            set { conquerTarget2X = value; }
        }
        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("X coordinate for third conquer location, use -1 for x and y to disable")]
        public int ConquerTarget3X
        {
            get { return conquerTarget3X; }
            set { conquerTarget3X = value; }
        }
        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("Y coordinate for first conquer location, use -1 for x and y to disable")]
        public int ConquerTarget1Y
        {
            get { return conquerTarget1Y; }
            set { conquerTarget1Y = value; }
        }
        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("Y coordinate for second conquer location, use -1 for x and y to disable")]
        public int ConquerTarget2Y
        {
            get { return conquerTarget2Y; }
            set { conquerTarget2Y = value; }
        }
        [CategoryAttribute("04 - Victory conditions"), DescriptionAttribute("Y coordinate for third conquer location, use -1 for x and y to disable")]
        public int ConquerTarget3Y
        {
            get { return conquerTarget3Y; }
            set { conquerTarget3Y = value; }
        }

        //private bool timeLimitDefeat = false;
        //private int timeLimitTimer = 10;
        [CategoryAttribute("05 - Defeat conditions"), DescriptionAttribute("When true, this battle is instantly lost after x rounds.")]
        public bool TimeLimitDefeat
        {
            get { return timeLimitDefeat; }
            set { timeLimitDefeat = value; }
        }

        [CategoryAttribute("05 - Defeat conditions"), DescriptionAttribute("Number of rounds after a battle is lost under the time limit defeat condition.")]
        public int TimeLimitTimer
        {
            get { return timeLimitTimer; }
            set { timeLimitTimer = value; }
        }
        //private bool protectionDefeat = false;
        //private string protectionTargetName = "none";
        [CategoryAttribute("05 - Defeat conditions"), DescriptionAttribute("When true, this battle is lost if an (allied) player character with name x drops to 0 or less hp. Note: enter the name below, not tag or filename")]
        public bool ProtectionDefeat
        {
            get { return protectionDefeat; }
            set { protectionDefeat = value; }
        }

        [CategoryAttribute("05 - Defeat conditions"), DescriptionAttribute("Name of the (allied) pc to protect (use pc.name property, not pc.tag or filename).")]
        public string ProtectionTargetName
        {
            get { return protectionTargetName; }
            set { protectionTargetName = value; }
        }
        //public bool noSpellCastModifier = false;
        //public bool noTraitUseModifier = false;
        //public bool noItemUseModifier = false;
        //public bool onlyOneMoveModifier = false;

        [CategoryAttribute("06 - Battle modifiers"), DescriptionAttribute("If true, no spell casting is possible for the party in this encounter.")]
        public bool NoSpellCastModifier
        {
            get { return noSpellCastModifier; }
            set { noSpellCastModifier = value; }
        }

        [CategoryAttribute("06 - Battle modifiers"), DescriptionAttribute("If true, no trait using is possible for the party in this encounter.")]
        public bool NoTraitUseModifier
        {
            get { return noTraitUseModifier; }
            set { noTraitUseModifier = value; }
        }

        [CategoryAttribute("06 - Battle modifiers"), DescriptionAttribute("If true, no item using is possible for the party in this encounter.")]
        public bool NoItemUseModifier
        {
            get { return noItemUseModifier; }
            set { noItemUseModifier = value; }
        }

        [CategoryAttribute("06 - Battle modifiers"), DescriptionAttribute("If true, the player characters can only move one step each turn in this encounter.")]
        public bool OnlyOneMoveModifier
        {
            get { return onlyOneMoveModifier; }
            set { onlyOneMoveModifier = value; }
        }

        [CategoryAttribute("07 - Not used anymore"), DescriptionAttribute("if True, the encounter will use the single image from the MapImage filename instead of using the tiles.")]
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
        [CategoryAttribute("03 - Music"), DescriptionAttribute("Filename of music for the area (no extension)")]
        public string AreaMusic
        {
            get { return areaMusic; }
            set { areaMusic = value; }
        }
        [CategoryAttribute("03 - Music"), DescriptionAttribute("Delay between replaying music (in milliseconds)")]
        public int AreaMusicDelay
        {
            get { return areaMusicDelay; }
            set { areaMusicDelay = value; }
        }
        [CategoryAttribute("03 - Music"), DescriptionAttribute("Add a random amount of delay (between 0 and this value) to the AreaMusicDelay value (in milliseconds)")]
        public int AreaMusicDelayRandomAdder
        {
            get { return areaMusicDelayRandomAdder; }
            set { areaMusicDelayRandomAdder = value; }
        }
        [Browsable(true), TypeConverter(typeof(LogicTreeConverter))]
        [CategoryAttribute("07 - Not used anymore"), DescriptionAttribute("LogicTree name to be run at the start of each combat round")]
        public string OnStartCombatRoundLogicTree
        {
            get { return onStartCombatRoundLogicTree; }
            set { onStartCombatRoundLogicTree = value; }
        }
        [CategoryAttribute("07 - Not used anymore"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnStartCombatRoundParms
        {
            get { return onStartCombatRoundParms; }
            set { onStartCombatRoundParms = value; }
        }
        [Browsable(true), TypeConverter(typeof(LogicTreeConverter))]
        [CategoryAttribute("07 - Not used anymore"), DescriptionAttribute("LogicTree name to be run at the start of each PC and Enemy turn in combat")]
        public string OnStartCombatTurnLogicTree
        {
            get { return onStartCombatTurnLogicTree; }
            set { onStartCombatTurnLogicTree = value; }
        }
        [CategoryAttribute("07 - Not used anymore"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnStartCombatTurnParms
        {
            get { return onStartCombatTurnParms; }
            set { onStartCombatTurnParms = value; }
        }
        [Browsable(true), TypeConverter(typeof(LogicTreeConverter))]
        [CategoryAttribute("07 - Not used anymore"), DescriptionAttribute("LogicTree name to be run at the end of combat")]
        public string OnEndCombatLogicTree
        {
            get { return onEndCombatLogicTree; }
            set { onEndCombatLogicTree = value; }
        }
        [CategoryAttribute("07 - Not used anymore"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
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
