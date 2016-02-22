using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class WeatherTypeList
    {
        private string _tag = "newWeatherTypeListTag";
        private string _name = "newWeatherTypeListName"; 
        public List<WeatherTypeListItem> weatherTypeListItems = new List<WeatherTypeListItem>();

        //likely better allow adding via lbx 
        /*
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Weather system")]
        public List<WeatherTypeListItem> weatherTypeListItems
        {
            get { return _weatherTypeListItems; }
            set { _weatherTypeListItems = value; }
        }
        */

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the WeatherTypeList (Must be unique)")]
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

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Name of the WeatherTypeList")]
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

        public WeatherTypeList()
        {
            /*
            if (_WeatherLayers.Count < 10)
            {
                for (int i = 0; i < 10; i++)
                {
                    FullScreenEffectLayer newLayer = new FullScreenEffectLayer();
                    WeatherLayers.Add(newLayer);
                }
            }
            */
        }

        public WeatherTypeList DeepCopy()
        {
            //WeatherTypeList other = (WeatherTypeList)this.MemberwiseClone();
            WeatherTypeList other = new WeatherTypeList();
            other._name = this._name;
            other.name = this.name;
            other._tag = this._tag;
            other.tag = this.tag;
            foreach (WeatherTypeListItem wtli in this.weatherTypeListItems)
            {
                WeatherTypeListItem wtli2 = wtli.DeepCopy();
                other.weatherTypeListItems.Add(wtli2);
            }            
            return other;
        }
    }
}
