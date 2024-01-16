using System.ComponentModel.DataAnnotations;

namespace MicroserviceIdentityAPI.Shared.Models
{
    public class AccessCredentials
    {
        [Required(ErrorMessage = "Username é de preenchimento obrigatório")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password é de preenchimento obrigatório")]
        public string Password { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        [Required(ErrorMessage = "GrantType é de preenchimento obrigatório! Insira password para gerar um token!")]
        public string GrantType { get; set; } = string.Empty;

    }
}