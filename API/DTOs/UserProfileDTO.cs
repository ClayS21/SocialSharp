using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.DTOs
{
    public class UserProfileDTO
    {
        public required string FullName { get; set; }

        public DateTime LastActive { get; set; }

        public string? ProfilePictureURL { get; set; }

        public string? Bio { get; set; }

        public ICollection<Photo>? Photos { get; set; }
    }
}