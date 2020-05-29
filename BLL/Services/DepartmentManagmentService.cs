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
    public class DepartmentManagmentService : IManagmentService<DepartamentVM>
    {
    
       UnitOfWork unitOf;
       IMapper departmentVMmap;
       IMapper vmDepartment;

       public DepartmentManagmentService()
       {
           unitOf = UnitOfWork.GetInstance();
           departmentVMmap = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<Department, DepartamentVM>();
           }).CreateMapper();
            vmDepartment = new MapperConfiguration(cfg =>
           {
               cfg.CreateMap<DepartamentVM, Department>();
           }).CreateMapper();
       }
       public void Add(DepartamentVM departmentVM )
       {
           Department department = new Department();


           if (departmentVM == null)
           {
           }
           else
           {
                department = AutomapHelper.MergeInto<Department>( vmDepartment, departmentVM );

           }

           unitOf.departmentRepository.Create(department);
           unitOf.Save();
       }
        public void Update(DepartamentVM departamentVM)
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
        public void Delete(DepartamentVM departamentVM)
        {
            var dep = unitOf.departmentRepository.GetById(departamentVM.DepartmentId);

            if (dep == null)
            {
                throw new Exception("Employee not found id DB");
            }

            var depEmpl = unitOf.departmentEmployeeRepository.GetByDepartment(dep);

            if( depEmpl != null && depEmpl.Count != 0 )
            {
                foreach( var de in depEmpl )
                {
                    unitOf.departmentEmployeeRepository.Delete(de);
                }
            }


            unitOf.departmentRepository.Delete(dep.departmentId);

            unitOf.Save();


        }
        public DepartamentVM GetById(int? id)
       {
           if (id == null)
               throw new Exception("Id of employee not set");

           var department = unitOf.departmentRepository.GetById(id.Value);
           DepartamentVM departamentVM = new DepartamentVM();

           if (department != null) 
            { 

               departamentVM = AutomapHelper.MergeInto<DepartamentVM>(departmentVMmap, department );
           }
           else
            {
                return null;
            }

           return departamentVM;
       }
       public DepartamentVM GetByName(string name)
       {
           if ( string.IsNullOrEmpty(name) )
               throw new Exception("Name of employee not set");

           var department = unitOf.departmentRepository.GetByName(name);
           DepartamentVM departamentVM = new DepartamentVM();

           if (department != null) {
                int numOfEmpl = unitOf.departmentEmployeeRepository.EmployeesInDepartment(department);
                if (department.numOfEmployees != numOfEmpl)
                {
                    department.numOfEmployees = numOfEmpl;
                    unitOf.departmentRepository.Update(department);
                }

                //throw new Exception("Employee not found");
                departamentVM = AutomapHelper.MergeInto<DepartamentVM>(departmentVMmap, department );
           }
           else
            {
                return null;
            }

           return departamentVM;
       }
       public IEnumerable<DepartamentVM> GetAll()
       {
           ObservableCollection<DepartamentVM> res = new ObservableCollection<DepartamentVM>();//new IEnumerable<EmployeeVM>();
          
           var depList = unitOf.departmentRepository.GetList();

           foreach (var dep in depList)
           {

               if (dep != null)
               {
                    int numOfEmpl = unitOf.departmentEmployeeRepository.EmployeesInDepartment(dep);
                    if ( dep.numOfEmployees != numOfEmpl )
                    {
                        dep.numOfEmployees = numOfEmpl;
                        unitOf.departmentRepository.Update(dep);
                    }

                    res.Add(AutomapHelper.MergeInto<DepartamentVM>(departmentVMmap, dep ) );
               }
               else
               {
                   throw new Exception("null department");
               }
           }
           return res;
       }
       public void Dispose()
       {
            unitOf.departmentRepository.Dispose(true);
       }
    }
}
