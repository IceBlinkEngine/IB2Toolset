using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class WeatherTypeListItem
    {
        //public string tag = "newWeatherTypeListItemTag";
        private string _tag = "newWeatherTypeListItemTag"; //unique tag name
        //public string name = "newWeatherTypeListItemName";
        private string _name = "change weather effect above instead"; //name in toolset
        private string _combinedInfo = "change values in 01 instead"; //name in toolset
        private int _chance = 0; //chance
        private int _duration = 50; //duration
        //private string _weatherEffectTag = "none";
        private string _weatherEffectName = "none";
        //public string weatherEffectName = "none";
        //public string weatherEffectTag = "none";

        /*
        [Browsable(true), TypeConverter(typeof(WeatherEffectsTagTypeConverter))]
        [CategoryAttribute("01 - Main"), DescriptionAttribute("the tag of weather effect to use for this weather type list item")]
        public string weatherEffectTag
        {
            get { return _weatherEffectTag; }
            set { _weatherEffectTag = value; }
        }
        */

        
                [Browsable(true), TypeConverter(typeof(WeatherEffectsNameTypeConverter))]
                [CategoryAttribute("01 - Main"), DescriptionAttribute("the name of weather effect to use for this weather type list item")]
                public string weatherEffectName
                {
                    get { return _weatherEffectName; }
                    set { _weatherEffectName = value; }
                }
               
        //XXXXXXXXXXXXXXXXX
        //gonna use above combo box element
        /*private List<FullScreenEffectLayer> _WeatherLayers = new List<FullScreenEffectLayer>();

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Weather system")]
        public List<FullScreenEffectLayer> WeatherLayers
        {
            get { return _WeatherLayers; }
            set { _WeatherLayers = value; }
        }
        */
        
        [CategoryAttribute("02 - Do not change"), DescriptionAttribute("Unique tag of the weatherListItem")]
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
        
        
        [CategoryAttribute("02 - Do not change"), DescriptionAttribute("Is automatically adjusted to chosen weather effect")]
        public string name
        {
            get
            {
                _name = weatherEffectName;
                return _name;
            }
            set
            {
                _name = weatherEffectName;
            }
        }
        

        [CategoryAttribute("02 - Do not change"), DescriptionAttribute("Is automatically adjusted to chosen weather effect, chance and duration")]
        public string combinedInfo
        {
            get
            {
                _combinedInfo = weatherEffectName + " (" + chance.ToString() + "%) (" + duration.ToString() + " ticks)";
                return _combinedInfo;
            }
            set
            {
                _combinedInfo = weatherEffectName + " (chance: " + chance.ToString() +")";
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("chance of the weather type list item to be called in its weather types list")]
        public int chance
        {
            get
            {
                return _chance;
            }
            set
            {
                _chance = value;
            }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("duration of the weather type list item")]
        public int duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
            }
        }

        public WeatherTypeListItem()
        {
            //for (int i = 0; i < 10; i++)
            //{
                //FullScreenEffectLayer newLayer = new FullScreenEffectLayer();
                //WeatherLayers.Add(newLayer);
            //}
        }

        public WeatherTypeListItem DeepCopy()
        {
            //WeatherTypeListItem other = (WeatherTypeListItem)this.MemberwiseClone();
            //WeatherTypeListItem other = (WeatherTypeListItem)this.Clone();
            WeatherTypeListItem other = new WeatherTypeListItem();
            other._name = this._name;
            other.name = this.name;
            other._tag = this._tag;
            other.tag = this.tag;
            other._chance = this._chance;
            other.chance = this.chance;
            other._weatherEffectName = this._weatherEffectName;
            other.weatherEffectName = this.weatherEffectName;

            return other;
        }
    }
}
