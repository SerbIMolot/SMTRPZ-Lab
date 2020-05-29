using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTRPZ_IT_company.ModelView;
using SMTRPZ_IT_company.Model;
using AutoMapper;
using System.Collections.ObjectModel;
using System.Data.Entity;

namespace SMTRPZ_IT_company.BLL.Services
{
    public class EmployeeManagmentService : IManagmentService<EmployeeVM>
    {
        UnitOfWork unitOf;
        IMapper employeeToVMmap;
        IMapper VMtoEmployeemap;
        IMapper ToDepEmpl;

       public  EmployeeManagmentService()
        {
            unitOf = UnitOfWork.GetInstance();
            employeeToVMmap = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeVM>();
                cfg.CreateMap<Department, EmployeeVM>();
            }).CreateMapper();
            VMtoEmployeemap = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeVM, Employee>();
            }).CreateMapper();
            ToDepEmpl = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, DepartmentEmployee>();
                cfg.CreateMap<Department, DepartmentEmployee>();
            }).CreateMapper();
        }
        public void Delete(EmployeeVM employeeVM)
        {
            var employee = unitOf.employeeRepository.GetById(employeeVM.EmployeeId);

            if (employee == null)
            {
                throw new Exception("Employee not found id DB");
            }

            var depEmpl = unitOf.departmentEmployeeRepository.GetByEmployee(employee);



            if( depEmpl != null  )
            {
                unitOf.departmentEmployeeRepository.Delete( depEmpl );
            }

            unitOf.employeeRepository.Delete(employee.employeeId);

            unitOf.Save();


        }
        public void Update(EmployeeVM employeeVM)
        {
            var employee = unitOf.employeeRepository.GetByIdDetached(employeeVM.EmployeeId);

            if (employee == null)
            {
                throw new Exception("Employee not found id DB");
            }

            var depEmpl = unitOf.departmentEmployeeRepository.GetByEmployee(employee);
            var newDep = unitOf.departmentRepository.GetByName(employeeVM.DepartmentName);
            
            employee = AutomapHelper.MergeInto<Employee>( VMtoEmployeemap, employeeVM );

            unitOf.employeeRepository.Update( employee );


            if( depEmpl != null && newDep != null && depEmpl.departmentId != newDep.departmentId )
            {
                unitOf.departmentEmployeeRepository.Delete( depEmpl );

                DepartmentEmployee departmentEmployee = AutomapHelper.MergeInto<DepartmentEmployee>(ToDepEmpl, employee, newDep);
                departmentEmployee.department = newDep;
                departmentEmployee.employee = employee;
                unitOf.departmentEmployeeRepository.Create(departmentEmployee);

            }
            else if(depEmpl == null)
            {

                DepartmentEmployee departmentEmployee = AutomapHelper.MergeInto<DepartmentEmployee>(ToDepEmpl, employee, newDep);
                departmentEmployee.department = newDep;
                departmentEmployee.employee = employee;
                unitOf.departmentEmployeeRepository.Create(departmentEmployee);
            }


            unitOf.Save();


        }
        public void Add(EmployeeVM employeeVM)
        {
            Employee employee = new Employee()
            {
                tasks = new List<Model.EmplTask>()
            };

            
            if (string.IsNullOrEmpty( employeeVM.DepartmentName ))
            {
                //throw new Exception("Department with that name not found");
            }
            else
            {

                

                Department dep = unitOf.departmentRepository.GetByName( employeeVM.DepartmentName );
                
                if ( dep != null )
                {
                    
                    DepartmentEmployee departmentEmployee = new DepartmentEmployee()
                    {
                        employee = employee,
                        employeeId = employee.employeeId,
                        department = dep,
                        departmentId = dep.departmentId
                    };

                    unitOf.departmentEmployeeRepository.Create(departmentEmployee);
                }
            }

            employee.firstName = employeeVM.FirstName;
            employee.lastName = employeeVM.LastName;

            unitOf.employeeRepository.Create(employee);
            unitOf.Save();
        }
        public EmployeeVM GetById(int? id)
        {
            if (id == null)
                throw new Exception("Id of employee not set");

            var employee = unitOf.employeeRepository.GetById(id.Value);
            EmployeeVM employeeVM;

            if (employee == null)
                throw new Exception("Employee not found");

            var departmentEmpl = unitOf.departmentEmployeeRepository.GetByEmployee( employee );

            if (departmentEmpl == null)
            {
                Department departament = new Department()
                {
                    departmentId = -1,
                    departmentName = "None"
                };

                employeeVM = AutomapHelper.MergeInto<EmployeeVM>(employeeToVMmap, employee, departament);
            }
            else
            {

                employeeVM = AutomapHelper.MergeInto<EmployeeVM>(employeeToVMmap, employee, departmentEmpl.department);
            }

            return employeeVM;
        }
        public IEnumerable<EmployeeVM> GetAll()
        {
            ObservableCollection<EmployeeVM> res = new ObservableCollection<EmployeeVM>();
            var emplList = unitOf.employeeRepository.GetList();
            var depEmplList = unitOf.departmentEmployeeRepository.GetList();

            foreach ( var emp in emplList )
            {
                var tempDE = depEmplList.FirstOrDefault( de => de.employeeId == emp.employeeId );
                
                if( tempDE == null )
                {
                    res.Add(AutomapHelper.MergeInto<EmployeeVM>(employeeToVMmap, emp,
                        new Department
                        {
                            departmentId = -1,
                            departmentName = "None"
                        }
                        ));
                }
                else
                {
                    var dep = unitOf.departmentRepository.GetById(tempDE.departmentId);
                    res.Add( AutomapHelper.MergeInto<EmployeeVM>(employeeToVMmap, emp, dep) );
                }
            }
            return res;
        }
        public void Dispose()
        {
            unitOf.employeeRepository.Dispose(true);
            unitOf.departmentEmployeeRepository.Dispose( true );
        }
    }
}
