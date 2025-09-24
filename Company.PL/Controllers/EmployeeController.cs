using Company.BLL.Dtos.EmployeeDtos;
using Company.BLL.Services.Interface;
using Company.DAL.Models.EmployeeModule;
using Company.DAL.Models.Shared;
using Company.PL.ViewModels;
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
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees = _employeeService.GetAllEmployee(EmployeeSearchName);
            return View(employees);
        }

        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedAndUpdatedEmployeeViewModel createdAndUpdatedEmployeeViewModel )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdEmployeeDto = new CreatedEmployeeDto
                    {
                        Name = createdAndUpdatedEmployeeViewModel.Name,
                        Age = createdAndUpdatedEmployeeViewModel.Age,
                        Address = createdAndUpdatedEmployeeViewModel.Address,
                        Salary = createdAndUpdatedEmployeeViewModel.Salary,
                        IsActive = createdAndUpdatedEmployeeViewModel.IsActive,
                        Email = createdAndUpdatedEmployeeViewModel.Email,
                        PhoneNumber = createdAndUpdatedEmployeeViewModel.PhoneNumber,
                        HiringDate = createdAndUpdatedEmployeeViewModel.HiringDate,
                        Gender = createdAndUpdatedEmployeeViewModel.Gender,
                        EmployeeType = createdAndUpdatedEmployeeViewModel.EmployeeType,
                        DepartmentId = createdAndUpdatedEmployeeViewModel.DepartmentId
                    };

                        int result = _employeeService.CreateEmployee(createdEmployeeDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty, "CreatedEmployee can not created");
                }
                catch (Exception ex)
                {
                    if (_webHostEnvironment.IsDevelopment())
                        _logger.LogError($"CreatedEmployee can not be create because : {ex.Message}");
                    else
                    {
                        _logger.LogError($"\"CreatedEmployee can not be create because : {ex}");
                        return View("ErrorView");
                    }
                }

            }
            return View(createdAndUpdatedEmployeeViewModel);
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

        //Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(!id.HasValue)
                return BadRequest();
            var empolee = _employeeService.GetEmployeeById(id.Value);
            if(empolee is null)
                return NotFound();
            return View(new CreatedAndUpdatedEmployeeViewModel()
            {
                Name = empolee.Name,
                Age = empolee.Age,
                Address = empolee.Address,
                Salary = empolee.Salary,
                IsActive = empolee.IsActive,
                Email = empolee.Email,
                PhoneNumber = empolee.PhoneNumber,
                HiringDate = empolee.HiringDate,
                Gender = Enum.Parse<Gender>(empolee.Gender),
                EmployeeType = Enum.Parse<EmployeeType>(empolee.EmployeeType),
                DepartmentId = empolee.DepartmentId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id , CreatedAndUpdatedEmployeeViewModel createdAndUpdatedEmployeeViewModel )
        {
            if(!id.HasValue)
                return BadRequest();
            if (!ModelState.IsValid)
                return View(createdAndUpdatedEmployeeViewModel);

            try
            {
                var updatedEmployeeDto = new UpdatedEmployeeDto
                {
                    Id = id.Value,
                    Name = createdAndUpdatedEmployeeViewModel.Name,
                    Age = createdAndUpdatedEmployeeViewModel.Age,
                    Address = createdAndUpdatedEmployeeViewModel.Address,
                    Salary = createdAndUpdatedEmployeeViewModel.Salary,
                    IsActive = createdAndUpdatedEmployeeViewModel.IsActive,
                    Email = createdAndUpdatedEmployeeViewModel.Email,
                    PhoneNumber = createdAndUpdatedEmployeeViewModel.PhoneNumber,
                    HiringDate = createdAndUpdatedEmployeeViewModel.HiringDate,
                    EmployeeType = createdAndUpdatedEmployeeViewModel.EmployeeType,
                    Gender = createdAndUpdatedEmployeeViewModel.Gender,
                    DepartmentId = createdAndUpdatedEmployeeViewModel.DepartmentId
                };

                int result = _employeeService.UpdateEmployee(updatedEmployeeDto);
                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee can not be updated");
                    return View(updatedEmployeeDto);
                }
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                    ModelState.AddModelError(string.Empty, $"CreatedEmployee can not be updated because : {ex.Message}");
                else
                   _logger.LogError($"\"CreatedEmployee can not be updated because : {ex}");
              

            }
            return View(createdAndUpdatedEmployeeViewModel);
        }

        //Delete
        public IActionResult Delete(int id)
        {
            if(id == 0)
                return BadRequest();
            try
            {
                bool isDeleted = _employeeService.DeleteEmployee(id);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError(string.Empty, "Can not Delete");
            }
            catch (Exception ex)
            {
                if (_webHostEnvironment.IsDevelopment())
                    _logger.LogError($"Employee can not be deleted because : {ex.Message}");
                else
                {
                    _logger.LogError($"\"Employee can not be deleted because : {ex}");
                    return View("ErrorView");
                }
            }
            return RedirectToAction(nameof(Delete), new { id });
        }

    }
}
