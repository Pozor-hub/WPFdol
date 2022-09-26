using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WPFdol
{
    public class Group
    {
        public static int GroupID = 0;
        private string course;
        private string speciality;
     

        public Group(string number, string course)
        {
            Number = number;
            Course = course;
          
        }

        public string Number
        {
            get => speciality;
            set
            {
                speciality = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Number1"));
            }
        }
        public string Course
        {
            get => course;
            set
            {
                course = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Course"));
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
