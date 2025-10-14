using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<User> userManager) : ControllerBase
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

            return new UserCredentialsDTO
            {
                FullName = user.FullName,
                Email = user.Email,
                Gender = user.Gender
            };
        }

        [HttpGet("get-users")]
        public async Task<ActionResult<IReadOnlyList<UserCredentialsDTO>>> GetUsers()
        {
            var users = await userManager.Users.Select(u => new UserCredentialsDTO
            {
                FullName = u.FullName,
                Email = u.Email!,
                Gender = u.Gender,
                ProfilePictureURL = u.ProfilePictureURL
            }).ToListAsync();

            return users;
        }
    }
}