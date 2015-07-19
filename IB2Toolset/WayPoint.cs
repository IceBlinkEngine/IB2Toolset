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
        string _areaName;
        string _departureTime;

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

        [CategoryAttribute("01 - Main"), DescriptionAttribute("name of the area that the waypoint exists on. First waypoint with different area name then the one before it initiates an area transition of the prop.")]
        public string areaName
        {
            get { return _areaName; }
            set { _areaName = value; }
        }

        [CategoryAttribute("01 - Main"), DescriptionAttribute("time in the relevant interval (daily, weekly, monthly or yearly) at which the prop leaves this waypoint and travels to the next waypoint. This is a string value that requires a strict format: [day, from 0 to 336, assuming 7 days a week with 4 weeks a month and 12 months a year: 0 is used for the daily cycle, while weekly, monthly and yearly cycles use day up to the max level for that cycle, i.e. 1-7 for weekly, 1-28 for monthly and 1 to 336 for yearly]:[hour, from 0 to 23]:[minute, from 0 to 59]. Examples: 0:16:31 means 16 hours and 31 minutes in a daily cycle; 5:3:8 means fifth day in weekly/monthly/yearly cycle, three o'clock in the morning and eight minutes.")]
        public string departureTime
        {
            get { return _departureTime; }
            set { _departureTime = value; }
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
