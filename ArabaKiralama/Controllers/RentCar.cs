using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;

namespace ArabaKiralama.Controllers
{
    public class RentCar : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        string connectionString = "Server=YIIT\\SQLEXPRESS;Database=emi;User ID=sa;Password=z1x2c3;Trusted_Connection=False;";


        [HttpPost]
        public IActionResult RentCarCreate(int carId, string customerName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Rentals (CarID, CustomerName, RentalDate) VALUES (@CarID, @CustomerName, @RentalDate)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@CarID", carId);
                cmd.Parameters.AddWithValue("@CustomerName", customerName);
                cmd.Parameters.AddWithValue("@RentalDate", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}