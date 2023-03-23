namespace Eddyproject.Common.Model;

public class Student : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public Address Address { get; set; } = default!;

    public Budget Budget { get; set; } = default!;

    public List<Course> Courses { get; set; } = default!;
}
