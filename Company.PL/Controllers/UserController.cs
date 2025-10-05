using Company.DAL.Models.IdentityModels;
using Company.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class UserController(UserManager<ApplicationUsers> _userManager) : Controller
    {
        [HttpGet]
        public IActionResult Index(string SearchValues)
        {
            var  userQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValues))
                userQuery.Where(u =>u.Email.ToLower().Contains(SearchValues.ToLower()));

            var users = userQuery.Select(u => new UserViewModel 
            {
                Id = u.Id,
                FirstName = u.FirstName,    
                LastName = u.LastName,
                Email = u.Email,
                //Roles
            }).ToList();

            foreach (var user in users)
                user.Roles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(user.Id).Result).Result;

            return View(users);
        }
    }
}
