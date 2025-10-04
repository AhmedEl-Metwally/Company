using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels.Identity
{
    public class LoginViewModels
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
