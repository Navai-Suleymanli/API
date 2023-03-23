using Eddyproject.Common.Dtos.Course;
using Eddyproject.Common.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EddyProject.API.Controllers;

[ApiController]
[Route("api/course")]

public class CourseController : ControllerBase
{
    private ICourseService CourseService { get; }
    public CourseController(ICourseService courseService)
    {
        CourseService = courseService;
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> CreateCourse(CourseCreate courseCreate)
    {
        var id = await CourseService.CreateCourseAsync(courseCreate);
        return Ok(id);
    }

    [HttpPut]
    [Route("Update")]
    public async Task<IActionResult> UpdateCourse(CourseUpdate courseUpdate) 
    {
        await CourseService.UpdateCourseAsync(courseUpdate);
        return Ok();
    }

    [HttpDelete]
    [Route("Delete")]
    public async Task<IActionResult> DeleteCourse(CourseDelete courseDelete)
    {
        await CourseService.DeleteCourseAsync(courseDelete);
        return Ok();
    }

    [HttpGet]
    [Route("Get/{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        var course = await CourseService.GetCourseAsync(id);
        return Ok(course);
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await CourseService.GetCoursesAsync();
        return Ok(courses);
    }

    


}
