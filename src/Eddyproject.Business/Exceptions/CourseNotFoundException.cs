using System.Runtime.Serialization;

namespace Eddyproject.Business.Exceptions;

[Serializable]
public class CourseNotFoundException : Exception
{
    public int Id { get; }

    public CourseNotFoundException()
    {
    }

    public CourseNotFoundException(int id)
    {
        this.Id = id;
    }

    public CourseNotFoundException(string? message) : base(message)
    {
    }

    public CourseNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected CourseNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}