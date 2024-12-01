using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FarmersMarketAPI.Models.Auth;
using FarmersMarketAPI.Data;
using FarmersMarketAPI.Utilities;
using Microsoft.AspNetCore.Identity;

namespace FarmersMarketAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors]
    public class UsersController(FarmersMarketContext context, UserManager<User> userManager) : ControllerBase
    {
        private readonly FarmersMarketContext _context = context;
        private readonly UserManager<User> _userManager = userManager;

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var count = await _context.Users.CountAsync();
            Response.Headers.Append("Access-Control-Expose-Headers", "Content-Range");
            Response.Headers.Append("Content-Range", $"X-Total-Count: 1 - {count} / {count}");

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

        [HttpGet("farmers")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<User>>> GetFarmers()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.
                            ToListAsync();

            List<User> farmers = [];
            foreach (var u in users)
            {
                if (await _userManager.IsInRoleAsync(u, UserRoles.Farmer)) {
                    farmers.Add(u); 
                }
            }

            if (farmers.Count == 0)
            {
                return NotFound("No farmers found.");
            }

            return farmers;
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