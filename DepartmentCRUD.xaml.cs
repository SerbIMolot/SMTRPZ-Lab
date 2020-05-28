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

namespace SMTRPZ_IT_company
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DepartmentCRUD : Window
    {
        public Department selectedDepartment { get; set; }

        public SQLDepartmentRepository departmentRepository { get; set; }
        public LabContext context { get; set; }

        public DepartmentCRUD()
        {
            context = new LabContext();
            selectedDepartment = new Department();
            departmentRepository = new SQLDepartmentRepository(context);

            InitializeComponent();

            UpdateDataGrid();
        }
        private void UpdateDataGrid()
        {
            DepartmentGrid.ItemsSource = departmentRepository.GetList().ToList();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            selectedDepartment.departmentName = DepartmentNameText.Text;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (departmentRepository.GetByName(selectedDepartment.departmentName) == null)
            {
                    departmentRepository.Create(selectedDepartment);
                    context.SaveChanges();
            }
            UpdateDataGrid();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (departmentRepository.GetById(selectedDepartment.departmentId) != null)
            {
                departmentRepository.Update(selectedDepartment);
                context.SaveChanges();
            }
            UpdateDataGrid();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if( departmentRepository.GetByName( selectedDepartment.departmentName ) != null )
            {
                departmentRepository.Delete(selectedDepartment.departmentId );
                context.SaveChanges();
            }
            UpdateDataGrid();
        }

        private void DepartmentGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            Department rowView = dataGrid.SelectedItem as Department;

            if (rowView != null)
            {
                //DepartamentVM dep = (DataContext).depService.GetByName(rowView.DepartmentName) as DepartamentVM;
                DepartmentNameText.Text = rowView.departmentName;
                selectedDepartment = rowView;
                //mehText
                UpdateBtn.IsEnabled = true;
                DeleteBtn.IsEnabled = true;
            }
            
        }
    }
}
