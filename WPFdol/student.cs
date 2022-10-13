using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPFdol
{
        public class student : INotifyPropertyChanged
        {
            public static int StudentID = 0;
            private string surname;
            private string name;

            public student(string surname, string name)
            {
                Surname = surname;

                Name = name;
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
            public string Name
            {
                get => name;
                set
                {
                    name = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Name"));
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
