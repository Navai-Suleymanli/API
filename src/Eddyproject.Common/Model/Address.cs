namespace Eddyproject.Common.Model;

public class Address : BaseEntity
{
    public string Country { get; set; } = default!;
    public string City { get; set; } = default!;

    public string Email { get; set; } = default!; // default! null ola bilmez demekdir

    public string? Phone { get; set; } //? null ola biler demekdir

    public List<Student> Students { get; set;} = default!;


}
