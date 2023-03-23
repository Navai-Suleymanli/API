namespace Eddyproject.Common.Model;

public class Budget : BaseEntity
{
    public string Currency { get; set; } = default!;

    public string Amount { get; set; } = default!;

    public List<Student> Students { get; set; } = default!;
}
