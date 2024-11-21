using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using FarmersMarketAPI.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FarmersMarketAPI.Models.Entities;

namespace FarmersMarketAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors]
    public class AuthenticateController(
        UserManager<User> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IConfiguration configuration) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager = roleManager;
        private readonly IConfiguration _configuration = configuration;

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    id = user.Id,
                    username = user.UserName,
                    roles = userRoles,
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response
                    {
                        Status = "Error", 
                        Message = "User creation failed! Please check user details and try again." 
                    });

            return Ok(new Response { Status = "Success", Message = "User with no Role created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new Response 
                    {
                        Status = "Error", 
                        Message = "User creation failed! Please check user details and try again." 
                    });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Farmer))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Farmer));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Buyer))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Buyer));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            return Ok(new Response { Status = "Success", Message = "Admin user created successfully!" });
        }

        [HttpPost]
        [Route("register-farmer")]
        public async Task<IActionResult> RegisterFarmer([FromBody] FarmerRegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName, 
                LastName = model.LastName, 
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber, 
                ProfileImgPath = model.ProfileImgPath
            };
            Farm farm = new() {
                FarmName = model.FarmName,
                Location = model.Location,
                FarmSize = model.FarmSize,
            };
            user.Farms?.Add(farm);

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "User creation failed! Please check user details and try again."
                    });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Farmer))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Farmer));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Buyer))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Buyer));

            if (await _roleManager.RoleExistsAsync(UserRoles.Farmer))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Farmer);
            }
            return Ok(new Response { Status = "Success", Message = "Farmer user created successfully!" });
        }

        [HttpPost]
        [Route("register-buyer")]
        public async Task<IActionResult> RegisterBuyer([FromBody] BuyerRegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName, 
                LastName = model.LastName, 
                BirthDate = model.BirthDate,
                PhoneNumber = model.PhoneNumber,
                PreferredDeliveryMethod = model.PreferredDeliveryMethod,
                PreferredPaymentMethod = model.PreferredPaymentMethod, 
                DeliveryAddress = model.DeliveryAddress
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "User creation failed! Please check user details and try again."
                    });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Farmer))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Farmer));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Buyer))
                await _roleManager.CreateAsync(new IdentityRole<Guid>(UserRoles.Buyer));

            if (await _roleManager.RoleExistsAsync(UserRoles.Buyer))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Buyer);
            }
            return Ok(new Response { Status = "Success", Message = "Buyer user created successfully!" });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
