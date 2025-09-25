using Microsoft.AspNetCore.Identity;


namespace Company.DAL.Models.IdentityModels
{
    public class ApplicationUsers : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
    }
}
