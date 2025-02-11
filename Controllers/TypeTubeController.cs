using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stage.Models;

namespace stage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeTubeController : ControllerBase
    {
        private readonly LabContext _context;

        public TypeTubeController(LabContext context)
        {
            _context = context;
        }

        // GET: api/TypeTubes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TypeTube>>> GetTypeTubes()
        {
            var typeTubes = await _context.Type_Tube
            .Select(t => new
            {
                t.CodeType,
                t.LiblletType,
                Couleur = t.Couleur != null ? Convert.ToBase64String(t.Couleur) : null,
                t.NomImage,
                t.UserCreate,
                t.DateCreate
            })
            .ToListAsync();

             return Ok(typeTubes);

        }

        // GET: api/TypeTubes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TypeTube>> GetTypeTube(int id)  // Changement du type id en int
        {
            var typeTube = await _context.Type_Tube
            .Where(t => t.CodeType == id)
            .Select(t => new
            {
                t.CodeType,
                t.LiblletType,
                Couleur = t.Couleur != null ? Convert.ToBase64String(t.Couleur) : null,
                t.NomImage,
                t.UserCreate,
                t.DateCreate
            })
            .FirstOrDefaultAsync();

            if (typeTube == null)
            {
                return NotFound();
            }

            return Ok(typeTube);

        }

        // PUT: api/TypeTubes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeTube(int id, TypeTube typeTube)  // Changement du type id en int
        {
            if (id != typeTube.CodeType)
            {
                return BadRequest();
            }

            _context.Entry(typeTube).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TypeTubeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TypeTubes
        [HttpPost]
        public async Task<ActionResult<TypeTube>> PostTypeTube(TypeTube typeTube)
        {
            _context.Type_Tube.Add(typeTube);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TypeTubeExists(typeTube.CodeType))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTypeTube", new { id = typeTube.CodeType }, typeTube);
        }

        // DELETE: api/TypeTubes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeTube(int id)  
        {
            var typeTube = await _context.Type_Tube.FindAsync(id);
            if (typeTube == null)
            {
                return NotFound();
            }

            _context.Type_Tube.Remove(typeTube);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TypeTubeExists(int id)  
        {
            return _context.Type_Tube.Any(e => e.CodeType == id);
        }
    }
}
