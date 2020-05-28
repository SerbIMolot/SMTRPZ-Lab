using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    /// Interaction logic for TaskCRUD.xaml
    /// </summary>
    public partial class DepartmentEmployeeCRUD : Window
    {
        public DepartmentEmployee selectedDepEmpl { get; set; }
        public SQLEmployeeRepository EmployeeRepository { get; set; }
        public SQLDepartmentRepository DepartmentRepository { get; set; }
        public SQLDepartmentEmployeeRepository DepartmentEmployeeRepository { get; set; }

        public LabContext context { get; set; }

        public DepartmentEmployeeCRUD()
        {

            context = new LabContext();
            selectedDepEmpl = new DepartmentEmployee();
            EmployeeRepository = new SQLEmployeeRepository(context);
            DepartmentRepository = new SQLDepartmentRepository(context);
            DepartmentEmployeeRepository = new SQLDepartmentEmployeeRepository(context);

            InitializeComponent();

            EmployeeName.ItemsSource = EmployeeRepository.GetList().ToList();
            DepartmentName.ItemsSource = DepartmentRepository.GetList().ToList();

            UpdateDataGrid();
        }
        private void UpdateDataGrid()
        {
            DepEmplGrid.ItemsSource = DepartmentEmployeeRepository.GetList().ToList();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

            DepartmentEmployeeRepository.Create(selectedDepEmpl);
            context.SaveChanges();

            UpdateDataGrid();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if ( DepartmentEmployeeRepository.GetByEmployee(selectedDepEmpl.employee) != null )
            {
                DepartmentEmployeeRepository.Update(selectedDepEmpl);
                context.SaveChanges();
            }
            UpdateDataGrid();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DepartmentEmployeeRepository.GetByEmployee( selectedDepEmpl.employee ) != null )
            {
                DepartmentEmployeeRepository.Delete(selectedDepEmpl);
                context.SaveChanges();
            }
            UpdateDataGrid();
        }

        private void EmployeeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            Employee empl = cmb.SelectedItem as Employee;

            selectedDepEmpl.employeeId = empl.employeeId;
            selectedDepEmpl.employee = empl;
        }
        private void DepartmentName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            Department dep = cmb.SelectedItem as Department;

            selectedDepEmpl.departmentId = dep.departmentId;
            selectedDepEmpl.department = dep;
        }
        
        private void TaskGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DepartmentEmployee rowView = dataGrid.SelectedItem as DepartmentEmployee;

            if (rowView != null)
            {
                EmployeeName.SelectedItem = rowView.employee;
                DepartmentName.SelectedItem = rowView.department;

                UpdateBtn.IsEnabled = true;
                DeleteBtn.IsEnabled = true;
            }
        }

    }
}
