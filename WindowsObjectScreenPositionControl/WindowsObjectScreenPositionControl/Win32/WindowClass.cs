using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsObjectScreenPositionControl.Win32
{
    public class WindowClass
    {
        public IntPtr Handle
        {
            get { return handle; }
            set
            {
                if (IntPtr.Zero == value)
                {
                    throw new ArgumentException("IntPtr.Zero == Value");
                }
                handle = value;
            }
        }
        private IntPtr handle = IntPtr.Zero;

        public string Text
        {
            get { return text; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                text = value;
            }
        }
        private string text = String.Empty;

        public string ClassName
        {
            get { return className; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                className = value;
            }
        }
        private string className = String.Empty;

        public int ProcessId
        {
            get { return processId; }
            private set { processId = value; }
        }
        private int processId;

        public WindowClass(IntPtr handle)
        {
            Handle = handle;

            Text = WindowsNativeMethods.GetWindowText(Handle);
            ClassName = WindowsNativeMethods.GetClassName(Handle);
            ProcessId = WindowsNativeMethods.GetWindowThreadProcessId(Handle);
        }

        public Rectangle Bounds
        {
            get
            {
                RECT handleRect = new RECT();
                User32.GetWindowRect(handle, out handleRect);
                return handleRect.ToRectangle();
            }
        }

        /// <summary>
        /// <see cref="Handle"/> 값의 컨트롤이 속한 부모폼 핸들을 가져옵니다.
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public IntPtr ParentHandle
        {
            get
            {
                IntPtr parentHandle = WindowsNativeMethods.GetParentFormHandle(Handle);
                return parentHandle;
            }
        }

        /// <summary>
        /// 부모폼 클래스를 생성하여 반환합니다.
        /// </summary>
        /// <returns></returns>
        public WindowClass CreateParentWindowClass()
        {
            return new WindowClass(ParentHandle);
        }
    }
}
