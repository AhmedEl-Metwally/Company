using Company.BLL.Dtos;
using Company.BLL.Repositories.Interface;
using Company.BLL.Repositories.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class DepartmentController
                                    (
                                    IDepartmentService _departmentService,
                                    IWebHostEnvironment _webHostEnvironment,
                                    ILogger<DepartmentController> _logger
                                    ) : Controller

    {
        //Index
        [HttpGet]
        public IActionResult Index()
        {
            var department = _departmentService.GetAllDepartments(); 
            return View(department);
        }


        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto createDepartmentDto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    int result = _departmentService.AddDepartment(createDepartmentDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Depatment can not created");
                    }
                }
                catch (Exception ex) 
                {
                    //Development ==> action , log error in console , view
                    if(_webHostEnvironment.IsDevelopment())
                    {
                        _logger.LogError($"Department can not be create because : {ex.Message}");
                    }
                    else
                    {
                        _logger.LogError($"\"Department can not be create because : {ex}");
                        return View("ErrorView");
                    }
                }
               
            }
             return View(createDepartmentDto);
        }
        
        

     



    }
}
