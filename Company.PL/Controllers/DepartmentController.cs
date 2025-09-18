using Company.BLL.Repositories.Interface;
using Company.BLL.Repositories.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
