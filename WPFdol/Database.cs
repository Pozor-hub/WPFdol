using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using NpgsqlTypes;

namespace WPFdol
{
    public class Database
    {
        private static NpgsqlConnection Connection { get; set; }

        public static void Connect(string config)
        {
            Connection = new NpgsqlConnection(config);
            Connection.Open();
        }

        public static NpgsqlCommand GetCommand(string sql)
        {
            NpgsqlCommand command = new NpgsqlCommand();
            command.Connection = Connection;
            command.CommandText = sql;
            return command;
        }
    }
}
