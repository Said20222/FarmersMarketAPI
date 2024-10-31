using Microsoft.AspNetCore.Mvc;
using FarmersMarketAPI.Data;
using FarmersMarketAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly FarmersMarketContext _context;

    public UserController(FarmersMarketContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}
