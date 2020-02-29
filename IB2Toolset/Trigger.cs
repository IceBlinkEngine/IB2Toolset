using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using Newtonsoft.Json;

namespace IB2Toolset
{
    public enum TriggerType
    {
        None = 0,
        Script = 1,
        Encounter = 2,
        Conversation = 3,
        Transition = 4,
        Container = 5
    }

    public class Trigger
    {
        #region Fields


        private string vanishInXTurns = "";
        private int vanishCounter = 0;
        private string appearInXTurns = "";
        private int appearCounter = 0;
        private bool changeWalkableStateOnEnabledStateChange = false;
        //below is likely only needed in toolset
        private string tagOfPRopToPlaceOnTop = "none";

        private string bumpTriggerDirection = "none"; //fromSouth, fromNorth, fromEast, fromWest, none
        private string mouseOverText = "none";

        private bool chkTrigHidden = false;
        private string txtTrigFindingTraitTag;
        private string txtTrigFindingDC;
        private string txtTrigSpawningTraitTag;
        private string txtTrigSpawningDC;
        private string txtTrigDespawningTraitTag;
        private string txtTrigDespawningDC;
        private string txtTrigDisablingTraitTag;
        private string txtTrigDisablingDC;
        private string txtTrigEnablingTraitTag;
        private string txtTrigEnablingDC;
        private bool chkTrigEnableOnFinding = false;
    

        private bool connectedDiscovery = false;
        private bool requiresActiveSearch = false;
        private string triggerTag = "newTrigger"; //must be unique
        private string triggerName = "newName";
        private string triggerCategory = "newCategory";
        private bool enabled = true;
        private bool doOnceOnly = false;
        private List<Coordinate> triggerSquaresList = new List<Coordinate>();

        private bool enabledEvent1 = true;
        private bool doOnceOnlyEvent1 = false;
        private bool event2RequiresTrueReturnCheck = false;
        private string event1Type = "none";
        //private TriggerType event1TypeEnum = TriggerType.None;
        //private EventObjEditorReturnObject parameters1 = new EventObjEditorReturnObject();
        private string event1FilenameOrTag = "none";        
        private int event1TransPointX = 0;
        private int event1TransPointY = 0;
        private string event1Parm1 = "none";
        private string event1Parm2 = "none";
        private string event1Parm3 = "none";
        private string event1Parm4 = "none";

        private bool enabledEvent2 = true;
        private bool doOnceOnlyEvent2 = false;
        private bool event3RequiresFalseReturnCheck = false;
        private string event2Type = "none";
        //private TriggerType event2TypeEnum = TriggerType.None;
        //private EventObjEditorReturnObject parameters2 = new EventObjEditorReturnObject();
        private string event2FilenameOrTag = "none";
        private int event2TransPointX = 0;
        private int event2TransPointY = 0;
        private string event2Parm1 = "none";
        private string event2Parm2 = "none";
        private string event2Parm3 = "none";
        private string event2Parm4 = "none";

        private bool enabledEvent3 = true;
        private bool doOnceOnlyEvent3 = false;
        private string event3Type = "none";
        //private TriggerType event3TypeEnum = TriggerType.None;
        private string event3FilenameOrTag = "none";
        private int event3TransPointX = 0;
        private int event3TransPointY = 0;
        private string event3Parm1 = "none";
        private string event3Parm2 = "none";
        private string event3Parm3 = "none";
        private string event3Parm4 = "none";

        private bool ConversationCannotBeAvoided = true;

        public int numberOfScriptCallsRemaining = 999;
        //public string traitTagForSpawning = "none";
        //public int DCForSpawning = 0;
        //public string traitTagForFinding = "none";
        //public int DCForFinding = 0;
        //public bool onlyFunctionalWhenFound = false;
        public bool canBeTriggeredByPc = true;  
        public bool canBeTriggeredByCreature = true;

        public bool isLinkToMaster = false;
        public string tagOfLinkedMaster = "none";
        public string tagOfLink = "none";
        public int transitionToMasterRotationCounter = 1;

        public bool encounterTriggerOnEveryStep = true;

        #endregion

        #region Properties
        [CategoryAttribute("0 - Main"), DescriptionAttribute("Tag of the Trigger (Must be unique)")]
        public string TriggerTag
        {
            get { return triggerTag; }
            set { triggerTag = value; }
        }
        [CategoryAttribute("0 - Main"), DescriptionAttribute("Name of the Trigger")]
        public string TriggerName
        {
            get { return triggerName; }
            set { triggerName = value; }
        }
        [CategoryAttribute("0 - Main"), DescriptionAttribute("Category of the Trigger")]
        public string TriggerCategory
        {
            get { return triggerCategory; }
            set { triggerCategory = value; }
        }
        [CategoryAttribute("0 - Main"), DescriptionAttribute("Used to Enable or Disable the trigger")]
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; }
        }
        [CategoryAttribute("0 - Main"), DescriptionAttribute("Used to allow ground trigger bound conversations to be affected by avoid conversations toggle; by default ground trigger bound conversations are not avoidable")]
        public bool conversationCannotBeAvoided
        {
            get { return enabled; }
            set { enabled = value; }
        }
        [CategoryAttribute("0 - Main"), DescriptionAttribute("Only allow the Trigger to function one time then disable it")]
        public bool DoOnceOnly
        {
            get { return doOnceOnly; }
            set { doOnceOnly = value; }
        }

        //requiresActiveSearch
        [CategoryAttribute("0 - Main"), DescriptionAttribute("If true, this trigger will only work if an active search action (space key) is performed on its square(s).")]
        public bool RequiresActiveSearch
        {
            get { return requiresActiveSearch; }
            set { requiresActiveSearch = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string BumpTriggerDirection
        {
            get { return bumpTriggerDirection; }
            set { bumpTriggerDirection = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public bool ChkTrigHidden
        {
            get { return chkTrigHidden; }
            set { chkTrigHidden = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public bool ChkTrigEnableOnFinding
        {
            get { return chkTrigEnableOnFinding; }
            set { chkTrigEnableOnFinding = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string VanishInXTurns
        {
            get { return vanishInXTurns; }
            set { vanishInXTurns = value; }
        }

        /*
        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string VanishCounter
        {
            get { return vanishCounter; }
            set { vanishCounter = value; }
        }
        */

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string AppearInXTurns
        {
            get { return appearInXTurns; }
            set { appearInXTurns = value; }
        }

        /*
        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string AppearCounter
        {
            get { return appearCounter; }
            set { appearCounter = value; }
        }
        */

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public bool ChangeWalkableStateOnEnabledStateChange
        {
            get { return changeWalkableStateOnEnabledStateChange; }
            set { changeWalkableStateOnEnabledStateChange = value; }
        }


        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TagOfPRopToPlaceOnTop
        {
            get { return tagOfPRopToPlaceOnTop; }
            set { tagOfPRopToPlaceOnTop = value; }
        }
      
        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TxtTrigFindingTraitTag
        {
            get { return txtTrigFindingTraitTag; }
            set { txtTrigFindingTraitTag = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TxtTrigFindingDC
        {
            get { return txtTrigFindingDC; }
            set { txtTrigFindingDC = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TxtTrigSpawningTraitTag
        {
            get { return txtTrigSpawningTraitTag; }
            set { txtTrigSpawningTraitTag = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TxtTrigSpawningDC
        {
            get { return txtTrigSpawningDC; }
            set { txtTrigSpawningDC = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TxtTrigDespawningTraitTag
        {
            get { return txtTrigDespawningTraitTag; }
            set { txtTrigDespawningTraitTag = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TxtTrigDespawningDC
        {
            get { return txtTrigDespawningDC; }
            set { txtTrigDespawningDC = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TxtTrigEnablingTraitTag
        {
            get { return txtTrigEnablingTraitTag; }
            set { txtTrigEnablingTraitTag = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TxtTrigEnablingDC
        {
            get { return txtTrigEnablingDC; }
            set { txtTrigEnablingDC = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TxtTrigDisablingTraitTag
        {
            get { return txtTrigDisablingTraitTag; }
            set { txtTrigDisablingTraitTag = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("none, fromEast, fromSouth, fromWest, fromNorth")]
        public string TxtTrigDisablingDC
        {
            get { return txtTrigDisablingDC; }
            set { txtTrigDisablingDC = value; }
        }

        /*
        private bool chkTrigHidden = false;
        private string txtTrigFindingTraitTag;
        private string txtTrigFindingDC;
        private string txtTrigSpawningTraitTag;
        private string txtTrigSpawningDC;
        private string txtTrigDespawningTraitTag;
        private string txtTrigDespawningDC;
        private string txtTrigDisablingTraitTag;
        private string txtTrigDisablingDC;
        private string txtTrigEnablingTraitTag;
        private string txtTrigEnablingDC;
        private bool chkTrigEnableOnFinding = false;
        */

        [CategoryAttribute("0 - Main"), DescriptionAttribute("mouse over text")]
        public string MouseOverText
        {
            get { return mouseOverText; }
            set { mouseOverText = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("If true, all squares in this trigger's square liste become visible if one of them is discovered.")]
        public bool ConnectedDiscovery
        {
            get { return connectedDiscovery; }
            set { connectedDiscovery = value; }
        }

        [CategoryAttribute("0 - Main"), DescriptionAttribute("List of all the squares that this Trigger covers"), ReadOnly(true)]
        public List<Coordinate> TriggerSquaresList
        {
            get { return triggerSquaresList; }
            set { triggerSquaresList = value; }
        }

        [CategoryAttribute("1 - Event"), DescriptionAttribute("Only allow the Event to function one time then disable it")]
        public bool DoOnceOnlyEvent1
        {
            get { return doOnceOnlyEvent1; }
            set { doOnceOnlyEvent1 = value; }
        }
        [CategoryAttribute("1 - Event"), DescriptionAttribute("Used to Enable or Disable the Event")]
        public bool EnabledEvent1
        {
            get { return enabledEvent1; }
            set { enabledEvent1 = value; }
        }
        [CategoryAttribute("1 - Event"), DescriptionAttribute("Type of event that will be triggered at this point in the order of events")]
        public string Event1Type
        {
            get { return event1Type; }
            set
            {
                event1Type = value;
            }
        }
        /*[CategoryAttribute("1 - Event"), DescriptionAttribute("Type of event that will be triggered at this point in the order of events")]
        public TriggerType Event1TypeEnum
        {
            get { return event1TypeEnum; }
            set
            {
                event1TypeEnum = value;
                event1Type = getStringEnum((int)value);                
            }
        }*/
        
        [CategoryAttribute("1 - Event"), DescriptionAttribute("Filename or Tag of the Event Type")]
        public string Event1FilenameOrTag
        {
            get { return event1FilenameOrTag; }
            set 
            { 
                event1FilenameOrTag = value; 
            }
        }
        [CategoryAttribute("1 - Event"), DescriptionAttribute("Transition to square of Area selected if Transition Type")]
        public int Event1TransPointX
        {
            get { return event1TransPointX; }
            set { event1TransPointX = value; }
        }
        [CategoryAttribute("1 - Event"), DescriptionAttribute("Transition to square of Area selected if Transition Type")]
        public int Event1TransPointY
        {
            get { return event1TransPointY; }
            set { event1TransPointY = value; }
        }
        [CategoryAttribute("1 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event1Parm1
        {
            get { return event1Parm1; }
            set { event1Parm1 = value; }
        }
        [CategoryAttribute("1 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event1Parm2
        {
            get { return event1Parm2; }
            set { event1Parm2 = value; }
        }
        [CategoryAttribute("1 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event1Parm3
        {
            get { return event1Parm3; }
            set { event1Parm3 = value; }
        }
        [CategoryAttribute("1 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event1Parm4
        {
            get { return event1Parm4; }
            set { event1Parm4 = value; }
        }

        [CategoryAttribute("2 - Event"), DescriptionAttribute("Only allow the Event to function one time then disable it")]
        public bool DoOnceOnlyEvent2
        {
            get { return doOnceOnlyEvent2; }
            set { doOnceOnlyEvent2 = value; }
        }

        [CategoryAttribute("2 - Event"), DescriptionAttribute("Will only work if a gc script check on event1 resulted in true (like skill check passed)")]
        public bool Event2RequiresTrueReturnCheck
        {
            get { return event2RequiresTrueReturnCheck; }
            set { event2RequiresTrueReturnCheck = value; }
        }

        [CategoryAttribute("2 - Event"), DescriptionAttribute("Used to Enable or Disable the Event")]
        public bool EnabledEvent2
        {
            get { return enabledEvent2; }
            set { enabledEvent2 = value; }
        }
        [CategoryAttribute("2 - Event"), DescriptionAttribute("Type of event that will be triggered at this point in the order of events")]
        public string Event2Type
        {
            get { return event2Type; }
            set
            {
                event2Type = value;
            }
        }
        /*[CategoryAttribute("2 - Event"), DescriptionAttribute("Type of event that will be triggered at this point in the order of events")]
        public TriggerType Event2TypeEnum
        {
            get { return event2TypeEnum; }
            set
            {
                event2TypeEnum = value;
                event2Type = getStringEnum((int)value);
            }
        }*/
        
        [CategoryAttribute("2 - Event"), DescriptionAttribute("Filename or Tag of the Event Type")]
        public string Event2FilenameOrTag
        {
            get { return event2FilenameOrTag; }
            set { event2FilenameOrTag = value; }
        }
        [CategoryAttribute("2 - Event"), DescriptionAttribute("Transition to square of Area selected if Transition Type")]
        public int Event2TransPointX
        {
            get { return event2TransPointX; }
            set { event2TransPointX = value; }
        }
        [CategoryAttribute("2 - Event"), DescriptionAttribute("Transition to square of Area selected if Transition Type")]
        public int Event2TransPointY
        {
            get { return event2TransPointY; }
            set { event2TransPointY = value; }
        }
        [CategoryAttribute("2 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event2Parm1
        {
            get { return event2Parm1; }
            set { event2Parm1 = value; }
        }
        [CategoryAttribute("2 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event2Parm2
        {
            get { return event2Parm2; }
            set { event2Parm2 = value; }
        }
        [CategoryAttribute("2 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event2Parm3
        {
            get { return event2Parm3; }
            set { event2Parm3 = value; }
        }
        [CategoryAttribute("2 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event2Parm4
        {
            get { return event2Parm4; }
            set { event2Parm4 = value; }
        }

        [CategoryAttribute("3 - Event"), DescriptionAttribute("Only allow the Event to function one time then disable it")]
        public bool DoOnceOnlyEvent3
        {
            get { return doOnceOnlyEvent3; }
            set { doOnceOnlyEvent3 = value; }
        }
        [CategoryAttribute("3 - Event"), DescriptionAttribute("Will only work if a gc script check on event2 resulted in true (like skill check passed)")]
        public bool Event3RequiresFalseReturnCheck
        {
            get { return event3RequiresFalseReturnCheck; }
            set { event3RequiresFalseReturnCheck = value; }
        }
        [CategoryAttribute("3 - Event"), DescriptionAttribute("Used to Enable or Disable the Event")]
        public bool EnabledEvent3
        {
            get { return enabledEvent3; }
            set { enabledEvent3 = value; }
        }
        [CategoryAttribute("3 - Event"), DescriptionAttribute("Type of event that will be triggered at this point in the order of events")]
        public string Event3Type
        {
            get { return event3Type; }
            set
            {
                event3Type = value;
            }
        }
        /*[CategoryAttribute("3 - Event"), DescriptionAttribute("Type of event that will be triggered at this point in the order of events")]
        public TriggerType Event3TypeEnum
        {
            get { return event3TypeEnum; }
            set
            {
                event3TypeEnum = value;
                event3Type = getStringEnum((int)value);
            }
        }*/
        
        [CategoryAttribute("3 - Event"), DescriptionAttribute("Filename or Tag of the Event Type")]
        public string Event3FilenameOrTag
        {
            get { return event3FilenameOrTag; }
            set { event3FilenameOrTag = value; }
        }
        [CategoryAttribute("3 - Event"), DescriptionAttribute("Transition to square of Area selected if Transition Type")]
        public int Event3TransPointX
        {
            get { return event3TransPointX; }
            set { event3TransPointX = value; }
        }
        [CategoryAttribute("3 - Event"), DescriptionAttribute("Transition to square of Area selected if Transition Type")]
        public int Event3TransPointY
        {
            get { return event3TransPointY; }
            set { event3TransPointY = value; }
        }
        [CategoryAttribute("3 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event3Parm1
        {
            get { return event3Parm1; }
            set { event3Parm1 = value; }
        }
        [CategoryAttribute("3 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event3Parm2
        {
            get { return event3Parm2; }
            set { event3Parm2 = value; }
        }
        [CategoryAttribute("3 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event3Parm3
        {
            get { return event3Parm3; }
            set { event3Parm3 = value; }
        }
        [CategoryAttribute("3 - Event"), DescriptionAttribute("Parameter for the Script chosen if Event Type is Script")]
        public string Event3Parm4
        {
            get { return event3Parm4; }
            set { event3Parm4 = value; }
        }
        #endregion

        public Trigger()
        {
        }
        public string getStringEnum(int value)
        {
            string returnString = "none";
            if (value == 0) { return "none"; }
            else if (value == 1) { return "script"; }
            else if (value == 2) { return "encounter"; }
            else if (value == 3) { return "conversation"; }
            else if (value == 4) { return "transition"; }
            else if (value == 5) { return "container"; }
            return returnString;
        }

        public Trigger DeepCopy()
        {
            Trigger other = new Trigger();


              other.vanishInXTurns = vanishInXTurns;
            other.vanishCounter = vanishCounter;
            other.appearInXTurns = appearInXTurns;
            other.appearCounter = appearCounter;
            other.changeWalkableStateOnEnabledStateChange = changeWalkableStateOnEnabledStateChange;
            //below is likely only needed in toolset
            other.tagOfPRopToPlaceOnTop = tagOfPRopToPlaceOnTop;

        other.bumpTriggerDirection = bumpTriggerDirection; //fromSouth, fromNorth, fromEast, fromWest, none
            other.mouseOverText = mouseOverText;
            other.connectedDiscovery = connectedDiscovery;
            other.requiresActiveSearch = requiresActiveSearch;
            other.triggerTag = triggerTag; //must be unique
            other.triggerName = triggerName;
            other.triggerCategory = triggerCategory;
            other.enabled = enabled;
            other.doOnceOnly = doOnceOnly;
            //private List<Coordinate> triggerSquaresList = new List<Coordinate>();

        other.enabledEvent1 = enabledEvent1;
            other.doOnceOnlyEvent1 = doOnceOnlyEvent1;
            other.event2RequiresTrueReturnCheck = event2RequiresTrueReturnCheck;
            other.event1Type = event1Type;
            //private TriggerType event1TypeEnum = TriggerType.None;
            //private EventObjEditorReturnObject parameters1 = new EventObjEditorReturnObject();
            other.event1FilenameOrTag = event1FilenameOrTag;
            other.event1TransPointX = event1TransPointX;
            other.event1TransPointY = event1TransPointY;
            other.event1Parm1 = event1Parm1;
            other.event1Parm2 = event1Parm2;
            other.event1Parm3 = event1Parm3;
            other.event1Parm4 = event1Parm4;

            other.enabledEvent2 = enabledEvent2;
            other.doOnceOnlyEvent2 = doOnceOnlyEvent2;
            other.event3RequiresFalseReturnCheck = event3RequiresFalseReturnCheck;
            other.event2Type = event2Type;
            //private TriggerType event2TypeEnum = TriggerType.None;
            //private EventObjEditorReturnObject parameters2 = new EventObjEditorReturnObject();
            other.event2FilenameOrTag = event2FilenameOrTag;
            other.event2TransPointX = event2TransPointX;
            other.event2TransPointY = event2TransPointY;
            other.event2Parm1 = event2Parm1;
            other.event2Parm2 = event2Parm2;
            other.event2Parm3 = event2Parm3;
            other.event2Parm4 = event2Parm4;

            other.enabledEvent3 = enabledEvent3;
            other.doOnceOnlyEvent3 = doOnceOnlyEvent3;
            other.event3Type = event3Type;
            //private TriggerType event3TypeEnum = TriggerType.None;
            other.event3FilenameOrTag = event3FilenameOrTag;
            other.event3TransPointX = event3TransPointX;
            other.event3TransPointY = event3TransPointY;
            other.event3Parm1 = event3Parm1;
            other.event3Parm2 = event3Parm2;
            other.event3Parm3 = event3Parm3;
            other.event3Parm4 = event3Parm4;

            other.ConversationCannotBeAvoided = ConversationCannotBeAvoided;



        other.chkTrigHidden = chkTrigHidden;
        other.txtTrigFindingTraitTag = txtTrigFindingTraitTag;
        other.txtTrigFindingDC = txtTrigFindingDC;
        other.txtTrigSpawningTraitTag = txtTrigSpawningTraitTag;
        other.txtTrigSpawningDC = txtTrigSpawningDC;
        other.txtTrigDespawningTraitTag = txtTrigDespawningTraitTag;
        other.txtTrigDespawningDC = txtTrigDespawningDC;
        other.txtTrigDisablingTraitTag = txtTrigDisablingTraitTag;
        other.txtTrigDisablingDC  = txtTrigDisablingDC;
        other.txtTrigEnablingTraitTag = txtTrigEnablingTraitTag;
        other.txtTrigEnablingDC = txtTrigEnablingDC;
        other.chkTrigEnableOnFinding = chkTrigEnableOnFinding;

        other.numberOfScriptCallsRemaining = numberOfScriptCallsRemaining;
            other.canBeTriggeredByPc = canBeTriggeredByPc;
            other.canBeTriggeredByCreature = canBeTriggeredByCreature;

            other.isLinkToMaster = isLinkToMaster;
            other.tagOfLinkedMaster = tagOfLinkedMaster;
            other.tagOfLink = tagOfLink;
            other.transitionToMasterRotationCounter = transitionToMasterRotationCounter;

            other.encounterTriggerOnEveryStep = encounterTriggerOnEveryStep;
        
            return other;
        }
    }
}
