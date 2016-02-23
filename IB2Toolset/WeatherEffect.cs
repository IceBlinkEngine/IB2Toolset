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
            //WeatherEffect other = (WeatherEffect)this.MemberwiseClone();
            WeatherEffect other = new WeatherEffect();
            other.name = this.name;
            other._name = this._name;
            other.tag = this.tag;
            other._tag = this._tag;
            foreach (FullScreenEffectLayer fsel in this.WeatherLayers)
            {
                FullScreenEffectLayer fsel2 = fsel.DeepCopy();
                other.WeatherLayers.Add(fsel2);
            }
            return other;
        }
    }
}
