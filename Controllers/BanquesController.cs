using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stage.Models;

namespace stage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanquesController : ControllerBase
    {
        private readonly LabContext _context;

        public BanquesController(LabContext context)
        {
            _context = context;
        }

        // GET: api/Banques

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Banque>>> GetBanques()
        {
            if (_context.Banques == null)
            {
                return NotFound();
            }
            return await _context.Banques.ToListAsync();
        }

        // GET: api/Banques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Banque>> GetBanque(string id)
        {
            if (_context.Banques == null)
            {
                return NotFound();
            }
            var banque = await _context.Banques.FindAsync(id);

            if (banque == null)
            {
                return NotFound();
            }

            return banque;
        }

        // PUT: api/Banques/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBanque(string id, Banque banque)
        {
            if (id != banque.Matban)
            {
                return BadRequest();
            }

            _context.Entry(banque).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BanqueExists(id))
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

        // POST: api/Banques
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Banque>> PostBanque(Banque banque)
        {
            if (_context.Banques == null)
            {
                return Problem("Entity set 'LabContext.Banques'  is null.");
            }
            _context.Banques.Add(banque);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BanqueExists(banque.Matban))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBanque", new { id = banque.Matban }, banque);
        }

        // DELETE: api/Banques/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBanque(string id)
        {
            if (_context.Banques == null)
            {
                return NotFound();
            }
            var banque = await _context.Banques.FindAsync(id);
            if (banque == null)
            {
                return NotFound();
            }

            _context.Banques.Remove(banque);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BanqueExists(string id)
        {
            return (_context.Banques?.Any(e => e.Matban == id)).GetValueOrDefault();
        }
        [HttpGet]
        [Route("GetListeBanque")]

        public ActionResult GetListeBanque()
        {
            try
            {
                var req = (from bq in _context.Banques

                           select new
                           {
                               matban = bq.Matban,
                               desban = bq.Desban
                           }).Distinct();


                return Ok(req.ToList().Distinct());
            }
            catch (Exception ex)
            {
                return Conflict();
            }

        }
    }
}
