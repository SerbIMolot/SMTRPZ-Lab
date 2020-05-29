using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SMTRPZ_IT_company.ModelView
{
    public class EmployeeVM : INotifyPropertyChanged
    {
        private int employeeId { get; set; }

        private string firstName { get; set; }
        private string lastName { get; set; }

        private List<Task> tasks { get; set; }

        private string departmentName { get; set; }


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
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName == value) return;
                firstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName == value) return;
                lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public List<Task> Tasks
        {
            get { return tasks; }
            set
            {
                if (tasks == value) return;
                tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        public string DepartmentName
        {
            get { return departmentName; }
            set
            {
                if (departmentName == value) return;
                departmentName = value;
                OnPropertyChanged("DepartmentName");
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
