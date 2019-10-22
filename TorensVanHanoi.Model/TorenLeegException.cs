using System;
using System.Collections.Generic;
using System.Text;

namespace TorensVanHanoi.Model
{

    [Serializable]
    public class TorenLeegException : Exception
    {
        public TorenLeegException() { }
        public TorenLeegException(string message) : base(message) { }
        public TorenLeegException(string message, Exception inner) : base(message, inner) { }
        protected TorenLeegException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}