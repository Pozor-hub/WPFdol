using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPFdol
{
    public class Employee : INotifyPropertyChanged
    {
        public static int EmployeeID = 0;
        private string surname;
        private string name;
        private string patronymic;
        private int position;

        public Employee(string surname, string name, string patronymic, int position)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Position = position;
            ID = ++EmployeeID;
        }

        public int ID { get; set; }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }
        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Surname"));
            }
        }

        public string Patronymic
        {
            get => patronymic;
            set
            {
                patronymic = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Patronymic"));
            }
        }
        public int Position
        {
            get => position;
            set
            {
                position = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Position"));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }






    }
}
