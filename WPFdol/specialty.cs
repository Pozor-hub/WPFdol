using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WPFdol
{
        public class Specialty : INotifyPropertyChanged
        {
            public static int SpecialityID = 0;
            private string code;
            private string name_specialty;
            private string qualification;



            public Specialty(string code, string namespec, string qualification)
            {
                Codes = code;




                Namespec = namespec;
                Qualification = qualification;
            }

            public string Codes
            {
                get => code;
                set
                {
                    code = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Codes"));
                }

            }
            public string Namespec
            {
                get => name_specialty;
                set
                {
                    name_specialty = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Namespec"));
                }

            }
            public string Qualification
            {
                get => qualification;
                set
                {
                    qualification = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Qualification"));
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
