using System.Runtime.Serialization;

namespace Eddyproject.Business.Exceptions;

[Serializable]
public class StudentNotFoundException : Exception
{
    public int Id {get; }

    public StudentNotFoundException()
    {
    }

    public StudentNotFoundException(int id)
    {
        this.Id = id;
    }

    public StudentNotFoundException(string? message) : base(message)
    {
    }

    public StudentNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected StudentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}