using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using Newtonsoft.Json;
//using IceBlink;

namespace IB2Toolset
{
    public class Creature
    {
        public enum crCategory
        {
            Ranged = 0,
            Melee = 1
        }
        public enum crProjectileImage
        {
            Arrow = 0,
            Bolt = 1,
            Stone = 2,
            Dart = 3,
            Dagger = 4
        }

        #region Fields
        [JsonIgnore]
        public Bitmap creatureIconBitmap;

        private string _factionTag = "none";

        private string _tokenFilename = "blank";
        private int _creatureSize = 1;
        public bool combatFacingLeft = true;
        public int combatLocX = 0;
        public int combatLocY = 0;
        private int _moveDistance = 5;
        private int _initiativeBonus = 0;
        private string _name = "newCreature";
        private string _tag = "newTag";
        private string _resref = "newResRef";
        private string _desc = ""; //detailed description
        private string _parentNodeName = "newCategory";
        private int _level = 1;
        private int _hp = 10;
        private int _hpMax = 10;
        private int _sp = 50;
        private int _XP = 10;
        private int _ac = 10;
        public string cr_status = "Alive"; //Alive, Dead, Held
        private int _att = 0;
        private int _attRange = 1;
        private int _damageNumDice = 1; //number of dice to roll for damage
        private int _damageDie = 4; //type of dice to roll for damage
        private int _damageAdder = 0;
        private string _category = "Melee"; //catergory type (Ranged, Melee)
        //private crCategory cr_category = crCategory.Melee; //catergory type (ranged, melee)
        private string _projSpriteFilename = "none"; //sprite filename including .spt
        private string _spriteEndingFilename = "none"; //sprite to use for end effect of projectiles
        private string _attackSound = "none";
        private int _numberOfAttacks = 1;
        private string _ai = "BasicAttacker";
        //private AiBasicTactic cr_ai = AiBasicTactic.BasicAttacker;
        private int _fortitude = 0;
        private int _will = 0;
        private int _reflex = 0;
        private int _damageTypeResistanceValueAcid = 0;
        private int _damageTypeResistanceValueNormal = 0;
        private int _damageTypeResistanceValueCold = 0;
        private int _damageTypeResistanceValueElectricity = 0;
        private int _damageTypeResistanceValueFire = 0;
        private int _damageTypeResistanceValueMagic = 0;
        private int _damageTypeResistanceValuePoison = 0;
        private int _spRegenerationPerRound = 0;
        private int _hpRegenerationPerRound = 0;
        private string _typeOfDamage = "Normal"; //Normal,Acid,Cold,Electricity,Fire,Magic,Poison
        //private DamageType typeOfDamage = DamageType.Slashing;
        private string _onScoringHit = "none";
        private string _onScoringHitParms = "none";
        private string _onScoringHitCastSpellTag = "none";
        private string _onDeathLogicTree = "none";
        private string _onDeathParms = "";
        private string _onDeathIBScript = "none";
        private string _onDeathIBScriptParms = "";
        private string _labelForCastAction = "casts";
        //private ScriptSelectEditorReturnObject onScoringHit = new ScriptSelectEditorReturnObject();  
        private List<string> _knownSpellsTags = new List<string>();
        private List<LocalInt> _castChances = new List<LocalInt>();
        public List<Effect> cr_effectsList = new List<Effect>();
        private List<LocalInt> creatureLocalInts = new List<LocalInt>();
        private List<LocalString> creatureLocalStrings = new List<LocalString>();
        private int _percentChanceToCastSpell = 100;
        private int _percentRequirementOfTargetInjuryForHealSpells = 50;
        private int _percentRequirementOfTargetSPLossForRestoreSPSpells = 50;

        #endregion

        #region Properties
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Token Filename of the Creature"), ReadOnly(true)]
        public string cr_tokenFilename
        {
            get { return _tokenFilename; }
            set { _tokenFilename = value; }
        }

        [CategoryAttribute("00 - Main"), DescriptionAttribute("Assign the tag of the creature's faction here (or leave at none). Faction can grant a faction buff in combat to AC/toHit/Saves.")]
        public string factionTag
        {
            get { return _factionTag; }
            set { _factionTag = value; }
        }

        [CategoryAttribute("00 - Main"), DescriptionAttribute("Name of the creature that shows up in the game engine")]
        public string cr_name
        {
            get { return _name; }
            set { _name = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Tag of the creature (Must be unique)")]
        public string cr_tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Resource Reference name of the creature used for updating all placed objects that share the same identifier (must be unique from other blueprints")]
        public string cr_resref
        {
            get { return _resref; }
            set { _resref = value; }
        }
        [Editor(typeof(MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Description of the creature")]
        public string cr_desc
        {
            get
            {
                return _desc;
            }
            set
            {
                _desc = value;
            }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Category that this creature belongs to")]
        public string cr_parentNodeName
        {
            get
            {
                return _parentNodeName;
            }
            set
            {
                _parentNodeName = value;
            }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Move distance for creature per round")]
        public int moveDistance
        {
            get { return _moveDistance; }
            set { _moveDistance = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Adder to initiative roll. Equivalent to the dexterity bonus that PCs use...so for a PC with a DEX of 16, the bonus would be (16 - 10) / 2 = 3")]
        public int initiativeBonus
        {
            get { return _initiativeBonus; }
            set { _initiativeBonus = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Level of creature")]
        public int cr_level
        {
            get { return _level; }
            set { _level = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Number for size of the creature (1 = normal,1x1, 2 = wide,2x1, 3 = tall,1x2 and 4 = large,2x2)")]
        public int creatureSize
        {
            get { return _creatureSize; }
            set { _creatureSize = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Creature's starting hp")]
        public int hp
        {
            get { return _hp; }
            set { _hp = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Creature's maximum hp")]
        public int hpMax
        {
            get { return _hpMax; }
            set { _hpMax = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Starting sp")]
        public int sp
        {
            get { return _sp; }
            set { _sp = value; }
        }
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("total AC for creature")]
        public int AC
        {
            get { return _ac; }
            set { _ac = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Known Spells"), ReadOnly(true), Browsable(false)]        
        public List<string> knownSpellsTags
        {
            get { return _knownSpellsTags; }
            set { _knownSpellsTags = value; }
        }
        [CategoryAttribute("01 - AI"), DescriptionAttribute("Cast Chances per Spell")]
        public List<LocalInt> castChances
        {
            get { return _castChances; }
            set { _castChances = value; }
        }
        [CategoryAttribute("01 - AI"), DescriptionAttribute("Chance to try spell casting this round")]
        public int percentChanceToCastSpell
        {
            get { return _percentChanceToCastSpell; }
            set { _percentChanceToCastSpell = value; }
        }
        [CategoryAttribute("01 - AI"), DescriptionAttribute("from 0 to 100, defaults at 50; a value of 75 eg means that a target must have at least lost 75% of it hp before coming an eglible heal target of this caster")]
        public int percentRequirementOfTargetInjuryForHealSpells
        {
            get { return _percentRequirementOfTargetInjuryForHealSpells; }
            set { _percentRequirementOfTargetInjuryForHealSpells = value; }
        }
        [CategoryAttribute("01 - AI"), DescriptionAttribute("from 0 to 100, defaults at 50; a value of 75 eg means that a target must have at least lost 75% of it sp before coming an eglible restore sp target of this caster")]
        public int percentRequirementOfTargetSPLossForRestoreSPSpells
        {
            get { return _percentRequirementOfTargetSPLossForRestoreSPSpells; }
            set { _percentRequirementOfTargetSPLossForRestoreSPSpells = value; }
        }


        //private int _percentRequirementOfTargetInjuryForHealSpells = 50;
        //private int _percentRequirementOfTargetSPLossForRestoreSPSpells = 50;

        [CategoryAttribute("00 - Main"), DescriptionAttribute("HP regeneration per combat round; does not work for creatures of zero or below hp.")]
        public int hpRegenerationPerRound
        {
            get { return _hpRegenerationPerRound; }
            set { _hpRegenerationPerRound = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("SP regeneration per combat round; does not work for creatures of zero or below hp.")]
        public int spRegenerationPerRound
        {
            get { return _spRegenerationPerRound; }
            set { _spRegenerationPerRound = value; }
        }
        [CategoryAttribute("03 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValuePoison
        {
            get { return _damageTypeResistanceValuePoison; }
            set { _damageTypeResistanceValuePoison = value; }
        }
        [CategoryAttribute("03 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueMagic
        {
            get { return _damageTypeResistanceValueMagic; }
            set { _damageTypeResistanceValueMagic = value; }
        }        
        [CategoryAttribute("03 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueNormal
        {
            get { return _damageTypeResistanceValueNormal; }
            set { _damageTypeResistanceValueNormal = value; }
        }
        [CategoryAttribute("03 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueAcid
        {
            get { return _damageTypeResistanceValueAcid; }
            set { _damageTypeResistanceValueAcid = value; }
        }
        [CategoryAttribute("03 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueCold
        {
            get { return _damageTypeResistanceValueCold; }
            set { _damageTypeResistanceValueCold = value; }
        }
        [CategoryAttribute("03 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueElectricity
        {
            get { return _damageTypeResistanceValueElectricity; }
            set { _damageTypeResistanceValueElectricity = value; }
        }
        [CategoryAttribute("03 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueFire
        {
            get { return _damageTypeResistanceValueFire; }
            set { _damageTypeResistanceValueFire = value; }
        }
        [Browsable(true), TypeConverter(typeof(DamageTypeConverter))]
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("The Type of Damage (useful with immunity checks)")]
        public string cr_typeOfDamage
        {
            get { return _typeOfDamage; }
            set { _typeOfDamage = value; }
        }
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("Number of attacks per round on same target")]
        public int cr_numberOfAttacks
        {
            get { return this._numberOfAttacks; }
            set { this._numberOfAttacks = value; }
        }
        //[CategoryAttribute("07 - Scripts"), DescriptionAttribute("fires when the creature makes a successful hit on a target")]
        //[Editor(typeof(ScriptSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        //public ScriptSelectEditorReturnObject OnScoringHit
        //{
        //    get { return onScoringHit; }
        //    set { onScoringHit = value; }
        //}
        [CategoryAttribute("04 - Scripts"), DescriptionAttribute("fires when the creature makes a successful hit on a target")]
        public string onScoringHit
        {
            get { return _onScoringHit; }
            set { _onScoringHit = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("label for the cast action in log")]
        public string labelForCastAction
        {
            get { return _labelForCastAction; }
            set { _labelForCastAction = value; }
        }
        [CategoryAttribute("04 - Scripts"), DescriptionAttribute("(not used yet)optional input parameters if using a LogicTree...comma separated parameters")]
        public string onScoringHitParms
        {
            get { return _onScoringHitParms; }
            set { _onScoringHitParms = value; }
        }
        [CategoryAttribute("04 - Scripts"), DescriptionAttribute("fires when the creature dies")]
        public string onDeathLogicTree
        {
            get { return _onDeathLogicTree; }
            set { _onDeathLogicTree = value; }
        }
        [CategoryAttribute("04 - Scripts"), DescriptionAttribute("optional input parameters for the LogicTree...comma separated parameters")]
        public string onDeathParms
        {
            get { return _onDeathParms; }
            set { _onDeathParms = value; }
        }
        [CategoryAttribute("04 - Scripts"), DescriptionAttribute("IBScript that fires when the creature dies")]
        public string onDeathIBScript
        {
            get { return _onDeathIBScript; }
            set { _onDeathIBScript = value; }
        }
        [CategoryAttribute("04 - Scripts"), DescriptionAttribute("optional input parameters for the IBScript...comma separated parameters")]
        public string onDeathIBScriptParms
        {
            get { return _onDeathIBScriptParms; }
            set { _onDeathIBScriptParms = value; }
        }
        [Browsable(true), TypeConverter(typeof(SpellTagTypeConverter))]
        [CategoryAttribute("06 - Spell/Effect System"), DescriptionAttribute("Cast this spell (special ability) upon the target after making a successful attack (melee/ranged) hit on the target")]
        public string onScoringHitCastSpellTag
        {
            get { return _onScoringHitCastSpellTag; }
            set { _onScoringHitCastSpellTag = value; }
        }
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("damage adder")]
        public int damageAdder
        {
            get { return this._damageAdder; }
            set { this._damageAdder = value; }
        }
        /*[CategoryAttribute("00 - Basic Creature"), DescriptionAttribute("initiative modifier")]
        public int InitiativeModifier
        {
            get { return this.initiativeModifier; }
            set { this.initiativeModifier = value; }
        }*/
        /*[CategoryAttribute("00 - Basic Creature"), DescriptionAttribute("Generic AI for the creature to use")]
        public AiBasicTactic CreatureAI
        {
            get { return this.cr_ai; }
            set { this.cr_ai = value; }
        }*/
        [Browsable(true), TypeConverter(typeof(AiTypeConverter))]
        [CategoryAttribute("01 - AI"), DescriptionAttribute("Generic AI for the creature to use")]
        public string cr_ai
        {
            get { return this._ai; }
            set { this._ai = value; }
        }              
        [Browsable(true), TypeConverter(typeof(SpriteConverter))]
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("Filename of the Sprite to use for the creature's projectiles")]
        public string cr_projSpriteFilename
        {
            get
            {
                return _projSpriteFilename;
            }
            set
            {
                _projSpriteFilename = value;
            }
        }
        [Browsable(true), TypeConverter(typeof(SpriteConverter))]
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("sprite to use for end effect of projectiles")]
        public string cr_spriteEndingFilename
        {
            get
            {
                return _spriteEndingFilename;
            }
            set
            {
                _spriteEndingFilename = value;
            }
        }
        [Browsable(true), TypeConverter(typeof(SoundConverter))]
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("Filename of sound to play when the creature makes an attack (no extension)")]
        public string cr_attackSound
        {
            get { return _attackSound; }
            set { _attackSound = value; }
        }
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Experience Points for the creature")]
        public int cr_XP
        {
            get
            {
                return _XP;
            }
            set
            {
                _XP = value;
            }
        }
        [XmlElement]
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Fortitude value for Creatures (overrides Class assigned value)")]
        public int fortitude
        {
            get { return _fortitude; }
            set { _fortitude = value; }
        }
        [XmlElement]
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Will value for Creatures (overrides Class assigned value)")]
        public int will
        {
            get { return _will; }
            set { _will = value; }
        }
        [XmlElement]
        [CategoryAttribute("00 - Main"), DescriptionAttribute("Reflex value for Creatures (overrides Class assigned value)")]
        public int reflex
        {
            get { return _reflex; }
            set { _reflex = value; }
        }
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("Attack of the creature")]
        public int cr_att
        {
            get
            {
                return _att;
            }
            set
            {
                _att = value;
            }
        }
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("Attack range of the creature measured in squares")]
        public int cr_attRange
        {
            get
            {
                return _attRange;
            }
            set
            {
                _attRange = value;
            }
        }
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("Number of dice to roll for damage")]
        public int cr_damageNumDice
        {
            get
            {
                return _damageNumDice;
            }
            set
            {
                _damageNumDice = value;
            }
        }
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("Die to roll for damage")]
        public int cr_damageDie
        {
            get
            {
                return _damageDie;
            }
            set
            {
                _damageDie = value;
            }
        }
        [Browsable(true), TypeConverter(typeof(WeaponTypeConverter))]
        [CategoryAttribute("02 - Attack/Defend"), DescriptionAttribute("Is weapon a melee or ranged weapon")]
        public string cr_category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }
        [CategoryAttribute("05 - Locals"), DescriptionAttribute("Can be used for creating new properties or making individual creatures act unique.")]
        public List<LocalInt> CreatureLocalInts
        {
            get { return creatureLocalInts; }
            set { creatureLocalInts = value; }
        }
        [CategoryAttribute("05 - Locals"), DescriptionAttribute("Can be used for creating new properties or making individual creatures act unique.")]
        public List<LocalString> CreatureLocalStrings
        {
            get { return creatureLocalStrings; }
            set { creatureLocalStrings = value; }
        }
        #endregion

        public Creature()
        {
            //this.OnScoringHit.FilenameOrTag = "none";            
        }
        public void LoadCreatureBitmap(ParentForm prntForm)
        {
            if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + this.cr_tokenFilename + ".png"))
            {
                this.creatureIconBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + this.cr_tokenFilename + ".png");
            }
            else if (File.Exists(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\" + this.cr_tokenFilename + ".png"))
            {
                this.creatureIconBitmap = new Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\" + this.cr_tokenFilename + ".png");
            }
            else
            {
                this.creatureIconBitmap = new Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\" + "missingtexture.png");
            }
        }
        public Creature ShallowCopy()
        {
            return (Creature)this.MemberwiseClone();
        }
        public Creature DeepCopy()
        {
            Creature other = (Creature)this.MemberwiseClone();            
            other.knownSpellsTags = new List<string>();
            foreach (string s in this.knownSpellsTags)
            {
                other.knownSpellsTags.Add(s);
            }
            other.castChances = new List<LocalInt>();
            foreach (LocalInt l in this.castChances)
            {
                LocalInt Lint = new LocalInt();
                Lint.Key = l.Key;
                Lint.Value = l.Value;
                other.castChances.Add(Lint);
            }

            other.CreatureLocalInts = new List<LocalInt>();
            foreach (LocalInt l in this.CreatureLocalInts)
            {
                LocalInt Lint = new LocalInt();
                Lint.Key = l.Key;
                Lint.Value = l.Value;
                other.CreatureLocalInts.Add(Lint);
            }
            other.CreatureLocalStrings = new List<LocalString>();
            foreach (LocalString l in this.CreatureLocalStrings)
            {
                LocalString Lstr = new LocalString();
                Lstr.Key = l.Key;
                Lstr.Value = l.Value;
                other.CreatureLocalStrings.Add(Lstr);
            }
            //other.EffectsList = new Effects();            
            //other.onScoringHit = this.onScoringHit.DeepCopy();            
            return other;
        }
    }

    public class CreatureRefs
    {      
        private string _creatureResRef = "";
        private string _creatureTag = "";
        private int _creatureStartLocationX = 0;
        private int _creatureStartLocationY = 0;

        [CategoryAttribute("01 - Main"), DescriptionAttribute("ResRef of the creature reference (must be left the same as the blueprint creature's resref)")]
        public string creatureResRef
        {
            get { return _creatureResRef; }
            set { _creatureResRef = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the creature (Must be unique)")]
        public string creatureTag
        {
            get { return _creatureTag; }
            set { _creatureTag = value; }
        }        
        [CategoryAttribute("01 - Main"), ReadOnly(true)]
        public int creatureStartLocationX
        {
            get { return _creatureStartLocationX; }
            set { _creatureStartLocationX = value; }
        }
        [CategoryAttribute("01 - Main"), ReadOnly(true)]
        public int creatureStartLocationY
        {
            get { return _creatureStartLocationY; }
            set { _creatureStartLocationY = value; }
        }
        
        public CreatureRefs()
        {
        }
        public CreatureRefs DeepCopy()
        {
            CreatureRefs other = (CreatureRefs)this.MemberwiseClone();
            return other;
        }
    }
}
