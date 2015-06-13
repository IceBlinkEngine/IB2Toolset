using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IB2Toolset
{
    public class BarkString
    {
        private string _FloatyTextOneLiner = "";
        private int _ChanceToShow = 10;
        private string _Color = "white";
        private int _LengthOfTimeToShowInMilliSeconds = 4000;
 
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The one-liner of text to show")]
        public string FloatyTextOneLiner
        {
            get { return _FloatyTextOneLiner; }
            set { _FloatyTextOneLiner = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The percent chance for the one-liner to show (0-100), 0 = never, 100 = always")]
        public int ChanceToShow
        {
            get { return _ChanceToShow; }
            set { _ChanceToShow = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("color of the floaty text (options: white, red, green, blue, yellow...must be all lowercase just as shown)")]
        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("The length of time that the text will stay on the screen in milliseconds")]
        public int LengthOfTimeToShowInMilliSeconds
        {
            get { return _LengthOfTimeToShowInMilliSeconds; }
            set { _LengthOfTimeToShowInMilliSeconds = value; }
        }

        public BarkString()
        {

        }
        public BarkString(string text, int chance, int showTimeInMs)
        {
            FloatyTextOneLiner = text;
            ChanceToShow = chance;
            LengthOfTimeToShowInMilliSeconds = showTimeInMs;
        }
        public BarkString DeepCopy()
        {
            BarkString other = (BarkString)this.MemberwiseClone();            
            return other;
        }
    }
}
