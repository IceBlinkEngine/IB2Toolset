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
    /*public class Traits
    {
        public List<Trait> traitList = new List<Trait>();

        public Traits()
        {
        }
        public void saveTraitsFile(string filename)
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public Traits loadTraitsFile(string filename)
        {
            Traits toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (Traits)serializer.Deserialize(file, typeof(Traits));
            }
            return toReturn;
        }
        public Trait getTraitByTag(string tag)
        {
            foreach (Trait ts in traitList)
            {
                if (ts.tag == tag) return ts;
            }
            return null;
        }
        public Trait getTraitByName(string name)
        {
            foreach (Trait ts in traitList)
            {
                if (ts.name == name) return ts;
            }
            return null;
        }
    }*/

    public class Trait
    {
        #region Fields        

        private bool _showOnMainMap = false;
        private string _methodOfChecking = "leader";
        private string _name = "newTrait"; //item name
        private string _tag = "newTraitTag"; //item unique tag name
        private string _traitImage = "sp_magebolt";
        private string _description = "";
        private string _prerequisiteTrait = "none";
        private int _skillModifier = 0;
        private string _skillModifierAttribute = "str";
        //private UsableInSituation useableInSituation = UsableInSituation.Always;
        private string _useableInSituation = "Always";

        private int _requiredStrength = 0;
        private int _requiredDexterity = 0;
        private int _requiredConstitution = 0;
        private int _requiredIntelligence = 0;
        private int _requiredWisdom = 0;
        private int _requiredCharisma = 0;

        //using associated spell for this
        //private string _spriteFilename = "none";
        //using associated spell for this
        //private string _spriteEndingFilename = "none";

        //using associated spell for this
        //private int _costSP = 0;
        //using associated spell for this 
        //private string _traitTargetType = "Enemy";

        //private TargetType spellTargetType = TargetType.Enemy;

        //using associated spell for this
        //private string _traitEffectType = "Damage";

        //private EffectType spellEffectType = EffectType.Damage;

        //using associated spell for this
        //private int _aoeRadius = 0;
        //using associated spell for this
        //private int _range = 0;

        //private ScriptSelectEditorReturnObject spellScript = new ScriptSelectEditorReturnObject();
        private string _traitScript = "none";//not used
        private string _associatedSpellTag = "none";
        private List<EffectTagForDropDownList> _traitEffectTagList = new List<EffectTagForDropDownList>();
        private List<LocalImmunityString> _traitWorksOnlyWhen = new List<LocalImmunityString>();
        private List<LocalImmunityString> _traitWorksNeverWhen = new List<LocalImmunityString>();

        private string _traitToReplaceByTag = "none";

        #endregion

        #region Properties  
        //private bool _showOnMainMap = false;
        //private string _methodOfChecking = "leader";

        [CategoryAttribute("01 - Main"), DescriptionAttribute("How is this trait typically checked for and displayed on mainmap (if at all): leader/Leader/-1 or highest/Highest/-2 or lowest/Lowest/-3 or average/Average/-4) or allMustSucceed/AllMustSucceed/-5 or oneMustSucceed/oneMustSucceed/-6")]
        public string methodOfChecking
        {
            get
            {
                return _methodOfChecking;
            }
            set
            {
                _methodOfChecking = value;
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Is this trait (and its power) displayed on right top corner of mainmap?")]
        public bool showOnMainMap
        {
            get
            {
                return _showOnMainMap;
            }
            set
            {
                _showOnMainMap = value;
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Name of the Trait")]
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
        //[Browsable(true), TypeConverter(typeof(EffectTagTypeConverter))]
        [CategoryAttribute("01 - Main"), DescriptionAttribute("List of EffectTags of this trait")]
        public List<EffectTagForDropDownList> traitEffectTagList
        {
            get { return _traitEffectTagList; }
            set { _traitEffectTagList = value; }
        }
        //[Browsable(true), TypeConverter(typeof(EffectTagTypeConverter))]
        [CategoryAttribute("01 - Main"), DescriptionAttribute("List of tags in pcTags list of a pc that each will allow this trait to work (permanent effect, active use trait); multiple entries are connected with OR")]
        public List<LocalImmunityString> traitWorksOnlyWhen
        {
            get { return _traitWorksOnlyWhen; }
            set { _traitWorksOnlyWhen = value; }
        }
        //[Browsable(true), TypeConverter(typeof(EffectTagTypeConverter))]
        [CategoryAttribute("01 - Main"), DescriptionAttribute("List of tags in pcTags list of a pc that each will prevent this trait from working (permanent effect, active use trait); multiples entries are connected with OR; overrides any entries that allow a trait to work")]
        public List<LocalImmunityString> traitWorksNeverWhen
        {
            get { return _traitWorksNeverWhen; }
            set { _traitWorksNeverWhen = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the Trait (Must be unique)")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the spell called when using this active trait")]
        public string associatedSpellTag
        {
            get
            {
                return _associatedSpellTag;
            }
            set
            {
                _associatedSpellTag = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Image icon of the Trait")]
        public string traitImage
        {
            get
            {
                return _traitImage;
            }
            set
            {
                _traitImage = value;
            }
        }
        [Editor(typeof(MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Detailed description of trait with some stats and cost as well")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("tag of the prerequisite trait if there is one, else use 'none'")]
        public string prerequisiteTrait
        {
            get { return _prerequisiteTrait; }
            set { _prerequisiteTrait = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The modifier value used during skill checks if this trait is a skill type")]
        public int skillModifier
        {
            get { return _skillModifier; }
            set { _skillModifier = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The required minimum attribute value for a trait to become electable for learning; does not apply to automatically learned traits")]
        public int requiredStrength
        {
            get { return _requiredStrength; }
            set { _requiredStrength = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The required minimum attribute value for a trait to become electable for learning; does not apply to automatically learned traits")]
        public int requiredDexterity
        {
            get { return _requiredDexterity; }
            set { _requiredDexterity = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The required minimum attribute value for a trait to become electable for learning; does not apply to automatically learned traits")]
        public int requiredConstitution
        {
            get { return _requiredConstitution; }
            set { _requiredConstitution = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The required minimum attribute value for a trait to become electable for learning; does not apply to automatically learned traits")]
        public int requiredIntelligence
        {
            get { return _requiredIntelligence; }
            set { _requiredIntelligence = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The required minimum attribute value for a trait to become electable for learning; does not apply to automatically learned traits")]
        public int requiredWisdom
        {
            get { return _requiredWisdom; }
            set { _requiredWisdom = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The required minimum attribute value for a trait to become electable for learning; does not apply to automatically learned traits")]
        public int requiredCharisma
        {
            get { return _requiredCharisma; }
            set { _requiredCharisma = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("If the trait is a skill type trait, this is the attribute that is used for additional modifiers to skill check (str, dex, con, int, wis, cha)")]
        public string skillModifierAttribute
        {
            get { return _skillModifierAttribute; }
            set { _skillModifierAttribute = value; }
        }


        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the trait that will be removed once this trait is acquired")]
        public string traitToReplaceByTag
        {
            get { return _traitToReplaceByTag; }
            set { _traitToReplaceByTag = value; }
        }

        /*[CategoryAttribute("01 - Main"), DescriptionAttribute("When can this be used: Always means that it can be used in combat and on the main maps, Passive means that it is always on and doesn't need to be activated.")]
        public UsableInSituation UseableInSituation
        {
            get { return useableInSituation; }
            set { useableInSituation = value; }
        }*/
        [CategoryAttribute("01 - Main"), DescriptionAttribute("When can this be used: Always, OutOfCombat, InCombat. Non-usable traits are identified by not containing an assoicated spell tag.")]
        public string useableInSituation
        {
            get { return _useableInSituation; }
            set { _useableInSituation = value; }
        }


        //[Browsable(true), TypeConverter(typeof(SpriteConverter))]
        /*
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
        */
        /*
        //[Browsable(true), TypeConverter(typeof(SpriteConverter))]
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Sprite to use for the ending effect (Sprite Filename with extension)")]
        public string spriteEndingFilename
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
        */
        /*
        [CategoryAttribute("01 - Main"), DescriptionAttribute("How much SP this Trait costs")]
        public int costSP
        {
            get
            {
                return _costSP;
            }
            set
            {
                _costSP = value;
            }
        }
        */

        /*[CategoryAttribute("02 - Target"), DescriptionAttribute("The type of target for this spell")]
        public TargetType SpellTargetType
        {
            get
            {
                return spellTargetType;
            }
            set
            {
                spellTargetType = value;
            }
        }*/

        /*
        [CategoryAttribute("02 - Target"), DescriptionAttribute("The type of target for this trait")]
        public string traitTargetType
        {
            get
            {
                return _traitTargetType;
            }
            set
            {
                _traitTargetType = value;
            }
        }
        */

        /*[CategoryAttribute("03 - Effect"), DescriptionAttribute("damage = persistent, negative; heal = persistent, positive; buff = temporary, positive; debuff = temporary, negative")]
        public EffectType SpellEffectType
        {
            get
            {
                return spellEffectType;
            }
            set
            {
                spellEffectType = value;
            }
        }*/

        /*
        [CategoryAttribute("03 - Effect"), DescriptionAttribute("damage = persistent, negative; heal = persistent, positive; buff = temporary, positive; debuff = temporary, negative")]
        public string traitEffectType
        {
            get
            {
                return _traitEffectType;
            }
            set
            {
                _traitEffectType = value;
            }
        }
        */

        /*
        [CategoryAttribute("02 - Target"), DescriptionAttribute("the radius of the AoE")]
        public int aoeRadius
        {
            get
            {
                return _aoeRadius;
            }
            set
            {
                _aoeRadius = value;
            }
        }
        */
        /*
        [CategoryAttribute("02 - Target"), DescriptionAttribute("the range allowed to the center or beginning of the AoE")]
        public int range
        {
            get
            {
                return _range;
            }
            set
            {
                _range = value;
            }
        }
        */

        /*[CategoryAttribute("01 - Main"), DescriptionAttribute("the script to use for this Spell")]
        [Editor(typeof(ScriptSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public ScriptSelectEditorReturnObject SpellScript
        {
            get { return spellScript; }
            set { spellScript = value; }
        }*/
        /*
        [CategoryAttribute("01 - Main"), DescriptionAttribute("the script to use for this Spell")]
        //[Editor(typeof(ScriptSelectEditor), typeof(System.Drawing.Design.UITypeEditor))]
        
        public string traitScript
        {
            get { return _traitScript; }
            set { _traitScript = value; }
        }
        */
        #endregion

        public Trait()
        {            
        }
        public override string ToString()
        {
            return name;
        }
        public Trait ShallowCopy()
        {
            return (Trait)this.MemberwiseClone();
        }
        public Trait DeepCopy()
        {
            Trait other = (Trait)this.MemberwiseClone();
            other.traitEffectTagList = new List<EffectTagForDropDownList>();
            foreach (EffectTagForDropDownList s in this.traitEffectTagList)
            {
                other.traitEffectTagList.Add(s);
            }
            other.traitWorksNeverWhen = new List<LocalImmunityString>();
            foreach (LocalImmunityString s in this.traitWorksNeverWhen)
            {
                other.traitWorksNeverWhen.Add(s);
            }
            other.traitWorksOnlyWhen = new List<LocalImmunityString>();
            foreach (LocalImmunityString s in this.traitWorksOnlyWhen)
            {
                other.traitWorksOnlyWhen.Add(s);
            }
            return other;
        }
    }
}
