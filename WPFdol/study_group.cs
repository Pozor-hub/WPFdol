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
        private int course;
        private int speciality;
     

        public Group(int number, int course)
        {
            Number = number;
            Course = course;
          
        }

        public int Number
        {
            get => speciality;
            set
            {
                speciality = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Number1"));
            }
        }
        public int Course
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
