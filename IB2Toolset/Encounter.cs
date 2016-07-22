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

namespace IB2miniToolset
{
    public class Encounter
    {
        private string encName = "newEncounter";
        //private string mapImage = "none";
        //private bool useMapImage = false;
        private bool useDayNightCycle = false;
        public int MapSizeX = 11;
        public int MapSizeY = 11;
        //public List<TileEnc> encounterTiles = new List<TileEnc>();
        public List<string> Layer1Filename = new List<string>();
        public List<int> Layer1Rotate = new List<int>();
        public List<int> Layer1Mirror = new List<int>();
        public List<string> Layer2Filename = new List<string>();
        public List<int> Layer2Rotate = new List<int>();
        public List<int> Layer2Mirror = new List<int>();
        public List<int> Walkable = new List<int>();
        public List<int> LoSBlocked = new List<int>();
        public List<CreatureRefs> encounterCreatureRefsList = new List<CreatureRefs>();
        public List<string> encounterCreatureList = new List<string>();
        public List<ItemRefs> encounterInventoryRefsList = new List<ItemRefs>();
        public List<Coordinate> encounterPcStartLocations = new List<Coordinate>();
        public int goldDrop = 0;        
        private string onSetupCombatIBScript = "none";
        private string onSetupCombatIBScriptParms = "";
        private string onStartCombatRoundIBScript = "none";
        private string onStartCombatRoundIBScriptParms = "";
        private string onStartCombatTurnIBScript = "none";
        private string onStartCombatTurnIBScriptParms = "";
        private string onEndCombatIBScript = "none";
        private string onEndCombatIBScriptParms = "";

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Name of Encounter, must be unique")]
        public string encounterName
        {
            get { return encName; }
            set { encName = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Set to true if this encounter will use the day/night cycle tinting.")]
        public bool UseDayNightCycle
        {
            get { return useDayNightCycle; }
            set { useDayNightCycle = value; }
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

            copy.Layer1Filename = new List<string>();
            foreach (string s in this.Layer1Filename)
            {
                copy.Layer1Filename.Add(s);
            }
            copy.Layer1Mirror = new List<int>();
            foreach (int s in this.Layer1Mirror)
            {
                copy.Layer1Mirror.Add(s);
            }
            copy.Layer1Rotate = new List<int>();
            foreach (int s in this.Layer1Rotate)
            {
                copy.Layer1Rotate.Add(s);
            }
            copy.Layer2Filename = new List<string>();
            foreach (string s in this.Layer2Filename)
            {
                copy.Layer2Filename.Add(s);
            }
            copy.Layer2Mirror = new List<int>();
            foreach (int s in this.Layer2Mirror)
            {
                copy.Layer2Mirror.Add(s);
            }
            copy.Layer2Rotate = new List<int>();
            foreach (int s in this.Layer2Rotate)
            {
                copy.Layer2Rotate.Add(s);
            }
            copy.Walkable = new List<int>();
            foreach (int s in this.Walkable)
            {
                copy.Walkable.Add(s);
            }
            copy.LoSBlocked = new List<int>();
            foreach (int s in this.LoSBlocked)
            {
                copy.LoSBlocked.Add(s);
            }
            return copy;
        }
        public void SetAllToGrass()
        {
            for (int index = 0; index < (this.MapSizeX * this.MapSizeY); index++)
            {
                this.Layer1Filename.Add("t_grass");
                this.Layer1Rotate.Add(0);
                this.Layer1Mirror.Add(0);
                this.Layer2Filename.Add("t_blank");
                this.Layer2Rotate.Add(0);
                this.Layer2Mirror.Add(0);
                this.Walkable.Add(1);
                this.LoSBlocked.Add(0);
            }

            /*for (int x = 0; x < this.MapSizeX; x++)
            {
                for (int y = 0; y < this.MapSizeY; y++)
                {
                    TileEnc t = new TileEnc();
                    encounterTiles.Add(t);   
                                                         
                }
            }*/
        }
    }  
}
