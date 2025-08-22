using CourseApi.Context;
using CourseApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CourseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }

        // GET all courses
        [HttpGet]
        public IActionResult Get()
        {
            var courses = _context.Courses.ToList();
            return Ok(courses); // return [] instead of 404
        }

        // GET course by Id
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == id);
            if (course == null)
                return NotFound($"Course with ID {id} not found.");
            return Ok(course);
        }

        // POST add new course
        [HttpPost]
        public IActionResult Post([FromBody] Course course)
        {
            course.CreatedOn = DateTime.Now;
            course.IsActive = true;

            _context.Courses.Add(course);
            _context.SaveChanges();

            return Ok(course); // return clean JSON
        }

        // DELETE course
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == id);
            if (course == null)
                return NotFound($"Course with ID {id} not found.");

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return Ok(new { message = $"Course with ID {id} deleted successfully." });
        }

        // PUT update course
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] Course temp)
        {
            var course = _context.Courses.FirstOrDefault(x => x.CourseId == id);
            if (course == null)
                return NotFound($"Course with ID {id} not found.");

            course.CourseName = temp.CourseName;
            course.Description = temp.Description;
            course.Duration = temp.Duration;
            course.IsActive = temp.IsActive;
            course.ModifiedOn = DateTime.Now;
            course.ModifiedBy = temp.ModifiedBy ?? 1;

            _context.SaveChanges();
            return Ok(course);
        }
    }
}
