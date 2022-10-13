using Npgsql;
using NpgsqlTypes;
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

    public partial class GroupEditPage : Page
    {
        public GroupEditPage()
        {
            InitializeComponent();

            Binding binding = new Binding();
            binding.Source = DataLoader.Specialitys;
            Binding binding1 = new Binding();
            binding1.Source = DataLoader.Groups;

            specialty_groups.SetBinding(ComboBox.ItemsSourceProperty, binding);
            groups_student.SetBinding(ListBox.ItemsSourceProperty, binding1);

            DataContext = this;
        }

        private void ButtonEditSpeciality_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(number_groups.Text.Trim());
            if (id == 0) return;

            int Name = int.Parse(course_groups.Text.Trim());
            if (Name == 0) return;

          
            NpgsqlCommand command = Database.GetCommand("UPDATE \"study_group\" SET course = @b, number = @c");
            command.Parameters.AddWithValue("@b", NpgsqlDbType.Integer, id);
            command.Parameters.AddWithValue("@c", NpgsqlDbType.Integer, Name);

            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Группа успешно отредактирована!");
                DataLoader.Fetch();
            }
        }
    }
}
