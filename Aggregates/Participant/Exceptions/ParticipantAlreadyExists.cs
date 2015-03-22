using System;
using System.Runtime.Serialization;

namespace MBACNationals.Participant
{
    public class ParticipantAlreadyExists : Exception, ISerializable
    {
        public ParticipantAlreadyExists()
        {
            // Add implementation.
        }
        public ParticipantAlreadyExists(string message)
        {
            // Add implementation.
        }
        public ParticipantAlreadyExists(string message, Exception inner)
        {
            // Add implementation.
        }

        // This constructor is needed for serialization.
        protected ParticipantAlreadyExists(SerializationInfo info, StreamingContext context)
        {
            // Add implementation.
        }
    }
}
