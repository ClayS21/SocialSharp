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
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var userExists = userManager.Users.Any(u => u.Email == registerDTO.Email);

            if (userExists) return BadRequest("Unable to register because this e-mail is already in use.");

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

            return new UserDTO
            {
                FullName = user.FullName,
                Email = user.Email,
                Gender = user.Gender
            };
        }

        [HttpGet("get-users")]
        public async Task<ActionResult<IReadOnlyList<User>>> GetUsers()
        {
            var users = await userManager.Users.ToListAsync();

            return users;
        }
    }
}