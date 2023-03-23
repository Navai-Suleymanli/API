using Eddyproject.Common.Dtos.Student;
namespace Eddyproject.Common.Interfaces;

public interface IStudentService
{
    Task<int> CreateStudentAsync(StudentCreate studentCreate);
    Task UpdateStudentAsync(StudentUpdate studentUpdate);

    Task<List<StudentList>> GetStudentsAsync(StudentFilter studentFilter);
    Task<StudentDetails> GetStudentAsync(int Id);

    Task DeleteStudentAsync(StudentDelete studentDelete);
}
