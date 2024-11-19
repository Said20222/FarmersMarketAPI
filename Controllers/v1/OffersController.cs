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
    public class OffersController : ControllerBase
    {
        private readonly FarmersMarketContext _context;

        public OffersController(FarmersMarketContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offer>>> GetOffers()
        {
            if (_context.Offers == null)
            {
                return NotFound();
            }
            return await _context.Offers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetOffer(string id)
        {
            if (_context.Offers == null)
            {
                return NotFound();
            }
            var Offer = await _context.Offers.FindAsync(new Guid(id));

            if (Offer == null)
            {
                return NotFound();
            }

            return Offer;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> PutOffer(int id, Offer Offer)
        {
            if (id != Offer.OfferId)
            {
                return BadRequest();
            }

            _context.Entry(Offer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(id))
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
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<Offer>> PostOffer(Offer Offer)
        {
            if (_context.Offers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Offers'  is null.");
            }
            _context.Offers.Add(Offer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOffer", new { id = Offer.OfferId }, Offer);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteOffer(string id)
        {
            if (_context.Offers == null)
            {
                return NotFound();
            }
            var Offer = await _context.Offers.FindAsync(new Guid(id));
            if (Offer == null)
            {
                return NotFound();
            }

            _context.Offers.Remove(Offer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfferExists(int id)
        {
            return (_context.Offers?.Any(e => e.OfferId == id)).GetValueOrDefault();
        }
    }
}