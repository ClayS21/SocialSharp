using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Photo
    {
        public int Id { get; set; }

        public required string Url { get; set; }

        public string? PublicId { get; set; }

        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;
    }
}