using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WindowsObjectScreenPositionControl.Win32
{
    public class User32
    {
        public const string DllName = Win32ExternDll.User32;

        #region EnumWindows

        public delegate bool EnumWindowsProcCallback(int hWnd, int lParam);
        
        [DllImport(DllName, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool EnumWindows(EnumWindowsProcCallback callback, int extraData);

        #endregion

        #region GetClassName

        /// <summary>
        /// 지정된 창이 속한 클래스의 이름을 검색합니다.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpClassName"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "GetClassName", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// 지정된 창이 속한 클래스의 이름을 검색합니다.
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpClassName"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "GetClassName", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetClassName(int hWnd, StringBuilder lpClassName, int nMaxCount);

        #endregion

        #region GetCursorPos

        [DllImport(DllName, EntryPoint = "GetCursorPos", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetCursorPos(out POINT outPoint);

        #endregion

        #region GetParent

        /// <summary>
        /// 창이 자식 창인 경우 반환 값은 부모 창에 대한 핸들입니다. 창이 WS_POPUP 스타일이 있는 최상위 창인 경우 반환 값은 소유자 창에 대한 핸들입니다.
        /// <para>실패 시 <see cref="Marshal.GetLastWin32Error()"/> 메서드를 통해 오류정보를 얻어 올 수 있음</para>
        /// <para>창은 소유하지 않았거나 WS_POPUP 스타일이 없는 최상위 창입니다.</para>
        /// <para>소유자 창에는 WS_POPUP 스타일이 있습니다.</para>
        /// </summary>
        /// <param name="hWnd">부모 창 핸들을 검색할 창에 대한 핸들입니다.</param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "GetParent", SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        /// <summary>
        /// 창이 자식 창인 경우 반환 값은 부모 창에 대한 핸들입니다. 창이 WS_POPUP 스타일이 있는 최상위 창인 경우 반환 값은 소유자 창에 대한 핸들입니다.
        /// <para>실패 시 <see cref="Marshal.GetLastWin32Error()"/> 메서드를 통해 오류정보를 얻어 올 수 있음</para>
        /// <para>창은 소유하지 않았거나 WS_POPUP 스타일이 없는 최상위 창입니다.</para>
        /// <para>소유자 창에는 WS_POPUP 스타일이 있습니다.</para>
        /// </summary>
        /// <param name="hWnd">부모 창 핸들을 검색할 창에 대한 핸들입니다.</param>
        /// <returns></returns>
        [DllImport(DllName, EntryPoint = "GetParent", SetLastError = true)]
        public static extern int GetParent(int hWnd);

        #endregion

        #region GetWindowLong

        [DllImport(DllName, EntryPoint = "GetWindowLong", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern long GetWindowLong(int hWnd, int nIndex);

        [DllImport(DllName, EntryPoint = "GetWindowLong", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern long GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport(DllName, EntryPoint = "GetWindowLong", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetWindowLongUInt(int hWnd, int nIndex);

        [DllImport(DllName, EntryPoint = "GetWindowLong", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetWindowLongUInt(IntPtr hWnd, int nIndex);

        #endregion

        #region GetWindowPlacement

        [DllImport(DllName, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        #endregion

        #region GetWindowRect

        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, out RECT rect);

        [DllImport(DllName, SetLastError = true)]
        public static extern IntPtr GetWindowRect(int hWnd, out RECT rect);

        #endregion

        #region GetWindowText

        [DllImport(DllName, EntryPoint = "GetWindowText", SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport(DllName, EntryPoint = "GetWindowText", SetLastError = true)]
        public static extern int GetWindowText(int hWnd, StringBuilder text, int count);

        #endregion

        #region GetWindowTextLength

        [DllImport(DllName, EntryPoint = "GetWindowTextLength", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport(DllName, EntryPoint = "GetWindowTextLength", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowTextLength(int hWnd);

        #endregion

        #region GetWindowThreadProcessId

        [DllImport(DllName, EntryPoint = "GetWindowThreadProcessId", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [DllImport(DllName, EntryPoint = "GetWindowThreadProcessId", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        #endregion

        #region MouseEvent

        [DllImport(DllName, EntryPoint = "mouse_event", SetLastError = true)]
        public static extern void MouseEvent(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        #endregion

        #region SetCursorPos

        [DllImport(DllName, EntryPoint = "SetCursorPos", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int x, int y);

        #endregion

        #region WindowFromPoint

        [DllImport(DllName, EntryPoint = "WindowFromPoint", SetLastError = true)]
        public static extern IntPtr WindowFromPoint(Point point);

        #endregion
    }
}
