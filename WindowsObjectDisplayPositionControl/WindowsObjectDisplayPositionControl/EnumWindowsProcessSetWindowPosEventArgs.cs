using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsObjectDisplayPositionControl.Win32;

namespace WindowsObjectDisplayPositionControl
{
    public class EnumWindowsProcessSetWindowPosEventArgs : EventArgs
    {
        public IntPtr Hwnd { get; private set; }
        public string Text { get; private set; }
        public string ClassName { get; private set; }
        public int ProcessId { get; private set; }

        public EnumWindowsProcessSetWindowPosEventArgs(IntPtr hWnd)
        {
            Hwnd = hWnd;

            Text = WindowsNativeMethods.GetWindowText(hWnd);
            ClassName = WindowsNativeMethods.GetClassName(hWnd);
            ProcessId = WindowsNativeMethods.GetWindowThreadProcessId(hWnd);
        }
    }
}
