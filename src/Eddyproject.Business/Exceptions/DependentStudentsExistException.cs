using Eddyproject.Common.Model;
using System.Runtime.Serialization;

namespace Eddyproject.Business.Exceptions;

[Serializable]
public class DependentStudentsExistException : Exception
{
    public List<Student> Students { get;}

    public DependentStudentsExistException()
    {
    }

    public DependentStudentsExistException(List<Student> students)
    {
        this.Students = students;
    }

    public DependentStudentsExistException(string? message) : base(message)
    {
    }

    public DependentStudentsExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected DependentStudentsExistException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}