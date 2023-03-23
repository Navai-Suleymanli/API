using Eddyproject.Common.Dtos.Student;
using Eddyproject.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace EddyProject.API.Controllers;

[ApiController]
[Route("api/student")]

public class StudentController : ControllerBase
{
    private IStudentService StudentService { get; }
    private ILogger<StudentController> Logger { get; }

    public StudentController(IStudentService studentService, ILogger<StudentController> logger)
    {
        StudentService = studentService;
        Logger = logger;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateStudent(StudentCreate studentCreate)
    {
        var id = await StudentService.CreateStudentAsync(studentCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateStudent(StudentUpdate studentUpdate)
    {
        await StudentService.UpdateStudentAsync(studentUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> DeleteStudent(StudentDelete studentDelete)
    {
        await StudentService.DeleteStudentAsync(studentDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetStudent(int id)
    {
        Logger.LogInformation("The student was called with id", id);
        var student = await StudentService.GetStudentAsync(id);
        return Ok(student);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetStudents([FromQuery] StudentFilter studentFilter)
    {
        var students = await StudentService.GetStudentsAsync(studentFilter);
        return Ok(students);
    }





}
