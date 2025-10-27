using System.Security.Claims;
using ApiInescafe.Models;
using ApiInescafe.Services;
using ApiInescafe.Services.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiInescafe.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpGet("GetAllBlogPosts")]
    public async Task<ActionResult<ResponseModel<List<BlogModel>>>> GetAllBlogPosts()
    {
        var result = await _blogService.GetAllBlogPostsAsync();
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    [HttpGet("GetBlogPostById/{id}")]
    public async Task<ActionResult<ResponseModel<BlogModel>>> GetBlogPostById(int id)
    {
        var result = await _blogService.GetBlogPostByIdAsync(id);
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("CreateBlogPost")]
    public async Task<ActionResult<ResponseModel<List<BlogModel>>>> CreateBlogPostAsync([FromBody] BlogCreatePostDto blogPost)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized();
        }
        if (!int.TryParse(userIdString, out int userId))
        {
            return BadRequest(new { Message = "Formato do ID de usuário no token é inválido." });
        }
        var result = await _blogService.CreateBlogPostAsync(blogPost, userId);
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpDelete("DeleteBlogPost/{id}")]
    public async Task<ActionResult<ResponseModel<List<BlogModel>>>> DeleteBlogPostAsync(int id)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized();
        }
        if (!int.TryParse(userIdString, out int userId))
        {
            return BadRequest(new { Message = "Formato do ID de usuário no token é inválido." });
        }
        var result = await _blogService.DeleteBlogPostAsync(id, userId);
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);

    }

    [HttpPut("LikeBlogPost")]
    public async Task<ActionResult<ResponseModel<bool>>> LikeBlogPostAsync(int blogPostId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized();
        }
        if (!int.TryParse(userIdString, out int userId))
        {
            return BadRequest(new { Message = "Formato do ID de usuário no token é inválido." });
        }
        var result = await _blogService.LikeBlogPostAsync(blogPostId, userId);
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

}