using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<User> userManager, ITokenService tokenService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserCredentialsDTO>> Register(RegisterDTO registerDTO)
        {
            var user = new User
            {
                UserName = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                DateOfBirth = registerDTO.DateOfBirth,
                Gender = registerDTO.Gender
            };

            var userCreationResult = await userManager.CreateAsync(user, registerDTO.Password);

            if (!userCreationResult.Succeeded)
            {
                foreach (var error in userCreationResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await SetRefreshToken(user);

            return new UserCredentialsDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Gender = user.Gender,
                Token = tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserCredentialsDTO>> Login(LoginDTO login)
        {
            var user = await userManager.FindByEmailAsync(login.Username);

            if (user is null) return BadRequest("Invalid email or password");

            var result = await userManager.CheckPasswordAsync(user, login.Password);

            if (!result) return BadRequest("Invalid username or password");

            await SetRefreshToken(user);

            return new UserCredentialsDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Gender = user.Gender,
                ProfilePictureURL = user.ProfilePictureURL,
                Token = tokenService.CreateToken(user)
            };
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<UserCredentialsDTO>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refresh-token"];
            if (refreshToken is null) return NoContent();

            var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow);

            if (user is null) return Unauthorized();

            await SetRefreshToken(user);

            var credentials = new UserCredentialsDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                Gender = user.Gender,
                ProfilePictureURL = user.ProfilePictureURL,
                Token = tokenService.CreateToken(user)
            };

            return credentials;
        }

        private async Task SetRefreshToken(User user)
        {
            var refreshToken = tokenService.CreateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await userManager.UpdateAsync(user);

            var options = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refresh-token", refreshToken, options);
        }
    }
}