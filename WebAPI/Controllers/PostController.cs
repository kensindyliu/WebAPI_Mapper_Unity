using AutoMapper;
using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using EntityService;
using Microsoft.EntityFrameworkCore;
using EntityService.Interfaces;
using Unity;
using Microsoft.Extensions.Hosting;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPostService _postService;

        public PostController(IMapper mapper, IPostService postService)
        {
            _mapper = mapper;
            _postService = postService;
            //_postService = container.Resolve<IPostService>();
        }


        [HttpGet]
        [Route("GetAllPost")]
        public async Task<IActionResult> GetAllPost()
        {
            List<Post> posts = await _postService.GetPosts();
            List<PostDTO> postDTOs = _mapper.Map<List<PostDTO>>(posts);

            return Ok(postDTOs);
        }

        [HttpGet]
        [Route("GetPostById/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            Post post = await _postService.GetPostsById(id);
            if (post == null)
            {
                return NotFound($"Post id: {id} is not found!");
            }
            PostDTO pDTO = _mapper.Map<PostDTO>(post);
            return Ok(pDTO);

        }

        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost(PostDTO postDTO)
        {
            Post post = _mapper.Map<Post>(postDTO);
            await _postService.AddPost(post);
            return Ok("Post Added Successfully");
        }

        [HttpPost]
        [Route("DeletePost/{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            string result = await _postService.DeletePost(id);
            return Ok(result);
        }

        [HttpPut("api/UpdatePost/{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostDTO postDTO)
        {
            Post post = _mapper.Map<Post>(postDTO);
            post.PostId = id;
            try
            {
                string result = await _postService.UpdatePost(post);
                return Ok(result); // Return success response
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}"); // Return error response
            }
        }


    }
}
