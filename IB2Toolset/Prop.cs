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

        #endregion

        #region Properties


        [Browsable(true), TypeConverter(typeof(IBScriptConverter))]  
        [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("IBScript name to be run for this Prop when a Player or Creature stands on this Prop")]  
        public string OnEnterSquareIBScript  
        {  
            get { return _OnEnterSquareIBScript; }  
            set { _OnEnterSquareIBScript = value; }  
        }

        [Browsable(true), TypeConverter(typeof(ScriptConverter))]  
         [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("Script name to be run for this Prop when a Player or Creature stands on this Prop")]  
         public string OnEnterSquareScript  
         {  
             get { return _OnEnterSquareScript; }  
             set { _OnEnterSquareScript = value; }  
         }  
         [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("Parameter 1 to be used for this Script hook (leave as 'none' if not used)")]  
         public string OnEnterSquareScriptParm1
         {  
             get { return _OnEnterSquareScriptParm1; }  
             set { _OnEnterSquareScriptParm1 = value; }  
         }  
         [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("Parameter 2 to be used for this Script hook (leave as 'none' if not used)")]  
         public string OnEnterSquareScriptParm2
         {  
             get { return _OnEnterSquareScriptParm2; }  
             set { _OnEnterSquareScriptParm2 = value; }  
         }  
         [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("Parameter 3 to be used for this Script hook (leave as 'none' if not used)")]  
         public string OnEnterSquareScriptParm3
         {  
             get { return _OnEnterSquareScriptParm3; }  
             set { _OnEnterSquareScriptParm3 = value; }  
         }

        [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("if true, the prop is used as a trap and can be disabled by a thief (pass/fail check made) or other selected classes.")]  
        public bool isTrap  
        {  
             get { return _isTrap; }  
             set { _isTrap = value; }  
        }
          
        [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("the Difficulty Check value used when a Player tries to disable this trap.")]  
        public int trapDCforDisableCheck
        {  
             get { return _trapDCforDisableCheck; }  
             set { _trapDCforDisableCheck = value; }  
        }  
          
         [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("Parameter 4 to be used for this Script hook (leave as 'none' if not used)")]  
         public string OnEnterSquareScriptParm4
         {  
             get { return _OnEnterSquareScriptParm4; }  
             set { _OnEnterSquareScriptParm4 = value; }  
         }  
         [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("if true, the prop OnEnterSquareIBScript can be triggered by Players; if false, Players will not trigger the ibscript.")]  
         public bool canBeTriggeredByPc
         {  
             get { return _canBeTriggeredByPc; }  
             set { _canBeTriggeredByPc = value; }  
         }  
         [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("if true, the prop OnEnterSquareIBScript can be triggered by Creatures; if false, Creatures will not trigger the ibscript.")]  
         public bool canBeTriggeredByCreature
         {  
             get { return _canBeTriggeredByCreature; }  
             set { _canBeTriggeredByCreature = value; }  
         }  
         [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("The number of times that the OnEnterSquare script or IBScript can be triggered. Each time the script is triggered, this number will be decremented by one. Once this number reaches zero, the Prop will be removed from the encounter map.")]  
         public int numberOfScriptCallsRemaining
         {  
             get { return _numberOfScriptCallsRemaining; }  
             set { _numberOfScriptCallsRemaining = value; }  
         }  
      
        [CategoryAttribute("03 - Triggers (combat)"), DescriptionAttribute("Parameters to be used for this IBScript hook (as many parameters as needed, comma deliminated with no spaces)")]  
        public string OnEnterSquareIBScriptParms
        {  
            get { return _OnEnterSquareIBScriptParms; }  
            set { _OnEnterSquareIBScriptParms = value; }  
        }  

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("This determines how swiftly your animated prop fades in and out after all frames have been done (numberOfCyclesNeededForCompletion * maxNumberOfFrames). Whens et to 0 (Deafult), tehre'sno fade in or out. The higher, the more slowly the fading happens. Please do not enter a number higher than half of (numberOfCyclesNeededForCompletion * maxNumberOfFrames). E.g. 15 framesNeededForFullFadeInOut feels quite nicely if you need  avalue for orientation.")]
        public int framesNeededForFullFadeInOut
        {
            get { return _framesNeededForFullFadeInOut; }
            set { _framesNeededForFullFadeInOut = value; }
        }

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("This determines how many times ou want the animation to repeat before it is considered complete. DoOnce will only register once all cycles have been done. ChanceToTriggerAnimationCycle will trigger the number of cycles specified here.")]
        public int numberOfCyclesNeededForCompletion
        {
            get { return _numberOfCyclesNeededForCompletion; }
            set { _numberOfCyclesNeededForCompletion = value; }
        }

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("Set the number of frames of this prop. 1 means a non-animated prop.")]
        public int maxNumberOfFrames
        {
            get { return _maxNumberOfFrames; }
            set { _maxNumberOfFrames = value; }
        }

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("The speed of the animation (should be independent from cpu or gpu speed). Higher is slower. Trying with default of 20.")]
        public float updateTicksNeededTillNextFrame
        {
            get { return _updateTicksNeededTillNextFrame; }
            set { _updateTicksNeededTillNextFrame = value; }
        }

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("The chance of triggering an animation cycle per second in percent. Defaults to 100. Use this for props that make randomized pauses before starting another animation cycle.")]
        public float chanceToTriggerAnimationCycle
        {
            get { return _chanceToTriggerAnimationCycle; }
            set { _chanceToTriggerAnimationCycle = value; }
        }

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("The height in pixels of one frame of this prop. Defaults to 100.")]
        public int propFrameHeight
        {
            get { return _propFrameHeight; }
            set { _propFrameHeight = value; }
        }

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("This scales the animated props x and y dimension by the entered number of precent points. Defaults to 100. Note that you always place the top left corner of a prop.")]
        public int sizeFactor
        {
            get { return _sizeFactor; }
            set { _sizeFactor = value; }
        }

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("Thé prop animation is played only once and then animationIsActive is set to fale; setting animationIsActive to true via script will allow for  another animation cycle.")]
        public bool doOnce
        {
            get { return _doOnce; }
            set { _doOnce = value; }
        }

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("Setting this to false turns teh animation off. Can be de/activated via script.")]
        public bool animationIsActive
        {
            get { return _animationIsActive; }
            set { _animationIsActive = value; }
        }

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("The prop is invisible when animation is not currently running, ideal for making occassionaly happening ambience animations as e.g. ground fog (when combined with chanceToTriggerAnimationCycle)")]
        public bool hiddenWhenComplete
        {
            get { return _hiddenWhenComplete; }
            set { _hiddenWhenComplete = value; }
        }

        [CategoryAttribute("06 - Prop Animation"), DescriptionAttribute("The prop is invisible when animation is not currently running, ideal for making occassionaly happening ambience animations as e.g. ground fog (when combined with chanceToTriggerAnimationCycle)")]
        public bool hiddenWhenNotActive
        {
            get { return _hiddenWhenNotActive; }
            set { _hiddenWhenNotActive = value; }
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The text to display when player mouses over the Prop on the adventure maps.")]
        public string MouseOverText
        {
            get { return _mouseOverText; }
            set { _mouseOverText = value; }
        }
        [Browsable(true), TypeConverter(typeof(ConversationTypeConverter))]
        [CategoryAttribute("03 - Triggers"), DescriptionAttribute("The conversation to launch when the party is on the same square as this prop.")]
        public string ConversationWhenOnPartySquare
        {
            get { return _ConversationWhenOnPartySquare; }
            set { _ConversationWhenOnPartySquare = value; }
        }
        [Browsable(true), TypeConverter(typeof(EncounterTypeConverter))]
        [CategoryAttribute("03 - Triggers"), DescriptionAttribute("The encounter to launch when the party is on the same square as this prop (conversations, if any, are run first then encounters).")]
        public string EncounterWhenOnPartySquare
        {
            get { return _EncounterWhenOnPartySquare; }
            set { _EncounterWhenOnPartySquare = value; }
        }
        [CategoryAttribute("03 - Triggers"), DescriptionAttribute("If set to true then the prop will be deleted from the area after the assigned encounter to this prop is won.")]
        public bool DeletePropWhenThisEncounterIsWon
        {
            get { return _DeletePropWhenThisEncounterIsWon; }
            set { _DeletePropWhenThisEncounterIsWon = value; }
        }
        [CategoryAttribute("04 - Locals"), DescriptionAttribute("Can be used for creating new properties or making individual props act unique.")]
        public List<LocalInt> PropLocalInts
        {
            get { return _PropLocalInts; }
            set { _PropLocalInts = value; }
        }
        [CategoryAttribute("04 - Locals"), DescriptionAttribute("Can be used for creating new properties or making individual props act unique.")]
        public List<LocalString> PropLocalStrings
        {
            get { return _PropLocalStrings; }
            set { _PropLocalStrings = value; }
        }
        
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("This is the location that a moving prop will use as its post location (the location where it will stand or return to unless chasing the party).")]
        public int PostLocationX
        {
            get { return _PostLocationX; }
            set { _PostLocationX = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("This is the location that a moving prop will use as its post location (the location where it will stand or return to unless chasing the party).")]
        public int PostLocationY
        {
            get { return _PostLocationY; }
            set { _PostLocationY = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("List of waypoints for patrolling type movements.")]
        public List<WayPoint> WayPointList
        {
            get { return _WayPointList; }
            set { _WayPointList = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("List of schedules for patrolling type movements.")]
        public List<Schedule> Schedules
        {
            get { return _Schedules; }
            set { _Schedules = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("if true, the prop can move. if false, the prop will not move at all.")]
        public bool isMover
        {
            get { return _isMover; }
            set { _isMover = value; }
        }
        [Browsable(true), TypeConverter(typeof(MoverTypeConverter))]
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("The type of normal movement for this prop.")]
        public string MoverType
        {
            get { return _MoverType; }
            set { _MoverType = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("The percent chance to move two squares in one turn.")]
        public int ChanceToMove2Squares
        {
            get { return _ChanceToMove2Squares; }
            set { _ChanceToMove2Squares = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("The percent chance to move zero squares in one turn.")]
        public int ChanceToMove0Squares
        {
            get { return _ChanceToMove0Squares; }
            set { _ChanceToMove0Squares = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("True = will chase the party if they come into detection range; False = will not chase the party.")]
        public bool isChaser
        {
            get { return _isChaser; }
            set { _isChaser = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("True = conversation on this prop cannot be avoided; False = conversation on this prop is not displayed when avoidconversations toggle is pressed.")]
        public bool unavoidableConversation
        {
            get { return _unavoidableConversation; }
            set { _unavoidableConversation = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("Light related - true = is 2 square radius light of yellowish color, flickering and position shifting, ie think torch like")]
        public bool isLight
        {
            get { return _isLight; }
            set { _isLight = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("Light related - true = draws the color of the light and its intense glow, False = colorless light with no glow, ideal for extending light range of other light sources, just place two squares away from them and light up large areas")]
        public bool hasHalo
        {
            get { return _hasHalo; }
            set { _hasHalo = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("Light related: defaults to 1.0f. The higher, the more colorful the center of the light becomes. Keep between 0.1f and 1.9f as suggestion.")]
        public float focalIntensity
        {
            get { return _focalIntensity; }
            set { _focalIntensity = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("Light related: defaults to 1.0f. The higher, the more colorful the rings (outside center) of the light become. Keep between 0.1f and 1.9f as suggestion.")]
        public float ringIntensity
        {
            get { return _ringIntensity; }
            set { _ringIntensity = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("If the party is within this range radius (in squares) and the Prop is a chaser (isChaser = true), the Prop will start chasing the party.")]
        public int ChaserDetectRangeRadius
        {
            get { return _ChaserDetectRangeRadius; }
            set { _ChaserDetectRangeRadius = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("If the party is outside this range radius (in squares) and the Prop was chasing, the Prop will stop chasing and return to its normal movements or post.")]
        public int ChaserGiveUpChasingRangeRadius
        {
            get { return _ChaserGiveUpChasingRangeRadius; }
            set { _ChaserGiveUpChasingRangeRadius = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("The Prop will only chase for this amount of time (seconds, one move is 6 seconds) and then give up and return to its normal movements or post.")]
        public int ChaserChaseDuration
        {
            get { return _ChaserChaseDuration; }
            set { _ChaserChaseDuration = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("This is the range from the PostLocation(X,Y) for determining allowable random locations to set as the next way point for Random MoverTypes.")]
        public int RandomMoverRadius
        {
            get { return _RandomMoverRadius; }
            set { _RandomMoverRadius = value; }
        }
        
        [Browsable(true), TypeConverter(typeof(IBScriptConverter))]
        [CategoryAttribute("03 - IBScript Hooks"), DescriptionAttribute("IBScript name to be run for this Prop at the end of each move on this area map (not combat)")]
        public string OnHeartBeatIBScript
        {
            get { return onHeartBeatIBScript; }
            set { onHeartBeatIBScript = value; }
        }
        [CategoryAttribute("03 - IBScript Hooks"), DescriptionAttribute("Parameters to be used for this IBScript hook (as many parameters as needed, comma deliminated with no spaces)")]
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
