using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IB2miniToolset
{
    public class TileBitmapNamePair
    {
        public Bitmap bitmap = null;
        public string filename = "";

        public TileBitmapNamePair()
        {
        }
        public TileBitmapNamePair(Bitmap bm, string fname)
        {
            bitmap = bm;
            filename = fname;
        }
    }
}
