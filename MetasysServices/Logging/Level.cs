

namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// Defines the levels of log messages.
    /// </summary>
    public enum Level
    {
        /// <summary>
        /// A system or major subsystem is not functional. Recovery is not possible.
        /// </summary>
        Catastrophic = 9000,

        /// <summary>
        /// An error occurred but recovery is possible. It is likely the system or
        /// subsystem is still functional.
        /// </summary>
        Error = 8000,

        /// <summary>
        /// Some system or subsystem may have experience a problem but it
        /// is not serious and is still functioning properly.
        /// </summary>
        Warning = 7000,

        /// <summary>
        /// Informational message.
        /// </summary>
        Info = 6000,

        /// <summary>
        /// Detailed information about a system or subsystem
        /// </summary>
        Verbose = 1000,

        /// <summary>
        /// Logging is disabled.
        /// </summary>
        Off = 0
    }
}