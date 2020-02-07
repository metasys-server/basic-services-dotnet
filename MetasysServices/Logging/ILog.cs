
using System;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// Defines methods for logging diagnostic messages.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Create a message in the log.
        /// </summary>
        /// <param name="level">Log Level</param>
        /// <param name="message">Message to be logged</param>
        void Write(Level level, String message);

        /// <summary>
        /// Create a message in the log
        /// </summary>
        /// <param name="level">Log Level</param>
        /// <param name="message">Message to be logged</param>
        /// <param name="exception">system exception which occurred</param>
        void Write(Level level, String message, Exception exception);

        /// <summary>
        /// Write a <see cref="Level.Error" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        void Error(String message);

        /// <summary>
        /// Write a <see cref="Level.Error" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="exception">Exception to be logged.</param>
        void Error(String message, Exception exception);

        /// <summary>
        /// Write a <see cref="Level.Warning" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        void Warning(String message);

        /// <summary>
        /// Write a <see cref="Level.Warning" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="exception">Exception to be logged.</param>
        void Warning(String message, Exception exception);

        /// <summary>
        /// Write a <see cref="Level.Info" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        void Info(String message);

        /// <summary>
        /// Write a <see cref="Level.Info" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="exception">Exception to be logged.</param>
        void Info(String message, Exception exception);

        /// <summary>
        /// Write a <see cref="Level.Verbose" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        void Verbose(String message);

        /// <summary>
        /// Write a <see cref="Level.Verbose" /> message in the log.
        /// </summary>
        /// <param name="message">Message to be logged.</param>
        /// <param name="exception">Exception to be logged.</param>
        void Verbose(String message, Exception exception);

        /// <summary>
        /// Indicates whether writing at the specified level would result in an 
        /// actual log message being generated.
        /// </summary>
        /// <param name="level">The level to check.</param>
        /// <returns><b>True</b> if a log message would be generated.</returns>
        Boolean IsEnabled(Level level);

        /// <summary>
        /// Returns <b>True</b> if a error message would actually be generated.
        /// </summary>
        Boolean IsError { get; }

        /// <summary>
        /// Returns <b>True</b> if a warning message would actually be generated.
        /// </summary>
        Boolean IsWarning { get; }

        /// <summary>
        /// Returns <b>True</b> if a info message would actually be generated.
        /// </summary>
        Boolean IsInfo { get; }

        /// <summary>
        /// Returns <b>True</b> if a verbose message would actually be generated.
        /// </summary>
        Boolean IsVerbose { get; }
    }
}