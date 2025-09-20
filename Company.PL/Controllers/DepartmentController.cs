using Company.BLL.Repositories.Interface;
using Company.BLL.Repositories.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService) : Controller
    {
        //Index
        [HttpGet]
        public IActionResult Index()
        {
            var department = _departmentService.GetAllDepartments(); 
            return View(department);
        }
    }
}
