namespace JohnsonControls.Metasys.ComServices
{
    /// <summary>
    /// Data value prior to the Audit.
    /// </summary>
    public class ComDataModel
    {
        /// <summary>
        /// The past or present value of the audit.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// The type of data value should be parsed into.
        /// See /enumSets/501/members for possible values.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The unit of measurement this data uses.
        /// See /enumSets/507/members for possible values.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// The precision, number of decimal places, this data uses.
        /// See /enumSets/0/members for possible values.
        /// </summary>
        public string Precision { get; set; }
    }
}
