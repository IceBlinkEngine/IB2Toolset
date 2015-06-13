using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IB2Toolset
{
    public class Coordinate
    {
        private int _X = 0;
        private int _Y = 0;
        public int X
        {
            get { return _X; }
            set { _X = value; }
        }
        public int Y
        {
            get { return _Y; }
            set { _Y = value; }
        }

        public Coordinate()
        {
        }
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
