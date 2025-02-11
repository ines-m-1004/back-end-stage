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
    public class TubeController : ControllerBase
    {
        private readonly LabContext _context;

        public TubeController(LabContext context)
        {
            _context = context;
        }

        // GET: api/Tube
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TubeDTO>>> GetTube()
        {
            var tubes = await _context.Tube
                .Join(_context.Type_Tube,
                    tube => tube.code_Type,
                    typeTube => typeTube.CodeType,
                    (tube, typeTube) => new TubeDTO
                    {
                        codeTube = tube.codeTube,
                        LiblletTube = tube.LiblletTube,
                        nom_image = typeTube.Couleur != null ? Convert.ToBase64String(typeTube.Couleur) : null, // Convert byte[] to Base64 string
                        code_Type = tube.code_Type,
                        couleur = typeTube.Couleur != null ? Convert.ToBase64String(typeTube.Couleur) : null
                    })
                .ToListAsync();

            return Ok(tubes);
        }

        // GET: api/Tube/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TubeDTO>> GetTube(string id)
        {
            var tube = await _context.Tube
                .Join(_context.Type_Tube,
                    tube => tube.code_Type,
                    typeTube => typeTube.CodeType,
                    (tube, typeTube) => new TubeDTO
                    {
                        codeTube = tube.codeTube,
                        LiblletTube = tube.LiblletTube,
                        nom_image = typeTube.Couleur != null ? Convert.ToBase64String(typeTube.Couleur) : null, 
                        code_Type = tube.code_Type,
                        couleur = typeTube.Couleur != null ? Convert.ToBase64String(typeTube.Couleur) : null
                    })
                .FirstOrDefaultAsync(t => t.codeTube == id);

            if (tube == null)
            {
                return NotFound();
            }

            return Ok(tube);
        }

        // PUT: api/Tube/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTube(string id, Tube tube)
        {
            if (id != tube.codeTube)
            {
                return BadRequest();
            }

            _context.Entry(tube).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TubeExists(id))
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

        // POST: api/Tube
        [HttpPost]
        public async Task<ActionResult<Tube>> PostTube(Tube tube)
        {
            _context.Tube.Add(tube);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TubeExists(tube.codeTube))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTube", new { id = tube.codeTube }, tube);
        }

        // DELETE: api/Tube/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTube(string id)
        {
            var tube = await _context.Tube.FindAsync(id);
            if (tube == null)
            {
                return NotFound();
            }

            _context.Tube.Remove(tube);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TubeExists(string id)
        {
            return _context.Tube.Any(e => e.codeTube == id);
        }
    }
}
