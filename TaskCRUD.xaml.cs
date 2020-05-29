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
using SMTRPZ_IT_company.ModelView;

namespace SMTRPZ_IT_company
{
    /// <summary>
    /// Interaction logic for TaskCRUD.xaml
    /// </summary>
    public partial class TaskCRUD : UserControl
    {
        public TaskCRUD( EmployeeVM employee )
        {

            InitializeComponent();
            date.SelectedDate = DateTime.Now;
            ((TaskEditVM)TaskGrid.DataContext).PassedEmployee = employee;
        }
    }
}
