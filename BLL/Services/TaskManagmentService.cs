﻿using System;
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
    class TaskManagmentService : IManagmentService<TaskVM>
    {
        UnitOfWork unitOf;
        IMapper taskVMmap;
        IMapper VMToTaskmap;
        public TaskManagmentService()
        {
            unitOf = UnitOfWork.GetInstance();
            taskVMmap = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmplTask, TaskVM>();
            }).CreateMapper();
            VMToTaskmap = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TaskVM , EmplTask>();
            }).CreateMapper();
        }
        public void Add(TaskVM taskVM)
        {
            EmplTask task = new EmplTask()
            {
                employeeId = taskVM.EmployeeId,
                task = taskVM.Task,
                deadLine = taskVM.DeadLine.Date
            };

            unitOf.taskRepository.Create( task );
            unitOf.Save();
        }
        public TaskVM GetById(int? id)
        {
            if (id == null)
                throw new Exception("Id of task not set");

            var task = unitOf.taskRepository.GetById(id.Value);

            if (task == null)
                throw new Exception("task not found");



            var taskVM = AutomapHelper.MergeInto<TaskVM>(taskVMmap, task );


            return taskVM;
        }
        public IEnumerable<TaskVM> GetAll()
        {
            ObservableCollection<TaskVM> res = new ObservableCollection<TaskVM>();
            var taskList = unitOf.taskRepository.GetList();


            foreach (var task in taskList)
            {
                res.Add(AutomapHelper.MergeInto<TaskVM>(taskVMmap, task));
            }
            return res;
        }
        public void Update(TaskVM taskVM)
        {
            var task = unitOf.taskRepository.GetByIdDetached(taskVM.TaskId);

            if (task == null)
            {
                throw new Exception("Task not found id DB");
            }

            task = AutomapHelper.MergeInto<EmplTask>(VMToTaskmap, taskVM);

            unitOf.taskRepository.Update(task);

            unitOf.Save();

        }
        public void Dispose()
        {
            unitOf.taskRepository.Dispose(true);
        }
    }
}
