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

namespace IB2Toolset
{
    
    public class Prop
    {
        #region Fields
        [JsonIgnore]
        public Bitmap propBitmap;

        private bool _isPureBreakableProp = false;
        private string _requiredItemInInventory = "none"; //like eg pick axes of varying qualities
        private string _breakableTraitTag = "none";
        private int _breakableDC = 10;
        private int _numberOfStages = 0;
        public int counterStages = 0;
        private string _debrisGraphic = "none";
        private string _stageGraphic1 = "none";
        private string _stageGraphic2 = "none";
        private string _stageGraphic3 = "none";
        private string _resRefOfItemGained = "none";
        private string _nameOfSoundFileBump = "none";
        private string _nameOfSoundFileBreak = "none";

        private bool _isLever = false;
        private bool _isOn = false;
        private string _nameOfBitmapON = "none";
        private string _nameOfBitmapOFF = "none";
        private string _keyOFGlobalIntToChange = "none";
        private int _valueOfGlobalIntOFF = 0;
        private int _valueOfGlobalIntON = 0;

        private string _keyOfFirstGlobalIntThatControllsDoor = "none";
        private int _valueOfFirstGlobalIntThatOpensDoor = 1;
        private string _keyOfFifthGlobalIntThatControllsDoor = "none";
        private int _valueOfFifthGlobalIntThatOpensDoor = 1;
        private string _keyOfSecondGlobalIntThatControllsDoor = "none";
        private int _valueOfSecondGlobalIntThatOpensDoor = 1;
        private string _keyOfThirdGlobalIntThatControllsDoor = "none";
        private int _valueOfThirdGlobalIntThatOpensDoor = 1;
        private string _keyOfFourthGlobalIntThatControllsDoor = "none";
        private int _valueOfFourthGlobalIntThatOpensDoor = 1;

        private string _keyOfFirstGlobalIntThatControllsChest = "none";
        private int _valueOfFirstGlobalIntThatOpensChest = 1;
        private string _keyOfFifthGlobalIntThatControllsChest = "none";
        private int _valueOfFifthGlobalIntThatOpensChest = 1;
        private string _keyOfSecondGlobalIntThatControllsChest = "none";
        private int _valueOfSecondGlobalIntThatOpensChest = 1;
        private string _keyOfThirdGlobalIntThatControllsChest = "none";
        private int _valueOfThirdGlobalIntThatOpensChest = 1;
        private string _keyOfFourthGlobalIntThatControllsChest = "none";
        private int _valueOfFourthGlobalIntThatOpensChest = 1;

        //04e - STEP: Climb
        private bool _isClimbable = false;
        private string _climbDirection = "north"; //north, east, south, west 
        private int _climbDC = 0;
        private string _climbTrait = "athlete";

        //1.pushable grid properties (04f - STEP: Pushable grid)
        //private bool _isGridForPushableObject = false;
        private int _turnsBeforeGridResets = 0;
        public int timerTurnsBeforeGridResets = 0;
        private bool _completionStateCanBeLostAgain = false;
        private bool _pushableGridCanBeResetViaHotkey = true;
        private bool _pushableGridCanBeResetEvenAfterCompletion = false;
        private int _partyDefaultPushableGridPositionX = 0;
        private int _partyDefaultPushableGridPositionY = 0;
        private string _partyDefaultDrawDirection = "down";
        private bool _allPushableGridTargetPositionsAreShared = true;

        private string _keyOfGlobalIntToChangeUponPushableGridCompletion = "none";
        private int _valueOfGlobalIntToChangeUponPushableGridCompletion = 0;
        private bool _lockGridOnCompletion = false;
        private bool _removeGridOnCompletion = false;
        private string _messageOnCompletion = "none";

        private string _keyOfGlobalIntToChangeUponPushableGridFailure = "none";
        private int _valueOfGlobalIntToChangeUponPushableGridFailure = 0;
        private bool _lockGridOnFailure = false;
        private bool _removeGridOnFailure = false;
        private string _messageOnFailure = "none";
        private bool _pushableGridIsResetOnEachFailure = false;

        //to do: all the property fields

        //2.pushable object properties (04f - STEP: Pushable object)
        private bool _isPushable = false;
        private string _pushableGridTriggerTag = "none";
        private int _pushableStartPositionX = 0;
        private int _pushableStartPositionY = 0;
        private int _pushableTargetPositionX = 0;
        private int _pushableTargetPositionY = 0;
        private string _pushableTraitTag = "strongman";
        private int _pushableDC = 10;

        public int _challengeLevelAssignedForEncounterInConvo = 0;
        public bool _alwaysFlagAsEncounter = false;
        //private bool _unavoidableConversation = false;
        private string _ingameShownEncName = "none";
        private bool _isContainer = false;
        private string _containerTag = "none";

        private bool _isDoor = false;
        private string _differentSpriteWhenOpen = "none";

        //private bool _canBeDisarmed = false;

        private bool _isHiddenInfo = false;
        private string _floatyAndLogText = "none";
        private string _conversationName = "none";
        private string _boxText = "none";
        private int _infoDC = 15;
        private string _infoTraitTag = "mechanics";
        private bool _showOnlyOnce = false;
        private string _globalStringKey = "none";
        private string _globalStringValue = "none";

        private bool _isSecretDoor = false;
        private int _secretDoorDC = 10;
        
        //new ones
        private string _secretDoorTraitTag = "mechanics";

        private bool _isTrapMain = false;
        private int _trapDC = 10;
        private string _trapTraitTag = "mechanics";

        private string _scriptFilename = "none";
        private string _parm1 = "none";
        private string _parm2 = "none";
        private string _parm3 = "none";
        private string _parm4 = "none";
        private bool _onlyOnce = false;
        private string _scriptActivationFloaty = "none";
        private string _scriptActivationLogEntry = "none";
        
        private bool _alwaysDrawNormalSize = false;

        private string _permanentText = "none";

        //to do!
        private int _movementSpeed = -1; //-1 flags a mvoer not affect by relative party speed
        private int _spotEnemy = -1;
        private int _stealth = -1;
        private bool _stealthSkipsPropTriggers = false;

        //respawn:
        private int _respawnTimeInHours = -1; //-1 meaning false, respawn time is in hours
        private int _maxNumberOfRespawns = -1;//-1 meaning no limit to the number of respawns
        private int _respawnTimeInMinutesPassedAlready = 0; //internal property, not for toolset XXX
        private int _numberOfRespawnsThatHappenedAlready = 0;//internal property, not for toolset XXX
        private string _nameAsMaster = "none";// blank meaning this prop is master of none
        private string _thisPropsMaster = "none"; //blank means this prop has no master; refers to nameAsMaster of another prop
        private bool _instantDeathOnMasterDeath = false; //if true,the propsis immediately placed on List<Prop> propsWaitingForRespawn of this module on its master's death
        private string _keyOfGlobalVarToSetTo1OnDeathOrInactivity = "none";//set to zero again on respawn
        public string spawnArea = ""; //Set automatically here in toolset
        public int spawnLocationX = 0; //Set automatically here in toolset
        public int spawnLocationY = 0; //Set automatically here in toolset
        public int spawnLocationZ = 0; //Set automatically here in toolset

        //faction:
        private string _factionTag = "none";
        private int _requiredFactionStrength = 0;
        private int _maxFactionStrength = -1;
        private int _worthForOwnFaction = 0;
        private string _otherFactionAffectedOnDeath1 = "none";
        private int _effectOnOtherFactionOnDeath1 = 0;
        private string _otherFactionAffectedOnDeath2 = "none";
        private int _effectOnOtherFactionOnDeath2 = 0;
        private string _otherFactionAffectedOnDeath3 = "none";
        private int _effectOnOtherFactionOnDeath3 = 0;
        private string _otherFactionAffectedOnDeath4 = "none";
        private int _effectOnOtherFactionOnDeath4 = 0;
        private bool _pendingFactionStrengthEffectReversal = false; //XXX

        //gcCheck
        private string _firstGcScriptName = "none";
        private string _firstGcParm1 = "none";
        private string _firstGcParm2 = "none";
        private string _firstGcParm3 = "none";
        private string _firstGcParm4 = "none";
        private bool _firstCheckForConditionFail = false;

        private string _secondGcScriptName = "none";
        private string _secondGcParm1 = "none";
        private string _secondGcParm2 = "none";
        private string _secondGcParm3 = "none";
        private string _secondGcParm4 = "none";
        private bool _secondCheckForConditionFail = false;

        private string _thirdGcScriptName = "none";
        private string _thirdGcParm1 = "none";
        private string _thirdGcParm2 = "none";
        private string _thirdGcParm3 = "none";
        private string _thirdGcParm4 = "none";
        private bool _thirdCheckForConditionFail = false;

        private bool _allConditionsMustBeTrue = true;

        //XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
        //do three new sections for properties: faction(7), activity conditions(8), respawn(9)  
        private bool _isTrap = false;  
        private int _trapDCforDisableCheck = 10;

        private int _LocationX = 0;
        private int _LocationY = 0;
        private int _LocationZ = 0;
        private string _ImageFileName = "blank";
        private bool _PropFacingLeft = true;
        private bool _HasCollision = false;
        private string _PropName = "newPropName";
        private string _propTag = "newProp";
        private bool _isShown = true;
        private bool _isActive = true;
        private string _PropCategoryName = "newCategory";
        private string _mouseOverText = "none";
        private string _ConversationWhenOnPartySquare = "none";
        private string _EncounterWhenOnPartySquare = "none";
        private bool _DeletePropWhenThisEncounterIsWon = false;
        private List<LocalInt> _PropLocalInts = new List<LocalInt>();
        private List<LocalString> _PropLocalStrings = new List<LocalString>();
        private int _PostLocationX = 0;
	    private int _PostLocationY = 0;
        private List<WayPoint> _WayPointList = new List<WayPoint>();
        private List<Schedule> _Schedules = new List<Schedule>();
        private bool _isMover = false;
        private int _ChanceToMove2Squares = 0;
	    private int _ChanceToMove0Squares = 0;
        private string _MoverType = "post"; //post, random, patrol, daily, weekly, monthly, yearly
        private bool _isChaser = false;
	    private int _ChaserDetectRangeRadius = 2;
	    private int _ChaserGiveUpChasingRangeRadius = 3;
	    private int _ChaserChaseDuration = 24;
	    private int _RandomMoverRadius = 5;
        //private string onHeartBeatLogicTree = "none";
        //private string onHeartBeatParms = "";
        private string onHeartBeatIBScript = "none";
        private string onHeartBeatIBScriptParms = "";
        private bool _unavoidableConversation = false;
        private bool _isLight = false;
        private bool _hasHalo = false;
        private float _focalIntensity = 1.0f;
        private float _ringIntensity = 1.0f;

        private int _maxNumberOfFrames = 1;
        private float _updateTicksNeededTillNextFrame = 20;
        private float _chanceToTriggerAnimationCycle = 100;
        private int _propFrameHeight = 100;
        private int _sizeFactor = 100;
        private bool _doOnce = false;
        private bool _animationIsActive = true;
        private bool _hiddenWhenComplete = false;
        private bool _hiddenWhenNotActive = false;
        private int _numberOfCyclesNeededForCompletion = 1;
        private int _framesNeededForFullFadeInOut = 0;

        private string _OnEnterSquareIBScript = "none";  
        private string _OnEnterSquareIBScriptParms = "";

        private string _OnEnterSquareScript = "none";  
        private string _OnEnterSquareScriptParm1 = "none";  
        private string _OnEnterSquareScriptParm2 = "none";  
        private string _OnEnterSquareScriptParm3 = "none";  
        private string _OnEnterSquareScriptParm4 = "none";  
        private int _numberOfScriptCallsRemaining = 999;  
        private bool _canBeTriggeredByPc = true;  
        private bool _canBeTriggeredByCreature = true;
        public bool _encounterPropTriggerOnEveryStep = true;

        private bool _randomAnimationDirectionEachCall = false;

        #endregion

        #region Properties

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("Enter anything else than one here to assign this prop to a faction.")]
        public string factionTag
        {
            get { return _factionTag; }
            set { _factionTag = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("When the prop's faction's strength (not rank) falls below this value, the prop is set to inactive and hidden.")]
        public int requiredFactionStrength
        {
            get { return _requiredFactionStrength; }
            set { _requiredFactionStrength = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("When the prop'S faction'S strength (not rank) rises above this value, the prop is set to inactive and hidden.")]
        public int maxFactionStrength
        {
            get { return _maxFactionStrength; }
            set { _maxFactionStrength = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("When this prop is removed (slain), because the party won the encounter on its encounter slot, this prop'S faction's strength is reduced by this value. The prop's faction has been weakend.")]
        public int worthForOwnFaction
        {
            get { return _worthForOwnFaction; }
            set { _worthForOwnFaction = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("Enter the tag of anonther faction whose strength shall be changed on this prop's death (encounter on its encounter slot has been won by the party).")]
        public string otherFactionAffectedOnDeath1
        {
            get { return _otherFactionAffectedOnDeath1; }
            set { _otherFactionAffectedOnDeath1 = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("The amount of change to apply to the other faction (can be positive or negative).")]
        public int effectOnOtherFactionOnDeath1
        {
            get { return _effectOnOtherFactionOnDeath1; }
            set { _effectOnOtherFactionOnDeath1 = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("Enter the tag of anonther faction whose strength shall be changed on this prop's death (encounter on its encounter slot has been won by the party).")]
        public string otherFactionAffectedOnDeath2
        {
            get { return _otherFactionAffectedOnDeath2; }
            set { _otherFactionAffectedOnDeath2 = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("The amount of change to apply to the other faction (can be positive or negative).")]
        public int effectOnOtherFactionOnDeath2
        {
            get { return _effectOnOtherFactionOnDeath2; }
            set { _effectOnOtherFactionOnDeath2 = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("Enter the tag of anonther faction whose strength shall be changed on this prop's death (encounter on its encounter slot has been won by the party).")]
        public string otherFactionAffectedOnDeath3
        
        {
            get { return _otherFactionAffectedOnDeath3; }
            set { _otherFactionAffectedOnDeath3 = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("The amount of change to apply to the other faction (can be positive or negative).")]
        public int effectOnOtherFactionOnDeath3
        {
            get { return _effectOnOtherFactionOnDeath3; }
            set { _effectOnOtherFactionOnDeath3 = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("Enter the tag of anonther faction whose strength shall be changed on this prop's death (encounter on its encounter slot has been won by the party).")]
        public string otherFactionAffectedOnDeath4
        {
            get { return _otherFactionAffectedOnDeath4; }
            set { _otherFactionAffectedOnDeath4 = value; }
        }

        [CategoryAttribute("10 - Faction"), DescriptionAttribute("The amount of change to apply to the other faction (can be positive or negative).")]
        public int effectOnOtherFactionOnDeath4
        {
            get { return _effectOnOtherFactionOnDeath4; }
            set { _effectOnOtherFactionOnDeath4 = value; }
        }
       
        [Browsable(true), TypeConverter(typeof(ScriptConverter))]
        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Only gc type scripts are viable here. Depending on the setting for allConditionsMustBeTrue, all hooked gc scripts here must retrun true OR only one of them. When the (combined) gc check(s) fails, the prop is set to inactive and hidden. Will only affect props within their min max faction strength window (if such is set up for the prop).")]
        public string firstGcScriptName
        {
            get { return _firstGcScriptName; }
            set { _firstGcScriptName = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 1 to be used for this Script hook (leave as 'none' if not used)")]
        public string firstGcParm1
        {
            get { return _firstGcParm1; }
            set { _firstGcParm1 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 2 to be used for this Script hook (leave as 'none' if not used)")]
        public string firstGcParm2
        {
            get { return _firstGcParm2; }
            set { _firstGcParm2 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 3 to be used for this Script hook (leave as 'none' if not used)")]
        public string firstGcParm3
        {
            get { return _firstGcParm3; }
            set { _firstGcParm3 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 4 to be used for this Script hook (leave as 'none' if not used)")]
        public string firstGcParm4
        {
            get { return _firstGcParm4; }
            set { _firstGcParm4 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("When set to true, the gc script checks for a false outsomce of the check. Handy for NOT logic.")]
        public bool firstCheckForConditionFail
        {
            get { return _firstCheckForConditionFail; }
            set { _firstCheckForConditionFail = value; }
        }

        [Browsable(true), TypeConverter(typeof(ScriptConverter))]
        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Only gc type scripts are viable here. Depending on the setting for allConditionsMustBeTrue, all hooked gc scripts here must retrun true OR only one of them. When the (combined) gc check(s) fails, the prop is set to inactive and hidden. Will only affect props within their min max faction strength window (if such is set up for the prop).")]
        public string secondGcScriptName
        {
            get { return _secondGcScriptName; }
            set { _secondGcScriptName = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 1 to be used for this Script hook (leave as 'none' if not used)")]
        public string secondGcParm1
        {
            get { return _secondGcParm1; }
            set { _secondGcParm1 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 2 to be used for this Script hook (leave as 'none' if not used)")]
        public string secondGcParm2
        {
            get { return _secondGcParm2; }
            set { _secondGcParm2 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 3 to be used for this Script hook (leave as 'none' if not used)")]
        public string secondGcParm3
        {
            get { return _secondGcParm3; }
            set { _secondGcParm3 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 4 to be used for this Script hook (leave as 'none' if not used)")]
        public string secondGcParm4
        {
            get { return _secondGcParm4; }
            set { _secondGcParm4 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("When set to true, the gc script checks for a false outsomce of the check. Handy for NOT logic.")]
        public bool secondCheckForConditionFail
        {
            get { return _secondCheckForConditionFail; }
            set { _secondCheckForConditionFail = value; }
        }

        [Browsable(true), TypeConverter(typeof(ScriptConverter))]
        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Only gc type scripts are viable here. Depending on the setting for allConditionsMustBeTrue, all hooked gc scripts here must retrun true OR only one of them. When the (combined) gc check(s) fails, the prop is set to inactive and hidden. Will only affect props within their min max faction strength window (if such is set up for the prop).")]
        public string thirdGcScriptName
        {
            get { return _thirdGcScriptName; }
            set { _thirdGcScriptName = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 1 to be used for this Script hook (leave as 'none' if not used)")]
        public string thirdGcParm1
        {
            get { return _thirdGcParm1; }
            set { _thirdGcParm1 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 2 to be used for this Script hook (leave as 'none' if not used)")]
        public string thirdGcParm2
        {
            get { return _thirdGcParm2; }
            set { _thirdGcParm2 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 3 to be used for this Script hook (leave as 'none' if not used)")]
        public string thirdGcParm3
        {
            get { return _thirdGcParm3; }
            set { _thirdGcParm3 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("Parameter 4 to be used for this Script hook (leave as 'none' if not used)")]
        public string thirdGcParm4
        {
            get { return _thirdGcParm4; }
            set { _thirdGcParm4 = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("When set to true, the gc script checks for a false outsomce of the check. Handy for NOT logic.")]
        public bool thirdCheckForConditionFail
        {
            get { return _thirdCheckForConditionFail; }
            set { _thirdCheckForConditionFail = value; }
        }

        [CategoryAttribute("11 - Existence Conditions"), DescriptionAttribute("When set to false, it is enough when one of the conditions is fullfilled (OR logic). Default (true) is AND logic.")]
        public bool allConditionsMustBeTrue
        {
            get { return _allConditionsMustBeTrue; }
            set { _allConditionsMustBeTrue = value; }
        }

        [CategoryAttribute("12 - Respawn System"), DescriptionAttribute("Number of hours before the checking for respawn starts. Set to -1 for a prop that is not meant to respawn (default).")]
        public int respawnTimeInHours
        {
            get { return _respawnTimeInHours; }
            set { _respawnTimeInHours = value; }
        }
        [CategoryAttribute("12 - Respawn System"), DescriptionAttribute("Max. number of respawns before respawning is not possible anymore. Set to -1 for unlimited respawns.")]
        public int maxNumberOfRespawns
        {
            get { return _maxNumberOfRespawns; }
            set { _maxNumberOfRespawns = value; }
        }
        [CategoryAttribute("12 - Respawn System"), DescriptionAttribute("This prop is (also) a master -enter teh anem with which its followers identify it here. Note: Several prosp can have teh same nameAsMAster, which is important when you upgrade (exchange) a master with a superior version (eg within the faction min max window system).")]
        public string nameAsMaster
        {
            get { return _nameAsMaster; }
            set { _nameAsMaster = value; }
        }
        [CategoryAttribute("12 - Respawn System"), DescriptionAttribute("The name (enter nameAsMaster here) of this prop's master. Note: also masters can have masters, creating hierarchies.")]
        public string thisPropsMaster
        {
            get { return _thisPropsMaster; }
            set { _thisPropsMaster = value; }
        }
        [CategoryAttribute("12 - Respawn System"), DescriptionAttribute("If true, this prop is slain once its master dies. If false, a master's death will only prevent a respawn, but does not kill immediately.")]
        public bool instantDeathOnMasterDeath
        {
            get { return _instantDeathOnMasterDeath; }
            set { _instantDeathOnMasterDeath = value; }
        }
        [CategoryAttribute("12 - Respawn System"), DescriptionAttribute("Key of the global variable to set to 1 once this prop is lsain or set inactive. If such gloabl does not exist, it is created.")]
        public string keyOfGlobalVarToSetTo1OnDeathOrInactivity
        {
            get { return _keyOfGlobalVarToSetTo1OnDeathOrInactivity; }
            set { _keyOfGlobalVarToSetTo1OnDeathOrInactivity = value; }
        }

        [Browsable(true), TypeConverter(typeof(IBScriptConverter))]  
        [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("IBScript name to be run for this Prop when a Player or Creature stands on this Prop")]  
        public string OnEnterSquareIBScript  
        {  
            get { return _OnEnterSquareIBScript; }  
            set { _OnEnterSquareIBScript = value; }  
        }

        [Browsable(true), TypeConverter(typeof(ScriptConverter))]  
         [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("Script name to be run for this Prop when a Player or Creature stands on this Prop")]  
         public string OnEnterSquareScript  
         {  
             get { return _OnEnterSquareScript; }  
             set { _OnEnterSquareScript = value; }  
         }  
         [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("Parameter 1 to be used for this Script hook (leave as 'none' if not used)")]  
         public string OnEnterSquareScriptParm1
         {  
             get { return _OnEnterSquareScriptParm1; }  
             set { _OnEnterSquareScriptParm1 = value; }  
         }  
         [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("Parameter 2 to be used for this Script hook (leave as 'none' if not used)")]  
         public string OnEnterSquareScriptParm2
         {  
             get { return _OnEnterSquareScriptParm2; }  
             set { _OnEnterSquareScriptParm2 = value; }  
         }  
         [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("Parameter 3 to be used for this Script hook (leave as 'none' if not used)")]  
         public string OnEnterSquareScriptParm3
         {  
             get { return _OnEnterSquareScriptParm3; }  
             set { _OnEnterSquareScriptParm3 = value; }  
         }

        [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("if true, the prop is used as a trap and can be disabled by a thief (pass/fail check made) or other selected classes.")]  
        public bool isTrap  
        {  
             get { return _isTrap; }  
             set { _isTrap = value; }  
        }
          
        [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("the Difficulty Check value used when a Player tries to disable this trap.")]  
        public int trapDCforDisableCheck
        {  
             get { return _trapDCforDisableCheck; }  
             set { _trapDCforDisableCheck = value; }  
        }  
          
         [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("Parameter 4 to be used for this Script hook (leave as 'none' if not used)")]  
         public string OnEnterSquareScriptParm4
         {  
             get { return _OnEnterSquareScriptParm4; }  
             set { _OnEnterSquareScriptParm4 = value; }  
         }  
         [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("if true, the prop OnEnterSquareIBScript can be triggered by Players; if false, Players will not trigger the ibscript.")]  
         public bool canBeTriggeredByPc
         {  
             get { return _canBeTriggeredByPc; }  
             set { _canBeTriggeredByPc = value; }  
         }  
         [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("if true, the prop OnEnterSquareIBScript can be triggered by Creatures; if false, Creatures will not trigger the ibscript.")]  
         public bool canBeTriggeredByCreature
         {  
             get { return _canBeTriggeredByCreature; }  
             set { _canBeTriggeredByCreature = value; }  
         }

        [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("if true, this prop can be triggered multiple times during the turn of a creature or pc, ie on every step; if false, a creature/pc can trigger it (and all of its squares) only once in a turn.")]
        public bool encounterPropTriggerOnEveryStep
        {
            get { return _encounterPropTriggerOnEveryStep; }
            set { _encounterPropTriggerOnEveryStep = value; }
        }
        [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("The number of times that the OnEnterSquare script or IBScript can be triggered. Each time the script is triggered, this number will be decremented by one. Once this number reaches zero, the Prop will be removed from the encounter map.")]  
         public int numberOfScriptCallsRemaining
         {  
             get { return _numberOfScriptCallsRemaining; }  
             set { _numberOfScriptCallsRemaining = value; }  
         }  
      
        [CategoryAttribute("05 - Triggers (combat)"), DescriptionAttribute("Parameters to be used for this IBScript hook (as many parameters as needed, comma deliminated with no spaces)")]  
        public string OnEnterSquareIBScriptParms
        {  
            get { return _OnEnterSquareIBScriptParms; }  
            set { _OnEnterSquareIBScriptParms = value; }  
        }  

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("This determines how swiftly your animated prop fades in and out after all frames have been done (numberOfCyclesNeededForCompletion * maxNumberOfFrames). Whens et to 0 (Deafult), tehre'sno fade in or out. The higher, the more slowly the fading happens. Please do not enter a number higher than half of (numberOfCyclesNeededForCompletion * maxNumberOfFrames). E.g. 15 framesNeededForFullFadeInOut feels quite nicely if you need  avalue for orientation.")]
        public int framesNeededForFullFadeInOut
        {
            get { return _framesNeededForFullFadeInOut; }
            set { _framesNeededForFullFadeInOut = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("This determines how many times ou want the animation to repeat before it is considered complete. DoOnce will only register once all cycles have been done. ChanceToTriggerAnimationCycle will trigger the number of cycles specified here.")]
        public int numberOfCyclesNeededForCompletion
        {
            get { return _numberOfCyclesNeededForCompletion; }
            set { _numberOfCyclesNeededForCompletion = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("Set the number of frames of this prop. 1 means a non-animated prop.")]
        public int maxNumberOfFrames
        {
            get { return _maxNumberOfFrames; }
            set { _maxNumberOfFrames = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("The speed of the animation (should be independent from cpu or gpu speed). Higher is slower. Trying with default of 20.")]
        public float updateTicksNeededTillNextFrame
        {
            get { return _updateTicksNeededTillNextFrame; }
            set { _updateTicksNeededTillNextFrame = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("The chance of triggering an animation cycle per second in percent. Defaults to 100. Use this for props that make randomized pauses before starting another animation cycle.")]
        public float chanceToTriggerAnimationCycle
        {
            get { return _chanceToTriggerAnimationCycle; }
            set { _chanceToTriggerAnimationCycle = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("The height in pixels of one frame of this prop. Defaults to 100.")]
        public int propFrameHeight
        {
            get { return _propFrameHeight; }
            set { _propFrameHeight = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("This scales the animated props x and y dimension by the entered number of precent points. Defaults to 100. Note that you always place the top left corner of a prop.")]
        public int sizeFactor
        {
            get { return _sizeFactor; }
            set { _sizeFactor = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("Thé prop animation is played only once and then animationIsActive is set to fale; setting animationIsActive to true via script will allow for  another animation cycle.")]
        public bool doOnce
        {
            get { return _doOnce; }
            set { _doOnce = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("When set to true this will make an animation play reverse with a 50% chance on each call (after all cycles have been played).")]
        public bool randomAnimationDirectionEachCall
        {
            get { return _randomAnimationDirectionEachCall; }
            set { _randomAnimationDirectionEachCall = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("Setting this to false turns teh animation off. Can be de/activated via script.")]
        public bool animationIsActive
        {
            get { return _animationIsActive; }
            set { _animationIsActive = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("The prop is invisible when animation is not currently running, ideal for making occassionaly happening ambience animations as e.g. ground fog (when combined with chanceToTriggerAnimationCycle)")]
        public bool hiddenWhenComplete
        {
            get { return _hiddenWhenComplete; }
            set { _hiddenWhenComplete = value; }
        }

        [CategoryAttribute("09 - Prop Animation"), DescriptionAttribute("The prop is invisible when animation is not currently running, ideal for making occassionaly happening ambience animations as e.g. ground fog (when combined with chanceToTriggerAnimationCycle)")]
        public bool hiddenWhenNotActive
        {
            get { return _hiddenWhenNotActive; }
            set { _hiddenWhenNotActive = value; }
        }

        [CategoryAttribute("03a - SEARCH: Secret Door"), DescriptionAttribute("If true, the prop acts as secret door. Place it on a tile with elevated height (wall section, adhere to rules for bridges/tunnels basically). Do not place on the outer squares (border) of a map.")]
        public bool isSecretDoor
        {
            get { return _isSecretDoor; }
            set { _isSecretDoor = value; }
        }

        [CategoryAttribute("03a - SEARCH: Secret Door"), DescriptionAttribute("The DC to match to open this door. Keep in mind that the roll is static 10 + skill (so you will usually add 10 to the DC to offset the roll).")]
        public int secretDoorDC
        {
            get { return _secretDoorDC; }
            set { _secretDoorDC = value; }
        }

        [CategoryAttribute("03a - SEARCH: Secret Door"), DescriptionAttribute("The tag of the trait that is used for opening secret doors of this type in your campaign, e.g. mechanics")]
        public string secretDoorTraitTag
        {
            get { return _secretDoorTraitTag; }
            set { _secretDoorTraitTag = value; }
        }

        [CategoryAttribute("03b - SEARCH: Hidden Info"), DescriptionAttribute("If true, the prop conatins info that needs a search action (space key) and skill roll to be unveiled. This can be tied to any trait, uses various optional output ways, can be set to be one-time-only and finally also can set a global string to the author's liking upon successful activation.")]
        public bool isHiddenInfo 
        {
            get { return _isHiddenInfo; }
            set { _isHiddenInfo = value; }
        }

        [CategoryAttribute("03b - SEARCH: Hidden Info"), DescriptionAttribute("The text to be shown in log and floaty upon prop activation. When set to none, no text is shown.")]
        public string floatyAndLogText
        {
            get { return _floatyAndLogText; }
            set { _floatyAndLogText = value; }
        }

        [CategoryAttribute("03b - SEARCH: Hidden Info"), DescriptionAttribute("The name of the convo to be shown upon prop activation. When set to none, no convo is shown.")]
        public string conversationName
        {
            get { return _conversationName; }
            set { _conversationName = value; }
        }

        [CategoryAttribute("03b - SEARCH: Hidden Info"), DescriptionAttribute("The message box text to be shown upon prop activation. When set to none, no message box is shown.")]
        public string boxText
        {
            get { return _boxText; }
            set { _boxText = value; }
        }

        [CategoryAttribute("03b - SEARCH: Hidden Info"), DescriptionAttribute("The DC (using roll ten, ie static 10 for skill roll) that has to be overcome for activation of the hidden prop.")]
        public int infoDC
        {
            get { return _infoDC; }
            set { _infoDC = value; }
        }

        [CategoryAttribute("03b - SEARCH: Hidden Info"), DescriptionAttribute("The tag of the trait to check against for hidden info prop activation.")]
        public string infoTraitTag
        {
            get { return _infoTraitTag; }
            set { _infoTraitTag = value; }
        }

        [CategoryAttribute("03b - SEARCH: Hidden Info"), DescriptionAttribute("If true, the hidden info prop can actviated only once.")]
        public bool showOnlyOnce
        {
            get { return _showOnlyOnce; }
            set { _showOnlyOnce = value; }
        }
        
        [CategoryAttribute("03b - SEARCH: Hidden Info"), DescriptionAttribute("The key of the global string whose value shall be changed once the hidden info prop hás been successfully activated.")]
        public string globalStringKey
        {
            get { return _globalStringKey; }
            set { _globalStringKey = value; }
        }

        [CategoryAttribute("03b - SEARCH: Hidden Info"), DescriptionAttribute("The new value of the global string that shall be changed once the hidden info prop hás been successfully activated.")]
        public string globalStringValue
        {
            get { return _globalStringValue; }
            set { _globalStringValue = value; }
        }

        //rockspider
        //01 - Main 
        //02 - Sprite

        //03a - SEARCH: secret door (space)
        //03b - SEARCH: hidden info (space)
        //03c - SEARCH: disarm trap (space)

        //04a - STEP: script (step)
        //04b - STEP: door (step)
        //04c - STEP: encounter (step)
        //04d - STEP: convo (step)

        //05 - COMBAT triggers

        //06  - IBScript hooks

        //from 4 onwards +3?


       
        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("The key of the first global int that will open this door automatically once its defined value is hit. Leave at none for the door to not rely upon a global int, ia from a push grid mechanism.")]
        public string keyOfFirstGlobalIntThatControllsDoor
        {
            get { return _keyOfFirstGlobalIntThatControllsDoor; }
            set { _keyOfFirstGlobalIntThatControllsDoor = value; }
        }

        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("The value of the first global int that must be matched to open this door.")]
        public int valueOfFirstGlobalIntThatOpensDoor
        {
            get { return _valueOfFirstGlobalIntThatOpensDoor; }
            set { _valueOfFirstGlobalIntThatOpensDoor = value; }
        }

        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("The key of the Second global int that will open this door automatically once its defined value is hit. Leave at none for the door to not rely upon a global int, ia from a push grid mechanism.")]
        public string keyOfSecondGlobalIntThatControllsDoor
        {
            get { return _keyOfSecondGlobalIntThatControllsDoor; }
            set { _keyOfSecondGlobalIntThatControllsDoor = value; }
        }

        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("The value of the Second global int that must be matched to open this door.")]
        public int valueOfSecondGlobalIntThatOpensDoor
        {
            get { return _valueOfSecondGlobalIntThatOpensDoor; }
            set { _valueOfSecondGlobalIntThatOpensDoor = value; }
        }

        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("The key of the Third global int that will open this door automatically once its defined value is hit. Leave at none for the door to not rely upon a global int, ia from a push grid mechanism.")]
        public string keyOfThirdGlobalIntThatControllsDoor
        {
            get { return _keyOfThirdGlobalIntThatControllsDoor; }
            set { _keyOfThirdGlobalIntThatControllsDoor = value; }
        }

        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("The value of the Third global int that must be matched to open this door.")]
        public int valueOfThirdGlobalIntThatOpensDoor
        {
            get { return _valueOfThirdGlobalIntThatOpensDoor; }
            set { _valueOfThirdGlobalIntThatOpensDoor = value; }
        }

        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("The key of the Fourth global int that will open this door automatically once its defined value is hit. Leave at none for the door to not rely upon a global int, ia from a push grid mechanism.")]
        public string keyOfFourthGlobalIntThatControllsDoor
        {
            get { return _keyOfFourthGlobalIntThatControllsDoor; }
            set { _keyOfFourthGlobalIntThatControllsDoor = value; }
        }

        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("The value of the Fourth global int that must be matched to open this door.")]
        public int valueOfFourthGlobalIntThatOpensDoor
        {
            get { return _valueOfFourthGlobalIntThatOpensDoor; }
            set { _valueOfFourthGlobalIntThatOpensDoor = value; }
        }

        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("The key of the Fifth global int that will open this door automatically once its defined value is hit. Leave at none for the door to not rely upon a global int, ia from a push grid mechanism.")]
        public string keyOfFifthGlobalIntThatControllsDoor
        {
            get { return _keyOfFifthGlobalIntThatControllsDoor; }
            set { _keyOfFifthGlobalIntThatControllsDoor = value; }
        }

        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("The value of the Fifth global int that must be matched to open this door.")]
        public int valueOfFifthGlobalIntThatOpensDoor
        {
            get { return _valueOfFifthGlobalIntThatOpensDoor; }
            set { _valueOfFifthGlobalIntThatOpensDoor = value; }
        }



        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("If true, this acts as door. Dont forget to setup the gaOpenObject script in the STEP: script section to define (1) the tag of the key needed, (2) whether key is consumed, (3) tag of trait to check for lock picking and (4) DC of lock picking attempt.")]
        public bool isDoor
        {
            get { return _isDoor; }
            set { _isDoor = value; }
        }

        [CategoryAttribute("04b - STEP: door "), DescriptionAttribute("If set set to anything else than none, this will replace the door sprite once the door is opened. Enter without filename ending, ie like prp_earawen (without the .png extension). Note that you could also use a blank/transparent sprite here is you want the door to vanish.")]
        public string differentSpriteWhenOpen
        {
            get { return _differentSpriteWhenOpen; }
            set { _differentSpriteWhenOpen = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The key of the first global int that will open this container automatically once its defined value is hit. Leave at none for the container to not rely upon a global int, ia from a push grid mechanism.")]
        public string keyOfFirstGlobalIntThatControllsChest
        {
            get { return _keyOfFirstGlobalIntThatControllsChest; }
            set { _keyOfFirstGlobalIntThatControllsChest = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The value of the first global int that must be matched to open this container.")]
        public int valueOfFirstGlobalIntThatOpensChest
        {
            get { return _valueOfFirstGlobalIntThatOpensChest; }
            set { _valueOfFirstGlobalIntThatOpensChest = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The key of the Second global int that will open this container automatically once its defined value is hit. Leave at none for the container to not rely upon a global int, ia from a push grid mechanism.")]
        public string keyOfSecondGlobalIntThatControllsChest
        {
            get { return _keyOfSecondGlobalIntThatControllsChest; }
            set { _keyOfSecondGlobalIntThatControllsChest = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The value of the Second global int that must be matched to open this container.")]
        public int valueOfSecondGlobalIntThatOpensChest
        {
            get { return _valueOfSecondGlobalIntThatOpensChest; }
            set { _valueOfSecondGlobalIntThatOpensChest = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The key of the Third global int that will open this container automatically once its defined value is hit. Leave at none for the container to not rely upon a global int, ia from a push grid mechanism.")]
        public string keyOfThirdGlobalIntThatControllsChest
        {
            get { return _keyOfThirdGlobalIntThatControllsChest; }
            set { _keyOfThirdGlobalIntThatControllsChest = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The value of the Third global int that must be matched to open this container.")]
        public int valueOfThirdGlobalIntThatOpensChest
        {
            get { return _valueOfThirdGlobalIntThatOpensChest; }
            set { _valueOfThirdGlobalIntThatOpensChest = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The key of the Fourth global int that will open this container automatically once its defined value is hit. Leave at none for the container to not rely upon a global int, ia from a push grid mechanism.")]
        public string keyOfFourthGlobalIntThatControllsChest
        {
            get { return _keyOfFourthGlobalIntThatControllsChest; }
            set { _keyOfFourthGlobalIntThatControllsChest = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The value of the Fourth global int that must be matched to open this container.")]
        public int valueOfFourthGlobalIntThatOpensChest
        {
            get { return _valueOfFourthGlobalIntThatOpensChest; }
            set { _valueOfFourthGlobalIntThatOpensChest = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The key of the Fifth global int that will open this container automatically once its defined value is hit. Leave at none for the container to not rely upon a global int, ia from a push grid mechanism.")]
        public string keyOfFifthGlobalIntThatControllsChest
        {
            get { return _keyOfFifthGlobalIntThatControllsChest; }
            set { _keyOfFifthGlobalIntThatControllsChest = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The value of the Fifth global int that must be matched to open this container.")]
        public int valueOfFifthGlobalIntThatOpensChest
        {
            get { return _valueOfFifthGlobalIntThatOpensChest; }
            set { _valueOfFifthGlobalIntThatOpensChest = value; }
        }


        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("If true, this acts as a container. Dont forget to setup the gaOpenObject script in the STEP: script section to define (1) the tag of the key needed, (2) whether key is consumed, (3) tag of trait to check for lock picking and (4) DC of lock picking attempt.")]
        public bool isContainer
        {
            get { return _isContainer; }
            set { _isContainer = value; }
        }

        [CategoryAttribute("04b - STEP: container "), DescriptionAttribute("The tag of the container you would like to open.")]
        public string containerTag
        {
            get { return _containerTag; }
            set { _containerTag = value; }
        }

        [CategoryAttribute("03c - SEARCH: disarm trap"), DescriptionAttribute("If true, the prop acts as a trap. It fires its script when stepping on it (gaCastSpell.cs is typicllay a good choice here) and can disarmed via search (space key) action using trait and dc defined here. Note the one time/every time setting for trap scripts that shall fire only once/on every contact.")]
        public bool isTrapMain 
        {
            get { return _isTrapMain; }
            set { _isTrapMain = value; }
        }

        [CategoryAttribute("03c - SEARCH: disarm trap"), DescriptionAttribute("The DC to match to disarm this trap. Keep in mind that the roll is static 10 + skill (so you will usually add 10 to the DC to offset the roll).")]
        public int trapDC
        {
            get { return _trapDC; }
            set { _trapDC = value; }
        }

        [CategoryAttribute("03c - SEARCH: disarm trap"), DescriptionAttribute("The tag of the trait that is used for disarming traps of this type in your campaign, e.g. mechanics")]
        public string trapTraitTag
        {
            get { return _trapTraitTag; }
            set { _trapTraitTag = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Current X location on map.")]
        public int LocationX
        {
            get { return _LocationX; }
            set { _LocationX = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Current Y location on map.")]
        public int LocationY
        {
            get { return _LocationY; }
            set { _LocationY = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Current Z location on map.")]
        public int LocationZ
        {
            get { return _LocationZ; }
            set { _LocationZ = value; }
        }
        [EditorAttribute(typeof(FileNameSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("02 - Sprite"), DescriptionAttribute("Filename for the prop's token (no extension). Use the IconSprite tab to change this image.")]
        public string ImageFileName
        {
            get { return _ImageFileName; }
            set { _ImageFileName = value; }
        }
       
        [CategoryAttribute("02 - Sprite"), DescriptionAttribute("When true, this sprite is drawn full size even when the area is set up to use tiny or mini props (zoomed out).")]
        public bool alwaysDrawNormalSize
        {
            get { return _alwaysDrawNormalSize; }
            set { _alwaysDrawNormalSize = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("if true, the prop has collision; if false, is walkable.")]
        public bool HasCollision
        {
            get { return _HasCollision; }
            set { _HasCollision = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("if true, the prop will start off facing left; if false, will start facing right.")]
        public bool PropFacingLeft
        {
            get { return _PropFacingLeft; }
            set { _PropFacingLeft = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("if true, the prop is visible. if false, the prop is hidden.")]
        public bool isShown
        {
            get { return _isShown; }
            set { _isShown = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("if true, the prop is active for moving and interacting with. if false, the prop is inactive.")]
        public bool isActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("this is the name of the prop.")]
        public string PropName
        {
            get { return _PropName; }
            set { _PropName = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("this must be unique.")]
        public string PropTag
        {
            get { return _propTag; }
            set { _propTag = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Category that this Prop belongs to.")]
        public string PropCategoryName
        {
            get
            {
                return _PropCategoryName;
            }
            set
            {
                _PropCategoryName = value;
            }
        }
        //public string permanentText = "none";
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The text to display permanently on top of the prop. Does not work for neighbouring maps. Prop must be shown and text must be anything else than none. Good for overview maps.")]
        public string permanentText
        {
            get { return _permanentText; }
            set { _permanentText = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("The text to display when player mouses over the Prop on the adventure maps.")]
        public string MouseOverText
        {
            get { return _mouseOverText; }
            set { _mouseOverText = value; }
        }

        /*
         //1.pushable grid properties (04f - STEP: Pushable grid)
        private bool _pushableGridCanBeResetViaHotkey = true;
        private bool _pushableGridCanBeResetEvenAfterCompletion = false;
        private int _partyDefaultPushableGridPositionX = 0;
        private int _partyDefaultPushableGridPositionY = 0;
        private bool _allPushableGridTargetPositionsAreShared = true;

        private string _keyOfGlobalIntToChangeUponPushableGridCompletion = "none";
        private int _valueOfGlobalIntToChangeUponPushableGridCompletion = 0;
        private bool _lockGridOnCompletion = false;
        private bool _removeGridOnCompletion = false;
        private string _messageOnCompletion = "none";

        private string _keyOfGlobalIntToChangeUponPushableGridFailure = "none";
        private int _valueOfGlobalIntToChangeUponPushableGridFailure = 0;
        private bool _lockGridOnFailure = false;
       
        private bool _pushableGridIsResetOnEachFailure = false;

        //to do: all the property fields

        //2.pushable object properties (04f - STEP: Pushable object)
        private bool _isPushable = false;
        private string _pushableGridTriggerTag = "none";
        private int _pushableStartPositionX = 0;
        private int _pushableStartPositionY = 0;
        private int _pushableTargetPositionX = 0;
        private int _pushableTargetPositionY = 0;
        private string _pushableTraitTag = "strongman";
        private int _pushableDC = 10;
        */

        /*
    private bool _isLever = false;
    private bool _isOn = false;
    private string _nameOfBitmapON = "none";
    private string _nameOfBitmapOFF = "none";
    private string _keyOFGlobalIntToChange = "none";
    private int _valueOfGlobalIntOFF = 0;
    private int _valueOfGlobalIntON = 0;
    */

        /*
            private bool _isPureBreakableProp = false;
            private string _requiredItemInInventory = "none"; //like eg pick axes of varying qualities
            private string _breakableTraitTag = "none";
            private int _breakableDC = 10;
            private int _numberOfStages = 0;
            private string _debrisGraphic = "none";
            private string _stageGraphic1 = "none";
            private string _stageGraphic2 = "none";
            private string _stageGraphic3 = "none";
            private string _resRefOfItemGained = "none";
            private string _nameOfSoundFileBump = "none";
            private string _nameOfSoundFileBreak = "none";
            */

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("If false (default), this prop acts as an indictaor for a diggable tile, think mining (remember to set script call to gaBreakObject for  all diggable indicators as well as pure breakable obejcts). Whe a digable indicator is successfully mined, layer2+ graphcis are set to blnak, revealign layer1. Also tile height of target square is adjusted to current party height. If set to true, this is a pure breakable objects (like a statue or crate).")]
        public bool isPureBreakableProp
        {
            get { return _isPureBreakableProp; }
            set { _isPureBreakableProp = value; }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The dc required when this prop requires a trait roll (as this is take ten roll, make it 10 higher than the skill level you aim for).")]
        public int breakableDC
        {
            get { return _breakableDC; }
            set { _breakableDC = value; }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The number of additional bumps it takes before the prop is considered broken/mined. Default is zero, so it takes only one bump. Can be set up to the value of three. Make sure to set up graphics for each existing stage. The lowest stage number is broken first (eg 1,broken or 1,2,broken).")]
        public int numberOfStages
        {
            get { return _numberOfStages; }
            set { _numberOfStages = value;
                this.counterStages = value;
            }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The resref of an item that has to be in party inventory, like eg a pick axe, in order to break/mine this object. Set to none if no item is required. This property is cumulative to trait required.")]
        public string requiredItemInInventory
        {
            get { return _requiredItemInInventory; }
            set { _requiredItemInInventory = value; }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The tag of the trait rolled when trying to break/mine this object. Set to none if no trait is required. This property is cumulative to item required.")]
        public string breakableTraitTag
        {
            get { return _breakableTraitTag; }
            set { _breakableTraitTag = value; }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The graphic, just the filename without extension, to use for debris of the object once broken. Set to none if the object shall simply vanish wihtout trace.")]
        public string debrisGraphic
        {
            get { return _debrisGraphic; }
            set { _debrisGraphic = value; }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The graphic, just the filename without extension, to use for this damage stage of the object. Set to none if this stage does not exist.")]
        public string stageGraphic1
        {
            get { return _stageGraphic1; }
            set { _stageGraphic1 = value; }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The graphic, just the filename without extension, to use for this damage stage of the object. Set to none if this stage does not exist.")]
        public string stageGraphic2
        {
            get { return _stageGraphic2; }
            set { _stageGraphic2 = value; }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The graphic, just the filename without extension, to use for this damage stage of the object. Set to none if this stage does not exist.")]
        public string stageGraphic3
        {
            get { return _stageGraphic3; }
            set { _stageGraphic3 = value; }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The resref of the item that is added to party inventory once the prop is destroyed/mined. Set to none if no item shall be found.")]
        public string resRefOfItemGained
        {
            get { return _resRefOfItemGained; }
            set { _resRefOfItemGained = value; }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The name of the sound file to play when bumping into this prop.")]
        public string nameOfSoundFileBump
        {
            get { return _nameOfSoundFileBump; }
            set { _nameOfSoundFileBump = value; }
        }

        [CategoryAttribute("04h - STEP: diggable tiles and breakable objects"), DescriptionAttribute("The name of the sound file to play when bumping into this prop.")]
        public string nameOfSoundFileBreak
        {
            get { return _nameOfSoundFileBreak; }
            set { _nameOfSoundFileBreak = value; }
        }

        [CategoryAttribute("04g - STEP: lever"), DescriptionAttribute("This prop is a lever which is operated by bumping into it. On each bump it will change its state between OFF and ON, which affects a defined global int and its value as well as the props graphics. Great for controlling doors and chests.")]
        public bool isLever
        {
            get { return _isLever; }
            set { _isLever = value; }
        }

        [CategoryAttribute("04g - STEP: lever"), DescriptionAttribute("Usually levers start OFF, ie isOn is set to false. But this can be freely defined here.")]
        public bool isOn
        {
            get { return _isOn; }
            set { _isOn = value; }
        }

        [CategoryAttribute("04g - STEP: lever"), DescriptionAttribute("Name of the bitmap to use for ON state. Use it without file name extension, like prp_erawen.")]
        public string nameOfBitmapON
        {
            get { return _nameOfBitmapON; }
            set { _nameOfBitmapON = value; }
        }

        [CategoryAttribute("04g - STEP: lever"), DescriptionAttribute("Name of the bitmap to use for OFF state. Use it without file name extension, like prp_erawen.")]
        public string nameOfBitmapOFF
        {
            get { return _nameOfBitmapOFF; }
            set { _nameOfBitmapOFF = value; }
        }

        [CategoryAttribute("04g - STEP: lever"), DescriptionAttribute("The key of the global int that is affected upon state change of the lever.")]
        public string keyOFGlobalIntToChange
        {
            get { return _keyOFGlobalIntToChange; }
            set { _keyOFGlobalIntToChange = value; }
        }

        [CategoryAttribute("04g - STEP: lever"), DescriptionAttribute("Value to assign to the affected global int when lever is in OFF state.")]
        public int valueOfGlobalIntOFF
        {
            get { return _valueOfGlobalIntOFF; }
            set { _valueOfGlobalIntOFF = value; }
        }

        [CategoryAttribute("04g - STEP: lever"), DescriptionAttribute("Value to assign to the affected global int when lever is in ON state.")]
        public int valueOfGlobalIntON
        {
            get { return _valueOfGlobalIntON; }
            set { _valueOfGlobalIntON = value; }
        }

        //_pushableGridCanBeResetEvenAfterCompletion
        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("Even after completion, this pushable grid can be reset via G hotkey while standing on it. Make sure to set pushableGridCanBeResetViaHotkey to true, too.")]
        public bool pushableGridCanBeResetEvenAfterCompletion
        {
            get { return _pushableGridCanBeResetEvenAfterCompletion; }
            set { _pushableGridCanBeResetEvenAfterCompletion = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("This pushable grid can be reset via G hotkey while standing on it.")]
        public bool pushableGridCanBeResetViaHotkey
        {
            get { return _pushableGridCanBeResetViaHotkey; }
            set { _pushableGridCanBeResetViaHotkey = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("This pushable grid counts as not completed (again) when you remove a pushable prop from its correct end position after completion. Doing so will reset the gloabl int that's normally affected on completion in this grid back to 0 again.")]
        public bool completionStateCanBeLostAgain
        {
            get { return _completionStateCanBeLostAgain; }
            set { _completionStateCanBeLostAgain = value; }
        }
        //private int _turnsBeforeGridResets = 0;
        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("If setting this larger than 0, then this grid will reset x turns after the party stepped on it. This settign can be used to make even more challenging grid puzzles, requiring the player to plan his moves in advance.")]
        public int turnsBeforeGridResets
        {
            get { return _turnsBeforeGridResets; }
            set { _turnsBeforeGridResets = value;
                this.timerTurnsBeforeGridResets = value;
                }
        }
        /*
        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("Even after auccessfully completing it, this pushable grid can be reset via G hotkey while standing on it (reset has no effect on the global int chnaged upon completion.)")]
        public bool pushableGridCanBeResetEvenAfterCompletion
        {
            get { return _pushableGridCanBeResetViaHotkey; }
            set { _pushableGridCanBeResetViaHotkey = value; }
        }
        */

        //_partyDefaultDrawDirection
        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("The orientation of the party tail after grid reset (up, left, down, right).")]
        public string partyDefaultDrawDirection
        {
            get { return _partyDefaultDrawDirection; }
            set { _partyDefaultDrawDirection = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("The X-component of the default position that the party is teleported to on reset of the grid via G hotkey.")]
        public int partyDefaultPushableGridPositionX
        {
            get { return _partyDefaultPushableGridPositionX; }
            set { _partyDefaultPushableGridPositionX = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("The Y-component of the default position that the party is teleported to on reset of the grid via G hotkey.")]
        public int partyDefaultPushableGridPositionY
        {
            get { return _partyDefaultPushableGridPositionY; }
            set { _partyDefaultPushableGridPositionY = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("When true, each pushable object can use the target square of any other object of this grid for successful completion (think Sokoban). When false, each object must end on its very own target square for completion.")]
        public bool allPushableGridTargetPositionsAreShared
        {
            get { return _allPushableGridTargetPositionsAreShared; }
            set { _allPushableGridTargetPositionsAreShared = value; }
        }
        
        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("The key of the global int to change upon completion of the grid.")]
        public string keyOfGlobalIntToChangeUponPushableGridCompletion
        {
            get { return _keyOfGlobalIntToChangeUponPushableGridCompletion; }
            set { _keyOfGlobalIntToChangeUponPushableGridCompletion = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("The value that the global int is set to upon completion.")]
        public int valueOfGlobalIntToChangeUponPushableGridCompletion
        {
            get { return _valueOfGlobalIntToChangeUponPushableGridCompletion; }
            set { _valueOfGlobalIntToChangeUponPushableGridCompletion = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("When true, the objects on the grid cannot be moved any more after full completion of the grid.")]
        public bool lockGridOnCompletion
        {
            get { return _lockGridOnCompletion; }
            set { _lockGridOnCompletion = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("When true, all objects on the grid are removed upon failure. This requires allPushableGridTargetPositionsAreShared to be set to false - failure is moving an object onto the target location of another object of the grid.")]
        public bool removeGridOnFailure
        {
            get { return _removeGridOnFailure; }
            set { _removeGridOnFailure = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("The text shown as floaty and in log  upon failure. When set to none, no text is shown.")]
        public string messageOnFailure
        {
            get { return _messageOnFailure; }
            set { _messageOnFailure = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("When true, all objects on the grid are removed upon completion. Completion is moving all objects onto the viable target location(s). Again, allPushableGridTargetPositionsAreShared determines whether each object has to end on its very own target location.")]
        public bool removeGridOnCompletion
        {
            get { return _removeGridOnCompletion; }
            set { _removeGridOnCompletion = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("The text shown as floaty and in log  upon completion. When set to none, no text is shown.")]
        public string messageOnCompletion
        {
            get { return _messageOnCompletion; }
            set { _messageOnCompletion = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("The key of the global int to change upon failure of the grid.")]
        public string keyOfGlobalIntToChangeUponPushableGridFailure
        {
            get { return _keyOfGlobalIntToChangeUponPushableGridFailure; }
            set { _keyOfGlobalIntToChangeUponPushableGridFailure = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("The value that the global int is set to upon failure.")]
        public int valueOfGlobalIntToChangeUponPushableGridFailure
        {
            get { return _valueOfGlobalIntToChangeUponPushableGridFailure; }
            set { _valueOfGlobalIntToChangeUponPushableGridFailure = value; }
        }

        [CategoryAttribute("04f - STEP: pushable grid"), DescriptionAttribute("When true, the objects on the grid cannot be moved any more after afilure of the grid.")]
        public bool lockGridOnFailure
        {
            get { return _lockGridOnFailure; }
            set { _lockGridOnFailure = value; }
        }
        /*
        //2.pushable object properties (04f - STEP: Pushable object)
        private bool _isPushable = false;
        private string _pushableGridTriggerTag = "none";
        private int _pushableStartPositionX = 0;
        private int _pushableStartPositionY = 0;
        private int _pushableTargetPositionX = 0;
        private int _pushableTargetPositionY = 0;
        private string _pushableTraitTag = "strongman";
        private int _pushableDC = 10;
        */

        [CategoryAttribute("04f - STEP: pushable object"), DescriptionAttribute("When true, this object can be pushed around within the limits of its grid, which is defined by an otherwise empty trigger and its squares (see pushableGridTriggerTag).")]
        public bool isPushable
        {
            get { return _isPushable; }
            set { _isPushable = value; }
        }

        [CategoryAttribute("04f - STEP: pushable object"), DescriptionAttribute("The tag of the trigger that defines the grid on which this pushable object can be moved.")]
        public string pushableGridTriggerTag
        {
            get { return _pushableGridTriggerTag; }
            set { _pushableGridTriggerTag = value; }
        }

        [CategoryAttribute("04f - STEP: pushable object"), DescriptionAttribute("The X-component of the position that this prop is set back to upon reset of the grid (eg via G hotkey).")]
        public int pushableStartPositionX
        {
            get { return _pushableStartPositionX; }
            set { _pushableStartPositionX = value; }
        }

        [CategoryAttribute("04f - STEP: pushable object"), DescriptionAttribute("The Y-component of the position that this prop is set back to upon reset of the grid (eg via G hotkey).")]
        public int pushableStartPositionY
        {
            get { return _pushableStartPositionY; }
            set { _pushableStartPositionY = value; }
        }

        [CategoryAttribute("04f - STEP: pushable object"), DescriptionAttribute("The X-component of the target position that this prop must end on for grid completion. Note that with shared target psoitions each prop can also end on the target positions of the other props.")]
        public int pushableTargetPositionX
        {
            get { return _pushableTargetPositionX; }
            set { _pushableTargetPositionX = value; }
        }

        [CategoryAttribute("04f - STEP: pushable object"), DescriptionAttribute("The Y-component of the target position that this prop must end on for grid completion. Note that with shared target psoitions each prop can also end on the target positions of the other props.")]
        public int pushableTargetPositionY
        {
            get { return _pushableTargetPositionY; }
            set { _pushableTargetPositionY = value; }
        }

        [CategoryAttribute("04f - STEP: pushable object"), DescriptionAttribute("The tag of the trait used for pushing this prop.")]
        public string pushableTraitTag
        {
            get { return _pushableTraitTag; }
            set { _pushableTraitTag = value; }
        }

        [CategoryAttribute("04f - STEP: pushable object"), DescriptionAttribute("The DC for pushing this prop (use 10 more than the skill level you aim at as this is a take-ten static check)")]
        public int pushableDC
        {
            get { return _pushableDC; }
            set { _pushableDC = value; }
        }

        [CategoryAttribute("04e - STEP: climbable prop"), DescriptionAttribute("Cimbable props can be approached from two sides: the neighbbouring square as wella s from their own square. In both cases, the player has to walk the party over the climbale side of the prop. Example: A prop with climb direction north can be used by moving upwards from its own square or downwards from the square north of it.")]
        public bool isClimbable
        {
            get { return _isClimbable; }
            set { _isClimbable = value; }
        }
        //A prop with climb direction north can be used by moving upwards from its own square or downwards from the square north of it.
        [CategoryAttribute("04e - STEP: climbable prop"), DescriptionAttribute("Can be: north, east, south or west. A prop with climb direction north can be used by moving upwards from its own square or downwards from the square north of it.")]
        public string climbDirection
        {
            get { return _climbDirection; }
            set { _climbDirection = value; }
        }

        [CategoryAttribute("04e - STEP: climbable prop"), DescriptionAttribute("The DC for the skill check (set it 10 higher than the skill level you aim for as a take-ten-check is applied).")]
        public int climbDC
        {
            get { return _climbDC; }
            set { _climbDC = value; }
        }

        [CategoryAttribute("04e - STEP: climbable prop"), DescriptionAttribute("The tag of the trait used for climbing.")]
        public string climbTrait
        {
            get { return _climbTrait; }
            set { _climbTrait = value; }
        }

        [Browsable(true), TypeConverter(typeof(ConversationTypeConverter))]
        [CategoryAttribute("04d - STEP: conversation"), DescriptionAttribute("The conversation to launch when the party is on the same square as this prop.")]
        public string ConversationWhenOnPartySquare
        {
            get { return _ConversationWhenOnPartySquare; }
            set { _ConversationWhenOnPartySquare = value; }
        }

        [CategoryAttribute("04d - STEP: conversation"), DescriptionAttribute("For props with convos that call for an encounter within. You can set the define the shwon challenge level with this property (also set alwaysFlagAsEncounter to true).")]
        public int challengeLevelAssignedForEncounterInConvo
        {
            get { return _challengeLevelAssignedForEncounterInConvo; }
            set { _challengeLevelAssignedForEncounterInConvo = value; }
        }

        [CategoryAttribute("04d - STEP: conversation"), DescriptionAttribute("Set to true to show enounter challenge level instead of info on optional/mandatory conversation state. In case the encounter is not directly attached to the prop, but called from within convo, also use challengeLevelAssignedForEncounterInConvo.")]
        public bool alwaysFlagAsEncounter
        {
            get { return _alwaysFlagAsEncounter; }
            set { _alwaysFlagAsEncounter = value; }
        }

        [CategoryAttribute("04d - STEP: conversation"), DescriptionAttribute("True = conversation on this prop cannot be avoided; False = conversation on this prop is not displayed when avoid conversations toggle is pressed.")]
        public bool unavoidableConversation
        {
            get { return _unavoidableConversation; }
            set { _unavoidableConversation = value; }
        }


        [Browsable(true), TypeConverter(typeof(EncounterTypeConverter))]
        [CategoryAttribute("04c - STEP: encounter"), DescriptionAttribute("The encounter to launch when the party is on the same square as this prop (conversations, if any, are run first then encounters).")]
        public string EncounterWhenOnPartySquare
        {
            get { return _EncounterWhenOnPartySquare; }
            set { _EncounterWhenOnPartySquare = value; }
        }
        [CategoryAttribute("04c - STEP: encounter"), DescriptionAttribute("If set to true then the prop will be deleted from the area after the assigned encounter to this prop is won.")]
        public bool DeletePropWhenThisEncounterIsWon
        {
            get { return _DeletePropWhenThisEncounterIsWon; }
            set { _DeletePropWhenThisEncounterIsWon = value; }
        }
        [CategoryAttribute("04c - STEP: encounter"), DescriptionAttribute("The name of the encounter ingame that is shown to the player when hovering the mouse over the prop that carries the encounter. Set to none, if no name shall be shown.")]
        public string ingameShownEncName
        {
            get { return _ingameShownEncName; }
            set { _ingameShownEncName = value; }
        }
        [CategoryAttribute("04a - STEP: script"), DescriptionAttribute("Name of the script to fire when stepping onto or bumping into this prop. Use the full file name, including .cs extension, like gaCastSpell.cs")]
        public string scriptFilename
        {
            get { return _scriptFilename; }
            set { _scriptFilename  = value; }
        }

        [CategoryAttribute("04a - STEP: script"), DescriptionAttribute("First parameter for the script.")]
        public string parm1
        {
            get { return _parm1; }
            set { _parm1 = value; }
        }

        [CategoryAttribute("04a - STEP: script"), DescriptionAttribute("Second parameter for the script.")]
        public string parm2
        {
            get { return _parm2; }
            set { _parm2 = value; }
        }

        [CategoryAttribute("04a - STEP: script"), DescriptionAttribute("Third parameter for the script.")]
        public string parm3
        {
            get { return _parm3; }
            set { _parm3 = value; }
        }

        [CategoryAttribute("04a - STEP: script"), DescriptionAttribute("Fourth parameter for the script.")]
        public string parm4
        {
            get { return _parm4; }
            set { _parm4 = value; }
        }

        [CategoryAttribute("04a - STEP: script"), DescriptionAttribute("Floaty that appears upon activation of the script. Leave at none for no floaty.")]
        public string scriptActivationFloaty
        {
            get { return _scriptActivationFloaty; }
            set { _scriptActivationFloaty = value; }
        }

        [CategoryAttribute("04a - STEP: script"), DescriptionAttribute("Log entry that appears upon activation of the script. LEave at none for no log entry.")]
        public string scriptActivationLogEntry
        {
            get { return _scriptActivationLogEntry; }
            set { _scriptActivationLogEntry = value; }
        }

        [CategoryAttribute("07 - Locals"), DescriptionAttribute("Can be used for creating new properties or making individual props act unique.")]
        public List<LocalInt> PropLocalInts
        {
            get { return _PropLocalInts; }
            set { _PropLocalInts = value; }
        }
        [CategoryAttribute("07 - Locals"), DescriptionAttribute("Can be used for creating new properties or making individual props act unique.")]
        public List<LocalString> PropLocalStrings
        {
            get { return _PropLocalStrings; }
            set { _PropLocalStrings = value; }
        }
        
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("This is the location that a moving prop will use as its post location (the location where it will stand or return to unless chasing the party).")]
        public int PostLocationX
        {
            get { return _PostLocationX; }
            set { _PostLocationX = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("This is the location that a moving prop will use as its post location (the location where it will stand or return to unless chasing the party).")]
        public int PostLocationY
        {
            get { return _PostLocationY; }
            set { _PostLocationY = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("List of waypoints for patrolling type movements.")]
        public List<WayPoint> WayPointList
        {
            get { return _WayPointList; }
            set { _WayPointList = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("List of schedules for patrolling type movements.")]
        public List<Schedule> Schedules
        {
            get { return _Schedules; }
            set { _Schedules = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("if true, the prop can move. if false, the prop will not move at all.")]
        public bool isMover
        {
            get { return _isMover; }
            set { _isMover = value; }
        }
        [Browsable(true), TypeConverter(typeof(MoverTypeConverter))]
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("The type of normal movement for this prop.")]
        public string MoverType
        {
            get { return _MoverType; }
            set { _MoverType = value; }
        }

        //private int _spotEnemy = -1;
        //private int _stealth = -1;
        //private bool _stealthSkipsPropTriggers = false;
        
        //spot Enemy
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("The party rolls its stealth skill against spot enemy value of a prop + 1 (acting as dc, so use at least 10 for an easy roll). On a successful roll the party's stealth skill makes chaser props not initiate or stop a running a chase. At two squres distance between party and prop, the rll is at its default difficulty. If the party gets closer the diffulty increases by 2, if further away it gets easier by 2. It also gets easier during the night (by 4) or in complete darkness (by 12). Also, if a prop has stealthSkipPropTriggers, a succesful roll means the party can just pass through the prop.")]
        public int spotEnemy
        {
            get { return _spotEnemy; }
            set { _spotEnemy = value; }
        }

        //stealth
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("The party rolls its spot enemy skill against stealth value of a prop, must match (acting as dc, so use at least 10 for an easy roll).The roll difficulty increases by 2 for each square of distance between party and prop and alos gets more diffcult with less light (by 4 during night, by 12 in complete darkness).  A failed roll makes props invisible for one step/time per step interval, so the prop can sneak up on the party.")]
        public int stealth
        {
            get { return _stealth; }
            set { _stealth = value; }
        }

        //_stealthSkipsPropTriggers
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("A successfully stealthing party - using spot enemy + 5 as DC - can move through a prop with this set to true, bypassing convo or encounter on this prop.")]
        public bool stealthSkipsPropTriggers
        {
            get { return _stealthSkipsPropTriggers; }
            set { _stealthSkipsPropTriggers = value; }
        }

        //movementSpeed
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("Movement speed of the prop on the adventure map. If set to default -1, the prop has a fixed 40% chance to skip its move in a turn, allowing the party to catch up with it easily. If 0 or more, this value is compared to party speed and defines chances for double move and skip move. Eg 10 is moderately fast, 20 fast and 30 very fast.")]
        public int movementSpeed
        {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
        }

        /*
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("The percent chance to move two squares in one turn.")]
        public int ChanceToMove2Squares
        {
            get { return _ChanceToMove2Squares; }
            set { _ChanceToMove2Squares = value; }
        }
        
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("The percent chance to move zero squares in one turn.")]
        public int ChanceToMove0Squares
        {
            get { return _ChanceToMove0Squares; }
            set { _ChanceToMove0Squares = value; }
        }
        */

        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("True = will chase the party if they come into detection range; False = will not chase the party.")]
        public bool isChaser
        {
            get { return _isChaser; }
            set { _isChaser = value; }
        }
        
        /*
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("True = conversation on this prop cannot be avoided; False = conversation on this prop is not displayed when avoidconversations toggle is pressed.")]
        public bool unavoidableConversation
        {
            get { return _unavoidableConversation; }
            set { _unavoidableConversation = value; }
        }
        */

        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("Light related - true = is 2 square radius light of yellowish color, flickering and position shifting, ie think torch like")]
        public bool isLight
        {
            get { return _isLight; }
            set { _isLight = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("Light related - true = draws the color of the light and its intense glow, False = colorless light with no glow, ideal for extending light range of other light sources, just place two squares away from them and light up large areas")]
        public bool hasHalo
        {
            get { return _hasHalo; }
            set { _hasHalo = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("Light related: defaults to 1.0f. The higher, the more colorful the center of the light becomes. Keep between 0.1f and 1.9f as suggestion.")]
        public float focalIntensity
        {
            get { return _focalIntensity; }
            set { _focalIntensity = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("Light related: defaults to 1.0f. The higher, the more colorful the rings (outside center) of the light become. Keep between 0.1f and 1.9f as suggestion.")]
        public float ringIntensity
        {
            get { return _ringIntensity; }
            set { _ringIntensity = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("If the party is within this range radius (in squares) and the Prop is a chaser (isChaser = true), the Prop will start chasing the party.")]
        public int ChaserDetectRangeRadius
        {
            get { return _ChaserDetectRangeRadius; }
            set { _ChaserDetectRangeRadius = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("If the party is outside this range radius (in squares) and the Prop was chasing, the Prop will stop chasing and return to its normal movements or post.")]
        public int ChaserGiveUpChasingRangeRadius
        {
            get { return _ChaserGiveUpChasingRangeRadius; }
            set { _ChaserGiveUpChasingRangeRadius = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("The Prop will only chase for this amount of time (seconds, one move is 6 seconds) and then give up and return to its normal movements or post.")]
        public int ChaserChaseDuration
        {
            get { return _ChaserChaseDuration; }
            set { _ChaserChaseDuration = value; }
        }
        [CategoryAttribute("08 - Project Living World"), DescriptionAttribute("This is the range from the PostLocation(X,Y) for determining allowable random locations to set as the next way point for Random MoverTypes.")]
        public int RandomMoverRadius
        {
            get { return _RandomMoverRadius; }
            set { _RandomMoverRadius = value; }
        }
        
        [Browsable(true), TypeConverter(typeof(IBScriptConverter))]
        [CategoryAttribute("06 - IBScript Hooks"), DescriptionAttribute("IBScript name to be run for this Prop at the end of each move on this area map (not combat)")]
        public string OnHeartBeatIBScript
        {
            get { return onHeartBeatIBScript; }
            set { onHeartBeatIBScript = value; }
        }
        [CategoryAttribute("06 - IBScript Hooks"), DescriptionAttribute("Parameters to be used for this IBScript hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnHeartBeatIBScriptParms
        {
            get { return onHeartBeatIBScriptParms; }
            set { onHeartBeatIBScriptParms = value; }
        }
        #endregion

        public Prop()
        {
        }
        
        public void LoadPropBitmap(ParentForm prntForm)
        {
            if (File.Exists(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + this.ImageFileName + ".png"))
            {
                this.propBitmap = new Bitmap(prntForm._mainDirectory + "\\modules\\" + prntForm.mod.moduleName + "\\graphics\\" + this.ImageFileName + ".png");
            }
            else if (File.Exists(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\" + this.ImageFileName + ".png"))
            {
                this.propBitmap = new Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\" + this.ImageFileName + ".png");
            }
            else
            {
                this.propBitmap = new Bitmap(prntForm._mainDirectory + "\\default\\NewModule\\graphics\\" + "missingtexture.png");
            }
        }        
        public void LoadPropSpriteStuffForTS(string moduleFolderPath)
        {
            /*if (File.Exists(moduleFolderPath + "\\graphics\\sprites\\props\\" + this.PropSpriteFilename))
            {
                this.PropSprite = PropSprite.loadSpriteFile(moduleFolderPath + "\\graphics\\sprites\\props\\" + this.PropSpriteFilename);
                this.PropSprite.passRefs(game);
                this.PropSprite.Image = PropSprite.LoadSpriteSheetBitmap(moduleFolderPath + "\\graphics\\sprites\\props\\" + this.PropSprite.SpriteSheetFilename);
                //this.PropSprite.TextureStream = PropSprite.LoadTextureStream(moduleFolderPath + "\\graphics\\sprites\\props\\" + this.PropSprite.SpriteSheetFilename);
            }            
            else
            {
                MessageBox.Show("failed to load prop SpriteStuff");
            }*/
        }
        public Prop ShallowCopy()
        {
            return (Prop)this.MemberwiseClone();
        }
        public Prop DeepCopy()
        {
            Prop other = (Prop)this.MemberwiseClone();
            other.PropLocalInts = new List<LocalInt>();
            foreach (LocalInt l in this.PropLocalInts)
            {
                LocalInt Lint = new LocalInt();
                Lint.Key = l.Key;
                Lint.Value = l.Value;
                other.PropLocalInts.Add(Lint);
            }
            other.PropLocalStrings = new List<LocalString>();
            foreach (LocalString l in this.PropLocalStrings)
            {
                LocalString Lstr = new LocalString();
                Lstr.Key = l.Key;
                Lstr.Value = l.Value;
                other.PropLocalStrings.Add(Lstr);
            }
            other.WayPointList = new List<WayPoint>();
            foreach (WayPoint coor in this.WayPointList)
            {
                other.WayPointList.Add(coor.DeepCopy());
            }
            return other;
        }
    }        
}
