using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace SMTRPZ_IT_company.ModelView
{
    public class DepartamentVM :INotifyPropertyChanged
    {
        private int departmentId { get; set; }
        private string departmentName { get; set; }

        public int DepartmentId
        {
            get { return departmentId;  }
            set
            {
                if (departmentId == value) return;
                departmentId = value;
                OnPropertyChanged("DepartmentId");
            }
        }
        private int numOfEmployees { get; set; }

        public int NumOfEmployees
        {
            get { return numOfEmployees;  }
            set
            {
                if (numOfEmployees == value) return;
                numOfEmployees = value;
                OnPropertyChanged("NumberOfEmployees");
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
