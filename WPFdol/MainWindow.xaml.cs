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

           
            Positions = new ObservableCollection<string>();
            Employees = new ObservableCollection<Employee>();
            

            Binding binding = new Binding();
            binding.Source = Positions;

            

            PositionList.SetBinding(ComboBox.ItemsSourceProperty, binding);
            CreatePositionList.SetBinding(ComboBox.ItemsSourceProperty, binding);

            Connect("10.14.206.27", "5432", "student", "1234", "Workers");
            LoadPositions();
            LoadWorkers();

            DataContext = this;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EnableControl(true);

            ButtonCreateEmployee.Content = "Зафиксировать изменения";
            ButtonCreateEmployee.Click -= Button_Click;
            ButtonCreateEmployee.Click += Confirm;

            Employees.Add(NewEmployee);

            

            string WorkersSurname = TextSurname.Text.Trim();
            if (WorkersSurname.Length == 0) return;
            string WorkersName = TextName.Text.Trim();
            if (WorkersName.Length == 0) return;
            string WorkersPatronymic = TextPatronymic.Text.Trim();
            if (WorkersSurname.Length == 0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO \"Workers\"(\"Surname\", \"Name\", \"Patronymic\") VALUES(@a, @b, @c)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, WorkersSurname);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, WorkersName);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, WorkersPatronymic);
            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Сотрудник успешно добавлен!");
                LoadWorkers();
            }


        }
        private void LoadWorkers()
        {
            Employees.Clear();

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT \"Surname\", \"Name\", \"Patronymic\", \"Position\" FROM \"Workers\"";
            var result = command.ExecuteReader();
            if (result == null) return;
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Employees.Add(new Employee(result.GetString(0), result.GetString(1), result.GetString(2), result.GetInt32(3)));

                }
            }
            result.Close();
        }

        private void Confirm(object sender, RoutedEventArgs e)
        {
            EnableControl(false);

            ButtonCreateEmployee.Content = "Новый сотрудник";
            ButtonCreateEmployee.Click += Button_Click;
            ButtonCreateEmployee.Click -= Confirm;

            
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
            string cs  = string.Format ("Host=10.14.206.27;Username=student;Password=1234;Database=Workers");
            NpgsqlConnection nc = new NpgsqlConnection(cs);
            connection = new NpgsqlConnection(cs);
            connection.Open();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string positionName = textBoxNewPosition.Text.Trim();
            if (positionName.Length==0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO \"Position\"(\"Position\") VALUES(@name)";
            command.Parameters.AddWithValue("@name", NpgsqlDbType.Varchar, positionName);

            int result = command.ExecuteNonQuery();
            if (result==1)
            {
                MessageBox.Show("Должность успешно добавлена!");
                LoadPositions();
            }
        }

        private void LoadPositions()
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT \"Position\" FROM \"Position\" ORDER BY \"Position\"";
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while(result.Read())
                {
                    Positions.Add(result.GetString(0));
                }
            }
            result.Close();
        }









    }
}
