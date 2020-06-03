using SMTRPZ_IT_company.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace SMTRPZ_IT_company.ModelView
{
    public class MainWindowViewModel : ViewModelBase
    {


        private ICommand _gotoView1Command;
        private ICommand _gotoView2Command;
        private ICommand _gotoTaskEditCommand;
        private UserControl _currentView;
        private DepartmentCRUDControl DepartmentUC;
        private EmployeeCRUD EmployeeVC;

        public MainWindowViewModel()
        {
            DepartmentUC = new DepartmentCRUDControl();
            EmployeeVC = new EmployeeCRUD();
            ((EmployeeCRUDVM)EmployeeVC.EmployeeGrid.DataContext).switchToEditTask += new EventHandler<switchTaskViewEventArgs>(SwitchToEditTask);

            CurrentView = DepartmentUC;
        }
        void SwitchToEditTask( object sender, EventArgs e )
        {
            GotoTaskEdit();
        }

        public object GotoView1Command
        {
            get
            {
                return _gotoView1Command ?? (_gotoView1Command = new RelayCommand(
                   x =>
                   {
                       GotoView1();
                   }));
            }
        }

        public ICommand GotoView2Command
        {
            get
            {
                return _gotoView2Command ?? (_gotoView2Command = new RelayCommand(
                   x =>
                   {
                       GotoView2();
                   }));
            }
        }
        public ICommand GotoTaskEditCommand
        {
            get
            {
                return _gotoTaskEditCommand ?? (_gotoTaskEditCommand = new RelayCommand(
                   x =>
                   {
                       GotoTaskEdit();
                   }));
            }
        }

        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }

        private async void GotoView1()
        {
            if (CurrentView == EmployeeVC)
            {
                await ((EmployeeCRUD)CurrentView).FadeOut();
            }
            CurrentView = DepartmentUC;

            await ((DepartmentCRUDControl)CurrentView).FadeIn();
        }

        private async void GotoView2()
        {
            if (CurrentView == DepartmentUC)
            {
                await((DepartmentCRUDControl)CurrentView).FadeOut();
            }
            CurrentView = EmployeeVC;
            await ((EmployeeCRUD)CurrentView).FadeIn();
        }
        private void GotoTaskEdit()
        {
            var view2VM = EmployeeVC.EmployeeGrid.DataContext as EmployeeCRUDVM;
            CurrentView = new TaskCRUD( view2VM.SelectedEmployee );
        }

    }
}
