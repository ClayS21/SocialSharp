using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController(AppDbContext dbContext, IPhotoService photoService) : ControllerBase
    {
        public async Task<ActionResult<List<Post>>> GetPosts()
        {
            var posts = await dbContext.Posts.Include(x => x.Likes).Include(x => x.Comments).ToListAsync();
            return posts;
        }

        [HttpPost("add-post")]
        public async Task<ActionResult<Post>> AddPost(PostRequest request)
        {
            var result = new ImageUploadResult();

            if (request.Image is not null)
            {
                result = await photoService.UploadImageAsync(request.Image);

                if (result.Error is not null)
                {
                    return BadRequest(result.Error.Message);
                }
            }

            var newPost = new Post
            {
                Content = request.Content,
                ImageURL = result.SecureUrl.AbsoluteUri,
                Created = DateTime.UtcNow,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            await dbContext.Posts.AddAsync(newPost);
            await dbContext.SaveChangesAsync();

            return newPost;
        }

    }
}