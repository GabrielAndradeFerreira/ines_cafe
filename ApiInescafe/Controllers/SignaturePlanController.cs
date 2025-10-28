using System.Security.Claims;
using ApiInescafe.DTOs.Product;
using ApiInescafe.DTOs.Review;
using ApiInescafe.Models;
using ApiInescafe.Services.Product;
using ApiInescafe.Services.SignaturePlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace  ApiInescafe.Controller;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SignaturePlanController : ControllerBase
{
private readonly ISignaturePlanService _signaturePlanService;
    public SignaturePlanController(ISignaturePlanService signaturePlanService)
    {
        _signaturePlanService = signaturePlanService;
    }

    [HttpGet("GetAllProducts")]
    public async Task<ActionResult<ResponseModel<List<SignaturePlanModel>>>> GetAllSignaturePlansAsync()
    {
        var result = await _signaturePlanService.GetAllSignaturePlansAsync();
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
    
    [HttpGet("GetProductById/{id}")]
    public async Task<ActionResult<ResponseModel<SignaturePlanModel>>> GetSignaturePlanByIdAsync(int id)
    {
        var result = await _signaturePlanService.GetSignaturePlanByIdAsync(id);
        if (result.Status == true)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpPost("SignPlan/{id}")]
    public async Task<ActionResult<ResponseModel<SignaturePlanModel>>> SignSignaturePlanAsync(int id)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized(new ResponseModel<SignaturePlanModel> { Status = false, Message = "Usuário não autenticado." });
        }

        if (!int.TryParse(userIdString, out int userId))
        {
            return BadRequest(new ResponseModel<SignaturePlanModel> { Status = false, Message = "Formato do ID de usuário no token é inválido." });
        }
        var result = await _signaturePlanService.SignSignaturePlanAsync(id, userId);
        if (result.Status)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }
}