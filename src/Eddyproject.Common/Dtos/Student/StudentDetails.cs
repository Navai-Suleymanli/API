using Eddyproject.Common.Dtos.Address;
using Eddyproject.Common.Dtos.Budget;
using Eddyproject.Common.Dtos.Course;

namespace Eddyproject.Common.Dtos.Student;

//to do: add students
public record StudentDetails(int Id, string FirstName, string LastName, AddressGet address, BudgetGet budget, List<CourseGet> Courses);