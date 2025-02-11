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
    public class PaillassesController : ControllerBase
    {
        private readonly LabContext _context;

        public PaillassesController(LabContext context)
        {
            _context = context;
        }

        // GET: api/Paillasses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paillasse>>> GetPaillasses()
        {
            return await _context.Paillasses.ToListAsync();
        }

        // GET: api/Paillasses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paillasse>> GetPaillasse(string id)
        {
            var paillasse = await _context.Paillasses.FindAsync(id);

            if (paillasse == null)
            {
                return NotFound();
            }

            return paillasse;
        }

        // PUT: api/Paillasses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaillasse(string id, Paillasse paillasse)
        {
            if (id != paillasse.CodePaillasse)
            {
                return BadRequest();
            }

            _context.Entry(paillasse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaillasseExists(id))
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

        // POST: api/Paillasses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Paillasse>> PostPaillasse(Paillasse paillasse)
        {
            _context.Paillasses.Add(paillasse);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PaillasseExists(paillasse.CodePaillasse))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPaillasse", new { id = paillasse.CodePaillasse }, paillasse);
        }

        // DELETE: api/Paillasses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaillasse(string id)
        {
            var paillasse = await _context.Paillasses.FindAsync(id);
            if (paillasse == null)
            {
                return NotFound();
            }

            _context.Paillasses.Remove(paillasse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaillasseExists(string id)
        {
            return _context.Paillasses.Any(e => e.CodePaillasse == id);
        }
    }
}
