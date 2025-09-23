using Company.BLL.Dtos.EmployeeDtos;
using Company.BLL.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class EmployeeController
                                  (
                                   IEmployeeService _employeeService,
                                   IWebHostEnvironment _webHostEnvironment,
                                   ILogger<EmployeeController> _logger
                                  ) : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployee();
            return View(employees);
        }

        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto createdEmployeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int result = _employeeService.CreateEmployee(createdEmployeeDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "CreatedEmployee can not created");
                }
                catch (Exception ex)
                {
                    //Development ==> action , log error in console , view
                    if (_webHostEnvironment.IsDevelopment())
                        _logger.LogError($"CreatedEmployee can not be create because : {ex.Message}");
                    else
                    {
                        _logger.LogError($"\"CreatedEmployee can not be create because : {ex}");
                        return View("ErrorView");
                    }
                }

            }
            return View(createdEmployeeDto);
        }

        //Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(!id.HasValue)
                return BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            return (employee is null) ? NotFound() : View(employee);
        }

    }
}
