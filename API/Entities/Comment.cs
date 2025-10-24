using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }
        public DateTime Created { get; set; }

        //Foreign keys
        public string PostId { get; set; }
        public string UserId { get; set; }

        // Navigation properties
        public Post Post { get; set; }
        public User User { get; set; }
    }
}