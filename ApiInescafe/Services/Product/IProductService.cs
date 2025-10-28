using ApiInescafe.DTOs.Product;
using ApiInescafe.DTOs.Review;
using ApiInescafe.Models;

namespace ApiInescafe.Services.Product;

public interface IProductService
{
    Task<ResponseModel<List<ProductModel>>> GetAllProductsAsync();
    Task<ResponseModel<ProductModel>> GetProductByIdAsync(int id);
    Task<ResponseModel<List<ProductModel>>> GetProductBySubstringAsync(string substring);
    Task<ResponseModel<List<ProductModel>>> CreateProductAsync(ProductCreateDto product);
    Task<ResponseModel<List<ReviewModel>>>  CreateReviewAsync(CreateReviewDto reviewDto, int userId);
    Task<ResponseModel<List<ProductModel>>> UpdateProductAsync(ProductEditDto product);
    Task<ResponseModel<List<ProductModel>>> DeleteProductAsync(int id);
    Task<ResponseModel<List<ReviewModel>>> DeleteReviewAsync(int ProductId, int userId);
}