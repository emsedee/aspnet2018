using System;
using System.Collections.Generic;
using System.Text;

namespace TorensVanHanoi.Model
{

    [Serializable]
    public class SchijfTeGrootException : Exception
    {
        public SchijfTeGrootException() { }
        public SchijfTeGrootException(string message) : base(message) { }
        public SchijfTeGrootException(string message, Exception inner) : base(message, inner) { }
        protected SchijfTeGrootException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}