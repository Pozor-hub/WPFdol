using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFdol
{
    public class DataLoader
    {
        public static ObservableCollection<Specialty> Specialitys { get; set; }
        public static ObservableCollection<Group> Groups { get; set; }
        public static ObservableCollection<student> Students { get; set; }

        public static void Fetch()
        {
            if (Specialitys == null)
            {
                Specialitys = new ObservableCollection<Specialty>();
            }
            if (Groups == null)
            {
                Groups = new ObservableCollection<Group>();
            }
            if (Students == null)
            {
                Students = new ObservableCollection<student>();
            }
            LoadSpecialty();
            LoadGroup();
            LoadStudent();
        }
        public static void LoadSpecialty()
        {
            Specialitys.Clear();

            var command = Database.GetCommand("SELECT code, caption ,qualification FROM specialty");
            var result = command.ExecuteReader();
            if (result == null) return;
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Specialitys.Add(new Specialty(result.GetInt32(0), result.GetString(1), result.GetString(2)));
                }
            }
            result.Close();
        }

        public static void LoadGroup()
        {
            Groups.Clear();

            var command = Database.GetCommand("SELECT number, course, caption FROM study_group as g join specialty as s on g.specialty=s.id ORDER BY number");
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Groups.Add(new Group(result.GetInt32(0), result.GetInt32(1), result.GetString(2)));
                }
            }
            result.Close();
        }

        public static void LoadStudent()
        {
            Students.Clear();

            var command = Database.GetCommand("SELECT surname, name FROM student");
            NpgsqlDataReader result = command.ExecuteReader();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Students.Add(new student(result.GetString(0), result.GetString(1)));
                }
            }
            result.Close();
        }

    }
}
