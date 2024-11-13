using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsObjectDisplayPositionControl
{
    partial class WindowsObjectScreenPositionManager
    {
        public event EventHandler<ExternalLoggingEventArgs> ExternalLogging;

        internal bool CanExternalLogging
        {
            get { return ExternalLogging != null; }
        }

        internal void OnExternalLogging(ExternalLoggingEventArgs e)
        {
            if (CanExternalLogging)
            {
                ExternalLogging(this, e);
            }
            else
            {
                if (e.IsError)
                {
                    Toolkit.TraceWriteLine(e.Message);
                    Toolkit.TraceWriteLine(e.Exception.StackTrace);
                }
                else
                {
                    Toolkit.TraceWriteLine(e.Message);
                }
            }
        }

        internal void OnTrace(string message)
        {
            OnExternalLogging(new ExternalLoggingEventArgs(message)
            {
                Level = LogLevel.Trace
            });
        }

        internal void OnDebug(string message)
        {
            OnExternalLogging(new ExternalLoggingEventArgs(message)
            {
                Level = LogLevel.Debug
            });
        }

        internal void OnInfo(string message)
        {
            OnExternalLogging(new ExternalLoggingEventArgs(message)
            {
                Level = LogLevel.Info
            });
        }

        internal void OnWarn(string message)
        {
            OnExternalLogging(new ExternalLoggingEventArgs(message)
            {
                Level = LogLevel.Warn
            });
        }

        internal void OnWarn(Exception exception)
        {
            OnExternalLogging(new ExternalLoggingEventArgs(exception)
            {
                Level = LogLevel.Warn
            });
        }

        internal void OnWarn(string message, Exception exception)
        {
            OnExternalLogging(new ExternalLoggingEventArgs(message, exception)
            {
                Level = LogLevel.Warn
            });
        }

        internal void OnError(string message)
        {
            OnExternalLogging(new ExternalLoggingEventArgs(message)
            {
                Level = LogLevel.Error
            });
        }

        internal void OnError(Exception exception)
        {
            OnExternalLogging(new ExternalLoggingEventArgs(exception)
            {
                Level = LogLevel.Error
            });
        }

        internal void OnError(string message, Exception exception)
        {
            OnExternalLogging(new ExternalLoggingEventArgs(message, exception)
            {
                Level = LogLevel.Error
            });
        }
    }
}
