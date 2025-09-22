using Demo.BusniessLogic.Dtos;

namespace Company.BLL.Repositories.Interface
{
    public interface IEmployeeService
    {
        //GetAll
        IEnumerable<EmployeeDto> GetAllEmployee(bool withTracking = false);
        //GetById
        EmployeeDetailsDto GetEmployeeById(int id);
        //Create
        int CreateEmployee(CreatedEmployeeDto employeeDto);
        //Update
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
        //Delete
        bool DeleteEmployee(int id);
    }
}
