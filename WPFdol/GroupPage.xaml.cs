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

    public partial class GroupPage : Page
    {

        public GroupPage()
        {
            InitializeComponent();



            Binding binding = new Binding();
            binding.Source = DataLoader.Specialitys;
            Binding binding1 = new Binding();
            binding1.Source = DataLoader.Groups;

            specialty_groups.SetBinding(ComboBox.ItemsSourceProperty, binding);
            groups_student.SetBinding(ListBox.ItemsSourceProperty, binding1);
        }


        private void ButtonCreateSpeciality_Click(object sender, RoutedEventArgs e)
        {

            int id = int.Parse(number_groups.Text.Trim());
            if (id == 0) return;

            int Name = int.Parse(course_groups.Text.Trim());
            if (Name == 0) return;


            NpgsqlCommand command = Database.GetCommand("INSERT INTO \"study_group\"(number, course) VALUES(@number, @course)");
            command.Parameters.AddWithValue("@number", NpgsqlDbType.Integer, id);
            command.Parameters.AddWithValue("@course", NpgsqlDbType.Integer, Name);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Группа успешно добавлена!");
                DataLoader.Fetch();
            }
        }

    }
}
