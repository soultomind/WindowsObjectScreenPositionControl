using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsObjectScreenPositionControl.Extension;
using WindowsObjectScreenPositionControl.Win32;

namespace WindowsObjectScreenPositionControl
{
    public partial class WindowsObjectScreenPositionManager
    {
        #region Static

        #region Properties

        /// <summary>
        /// 위치조정 처리시 무시할 클래스네임 기본 목록 리스트를 나타냅니다.
        /// </summary>
        public static IList<string> DefaultIgnoreAdjustPositionClassNameList
        {
            get { return defaultIgnoreAdjustPositionClassNameList; }
        }
        private static readonly IList<string> defaultIgnoreAdjustPositionClassNameList = new List<string>()
        {
            "WorkerW", "Progman", "TabletModeCoverWindow"
        };

        #endregion

        #endregion

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

        /// <summary>
        /// 디스플레이 환경
        /// </summary>
        public DisplayEnvironment DisplayEnvironment
        {
            get { return DisplayEnvironment; }
            private set { displayEnvironment = value; }
        }
        private volatile DisplayEnvironment displayEnvironment;

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

                if (ScreenManager.GetPrimaryScreenIndex() == value)
                {
                    throw new ArgumentException("ScreenManager.GetPrimaryScreenIndex() == value");
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
                    OnWarn(ex.StackTrace);
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
                    OnWarn(ex.StackTrace);
                    throw;
                }
                return adjustScreen;
            }
        }

        #endregion

        #endregion

        #region Mouse

        public int MouseCursorAdjustShiftPositionX
        {
            get { return mouseCursorAdjustShiftPositionX; }
            set
            {
                if (value > 0)
                {
                    Rectangle userWorkingArea = AdjustScreen.UserWorkingArea();
                    if (!(userWorkingArea.Width > value))
                    {
                        throw new ArgumentException();
                    }

                    mouseCursorAdjustShiftPositionX = value;
                }
            }
        }
        private volatile int mouseCursorAdjustShiftPositionX = 50;

        #endregion

        /// <summary>
        /// 위치조정 처리시 무시할 클래스네임 목록 셋입니다.
        /// </summary>
        public ISet<string> IgnoreAdjustPositionClassNameSet
        {
            get { return ignoreAdjustPositionClassNameSet; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                ignoreAdjustPositionClassNameSet = value;
            }
        }
        private ISet<string> ignoreAdjustPositionClassNameSet;

        /// <summary>
        /// <see cref="JobScreen"/> 스크린 영역에 위치해도 되는 특정 윈도우에 창 텍스트 목록 셋입니다.
        /// </summary>
        public ISet<string> AllowPreventScreenTextSet
        {
            get { return allowPreventScreenTextSet; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                allowPreventScreenTextSet = value;
            }
        }
        private ISet<string> allowPreventScreenTextSet;

        public WindowsObject WindowsObject
        {
            get { return windowsObject; }
            set { windowsObject = value; }
        }
        private volatile WindowsObject windowsObject = WindowsObject.Window;


        public event Func<object, EnumWindowsProcessSetWindowPosEventArgs, bool> EnumWindowsProcessSetWindowPos;

        public WindowsObjectScreenPositionManager(int jobScreenIndex, int adjustScreenIndex, WindowsObject windowsObject)
            : this(jobScreenIndex, adjustScreenIndex, windowsObject, (List<string>)DefaultIgnoreAdjustPositionClassNameList)
        {
            
        }

        public WindowsObjectScreenPositionManager(int jobScreenIndex, int adjustScreenIndex, WindowsObject windowsObject, List<string> defaultIgnoreAdjustPositionClassNameList)
        {
            JobScreenIndex = jobScreenIndex;
            AdjustScreenIndex = adjustScreenIndex;
            WindowsObject = windowsObject;

            IgnoreAdjustPositionClassNameSet = new HashSet<string>();
            AllowPreventScreenTextSet = new HashSet<string>();

            if (Object.ReferenceEquals(DefaultIgnoreAdjustPositionClassNameList, defaultIgnoreAdjustPositionClassNameList))
            {
                AddIgnoreAdjustPositionClassNameList(DefaultIgnoreAdjustPositionClassNameList);
            }
            else
            {
                AddIgnoreAdjustPositionClassNameList(DefaultIgnoreAdjustPositionClassNameList);
                AddIgnoreAdjustPositionClassNameList(defaultIgnoreAdjustPositionClassNameList);
            }
        }

        #region IgnoreAdjustPositionClassNameList

        /// <summary>
        /// 윈도우 위치조정 처리시 무시할 클래스네임 목록을 추가합니다.
        /// </summary>
        /// <param name="ignoreAdjustPositionClassNameList"></param>
        public void AddIgnoreAdjustPositionClassNameList(IList<string> ignoreAdjustPositionClassNameList)
        {
            foreach (var ignoreAdjustPositionClassName in ignoreAdjustPositionClassNameList)
            {
                AddIgnoreAdjustPositionClassName(ignoreAdjustPositionClassName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ignoreAdjustPositionClassName"></param>
        public bool AddIgnoreAdjustPositionClassName(string ignoreAdjustPositionClassName)
        {
            if (ignoreAdjustPositionClassName == null)
            {
                throw new ArgumentNullException(nameof(ignoreAdjustPositionClassName));
            }

            if (ignoreAdjustPositionClassName.Length == 0)
            {
                throw new ArgumentException(nameof(ignoreAdjustPositionClassName) + ".Length == 0");
            }

            bool bResult = IgnoreAdjustPositionClassNameSet.Add(ignoreAdjustPositionClassName); ;
            if (bResult)
            {
                OnDebug(String.Format("AddIgnoreAdjustPositionClassName({0})", ignoreAdjustPositionClassName));
            }

            return bResult;
        }

        #endregion

        #region AllowPreventScreenTextList

        public bool AddAllowPreventScreenText(string allowPreventScreenText)
        {
            if (allowPreventScreenText == null)
            {
                throw new ArgumentNullException(nameof(allowPreventScreenText));
            }

            if (allowPreventScreenText.Length == 0)
            {
                throw new ArgumentException(nameof(allowPreventScreenText) + ".Length == 0");
            }

            bool bResult = AllowPreventScreenTextSet.Add(allowPreventScreenText);
            if (bResult)
            {
                OnDebug(String.Format("AddAllowPreventScreenText({0})", allowPreventScreenText));
            }
            
            return bResult;
        }

        #endregion

        /// <summary>
        /// 작업을 시작 할 수 있는지 여부를 나타냅니다.
        /// </summary>
        public bool CanStartJob
        {
            get
            {
                if (ScreenManager.AllScreenLength == 1)
                {
                    OnWarn("ScreenManager.AllScreenLength == 1");
                    return false;
                }

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

        public bool StartJob(int hWnd, out StartJobResult startJobResult)
        {
            if (WindowsObject == WindowsObject.None)
            {
                OnInfo("WindowsObject == WindowsObject.None");
                startJobResult = StartJobResult.WindowsObjectValueNone;
                return false;
            }

            if (ScreenManager.OnlyOneIsScreenConnected)
            {
                OnWarn("ScreenManager.OnlyOneIsScreenConnected");
                startJobResult = StartJobResult.OnlyOneScreenIsConnected;
                return false;
            }

            OnInfo("현재 디스플레이 정보");
            OnInfo(ScreenManager.ToStringAllScreenBounds(Screen.AllScreens));
            OnInfo("정렬된 디스플레이 정보");
            OnInfo(ScreenManager.ToStringAllScreenBounds(ScreenManager.AsscendingAllScreens));

            if (!ScreenManager.IsLeftOrRightNearScreen(JobScreen, AdjustScreen))
            {
                OnWarn("서로 이웃하지 않은 디스플레이 설정입니다.");
                startJobResult = StartJobResult.LeftOrRightNearScreenEnvironmentIsNot;
                return false;
            }

            bool bResult = false;

            bool bStartTaskManagerProcessTabMainWindow = false;
            if (WindowsObject.HasFlag(WindowsObject.Window))
            {
                try
                {
                    StartTaskManagerProcessTabMainWindow(hWnd);
                    bStartTaskManagerProcessTabMainWindow = true;
                }
                catch (Exception ex)
                {
                    OnWarn(ex.StackTrace);
                    startJobResult = StartJobResult.ExceptionWindow;
                    return false;
                }
            }

            bool bStartAdjustMouseCursorPosition = false;
            if (WindowsObject.HasFlag(WindowsObject.Mouse))
            {
                try
                {
                    StartAdjustMouseCursorPosition();
                    bStartAdjustMouseCursorPosition = true;
                }
                catch (Exception ex)
                {
                    OnWarn(ex.StackTrace);
                    startJobResult = StartJobResult.ExceptionMouse;
                    return false;
                }
            }

            OnInfo("ScreenAllLength=" + ScreenAllLength);
            OnInfo("정상적인 환경입니다. 작업을 시작합니다.");
            ScreenAllLength = ScreenManager.AllScreenLength;
            DisplayEnvironment = DisplayEnvironment.Normal;
            
            if (Toolkit.IsCurrentProcessExecuteAdministrator())
            {
                OnInfo("관리자권한모드로 실행합니다.");
            }
            else
            {
                OnInfo("일반실행모드로 실행합니다.");
                OnInfo("윈도우에서 제공하는 기본 프로그램의 창들을 제어하기 위해서는 관리자 권한 실행이 필요할수도 있습니다");
            }

            if (WindowsObject.HasFlag(WindowsObject.All))
            {
                bResult = bStartTaskManagerProcessTabMainWindow && bStartAdjustMouseCursorPosition;
            }
            else if (WindowsObject.HasFlag(WindowsObject.Window))
            {
                bResult = bStartTaskManagerProcessTabMainWindow;
            }
            else if (WindowsObject.HasFlag(WindowsObject.Mouse))
            {
                bResult = bStartAdjustMouseCursorPosition;
            }

            startJobResult = StartJobResult.Success;
            return bResult;
        }

        #region Window
        internal void StartTaskManagerProcessTabMainWindow(int hWnd = 0)
        {
            OnDebug(String.Format("StartTaskManagerProcessTabMainWindow({0})", hWnd));
            User32.EnumWindows(new User32.EnumWindowsProcCallback(EnumWindowsProcMainWindow), hWnd);
        }

        internal bool EnumWindowsProcMainWindow(int hWnd, int lParam)
        {
            return true;
        }

        internal void StartTaskManagerProcessTabMainWindowInChildWindow(int hWnd)
        {
            OnWarn(String.Format("StartTaskManagerProcessTabMainWindowInChildWindow({0})", hWnd));
            User32.EnumWindows(new User32.EnumWindowsProcCallback(EnumWindowsProcMainWindowInChildWindow), hWnd);
        }

        internal bool EnumWindowsProcMainWindowInChildWindow(int hWndChild, int hWndParent)
        {
            return true;
        }

        #endregion

        #region Mouse
        internal void StartAdjustMouseCursorPosition()
        {
            if (!CanStartJob)
            {
                return;
            }

            Screen jobScreen = null;
            Screen adjustScreen = null;
            try
            {
                jobScreen = JobScreen;
                adjustScreen = AdjustScreen;

                Point currentMouseCursor = Cursor.Position;
                Point moveCurrentMouseCursor = Point.Empty;
                if (jobScreen.Bounds.Contains(currentMouseCursor))
                {
                    // adjustScreen 과 preventScreen 이 바로 옆에 있는지 여부
                    if (ScreenManager.IsLeftOrRightNearScreen(adjustScreen, jobScreen))
                    {
                        if (ScreenManager.IsRightScreen(adjustScreen, jobScreen))
                        {
                            int x = (adjustScreen.Bounds.Left + adjustScreen.Bounds.Width) - MouseCursorAdjustShiftPositionX;
                            int y = currentMouseCursor.Y;
                            moveCurrentMouseCursor.X = x;
                            moveCurrentMouseCursor.Y = y;
                        }
                        else
                        {
                            int x = adjustScreen.Bounds.Left + MouseCursorAdjustShiftPositionX;
                            int y = currentMouseCursor.Y;
                            moveCurrentMouseCursor.X = x;
                            moveCurrentMouseCursor.Y = y;
                        }

                        MouseNativeMethods.SetCursorPoint(moveCurrentMouseCursor.X, moveCurrentMouseCursor.Y);
                        OnDebug(String.Format("기존 마우스 좌표 {0} -> {1} 로 이동시켰습니다.", currentMouseCursor, moveCurrentMouseCursor));
                    }
                    else
                    {
                        OnWarn("조정해야 하는 스크린정보와 방지하는 스크린 정보가 서로 이웃되게(바로 옆에) 있지 않습니다.");
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                OnWarn(ex.StackTrace);
                return;
            }
        }
        #endregion
    }
}
