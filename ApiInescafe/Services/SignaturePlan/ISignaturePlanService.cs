using ApiInescafe.Models;

namespace ApiInescafe.Services.SignaturePlan;

public interface ISignaturePlanService
{
    Task<ResponseModel<List<SignaturePlanModel>>> GetAllSignaturePlansAsync();
    Task<ResponseModel<SignaturePlanModel>> GetSignaturePlanByIdAsync(int id);
    Task<ResponseModel<bool>> SignSignaturePlanAsync(int id, int userId);
}