using API.Models;
using System.Collections.Generic;

namespace API.Repository.Interface
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        //ini methode
        Employee Get(string NIK);
        int Insert(Employee employee); // ini seharusnya employee aja jangan Employees
        int Update(Employee employee);
        int Delete(string NIK);
    }
}
