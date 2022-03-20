using System;
using System.Runtime.Serialization;

namespace CodingDays.Exceptions;
[Serializable]
public class UsageException : Exception
{
    public UsageException() { }
    public UsageException(string message) : base(message) { }
    public UsageException(string message, Exception inner) : base(message, inner) { }
    protected UsageException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
