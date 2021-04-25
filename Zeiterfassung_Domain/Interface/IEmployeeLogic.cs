using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zeiterfassung_Domain.Interface
{
    public interface IEmployeeLogic : ILogic
    {
        List<Employee> SQLReadEmployees();

        Employee SQLReadEmployeebyID(int id);

        Employee SQLReadEmployeebyEmail(string Email);

        void SQLAddEmployee(Employee employee);

        void SQLUpdateEmployee(Employee employee);
        void SQLRemoveEmployee(int id);
    }
}
