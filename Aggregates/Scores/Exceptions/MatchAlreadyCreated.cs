using System;
using System.Runtime.Serialization;

namespace MBACNationals.Scores
{
    public class MatchAlreadyCreated : Exception, ISerializable
    {
        public MatchAlreadyCreated()
        {
            // Add implementation.
        }
        public MatchAlreadyCreated(string message)
        {
            // Add implementation.
        }
        public MatchAlreadyCreated(string message, Exception inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected MatchAlreadyCreated(SerializationInfo info, StreamingContext context)
        {
            // Add implementation.
        }
    }
}
