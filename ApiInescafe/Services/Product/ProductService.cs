using System.Runtime.CompilerServices;
using ApiInescafe.Data;
using ApiInescafe.DTOs.Product;
using ApiInescafe.DTOs.Review;
using ApiInescafe.Models;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace ApiInescafe.Services.Product;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext _context)
    {
        this._context = _context;
    }

    public async Task<ResponseModel<List<ProductModel>>> CreateProductAsync(ProductCreateDto product)
    {
        var response = new ResponseModel<List<ProductModel>>();
        try
        {
            var Product = new ProductModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl
            };
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            var products = await _context.Products.ToListAsync();
            response.Data = products;
            response.Message = "Produto criado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar os produtos: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<ReviewModel>>> CreateReviewAsync(CreateReviewDto review, int userId)
        {
            var response = new ResponseModel<List<ReviewModel>>();
            try
            {
                var product = await _context.Products.Include(p => p.Reviews).FirstOrDefaultAsync(p => p.Id == review.ProductId);
                if (product == null)
                {
                    response.Status = false;
                    response.Message = "Produto não encontrado.";
                    return response;
                }

                var userHasReviewed = product.Reviews.Any(r => r.UserId == userId);
                if (userHasReviewed)
                {
                    response.Status = false;
                    response.Message = "Usuário já fez uma avaliação para este produto.";
                    return response;
                }

                var newReview = new ReviewModel
                {
                    ProductId = review.ProductId,
                    UserId = userId,
                    Comment = review.Comment,
                    Rating = review.Rating,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Reviews.Add(newReview);
                await _context.SaveChangesAsync();
                var reviews = (await _context.Reviews.ToListAsync())
                    .Where(r => r.ProductId == review.ProductId)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToList();
                response.Data = reviews;
                response.Message = "Avaliação criada com sucesso.";
                return response;
            }
            catch (Exception e)
            {
                response.Message = "Erro ao criar a avaliação: " + e.Message;
                response.Status = false;
                return response;
            }
        }

    public async Task<ResponseModel<List<ProductModel>>> DeleteProductAsync(int id)
    {
        var response = new ResponseModel<List<ProductModel>>();
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (string.IsNullOrEmpty(product?.Name))
            {
                response.Message = "Produto não encontrado.";
                return response;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            var products = await _context.Products.ToListAsync();
            response.Data = products;
            response.Message = "Produto deletado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar os produtos: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<ReviewModel>>> DeleteReviewAsync(int ProductId, int userId)
        {
            var response = new ResponseModel<List<ReviewModel>>();
            try
            {
                var product = await _context.Products.Include(p => p.Reviews).FirstOrDefaultAsync(p => p.Id == ProductId);
                if (product == null)
                {
                    response.Status = false;
                    response.Message = "Produto não encontrado.";
                    return response;
                }

                var userHasReviewed = product.Reviews.Any(r => r.UserId == userId);
                if (!userHasReviewed)
                {
                    response.Status = false;
                    response.Message = "Usuário nunca fez uma avaliação para este produto.";
                    return response;
                }
                var review = product.Reviews.FirstOrDefault(r => r.UserId == userId && r.ProductId == ProductId);
                if (review == null)
                {
                    response.Status = false;
                    response.Message = "Avaliação não encontrada.";
                    return response;
                }
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
                var reviews = (await _context.Reviews.ToListAsync())
                    .Where(r => r.ProductId == review.ProductId)
                    .OrderByDescending(r => r.CreatedAt)
                    .ToList();
                response.Data = reviews;
                response.Message = "Avaliação removida com sucesso.";
                return response;
            }
            catch (Exception e)
            {
                response.Message = "Erro ao remover a avaliação: " + e.Message;
                response.Status = false;
                return response;
            }
        }

    public async Task<ResponseModel<List<ProductModel>>> GetAllProductsAsync()
    {
        var response = new ResponseModel<List<ProductModel>>();
        try
        {
            var products = await _context.Products.ToListAsync();
            response.Data = products;
            response.Message = "Todos os produtos foram recuperados com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar os produtos: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<ProductModel>> GetProductByIdAsync(int id)
    {
        var response = new ResponseModel<ProductModel>();
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (string.IsNullOrEmpty(product?.Name))
            {
                response.Message = "Produto não encontrado.";
                response.Status = false;
                return response;
            }
            response.Data = product;
            response.Message = "Produto recuperado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar o produto: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<ProductModel>>> GetProductBySubstringAsync(string substring)
    {
        var response = new ResponseModel<List<ProductModel>>();
        try
        {
            var products = _context.Products
                .Where(p => p.Name.Contains(substring))
                .ToListAsync();
            if (products.Result.Count == 0)
            {
                response.Message = "Nenhum produto encontrado com o termo fornecido.";
                response.Status = true;
                return response;
            }
            response.Data = products.Result;
            response.Message = "Produtos recuperados com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar o produto: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<ProductModel>>> UpdateProductAsync(ProductEditDto product)
    {
        var response = new ResponseModel<List<ProductModel>>();
        try
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (string.IsNullOrEmpty(existingProduct?.Name))
            {
                response.Message = "Produto não encontrado.";
                return response;
            }
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.ImageUrl = product.ImageUrl;
            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
            var products = await _context.Products.ToListAsync();
            response.Data = products;
            response.Message = "Produto atualizado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar o produto: " + e.Message;
            response.Status = false;
            return response;
        }
    }

}