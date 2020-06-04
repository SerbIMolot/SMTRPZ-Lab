using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTRPZ_IT_company.ModelView;
using SMTRPZ_IT_company.Model;
using AutoMapper;
using System.Collections.ObjectModel;

namespace SMTRPZ_IT_company.BLL.Services
{
    public class DepartmentManagmentService : IManagmentService<DepartmentVM>
    {

        IMapper departmentVMmap;
        IMapper vmDepartment;

        public DepartmentManagmentService()
        {
            departmentVMmap = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Department, DepartmentVM>();
            }).CreateMapper();
            vmDepartment = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DepartmentVM, Department>();
            }).CreateMapper();
        }
        public void Add(DepartmentVM departmentVM)
        {
            Department department = new Department();


            if (departmentVM == null)
            {
            }
            else
            {
                department = AutomapHelper.MergeInto<Department>(vmDepartment, departmentVM);

            }
            using (var unitOf = new UnitOfWork())
            {
                unitOf.departmentRepository.Create(department);
                unitOf.Save();
            }
        }
        public void Update(DepartmentVM departamentVM)
        {
            using (var unitOf = new UnitOfWork())
            {
                var department = unitOf.departmentRepository.GetByIdDetached(departamentVM.DepartmentId);

                if (department == null)
                {
                    throw new Exception("Department not found id DB");
                }

                var newDep = AutomapHelper.MergeInto<Department>(vmDepartment, departamentVM);

                unitOf.departmentRepository.Update(newDep);

                unitOf.Save();
            }

        }
        public void Delete(DepartmentVM departamentVM)
        {
            using (var unitOf = new UnitOfWork())
            {
                var dep = unitOf.departmentRepository.GetById(departamentVM.DepartmentId);

                if (dep == null)
                {
                    throw new Exception("Employee not found id DB");
                }

                var depEmplList = unitOf.departmentEmployeeRepository.GetByDepartment(dep);

                if (depEmplList != null && depEmplList.Count > 0)
                {
                    unitOf.departmentEmployeeRepository.DeleteRange(depEmplList);
                }


                unitOf.departmentRepository.Delete(dep.departmentId);

                unitOf.Save();

            }
        }
        public DepartmentVM GetById(int? id)
        {
            if (id == null)
                throw new Exception("Id of employee not set");
            using (var unitOf = new UnitOfWork())
            {
                var department = unitOf.departmentRepository.GetById(id.Value);
                DepartmentVM departamentVM = new DepartmentVM();

                if (department != null)
                {

                    departamentVM = AutomapHelper.MergeInto<DepartmentVM>(departmentVMmap, department);
                }
                else
                {
                    return null;
                }

                return departamentVM;
            }
        }
        public DepartmentVM GetByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Name of employee not set");

            using (var unitOf = new UnitOfWork())
            {
                var department = unitOf.departmentRepository.GetByName(name);
                DepartmentVM departamentVM = new DepartmentVM();

                if (department != null)
                {
                    int numOfEmpl = unitOf.departmentEmployeeRepository.EmployeesInDepartment(department);
                    if (department.numOfEmployees != numOfEmpl)
                    {
                        department.numOfEmployees = numOfEmpl;
                        unitOf.departmentRepository.Update(department);
                    }

                    departamentVM = AutomapHelper.MergeInto<DepartmentVM>(departmentVMmap, department);
                }
                else
                {
                    return null;
                }

                return departamentVM;
            }
        }
        public IEnumerable<DepartmentVM> GetAll()
        {
            ObservableCollection<DepartmentVM> res = new ObservableCollection<DepartmentVM>();
            using (var unitOf = new UnitOfWork())
            {
                var depList = unitOf.departmentRepository.GetList();

                foreach (var dep in depList)
                {

                    if (dep != null)
                    {
                        int numOfEmpl = unitOf.departmentEmployeeRepository.EmployeesInDepartment(dep);
                        if (dep.numOfEmployees != numOfEmpl)
                        {
                            dep.numOfEmployees = numOfEmpl;
                            unitOf.departmentRepository.Update(dep);
                        }

                        res.Add(AutomapHelper.MergeInto<DepartmentVM>(departmentVMmap, dep));
                    }
                    else
                    {
                        throw new Exception("null department");
                    }
                }
                return res;
            }
        }
        public void Dispose()
        {
            using (var unitOf = new UnitOfWork())
            {
                unitOf.departmentRepository.Dispose(true);
            }
        }
    }
}
