
using System;
using log4net;
using log4net.Config;

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "JohnsonControls.Metasys.BasicServices.Log4NetLog..cctor()")]

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// The Logger class is used by applications to log messages to a common location
    /// </summary>
    public sealed class Log4NetLog : ILog
    {
        private readonly log4net.ILog log4Net;
        private const string ConfigFileName = "MetasysService.Logging.config";

        /// <summary>
        /// Constructs a logger for the specified source.
        /// </summary>
        /// <param name="source">
        /// The source of the log message. Typically the fully-qualified name of the class
        /// the log messages.
        /// </param>
        public Log4NetLog(String source)
        {
            log4Net = LogManager.GetLogger(Type.GetType(source));
        }

        #region ILog Members

        /// <summary>
        /// Create a message in the log
        /// </summary>
        /// <param name="level">The level of detail associated with this message</param>
        /// <param name="message">Text to be logged</param>
        public void Write(Level level, String message)
        {
            Write(level, message, null);
        }

        /// <summary>
        /// Create a message in the log
        /// </summary>
        /// <param name="level">The level of detail associated with this message</param>
        /// <param name="message">Text to be logged</param>
        /// <param name="exception">The exception that caused us to create this message</param>
        public void Write(Level level, String message, Exception exception)
        {
            switch (level)
            {
                case Level.Error:
                    log4Net.Error(message, exception);
                    break;
                case Level.Warning:
                    log4Net.Warn(message, exception);
                    break;
                case Level.Info:
                    log4Net.Info(message, exception);
                    break;
                case Level.Verbose:
                    log4Net.Debug(message, exception);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Indicates whether writing at the specified level would result in an 
        /// actual log message being generated.
        /// </summary>
        /// <param name="level">The level to check.</param>
        /// <returns><b>True</b> if a log message would be generated.</returns>
        public bool IsEnabled(Level level)
        {
            switch (level)
            {
                case Level.Error:
                    return log4Net.IsErrorEnabled;
                case Level.Warning:
                    return log4Net.IsWarnEnabled;
                case Level.Info:
                    return log4Net.IsInfoEnabled;
                case Level.Verbose:
                    return log4Net.IsDebugEnabled;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Returns <b>True</b> if a error messages would actually be generated.
        /// </summary>
        public bool IsError { get { return IsEnabled(Level.Error); } }

        /// <summary>
        /// Returns <b>True</b> if a warning messages would actually be generated.
        /// </summary>
        public bool IsWarning { get { return IsEnabled(Level.Warning); } }

        /// <summary>
        /// Returns <b>True</b> if a info messages would actually be generated.
        /// </summary>
        public bool IsInfo { get { return IsEnabled(Level.Info); } }

        /// <summary>
        /// Returns <b>True</b> if a verbose messages would actually be generated.
        /// </summary>
        public bool IsVerbose { get { return IsEnabled(Level.Verbose); } }

        /// <summary>
        /// Write a <see cref="Level.Error" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        public void Error(String message)
        {
            Write(Level.Error, message, null);
        }

        /// <summary>
        /// Write a <see cref="Level.Error" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="exception">Exception to be logged.</param>
        public void Error(String message, Exception exception)
        {
            Write(Level.Error, message, exception);
        }

        /// <summary>
        /// Write a <see cref="Level.Warning" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        public void Warning(String message)
        {
            Write(Level.Warning, message, null);
        }

        /// <summary>
        /// Write a <see cref="Level.Warning" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="exception">Exception to be logged.</param>
        public void Warning(String message, Exception exception)
        {
            Write(Level.Warning, message, exception);
        }

        /// <summary>
        /// Write a <see cref="Level.Info" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        public void Info(String message)
        {
            Write(Level.Info, message, null);
        }

        /// <summary>
        /// Write a <see cref="Level.Info" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="exception">Exception to be logged.</param>
        public void Info(String message, Exception exception)
        {
            Write(Level.Info, message, exception);
        }

        /// <summary>
        /// Write a <see cref="Level.Verbose" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        public void Verbose(String message)
        {
            Write(Level.Verbose, message, null);
        }

        /// <summary>
        /// Write a <see cref="Level.Verbose" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="exception">Exception to be logged.</param>
        public void Verbose(String message, Exception exception)
        {
            Write(Level.Verbose, message, exception);
        }

        #endregion
    }
}