using NaturalCosmeticsECommerce.Models;
using System.Collections.Generic;

namespace NaturalCosmeticsECommerce.ViewModels
{
    public class UserRolesViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
