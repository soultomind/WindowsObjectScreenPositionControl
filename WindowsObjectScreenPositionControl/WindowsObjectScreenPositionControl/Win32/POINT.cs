using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsObjectScreenPositionControl.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public static POINT Empty = new POINT(0, 0);

        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public bool IsEmpty
        {
            get { return X == 0 && Y == 0; }
        }

        public static implicit operator Point(POINT p)
        {
            return new Point(p.X, p.Y);
        }

        public static implicit operator POINT(Point p)
        {
            return new POINT(p.X, p.Y);
        }
    }
}
