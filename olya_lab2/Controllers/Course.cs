using Microsoft.AspNetCore.Mvc;
using Olya.model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace olya_lab2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly List<Course> _courses;

        public CourseController()
        {
            _courses = new List<Course>();
        }

        // GET api/course
        [HttpGet]
        public IActionResult GetAllCourses()
        {
            // Операція для отримання всіх курсів
            return Ok(_courses);
        }

        // GET api/course/{id}
        [HttpGet("{id}")]
        public IActionResult GetCourseById(Guid id)
        {
            // Операція для отримання курсу за ідентифікатором
            var course = _courses.FirstOrDefault(c => c.CourseId == id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

        // POST api/course
        [HttpPost]
        public IActionResult CreateCourse([FromBody] Course course)
        {
            // Операція для створення нового курсу
            course.CourseId = Guid.NewGuid();
            _courses.Add(course);
            return CreatedAtAction(nameof(GetCourseById), new { id = course.CourseId }, course);
        }

        // DELETE api/course/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCourse(Guid id)
        {
            // Операція для видалення курсу за ідентифікатором
            var course = _courses.FirstOrDefault(c => c.CourseId == id);
            if (course == null)
                return NotFound();

            _courses.Remove(course);
            return NoContent();
        }
    }
}