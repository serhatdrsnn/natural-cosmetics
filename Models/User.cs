using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace NaturalCosmeticsECommerce.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Favori ürünlerle ilişki - null olmasın, başlangıçta boş list // hata aldım burda.
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}