using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class Weather
    {
        private string _tag = "newWeatherTag";
        private string _name = "newWeatherName";
        public List<WeatherTypeList> weatherTypeLists = new List<WeatherTypeList>();

        //use add in lbx
        /*
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Weather system")]
        public List<FullScreenEffectLayer> WeatherLayers
        {
            get { return _WeatherLayers; }
            set { _WeatherLayers = value; }
        }
        */

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the Weather (Must be unique)")]
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

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Name of the Weather")]
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

        public Weather()
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

        public Weather DeepCopy()
        {
            //Weather other = (Weather)this.Clone();

            Weather other = new Weather();
            other._name = this._name;
            other.name = this.name;
            other._tag = this._tag;
            other.tag = this.tag;
            foreach (WeatherTypeList wtl in this.weatherTypeLists)
            {
                WeatherTypeList wtl2 = wtl.DeepCopy();
                other.weatherTypeLists.Add(wtl2);
            } 
            return other;
        }
    }
}
