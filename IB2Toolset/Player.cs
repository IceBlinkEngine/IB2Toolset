using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Newtonsoft.Json;

namespace IB2Toolset
{
    public class Player
    {
        public string tokenFilename = "blank";
	    public bool combatFacingLeft = true;
        private bool _nonRemoveablePc = false;
	    public int combatLocX = 0;
        public int combatLocY = 0;
        public int moveDistance = 5;
        private int _baseMoveDistance = 5;
	    private string _name = "newPlayer";	
	    private string _tag = "newTag";	
	    private string _raceTag = "human";
        [JsonIgnore]
	    public Race race = new Race();
	    private string _classTag = "fighter";
        [JsonIgnore]
	    public PlayerClass playerClass = new PlayerClass();
	    public List<string> knownSpellsTags = new List<string>();
	    public List<Spell> knownSpellsList = new List<Spell>();	
        public List<string> knownTraitsTags = new List<string>();
	    public List<Trait> knownTraitsList = new List<Trait>();	
	    public List<Effect> effectsList = new List<Effect>();
	    private int _classLevel = 1;
	    private bool _isMale = true;
	    public string charStatus = "Alive"; //Alive, Dead, Held
	    private int _baseFortitude = 0;
	    private int _baseWill = 0;
	    private int _baseReflex = 0;
	    public int fortitude = 0;
	    public int will = 0;
	    public int reflex = 0;        
        public int strength = 10;
	    public int dexterity = 10;	
	    public int intelligence = 10;	
	    public int charisma = 10;
	    private int _baseStr = 10;
	    private int _baseDex = 10;
	    private int _baseInt = 10;
	    private int _baseCha = 10;
	    private int _ACBase = 10;
	    public int AC = 10;
	    public int classBonus = 0;
	    public int baseAttBonus = 1;
	    public int baseAttBonusAdders = 0;
	    public int hp = 10;
	    public int hpMax = 10;
	    public int sp = 50;
	    public int spMax = 50;
	    private int _XP = 0;
	    public int XPNeeded = 200;
        public int hpRegenTimePassedCounter = 0;
        public int spRegenTimePassedCounter = 0;
	    public ItemRefs HeadRefs = new ItemRefs();
        public ItemRefs NeckRefs = new ItemRefs();
        public ItemRefs BodyRefs = new ItemRefs();
        public ItemRefs MainHandRefs = new ItemRefs();
        public ItemRefs OffHandRefs = new ItemRefs();
        public ItemRefs RingRefs = new ItemRefs();
        public ItemRefs Ring2Refs = new ItemRefs();
        public ItemRefs FeetRefs = new ItemRefs();
        public ItemRefs AmmoRefs = new ItemRefs();
        //public string HeadTag = "";
        //public string NeckTag = "";
        //public string BodyTag = "";
        //public string MainHandTag = "";
        //public string OffHandTag = "";
        //public string RingTag = "";
        //public string Ring2Tag = "";
        //public string FeetTag = "";
        //[JsonIgnore]
		//public Item Head;
        //[JsonIgnore]
		//public Item Neck;
        //[JsonIgnore]
		//public Item Body;
        //[JsonIgnore]
        //public Item MainHand;
        //[JsonIgnore]
		//public Item OffHand;
        //[JsonIgnore]
		//public Item Ring;
        //[JsonIgnore]
		//public Item Ring2;
        //[JsonIgnore]
		//public Item Feet;	
	    public int damageTypeResistanceTotalAcid = 0;
	    public int damageTypeResistanceTotalCold = 0;
	    public int damageTypeResistanceTotalNormal = 0;
	    public int damageTypeResistanceTotalElectricity = 0;
	    public int damageTypeResistanceTotalFire = 0;
	    public int damageTypeResistanceTotalMagic = 0;
	    public int damageTypeResistanceTotalPoison = 0;

        [CategoryAttribute("00 - Main"), DescriptionAttribute("Name of the player")]
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("tag of the player. Used for save player file name, inter party conversation file name, etc.")]
        public string tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("If this is a Companion that must remain in the party, set to 'true'")]
        public bool nonRemoveablePc
        {
            get { return _nonRemoveablePc; }
            set { _nonRemoveablePc = value; }
        }
        [Browsable(true), TypeConverter(typeof(RaceTagTypeConverter))]
        [CategoryAttribute("00 - Main"), DescriptionAttribute("tag of the race.")]
        public string raceTag
        {
            get { return _raceTag; }
            set { _raceTag = value; }
        }
        [Browsable(true), TypeConverter(typeof(PlayerClassTagTypeConverter))]
        [CategoryAttribute("00 - Main"), DescriptionAttribute("tag of the player class.")]
        public string classTag
        {
            get { return _classTag; }
            set { _classTag = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Level of the player.")]
        public int classLevel
        {
            get { return _classLevel; }
            set { _classLevel = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Base movement distance in combat round.")]
        public int baseMoveDistance
        {
            get { return _baseMoveDistance; }
            set { _baseMoveDistance = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Used to determine gender.")]
        public bool isMale
        {
            get { return _isMale; }
            set { _isMale = value; }
        }
        [CategoryAttribute("02 - Saving Throws"), DescriptionAttribute("base fortitude saving throw")]
        public int baseFortitude
        {
            get { return _baseFortitude; }
            set { _baseFortitude = value; }
        }
        [CategoryAttribute("02 - Saving Throws"), DescriptionAttribute("base will saving throw")]
        public int baseWill
        {
            get { return _baseWill; }
            set { _baseWill = value; }
        }
        [CategoryAttribute("02 - Saving Throws"), DescriptionAttribute("base reflex saving throw")]
        public int baseReflex
        {
            get { return _baseReflex; }
            set { _baseReflex = value; }
        }
        [CategoryAttribute("01 - Attributes"), DescriptionAttribute("base strength attribute")]
        public int baseStr
        {
            get { return _baseStr; }
            set { _baseStr = value; }
        }
        [CategoryAttribute("01 - Attributes"), DescriptionAttribute("base dexterity attribute")]
        public int baseDex
        {
            get { return _baseDex; }
            set { _baseDex = value; }
        }
        [CategoryAttribute("01 - Attributes"), DescriptionAttribute("base intelligence attribute")]
        public int baseInt
        {
            get { return _baseInt; }
            set { _baseInt = value; }
        }
        [CategoryAttribute("01 - Attributes"), DescriptionAttribute("base charisma attribute")]
        public int baseCha
        {
            get { return _baseCha; }
            set { _baseCha = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("base armor class")]
        public int ACBase
        {
            get { return _ACBase; }
            set { _ACBase = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("starting XP")]
        public int XP
        {
            get { return _XP; }
            set { _XP = value; }
        }

        public Player()
        {
        }
    }
}
