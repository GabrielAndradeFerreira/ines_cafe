using System.ComponentModel.DataAnnotations;

namespace ApiInescafe.DTOs;

public class ContatoDTO
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public required string Name { get; set; }
        
        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public required string Email { get; set; }
        
        [Required(ErrorMessage = "Assunto é obrigatório")]
        public required string Topic { get; set; }
        
        [Required(ErrorMessage = "Mensagem é obrigatória")]
        public required string Message { get; set; }
    }