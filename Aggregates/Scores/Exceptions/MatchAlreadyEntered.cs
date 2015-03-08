using System;
using System.Runtime.Serialization;

namespace MBACNationals.Scores
{
    public class MatchAlreadyEntered : Exception, ISerializable
    {
        public MatchAlreadyEntered()
        {
            // Add implementation.
        }
        public MatchAlreadyEntered(string message)
        {
            // Add implementation.
        }
        public MatchAlreadyEntered(string message, Exception inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected MatchAlreadyEntered(SerializationInfo info, StreamingContext context)
        {
            // Add implementation.
        }
    }
}
