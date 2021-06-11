using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Security;
using Zeiterfassung_Domain;
using Zeiterfassung_Domain.Interface;

namespace DB
{
    public class DBEmployeeLogic : IEmployeeLogic
    {
        internal string connection = "datasource = localhost; port= 3306; username = root; password=; database = zeiterfassung";
        Hashing hashing = new Hashing();
        public void OpenConnection(MySqlConnection DBConnect)
        {
            if (DBConnect.State != System.Data.ConnectionState.Open)
            {
                DBConnect.Open();
            }
        }
        public int CountRows()
        {
            MySqlConnection DBConnect = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand("SELECT EmployeeNumber FROM employees", DBConnect);
            OpenConnection(DBConnect);
            MySqlDataReader rdr = cmd.ExecuteReader();

            int count = 0;

            while (rdr.Read())
            {
                count++;
            }
            return count;
        }

        public void CheckRows()
        {
            List<Employee> cr = SQLReadEmployees();

            foreach (Employee item in cr)
            {
                if (item.EmployeeNumber == 1)
                {
                    continue;
                }
                if (item.EmployeeNumber -1 == cr.IndexOf(item)+1)
                {
                    for (int i = cr.IndexOf(item); i < cr.Count -1; i++)
                    {
                        cr[i].EmployeeNumber -= 1;
                    }
                    break;
                }
            }


        }


        public void SQLAddEmployee(Employee employee)
        {
            

            MySqlConnection DBConnect = new MySqlConnection(connection);
            OpenConnection(DBConnect);
            string HashedPassword = hashing.GetHash(employee.Password);

            string SQL = "INSERT INTO employees(EmployeeNumber, Name, Email, Password, Admin) VALUES(@EmployeeID, @Name,@Email,@Password,@Admin)";
            MySqlCommand cmd = new MySqlCommand(SQL, DBConnect);

            cmd.Parameters.AddWithValue("EmployeeID", CountRows());
            cmd.Parameters.AddWithValue("Name", employee.Name);
            cmd.Parameters.AddWithValue("Email", employee.GivenEmail);
            cmd.Parameters.AddWithValue("Password", HashedPassword);
            cmd.Parameters.AddWithValue("Admin", employee.Admin);

            cmd.ExecuteNonQuery();

        }
        public List<Employee> SQLReadEmployees()
        {
            MySqlConnection DBConnect = new MySqlConnection(connection);
            OpenConnection(DBConnect);

            List<Employee> returnList = new List<Employee>();

            string sql = "SELECT * FROM employees";
            MySqlCommand cmd = new MySqlCommand(sql, DBConnect);
            OpenConnection(DBConnect);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Employee emp = new Employee();

                    emp.EmployeeNumber = rdr.GetInt32(0);
                    emp.Name = rdr.GetString(1);
                    emp.Email = rdr.GetString(2);
                    emp.Password = rdr.GetString(3);
                    emp.Admin = rdr.GetBoolean(4);

                    returnList.Add(emp);
                }
            }
            return returnList;
        }

        public Employee SQLReadEmployeebyID(int id)
        {
            MySqlConnection DBConnect = new MySqlConnection(connection);
            OpenConnection(DBConnect);

            string sql = "SELECT * FROM employees WHERE EmployeeNumber =" +id;
            MySqlCommand cmd = new MySqlCommand(sql, DBConnect);
            OpenConnection(DBConnect);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Employee emp = new Employee();

                    emp.EmployeeNumber = rdr.GetInt32(0);
                    emp.Name = rdr.GetString(1);
                    emp.Email = rdr.GetString(2);
                    emp.Password = rdr.GetString(3);
                    emp.Admin = rdr.GetBoolean(4);

                    return emp;
                }
            }
            return null;
        }

        public Employee SQLReadEmployeebyEmail(string Email)
        {
            MySqlConnection DBConnect = new MySqlConnection(connection);
            OpenConnection(DBConnect);

            string sql = $"SELECT * FROM employees WHERE Email = '{Email}'";
            MySqlCommand cmd = new MySqlCommand(sql, DBConnect);
            OpenConnection(DBConnect);
            MySqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    Employee emp = new Employee();

                    emp.EmployeeNumber = rdr.GetInt32(0);
                    emp.Name = rdr.GetString(1);
                    emp.Email = rdr.GetString(2);
                    emp.Password = rdr.GetString(3);
                    emp.Admin = rdr.GetBoolean(4);

                    return emp;
                }
            }
            return null;
        }


        public void SQLUpdateEmployee(Employee employee)
        {
            MySqlConnection DBConnect = new MySqlConnection(connection);
            OpenConnection(DBConnect);


            string SQL = "UPDATE employees SET Name = @Name, Email = @Email, Password = @Password, Admin = @Admin WHERE EmployeeNumber = @EmployeeNr";
            MySqlCommand cmd = new MySqlCommand(SQL, DBConnect);

            cmd.Parameters.AddWithValue("EmployeeNr", employee.EmployeeNumber);

            cmd.Parameters.AddWithValue("EmployeeNr", employee.EmployeeNumber); 
            cmd.Parameters.AddWithValue("Name", employee.Name);
            cmd.Parameters.AddWithValue("Email", employee.Email);
            cmd.Parameters.AddWithValue("Password", employee.Password);
            cmd.Parameters.AddWithValue("Admin", employee.Admin);


            cmd.ExecuteNonQuery();
        }
        

        public void SQLRemoveEmployee(int id)
        {
            MySqlConnection DBConnect = new MySqlConnection(connection);
            OpenConnection(DBConnect);

            string SQl = "DELETE FROM employees WHERE EmployeeNumber =" +id;

            MySqlCommand cmd = new MySqlCommand(SQl, DBConnect);

            cmd.ExecuteNonQuery();
            CheckRows();
        }

    }
}
