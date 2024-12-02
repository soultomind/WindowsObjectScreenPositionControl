using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsObjectScreenPositionControl.Win32
{
    public static class MouseNativeMethods
    {
        public static void SetCursorPoint(int x, int y)
        {
            User32.SetCursorPos(x, y);
        }
    }
}
