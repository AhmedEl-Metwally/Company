using Company.DAL.Models.IdentityModels;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class AccountController(UserManager<ApplicationUsers> _userManager) : Controller
    {
        //Register
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) 
                return View(registerViewModel);

            var user = new ApplicationUsers
            {
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
            };

            var result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
            if (result.Succeeded)
                return RedirectToAction("Login");
            else
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(registerViewModel);
            }
        }





        //SignIn
        //SignOut
    }
}
