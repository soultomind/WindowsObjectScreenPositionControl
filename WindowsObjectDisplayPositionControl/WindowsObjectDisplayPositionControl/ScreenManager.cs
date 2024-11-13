using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsObjectDisplayPositionControl
{
    public static class ScreenManager
    {
        /// <summary>
        /// 유효하지 않는 스크린 인덱스
        /// </summary>
        public const int InvalidSceenIndex = -1;

        /// <summary>
        /// 현재 스크린 수
        /// <para><see cref="System.Windows.Forms.Screen.AllScreens"/>.Length</para>
        /// </summary>
        public static int AllScreenLength
        {
            get { return Screen.AllScreens.Length; }
        }

        /// <summary>
        /// <paramref name="screenIndex"/> 값이 유효한 인덱스 값인지 여부를 반환합니다.
        /// </summary>
        /// <param name="screenIndex"></param>
        /// <returns></returns>
        public static bool IsValidIndex(int screenIndex)
        {
            if (screenIndex >= 0 && AllScreenLength > screenIndex)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 주 모니터 디스플레이 인덱스 값을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static int GetPrimaryScreenIndex()
        {
            if (AllScreenLength == 1)
            {
                return 0;
            }

            int primaryScreenIndex = 0;
            for (int index = 0; index < AllScreenLength; index++)
            {
                if (Screen.AllScreens[index].Equals(Screen.PrimaryScreen))
                {
                    primaryScreenIndex = index;
                    break;
                }
            }
            return primaryScreenIndex;
        }

        /// <summary>
        /// <paramref name="screen"/> 값에 해당하는 인덱스(<see cref="int"/>)를 반환합니다.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static int GetIndexByScreen(Screen screen)
        {
            int screenIndex = InvalidSceenIndex;
            for (int index = 0; index < AllScreenLength; index++)
            {
                if (screen.Equals(Screen.AllScreens[index]))
                {
                    screenIndex = index;
                    break;
                }
            }

            return screenIndex;
        }

        /// <summary>
        /// <paramref name="screenIndex"/> 값에 해당하는 스크린(<see cref="Screen"/>)을 반환합니다.
        /// </summary>
        /// <param name="screenIndex"></param>
        /// <returns></returns>
        public static Screen GetScreenByIndex(int screenIndex)
        {
            if (screenIndex == InvalidSceenIndex)
            {
                throw new ArgumentException(nameof(screenIndex));
            }

            Screen screen = null;
            for (int i = 0; i < AllScreenLength; i++)
            {
                if (i == screenIndex)
                {
                    screen = Screen.AllScreens[i];
                    break;
                }
            }
            return screen;
        }

        /// <summary>
        /// 스크린(<paramref name="screen"/>)사이즈가 너비보다 높이가 큰지 여부를 반환합니다.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static bool IsPortraitScreen(Screen screen)
        {
            if (screen.Bounds.Height > screen.Bounds.Width)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 스크린(<paramref name="screen"/>)사이즈가 높이보다 너비가 큰지 여부를 반환합니다.
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static bool IsLandScapeScreen(Screen screen)
        {
            if (screen.Bounds.Height < screen.Bounds.Width)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 가로보다 세로가 큰 스크린인덱스 값을 가져온다. 
        /// <para>가로보다 세로가 큰 스크린이 2개이상일 경우 먼저 발견된 스크린 인덱스 값을 <paramref name="screenIndex"/> 값에 설정</para>
        /// </summary>
        /// <param name="screenIndex"></param>
        /// <returns><paramref name="screenIndex"/> 값 찾아서 설정 여부</returns>
        public static bool TryGetPortraitScreenIndex(out int screenIndex)
        {
            screenIndex = InvalidSceenIndex;
            for (int index = 0; index < AllScreenLength; index++)
            {
                Screen screen = Screen.AllScreens[index];
                if (IsPortraitScreen(screen))
                {
                    screenIndex = index;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// <paramref name="thisScreen"/> 스크린 값과 <paramref name="otherScreen"/> 스크린 값이 서로 이웃하고 있는지 여부를 나타냅니다.
        /// </summary>
        /// <param name="thisScreen"></param>
        /// <param name="otherScreen"></param>
        /// <returns></returns>
        public static bool IsLeftOrRightNearScreen(Screen thisScreen, Screen otherScreen)
        {
            if (IsLeftScreen(thisScreen, otherScreen))
            {
                if (thisScreen.Bounds.Left == (otherScreen.Bounds.Left + otherScreen.Bounds.Width))
                {
                    return true;
                }
                return false;
            }
            else
            {
                if ((thisScreen.Bounds.Left + thisScreen.Bounds.Width) == otherScreen.Bounds.Left)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// <paramref name="thisScreen"/> 스크린 값이 <paramref name="otherScreen"/> 스크린 값보다 오른쪽에 있는지 여부를 나타냅니다.
        /// </summary>
        /// <param name="thisScreen"></param>
        /// <param name="otherScreen"></param>
        /// <returns></returns>
        public static bool IsRightScreen(Screen thisScreen, Screen otherScreen)
        {
            if (thisScreen.Bounds.Left < otherScreen.Bounds.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// <paramref name="thisScreen"/> 스크린 값이 <paramref name="otherScreen"/> 스크린 값보다 왼쪽에 있는지 여부를 나타냅니다.
        /// </summary>
        /// <param name="thisScreen">기준이 되는 스크린</param>
        /// <param name="otherScreen">다른 스크린</param>
        /// <returns></returns>
        public static bool IsLeftScreen(Screen thisScreen, Screen otherScreen)
        {
            if (thisScreen.Bounds.Left > otherScreen.Bounds.Left)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
