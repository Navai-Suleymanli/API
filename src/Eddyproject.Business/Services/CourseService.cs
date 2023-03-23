using AutoMapper;
using Eddyproject.Common.Dtos.Course;
using Eddyproject.Common.Interfaces;
using Eddyproject.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Eddyproject.Business.Services;

public class CourseService : ICourseService
{
    private IGenericRepository<Course> CourseRepository { get; }
    private IGenericRepository<Student> StudentRepository { get; }
    private IMapper Mapper { get; }

    public CourseService(IGenericRepository<Course> courseRepository, IGenericRepository<Student> studentRepository,
        IMapper mapper)
    {
        CourseRepository = courseRepository;
        StudentRepository = studentRepository;
        Mapper = mapper;
    }


    public async Task<int> CreateCourseAsync(CourseCreate courseCreate)
    {
        Expression<Func<Student, bool>> studentFilter = (Student) => courseCreate.Students.Contains(Student.Id);
        var students = await StudentRepository.GetFilteredAsync(new[] { studentFilter }, null, null);
        var entity = Mapper.Map<Course>(courseCreate);
        entity.Students = students;
        await CourseRepository.InsertAsync(entity);
        await CourseRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteCourseAsync(CourseDelete courseDelete)
    {
        var entity = await CourseRepository.GetByIdAsync(courseDelete.Id);
        CourseRepository.Delete(entity);
        await CourseRepository.SaveChangesAsync();
    }

    public async Task<CourseGet> GetCourseAsync(int id)
    {
        var entity = await CourseRepository.GetByIdAsync(id, (team) => team.Students);
        return Mapper.Map<CourseGet>(entity);
    }

    public async Task<List<CourseGet>> GetCoursesAsync()
    {
        var entities = await CourseRepository.GetAsync(null, null, (course) => course.Students);
        return Mapper.Map<List<CourseGet>>(entities);
    }

    public async Task UpdateCourseAsync(CourseUpdate courseUpdate)
    {
        Expression<Func<Student, bool>> studentFilter = (Student) => courseUpdate.Students.Contains(Student.Id);
        var students = await StudentRepository.GetFilteredAsync(new[] { studentFilter }, null, null);
        var existingEntity = await CourseRepository.GetByIdAsync(courseUpdate.Id, (course) => course.Students);
        Mapper.Map(courseUpdate, existingEntity);
        existingEntity.Students = students;
        CourseRepository.Update(existingEntity);
        await CourseRepository.SaveChangesAsync();
    }
}
