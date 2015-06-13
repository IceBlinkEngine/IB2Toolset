using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace IB2Toolset
{
    public class WayPoint
    {
        private int _X = 0;
        private int _Y = 0;
        private int _WaitDuration = 6; //in seconds
        private List<BarkString> _BarkStringsAtWayPoint = new List<BarkString>();
        private List<BarkString> _BarkStringsOnTheWayToNextWayPoint = new List<BarkString>();
	    //private int _StartWaitDurationTime = 0; //in seconds, this is set to the world time once at waypoint

        [CategoryAttribute("01 - Main"), DescriptionAttribute("X coordinate of waypoint")]
        public int X
        {
            get { return _X; }
            set { _X = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Y coordinate of waypoint")]
        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("number of seconds (6 seconds per round) for the prop to stop at this waypoint before moving to the next waypoint.")]
        public int WaitDuration
        {
            get { return _WaitDuration; }
            set { _WaitDuration = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Floaty message bark strings to play at waypoint.")]
        public List<BarkString> BarkStringsAtWayPoint
        {
            get { return _BarkStringsAtWayPoint; }
            set { _BarkStringsAtWayPoint = value; }
        }
        [CategoryAttribute("01 - Main"), DescriptionAttribute("Floaty message bark strings to play on the way to the next waypoint.")]
        public List<BarkString> BarkStringsOnTheWayToNextWayPoint
        {
            get { return _BarkStringsOnTheWayToNextWayPoint; }
            set { _BarkStringsOnTheWayToNextWayPoint = value; }
        }

        public WayPoint()
        {
        }
        public WayPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
        public WayPoint DeepCopy()
        {
            WayPoint other = (WayPoint)this.MemberwiseClone();
            other.BarkStringsAtWayPoint = new List<BarkString>();
            foreach (BarkString bs in this.BarkStringsAtWayPoint)
            {
                other.BarkStringsAtWayPoint.Add(bs.DeepCopy());
            }
            other.BarkStringsOnTheWayToNextWayPoint = new List<BarkString>();
            foreach (BarkString bs in this.BarkStringsOnTheWayToNextWayPoint)
            {
                other.BarkStringsOnTheWayToNextWayPoint.Add(bs.DeepCopy());
            }
            return other;
        }
    }
}
