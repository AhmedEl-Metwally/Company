using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels.Identity
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;
    }
}
