using ApiInescafe.Data;
using ApiInescafe.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiInescafe.Services.Blog;

public class BlogService : IBlogService
{
    public readonly AppDbContext _context;

    public BlogService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<List<BlogModel>>> CreateBlogPostAsync(BlogCreatePostDto blogPost, int userId)
    {
        var response = new ResponseModel<List<BlogModel>>();
        try
        {
            var Post = new BlogModel
            {
                Title = blogPost.Title,
                Content = blogPost.Content,
                AuthorId = userId
            };
            _context.BlogPosts.Add(Post);
            await _context.SaveChangesAsync();
            var posts = await _context.BlogPosts.ToListAsync();
            response.Data = posts;
            response.Message = "Post do blog criado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao criar o post do blog: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<BlogModel>>> DeleteBlogPostAsync(int id, int userId)
    {
        var response = new ResponseModel<List<BlogModel>>();
        try
        {
            var post = await _context.BlogPosts.FirstOrDefaultAsync(p => p.Id == id && p.Author.Id == userId);
            if (string.IsNullOrEmpty(post?.Content))
            {
                response.Message = "Post do blog não encontrado.";
                return response;
            }
            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();
            var posts = await _context.BlogPosts.ToListAsync();
            response.Data = posts;
            response.Message = "Post do blog deletado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao deletar o post do blog: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<BlogModel>>> GetAllBlogPostsAsync()
    {
        var response = new ResponseModel<List<BlogModel>>();
        try
        {
            var posts = _context.BlogPosts
                                .Include(blog => blog.Likes)
                                .ToListAsync();
            if (posts.Result.Count == 0)
            {
                response.Message = "Nenhum post do blog encontrado.";
                response.Status = true;
                return response; 
            }
            response.Data = posts.Result;
            response.Message = "Posts do blog recuperados com sucesso.";
            response.Status = true;
            return response; 
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar os posts do blog: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<BlogModel>> GetBlogPostByIdAsync(int id)
    {
        var response = new ResponseModel<BlogModel>();
        try
        {
            var post = await _context.BlogPosts
                            .Include(blog => blog.Likes)
                            .FirstOrDefaultAsync(p => p.Id == id);
            if (string.IsNullOrEmpty(post?.Content))
            {
                response.Message = "Post do blog não encontrado.";
                return response;
            }
            response.Data = post;
            response.Message = "Post do blog recuperado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar o post do blog: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<bool>> LikeBlogPostAsync(int blogPostId, int userId)
    {
        var response = new ResponseModel<bool>();
        try
        {
            var like =  await _context.BlogLikes
                .FirstOrDefaultAsync(l => l.BlogId == blogPostId && l.User.Id == userId);
            if (string.IsNullOrEmpty(like?.BlogId.ToString()))
            {
                var newLike = new blogLikesModel
                {
                    BlogId = blogPostId,
                    UserId = userId
                };
                _context.BlogLikes.Add(newLike);
                response.Message = "Post do blog curtido com sucesso por: "+userId+".";
            }
            else
            {
                _context.BlogLikes.Remove(like);
                response.Message = "Curtida removida do post do blog com sucesso por: "+userId+".";

            }
            await _context.SaveChangesAsync();
            response.Status = true;
            response.Data = true;
            return response;
        }
        catch (DbUpdateException)
        {
        response.Message = "Não foi possível curtir. O post ou o usuário não foi encontrado.";
        response.Status = false;
        response.Data = false;
        return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao curtir o post do blog: " + e.Message;
            response.Status = false;
            return response;
        }
    }
}