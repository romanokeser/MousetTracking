using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseTrackingWPF
{
    public struct  Point
    {
            public int X;
            public int Y;
            public override string ToString()
            {
                return X + "," + Y;
            }
    }
}
