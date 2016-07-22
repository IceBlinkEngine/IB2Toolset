using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using Newtonsoft.Json;
//using IceBlink;

namespace IB2miniToolset
{
    public class Effect
    {        
        #region Fields        
        private string _name = "newEffect";
        private string _tag = "newEffectTag";
        private string _tagOfSender = "senderTag";
        private int _classLevelOfSender = 0;
        private string _description = "";
        private string _spriteFilename = "held";
        private int _durationInUnits = 0;
        private int _currentDurationInUnits = 0;
        private int _startingTimeInUnits = 0;
        private int _babModifier = 0;
        private int _acModifier = 0;
        private bool _isStackableEffect = false;
        private bool _isStackableDuration = false;
        private bool _usedForUpdateStats = false;
        private string _effectScript = "efGeneric";

        private string _saveCheckType = "none"; //none, reflex, will, fortitude
        private int _saveCheckDC = 10;
        
        //DAMAGE (hp)
        private bool _doDamage = false;
        private string _damType = "Normal"; //Normal,Acid,Cold,Electricity,Fire,Magic,Poison
        //(for reference) Attack: AdB+C for every D levels after level E up to F levels total
        private int _damNumOfDice = 0; //(A)how many dice to roll
        private int _damDie = 0; //(B)type of die to roll such as 4 sided or 10 sided, etc.
        private int _damAdder = 0; //(C)integer adder to total damage such as the "1" in 2d4+1
        private int _damAttacksEveryNLevels = 0; //(D)
        private int _damAttacksAfterLevelN = 0; //(E)
        private int _damAttacksUpToNLevelsTotal = 0; //(F)
        //(for reference) NumOfAttacks: A of these attacks for every B levels after level C up to D attacks total
        private int _damNumberOfAttacks = 0; //(A)
        private int _damNumberOfAttacksForEveryNLevels = 0; //(B)
        private int _damNumberOfAttacksAfterLevelN = 0; //(C)
        private int _damNumberOfAttacksUpToNAttacksTotal = 0; //(D)

        //HEAL (hp)
        private bool _doHeal = false;
        private string _healType = "Organic"; //Organic (living things), NonOrganic (robots, constructs)
        //(for reference) HealActions: AdB+C for every D levels after level E up to F levels total
        private int _healNumOfDice = 0; //(A)how many dice to roll
        private int _healDie = 0; //(B)type of die to roll such as 4 sided or 10 sided, etc.
        private int _healAdder = 0; //(C)integer adder to total damage such as the "1" in 2d4+1
        private int _healActionsEveryNLevels = 0; //(D)
        private int _healActionsAfterLevelN = 0; //(E)
        private int _healActionsUpToNLevelsTotal = 0; //(F)

        //BUFF and DEBUFF
        private bool _doBuff = false;
        private bool _doDeBuff = false;
        private string _statusType = "none"; //none, Held, Immobile, Invisible, Silenced, etc.
        private int _modifyFortitude = 0;
        private int _modifyWill = 0;
        private int _modifyReflex = 0;
        //For PC only
        private int _modifyStr = 0;
        private int _modifyDex = 0;
        private int _modifyInt = 0;
        private int _modifyCha = 0;
        private int _modifyCon = 0;
        private int _modifyWis = 0;
        private int _modifyLuk = 0;
        //end PC only
        private int _modifyMoveDistance = 0;
        private int _modifyHpMax = 0;
        private int _modifySpMax = 0;
        private int _modifySp = 0;
        private int _modifyDamageTypeResistanceAcid = 0;
        private int _modifyDamageTypeResistanceCold = 0;
        private int _modifyDamageTypeResistanceNormal = 0;
        private int _modifyDamageTypeResistanceElectricity = 0;
        private int _modifyDamageTypeResistanceFire = 0;
        private int _modifyDamageTypeResistanceMagic = 0;
        private int _modifyDamageTypeResistancePoison = 0;
        private int _modifyNumberOfMeleeAttacks = 0;
        private int _modifyNumberOfRangedAttacks = 0;
        #endregion

        #region Properties
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Name of the Effect")]
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the Effect (Must be unique)")]
        public string tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the Effect sender, the one who created the effect (Must be unique)"), ReadOnly(true)]
        public string tagOfSender
        {
            get
            {
                return _tagOfSender;
            }
            set
            {
                _tagOfSender = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Level of Effect sender, the one (Creature, Player, Item, etc.) who created the effect"), ReadOnly(true)]
        public int classLevelOfSender
        {
            get
            {
                return _classLevelOfSender;
            }
            set
            {
                _classLevelOfSender = value;
            }
        }
        [Editor(typeof(MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Detailed description of effect with some stats")]
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        //[Browsable(true), TypeConverter(typeof(SpriteConverter))]
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Sprite to use for the effect (Sprite Filename with extension)")]
        public string spriteFilename
        {
            get
            {
                return _spriteFilename;
            }
            set
            {
                _spriteFilename = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("How long the Effect lasts in units of time. For instantaneous effects like fireball or mage bolt, set this to = 0")]
        public int durationInUnits
        {
            get
            {
                return _durationInUnits;
            }
            set
            {
                _durationInUnits = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("(no longer used)How long the Effect has been going on so far in units of time"), ReadOnly(true)]
        public int currentDurationInUnits
        {
            get
            {
                return _currentDurationInUnits;
            }
            set
            {
                _currentDurationInUnits = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("(no longer used)At what time did the Effect begin, in units of time"), ReadOnly(true)]
        public int startingTimeInUnits
        {
            get
            {
                return _startingTimeInUnits;
            }
            set
            {
                _startingTimeInUnits = value;
            }
        }
        [CategoryAttribute("03 - Effect"), DescriptionAttribute("adds or subtracts from BAB")]
        public int babModifier
        {
            get { return _babModifier; }
            set { _babModifier = value; }
        }
        [CategoryAttribute("03 - Effect"), DescriptionAttribute("adds or subtracts from Armor Class")]
        public int acModifier
        {
            get { return _acModifier; }
            set { _acModifier = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Should the effect be stackable, true = stackable")]
        public bool isStackableEffect
        {
            get
            {
                return _isStackableEffect;
            }
            set
            {
                _isStackableEffect = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Should the effect duration be stackable, true = stackable")]
        public bool isStackableDuration
        {
            get
            {
                return _isStackableDuration;
            }
            set
            {
                _isStackableDuration = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Determines if this effect is used specifically for modifying PC stats only")]
        public bool usedForUpdateStats
        {
            get
            {
                return _usedForUpdateStats;
            }
            set
            {
                _usedForUpdateStats = value;
            }
        }        
        /*[CategoryAttribute("02 - Scripts"), DescriptionAttribute("fires on each round or turn")]
        [Editor(typeof(ScriptSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public ScriptSelectEditorReturnObject EffectScript
        {
            get { return effectScript; }
            set { effectScript = value; }
        }*/
        public string effectScript
        {
            get { return _effectScript; }
            set { _effectScript = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Type of saving throw check made (none, reflex, will, fortitude)")]
        public string saveCheckType
        {
            get { return _saveCheckType; }
            set { _saveCheckType = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Difficulty Class for saving throw check (pass if 1d20 + reflex/will/fortitude >= DC)")]
        public int saveCheckDC
        {
            get { return _saveCheckDC; }
            set { _saveCheckDC = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("set to true if this Effect uses a Damage type Effect")]
        public bool doDamage
        {
            get { return _doDamage; }
            set { _doDamage = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("Type of damage (Normal,Acid,Cold,Electricity,Fire,Magic,Poison)")]
        public string damType
        {
            get { return _damType; }
            set { _damType = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("(A)how many dice to roll [Attack: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int damNumOfDice
        {
            get { return _damNumOfDice; }
            set { _damNumOfDice = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("(B)number of sides on the die such as 4 sided or 10 sided, etc. [Attack: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int damDie
        {
            get { return _damDie; }
            set { _damDie = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("(C)integer adder to total damage such as the '1' in 2d4+1 [Attack: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int damAdder
        {
            get { return _damAdder; }
            set { _damAdder = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("(D) [Attack: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int damAttacksEveryNLevels
        {
            get { return _damAttacksEveryNLevels; }
            set { _damAttacksEveryNLevels = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("(E) [Attack: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int damAttacksAfterLevelN
        {
            get { return _damAttacksAfterLevelN; }
            set { _damAttacksAfterLevelN = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("(F) [Attack: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int damAttacksUpToNLevelsTotal
        {
            get { return _damAttacksUpToNLevelsTotal; }
            set { _damAttacksUpToNLevelsTotal = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("(A) [NumOfAttacks: A of these attacks for every B levels after level C up to D attacks total]"), ReadOnly(true), Browsable(false)]
        public int damNumberOfAttacks
        {
            get { return _damNumberOfAttacks; }
            set { _damNumberOfAttacks = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("(B) [NumOfAttacks: A of these attacks for every B levels after level C up to D attacks total]"), ReadOnly(true), Browsable(false)]
        public int damNumberOfAttacksForEveryNLevels
        {
            get { return _damNumberOfAttacksForEveryNLevels; }
            set { _damNumberOfAttacksForEveryNLevels = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("(C) [NumOfAttacks: A of these attacks for every B levels after level C up to D attacks total]"), ReadOnly(true), Browsable(false)]
        public int damNumberOfAttacksAfterLevelN
        {
            get { return _damNumberOfAttacksAfterLevelN; }
            set { _damNumberOfAttacksAfterLevelN = value; }
        }
        [CategoryAttribute("02 - Damage"), DescriptionAttribute("(D) [NumOfAttacks: A of these attacks for every B levels after level C up to D attacks total]"), ReadOnly(true), Browsable(false)]
        public int damNumberOfAttacksUpToNAttacksTotal
        {
            get { return _damNumberOfAttacksUpToNAttacksTotal; }
            set { _damNumberOfAttacksUpToNAttacksTotal = value; }
        }
        [CategoryAttribute("02 - Heal"), DescriptionAttribute("set to true if this Effect uses a Heal type Effect")]
        public bool doHeal
        {
            get { return _doHeal; }
            set { _doHeal = value; }
        }
        [CategoryAttribute("02 - Heal"), DescriptionAttribute("Organic (living things), NonOrganic (robots, constructs)")]
        public string healType
        {
            get { return _healType; }
            set { _healType = value; }
        }
        [CategoryAttribute("02 - Heal"), DescriptionAttribute("(A) [HealActions: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int healNumOfDice
        {
            get { return _healNumOfDice; }
            set { _healNumOfDice = value; }
        }
        [CategoryAttribute("02 - Heal"), DescriptionAttribute("(B) [HealActions: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int healDie
        {
            get { return _healDie; }
            set { _healDie = value; }
        }
        [CategoryAttribute("02 - Heal"), DescriptionAttribute("(C) [HealActions: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int healAdder
        {
            get { return _healAdder; }
            set { _healAdder = value; }
        }
        [CategoryAttribute("02 - Heal"), DescriptionAttribute("(D) [HealActions: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int healActionsEveryNLevels
        {
            get { return _healActionsEveryNLevels; }
            set { _healActionsEveryNLevels = value; }
        }
        [CategoryAttribute("02 - Heal"), DescriptionAttribute("(E) [HealActions: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int healActionsAfterLevelN
        {
            get { return _healActionsAfterLevelN; }
            set { _healActionsAfterLevelN = value; }
        }
        [CategoryAttribute("02 - Heal"), DescriptionAttribute("(F) [HealActions: AdB+C for every D levels after level E up to F levels total]"), ReadOnly(true), Browsable(false)]
        public int healActionsUpToNLevelsTotal
        {
            get { return _healActionsUpToNLevelsTotal; }
            set { _healActionsUpToNLevelsTotal = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("set to true if this Effect uses a Buff type Effect")]
        public bool doBuff
        {
            get { return _doBuff; }
            set { _doBuff = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("set to true if this Effect uses a DeBuff type Effect")]
        public bool doDeBuff
        {
            get { return _doDeBuff; }
            set { _doDeBuff = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("none, Held, Immobile, Invisible, Silenced")]
        public string statusType
        {
            get { return _statusType; }
            set { _statusType = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyFortitude
        {
            get { return _modifyFortitude; }
            set { _modifyFortitude = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyWill
        {
            get { return _modifyWill; }
            set { _modifyWill = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyReflex
        {
            get { return _modifyReflex; }
            set { _modifyReflex = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("[applies to PCs Only] can be a positive or negative amount")]
        public int modifyStr
        {
            get { return _modifyStr; }
            set { _modifyStr = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("[applies to PCs Only] can be a positive or negative amount")]
        public int modifyDex
        {
            get { return _modifyDex; }
            set { _modifyDex = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("[applies to PCs Only] can be a positive or negative amount")]
        public int modifyInt
        {
            get { return _modifyInt; }
            set { _modifyInt = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("[applies to PCs Only] can be a positive or negative amount")]
        public int modifyCha
        {
            get { return _modifyCha; }
            set { _modifyCha = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("[applies to PCs Only] can be a positive or negative amount")]
        public int modifyCon
        {
            get { return _modifyCon; }
            set { _modifyCon = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("[applies to PCs Only] can be a positive or negative amount")]
        public int modifyWis
        {
            get { return _modifyWis; }
            set { _modifyWis = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("[applies to PCs Only] can be a positive or negative amount")]
        public int modifyLuk
        {
            get { return _modifyLuk; }
            set { _modifyLuk = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyMoveDistance
        {
            get { return _modifyMoveDistance; }
            set { _modifyMoveDistance = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyHpMax
        {
            get { return _modifyHpMax; }
            set { _modifyHpMax = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifySpMax
        {
            get { return _modifySpMax; }
            set { _modifySpMax = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifySp
        {
            get { return _modifySp; }
            set { _modifySp = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyDamageTypeResistanceAcid
        {
            get { return _modifyDamageTypeResistanceAcid; }
            set { _modifyDamageTypeResistanceAcid = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyDamageTypeResistanceCold
        {
            get { return _modifyDamageTypeResistanceCold; }
            set { _modifyDamageTypeResistanceCold = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyDamageTypeResistanceNormal
        {
            get { return _modifyDamageTypeResistanceNormal; }
            set { _modifyDamageTypeResistanceNormal = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyDamageTypeResistanceElectricity
        {
            get { return _modifyDamageTypeResistanceElectricity; }
            set { _modifyDamageTypeResistanceElectricity = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyDamageTypeResistanceFire
        {
            get { return _modifyDamageTypeResistanceFire; }
            set { _modifyDamageTypeResistanceFire = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyDamageTypeResistanceMagic
        {
            get { return _modifyDamageTypeResistanceMagic; }
            set { _modifyDamageTypeResistanceMagic = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyDamageTypeResistancePoison
        {
            get { return _modifyDamageTypeResistancePoison; }
            set { _modifyDamageTypeResistancePoison = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyNumberOfMeleeAttacks
        {
            get { return _modifyNumberOfMeleeAttacks; }
            set { _modifyNumberOfMeleeAttacks = value; }
        }
        [CategoryAttribute("03 - Buff/DeBuff"), DescriptionAttribute("can be a positive or negative amount")]
        public int modifyNumberOfRangedAttacks
        {
            get { return _modifyNumberOfRangedAttacks; }
            set { _modifyNumberOfRangedAttacks = value; }
        }
        #endregion

        public Effect()
        {            
        }
        public override string ToString()
        {
            return name;
        }
        public Effect ShallowCopy()
        {
            return (Effect)this.MemberwiseClone();
        }
        public Effect DeepCopy()
        {
            Effect other = (Effect)this.MemberwiseClone();
            return other;
        }
    }
}
