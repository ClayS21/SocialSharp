using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserCredentialsDTO
    {

        public required string Id { get; set; }
        
        public required string FullName { get; set; }

        public required string Email { get; set; }

        public required string Gender { get; set; }

        public string? ProfilePictureURL { get; set; }

        public required string Token { get; set; }
    }
}