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
    

    public class Faction
    {
        #region Fields        

        private bool _showThisFactionInJournal = true;
        private string _factionDescriptionInJournal = "none";
        private string _name = "newFaction";
        private string _tag = "newFactionTag";

        private int _strength = 0;
        private bool _showStrengthInJournal = false;

        private int _rank = 1;//No
        private bool _showRankInJournal = false;
        private bool _displayRankInWords = false;

        private int _intervalOfFactionStrengthChangeInHours = 0;
        private int _amountOfFactionStrengthChangePerInterval = 0;
        private bool _showChangeRateInJournal = false;

        private int _timePassedInThisInterval = 0;//No

        private string _nameRank1 = "Rank 1";
        private string _nameRank2 = "Rank 2";
        private string _nameRank3 = "Rank 3";
        private string _nameRank4 = "Rank 4";
        private string _nameRank5 = "Rank 5";
        private string _nameRank6 = "Rank 6";
        private string _nameRank7 = "Rank 7";
        private string _nameRank8 = "Rank 8";
        private string _nameRank9 = "Rank 9";
        private string _nameRank10 = "Rank 10";

        private int _accumulatedBuffStrengthRank1 = 0;
        private int _accumulatedBuffStrengthRank2 = 1;
        private int _accumulatedBuffStrengthRank3 = 2;
        private int _accumulatedBuffStrengthRank4 = 3;
        private int _accumulatedBuffStrengthRank5 = 5;
        private int _accumulatedBuffStrengthRank6 = 7;
        private int _accumulatedBuffStrengthRank7 = 10;
        private int _accumulatedBuffStrengthRank8 = 13;
        private int _accumulatedBuffStrengthRank9 = 16;
        private int _accumulatedBuffStrengthRank10 = 20;

        private int _factionStrengthRequiredForRank2 = 100;
        private int _factionStrengthRequiredForRank3 = 100;
        private int _factionStrengthRequiredForRank4 = 100;
        private int _factionStrengthRequiredForRank5 = 100;
        private int _factionStrengthRequiredForRank6 = 100;
        private int _factionStrengthRequiredForRank7 = 100;
        private int _factionStrengthRequiredForRank8 = 100;
        private int _factionStrengthRequiredForRank9 = 100;
        private int _factionStrengthRequiredForRank10 = 100;

        #endregion

        #region Properties  
    
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Name of the Faction")]
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


        //private bool _showThisFactionInJournal = true;
        //private string _factionDescriptionInJournal = "none";
        [CategoryAttribute("01 - Main"), DescriptionAttribute("When true, this faction has an own entry section in the faction quest in the journal.")]
        public bool showThisFactionInJournal
        {
            get
            {
                return _showThisFactionInJournal;
            }
            set
            {
                _showThisFactionInJournal = value;
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("The free form descriptive text shown for this faction in its entry within the faction quest in the journal.")]
        public string factionDescriptionInJournal
        {
            get
            {
                return _factionDescriptionInJournal;
            }
            set
            {
                _factionDescriptionInJournal = value;
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the FAction (Must be unique)")]
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
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Strength of this Faction (used for min-max windows for spawning props as well as for determining Rank)")]
        public int strength
        {
            get
            {
                return _strength;
            }
            set
            {
                _strength = value;
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Strength of this Faction (used for min-max windows for spawning props as well as for determining Rank)")]
        public bool showStrengthInJournal
        {
            get
            {
                return _showStrengthInJournal;
            }
            set
            {
                _showStrengthInJournal = value;
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("When true, the Rank - if shown at all - is written in words (determined by rank name proeprties below) instead of pure numbers")]
        public bool displayRankInWords
        {
            get
            {
                return _displayRankInWords;
            }
            set
            {
                _displayRankInWords = value;
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("When true, the Rank of this Faction is shwon in Journal.")]
        public bool showRankInJournal
        {
            get
            {
                return _showRankInJournal;
            }
            set
            {
                _showRankInJournal = value;
            }
        }

       
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Number of hours needed (interval) for a faction strength change to occur.")]
        public int intervalOfFactionStrengthChangeInHours
        {
            get
            {
                return _intervalOfFactionStrengthChangeInHours;
            }
            set
            {
                _intervalOfFactionStrengthChangeInHours = value;
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Amount of faction chnage per interval (can be negative, too).")]
        public int amountOfFactionStrengthChangePerInterval
        {
            get
            {
                return _amountOfFactionStrengthChangePerInterval;
            }
            set
            {
                _amountOfFactionStrengthChangePerInterval = value;
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("When true, the amount of faction change per interval is shown in the Journal, like: +3 every 6 hours")]
        public bool showChangeRateInJournal
        {
            get
            {
                return _showChangeRateInJournal;
            }
            set
            {
                _showChangeRateInJournal = value;
            }
        }

        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Ingame name used for Rank 1 (eg very weak)")]
        public string nameRank1
        {
            get
            {
                return _nameRank1;
            }
            set
            {
                _nameRank1 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Ingame name used for Rank 2 (eg very weak)")]
        public string nameRank2
        {
            get
            {
                return _nameRank2;
            }
            set
            {
                _nameRank2 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Ingame name used for Rank 3 (eg very weak)")]
        public string nameRank3
        {
            get
            {
                return _nameRank3;
            }
            set
            {
                _nameRank3 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Ingame name used for Rank 4 (eg very weak)")]
        public string nameRank4
        {
            get
            {
                return _nameRank4;
            }
            set
            {
                _nameRank4 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Ingame name used for Rank 5 (eg very weak)")]
        public string nameRank5
        {
            get
            {
                return _nameRank5;
            }
            set
            {
                _nameRank5 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Ingame name used for Rank 6 (eg very weak)")]
        public string nameRank6
        {
            get
            {
                return _nameRank6;
            }
            set
            {
                _nameRank6 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Ingame name used for Rank 7 (eg very weak)")]
        public string nameRank7
        {
            get
            {
                return _nameRank7;
            }
            set
            {
                _nameRank7 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Ingame name used for Rank 8 (eg very weak)")]
        public string nameRank8
        {
            get
            {
                return _nameRank8;
            }
            set
            {
                _nameRank8 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Ingame name used for Rank 9 (eg very weak)")]
        public string nameRank9
        {
            get
            {
                return _nameRank9;
            }
            set
            {
                _nameRank9 = value;
            }
        }

        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Ingame name used for Rank 10 (eg very weak)")]
        public string nameRank10
        {
            get
            {
                return _nameRank10;
            }
            set
            {
                _nameRank10 = value;
            }
        }
      
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of buff (debuff if negative) granted to AC, toHit and all saves for creatures who are members of this Faction. The buff/ebuff is the whole total bonus at this Rank (prior ranks are are not added together).")]
        public int accumulatedBuffStrengthRank1
        {
            get
            {
                return _accumulatedBuffStrengthRank1;
            }
            set
            {
                _accumulatedBuffStrengthRank1 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of buff (debuff if negative) granted to AC, toHit and all saves for creatures who are members of this Faction. The buff/ebuff is the whole total bonus at this Rank (prior ranks are are not added together).")]
        public int accumulatedBuffStrengthRank2
        {
            get
            {
                return _accumulatedBuffStrengthRank2;
            }
            set
            {
                _accumulatedBuffStrengthRank2 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of buff (debuff if negative) granted to AC, toHit and all saves for creatures who are members of this Faction. The buff/ebuff is the whole total bonus at this Rank (prior ranks are are not added together).")]
        public int accumulatedBuffStrengthRank3
        {
            get
            {
                return _accumulatedBuffStrengthRank3;
            }
            set
            {
                _accumulatedBuffStrengthRank3 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of buff (debuff if negative) granted to AC, toHit and all saves for creatures who are members of this Faction. The buff/ebuff is the whole total bonus at this Rank (prior ranks are are not added together).")]
        public int accumulatedBuffStrengthRank4
        {
            get
            {
                return _accumulatedBuffStrengthRank4;
            }
            set
            {
                _accumulatedBuffStrengthRank4 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of buff (debuff if negative) granted to AC, toHit and all saves for creatures who are members of this Faction. The buff/ebuff is the whole total bonus at this Rank (prior ranks are are not added together).")]
        public int accumulatedBuffStrengthRank5
        {
            get
            {
                return _accumulatedBuffStrengthRank5;
            }
            set
            {
                _accumulatedBuffStrengthRank5 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of buff (debuff if negative) granted to AC, toHit and all saves for creatures who are members of this Faction. The buff/ebuff is the whole total bonus at this Rank (prior ranks are are not added together).")]
        public int accumulatedBuffStrengthRank6
        {
            get
            {
                return _accumulatedBuffStrengthRank6;
            }
            set
            {
                _accumulatedBuffStrengthRank6 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of buff (debuff if negative) granted to AC, toHit and all saves for creatures who are members of this Faction. The buff/ebuff is the whole total bonus at this Rank (prior ranks are are not added together).")]
        public int accumulatedBuffStrengthRank7
        {
            get
            {
                return _accumulatedBuffStrengthRank7;
            }
            set
            {
                _accumulatedBuffStrengthRank7 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of buff (debuff if negative) granted to AC, toHit and all saves for creatures who are members of this Faction. The buff/ebuff is the whole total bonus at this Rank (prior ranks are are not added together).")]
        public int accumulatedBuffStrengthRank8
        {
            get
            {
                return _accumulatedBuffStrengthRank8;
            }
            set
            {
                _accumulatedBuffStrengthRank8 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of buff (debuff if negative) granted to AC, toHit and all saves for creatures who are members of this Faction. The buff/ebuff is the whole total bonus at this Rank (prior ranks are are not added together).")]
        public int accumulatedBuffStrengthRank9
        {
            get
            {
                return _accumulatedBuffStrengthRank9;
            }
            set
            {
                _accumulatedBuffStrengthRank9 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of buff (debuff if negative) granted to AC, toHit and all saves for creatures who are members of this Faction. The buff/ebuff is the whole total bonus at this Rank (prior ranks are are not added together).")]
        public int accumulatedBuffStrengthRank10
        {
            get
            {
                return _accumulatedBuffStrengthRank10;
            }
            set
            {
                _accumulatedBuffStrengthRank10 = value;
            }
        }

        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of Faction strength needed for Rank 2.")]
        public int factionStrengthRequiredForRank2
        {
            get
            {
                return _factionStrengthRequiredForRank2;
            }
            set
            {
                _factionStrengthRequiredForRank2 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of Faction strength needed for Rank 3.")]
        public int factionStrengthRequiredForRank3
        {
            get
            {
                return _factionStrengthRequiredForRank3;
            }
            set
            {
                _factionStrengthRequiredForRank3 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of Faction strength needed for Rank 4.")]
        public int factionStrengthRequiredForRank4
        {
            get
            {
                return _factionStrengthRequiredForRank4;
            }
            set
            {
                _factionStrengthRequiredForRank4 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of Faction strength needed for Rank 5.")]
        public int factionStrengthRequiredForRank5
        {
            get
            {
                return _factionStrengthRequiredForRank5;
            }
            set
            {
                _factionStrengthRequiredForRank5 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of Faction strength needed for Rank 6.")]
        public int factionStrengthRequiredForRank6
        {
            get
            {
                return _factionStrengthRequiredForRank6;
            }
            set
            {
                _factionStrengthRequiredForRank6 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of Faction strength needed for Rank 7.")]
        public int factionStrengthRequiredForRank7
        {
            get
            {
                return _factionStrengthRequiredForRank7;
            }
            set
            {
                _factionStrengthRequiredForRank7 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of Faction strength needed for Rank 8.")]
        public int factionStrengthRequiredForRank8
        {
            get
            {
                return _factionStrengthRequiredForRank8;
            }
            set
            {
                _factionStrengthRequiredForRank8 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of Faction strength needed for Rank 9.")]
        public int factionStrengthRequiredForRank9
        {
            get
            {
                return _factionStrengthRequiredForRank9;
            }
            set
            {
                _factionStrengthRequiredForRank9 = value;
            }
        }
        [CategoryAttribute("02 - The Ranks"), DescriptionAttribute("Amount of Faction strength needed for Rank 10.")]
        public int factionStrengthRequiredForRank10
        {
            get
            {
                return _factionStrengthRequiredForRank10;
            }
            set
            {
                _factionStrengthRequiredForRank10 = value;
            }
        }

        #endregion

        public Faction()
        {            
        }
        public override string ToString()
        {
            return name;
        }
        public Faction ShallowCopy()
        {
            return (Faction)this.MemberwiseClone();
        }
        public Faction DeepCopy()
        {
            Faction other = (Faction)this.MemberwiseClone();
            return other;
        }
    }
}
