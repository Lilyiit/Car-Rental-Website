using Microsoft.AspNetCore.Mvc;
using ArabaKiralama1.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web;

using RestSharp;
namespace ArabaKiralama1.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(User user)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı veritabanına kaydet
                SaveUserToDatabase(user);

                // Modal mesajı göstermek için TempData kullanılıyor
                TempData["ShowModal"] = true;
                return RedirectToAction("Login","Account");
            }

            return View(user);
        }

        private void SaveUserToDatabase(User user)
        {
            // Veritabanı bağlantı dizesi
            string connectionString = "Server=YIIT\\SQLEXPRESS;Database=emi;User ID=sa;Password=z1x2c3;Trusted_Connection=False;";



            // SQL sorgusu
            string query = "INSERT INTO Uyeler (Ad_Soyad, Email, Parola) VALUES (@Username, @Email, @Password)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password; // Şifre hashleme isteğe bağlı

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]

        public IActionResult yeni()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Oturumu temizle
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public IActionResult LoginUser(User user)
        {
            // Kullanıcı doğrulama
            if (ValidateUser(user))
            {

                HttpContext.Session.SetString("LoggedUser", user.Email);


                // Giriş başarılıysa ana menüye yönlendir
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Hatalı giriş durumu
                ModelState.AddModelError("", "E-posta veya şifre hatalı.");
            }

            return View("Login");
        }

        private bool ValidateUser(User user)
        {
            // Veritabanı bağlantı dizesi
            string connectionString = "Server=YIIT\\SQLEXPRESS;Database=emi;User ID=sa;Password=z1x2c3;Trusted_Connection=False;";
          
            // Kullanıcıyı kontrol eden SQL sorgusu
            string query = "SELECT COUNT(*) FROM Uyeler WHERE Email = @Email AND Parola = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar).Value = user.Password;

                    connection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0; // Kullanıcı bulunduysa true döner

                }
            }
        }
    }
}