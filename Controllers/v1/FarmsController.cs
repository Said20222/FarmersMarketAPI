using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmersMarketAPI.Models.Auth;
using FarmersMarketAPI.Models.Entities;
using FarmersMarketAPI.Data;

namespace FarmersMarketAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors]
    public class FarmsController(FarmersMarketContext context) : ControllerBase
    {
        private readonly FarmersMarketContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Farm>>> GetFarms()
        {
            if (_context.Farms == null)
            {
                return NotFound();
            }

            var count = await _context.Farms.CountAsync();
            Response.Headers.Append("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Append("Content-Range", $"X-Total-Count: 1 - {count} / {count}");

            return await _context.Farms.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Farm>> GetFarm(string id)
        {
            if (_context.Farms == null)
            {
                return NotFound();
            }
            var Farm = await _context.Farms.FindAsync(new Guid(id));

            if (Farm == null)
            {
                return NotFound();
            }

            return Farm;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = $"{UserRoles.Farmer},{UserRoles.Admin}")]
        public async Task<IActionResult> PutFarm(int id, Farm Farm)
        {
            if (id != Farm.Id)
            {
                return BadRequest();
            }

            _context.Entry(Farm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FarmExists(id))
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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = $"{UserRoles.Farmer},{UserRoles.Admin}")]
        public async Task<ActionResult<Farm>> PostFarm(Farm Farm)
        {
            if (_context.Farms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Farms'  is null.");
            }
            _context.Farms.Add(Farm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFarm", new { id = Farm.Id }, Farm);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{UserRoles.Farmer},{UserRoles.Admin}")]
        public async Task<IActionResult> DeleteFarm(string id)
        {
            if (_context.Farms == null)
            {
                return NotFound();
            }
            var Farm = await _context.Farms.FindAsync(new Guid(id));
            if (Farm == null)
            {
                return NotFound();
            }

            _context.Farms.Remove(Farm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FarmExists(int id)
        {
            return (_context.Farms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}