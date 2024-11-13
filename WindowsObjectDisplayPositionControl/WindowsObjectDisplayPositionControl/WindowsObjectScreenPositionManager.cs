using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsObjectDisplayPositionControl
{
    public partial class WindowsObjectScreenPositionManager
    {
        /// <summary>
        /// 개발모드 여부를 나타냅니다.
        /// </summary>
        public bool DevelopMode
        {
            get { return developMode; }
            set { developMode = value; }
        }
        private volatile bool developMode;

        #region Screen

        /// <summary>
        /// 스크린 수(<see cref="Screen.AllScreens"/>.Length)
        /// </summary>
        public int ScreenAllLength
        {
            get { return screenAllLength; }
            private set { screenAllLength = value; }
        }
        private volatile int screenAllLength;

        #region JobScreen

        public int JobScreenIndex
        {
            get { return jobScreenIndex; }
            set
            {
                if (!ScreenManager.IsValidIndex(value))
                {
                    throw new ArgumentException("!ScreenManager.IsValidIndex(value)");
                }
                jobScreenIndex = value;
            }
        }
        private volatile int jobScreenIndex;

        public Screen JobScreen
        {
            get
            {
                Screen jobScreen = null;
                try
                {
                    jobScreen = Screen.AllScreens[JobScreenIndex];
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw;
                }
                return jobScreen;
            }
        }

        #endregion

        #region AdjustScreen

        public int AdjustScreenIndex
        {
            get { return adjustScreenIndex; }
            set
            {
                if (!ScreenManager.IsValidIndex(value))
                {
                    throw new ArgumentException("!ScreenManager.IsValidIndex(value)");
                }
                adjustScreenIndex = value;
            }
        }
        private volatile int adjustScreenIndex;

        public Screen AdjustScreen
        {
            get
            {
                Screen adjustScreen = null;
                try
                {
                    adjustScreen = Screen.AllScreens[AdjustScreenIndex];
                }
                catch (IndexOutOfRangeException ex)
                {
                    throw;
                }
                return adjustScreen;
            }
        }

        #endregion

        #endregion

        public WindowsObject WindowsObject
        {
            get { return windowsObject; }
            set { windowsObject = value; }
        }
        private volatile WindowsObject windowsObject = WindowsObject.Window;


        public event Func<object, EnumWindowsProcessSetWindowPosEventArgs, bool> EnumWindowsProcessSetWindowPos;

        public WindowsObjectScreenPositionManager(int jobScreenIndex, int adjustScreenIndex, WindowsObject windowsObject)
        {
            JobScreenIndex = jobScreenIndex;
            AdjustScreenIndex = adjustScreenIndex;
            WindowsObject = windowsObject;
        }

        internal bool CanStartJob
        {
            get
            {
                if ((ScreenManager.IsValidIndex(JobScreenIndex) && ScreenManager.IsValidIndex(AdjustScreenIndex)))
                {
                    if (!ScreenManager.IsLeftOrRightNearScreen(JobScreen, AdjustScreen))
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    OnWarn(String.Format("JobScreenIndex={0}, AdjustScreenIndex={1}", JobScreenIndex, AdjustScreenIndex));
                    return false;
                }
            }
        }

        internal bool StartJob(int hWnd = 0)
        {
            if (Toolkit.IsCurrentProcessExecuteAdministrator())
            {
                OnInfo("관리자권한모드로 실행합니다.");
            }
            else
            {
                OnInfo("일반실행모드로 실행합니다.");
                OnInfo("윈도우에서 제공하는 기본 프로그램의 창들을 제어하기 위해서는 관리자 권한 실행이 필요할수도 있습니다");
            }

            return true;
        }
    }
}
