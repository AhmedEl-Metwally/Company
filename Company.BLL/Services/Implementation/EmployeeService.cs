using AutoMapper;
using Company.BLL.Repositories.Interface;
using Company.DAL.Models.DepartmentModule;
using Company.DAL.Models.EmployeeModule;
using Company.DAL.Repositories.Interface;
using Demo.BusniessLogic.Dtos;

namespace Company.BLL.Repositories.Services
{
    public class EmployeeService(IEmployeeRepositories _employeeRepositories ,IMapper _mapper) : IEmployeeService
    {

        public IEnumerable<EmployeeDto> GetAllEmployee(bool withTracking = false)
        {
            var employee = _employeeRepositories.GetAll(withTracking);
            var employeeDto= _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(employee);
            return employeeDto;
        }

        public EmployeeDetailsDto GetEmployeeById(int id)
        {
            var employee = _employeeRepositories.GetById(id);
            return employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(employee);          
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            return _employeeRepositories.Add(employee); 
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
            => _employeeRepositories.Update(_mapper.Map<UpdatedEmployeeDto,Employee>(employeeDto));

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepositories.GetById(id);
            if (employee is null)
                return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepositories.Update(employee) > 0 ? true : false;
            }
        }
    }
}
