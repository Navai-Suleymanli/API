namespace Eddyproject.Common.Dtos.Student;

public record StudentUpdate(int Id, string FirstName, string LastName, int AddressId, int   CourseId)
{
    public int BudgetId;
}
