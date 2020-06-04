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
using System.Windows.Media.Animation;
using SMTRPZ_IT_company.Model;
using SMTRPZ_IT_company.Repository;
using SMTRPZ_IT_company.ModelView;
using SMTRPZ_IT_company.Helpers;
using SMTRPZ_IT_company.BLL.Services;
using System.Collections.ObjectModel;

namespace SMTRPZ_IT_company
{
    public partial class EmployeeCRUD : UserControl
    {

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
        public Task FadeIn()
        {
            using (var depMen = new DepartmentManagmentService())
            {
                ((EmployeeCRUDVM)EmployeeGrid.DataContext).Departments = new ObservableCollection<DepartmentVM>(depMen.GetAll().ToList());
            }

            Visibility = System.Windows.Visibility.Visible;

            var a = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                FillBehavior = FillBehavior.Stop,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            var storyboard = new Storyboard();

            storyboard.Children.Add(a);
            Storyboard.SetTarget(a, this);
            Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
            storyboard.Completed += delegate { Visibility = System.Windows.Visibility.Visible; };

            return StoryboardExtensions.BeginAsync(storyboard);
        }
        public Task FadeOut()
        {
            Visibility = System.Windows.Visibility.Visible;

            var a = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                FillBehavior = FillBehavior.Stop,
                BeginTime = TimeSpan.FromSeconds(0),
                Duration = new Duration(TimeSpan.FromSeconds(0.2))
            };
            var storyboard = new Storyboard();

            storyboard.Children.Add(a);
            Storyboard.SetTarget(a, this);
            Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
            storyboard.Completed += delegate { Visibility = System.Windows.Visibility.Hidden; };

            return StoryboardExtensions.BeginAsync(storyboard);
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
