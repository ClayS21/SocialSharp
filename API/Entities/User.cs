using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class User : IdentityUser
    {
        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public string FullName
        {
            get => $"{FirstName} {LastName}";
        }

        public DateOnly DateOfBirth { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public DateTime LastActive { get; set; } = DateTime.UtcNow;

        public string? ProfilePictureURL { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }

        public required string Gender { get; set; }

        public string? Bio { get; set; }

        // Navigation Properties


        public ICollection<Photo> Photos { get; set; } = [];
    }
}