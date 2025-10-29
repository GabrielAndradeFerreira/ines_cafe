using ApiInescafe.DTOs.Pagamento;

namespace ApiInescafe.Services.Pagamento;


public interface IPagamentoService
{
    // Retorna os dados do PIX para o frontend
    Task<CriarCobrancaResponse> CriarCobrancaPixAsync(CriarCobrancaRequest request);

    // Processa o webhook recebido do AbacatePay
    Task ProcessarWebhookAsync(dynamic payload);
}