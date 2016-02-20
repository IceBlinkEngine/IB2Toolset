using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class WeatherEffect
    {
        private string _tag = "newWeatherEffectTag"; //item unique tag name
        private string _name = "newWeatherEffectName"; //weather effect name in toolset
        private List<FullScreenEffectLayer> _WeatherLayers = new List<FullScreenEffectLayer>();

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Weather system")]
        public List<FullScreenEffectLayer> WeatherLayers
        {
            get { return _WeatherLayers; }
            set { _WeatherLayers = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Tag of the WeatherEffect (Must be unique)")]
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

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Name of the WeatherEffect")]
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

        public WeatherEffect()
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

        public WeatherEffect DeepCopy()
        {
            WeatherEffect other = (WeatherEffect)this.MemberwiseClone();
            return other;
        }
    }
}
