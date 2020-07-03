namespace Fur.FastMember
{
    /// <summary>
    /// Emphasizes column position used in <see cref="System.Data.IDataReader"/> instance.
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, AllowMultiple = false)]
    public class OrdinalAttribute : System.Attribute
    {
        /// <summary>
        /// Creates a new instance of <see cref="OrdinalAttribute"/> class.
        /// </summary>
        /// <param name="ordinal"></param>
        public OrdinalAttribute(ushort ordinal)
        {
            Ordinal = ordinal;
        }

        /// <summary>
        /// Column ordinal used in <see cref="System.Data.IDataReader"/> instance.
        /// </summary>
        public ushort Ordinal { get; private set; }
    }
}
