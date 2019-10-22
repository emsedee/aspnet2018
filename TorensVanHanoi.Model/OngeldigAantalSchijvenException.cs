using System;
using System.Collections.Generic;
using System.Text;

namespace TorensVanHanoi.Model
{

    [Serializable]
    public class OngeldigAantalSchijvenException : Exception
    {
        public OngeldigAantalSchijvenException() { }
        public OngeldigAantalSchijvenException(string message) : base(message) { }
        public OngeldigAantalSchijvenException(string message, Exception inner) : base(message, inner) { }
        protected OngeldigAantalSchijvenException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}