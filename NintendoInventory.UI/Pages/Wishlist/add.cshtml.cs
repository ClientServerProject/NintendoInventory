using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;

namespace NintendoInventory.UI.Pages.Wishlist
{
    public class addModel : PageModel
    {
        public void OnGet()
        {
        }
        //Adds games/consoles to the wishlist. Probably will be used in games and consoles page and not wishlist.

        public IActionResult OnPost()
        {
            //if (LikeButton is selected) //pseudocode
            {
                /*
                             * 1. Create a SQL connection object
                             * 2. Construct a SQL statement
                             * 3. Create a SQL command object
                             * 4. Open the SQL connection
                             * 5. Execute the SQL command
                             * 6. Close the SQL connection
                             * 
                             */
                using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
                {
                    // step 1
                    // step 2
                    string sql = "INSERT INTO Wishlist(WLItemImageURL, WLItemName, WLItemReleaseDate, WLItemPrice) " +
                        "VALUES (@wLItemImageURL, @wLItemName, @wLItemReleaseDate, @wLItemPrice)";
                    // step 3
                    SqlCommand cmd = new SqlCommand(sql, conn);
                   //cmd.Parameters.AddWithValue("@wLItemImageUR", NewWLItem.WLItemImageUR);
                   //cmd.Parameters.AddWithValue("@wLItemName", NewWLItem.WLItemName);
                   //cmd.Parameters.AddWithValue("@wLItemReleaseDateL", NewWLItem.WLItemReleaseDate);
                   //cmd.Parameters.AddWithValue("@wLItemPrice", NewWLItem.WLItemPrice);
                    // step 4
                    conn.Open();
                    // step 5
                    cmd.ExecuteNonQuery();
                }
            }
            return Page();


        }
    }
}
