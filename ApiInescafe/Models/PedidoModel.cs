using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiInescafe.Models;

public class PedidoModel
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string CobrancaId { get; set; } // ID da cobrança gerado pela AbacatePay

    [Required]
    public string NomeProduto { get; set; } = string.Empty;

    [Required]
    public int ValorEmCentavos { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty; // Ex: "Pendente", "Pago", "Cancelado"
    
}

