using Eddyproject.Common.Dtos.Student;

namespace Eddyproject.Common.Dtos.Course;

public record CourseUpdate(int Id, string Name, List<int> Students);