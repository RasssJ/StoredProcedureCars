using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using StoredProc.Models;

namespace StoredProc.Controllers
{
    public class DynamicSQLController : Controller
    {
        public IActionResult Index()
        {
            string strConnection = "server=(localDb)\\MSSQLLocalDB;database=StoredProc;Trusted_Connection=true;MultipleActiveResultSets=true;";

            using (SqlConnection con = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                StringBuilder sbCommand = new
                    StringBuilder("Select * from Employee where 1 = 1");

                cmd.CommandText = sbCommand.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                List<Employee> model = new List<Employee>();
                while (rdr.Read())
                {
                    var details = new Employee();
                    details.FirstName = rdr["FirstName"].ToString();
                    details.LastName = rdr["LastName"].ToString();
                    details.Gender = rdr["Gender"].ToString();
                    details.Salary = Convert.ToInt32(rdr["Salary"]);
                    model.Add(details);
                }
                return View(model);
            }

        }
        [HttpPost]
        public IActionResult Index(string inputFirstname, string inputLastname, string inputGender, int inputSalary)
        {
            string strConnection = "server=(localDb)\\MSSQLLocalDB;database=StoredProc;Trusted_Connection=true;MultipleActiveResultSets=true;";

            using (SqlConnection con = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

                StringBuilder sbCommand = new
                    StringBuilder("Select * from Employees where 1 = 1");

                if (inputFirstname != null)
                {
                    sbCommand.Append(" AND FirstName=@FirstName");
                    SqlParameter param = new
                        SqlParameter("@FirstName", inputFirstname);
                    cmd.Parameters.Add(param);
                }

                if (inputLastname != null)
                {
                    sbCommand.Append(" AND LastName=@LastName");
                    SqlParameter param = new
                        SqlParameter("@LastName", inputLastname);
                    cmd.Parameters.Add(param);
                }

                if (inputGender != null)
                {
                    sbCommand.Append(" AND Gender=@Gender");
                    SqlParameter param = new
                        SqlParameter("@Gender", inputGender);
                    cmd.Parameters.Add(param);
                }

                if (inputSalary != 0)
                {
                    sbCommand.Append(" AND Salary=@Salary");
                    SqlParameter param = new
                        SqlParameter("@Salary", inputSalary);
                    cmd.Parameters.Add(param);
                }

                cmd.CommandText = sbCommand.ToString();
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                List<Employee> model = new List<Employee>();
                while (rdr.Read())
                {
                    var details = new Employee();
                    details.FirstName = rdr["FirstName"].ToString();
                    details.LastName = rdr["LastName"].ToString();
                    details.Gender = rdr["Gender"].ToString();
                    details.Salary = Convert.ToInt32(rdr["Salary"]);
                    model.Add(details);
                }
                return View(model);
            }
        }
    }
}
