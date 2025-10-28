using ApiInescafe.Data;
using ApiInescafe.DTOs.SignaturePlan;
using ApiInescafe.Models;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace ApiInescafe.Services.SignaturePlan;

public class SignaturePlanService : ISignaturePlanService
{
    private readonly AppDbContext _context;

    public SignaturePlanService(AppDbContext _context)
    {
        this._context = _context;
    }
    public async Task<ResponseModel<List<SignaturePlanModel>>> GetAllSignaturePlansAsync()
    {
        var response = new ResponseModel<List<SignaturePlanModel>>();
        try
        {
            var planos = await _context.SignaturePlans.ToListAsync();
            if (planos.Count() == 0)
            {
                response.Message = "Nenhum plano de assinatura encontrado.";
                response.Status = false;
                return response;
            }
            response.Message = "Planos de assinatura recuperados com sucesso.";
            response.Status = true;
            response.Data = planos;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar os planos de assinatura: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<SignaturePlanModel>> GetSignaturePlanByIdAsync(int id)
    {
        var response = new ResponseModel<SignaturePlanModel>();
        try
        {
            var plano = await _context.SignaturePlans.FirstOrDefaultAsync(p => p.Id == id);
            if (plano == null)
            {
                response.Message = "Nenhum plano de assinatura encontrado.";
                response.Status = false;
                return response;
            }
            response.Message = "Plano de assinatura recuperado com sucesso.";
            response.Status = true;
            response.Data = plano;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar os planos de assinatura: " + e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<bool>> SignSignaturePlanAsync(int id, int userId)
    {
        var response = new ResponseModel<bool>();
        try
        {
            var plano = await _context.SignaturePlans.FirstOrDefaultAsync(p => p.Id == id);
            if (plano == null)
            {
                response.Message = "Nenhum plano de assinatura encontrado.";
                response.Status = false;
                return response;
            }
            var newSignMember = new SignPlanDto
            {
                SignaturePlanId = id,
                UserId = userId
            };
            _context.Add(newSignMember);
            await _context.SaveChangesAsync();
            response.Message = "Plano de assinatura assinado com sucesso.";
            response.Status = true;
            return response;
        }
        catch (Exception e)
        {
            response.Message = "Erro ao recuperar os planos de assinatura: " + e.Message;
            response.Status = false;
            return response;
        }
    }
}