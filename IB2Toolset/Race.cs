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
    public class Race
    {        
        #region Fields        
        private string _name = "newRace"; //item name
        private string _tag = "newRaceTag"; //item unique tag name
        private bool usableByPlayer = true;
        private string _description = "";
        private int _strMod = 0;
        private int _dexMod = 0;
        private int _intMod = 0;
        private int _chaMod = 0;
        private int _conMod = 0;
        private int _wisMod = 0; 
        private int _damageTypeResistanceValueAcid = 0;
        private int _damageTypeResistanceValueNormal = 0;
        private int _damageTypeResistanceValueCold = 0;
        private int _damageTypeResistanceValueElectricity = 0;
        private int _damageTypeResistanceValueFire = 0;
        private int _damageTypeResistanceValueMagic = 0;
        private int _damageTypeResistanceValuePoison = 0;
        private int moveDistanceLightArmor = 6;
        private int moveDistanceMediumHeavyArmor = 4;
        private List<string> _classesAllowed = new List<string>();
        private SortableBindingList<TraitAllowed> _traitsAllowed = new SortableBindingList<TraitAllowed>();
        //private SortableBindingList<TraitAllowed> traitsAllowed = new SortableBindingList<TraitAllowed>();
        #endregion

        #region Properties        
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Name of the Race")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the Race (Must be unique)")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("This race can be selected and used by players?")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Detailed description of race")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("How far this race can move in one turn of combat with no armor or light armor (measured in squares)")]
        public int MoveDistanceLightArmor
        {
            get
            {
                return moveDistanceLightArmor;
            }
            set
            {
                moveDistanceLightArmor = value;
            }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("How far this race can move in one turn of combat with medium or heavy armor (measured in squares)")]
        public int MoveDistanceMediumHeavyArmor
        {
            get
            {
                return moveDistanceMediumHeavyArmor;
            }
            set
            {
                moveDistanceMediumHeavyArmor = value;
            }
        }
        [CategoryAttribute("02 - Attribute Modifiers"), DescriptionAttribute("modifier to the PC's base attribute")]
        public int strMod
        {
            get
            {
                return _strMod;
            }
            set
            {
                _strMod = value;
            }
        }
        [CategoryAttribute("02 - Attribute Modifiers"), DescriptionAttribute("modifier to the PC's base attribute")]
        public int dexMod
        {
            get
            {
                return _dexMod;
            }
            set
            {
                _dexMod = value;
            }
        }
        [CategoryAttribute("02 - Attribute Modifiers"), DescriptionAttribute("modifier to the PC's base attribute")]
        public int intMod
        {
            get
            {
                return _intMod;
            }
            set
            {
                _intMod = value;
            }
        }
        [CategoryAttribute("02 - Attribute Modifiers"), DescriptionAttribute("modifier to the PC's base attribute")]
        public int chaMod
        {
            get
            {
                return _chaMod;
            }
            set
            {
                _chaMod = value;
            }
        }
        [CategoryAttribute("02 - Attribute Modifiers"), DescriptionAttribute("modifier to the PC's base attribute")]
        public int conMod
        {
            get
            {
                return _conMod;
            }
            set
            {
                _conMod = value;
            }
        }
        [CategoryAttribute("02 - Attribute Modifiers"), DescriptionAttribute("modifier to the PC's base attribute")]
        public int wisMod
        {
            get
            {
                return _wisMod;
            }
            set
            {
                _wisMod = value;
            }
        }
        [CategoryAttribute("00 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValuePoison
        {
            get { return _damageTypeResistanceValuePoison; }
            set { _damageTypeResistanceValuePoison = value; }
        }
        [CategoryAttribute("00 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueMagic
        {
            get { return _damageTypeResistanceValueMagic; }
            set { _damageTypeResistanceValueMagic = value; }
        }
        [CategoryAttribute("00 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueNormal
        {
            get { return _damageTypeResistanceValueNormal; }
            set { _damageTypeResistanceValueNormal = value; }
        }
        [CategoryAttribute("00 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueAcid
        {
            get { return _damageTypeResistanceValueAcid; }
            set { _damageTypeResistanceValueAcid = value; }
        }
        [CategoryAttribute("00 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueCold
        {
            get { return _damageTypeResistanceValueCold; }
            set { _damageTypeResistanceValueCold = value; }
        }
        [CategoryAttribute("00 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueElectricity
        {
            get { return _damageTypeResistanceValueElectricity; }
            set { _damageTypeResistanceValueElectricity = value; }
        }
        [CategoryAttribute("00 - Resistance Modifiers"), DescriptionAttribute("Damage resistance value (default is to use -100 to 100 as a percentage of immunity, damage multiplied by percentage so 0 = full damage, 100 = no damage, -100 = double damage)")]
        public int damageTypeResistanceValueFire
        {
            get { return _damageTypeResistanceValueFire; }
            set { _damageTypeResistanceValueFire = value; }
        }
        [CategoryAttribute("03 - Restrictions"), DescriptionAttribute("A list of classes that the race can learn"), Browsable(false)]
        public List<string> classesAllowed
        {
            get
            {
                return _classesAllowed;
            }
            set
            {
                _classesAllowed = value;
            }
        }
        [CategoryAttribute("04 - Racial Traits"), DescriptionAttribute("A list of traits that the class can use"), Browsable(false), ReadOnly(true)]
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
        /*[CategoryAttribute("04 - Racial Traits"), DescriptionAttribute("A list of racial traits"), Browsable(false), ReadOnly(true)]
        public SortableBindingList<TraitAllowed> TraitsAllowed
        {
            get
            {
                return traitsAllowed;
            }
            set
            {
                traitsAllowed = value;
            }
        }*/
        #endregion

        public Race()
        {            
        }
        public override string ToString()
        {
            return name;
        }
        public Race ShallowCopy()
        {
            return (Race)this.MemberwiseClone();
        }
        public Race DeepCopy()
        {
            Race other = (Race)this.MemberwiseClone();
            other.classesAllowed = new List<string>();
            foreach (string s in this.classesAllowed)
            {
                other.classesAllowed.Add(s);
            }
            /*other.traitsAllowed = new SortableBindingList<TraitAllowed>();
            foreach (TraitAllowed s in this.traitsAllowed)
            {
                TraitAllowed sa = s.DeepCopy();
                other.traitsAllowed.Add(sa);
            }*/
            return other;
        }
    }
}
