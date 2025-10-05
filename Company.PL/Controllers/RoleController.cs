using Company.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
using System.Runtime.Intrinsics.X86;

namespace Company.PL.Controllers
{
    public class RoleController(RoleManager<IdentityRole> _roleManager, IWebHostEnvironment _webHostEnvironment) : Controller
    {
        //Index
        [HttpGet]
        public IActionResult Index(string SearchValues)
        {
            var rolesQuery = _roleManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(SearchValues))
                rolesQuery = rolesQuery.Where(r => r.Name.ToLower().Contains(SearchValues.ToLower()));

            var roles = rolesQuery.Select(r => new RoleViewModel()
            {
                Id = r.Id,
                Name = r.Name,
            }).ToList();

            return View(roles);
        }

        //Details
        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (id is null)
                return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null)
                return NotFound();
            var roleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(roleViewModel);
        }

        //Edit
        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (id is null)
                return BadRequest();
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null)
                return NotFound();
            var roleViewModel = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name,
            };
            return View(roleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string? id,RoleViewModel roleViewModel) 
        {
            if (!ModelState.IsValid)
                return View(roleViewModel);
            if (id is null || id != roleViewModel.Id)
                return BadRequest();
            string message = string.Empty;
            try
            {
                 var role = _roleManager.FindByIdAsync(id).Result;
                   if (role is null)
                            return BadRequest();
                 role.Name = roleViewModel.Name;
                var result = _roleManager.UpdateAsync(role).Result;

                 if (result.Succeeded)
                  return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Failed to update role";
            }
            return View(roleViewModel);

        }

        //Delete
        public IActionResult Delete(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role is null)
                return NotFound();
            string message = string.Empty;

            try
            {
                var result = _roleManager.DeleteAsync(role).Result;
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    message = "Failed to delete role";
            }
            catch (Exception ex)
            {
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "Failed to delete role";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
        }

        //Create
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleViewModel roleViewModel)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                var existingRole = _roleManager.CreateAsync(new IdentityRole{ Name =roleViewModel.Name}).Result;
                if (existingRole.Succeeded)
                    return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "Role already exists");
            return View(roleViewModel);
        }

    }
}
