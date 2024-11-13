using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsObjectDisplayPositionControl.Extension
{
    public static class ScreenExtension
    {
        /// <summary>
        /// <paramref name="screen"/> 값의 스크린이 작업표시줄 영역이 존재하는지 여부
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static bool UseTaskBarArea(this Screen screen)
        {
            if ((screen.WorkingArea.Width == screen.Bounds.Width) && (screen.WorkingArea.Height == screen.Bounds.Height))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 사용자 작업영역을 반환합니다.
        /// <para>
        /// <paramref name="screen"/> 값의 스크린이 작업표시줄 영역이 있는지 체크하여 있으면 <see cref="Screen.WorkingArea"/> 없으면 <see cref="Screen.Bounds"/> 값을 반환합니다.
        /// </para>
        /// </summary>
        /// <param name="screen"></param>
        /// <returns></returns>
        public static Rectangle UserWorkingArea(this Screen screen)
        {
            if (screen.UseTaskBarArea())
            {
                return screen.WorkingArea;
            }
            else
            {
                return screen.Bounds;
            }
        }
    }
}
