using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace WindowsObjectDisplayPositionControl
{
    internal static class Toolkit
    {
        /// <summary>
        /// DebugView Filter 이름
        /// <para>Filter/Hightlight 메뉴 Include 항목에 사용될 값</para>
        /// </summary>
        public static string IncludeFilterName;

        /// <summary>
        /// <see cref="Debug.WriteLine(object)"/>,<see cref="Debug.Write(object)"/>
        /// <para>메서드 출력 여부</para>
        /// </summary>
        public static bool IsDebugEnabled;

        /// <summary>
        /// <see cref="Trace.WriteLine(object)"/>,<see cref="Trace.Write(object)"/>
        /// <para>메서드 출력 여부</para>
        /// </summary>
        public static bool IsTraceEnabled;

        /// <summary>
        /// 메시지 출력시 현재시간 출력 여부
        /// </summary>
        public static bool UseNowToString;
        static Toolkit()
        {

            IncludeFilterName = CreateNamespace();
#if DEBUG
            IsDebugEnabled = true;
#else
            IsDebugEnabled = false;
#endif
            IsTraceEnabled = true;
            UseNowToString = false;
        }

        private static string CreateNamespace()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(Toolkit));
            return assembly.GetName().Name;
        }

        private static string NowToString(string format = "yyyy/MM/dd HH:mm:ss")
        {
            return DateTime.Now.ToString(format);
        }

        private static string MakeMessage(string header, string message)
        {
            if (UseNowToString)
            {
#if DEBUG
                message = String.Format("[{0}] {1} [{2}] DEBUG - {3}", IncludeFilterName, NowToString(), header, message);
#else
                message = String.Format("[{0}] {1} [{2}] TRACE - {3}", IncludeFilterName, NowToString(), header, message);
#endif
            }
            else
            {
#if DEBUG
                message = String.Format("[{0}] [{1}] DEBUG - {2}", IncludeFilterName, header, message);
#else
                message = String.Format("[{0}] [{1}] TRACE - {2}", IncludeFilterName, header, message);
#endif
            }
            return message;
        }

        private static string MakeStackFrameHeader(int skipFrames = 2)
        {
            StackFrame sf = new StackFrame(skipFrames, true);
            string className = sf.GetMethod().ReflectedType.Name;
            string methodName = sf.GetMethod().Name;
            string header = String.Format("{0} :: {1}", className, methodName);
            return header;
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Debug.WriteLine(object)"/>을 활용하여 메시지를 출력합니다.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="skipFrames"></param>
        internal static void DebugWriteLine(string message, int skipFrames = 2)
        {
            if (IsDebugEnabled)
            {
                string header = MakeStackFrameHeader(skipFrames);
                message = MakeMessage(header, message);
                Debug.WriteLine(message);
            }
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Debug.WriteLine(object)"/>을 활용하여 예외를 출력합니다.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="skipFrames"></param>
        internal static void DebugWriteLine(Exception ex, int skipFrames = 2)
        {
            string header = MakeStackFrameHeader(skipFrames);

            string message = MakeMessage(header, ex.Message);
            DebugWriteLine(message);

            message = MakeMessage(header, ex.StackTrace);
            DebugWriteLine(message);
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Debug.Write(object)"/>을 활용하여 메시지를 출력합니다.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="skipFrames"></param>
        internal static void DebugWrite(string message, int skipFrames = 2)
        {
            if (IsDebugEnabled)
            {
                string header = MakeStackFrameHeader(skipFrames);
                message = MakeMessage(header, message);
                Debug.Write(message);
            }
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Trace.WriteLine(object)"/>을 활용하여 메시지를 출력합니다.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="skipFrames"></param>
        internal static void TraceWriteLine(string message, int skipFrames = 2)
        {
            if (IsTraceEnabled)
            {
                string header = MakeStackFrameHeader(skipFrames);
                message = MakeMessage(header, message);
                Trace.WriteLine(message);
            }
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Trace.WriteLine(object)"/>을 활용하여 예외를 출력합니다.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="skipFrames"></param>
        internal static void TraceWriteLine(Exception ex, int skipFrames = 2)
        {
            string header = MakeStackFrameHeader(skipFrames);

            string message = MakeMessage(header, ex.Message);
            TraceWriteLine(message);

            message = MakeMessage(header, ex.StackTrace);
            TraceWriteLine(message);
        }

        /// <summary>
        /// <see cref="System.Diagnostics.Trace.Write(object)"/>을 활용하여 메시지를 출력합니다.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="skipFrames"></param>
        internal static void TraceWrite(string message, int skipFrames = 2)
        {
            if (IsTraceEnabled)
            {
                string header = MakeStackFrameHeader(skipFrames);
                message = MakeMessage(header, message);
                Trace.Write(message);
            }
        }

        internal static bool IsCurrentProcessExecuteAdministrator()
        {
            bool flag;

            WindowsIdentity identity = null;
            try
            {
                identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                flag = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception)
            {
                flag = false;
            }
            finally
            {
                if (identity != null)
                {
                    identity.Dispose();
                }
            }

            return flag;
        }
    }
}
