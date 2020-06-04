using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMTRPZ_IT_company.Repository;
using SMTRPZ_IT_company.Model;
using SMTRPZ_IT_company.BLL.Services;
using System.Collections.ObjectModel;
using AutoMapper;
using System.Runtime.CompilerServices;
using System.ComponentModel;
namespace SMTRPZ_IT_company.ModelView
{
    public class DepartmentCRUDVM : INotifyPropertyChanged
    {
        private ObservableCollection<DepartmentVM> departments;
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
                departments = value;
                OnPropertyChanged("Departments");
            }
        }
        private DepartmentVM selectedDepartment;
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
                selectedDepartment = value;
                OnPropertyChanged("SelectedDepartment");
            }
        }

        public DepartmentManagmentService depService;

        public DepartmentCRUDVM()
        {
            depService = new DepartmentManagmentService();

            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Department, DepartmentVM>();
            }).CreateMapper();

            departments = new ObservableCollection<DepartmentVM>(depService.GetAll());

        }
        private RelayCommand addBtnCommand;
        public RelayCommand AddBtnCommand
        {
            get
            {
                return addBtnCommand ??
                    (addBtnCommand = new RelayCommand(obj =>
                    {
                        var dep = obj as DepartmentVM;
                        if (string.IsNullOrEmpty(dep.DepartmentName))
                        {
                            return;
                        }
                        Departments.Add(dep);
                        depService.Add(dep);
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
                        var dep = obj as DepartmentVM;
                        if (string.IsNullOrEmpty(dep.DepartmentName))
                        {
                            return;
                        }

                        Departments.Remove(dep);
                        depService.Delete(dep);
                    }));
            }
        }

        private RelayCommand updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return updateCommand ??
                    (updateCommand = new RelayCommand(obj =>
                    {
                        var dep = obj as DepartmentVM;

                        if (string.IsNullOrEmpty(dep.DepartmentName))
                        {
                            return;
                        }
                        depService.Update(dep);

                        var newDep = depService.GetAll().FirstOrDefault(e => e.DepartmentId == dep.DepartmentId);
                        if (newDep != null)
                        {
                            Departments[departments.IndexOf(dep)] = newDep;
                        }
                    }));
            }
        }
        private RelayCommand loadedCommand;
        public RelayCommand LoadedCommand
        {
            get
            {
                return loadedCommand ??
                    (loadedCommand = new RelayCommand(obj =>
                    {
                        Departments = new ObservableCollection<DepartmentVM>(depService.GetAll());
                    }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
