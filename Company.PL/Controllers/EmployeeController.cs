using Company.BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployee();
            return View(employees);
        }
    }
}
