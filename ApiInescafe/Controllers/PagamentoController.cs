using ApiInescafe.DTOs.Pagamento;
using ApiInescafe.Services.Pagamento;
using Microsoft.AspNetCore.Mvc;
namespace ApiInescafe.Controller;

[ApiController]
[Route("api/[controller]")]
public class PagamentoController : ControllerBase
{
    private readonly IPagamentoService _pagamentoService;

    public PagamentoController(IPagamentoService pagamentoService)
    {
        _pagamentoService = pagamentoService;
    }

    [HttpPost("pix")]
    public async Task<IActionResult> CriarCobrancaPix([FromBody] CriarCobrancaRequest request)
    {
        try
        {
            var resposta = await _pagamentoService.CriarCobrancaPixAsync(request);
            return Ok(resposta);
        }
        catch (ApplicationException ex)
        {
            // Erro de negócio tratado
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (Exception ex)
        {
            // Erro inesperado
            return StatusCode(500, new { mensagem = "Erro interno no servidor.", erro = ex.Message });
        }
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> WebhookAbacatePay([FromBody] dynamic payload)
    {
        await _pagamentoService.ProcessarWebhookAsync(payload);
        
        return Ok();
    }
}