using ICMA_LEARN.API.Data.Entity;
using ICMA_LEARN.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ICMA_LEARN.API.DataModel.CourseModel;
using Microsoft.AspNetCore.Authorization;
[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly ICMAContext _context;

    public CoursesController(ICMAContext context)
    {
        _context = context;
    }

    // GET: api/courses
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CourseDataModel>>> GetCourses()
    {
        var courses = await _context.Courses
            .Include(c => c.Category) // Include related CourseContents
            .ToListAsync();

        var courseDtos = courses.Select(c => new CourseDataModel
        {
            CourseID = c.CourseID,
            CourseName = c.CourseName,
            Description = c.Description,
            UserID = c.UserID ?? 0,
            CategoryID = c.CategoryID ?? 0,
            CreatedAt = c.CreatedAt ?? DateTime.Now,
        }).ToList();

        return Ok(courseDtos);
    }

    // GET: api/courses/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseDataModel>> GetCourse(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Category) // Include related CourseContents
            .FirstOrDefaultAsync(c => c.CourseID == id);

        if (course == null)
        {
            return NotFound();
        }

        var courseDto = new CourseDataModel
        {
            CourseID = course.CourseID,
            CourseName = course.CourseName,
            Description = course.Description,
            UserID = course.UserID ?? 0,
            CategoryID = course.CategoryID ?? 0,
            CreatedAt = course.CreatedAt ?? DateTime.Now,
        };

        return Ok(courseDto);
    }

    // POST: api/courses
    [HttpPost]
    public async Task<ActionResult<CourseDataModel>> CreateCourse(CourseDataModel courseDto)
    {
        var course = new Course
        {
            CourseName = courseDto.CourseName,
            Description = courseDto.Description,
            UserID = courseDto.UserID,
            CategoryID = courseDto.CategoryID,
            CreatedAt = DateTime.UtcNow
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        courseDto.CourseID = course.CourseID;

        return CreatedAtAction(nameof(GetCourse), new { id = course.CourseID }, courseDto);
    }

    // PUT: api/courses/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, CourseDataModel courseDto)
    {
        if (id != courseDto.CourseID)
        {
            return BadRequest();
        }

        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        course.CourseName = courseDto.CourseName;
        course.Description = courseDto.Description;
        course.UserID = courseDto.UserID;
        course.CategoryID = courseDto.CategoryID;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/courses/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{id}/content")]
    public async Task<ActionResult<CourseContentDataModel>> UploadCourseContent(int id, [FromBody] CourseContentDataModel contentDto)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
        {
            return NotFound();
        }

        var courseContent = new CourseContent
        {
            CourseID = id,
            FilePath = contentDto.FilePath 
        };

        _context.CourseContents.Add(courseContent);
        await _context.SaveChangesAsync();

        contentDto.CourseContentID = courseContent.CourseContentID;

        return CreatedAtAction(nameof(GetCourse), new { id = courseContent.CourseContentID }, contentDto);
    }

}
