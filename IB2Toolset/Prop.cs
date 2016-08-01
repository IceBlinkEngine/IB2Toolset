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

        private int _LocationX = 0;
        private int _LocationY = 0;
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
        private string onHeartBeatLogicTree = "none";
        private string onHeartBeatParms = "";
        private string onHeartBeatIBScript = "none";
        private string onHeartBeatIBScriptParms = "";
        private bool _unavoidableConversation = false;
        private bool _isLight = false;
        private bool _hasHalo = false;
        #endregion

        #region Properties
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
        [EditorAttribute(typeof(FileNameSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("02 - Sprite"), DescriptionAttribute("Filename for the prop's token (no extension).")]
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
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("True = is 2 square radius light of yellowish color, flickering and position shifting, ie think torch like")]
        public bool isLight
        {
            get { return _isLight; }
            set { _isLight = value; }
        }
        [CategoryAttribute("05 - Project Living World"), DescriptionAttribute("True = draws the color of the light and its intense glow, False = colorless light with no glow, ideal for extending light range of other light sources, just place two squares away from them and light up large areas")]
        public bool hasHalo
        {
            get { return _hasHalo; }
            set { _hasHalo = value; }
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
        [Browsable(true), TypeConverter(typeof(LogicTreeConverter))]
        [CategoryAttribute("03 - LogicTree Hooks"), DescriptionAttribute("LogicTree name to be run for this Prop at the end of each move on this area map (not combat)")]
        public string OnHeartBeatLogicTree
        {
            get { return onHeartBeatLogicTree; }
            set { onHeartBeatLogicTree = value; }
        }
        [CategoryAttribute("03 - LogicTree Hooks"), DescriptionAttribute("Parameters to be used for this LogicTree hook (as many parameters as needed, comma deliminated with no spaces)")]
        public string OnHeartBeatParms
        {
            get { return onHeartBeatParms; }
            set { onHeartBeatParms = value; }
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
