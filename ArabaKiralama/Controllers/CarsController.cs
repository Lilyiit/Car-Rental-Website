using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace internetProgramcılığı.Controllers
{
    public class CarController : Controller
    {
        private readonly string _connectionString = "Server=YIIT\\SQLEXPRESS;Database=emi;User ID=sa;Password=z1x2c3;Trusted_Connection=False;";

        // Kiralama İşlemi
        [HttpPost]
        public IActionResult RentCar(int carId, string customerName)
        {
            try
            {
                // 1. Veritabanı bağlantısı oluştur
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    // 2. Stok kontrolü yap
                    string checkStockQuery = "SELECT Stok FROM Cars WHERE CarID = @CarID";
                    SqlCommand checkStockCommand = new SqlCommand(checkStockQuery, connection);
                    checkStockCommand.Parameters.Add("@CarID", SqlDbType.Int).Value = carId;

                    connection.Open();
                    int stock = Convert.ToInt32(checkStockCommand.ExecuteScalar());

                    if (stock > 0) // Eğer stok varsa
                    {
                        // 3. Stok azaltma sorgusu
                        string updateStockQuery = "UPDATE Cars SET Stok = Stok - 1 WHERE CarID = @CarID";
                        SqlCommand updateStockCommand = new SqlCommand(updateStockQuery, connection);
                        updateStockCommand.Parameters.Add("@CarID", SqlDbType.Int).Value = carId;
                        updateStockCommand.ExecuteNonQuery();

                        // 4. Kiralama kaydını ekleme
                        string insertRentalQuery = "INSERT INTO Rentals (CarID, CustomerName, RentalDate) VALUES (@CarID, @CustomerName, @RentalDate)";
                        SqlCommand insertRentalCommand = new SqlCommand(insertRentalQuery, connection);
                        insertRentalCommand.Parameters.Add("@CarID", SqlDbType.Int).Value = carId;
                        insertRentalCommand.Parameters.Add("@CustomerName", SqlDbType.NVarChar).Value = customerName;
                        insertRentalCommand.Parameters.Add("@RentalDate", SqlDbType.DateTime).Value = DateTime.Now;
                        insertRentalCommand.ExecuteNonQuery();

                        return Json(new { success = true, message = "Araç başarıyla kiralandı." });
                    }
                    else
                    {
                        return Json(new { success = false, message = "Araç stokta yok." });
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Bir hata oluştu: " + ex.Message });
            }
        }
    }
}