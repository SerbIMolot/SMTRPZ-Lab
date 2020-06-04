using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using SMTRPZ_IT_company.Repository;
using SMTRPZ_IT_company.ModelView;
using SMTRPZ_IT_company.Model;
using SMTRPZ_IT_company.BLL.Services;
using System.Collections.ObjectModel;
using AutoMapper;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Threading;

namespace SMTRPZ_IT_company.ModelView
{
    public class switchTaskViewEventArgs : EventArgs
    {
        public EmployeeVM employee { get; set; }
    }
    public class EmployeeCRUDVM : INotifyPropertyChanged
    {
        public event EventHandler<switchTaskViewEventArgs> switchToEditTask;
        private void SwitchToEditTask( switchTaskViewEventArgs e )
        {
            EventHandler<switchTaskViewEventArgs> handler = switchToEditTask;
            if (handler != null )
            {
                handler( this, e );
            }
        }
        private ObservableCollection<EmployeeVM> employees { get; set; }
        public ObservableCollection<EmployeeVM> Employees
        {
            get
            {
                if (employees == null)
                    employees = new ObservableCollection<EmployeeVM>();
                return employees;
            }
            set
            {
                // обработка изменения свойства
                employees = value;
                OnPropertyChanged("Employees");
            }
        }
        private ObservableCollection<DepartmentVM> departments { get; set; }
        public ObservableCollection<DepartmentVM> Departments
        {
            get
            {
                if (departments == null)
                    departments = new ObservableCollection<DepartmentVM>();
                return departments;
            }
            set
            {
                // обработка изменения свойства
                departments = value;
                OnPropertyChanged("Departments");
            }
        }
        private EmployeeVM selectedEmployee { get; set; }
        public EmployeeVM SelectedEmployee
        {
            get 
            {
                if (selectedEmployee == null)
                    selectedEmployee = new EmployeeVM();
                return selectedEmployee; 
            }
            set
            {
                // обработка изменения свойства
                selectedEmployee = value;
                OnPropertyChanged("SelectedEmployee");
            }
        }
        private DepartmentVM selectedDepartment { get; set; }
        public DepartmentVM SelectedDepartment
        {
            get 
            {
                if (selectedDepartment == null)
                    selectedDepartment = new DepartmentVM();
                return selectedDepartment; 
            }
            set
            {
                // обработка изменения свойства
                selectedDepartment = value;
                OnPropertyChanged("SelectedDepartment");
            }
        }
        public EmployeeManagmentService emplService;
        public DepartmentManagmentService depService;

        public EmployeeCRUDVM()
        {
            emplService = new EmployeeManagmentService();
            depService = new DepartmentManagmentService();
            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeVM>();
                cfg.CreateMap<DepartmentEmployee, EmployeeVM>();
            }).CreateMapper();
            employees = new ObservableCollection<EmployeeVM>( emplService.GetAll() );
            departments = new ObservableCollection<DepartmentVM>( depService.GetAll() );

        }

        private RelayCommand addBtnCommand;
        public RelayCommand AddBtnCommand
        {
            get
            {
                return addBtnCommand ??
                    (addBtnCommand = new RelayCommand(obj =>
                    {
                        var empl = obj as EmployeeVM;
                        empl.DepartmentName = selectedDepartment.DepartmentName;

                        if (string.IsNullOrEmpty(empl.FirstName)
                        || string.IsNullOrEmpty(empl.LastName))
                        {
                            return;
                        }
                        else if (depService.GetById(SelectedDepartment.DepartmentId) == null || SelectedDepartment == null )
                        {
                            ConfirmationWindow win = new ConfirmationWindow("Department with that name doesn't exist, employee would be created without it");
                            win.ShowDialog();
                            if (win.confirm == false)
                            {
                                return;
                            }
                        }
                        emplService.Add(empl);
                        employees.Add(emplService.GetAll().SingleOrDefault( e => e.FirstName == empl.FirstName && e.LastName == empl.LastName ));
                    }));
            }
        }
        private RelayCommand deleteBtnCommand;
        public RelayCommand DeleteBtnCommand
        {
            get
            {
                return deleteBtnCommand ??
                    (deleteBtnCommand = new RelayCommand(obj =>
                    {
                        var empl = obj as EmployeeVM;
                        empl.DepartmentName = selectedDepartment.DepartmentName;

                        ConfirmationWindow win = new ConfirmationWindow("Are you sure you want to delete " + empl.FirstName + " " + empl.LastName + "?");
                        win.ShowDialog();
                        if (win.confirm == false)
                        {
                            return;
                        }
                        
                        emplService.Delete(empl);
                        Employees.Remove(empl);
                    }));
            }
        }
        private RelayCommand taskEditBtnCommand;
        public RelayCommand TaskEditBtnCommand
        {
            get
            {
                return taskEditBtnCommand ??
                    (taskEditBtnCommand = new RelayCommand(obj =>
                    {
                        var evArgs = new switchTaskViewEventArgs();
                        if( SelectedEmployee != null )
                        {
                            evArgs.employee = SelectedEmployee;
                            SwitchToEditTask( evArgs );

                        }
                    }));
            }
        }



        private bool isEmployeeEmpty( EmployeeVM employee )
        {
            if(string.IsNullOrEmpty(employee.FirstName)
                || string.IsNullOrEmpty(employee.LastName)
                || string.IsNullOrEmpty(employee.DepartmentName) )
            {
                return false;
            }
            return true;
        }

        private RelayCommand updateEmployeeCommand;
        public RelayCommand UpdateEmployeeCommand
        {
            get
            {
                return updateEmployeeCommand ??
                    (updateEmployeeCommand = new RelayCommand(obj =>
                    {
                        var empl = obj as EmployeeVM;
                        empl.DepartmentName = SelectedDepartment.DepartmentName;
                        if (string.IsNullOrEmpty(empl.FirstName)
                        || string.IsNullOrEmpty(empl.LastName))
                        {
                            return;
                        }
                        else if (depService.GetByName(SelectedDepartment.DepartmentName) == null || SelectedDepartment == null )
                        {
                            ConfirmationWindow win = new ConfirmationWindow("Department with that name doesn't exist, employee would be created without it");
                            win.ShowDialog();
                            if (win.confirm == false)
                            {
                                return;
                            }
                        }

                        emplService.Update(empl);
                        var newEmpl = employees.FirstOrDefault(e => e == empl);
                        if (newEmpl != null)
                        {
                            employees[employees.IndexOf(newEmpl)] = emplService.GetById(newEmpl.EmployeeId);
                        }
                    })); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
