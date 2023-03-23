using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eddyproject.Common.Dtos.Course;

namespace Eddyproject.Common.Interfaces;


public interface ICourseService
{
    Task<int> CreateCourseAsync(CourseCreate courseCreate);
    Task UpdateCourseAsync(CourseUpdate courseUpdate);
    Task<List<CourseGet>> GetCoursesAsync();
    Task<CourseGet> GetCourseAsync(int id);
    Task DeleteCourseAsync(CourseDelete courseDelete);
}
