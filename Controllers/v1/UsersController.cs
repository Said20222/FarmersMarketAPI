using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmersMarketAPI.Models.Auth;
using FarmersMarketAPI.Data;
using FarmersMarketAPI.Utilities;

namespace FarmersMarketAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors]
    public class UsersController : ControllerBase
    {
        private readonly FarmersMarketContext _context;

        public UsersController(FarmersMarketContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{UserRoles.Admin}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(new Guid(id));

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            Guid guid = new Guid(id);
            if (guid != user.Id)
            {
                return BadRequest("User id does not match to the id provided");
            }

            var user_db = _context.Users?.AsNoTracking().Where(x => x.Id == user.Id)?.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                user.PasswordHash = user_db?.PasswordHash;
            else
                user.PasswordHash = PasswordHelper.HashPassword(user.PasswordHash);

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(guid))
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
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
                return Problem("Entity set 'ApplicationDbContext.Users'  is null.");

            if (_context.Users.Select(u => u.Email).ToList().Contains(user.Email))
                return Problem("The user with the same Email address already exists");
            
            user.PasswordHash = PasswordHelper.HashPassword(user.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(new Guid(id));
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}