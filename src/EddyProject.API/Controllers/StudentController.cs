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
    public StudentController(IStudentService studentService)
    {
        StudentService = studentService;
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
