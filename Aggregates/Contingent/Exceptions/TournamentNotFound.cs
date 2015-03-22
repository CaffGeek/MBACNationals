using System;
using System.Runtime.Serialization;

namespace MBACNationals.Contingent
{
    public class TournamentNotFound : Exception, ISerializable
    {
        public TournamentNotFound()
        {
            // Add implementation.
        }
        public TournamentNotFound(string message)
        {
            // Add implementation.
        }
        public TournamentNotFound(string message, Exception inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected TournamentNotFound(SerializationInfo info, StreamingContext context)
        {
            // Add implementation.
        }
    }
}
