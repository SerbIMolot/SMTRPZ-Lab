using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SMTRPZ_IT_company.ModelView
{
    class TaskVM : INotifyPropertyChanged
    {
        private int taskId { get; set; }

        private string task { get; set; }

        private int employeeId { get; set; }

        private DateTime deadLine { get; set; }
        public DateTime DeadLine
        {
            get { return deadLine; }
            set
            {
                if (deadLine == value) return;
                deadLine = value;
                OnPropertyChanged("TaskId");
            }
        }

        public int TaskId
        {
            get { return taskId; }
            set
            {
                if (taskId == value) return;
                taskId = value;
                OnPropertyChanged("TaskId");
            }
        }
        public string Task
        {
            get { return task; }
            set
            {
                if (task == value) return;
                task = value;
                OnPropertyChanged("Task");
            }
        }
        public int EmployeeId
        {
            get { return employeeId; }
            set
            {
                if (employeeId == value) return;
                employeeId = value;
                OnPropertyChanged("EmployeeId");
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
