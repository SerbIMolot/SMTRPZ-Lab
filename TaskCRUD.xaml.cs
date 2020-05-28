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
    /// Interaction logic for TaskCRUD.xaml
    /// </summary>
    public partial class TaskCRUD : Window
    {
        public EmplTask selectedTask { get; set; }
        public SQLEmployeeRepository EmployeeRepository { get; set; }
        public SQLTaskRepository TaskRepository { get; set; }
        public LabContext context { get; set; }

        public TaskCRUD()
        {

            context = new LabContext();
            selectedTask = new EmplTask();
            TaskRepository = new SQLTaskRepository(context);
            EmployeeRepository = new SQLEmployeeRepository(context);

            InitializeComponent();

            EmployeeName.ItemsSource = EmployeeRepository.GetList().ToList();

            UpdateDataGrid();
        }
        private void UpdateDataGrid()
        {
            TaskGrid.ItemsSource = TaskRepository.GetList().ToList();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            selectedTask.task = TaskText.Text;
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

            TaskRepository.Create(selectedTask);
            context.SaveChanges();

            UpdateDataGrid();
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (TaskRepository.GetById(selectedTask.taskId) != null)
            {
                TaskRepository.Update(selectedTask);
                context.SaveChanges();
            }
            UpdateDataGrid();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (TaskRepository.GetById(selectedTask.taskId) != null)
            {
                TaskRepository.Delete(selectedTask.taskId);
                context.SaveChanges();
            }
            UpdateDataGrid();
        }

        private void TaskText_TextChanged(object sender, TextChangedEventArgs e)
        {
            selectedTask.task = TaskText.Text;
        }

        private void EmployeeName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            Employee empl = cmb.SelectedItem as Employee;
            selectedTask.employeeId = empl.employeeId;
        }

        private void TaskGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            EmplTask rowView = dataGrid.SelectedItem as EmplTask;

            if (rowView != null)
            {

                TaskText.Text = rowView.task;
                selectedTask = rowView;

                UpdateBtn.IsEnabled = true;
                DeleteBtn.IsEnabled = true;
            }
        }

        private void DeadLinePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            selectedTask.deadLine = DeadLinePick.SelectedDate.Value.Date;
        }
    }
}
