using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsObjectScreenPositionControl
{
    [FlagsAttribute()]
    public enum WindowsObject
    {
        /// <summary>
        /// 사용안함
        /// </summary>
        None,

        /// <summary>
        /// 마우스
        /// </summary>
        Mouse = 1,

        /// <summary>
        /// 윈도우
        /// </summary>
        Window = 2,

        /// <summary>
        /// 모두(마우스(<see cref="WindowsObject.Mouse"/>), 윈도우(<see cref="WindowsObject.Window"/>))
        /// </summary>
        All = Mouse | Window
    }
}
