using HousingBillManagement.API.DTOs;
using HousingBillManagement.API.Models;
using HousingBillManagement.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HousingBillManagement.API.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService _tokenService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, TokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto registRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = registRequest.TCNo,
                FullName = registRequest.FullName,
                TCNo = registRequest.TCNo,
                PhoneNumber = registRequest.PhoneNumber,
                Email = registRequest.Email
            };

            var result = await _userManager.CreateAsync(user, registRequest.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // role checker
            if (!string.IsNullOrEmpty(registRequest.Role))
            {
                var roleExists = await _roleManager.RoleExistsAsync(registRequest.Role);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(registRequest.Role));
                }

                // role assigning
                var roleResult = await _userManager.AddToRoleAsync(user, registRequest.Role);
                if (!roleResult.Succeeded)
                {
                    return BadRequest(roleResult.Errors);
                }
            }

            var token = await _tokenService.GenerateToken(registRequest.TCNo, registRequest.PhoneNumber, registRequest.Password);

            return Ok(new { Token = token });
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userManager.FindByNameAsync($"{loginRequest.TCNo}-{loginRequest.PhoneNumber}");

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid TCNo, PhoneNumber, or password." });
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    return Unauthorized(new { message = "Invalid TCNo, PhoneNumber, or password." });
                }

                var isAdmin = await _userManager.IsInRoleAsync(user, "admin");
                var token = await _tokenService.GenerateToken(loginRequest.TCNo, loginRequest.PhoneNumber, loginRequest.Password);

                return Ok(new { Token = token, IsAdmin = isAdmin });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
