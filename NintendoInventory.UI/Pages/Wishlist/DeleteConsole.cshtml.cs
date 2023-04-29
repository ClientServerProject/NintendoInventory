using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;

namespace NintendoInventory.UI.Pages.Wishlist
{
    public class DeleteConsoleModel : PageModel
    {
        public IActionResult OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                string sql = "DELETE FROM ConsoleWishlist WHERE ConsoleID = @ConsoleID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ConsoleID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");
            }
        }
    }
}
