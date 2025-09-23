using Company.BLL.Dtos.DepartmentDtos;
using Company.BLL.Services.Interface;
using Company.PL.ViewModels;
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
        [ValidateAntiForgeryToken]
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
                        ModelState.AddModelError(string.Empty, "Depatment can not created");
                }
                catch (Exception ex) 
                {
                    //Development ==> action , log error in console , view
                    if(_webHostEnvironment.IsDevelopment())
                        _logger.LogError($"Department can not be create because : {ex.Message}");
                    else
                    {
                        _logger.LogError($"\"Department can not be create because : {ex}");
                        return View("ErrorView");
                    }
                }
               
            }
             return View(createDepartmentDto);
        }

        //Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if(!id.HasValue)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if(department is null)
                return BadRequest();

            return View(department);
        }

        //Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if(department is null)
                return BadRequest();

            var departmentViewModels = new EditDepartmentViewModels()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreatedOn = department.CreateOn.HasValue ? department.CreateOn.Value : default
            };

            return View(departmentViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute]int? id ,EditDepartmentViewModels editDepartmentViewModels)
        {
            if(ModelState.IsValid)
            { 
                try
                {
                    if(!id.HasValue) return BadRequest();
                    var updatedDepartmentDto = new UpdatedDepartmentDto()
                    {
                        Id = id.Value,
                        Code = editDepartmentViewModels.Code,
                        Name = editDepartmentViewModels.Name,
                        Description = editDepartmentViewModels.Description,
                        DateOfCreation = editDepartmentViewModels.CreatedOn
                    };
                    int result = _departmentService.UpdatedDepartment(updatedDepartmentDto);
                    if (result > 0)
                        return RedirectToAction(nameof(Index));
                    else
                        ModelState.AddModelError(string.Empty,"can not be updated");
                
                }
                catch (Exception ex)
                {
                    //Development ==> action , log error in console , view
                    if (_webHostEnvironment.IsDevelopment())
                        _logger.LogError($"Department can not be create because : {ex.Message}");
                    else
                    {
                        _logger.LogError($"\"Department can not be create because : {ex}");
                        return View("ErrorView");
                    }
                }
            }
            return View (editDepartmentViewModels);
        }

        //Delete
        //Get ==> result from view
        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if(!id.HasValue)
        //        return BadRequest();
        //    var department = _departmentService.GetDepartmentById(id.Value);
        //    if(department is null)
        //        return NotFound();  

        //    return View(department);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if(id == 0)
                return BadRequest();
            try 
            {
                bool isDeleted = _departmentService.DeleteDepartment(id);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));
                else 
                    ModelState.AddModelError(string.Empty, "Can not Delete"); 
            }
            catch (Exception ex)
            {
                //Development ==> action , log error in console , view
                if (_webHostEnvironment.IsDevelopment())
                    _logger.LogError($"Department can not be deleted because : {ex.Message}");
                else
                {
                    _logger.LogError($"\"Department can not be deleted because : {ex}");
                    return View("ErrorView");
                }
            }
            return RedirectToAction(nameof(Delete), new { id });

        }


    }
}
