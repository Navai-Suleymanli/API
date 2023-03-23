using AutoMapper;
using Eddyproject.Business.Exceptions;
using Eddyproject.Business.Validation;
using Eddyproject.Common.Dtos.Course;
using Eddyproject.Common.Interfaces;
using Eddyproject.Common.Model;
using FluentValidation;
using System.Linq.Expressions;


namespace Eddyproject.Business.Services;

public class CourseService : ICourseService
{
    private IGenericRepository<Course> CourseRepository { get; }
    private IGenericRepository<Student> StudentRepository { get; }
    private IMapper Mapper { get; }
    private CourseCreateValidator CourseCreateValidator { get; }
    private CourseUpdateValidator CourseUpdateValidator { get; }

    public CourseService(IGenericRepository<Course> courseRepository, IGenericRepository<Student> studentRepository,
        IMapper mapper, CourseCreateValidator courseCreateValidator, CourseUpdateValidator courseUpdateValidator)
    {
        CourseRepository = courseRepository;
        StudentRepository = studentRepository;
        Mapper = mapper;
        CourseCreateValidator = courseCreateValidator;
        CourseUpdateValidator = courseUpdateValidator;
    }


    public async Task<int> CreateCourseAsync(CourseCreate courseCreate)
    {
        await CourseCreateValidator.ValidateAndThrowAsync(courseCreate);

        Expression<Func<Student, bool>> studentFilter = (Student) => courseCreate.Students.Contains(Student.Id);
        var students = await StudentRepository.GetFilteredAsync(new[] { studentFilter }, null, null);
        var missingStudents = courseCreate.Students.Where((id) => !students.Any(existing => existing.Id == id));

        if(missingStudents.Any()) 
            throw new StudentsNotFoundException(missingStudents.ToArray());

        var entity = Mapper.Map<Course>(courseCreate);
        entity.Students = students;
        await CourseRepository.InsertAsync(entity);
        await CourseRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteCourseAsync(CourseDelete courseDelete)
    {
        var entity = await CourseRepository.GetByIdAsync(courseDelete.Id);

        if (entity == null)
            throw new CourseNotFoundException(courseDelete.Id);
        CourseRepository.Delete(entity);
        await CourseRepository.SaveChangesAsync();
    }

    public async Task<CourseGet> GetCourseAsync(int id)
    {
        var entity = await CourseRepository.GetByIdAsync(id, (team) => team.Students);
        if (entity == null)
            throw new CourseNotFoundException(id);
        return Mapper.Map<CourseGet>(entity);
    }

    public async Task<List<CourseGet>> GetCoursesAsync()
    {
        var entities = await CourseRepository.GetAsync(null, null, (course) => course.Students);
        return Mapper.Map<List<CourseGet>>(entities);
    }

    public async Task UpdateCourseAsync(CourseUpdate courseUpdate)
    {
        await CourseUpdateValidator.ValidateAndThrowAsync(courseUpdate);

        Expression<Func<Student, bool>> studentFilter = (Student) => courseUpdate.Students.Contains(Student.Id);
        var students = await StudentRepository.GetFilteredAsync(new[] { studentFilter }, null, null);
        var missingStudents = courseUpdate.Students.Where((id) => !students.Any(existing => existing.Id == id));

        if (missingStudents.Any())
            throw new StudentsNotFoundException(missingStudents.ToArray());

        var existingEntity = await CourseRepository.GetByIdAsync(courseUpdate.Id, (course) => course.Students);
        if (existingEntity == null)
            throw new CourseNotFoundException(courseUpdate.Id);

        Mapper.Map(courseUpdate, existingEntity);
        existingEntity.Students = students;
        CourseRepository.Update(existingEntity);
        await CourseRepository.SaveChangesAsync();
    }
}
