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
using System.Collections.ObjectModel;
using Npgsql;
using NpgsqlTypes;

namespace WPFdol
{

    public partial class MainWindow : Window
    {
        private NpgsqlConnection connection;
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<string> Positions { get; set; }
        public Employee NewEmployee { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            Positions = new ObservableCollection<string>()
            {
                "Директор", "Менеджер", "Продавец"
            };

            Employees = new ObservableCollection<Employee>()
            {
                new Employee("Коренев", "Сергей", "Владимирович", Positions[2])
            };

            Binding binding = new Binding();
            binding.Source = Positions;

            NewEmployee = new Employee("", "", "", Positions[0]);

            PositionList.SetBinding(ComboBox.ItemsSourceProperty, binding);
            CreatePositionList.SetBinding(ComboBox.ItemsSourceProperty, binding);

            Connect("localhost", "5432", "Denis", "1234", "Workers");

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EnableControl(true);

            ButtonCreateEmployee.Content = "Зафиксировать изменения";
            ButtonCreateEmployee.Click -= Button_Click;
            ButtonCreateEmployee.Click += Confirm;

            Employees.Add(NewEmployee);
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            EnableControl(false);

            ButtonCreateEmployee.Content = "Новый сотрудник";
            ButtonCreateEmployee.Click += Button_Click;
            ButtonCreateEmployee.Click -= Confirm;

            NewEmployee = new Employee("", "", "", Positions[0]);
            NewEmployeePanel.GetBindingExpression(DataContextProperty).UpdateTarget();
        }

        private void EnableControl(bool isEnable)
        {
            foreach (UIElement element in NewEmployeePanel.Children)
            {
                if (element.GetType() == typeof(TextBox) ||
                    element.GetType() == typeof(ComboBox))
                {
                    element.IsEnabled = isEnable;
                }
            }
        }

        private void Connect(string host, string port, string user, string pass, string dbname)
        {
            string cs = string.Format("Server={0}; Port={1); User ID={2}; Password={3}; DataBase={4}", host, port, user, pass, dbname);

            connection = new NpgsqlConnection(cs);
            connection.Open();
        }

    }
}
