using Company.DAL.Models.IdentityModels;
using Company.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class UserController(UserManager<ApplicationUsers> _userManager, IWebHostEnvironment _webHostEnvironment) : Controller
    {
        //Index
        [HttpGet]
        public IActionResult Index(string SearchValues)
        {
            var  userQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValues))
                    userQuery = userQuery.Where(u =>u.Email.ToLower().Contains(SearchValues.ToLower()));

            var users = userQuery.Select(u => new UserViewModel 
            {
                Id = u.Id,
                FirstName = u.FirstName,    
                LastName = u.LastName,
                Email = u.Email,
               // Roles = _userManager.GetRolesAsync(u).Result

            }).ToList();

            foreach (var user in users)
                user.Roles = _userManager.GetRolesAsync(_userManager.FindByIdAsync(user.Id).Result).Result;

            return View(users);
        }

        //Details
        [HttpGet]
        public IActionResult Details(string? id)
        {
            if(id is null)
                return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;

            if(user is null)
               return NotFound();
            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName ?? string.Empty,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };
            return View(userViewModel);
        }

        //Edit
        public IActionResult Edit(string? id )
        {
            if (id is null)
                return BadRequest();
            var user = _userManager.FindByIdAsync(id).Result;

            if (user is null)
                return NotFound();
            return View(new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName ?? string.Empty,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            }); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult Edit(UserViewModel userViewModel,string id)
        {
            if(!ModelState.IsValid)
                return View(userViewModel);
            if(userViewModel.Id != id )
                return BadRequest();
            string message = string.Empty;  

            try 
            {
                var user = _userManager.FindByIdAsync(id).Result;
                if(user is null)
                    return NotFound();
                user.FirstName = userViewModel.FirstName;
                user.LastName = userViewModel.LastName;
                user.Email = userViewModel.Email;

                var result = _userManager.UpdateAsync(user).Result;
                if(result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message ="Failed to update user";
            }
            catch(Exception ex) 
            {
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Failed to update user";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(userViewModel);
        }

        //Delete
        public IActionResult Delete(string id)
        { 
            var user = _userManager.FindByIdAsync(id).Result;
            if (user is null)
                return NotFound();
            string message = string.Empty;  

            try 
            {
                var result = _userManager.DeleteAsync(user).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Failed to delete user";
            }
            catch (Exception ex)
            {
                 message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Failed to delete user";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
        }
    }
}
