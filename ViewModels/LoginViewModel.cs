using System.ComponentModel.DataAnnotations;

namespace NaturalCosmeticsECommerce.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "E-posta gerekli")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Şifre gerekli")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
