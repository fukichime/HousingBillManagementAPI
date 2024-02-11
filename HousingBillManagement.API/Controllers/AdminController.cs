using HousingBillManagement.API.DTOs;
using HousingBillManagement.API.Models;
using HousingBillManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HousingBillManagement.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;

        public AdminController(UserManager<User> userManager, TokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("CreateAdmin")]
        public async Task<IActionResult> CreateAdmin()
        {
            try
            {
                var adminUser = await _userManager.FindByNameAsync("admin");

                if (adminUser == null)
                {
                    adminUser = new User
                    {
                        UserName = "admin",
                    };

                    var result = await _userManager.CreateAsync(adminUser, "adminPassword");

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(adminUser, "admin");
                        var token = await _tokenService.GenerateToken("admin", null, null, "adminPassword");

                        return Ok(new { Token = token });
                    }
                    else
                    {
                        return BadRequest(result.Errors);
                    }
                }
                else
                {
                    return BadRequest("Admin user already exists.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin([FromBody] UserLoginDto loginRequest)
        {
            try
            {
                var adminUser = await _userManager.FindByNameAsync("admin");

                if (adminUser == null)
                {
                    return BadRequest("Admin user does not exist.");
                }

                var result = await _userManager.CheckPasswordAsync(adminUser, loginRequest.Password);

                if (result)
                {
                    var token = await _tokenService.GenerateToken("admin", null, null, loginRequest.Password);
                    return Ok(new { Token = token });
                }
                else
                {
                    return Unauthorized(new { message = "Invalid password for admin user." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
