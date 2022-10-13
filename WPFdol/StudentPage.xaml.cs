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

    public partial class StudentPage : Page
    {

        public StudentPage()
        {
            InitializeComponent();

            Binding binding1 = new Binding();
            binding1.Source = DataLoader.Groups;
            Binding binding2 = new Binding();
            binding2.Source = DataLoader.Students;

            groups_student.SetBinding(ComboBox.ItemsSourceProperty, binding1);
            studentList.SetBinding(ListBox.ItemsSourceProperty, binding2);

        }

        private void ButtonCreateSpeciality_Click(object sender, RoutedEventArgs e)
        {

            string surname_group = surname_student.Text.Trim();
            if (surname_group.Length == 0) return;
            string name_group = name_student.Text.Trim();
            if (name_group.Length == 0) return;

            NpgsqlCommand command = Database.GetCommand("INSERT INTO student(name, surname) VALUES(@a, @b)"); 
            command.Parameters.AddWithValue("@a", NpgsqlDbType.Varchar, surname_group);
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Varchar, name_group);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Студент успешно добавлен!");
                DataLoader.Fetch();
            }
        }
    }
}
