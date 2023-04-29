using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;
using System.Security.Cryptography;

namespace NintendoInventory.UI.Pages.Account
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User NewUser { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var random = RandomNumberGenerator.Create();
                var passwordHash = HashPasswordV2(NewUser.Password, random);
                byte[] salt = new byte[128 / 8];
                Buffer.BlockCopy(passwordHash, 1, salt, 0, salt.Length);

                using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
                {
                    string sql = "INSERT INTO [User] (Email, FirstName, LastName, DateJoined) " +
                        "VALUES (@email, @firstName, @lastName, @dateJoined)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@email", NewUser.Email);
                    cmd.Parameters.AddWithValue("@firstName", NewUser.FirstName);
                    cmd.Parameters.AddWithValue("@lastName", NewUser.LastName);
                    cmd.Parameters.AddWithValue("@dateJoined", DateTime.Now);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    sql = "Select UserID from [User] where Email = @email";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@email", NewUser.Email);

                    int userID = 0;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userID = reader.GetInt32(0); 
                        }
                    }
                    conn.Close();
                    sql = "Insert INTO Login (UserID, PasswordHash, Salt, LastLoginTime) " +
                        "VALUES (@userID, @passwordHash, @salt, @lastLoginTime)";
                    cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@salt", Convert.ToBase64String(salt));
                    cmd.Parameters.AddWithValue("@passwordHash", Convert.ToBase64String(passwordHash));
                    cmd.Parameters.AddWithValue("@lastLoginTime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@userID", userID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                return RedirectToPage("Login");
            }
            return Page();
        }

        private static byte[] GenerateSalt(int length)
        {
            using RSACryptoServiceProvider rngCsp = new();
            byte[] randomBytes = new byte[length];
            return randomBytes;
        }

        private static byte[] HashPasswordV2(string password, RandomNumberGenerator rng)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA256;
            const int Pbkdf2IterCount = 100000;
            const int SaltSize = 128 / 8; // 128 bits
            const int Pbkdf2SubKeyLength = 256 / 8; // 256 bits

            byte[] salt = new byte[SaltSize];
            rng.GetBytes(salt);
            byte[] subKey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubKeyLength);

            var outputBytes = new byte[SaltSize + Pbkdf2SubKeyLength];
            //outputBytes[0] = 0x00; // format marker
            Buffer.BlockCopy(salt, 0, outputBytes, 0, SaltSize);
            Buffer.BlockCopy(subKey, 0, outputBytes, SaltSize, Pbkdf2SubKeyLength);
            return outputBytes;
        }
    }
}