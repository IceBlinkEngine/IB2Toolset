using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IB2miniToolset
{
    public class EffectTagForDropDownList
    {
        private string _tag = "none"; //item unique tag name

        [Browsable(true), TypeConverter(typeof(EffectTagTypeConverter))]
        [CategoryAttribute("01 - Effect Tag"), DescriptionAttribute("Effect Tag that will be removed from the target when this spell is used (used for dispell magic, free action, neutralize poison, etc.)")]
        public string tag
        {
            get { return _tag; }
            set { _tag = value; }
        }
        public EffectTagForDropDownList()
        {

        }
        public override string ToString()
        {
            return tag;
        }
    }
}
