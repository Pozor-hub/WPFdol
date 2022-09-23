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

            //Binding binding = new Binding();
            //binding.Source = Codes;
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
            
            Specialitys.Add(NewSpeciality);

            string CodeSpec = code_specialty.Text.Trim();
            if (CodeSpec.Length == 0) return;
            string NameSpec = name_specialty.Text.Trim();
            if (NameSpec.Length == 0) return;
            string qualification = kval_specialty.Text.Trim();
            if (qualification.Length == 0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO speciality(code, namespec, qualifiation) VALUES(@a, @b, @c)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, CodeSpec);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, NameSpec);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Varchar, qualification);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Специальность успешно добавлена!");
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
                    Specialitys.Add(new Specialty(result.GetString(0), result.GetString(1), result.GetString(2)));
                }
            }
            result.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Groups.Add(NewGroups);

            string number_group = number_groups.Text.Trim();
            if (number_group.Length == 0) return;
            string CourseGroup = course_groups.Text.Trim();
            if (CourseGroup.Length == 0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO group(number, course) VALUES(@a, @b)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, number_group);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, CourseGroup);

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
            command.CommandText = "SELECT course FROM study_group ORDER BY number";
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    //Groups.Add(new Group(result.GetString(0), result.GetString(1)));
                }
            }
            result.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            Students.Add(NewStudent);

            string SurName = surname_groups.Text.Trim();
            if (SurName.Length == 0) return;
            string StName = name_groups.Text.Trim();
            if (StName.Length == 0) return;

            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO \"Position\"(\"Position\") VALUES(@a, @b)";
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, SurName);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, StName);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Студент успешно добавлен!");

            }
        }
    }
}