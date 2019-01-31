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
    /*public class PlayerClasses
    {
        public List<PlayerClass> playerClassList = new List<PlayerClass>();

        public PlayerClasses()
        {
        }
        public void savePlayerClassesFile(string filename)
        {
            string json = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            using (StreamWriter sw = new StreamWriter(filename))
            {
                sw.Write(json.ToString());
            }
        }
        public PlayerClasses loadPlayerClassesFile(string filename)
        {
            PlayerClasses toReturn = null;

            // deserialize JSON directly from a file
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                toReturn = (PlayerClasses)serializer.Deserialize(file, typeof(PlayerClasses));
            }
            return toReturn;
        }
        public PlayerClass getPlayerClassByTag(string tag)
        {
            foreach (PlayerClass ts in playerClassList)
            {
                if (ts.tag == tag) return ts;
            }
            return null;
        }
    }*/

    public class PlayerClass
    {        
        #region Fields        
        private string _name = "newClass"; //item name
        private string _tag = "newClassTag"; //item unique tag name
        private bool usableByPlayer = true;
        private string _description = "";
        private int _startingHP = 10;
        private int _startingSP = 20;
        private int _hpPerLevelUp = 10;
        private int _spPerLevelUp = 20;
        private int _hpRegenTimeNeeded = 0;
        private int _spRegenTimeNeeded = 0;
        private int[] _baseFortitudeAtLevel = new int[] { 0, 2, 3, 3, 4, 4, 5, 5, 6 };
        private int[] _baseWillAtLevel = new int[] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
        private int[] _baseReflexAtLevel = new int[] { 0, 0, 0, 1, 1, 1, 2, 2, 2 };
        private int[] _babTable = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        private int[] _xpTable = new int[] { 0, 200, 400, 800, 1600, 3200, 6500, 12500, 25000, 50000 };
        private List<ItemRefs> _itemsAllowed = new List<ItemRefs>();
        //private SortableBindingList<TraitAllowed> _traitsAllowed = new SortableBindingList<TraitAllowed>();
        //private SortableBindingList<SpellAllowed> _spellsAllowed = new SortableBindingList<SpellAllowed>();
        private SortableBindingList<TraitAllowed> _traitsAllowed = new SortableBindingList<TraitAllowed>();
        private SortableBindingList<SpellAllowed> _spellsAllowed = new SortableBindingList<SpellAllowed>();
        private string _labelForCastAction = "casts";
        //private string _labelForTheSpellsButtonInCombat = "SPELL";
        private string _labelForUseTraitAction = "uses";
        //private string _labelForTheUseTraitButtonInCombat = "TRAIT";
        private string _modifierFromSPRelevantAttribute = "intelligence";
        //sort later which type of spell labeling to use
        private string _spellLabelSingular = "Spell";  
        private string _spellLabelPlural = "Spells";
        private string _traitLabelSingular = "Trait";
        private string _traitLabelPlural = "Traits";
        private int[] _traitsToLearnAtLevelTable = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 };  
        private int[] _spellsToLearnAtLevelTable = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 };


        #endregion

        #region Properties
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Name of the PlayerClass")]
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
        [CategoryAttribute("04 - Labels"), DescriptionAttribute("Label for the cast action in log, third person singular")]
        public string labelForCastAction
        {
            get { return _labelForCastAction; }
            set { _labelForCastAction = value; }
        }
        /*
        [CategoryAttribute("01 - Main"), DescriptionAttribute("label for the cast button in combat")]
        public string labelForTheSpellsButtonInCombat
        {
            get { return _labelForTheSpellsButtonInCombat; }
            set { _labelForTheSpellsButtonInCombat = value; }
        }
        */
        [CategoryAttribute("04 - Labels"), DescriptionAttribute("Label for the use trait action in log, third person singular")]
        public string labelForUseTraitAction
        {
            get { return _labelForUseTraitAction; }
            set { _labelForUseTraitAction = value; }
        }
        /*
        [CategoryAttribute("01 - Main"), DescriptionAttribute("label for the use trait button in combat")]
        public string labelForTheUseTraitButtonInCombat
        {
            get { return _labelForTheUseTraitButtonInCombat; }
            set { _labelForTheUseTraitButtonInCombat = value; }
        }
        */
        [CategoryAttribute("01 - Main"), DescriptionAttribute("name of attribute taht is used for sp calculation for this class, eg intelligence or wisdom")]
        public string modifierFromSPRelevantAttribute
        {
            get { return _modifierFromSPRelevantAttribute; }
            set { _modifierFromSPRelevantAttribute = value; }
        }

        [CategoryAttribute("04 - Labels"), DescriptionAttribute("Label used for Spell singular form on cast screens (ex. Spell, Power, etc.)")]  
        public string spellLabelSingular  
        {  
             get { return _spellLabelSingular; }  
             set { _spellLabelSingular = value; }  
        }
          
        [CategoryAttribute("04 - Labels"), DescriptionAttribute("Label used for Spell plural form on cast screens (ex. Spells, Powers, etc.)")]  
        public string spellLabelPlural
        {  
            get { return _spellLabelPlural; }
            set { _spellLabelPlural = value; }  
        }

        [CategoryAttribute("04 - Labels"), DescriptionAttribute("Label used for Trait singular form on use trait screens")]
        public string traitLabelSingular
        {
            get { return _traitLabelSingular; }
            set { _traitLabelSingular = value; }
        }

        [CategoryAttribute("04 - Labels"), DescriptionAttribute("Label used for Trait plural form on use trait screens")]
        public string traitLabelPlural
        {
            get { return _traitLabelPlural; }
            set { _traitLabelPlural = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the PlayerClass (Must be unique)")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("This class can be selected and used by players?")]
        public bool UsableByPlayer
        {
            get
            {
                return usableByPlayer;
            }
            set
            {
                usableByPlayer = value;
            }
        }
        [Editor(typeof(MultilineStringEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Detailed description of class")]
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
        [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("HP received at the first level")]
        public int startingHP
        {
            get
            {
                return _startingHP;
            }
            set
            {
                _startingHP = value;
            }
        }
        [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("SP received at the first level")]
        public int startingSP
        {
            get
            {
                return _startingSP;
            }
            set
            {
                _startingSP = value;
            }
        }
        [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("HP received at each level up after level one")]
        public int hpPerLevelUp
        {
            get
            {
                return _hpPerLevelUp;
            }
            set
            {
                _hpPerLevelUp = value;
            }
        }
        [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("SP received at each level up after level one")]
        public int spPerLevelUp
        {
            get
            {
                return _spPerLevelUp;
            }
            set
            {
                _spPerLevelUp = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("How many seconds need to pass by before regenerating 1 HP. Setting this value to '0' will turn off any HP regeneration over time and only potions, rest scripts, etc. will replenish HP.")]
        public int hpRegenTimeNeeded
        {
            get
            {
                return _hpRegenTimeNeeded;
            }
            set
            {
                _hpRegenTimeNeeded = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("How many seconds need to pass by before regenerating 1 SP. Setting this value to '0' will turn off any SP regeneration over time and only potions, rest scripts, etc. will replenish SP.")]
        public int spRegenTimeNeeded
        {
            get
            {
                return _spRegenTimeNeeded;
            }
            set
            {
                _spRegenTimeNeeded = value;
            }
        }
        [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("BaseFortitude Table")]
        public int[] baseFortitudeAtLevel
        {
            get
            {
                return _baseFortitudeAtLevel;
            }
            set
            {
                _baseFortitudeAtLevel = value;
            }
        }
        [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("BaseWill Table")]
        public int[] baseWillAtLevel
        {
            get
            {
                return _baseWillAtLevel;
            }
            set
            {
                _baseWillAtLevel = value;
            }
        }
        [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("BaseReflex Table")]
        public int[] baseReflexAtLevel
        {
            get
            {
                return _baseReflexAtLevel;
            }
            set
            {
                _baseReflexAtLevel = value;
            }
        }
        [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("Base Attack Bonus at Level Table")]
        public int[] babTable
        {
            get { return _babTable; }
            set { _babTable = value; }
        }
        [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("XP Table")]
        public int[] xpTable
        {
            get
            {
                return _xpTable;
            }
            set
            {
                _xpTable = value;
            }
        }

        [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("define how many spells can be learned at each level...depends on if there are any available to learn.")]  
        public int[] spellsToLearnAtLevelTable  
        {  
             get  
             {  
                 return _spellsToLearnAtLevelTable;  
             }  
             set  
             {  
                 _spellsToLearnAtLevelTable = value;  
             }  
         }  
   
         [CategoryAttribute("02 - Stat Development"), DescriptionAttribute("define how many traits can be learned at each level...depends on if there are any available to learn.")]  
         public int[] traitsToLearnAtLevelTable
         {  
             get  
             {  
                 return _traitsToLearnAtLevelTable;  
             }  
             set  
             {  
                 _traitsToLearnAtLevelTable = value;  
             }  
         }  

             
        [CategoryAttribute("03 - Allowed"), DescriptionAttribute("A list of items that the class can use"), Browsable(false), ReadOnly(true)]
        public List<ItemRefs> itemsAllowed
        {
            get
            {
                return _itemsAllowed;
            }
            set
            {
                _itemsAllowed = value;
            }
        }
        [CategoryAttribute("03 - Allowed"), DescriptionAttribute("A list of traits that the class can use"), Browsable(false), ReadOnly(true)]
        public SortableBindingList<TraitAllowed> traitsAllowed
        {
            get
            {
                return _traitsAllowed;
            }
            set
            {
                _traitsAllowed = value;
            }
        }
        [CategoryAttribute("03 - Allowed"), DescriptionAttribute("A list of spells that the class can use"), Browsable(false), ReadOnly(true)]
        public SortableBindingList<SpellAllowed> spellsAllowed
        {
            get
            {
                return _spellsAllowed;
            }
            set
            {
                _spellsAllowed = value;
            }
        }
        #endregion

        public PlayerClass()
        {
        }        
        public override string ToString()
        {
            return name;
        }
        public bool containsItemRefsWithResRef(String resref)
	    {
		    foreach (ItemRefs i in this.itemsAllowed)
		    {
			    if (i.resref == resref) { return true; }
		    }
		    return false;
	    }
        public TraitAllowed getTraitAllowedByTag(string tag)
        {
            foreach (TraitAllowed ta in this.traitsAllowed)
            {
                if (ta.tag == tag) return ta;
            }
            return null;
        }
        public SpellAllowed getSpellAllowedByTag(string tag)
        {
            foreach (SpellAllowed sa in this.spellsAllowed)
            {
                if (sa.tag == tag) return sa;
            }
            return null;
        }
        public PlayerClass ShallowCopy()
        {
            return (PlayerClass)this.MemberwiseClone();
        }
        public PlayerClass DeepCopy()
        {
            PlayerClass other = (PlayerClass)this.MemberwiseClone();
            other.itemsAllowed = new List<ItemRefs>();
            foreach (ItemRefs s in this.itemsAllowed)
            {
                other.itemsAllowed.Add(s.DeepCopy());
            }
            other.traitsAllowed = new SortableBindingList<TraitAllowed>();
            foreach (TraitAllowed s in this.traitsAllowed)
            {
                TraitAllowed sa = s.DeepCopy();
                other.traitsAllowed.Add(sa);
            }
            other.spellsAllowed = new SortableBindingList<SpellAllowed>();
            foreach (SpellAllowed s in this.spellsAllowed)
            {
                SpellAllowed sa = s.DeepCopy();
                other.spellsAllowed.Add(sa);
            }
            return other;
        }
    }

    public class TraitAllowed : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _name = "";
        private string _tag = "";
        private int _atWhatLevelIsAvailable = 0;
        private bool _automaticallyLearned = false;
        private bool _needsSpecificTrainingToLearn = false;
        private bool _allow = false;
        
        //InCombat, OutOfCombat, Always, Passive
        //Passive flags a trait that cannot be used like a spell, ie is not active
        private string _useableInSituation = "Passive";
        
        //contains tag of one spell that is cast when the trait is used (inside and/or outside combat, see below)
        private string _associatedSpellTag = "none";

        //add more work for these below!! to do
        private List<EffectTagForDropDownList> _traitEffectTagList = new List<EffectTagForDropDownList>();
        private List<LocalImmunityString> _traitWorksOnlyWhen = new List<LocalImmunityString>();
        private List<LocalImmunityString> _traitWorksNeverWhen = new List<LocalImmunityString>();

        public string name
        {
            get { return _name; }
            set
            {
                _name = value;
                this.NotifyPropertyChanged("name");
            }
        }

        public string tag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                this.NotifyPropertyChanged("tag");
            }
        }

        public string useableinsituation
        {
            get { return _useableInSituation; }
            set
            {
                _useableInSituation = value;
                this.NotifyPropertyChanged("useableinsituation");
            }
        }

        public string associatedSpellTag
        {
            get { return _associatedSpellTag; }
            set
            {
                _associatedSpellTag = value;
                this.NotifyPropertyChanged("associatedSpellTag");
            }
        }

        public int atWhatLevelIsAvailable
        {
            get { return _atWhatLevelIsAvailable; }
            set
            {
                _atWhatLevelIsAvailable = value;
                this.NotifyPropertyChanged("atWhatLevelIsAvailable");
            }
        }
        public bool automaticallyLearned
        {
            get { return _automaticallyLearned; }
            set
            {
                _automaticallyLearned = value;
                this.NotifyPropertyChanged("automaticallyLearned");
            }
        }
        public bool needsSpecificTrainingToLearn
        {
            get { return _needsSpecificTrainingToLearn; }
            set
            {
                _needsSpecificTrainingToLearn = value;
                this.NotifyPropertyChanged("needsSpecificTrainingToLearn");
            }
        }
        public bool allow
        {
            get { return _allow; }
            set
            {
                _allow = value;
                this.NotifyPropertyChanged("allow");
            }
        }
        /*
        public List<LocalImmunityString> traitWorksNeverWhen
        {
            get { return _traitWorksNeverWhen; }
            set
            {
                _traitWorksNeverWhen = value;
                this.NotifyPropertyChanged("traitWorksNeverWhen");
            }
        }

        public List<LocalImmunityString> traitWorksOnlyWhen
        {
            get { return _traitWorksOnlyWhen; }
            set
            {
                _traitWorksOnlyWhen = value;
                this.NotifyPropertyChanged("traitWorksOnlyWhen");
            }
        }

        public List<EffectTagForDropDownList> traitEffectTagList
        {
            get { return _traitEffectTagList; }
            set
            {
               _ traitEffectTagList = value;
                this.NotifyPropertyChanged("traitEffectTagList");
            }
        }
        */

        public TraitAllowed()
        {
        }
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public TraitAllowed DeepCopy()
        {
            TraitAllowed other = (TraitAllowed)this.MemberwiseClone();
            /*
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
            */

            return other;
        }
    }
        
    public class SpellAllowed : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _name = "";
        private string _tag = "";
        private int _atWhatLevelIsAvailable = 0;
        private bool _automaticallyLearned = false;
        private bool _allow = false;
        private bool _needsSpecificTrainingToLearn = false;

        public string name
        {
            get 
            {
                try
                {
                    return _name;
                }
                catch (Exception ex)
                {
                    int x = 0;
                }
                return _name;
            }
            set
            {
                _name = value;
                this.NotifyPropertyChanged("name");
            }
        }

        public bool needsSpecificTrainingToLearn
        {
            get { return _needsSpecificTrainingToLearn; }
            set
            {
                _needsSpecificTrainingToLearn = value;
                this.NotifyPropertyChanged("needsSpecificTrainingToLearn");
            }
        }

        public string tag
        {
            get { return _tag; }
            set
            {
                _tag = value;
                this.NotifyPropertyChanged("tag");
            }
        }
        public int atWhatLevelIsAvailable
        {
            get { return _atWhatLevelIsAvailable; }
            set
            {
                _atWhatLevelIsAvailable = value;
                this.NotifyPropertyChanged("atWhatLevelIsAvailable");
            }
        }
        public bool automaticallyLearned
        {
            get { return _automaticallyLearned; }
            set
            {
                _automaticallyLearned = value;
                this.NotifyPropertyChanged("AutomaticallyLearned");
            }
        }
        public bool allow
        {
            get { return _allow; }
            set
            {
                _allow = value;
                this.NotifyPropertyChanged("allow");
            }
        }

        public SpellAllowed()
        {
        }
        private void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        public SpellAllowed DeepCopy()
        {
            SpellAllowed other = (SpellAllowed)this.MemberwiseClone();
            return other;
        }
    }
}
