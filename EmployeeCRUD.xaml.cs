using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SMTRPZ_IT_company.Model;
using SMTRPZ_IT_company.Repository;
using SMTRPZ_IT_company.ModelView;

namespace SMTRPZ_IT_company
{
    public partial class EmployeeCRUD : UserControl
    {
        IRepository<Employee> employees { get; set; }
        IRepository<DepartmentEmployee> depEmployees { get; set; }
        SQLDepartmentRepository departments { get; set; }

        public EmployeeVM selectedEmployee { get; set; }


        public EmployeeCRUD()
        {
            InitializeComponent();

            DataContext = new EmployeeCRUDVM();

        }

        public EmployeeVM getSelectedEmployee()
        {
            return selectedEmployee;
        }
        private void EmployeeGridSelectedEvent(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            EmployeeVM rowView = dataGrid.SelectedItem as EmployeeVM;

            if (rowView != null)
            {

                firstNameText.Text = rowView.FirstName;
                lastNameText.Text = rowView.LastName;

                departmentBox.Text = rowView.DepartmentName;

                updateBtn.IsEnabled = true;
                deleteBtn.IsEnabled = true;
                editTaskBtn.IsEnabled = true;
            }
        }

    }
}
