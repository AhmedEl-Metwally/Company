using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels.Identity
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
