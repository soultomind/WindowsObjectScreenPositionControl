using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsObjectScreenPositionControl
{
    public enum StartJobResult
    {
        /// <summary>
        /// 성공
        /// </summary>
        Success,

        /// <summary>
        /// <see cref="WindowsObject.None"/> 일때
        /// </summary>
        WindowsObjectValueNone,

        /// <summary>
        /// <see cref="System.Windows.Forms.Screen.AllScreens"/>.Length 값이 1일때
        /// </summary>
        OnlyOneScreenIsConnected,

        /// <summary>
        /// JobScreen, AdjustScreen 환경이 서로이웃한 스크린환경이 아닐경우
        /// </summary>
        LeftOrRightNearScreenEnvironmentIsNot,

        /// <summary>
        /// 예외 (윈도우 작업)
        /// </summary>
        ExceptionWindow,

        /// <summary>
        /// 예외 (마우스 작업)
        /// </summary>
        ExceptionMouse,
    }
}
