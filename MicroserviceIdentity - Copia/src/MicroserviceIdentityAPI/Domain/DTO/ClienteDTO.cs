using System.ComponentModel.DataAnnotations;

namespace MicroserviceIdentityAPI.Domain.DTO
{
    public class ClienteDTO
    {
        [Required(ErrorMessage = "Nome é de preenchimento obrigatório")]
        [StringLength(50, ErrorMessage = "Nome deve conter entre 3 e 50 caracteres", MinimumLength = 3)]
        public string Nome { get; set; }
        
        [StringLength(50, ErrorMessage = "Codigo do cliente deve conter entre 3 e 50 caracteres", MinimumLength = 3)]
        public string? CodigoCliente { get; set; }
    }
}