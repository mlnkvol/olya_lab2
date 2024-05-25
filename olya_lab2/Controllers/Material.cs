using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Olya.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Olya.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialController : ControllerBase
    {
        private readonly CoursePlatformContext _context;

        public MaterialController(CoursePlatformContext context)
        {
            _context = context;
        }

        // GET: api/Material
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            return await _context.Materials
                                 .Include(m => m.Course)
                                 .ToListAsync();
        }

        // GET: api/Material/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetMaterial(Guid id)
        {
            var material = await _context.Materials
                                         .Include(m => m.Course)
                                         .FirstOrDefaultAsync(m => m.MaterialId == id);

            if (material == null)
            {
                return NotFound();
            }

            return material;
        }

        // POST: api/Material
        [HttpPost]
        public async Task<ActionResult<Material>> CreateMaterial(Material material)
        {
            var course = await _context.Courses.FindAsync(material.CourseId);
            if (course == null)
            {
                return BadRequest("Invalid CourseId");
            }

            material.Course = course;
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMaterial), new { id = material.MaterialId }, material);
        }

        // DELETE: api/Material/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaterial(Guid id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }

            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
