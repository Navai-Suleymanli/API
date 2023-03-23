namespace Eddyproject.Common.Model;

public class Course : BaseEntity
{
    public string Name { get; set; } = default!;

    public List<Student> Students { get; set; } = default!;
}
