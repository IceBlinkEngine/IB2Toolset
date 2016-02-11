using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class WeatherEffect
    {
        private List<FullScreenEffectLayer> _WeatherLayers = new List<FullScreenEffectLayer>();

        [CategoryAttribute("01 - Main"), DescriptionAttribute("Weather system")]
        public List<FullScreenEffectLayer> WeatherLayers
        {
            get { return _WeatherLayers; }
            set { _WeatherLayers = value; }
        }

        public WeatherEffect()
        {
            for (int i = 0; i < 10; i++)
            {
                FullScreenEffectLayer newLayer = new FullScreenEffectLayer();
                WeatherLayers.Add(newLayer);
            }
        }
    }
}
