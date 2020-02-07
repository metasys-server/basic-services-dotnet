
namespace JohnsonControls.Metasys.BasicServices
{
    /// <summary>
    /// Describes a service for providing insight diagnostic information 
    /// </summary>
    public interface IDiagnosticModule
    {
        /// <summary>
        /// Returns a string containing diagnostic or debugging information about implementor
        /// </summary>
        /// <param name="diagnosticLevel">DiagnosticLevel enum</param>
        /// <returns>String</returns>
        string GetDiagnosticInformation(DiagnosticLevel diagnosticLevel);
    }
}
