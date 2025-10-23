using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController(AppDbContext dbContext) : ControllerBase
    {
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            var posts = await dbContext.Posts.Include(x => x.Likes).Include(x => x.Comments).ToListAsync();
            return posts;
        }

        [HttpPost("add-post")]
        public async Task<ActionResult<Post>> AddPost(PostRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var newPost = new Post
            {
                Content = request.Content,
                ImageURL = 
                Created = DateTime.UtcNow,
                UserId = userId
            };

        }

    }
}