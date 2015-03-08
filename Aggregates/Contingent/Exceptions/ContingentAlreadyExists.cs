using System;
using System.Runtime.Serialization;

namespace MBACNationals.Contingent
{
    public class ContingentAlreadyExists : Exception, ISerializable
    {
        public ContingentAlreadyExists()
        {
            // Add implementation.
        }
        public ContingentAlreadyExists(string message)
        {
            // Add implementation.
        }
        public ContingentAlreadyExists(string message, Exception inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected ContingentAlreadyExists(SerializationInfo info, StreamingContext context)
        {
            // Add implementation.
        }
    }
}
