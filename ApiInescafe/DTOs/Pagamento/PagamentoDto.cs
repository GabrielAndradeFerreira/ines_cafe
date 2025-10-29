namespace ApiInescafe.DTOs.Pagamento;

public class CriarCobrancaRequest
{
    public string NomeCliente { get; set; }
    public string EmailCliente { get; set; }
    public string CpfCliente { get; set; }
    public int ValorEmCentavos { get; set; }
    public string NomeProduto { get; set; }
    public string cellphone { get; set; }
}

// DTO para enviar a resposta ao frontend
public class CriarCobrancaResponse
{
    public string IdCobranca { get; set; }
    
    // Troque as propriedades do QR Code por esta:
    public string UrlPagamento { get; set; } 
}