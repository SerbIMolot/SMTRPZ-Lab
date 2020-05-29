using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using SMTRPZ_IT_company.Model;

namespace SMTRPZ_IT_company.ModelView
{
    class DepartamentEmployeeVM : INotifyPropertyChanged
    {
        private int employeeId { get; set; }
        private int departmentId { get; set; }
        private EmployeeVM employee { get; set; }
        private DepartamentVM department { get; set; }

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
        public int DepartmentId
        {
            get { return departmentId; }
            set
            {
                if (departmentId == value) return;
                departmentId = value;
                OnPropertyChanged("DepartmentId");
            }
        }
        public EmployeeVM Employee
        {
            get { return employee; }
            set
            {
                if (employee == value) return;
                employee = value;
                OnPropertyChanged("Employee");
            }
        }
        public DepartamentVM Department
        {
            get { return department; }
            set
            {
                if (department == value) return;
                department = value;
                OnPropertyChanged("Department");
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
