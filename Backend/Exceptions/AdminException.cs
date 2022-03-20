using System;
using System.Runtime.Serialization;

namespace CodingDays.Exceptions;
[Serializable]
public class AdminException : Exception
{
    public AdminException() { }
    public AdminException(string message) : base(message) { }
    public AdminException(string message, Exception inner) : base(message, inner) { }
    protected AdminException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
