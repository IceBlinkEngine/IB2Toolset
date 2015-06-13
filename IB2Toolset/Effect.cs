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
    /*public class Effects
    {
        public List<Effect> effectsList = new List<Effect>();

        public Effects()
        {
        }
        public void saveEffectsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public Effects loadEffectsFile(string filename)
        {
            Effects toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (Effects)serializer.Deserialize(file, typeof(Effects));
            }
            return toReturn;
        }
        public Effect getEffectByTag(string tag)
        {
            foreach (Effect ef in effectsList)
            {
                if (ef.tag == tag) return ef;
            }
            return null;
        }
    }*/

    public class Effect
    {        
        #region Fields        
        private string _name = "newEffect";
        private string _tag = "newEffectTag";
        private string _tagOfSender = "senderTag";
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
        private string _effectScript = "none";
        //private ScriptSelectEditorReturnObject effectScript = new ScriptSelectEditorReturnObject();
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("How long the Effect lasts in units of time")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("How long the Effect has been going on so far in units of time")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("At what time did the Effect begin, in units of time")]
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
