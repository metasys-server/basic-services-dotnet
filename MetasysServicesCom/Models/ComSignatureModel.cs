namespace JohnsonControls.Metasys.ComServices
{
    /// <summary>
    /// The user who created this audit.
    /// </summary>
    public class ComSignatureModel
    {
        /// <summary>
        /// The name of the person "signing".
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The reason for signing.
        /// </summary>
        public string Reason { get; set; }
    }
}
