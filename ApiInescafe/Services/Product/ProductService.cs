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

    // public async Task<ResponseModel<List<ProductModel>>> CreateReviewAsync(CreateReviewDto review)
    // {
    //     var response = new ResponseModel<List<ProductModel>>();
    //     try
    //     {
    //         var Product = product.Data;
    //         var review = new CreateReviewDto
    //         {
    //         };
    //         return response;
    //     }
    //     catch (Exception e)
    //     {
    //         response.Message = "Erro ao recuperar os produtos: " + e.Message;
    //         response.Status = false;
    //         return response;
    //     }
    // }

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

    // public Task<ResponseModel<List<ProductModel>>> DeleteReviewAsync(int ProductId)
    // {
        
    // }

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