using AutoMapper;
using Company.BLL.Dtos.EmployeeDtos;
using Company.BLL.Services.Interface;
using Company.DAL.Models.EmployeeModule;
using Company.DAL.Repositories.Interface;
using Company.DAL.Repositories.UnitOfWorks;

namespace Company.BLL.Services.Implementation
{
    public class EmployeeService(IUnitOfWork _unitOfWork ,IMapper _mapper) : IEmployeeService
    {

        public IEnumerable<EmployeeDto> GetAllEmployee(string EmployeeSearchName, bool withTracking = false)
        {
            IEnumerable<Employee> employee;

            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                 employee = _unitOfWork.EmployeeRepositories.GetAll(withTracking);
            else
                 employee = _unitOfWork.EmployeeRepositories.GetAll(E => E.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            
            var employeeDto= _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto>>(employee);
            return employeeDto;
        }

        public EmployeeDetailsDto GetEmployeeById(int id)
        {
            var employee = _unitOfWork.EmployeeRepositories.GetById(id);
            return employee is null ? null : _mapper.Map<Employee,EmployeeDetailsDto>(employee);          
        }

        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<CreatedEmployeeDto, Employee>(employeeDto);
            _unitOfWork.EmployeeRepositories.Add(employee);
            return _unitOfWork.SaveChanges();
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            _unitOfWork.EmployeeRepositories.Update(_mapper.Map<UpdatedEmployeeDto,Employee>(employeeDto));
            return _unitOfWork.SaveChanges();
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _unitOfWork.EmployeeRepositories.GetById(id);
            if (employee is null)
                return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepositories.Update(employee);
                return _unitOfWork.SaveChanges() > 0;
            }
        }
    }
}
