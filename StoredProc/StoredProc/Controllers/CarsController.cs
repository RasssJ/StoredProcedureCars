using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StoredProc.Data;
using StoredProcedure.Models;

namespace StoredProcedure.Controllers
{
    public class CarsController : Controller
    {
        public StoredProcDbContext _context;
        public IConfiguration _config { get; }

        public CarsController
            (
            StoredProcDbContext context,
            IConfiguration config
            )
        {
            _context = context;
            _config = config;

        }

        public IActionResult Index()
        {
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "dbo.spSearchCars";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Car> model = new List<Car>();
                while (sdr.Read())
                {
                    var details = new Car();
                    details.carManufacturer = sdr["Make"].ToString();
                    details.carModel = sdr["Car_Model"].ToString();
                    details.carModelYear = Convert.ToInt32(sdr["Car_Model_Year"]);
                    details.carColor = sdr["CAR_Color"].ToString();
                    model.Add(details);
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Index(string Make, string Car_Model, int Car_Model_Year, string CAR_Color)
        {
            using (SqlConnection con = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "dbo.spSearchCars";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                if (Make != null)
                {
                    SqlParameter param = new SqlParameter("@CarManufacturer", Make);
                    cmd.Parameters.Add(param);
                }
                if (Car_Model != null)
                {
                    SqlParameter param = new SqlParameter("@CarModel", Car_Model);
                    cmd.Parameters.Add(param);
                }
                if (Car_Model_Year != 0)
                {
                    SqlParameter param = new SqlParameter("@CarModelYear", Car_Model_Year);
                    cmd.Parameters.Add(param);
                }
                if (CAR_Color != null)
                {
                    SqlParameter param = new SqlParameter("@CarColor", CAR_Color);
                    cmd.Parameters.Add(param);
                }
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                List<Car> model = new List<Car>();
                while (sdr.Read())
                {
                    var details = new Car();
                    details.carManufacturer = sdr["Make"].ToString();
                    details.carModel = sdr["Car_Model"].ToString();
                    details.carModelYear = Convert.ToInt32(sdr["Car_Model_Year"]);
                    details.carColor = sdr["CAR_Color"].ToString();
                    model.Add(details);
                }
                return View(model);
            }
        }
    }
}