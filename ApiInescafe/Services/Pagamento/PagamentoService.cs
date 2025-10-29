using Abacatepay;
using ApiInescafe.Data;
using ApiInescafe.DTOs.Pagamento;
using ApiInescafe.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json; // <-- 1. IMPORTAR NEWTONSOFT

namespace ApiInescafe.Services.Pagamento;

public class PagamentoService : IPagamentoService // <-- Use sua interface aqui
{
    private readonly dynamic _abacatePayClient;
    private readonly ILogger<PagamentoService> _logger;
    private readonly AppDbContext _context; 

    public PagamentoService(
        dynamic abacatePayClient, 
        ILogger<PagamentoService> logger,
        AppDbContext context)
    {
        _abacatePayClient = abacatePayClient;
        _logger = logger;
        _context = context;
    }

    public async Task<CriarCobrancaResponse> CriarCobrancaPixAsync(CriarCobrancaRequest request)
    {
        try
        {
            var body = new
            {
                frequency = "ONE_TIME",
                methods = new[] { "PIX" },
                products = new[]
                {
                    new
                    {
                        externalId = Guid.NewGuid().ToString(),
                        name = request.NomeProduto,
                        quantity = 1,
                        price = request.ValorEmCentavos
                    }
                },
                returnUrl = "https://seusite.com/retorno",
                completionUrl = "https://seusite.com/sucesso",
                customer = new
                {
                    name = request.NomeCliente,
                    email = request.EmailCliente,
                    taxId = request.CpfCliente,
                    cellphone = request.cellphone
                }
            };

            // 1. Chamar a API. Ela retorna uma STRING JSON.
            string jsonResponse = await Task.Run(() => 
                _abacatePayClient.PixBillingCreate(null, body)
            );

            dynamic response;
            try
            {
                // 2. Desserializar (Parsear) a string JSON
                response = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            }
            catch (Exception parseEx)
            {
                // 3. Se não for JSON, é um erro de texto puro (ex: API Key errada)
                _logger.LogError(parseEx, "A API AbacatePay retornou um erro em texto (não-JSON): {ApiErrorResponse}", jsonResponse);
                throw new ApplicationException($"Falha na API AbacatePay: {jsonResponse}");
            }

            // 4. Verificar se DENTRO do JSON existe a propriedade 'error'
            if (response.error != null)
            {
                string errorMessage = response.error.ToString();
                _logger.LogError("A API AbacatePay retornou um erro JSON: {ApiError}", errorMessage);
                throw new ApplicationException($"Falha na API AbacatePay: {errorMessage}");
            }

            // 5. SUCESSO! A API retornou dados válidos.
            var responseData = response.data;
            string cobrancaId = responseData.id;
            string urlPagamento = responseData.url;

            // 6. AGORA SIM, crie e salve o Pedido no banco com o ID da cobrança
            var novoPedido = new PedidoModel
            {
                NomeProduto = request.NomeProduto,
                ValorEmCentavos = request.ValorEmCentavos,
                Status = "Pendente",
                CobrancaId = cobrancaId // <-- 7. SALVAR O ID É CRÍTICO
                // Salve outros dados se necessário (ex: request.EmailCliente)
            };

            _context.Pedidos.Add(novoPedido);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation("Pedido ID {PedidoId} criado com CobrancaId: {CobrancaId}", novoPedido.Id, cobrancaId);

            // 8. Retorne a resposta correta para o frontend
            return new CriarCobrancaResponse
            {
                IdCobranca = cobrancaId,
                UrlPagamento = urlPagamento // <-- Usando o DTO corrigido
            };
        }
        catch (ApplicationException ex)
        {
            // Re-lança erros da API que já tratamos
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao criar cobrança PIX.");
            throw new ApplicationException("Erro interno ao processar pagamento.", ex);
        }
    }

    public async Task ProcessarWebhookAsync(dynamic payload)
    {
        try
        {
            string eventType = payload.eventType;
            _logger.LogInformation("Webhook AbacatePay recebido: {EventType}", eventType);

            // TODO: VALIDAR A ASSINATURA DO WEBHOOK (MUITO IMPORTANTE)
            // (Verificar o header 'abacatepay-signature')

            if (eventType == "PAYMENT_CONFIRMED")
            {
                // 9. CORREÇÃO: O ID é uma STRING
                string cobrancaId = payload.data.id;
                _logger.LogInformation("Pagamento confirmado para Cobrança ID: {CobrancaId}", cobrancaId);

                // 10. Esta busca agora vai funcionar, pois salvamos o CobrancaId
                var pedido = await _context.Pedidos.FirstOrDefaultAsync(p => p.CobrancaId == cobrancaId);

                if (pedido != null && pedido.Status != "Pago")
                {
                    pedido.Status = "Pago";
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Pedido ID {PedidoId} atualizado para 'Pago'.", pedido.Id);
                    // Aqui você pode adicionar lógica para liberar o acesso ao produto/serviço
                }
                else
                {
                    _logger.LogWarning("Pedido com CobrancaId {CobrancaId} não encontrado ou já está pago.", cobrancaId);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao processar webhook do AbacatePay.");
            // Não relance a exceção aqui, retorne Ok() no controller
        }
    }
}