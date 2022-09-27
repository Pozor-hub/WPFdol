using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Npgsql;
using NpgsqlTypes;

namespace WPFdol
{

    public partial class MainWindow : Window
    {

        private NpgsqlConnection connection;

        public ObservableCollection<Specialty> Specialitys { get; set; }
        public ObservableCollection<Group> Groups { get; set; }
        public ObservableCollection<student> Students { get; set; }
        public student NewStudent { get; set; }
        public Specialty NewSpeciality { get; set; }
        public Group NewGroups { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            Specialitys = new ObservableCollection<Specialty>();
            Groups = new ObservableCollection<Group>();
            Students = new ObservableCollection<student>();



            Connect("10.14.206.27", "5432", "Denis", "1234", "363Min");

            Binding binding = new Binding();
            binding.Source = Specialitys;
            Binding binding1 = new Binding();
            binding1.Source = Groups;

            specialty_groups.SetBinding(ListBox.ItemsSourceProperty, binding);
            groups_student.SetBinding(ListBox.ItemsSourceProperty, binding1);

            //binding.Source = Numbers;

            LoadSpec();
            LoadGroup();
            DataContext = this;
        }
        private void Connect(string host, string port, string user, string pass, string dbname)
        {
            string cs = string.Format("Host=10.14.206.27;Username=student;Password=1234;Database=363Min");
            NpgsqlConnection nc = new NpgsqlConnection(cs);
            connection = new NpgsqlConnection(cs);
            connection.Open();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            code_specialty.Text = code_specialty.Text.Trim();
            if (code_specialty.Text.Length == 0) return;

            int code = int.Parse(code_specialty.Text);

            string name = name_specialty.Text.Trim();
            if (name.Length == 0) return;
            string qualification = kval_specialty.Text.Trim();
            if (qualification.Length == 0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO specialty (code, caption, qualification) VALUES (@code, @name, @qualification)";
            command.Parameters.AddWithValue("@code", code);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@qualification", qualification);
            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Специальность успешно добавлена");
                LoadSpec();
            }
        }
        private void LoadSpec()
        {
            Specialitys.Clear();

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT code, caption ,qualification FROM specialty";
            var result = command.ExecuteReader();
            if (result == null) return;
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Specialitys.Add(new Specialty(result.GetInt32(0), result.GetString(1), result.GetString(2)));
                }
            }
            result.Close();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            
            Groups.Add(NewGroups);

            int id = int.Parse(number_groups.Text.Trim());
            if (id == 0 ) return;
            
            int Name = int.Parse(course_groups.Text.Trim());
            if  (Name == 0) return;


            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO \"study_group\"(number, course) VALUES(@number, @course)";
            command.Parameters.AddWithValue("@number", NpgsqlDbType.Integer, id);
            command.Parameters.AddWithValue("@course", NpgsqlDbType.Integer, Name);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Группа успешно добавлена!");
                LoadGroup();
            }

        }
        private void LoadGroup()
        {
            Groups.Clear();
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT number, course FROM study_group ORDER BY number";
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                   Groups.Add(new Group(result.GetInt32(0), result.GetInt32(1)));
                }
            }
            result.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Students.Add(NewStudent);

            string surname_group = surname_student.Text.Trim();
            if (surname_group.Length == 0) return;
            string name_group = name_student.Text.Trim();
            if (name_group.Length == 0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO student(name, surname) VALUES(@a, @b)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, surname_group);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, name_group);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Студент успешно добавлен!");
            }
        }
    }
}