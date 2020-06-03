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
using SMTRPZ_IT_company.Helpers;

namespace SMTRPZ_IT_company
{

    public partial class DepartmentCRUDControl : UserControl
    {
        public DepartmentCRUDControl()
        {
            InitializeComponent();
        }
        public Task FadeIn()
        {
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
    }
}
