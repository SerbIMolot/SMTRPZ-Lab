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
using System.Windows.Shapes;
using SMTRPZ_IT_company.Model;
using SMTRPZ_IT_company.Repository;

namespace SMTRPZ_IT_company
{
    /// <summary>
    /// Interaction logic for EmployeeCRUD.xaml
    /// </summary>
    public partial class EmployeeCRUD : Window
    {
        public Employee selectedEmployee { get; set; }

        public SQLEmployeeRepository EmployeeRepository { get; set; }
        public LabContext context { get; set; }

        public EmployeeCRUD()
        {
            context = new LabContext();
            selectedEmployee = new Employee();
            EmployeeRepository = new SQLEmployeeRepository( context );

            InitializeComponent();

            UpdateDataGrid();
        }
        private void UpdateDataGrid()
        {
            EmployeeGrid.ItemsSource = EmployeeRepository.GetList().ToList();
        }

        private void FirstNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            selectedEmployee.firstName = EmployeeFirstNameText.Text;
        }
        private void LastNameText_TextChanged(object sender, TextChangedEventArgs e)
        {
            selectedEmployee.lastName = EmployeeLastNameText.Text;
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {

            EmployeeRepository.Create(selectedEmployee);
            context.SaveChanges();

            UpdateDataGrid();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeRepository.GetById(selectedEmployee.employeeId) != null)
            {
                EmployeeRepository.Update(selectedEmployee);
                context.SaveChanges();
            }
            UpdateDataGrid();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeRepository.GetById(selectedEmployee.employeeId) != null)
            {
                EmployeeRepository.Delete(selectedEmployee.employeeId);
                context.SaveChanges();
            }
            UpdateDataGrid();
        }

        private void EmployeeGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            Employee rowView = dataGrid.SelectedItem as Employee;

            if (rowView != null)
            {
                //DepartamentVM dep = (DataContext).depService.GetByName(rowView.EmployeeName) as DepartamentVM;
                EmployeeFirstNameText.Text = rowView.firstName;
                EmployeeLastNameText.Text = rowView.lastName;

                selectedEmployee = rowView;
                //mehText
                UpdateBtn.IsEnabled = true;
                DeleteBtn.IsEnabled = true;
            }

        }
    }
}
