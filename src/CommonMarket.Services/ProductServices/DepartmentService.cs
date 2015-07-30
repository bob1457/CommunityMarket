using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonMarket.Core.Entities;
using CommonMarket.Core.Interface;
using CommonMarket.DataAccess;

namespace CommonMarket.Services.ProductServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IUnitOfWork _uow;

        public DepartmentService(IRepository<Department> departmentRepository, IUnitOfWork uow)
        {
            _departmentRepository = departmentRepository;
            _uow = uow;
        }

        #region Service Operations

        public IEnumerable<Department> FindAllDepartments()
        {
            return _departmentRepository.GetAll();
        }

        public Department FindDepartmentById(int id)
        {
            return _departmentRepository.GetAll().FirstOrDefault(c => c.Id == id);
        }

        public void AddNewDepartment(Department productDeparment)
        {
            try
            {
                _departmentRepository.Add(productDeparment);
                //_uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed adding the department", ex);
            }

        }

        public void UpdateDepartment(Department department)
        {
            try
            {
                //ProductCategory category = _categoryRepository.GetAll().FirstOrDefault(i => i.Id == id);
                //category.DepartmentId = 1;

                _departmentRepository.Update(department);
                _uow.Save();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed updating the department", ex);
            }
        }

        public void DeleteCategory(int id)
        {

        }




        #endregion
    }
}
