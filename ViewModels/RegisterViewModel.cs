// ViewModels/RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace NaturalCosmeticsECommerce.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad gerekli")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Soyad gerekli")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Kullanıcı adı gerekli")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email gerekli")]
        [EmailAddress(ErrorMessage = "Geçerli bir email girin")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre gerekli")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre tekrar gerekli")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
