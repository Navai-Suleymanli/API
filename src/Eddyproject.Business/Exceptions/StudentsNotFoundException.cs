using System.Runtime.Serialization;

namespace Eddyproject.Business.Exceptions;

[Serializable]
public class StudentsNotFoundException : Exception
{
    public int[]StudentIds { get; }

    public StudentsNotFoundException()
    {
    }

    public StudentsNotFoundException(int[] studentIds)
    {
        StudentIds = studentIds;
    }

    public StudentsNotFoundException(string? message) : base(message)
    {
    }

    public StudentsNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected StudentsNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}