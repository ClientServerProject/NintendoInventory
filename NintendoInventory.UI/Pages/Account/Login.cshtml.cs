using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NintendoInventory.UI.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credintial LoginInfo { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
                {
 

                    int userID = -1;
                    string validPswd = string.Empty;
                    try
                    {
                        string sql = "Select UserID from [User] where Email = @email";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@email", LoginInfo.Email);
                        
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
                        validPswd = GetValidPswd(userID);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "Invalid email");
                        if (!ModelState.IsValid)
                        {
                            return Page();
                        }
                    }
                    var passwordHash = HashPasswordV2(LoginInfo.Password, GetUserSalt(userID));
                    string enteredPswd = Convert.ToBase64String(passwordHash);

                    //verify user creditianl
                    if (LoginInfo.Email == "admin@mysite.com" && enteredPswd.Equals(validPswd))
                    {
                        //create security context

                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, LoginInfo.Email),
                        new Claim(ClaimTypes.Name, "Admin"),
                        new Claim("Username", "Admin")
                    };

                        var identity = new ClaimsIdentity(claims, "NintendoInventoryCookie");
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("NintendoInventoryCookie", principal);

                        return RedirectToPage("/Index");

                        //create secutrity context
                    }
                    else if (enteredPswd.Equals(validPswd))
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, LoginInfo.Email),
                        new Claim(ClaimTypes.Name, "User"),
                        new Claim("Username", "User")
                    };

                        var identity = new ClaimsIdentity(claims, "NintendoInventoryCookie");
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("NintendoInventoryCookie", principal);

                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, enteredPswd + "  " + validPswd);
                        if (!ModelState.IsValid)
                        {
                            return Page();
                        }
                    }
                }
            }
            return Page();
        }

        private string GetValidPswd(int userID)
        {
            using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                string sql = "SELECT PasswordHash FROM [Login] WHERE UserID=@userID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader["PasswordHash"].ToString();
                }
                return "";
            }
        }
        private string GetUserSalt(int userID)
        {
            using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                string sql = "SELECT Salt FROM [Login] WHERE UserID=@userID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    return reader["Salt"].ToString();
                }
                return "";
            }
        }
        private static byte[] HashPasswordV2(string password, string saltStr)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA256;
            const int Pbkdf2IterCount = 100000;
            const int SaltSize = 128 / 8; // 128 bits
            const int Pbkdf2SubKeyLength = 256 / 8; // 256 bits

            byte[] salt = Encoding.ASCII.GetBytes(saltStr);
            byte[] subKey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubKeyLength);

            var outputBytes = new byte[SaltSize + Pbkdf2SubKeyLength];
            //outputBytes[0] = 0x00; // format marker
            Buffer.BlockCopy(salt, 0, outputBytes, 0, SaltSize);
            Buffer.BlockCopy(subKey, 0, outputBytes, SaltSize, Pbkdf2SubKeyLength);
            return outputBytes;
        }
    }

    public class Credintial
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
