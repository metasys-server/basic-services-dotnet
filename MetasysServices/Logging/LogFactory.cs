
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using log4net;
using log4net.Appender;
using log4net.Repository;

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// Class for diagnostic logging
    /// </summary>
    public class LogFactory : ILogFactory, IDiagnosticModule
    {
        // ReSharper disable once InconsistentNaming
        private static LogFactory instance;

        /// <summary>
        /// Gets instance of log factory
        /// </summary>
        public static LogFactory Instance
        {
            get
            {
                return instance = instance ?? new LogFactory();
            }
        }

        /// <summary>
        /// Creates a diagnostic logger
        /// </summary>
        /// <param name="source">The specified source of logger</param>
        /// <returns>A diagnostic logger</returns>
        public ILog CreateLogger(string source)
        {
            return new Log4NetLog(source);
        }

        /// <summary>
        /// Returns diagnostic information
        /// </summary>
        /// <param name="diagnosticLevel"></param>
        /// <returns>string</returns>
        [ExcludeFromCodeCoverage]
        public string GetDiagnosticInformation(DiagnosticLevel diagnosticLevel)
        {
            ILog logger = CreateLogger("MetasysService.Logging");
            const int maxStringSize = 512000; // 500k
            ILoggerRepository[] repositories = LogManager.GetAllRepositories();
            foreach (ILoggerRepository repository in repositories)
            {
                foreach (IAppender appender in repository.GetAppenders())
                {
                    RollingFileAppender rollingFileAppender = appender as RollingFileAppender;
                    if (rollingFileAppender != null)
                    {
                        string filename = rollingFileAppender.File;
                        if (File.Exists(filename))
                        {
                            try
                            {
                                using (FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                                {
                                    string logText;
                                    using (StreamReader reader = new StreamReader(stream))
                                    {
                                        logText = reader.ReadToEnd();
                                    }
                                    logText = (logText.Length > maxStringSize)
                                        ? logText.Substring(logText.Length - maxStringSize, maxStringSize)
                                        : logText;
                                    string[] logLines = logText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                                    List<string> logArray = logLines.ToList();
                                    logArray.Reverse();
                                    logText = string.Join(Environment.NewLine, logArray);
                                    return String.Format("{0}{1}{2}", "<pre>", logText, "</pre>");
                                }
                            }
                            catch
                            {
                                logger.Error(String.Format("Could not open or read the existing RollingFileAppender log file : {0}.", filename));
                            }
                        }
                    }
                }
            }
            return "Current log file did not exist or could not be opened.";
        }
    }
}
