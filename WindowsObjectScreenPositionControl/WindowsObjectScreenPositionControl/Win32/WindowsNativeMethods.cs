using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsObjectScreenPositionControl.Win32
{
    public static class WindowsNativeMethods
    {
        #region GetClassName

        public static string GetClassName(IntPtr hWnd)
        {
            StringBuilder lpClassName = new StringBuilder(1024);
            User32.GetClassName(hWnd, lpClassName, lpClassName.Capacity);
            string className = lpClassName.ToString();
            return className;
        }

        #endregion

        #region GetParentFormHandle

        private static IntPtr GetParentFormHandle(IntPtr parentHandle, IntPtr childHandle)
        {
            if (parentHandle != IntPtr.Zero)
            {
                childHandle = parentHandle;
                parentHandle = User32.GetParent(parentHandle);
                return GetParentFormHandle(parentHandle, childHandle);
            }
            else
            {
                return childHandle;
            }
        }

        public static IntPtr GetParentFormHandle(IntPtr hWnd)
        {
            return GetParentFormHandle(User32.GetParent(hWnd), hWnd);
        }

        #endregion

        #region GetWindowText

        public static string GetWindowText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = User32.GetWindowTextLength(hWnd);

            // length * 4 한글일때
            StringBuilder builder = new StringBuilder(length * 4);
            User32.GetWindowText(hWnd, builder, builder.Capacity);
            return builder.ToString();
        }

        #endregion

        #region GetWindowThreadProcessId

        public static int GetWindowThreadProcessId(IntPtr hWnd)
        {
            int processId = 0;
            User32.GetWindowThreadProcessId(hWnd, out processId);
            return processId;
        }

        #endregion

        #region
        public static bool IsChildWindowByHandleCheckedWin32Style(IntPtr hWnd)
        {
            uint windowLong = User32.GetWindowLongUInt(hWnd, Win32GCL.GCL_HMODULE);
            return IsChildWindowByHandleAndWindowLongCheckedWin32Style(hWnd, windowLong);
        }
        
        public static bool IsChildWindowByHandleAndWindowLongCheckedWin32Style(IntPtr hWnd, uint windowLong)
        {
            if (((windowLong & Win32Style.WS_POPUP) == Win32Style.WS_POPUP) ||
                ((windowLong & Win32Style.WS_CHILD) == Win32Style.WS_CHILD) ||
                ((windowLong & Win32Style.WS_CAPTION) == Win32Style.WS_CAPTION) ||
                ((windowLong & Win32Style.WS_MAXIMIZEBOX) == Win32Style.WS_MAXIMIZEBOX) ||
                ((windowLong & Win32Style.WS_MINIMIZEBOX) == Win32Style.WS_MINIMIZEBOX) ||
                ((windowLong & Win32Style.WS_VISIBLE) == Win32Style.WS_VISIBLE)
               )
            {
                return true;
            }
            return false;
        }

        #endregion

        #region 

        public static bool IsMainWindowByHandleAndWindowLongCheckedWin32Style(IntPtr hWnd)
        {
            uint windowLong = User32.GetWindowLongUInt(hWnd, Win32GCL.GCL_HMODULE);
            return IsMainWindowByHandleAndWindowLongCheckedWin32Style(hWnd, windowLong);
        }

        public static bool IsMainWindowByHandleAndWindowLongCheckedWin32Style(IntPtr hWnd, uint windowLong)
        {
            if (((windowLong & Win32Style.WS_POPUP) == Win32Style.WS_POPUP) ||
                ((windowLong & Win32Style.WS_CHILD) == Win32Style.WS_CHILD) ||
                ((windowLong & Win32Style.WS_CAPTION) == Win32Style.WS_CAPTION) ||
                ((windowLong & Win32Style.WS_MAXIMIZEBOX) == Win32Style.WS_MAXIMIZEBOX) ||
                ((windowLong & Win32Style.WS_MINIMIZEBOX) == Win32Style.WS_MINIMIZEBOX)
               )
            {
                return true;
            }
            return false;
        }

        #endregion

        #region IsWindowControlVisible

        public static bool IsWindowControlVisible(IntPtr hWnd, ref WINDOWPLACEMENT windowplacement)
        {
            windowplacement = WINDOWPLACEMENT.Default;
            if (User32.GetWindowPlacement(hWnd, ref windowplacement))
            {
                if (windowplacement.ShowCmd == ShowWindowCommands.Normal ||
                    windowplacement.ShowCmd == ShowWindowCommands.Maximize ||
                    windowplacement.ShowCmd == ShowWindowCommands.ShowMaximized ||
                    windowplacement.ShowCmd == ShowWindowCommands.Show)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsWindowControlVisibleByIntHandle(int hWnd, ref WINDOWPLACEMENT windowplacement)
        {
            return IsWindowControlVisible((IntPtr)hWnd, ref windowplacement);
        }

        #endregion

        #region IsParentChildRelationship
        public static bool IsParentChildRelationship(IntPtr handleParent, IntPtr handleChild)
        {
            if (handleParent == IntPtr.Zero || handleChild == IntPtr.Zero)
            {
                return false;
            }

            if (User32.GetParent(handleChild) == handleParent)
            {
                return true;
            }
            return false;
        }

        public static bool IsParentChildRelationship(int handleParent, int handleChild)
        {
            return IsParentChildRelationship((IntPtr)handleParent, (IntPtr)handleChild);
        }

        #endregion

        #region IsParentDesktopWindow

        public static bool IsParentDeskopWindow(int hWnd)
        {
            return 0 == User32.GetParent(hWnd);
        }

        #endregion
    }
}
