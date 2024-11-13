using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsObjectDisplayPositionControl.Win32
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public static RECT Empty = new RECT(0, 0, 0, 0);

        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        public RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
    }
}
