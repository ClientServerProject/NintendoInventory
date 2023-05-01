using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;

namespace NintendoInventory.UI.Pages.Wishlist
{
    public class DeleteGameModel : PageModel
    {
        public IActionResult OnGet(int id, string itemType)
        {
            using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                string sql = "DELETE FROM GameWishlist WHERE GameID = @GameID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gameID", id);
                conn.Open();
                cmd.ExecuteNonQuery();

                if(itemType == "deleteFromWishlistPg")
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    return RedirectToPage("/Games/Index");
                }
            }
        }
    }
}
