using System;
using System.Runtime.Serialization;

namespace MBACNationals.Tournament
{
    public class TournamentAlreadyExists : Exception, ISerializable
    {
        public TournamentAlreadyExists()
        {
            // Add implementation.
        }
        public TournamentAlreadyExists(string message)
        {
            // Add implementation.
        }
        public TournamentAlreadyExists(string message, Exception inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected TournamentAlreadyExists(SerializationInfo info, StreamingContext context)
        {
            // Add implementation.
        }
    }
}
