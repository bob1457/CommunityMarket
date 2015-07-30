using System.Collections.Generic;
using CommonMarket.Core.Entities;

namespace CommonMarket.Core.Interface
{
    public interface IDepartmentService
    {
        IEnumerable<Department> FindAllDepartments();
        Department FindDepartmentById(int id);
        void AddNewDepartment(Department productDeparment);
        void UpdateDepartment(Department department);
        void DeleteCategory(int id);
    }
}