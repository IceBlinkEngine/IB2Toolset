using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class FullScreenEffectLayer
    {
        private int _fullScreenEffectOrderNumber = 0;

        private bool _useFullScreenEffectLayer = false;
        private bool _fullScreenEffectLayerIsActive = true;
        private string _fullScreenEffectLayerName = "sea";
        private float _fullScreenAnimationSpeed = 1.0f;
        private float _fullScreenAnimationSpeedX = 0.5f;
        private float _fullScreenAnimationSpeedY = -1.15f;
        private bool _fullScreenEffectLayerIsTop = false;
        private int _fullScreenEffectChanceToOccur = 100;
        //zero signifies an endlessly running animation
        private int _numberOfCyclesPerOccurence = 0;
        private float _cycleCounter = 0;
        private bool _containEffectInsideAreaBorders = false;
        private int _activateTargetChannelInParallelToThisChannel = 0;

        private bool _isChanging = true;
        private float _changeCounter = 0;
        private float _changeLimit = 15;
        private float _changeFrameCounter = 1;
        private float _changeNumberOfFrames = 6;
        private bool _useCyclicFade = false;

        private float _fullScreenAnimationFrameCounterX = 0;
        private float _fullScreenAnimationFrameCounterY = 1.5f;
        private int _fullScreenAnimationFrameCounter = 0;

        //setting up the override movement patterns
        //a value of -100 ("" in case of overrideIsNoScrollSource2) means that the default setting of the overide animation pattern for this parameter shall be used
        //so far existing directional overrides: 
        //rain (2 layers recommended; make one layer's y speed a little slower than default -2.8, like -2.4) 
        //clouds (1 layer recommended; defaults at 0.5y, 0.5x, 750 delay)
        //snow (2 layers recommended; make one layer's y speed a little faster than default -0.55, like -0.65, mayhaps slower for x (default: 0.45 to e.g. 0.4), and overrideDelayLimit1 a little less than defaut 470, like 380) 
        //fog (2 layers recommended, make one layer's overrideDelayLimit1 a little less than default 125, like 110)
        private string _directionalOverride = "fog";
        private float _overrideSpeedX = -100f;
        private float _overrideSpeedY = -100;
        private int _overrideDelayLimit = 40;
        private int _overrideDelayCounter = 10000;
        private string _overrideIsNoScrollSource = "";
        private bool _changeableByWeatherScript = false;

        [CategoryAttribute("01 - Main"), DescriptionAttribute("use this layer yes/no: shut this layer down compeltely, set to no for channels not used anyway by this effect")]
        public bool useFullScreenEffectLayer
        {
            get { return _useFullScreenEffectLayer; }
            set { _useFullScreenEffectLayer = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("in doubt: do not chnage this setting; activate this layer on/off")]
        public bool fullScreenEffectLayerIsActive
        {
            get { return _fullScreenEffectLayerIsActive; }
            set { _fullScreenEffectLayerIsActive = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("layer name: this is name of .png graphic to use for this effect layer")]
        public string fullScreenEffectLayerName
        {
            get { return _fullScreenEffectLayerName; }
            set { _fullScreenEffectLayerName = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("full screen animation speed")]
        public float fullScreenAnimationSpeed
        {
            get { return _fullScreenAnimationSpeed; }
            set { _fullScreenAnimationSpeed = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("fullScreenAnimationSpeedX")]
        public float fullScreenAnimationSpeedX
        {
            get { return _fullScreenAnimationSpeedX; }
            set { _fullScreenAnimationSpeedX = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("fullScreenAnimationSpeedY")]
        public float fullScreenAnimationSpeedY
        {
            get { return _fullScreenAnimationSpeedY; }
            set { _fullScreenAnimationSpeedY = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("fullScreenEffectLayerIsTop")]
        public bool fullScreenEffectLayerIsTop
        {
            get { return _fullScreenEffectLayerIsTop; }
            set { _fullScreenEffectLayerIsTop = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("fullScreenEffectChanceToOccur")]
        public int fullScreenEffectChanceToOccur
        {
            get { return _fullScreenEffectChanceToOccur; }
            set { _fullScreenEffectChanceToOccur = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("numberOfCyclesPerOccurence")]
        public int numberOfCyclesPerOccurence
        {
            get { return _numberOfCyclesPerOccurence; }
            set { _numberOfCyclesPerOccurence = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("fullScreenEffectChanceToOccur")]
        public int fullScreenEffectOrderNumber
        {
            get { return _fullScreenEffectOrderNumber; }
            set { _fullScreenEffectOrderNumber = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("cycleCounter")]
        public float cycleCounter
        {
            get { return _cycleCounter; }
            set { _cycleCounter = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("containEffectInsideAreaBorders")]
        public bool containEffectInsideAreaBorders
        {
            get { return _containEffectInsideAreaBorders; }
            set { _containEffectInsideAreaBorders = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("activateTargetChannelInParallelToThisChannel")]
        public int activateTargetChannelInParallelToThisChannel
        {
            get { return _activateTargetChannelInParallelToThisChannel; }
            set { _activateTargetChannelInParallelToThisChannel = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("isChanging")]
        public bool isChanging
        {
            get { return _isChanging; }
            set { _isChanging = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("changeCounter")]
        public float changeCounter
        {
            get { return _changeCounter; }
            set { _changeCounter = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("changeLimit")]
        public float changeLimit
        {
            get { return _changeLimit; }
            set { _changeLimit = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("changeFrameCounter")]
        public float changeFrameCounter
        {
            get { return _changeFrameCounter; }
            set { _changeFrameCounter = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("changeNumberOfFrames")]
        public float changeNumberOfFrames
        {
            get { return _changeNumberOfFrames; }
            set { _changeNumberOfFrames = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("useCyclicFade")]
        public bool useCyclicFade
        {
            get { return _useCyclicFade; }
            set { _useCyclicFade = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("fullScreenAnimationFrameCounterX")]
        public float fullScreenAnimationFrameCounterX
        {
            get { return _fullScreenAnimationFrameCounterX; }
            set { _fullScreenAnimationFrameCounterX = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("fullScreenAnimationFrameCounterY")]
        public float fullScreenAnimationFrameCounterY
        {
            get { return _fullScreenAnimationFrameCounterY; }
            set { _fullScreenAnimationFrameCounterY = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("fullScreenAnimationFrameCounter")]
        public int fullScreenAnimationFrameCounter
        {
            get { return _fullScreenAnimationFrameCounter; }
            set { _fullScreenAnimationFrameCounter = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("directionalOverride")]
        public string directionalOverride
        {
            get { return _directionalOverride; }
            set { _directionalOverride = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("overrideSpeedX")]
        public float overrideSpeedX
        {
            get { return _overrideSpeedX; }
            set { _overrideSpeedX = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("overrideSpeedY")]
        public float overrideSpeedY
        {
            get { return _overrideSpeedY; }
            set { _overrideSpeedY = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("overrideDelayLimit")]
        public int overrideDelayLimit
        {
            get { return _overrideDelayLimit; }
            set { _overrideDelayLimit = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("overrideDelayCounter")]
        public int overrideDelayCounter
        {
            get { return _overrideDelayCounter; }
            set { _overrideDelayCounter = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("overrideIsNoScrollSource")]
        public string overrideIsNoScrollSource
        {
            get { return _overrideIsNoScrollSource; }
            set { _overrideIsNoScrollSource = value; }
        }
        [CategoryAttribute("02 - In doubt: do not change"), DescriptionAttribute("changeableByWeatherScript")]
        public bool changeableByWeatherScript
        {
            get { return _changeableByWeatherScript; }
            set { _changeableByWeatherScript = value; }
        }
       
        public FullScreenEffectLayer()
        {

        }

        public FullScreenEffectLayer DeepCopy()
        {
            //WeatherTypeList other = (WeatherTypeList)this.MemberwiseClone();
            FullScreenEffectLayer other = new FullScreenEffectLayer();
            other.activateTargetChannelInParallelToThisChannel = this.activateTargetChannelInParallelToThisChannel;
            other.changeableByWeatherScript = this.changeableByWeatherScript;
            other.changeCounter = this.changeCounter;
            other.changeFrameCounter = this.changeFrameCounter;
            other.changeLimit = this.changeLimit;
            other.changeNumberOfFrames = this.changeNumberOfFrames;
            other.containEffectInsideAreaBorders = this.containEffectInsideAreaBorders;
            other.directionalOverride = this.directionalOverride;
            other.fullScreenAnimationSpeed = this.fullScreenAnimationSpeed;
            other.fullScreenAnimationSpeedX = this.fullScreenAnimationSpeedX;
            other.fullScreenAnimationSpeedY = this.fullScreenAnimationSpeedY;
            other.fullScreenEffectChanceToOccur = this.fullScreenEffectChanceToOccur;
            other.fullScreenEffectLayerIsActive = this.fullScreenEffectLayerIsActive;
            other.fullScreenEffectLayerIsTop = this.fullScreenEffectLayerIsTop;
            other.fullScreenEffectLayerName = this.fullScreenEffectLayerName;
            other.fullScreenEffectOrderNumber = this.fullScreenEffectOrderNumber;
            other.isChanging = this.isChanging;
            other.numberOfCyclesPerOccurence = this.numberOfCyclesPerOccurence;
            other.overrideDelayLimit = this.overrideDelayLimit;
            other.overrideIsNoScrollSource = this.overrideIsNoScrollSource;
            other.overrideDelayCounter = this.overrideDelayCounter;
            other.overrideSpeedX = this.overrideSpeedX;
            other.overrideSpeedY = this.overrideSpeedY;
            other.useCyclicFade = this.useCyclicFade;
            other.useFullScreenEffectLayer = this.useFullScreenEffectLayer;

            /*
            other.name = this.name;
            other._tag = this._tag;
            other.tag = this.tag;
            foreach (WeatherTypeListItem wtli in this.weatherTypeListItems)
            {
                WeatherTypeListItem wtli2 = wtli.DeepCopy();
                other.weatherTypeListItems.Add(wtli2);
            }
            */
            return other;
        }

    }
}
