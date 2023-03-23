using System.Runtime.Serialization;

namespace Eddyproject.Business.Exceptions;

[Serializable]
public class BudgetNotFoundException : Exception
{
    public int Id;

    public BudgetNotFoundException()
    {
    }

    public BudgetNotFoundException(int id)
    {
        this.Id = id;
    }

    public BudgetNotFoundException(string? message) : base(message)
    {
    }

    public BudgetNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected BudgetNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}