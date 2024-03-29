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
    public class CommentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public CommentController(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
            //_postService = container.Resolve<IPostService>();
        }

        [HttpGet]
        [Route("GetAllComments")]
        public async Task<IActionResult> GetAllComments()
        {
            List<Comment> comments = await _commentService.GetComment();
            List<CommentDTO> commentDTOs = _mapper.Map<List<CommentDTO>>(comments);

            return Ok(commentDTOs);
        }

        [HttpGet]
        [Route("GetCommentById/{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            Comment comment = await _commentService.GetCommentById(id);

            if (comment == null)
            {
                return NotFound($"Comment id: {id} is not found!");
            }
            CommentDTO cDTO = _mapper.Map<CommentDTO>(comment);
            return Ok(cDTO);

        }

        [HttpPost]
        [Route("AddComment")]
        public async Task<IActionResult> AddComment(CommentDTO commentDTO)
        {
            Comment comment = _mapper.Map<Comment>(commentDTO);
            string result = await _commentService.AddComment(comment);
            return Ok(result);
        }

        [HttpPost]
        [Route("DeleteComment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            string result = await _commentService.DeleteComment(id);
            return Ok(result);
        }


        [HttpPut("api/UpdateComment/{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] CommentDTO commentDTO)
        {
            Comment c = _mapper.Map<Comment>(commentDTO);
            c.CommentId = id;
            try
            {
                string result = await _commentService.UpdateComment(c);
                return Ok(result); // Return success response
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}"); // Return error response
            }
        }

    }
}
