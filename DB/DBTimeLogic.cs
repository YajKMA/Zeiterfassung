using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Security;
using Zeiterfassung_Domain;
using Zeiterfassung_Domain.Interface;

namespace DB
{
    public class DBTimeLogic : ITimeLogic
    {
        internal string connection = "datasource = localhost; port= 3306; username = root; password=; database = zeiterfassung";

        IEmployeeLogic db = new DBEmployeeLogic();
        public void CheckRows()
        {
            throw new System.NotImplementedException();
        }

        public int CountRows()
        {
            MySqlConnection DBConnect = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand("SELECT EmployeeNumber FROM employees", DBConnect);
            OpenConnection(DBConnect);
            MySqlDataReader rdr = cmd.ExecuteReader();

            int count = 1;

            while (rdr.Read())
            {
                count++;
            }
            return count;
        }

        public void OpenConnection(MySqlConnection DBConnect)
        {
            if (DBConnect.State != System.Data.ConnectionState.Open)
            {
                DBConnect.Open();
            }
        }

        public void SQLAddTime(Time time, int id)
        {
            MySqlConnection DBConnect = new MySqlConnection(connection);
            OpenConnection(DBConnect);
            Employee emp = new Employee();


            
            string SQL = "INSERT INTO time(EmployeeNumber, StartTime, EndTime, ResultTime) VALUES(@EmployeeID,@StartTime,@EndTime,@ResultTime)";
            MySqlCommand cmd = new MySqlCommand(SQL, DBConnect);

            cmd.Parameters.AddWithValue("EmployeeID", id);
            cmd.Parameters.AddWithValue("StartTime", time.StartTime);
            cmd.Parameters.AddWithValue("EndTime", time.EndTime);
            cmd.Parameters.AddWithValue("ResultTime", time.ResultTime.ToString());


            cmd.ExecuteNonQuery();
        }

        public List<Time> SQLReadTime()
        {
            {
                MySqlConnection DBConnect = new MySqlConnection(connection);
                OpenConnection(DBConnect);

                List<Time> returnList = new List<Time>();

                string sql = "SELECT * FROM time";
                MySqlCommand cmd = new MySqlCommand(sql, DBConnect);
                OpenConnection(DBConnect);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Time t = new Time();

                        t.EmployeeID = rdr.GetInt32(0);
                        t.StartTime = rdr.GetDateTime(1);
                        t.EndTime = rdr.GetDateTime(2);
                        t.ResultTime = rdr.GetTimeSpan(3);

                        returnList.Add(t);         
                    }
                }
                return returnList;
            }
        }

        

        public List<Time> SQLReadTimebyID(int id)
        {
            {
                MySqlConnection DBConnect = new MySqlConnection(connection);
                OpenConnection(DBConnect);

                List<Time> returnList = new List<Time>();

                string sql = $"SELECT * FROM time WHERE EmployeeNumber = '{id}'";
                MySqlCommand cmd = new MySqlCommand(sql, DBConnect);
                OpenConnection(DBConnect);
                MySqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Time t = new Time();

                        t.EmployeeID = rdr.GetInt32(0);
                        t.StartTime = rdr.GetDateTime(1);
                        t.EndTime = rdr.GetDateTime(2);
                        t.ResultTime = rdr.GetTimeSpan(3);

                        returnList.Add(t);
                    }
                }
                return returnList;
            }
        }

        

        public void SQLRemoveTime(int id)
        {
            throw new System.NotImplementedException();
        }

        public void SQLUpdateTime(Time time)
        {
            throw new System.NotImplementedException();
        }
    }
}