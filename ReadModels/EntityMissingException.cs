using System;

namespace MBACNationals.ReadModels
{
    [Serializable]
    public class EntityMissingException : Exception
    {
        public EntityMissingException() { }
        public EntityMissingException(string message) : base(message) { }
        public EntityMissingException(string message, Exception inner) : base(message, inner) { }
        protected EntityMissingException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
