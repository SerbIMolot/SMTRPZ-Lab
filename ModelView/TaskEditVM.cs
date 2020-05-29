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
    class TaskEditVM : INotifyPropertyChanged
    {
        private ObservableCollection<TaskVM> tasks;
        public ObservableCollection<TaskVM> Tasks
        {
            get
            {
                if (tasks == null)
                    tasks = new ObservableCollection<TaskVM>();
                return tasks;
            }
            set
            {
                tasks = value;
                OnPropertyChanged("Tasks");
            }
        }
        private TaskVM selectedTask;
        public TaskVM SelectedTask
        {
            get
            {
                if (selectedTask == null)
                    selectedTask = new TaskVM();
                return selectedTask;
            }
            set
            {
                // обработка изменения свойства
                selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }
        private EmployeeVM passedEmployee;
        public EmployeeVM PassedEmployee
        {
            get
            {
                if (passedEmployee == null)
                    passedEmployee = new EmployeeVM();
                return passedEmployee;
            }
            set
            {
                // обработка изменения свойства
                passedEmployee = value;
                OnPropertyChanged("PassedEmployee");
            }
        }
        public UnitOfWork unit;
        public TaskManagmentService taskService;
        public TaskEditVM()
        {
            unit = UnitOfWork.GetInstance();
            taskService = new TaskManagmentService();

            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Task, TaskVM>();
            }).CreateMapper();

            tasks = new ObservableCollection<TaskVM>(taskService.GetAll());

        }
        private RelayCommand addBtnCommand;
        public RelayCommand AddBtnCommand
        {
            get
            {
                return addBtnCommand ??
                    (addBtnCommand = new RelayCommand(obj =>
                    {
                        var task = obj as TaskVM;
                        if (string.IsNullOrEmpty(task.Task))
                        {
                            return;
                        }
                        task.EmployeeId = PassedEmployee.EmployeeId;
                        taskService.Add(task);
                        Console.WriteLine(task.TaskId);
                        Tasks.Add( taskService.GetAll().SingleOrDefault( t => t.Task == task.Task && t.EmployeeId == task.EmployeeId ) );
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
                        var task = obj as TaskVM;
                        if (string.IsNullOrEmpty(task.Task))
                        {
                            return;
                        }


                        taskService.Update(task);
                        var newTask = tasks.FirstOrDefault(e => e.TaskId == task.TaskId);
                        if (newTask != null)
                        {
                            tasks[tasks.IndexOf(newTask)] = taskService.GetById(newTask.TaskId);
                        }
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
