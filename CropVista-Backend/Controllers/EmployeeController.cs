using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        string connectionString = "Data Source=DESKTOP-RO3M9PJ\\SQLEXPRESS;Initial Catalog=cropVista;Integrated Security=True; Encrypt=False;";

        [HttpGet]
        [Route("getEmployees")]
        public Result<List<Employee>> GetEmployees()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    List<Employee> employeesList = new List<Employee>();

                    EmployeeServices employeeServices = new EmployeeServices();

                    employeesList = employeeServices.GetEmployees(connection);

                    return new Result<List<Employee>>
                    {
                        result = employeesList,
                        success = true,
                        message = "GET_EMPLOYEES"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<Employee>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getEmployeesById/{id}")]
        public Result<Employee> getEmployeesById(int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Employee employee = new Employee();

                    EmployeeServices employeeServices = new EmployeeServices();
                    employee = employeeServices.getEmployeesById(connection, id);

                    return new Result<Employee>
                    {
                        success = true,
                        result = employee,
                        message = "GET_EMPLOYEE_BY_ID"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Employee>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("create")]
        public Result<Employee> AddEmployee(Employee employee)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    EmployeeServices employeeServices = new EmployeeServices();
                    employee = employeeServices.AddEmployee(connection, employee);

                    return new Result<Employee>
                    {
                        success = true,
                        result = employee,
                        message = "ADD_EMPLOYEE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Employee>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}