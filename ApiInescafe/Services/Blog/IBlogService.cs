using ApiInescafe.Models;

namespace ApiInescafe.Services.Blog;

public interface IBlogService
{
    Task<ResponseModel<List<BlogModel>>> GetAllBlogPostsAsync();
    Task<ResponseModel<BlogModel>> GetBlogPostByIdAsync(int id);
    Task<ResponseModel<List<BlogModel>>> CreateBlogPostAsync(BlogCreatePostDto blogPost, int userId);
    Task<ResponseModel<List<BlogModel>>> DeleteBlogPostAsync(int id, int userId);
    Task<ResponseModel<bool>> LikeBlogPostAsync(int blogPostId, int userId);
}
