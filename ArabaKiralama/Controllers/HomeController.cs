using System.Data.SqlClient;
using internetProgramcılığı.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArabaKiralama1.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {

            var loggedUSerName = HttpContext.Session.GetString("LoggedUser");
            if (!string.IsNullOrEmpty(loggedUSerName))
            {
                ViewBag.username = loggedUSerName;
            }

            string _connectionString = "Server=YIIT\\SQLEXPRESS;Database=emi;User ID=sa;Password=z1x2c3;Trusted_Connection=False;";




            List<Cars> cars = new List<Cars>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Cars";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Cars car = new Cars
                    {
                        CarID = Convert.ToInt32(reader["carID"]),
                        Brand = reader["Brand"].ToString(),
                        Model = reader["Model"].ToString(),
                        Engine = reader["Engine"].ToString(),
                        ImageUrl = reader["ImageUrl"].ToString(),
                        Price = Convert.ToDouble((decimal)reader["Price"]),
                    };

                    cars.Add(car);
                }
            }
            return View(cars); // Kullanıcıyı 2Y RENTAL ekranına yönlendir
        }

    }
}