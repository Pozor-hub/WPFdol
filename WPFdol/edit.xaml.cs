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

namespace WPFdol
{
    /// <summary>
    /// Логика взаимодействия для edit.xaml
    /// </summary>
    public partial class edit : Page
    {
        public edit()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SpecialtyPage());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GroupPage());
        }

        private void createStudent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new StudentPage());
        }

        private void editSpeciality_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void editGroup_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GroupEditPage());
        }
    }
}
