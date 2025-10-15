using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Post
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string? ImageURL { get; set; }

        public DateTime Created { get; set; }


        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Like> Likes { get; set; } = [];

        public ICollection<Comment> Comments { get; set; } = [];

    }
}