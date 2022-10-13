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

    public partial class SpecialtyPage : Page
    {



        public SpecialtyPage()
        {
            InitializeComponent();
          

            Binding binding = new Binding();
            binding.Source = DataLoader.Specialitys;

            specialty_groups.SetBinding(ListBox.ItemsSourceProperty, binding);
            DataContext = this;
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

            NpgsqlCommand command = Database.GetCommand("INSERT INTO specialty (code, caption, qualification) VALUES (@code, @name, @qualification)");           
            command.Parameters.AddWithValue("@code", code);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@qualification", qualification);
            int result = command.ExecuteNonQuery();
            if (result == 1)
            {
                MessageBox.Show("Специальность успешно добавлена");
                DataLoader.Fetch();
            }
        }
    }
}
