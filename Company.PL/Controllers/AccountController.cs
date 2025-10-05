using Company.BLL.Services.EmailSettings;
using Company.DAL.Models.IdentityModels;
using Company.DAL.Models.Shared;
using Company.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class AccountController
                                    (
                                        UserManager<ApplicationUsers> _userManager,
                                        SignInManager<ApplicationUsers> _signInManager,
                                        IEmailSetting _emailSetting
                                    ) : Controller
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


        //Login & SignIn
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModels loginViewModels)
        {
            if (!ModelState.IsValid)
                return View(loginViewModels);

            // 1- Find user by email
            // 2- If user is not null 
            // 3- Check the password
            // 4- sign in the user
            // 5- Check if the user is not locked or owned

            var user = _userManager.FindByEmailAsync(loginViewModels.Email).Result;
            if (user is not null)
            {
                var flag = _userManager.CheckPasswordAsync(user,loginViewModels.Password).Result;
                if (flag)
                {
                   var result = _signInManager.PasswordSignInAsync(user,loginViewModels.Password,loginViewModels.RememberMe,false).Result;
                    if(result.IsNotAllowed)
                        ModelState.AddModelError(string.Empty, "Your account not allowed");
                    if(result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your account is locked");
                    if(result.Succeeded)
                        return RedirectToAction(nameof(HomeController.Index), "Home");
                   
                }
            }
            ModelState.AddModelError(string.Empty, "Oooops Something went wrong.......");
            return View(loginViewModels);
        }


        //Logout & SignOut  
        [HttpGet]
        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction(nameof(Login));
        }


        //Forget Password
        [HttpGet]
        public IActionResult ForgetPassword() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendResetPasswordUrl(ForgetPasswordViewModel forgetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(forgetPasswordViewModel.Email).Result;
                if (user is not null)
                {
                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var url = Url.Action("ResetPassword", "Account", new { email = forgetPasswordViewModel.Email, token }, Request.Scheme);
                    var email = new Email()
                    {
                        To = forgetPasswordViewModel.Email,
                        Subject = "Reset your password",
                        Body = url,
                    };
                    _emailSetting.sendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
                else
                    ModelState.AddModelError(string.Empty, "Oooops Something went wrong.......");
            }
            return View(forgetPasswordViewModel);
        }

        [HttpGet]
        public IActionResult CheckYourInbox() => View();    


    }
}
