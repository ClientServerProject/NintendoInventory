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
                    string validPswd = string.Empty;
                    int userID = -1;
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
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid email");
                        if (!ModelState.IsValid)
                        {
                            return Page();
                        }
                    }
                    conn.Close();


                    var validPassword = GetValidPswd(userID);
                    var salt = validPassword.Salt;

                    var storedPasswordHash = validPassword.PasswordHash;
                    /*                    var enteredPasswordHash = KeyDerivation.Pbkdf2(LoginInfo.Password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubKeyLength);
                    */
                    var enteredPasswordHash = HashPasswordV2(LoginInfo.Password, salt);


                    //verify user creditianl
                    if (LoginInfo.Email == "admin@mysite.com" && LoginInfo.Password == "NintendoInventory1988!" || enteredPasswordHash.SequenceEqual(storedPasswordHash))
                    {
                        int AdminRoleID = -1;
                        sql = "Select SystemRoleID from SystemRole where SystemRoleName = @Admin";
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@Admin", "Administrator");

                        conn.Open();
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                AdminRoleID = reader.GetInt32(0);

                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Database Error");
                            if (!ModelState.IsValid)
                            {
                                return Page();
                            }
                        }
                        conn.Close();

                        int currentSystemRole = -1;
                        sql = "Select SystemRoleID from UserSystemRole where UserID = @userID";
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@userID", userID);

                        conn.Open();
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                currentSystemRole = reader.GetInt32(0);

                            }
                        }
                        else
                        {
                            conn.Close();
                            sql = "Insert INTO UserSystemRole (UserID, SystemRoleID) " +
                                                   "VALUES (@userID, @SystemRoleID)";
                            cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@userID", userID);
                            cmd.Parameters.AddWithValue("@SystemRoleID", AdminRoleID);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();

                        //create security context

                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, LoginInfo.Email),
                        new Claim(ClaimTypes.Name, "Admin"),
                        new Claim("Username", "Admin"),
                        new Claim(ClaimTypes.NameIdentifier, userID.ToString())
                    };

                        var identity = new ClaimsIdentity(claims, "NintendoInventoryCookie");
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("NintendoInventoryCookie", principal);

                        return RedirectToPage("/Index");

                        //create secutrity context
                    }
                    else if (enteredPasswordHash.SequenceEqual(storedPasswordHash))
                    {
                        int UserRoleID = -1;
                        sql = "Select SystemRoleID from SystemRole where SystemRoleName = @User";
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@User", "User");

                        conn.Open();
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserRoleID = reader.GetInt32(0);

                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Database Error");
                            if (!ModelState.IsValid)
                            {
                                return Page();
                            }
                        }
                        conn.Close();

                        int currentSystemRole = -1;
                        sql = "Select SystemRoleID from UserSystemRole where UserID = @userID";
                        cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@userID", userID);

                        conn.Open();
                        reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                currentSystemRole = reader.GetInt32(0);

                            }
                        }
                        else
                        {
                            conn.Close();
                            sql = "Insert INTO UserSystemRole (UserID, SystemRoleID) " +
                                                   "VALUES (@userID, @SystemRoleID)";
                            cmd = new SqlCommand(sql, conn);
                            cmd.Parameters.AddWithValue("@userID", userID);
                            cmd.Parameters.AddWithValue("@SystemRoleID", UserRoleID);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                        conn.Close();

                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, LoginInfo.Email),
                        new Claim(ClaimTypes.Name, "User"),
                        new Claim("Username", "User"),
                        new Claim(ClaimTypes.NameIdentifier, userID.ToString())

                    };

                        var identity = new ClaimsIdentity(claims, "NintendoInventoryCookie");
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync("NintendoInventoryCookie", principal);
                        HttpContext.Session.SetInt32("UserID", userID);

                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid password");
                        if (!ModelState.IsValid)
                        {
                            return Page();
                        }
                    }
                }
            }
            return Page();
        }

        private (byte[] Salt, byte[] PasswordHash) GetValidPswd(int userID)
        {
            using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                string sql = "SELECT PasswordHash FROM [Login] WHERE UserID=@userID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows && reader.Read())
                {

                    string passwordHashStr = (string)reader["PasswordHash"];

                    /*                    ModelState.AddModelError(string.Empty, "byte empty?  " + passwordHashStr);
                                        if (!ModelState.IsValid)
                                        {
                                            return (new byte[0], new byte[0]);
                                        }*/
                    // Extract the salt and password hash from the combined password hash
                    byte[] passwordHash = Convert.FromBase64String(passwordHashStr);
                    const int SaltSize = 128 / 8; // 128 bits

                    byte[] salt = new byte[SaltSize];

                    Buffer.BlockCopy(passwordHash, 0, salt, 0, SaltSize);

                    return (salt, passwordHash);
                }
                return (new byte[0], new byte[0]);
            }
        }

        private static byte[] HashPasswordV2(string password, byte[] salt)
        {
            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA256;
            const int Pbkdf2IterCount = 100000;
            const int SaltSize = 128 / 8; // 128 bits
            const int Pbkdf2SubKeyLength = 256 / 8; // 256 bits

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
