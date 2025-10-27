using ApiInescafe.DTOs.Product;
using ApiInescafe.Models;

namespace ApiInescafe.Services.Product;

public interface IProductService
{
    Task<ResponseModel<List<ProductModel>>> GetAllProductsAsync();
    Task<ResponseModel<ProductModel>> GetProductByIdAsync(int id);
    Task<ResponseModel<List<ProductModel>>> GetProductBySubstringAsync(string substring);
    Task<ResponseModel<List<ProductModel>>> CreateProductAsync(ProductCreateDto product);
    Task<ResponseModel<List<ProductModel>>> CreateReviewAsync(int id);
    Task<ResponseModel<List<ProductModel>>> UpdateProductAsync(ProductEditDto product);
    Task<ResponseModel<List<ProductModel>>> DeleteProductAsync(int id);
    Task<ResponseModel<List<ProductModel>>> DeleteReviewAsync(int id);
}