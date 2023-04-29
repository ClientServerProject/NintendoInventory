using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

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
                //verify user creditianl
                if (LoginInfo.Email == "admin@mysite.com" && LoginInfo.Password == "NintendoInventory1988!")
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
                return Page();
            }
            return Page();
        }
        private bool LoginVerified()
        {

            return true;
        }

        private string GetUserSalt(string email)
        {
            using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                string sql = "SELECT salt FROM [User] WHERE Email=@email";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email", LoginInfo.Email);
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
