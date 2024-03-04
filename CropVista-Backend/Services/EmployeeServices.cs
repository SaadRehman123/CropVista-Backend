using CropVista_Backend.Models;
using System.Data;
using System.Data.SqlClient;

namespace CropVista_Backend.Services
{
    public class EmployeeServices
    {
        public List<Employee> GetEmployees(SqlConnection connection)
        {
            List<Employee> employeesList = new List<Employee>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM employee", connection))
            {
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Employee employee = new Employee
                        {
                            Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                            first_name = Convert.ToString(dt.Rows[i]["first_name"]),
                            last_name = Convert.ToString(dt.Rows[i]["last_name"])
                        };

                        employeesList.Add(employee);
                    }
                }
            }

            return employeesList;
        }

        public Employee getEmployeesById(SqlConnection connection, int id)
        {
            Employee employee = new Employee();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM employee WHERE Id = '" + id + "'", connection))
            {
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    employee.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                    employee.first_name = Convert.ToString(dt.Rows[0]["first_name"]);
                    employee.last_name = Convert.ToString(dt.Rows[0]["last_name"]);
                }
            }

            return employee;
        }

        public Employee AddEmployee(SqlConnection connection, Employee employee)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO employee (first_name, last_name) VALUES ('" + employee.first_name + "', '" + employee.last_name + "')", connection))
            {
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
            }

            return employee;
        }
    }
}