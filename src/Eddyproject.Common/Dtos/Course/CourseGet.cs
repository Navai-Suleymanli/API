using Eddyproject.Common.Dtos.Student;

namespace Eddyproject.Common.Dtos.Course;

public record CourseGet(int Id, string Name, List<StudentList> Students);