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
    public class MarketOrdersController(FarmersMarketContext context) : ControllerBase
    {
        private readonly FarmersMarketContext _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketOrder>>> GetMarketOrders()
        {
            if (_context.MarketOrders == null)
            {
                return NotFound();
            }

            var count = await _context.MarketOrders.CountAsync();
            Response.Headers.Append("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Append("Content-Range", $"X-Total-Count: 1 - {count} / {count}");

            return await _context.MarketOrders.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarketOrder>> GetMarketOrder(int id)
        {
            if (_context.MarketOrders == null)
            {
                return NotFound();
            }
            var MarketOrder = await _context.MarketOrders.FindAsync(id);

            if (MarketOrder == null)
            {
                return NotFound();
            }

            return MarketOrder;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = $"{UserRoles.Farmer},{UserRoles.Admin}")]
        public async Task<IActionResult> PutMarketOrder(int id, MarketOrder marketOrder)
        {
            if (id != marketOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(marketOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MarketOrderExists(id))
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
        public async Task<ActionResult<MarketOrder>> PostMarketOrder(MarketOrder MarketOrder)
        {
            if (_context.MarketOrders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.MarketOrders'  is null.");
            }
            _context.MarketOrders.Add(MarketOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMarketOrder", new { id = MarketOrder.Id }, MarketOrder);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{UserRoles.Farmer},{UserRoles.Admin}")]
        public async Task<IActionResult> DeleteMarketOrder(int id)
        {
            if (_context.MarketOrders == null)
            {
                return NotFound();
            }
            var MarketOrder = await _context.MarketOrders.FindAsync(id);
            if (MarketOrder == null)
            {
                return NotFound();
            }

            _context.MarketOrders.Remove(MarketOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MarketOrderExists(int id)
        {
            return (_context.MarketOrders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
