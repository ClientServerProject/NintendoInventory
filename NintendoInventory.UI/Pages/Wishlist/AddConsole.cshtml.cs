using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;

namespace NintendoInventory.UI.Pages.Wishlist
{
    public class AddConsoleModel : PageModel
    {
        //Adds games/consoles to the wishlist. Probably will be used in games and consoles page and not wishlist.
        [BindProperty]
        public List<Models.ConsoleWishlistItem> ConsoleWishlistItem { get; set; } = new List<Models.ConsoleWishlistItem>();

        public IActionResult OnGet(int id)
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
                // step 2
                string sql = "INSERT INTO ConsoleWishlist(ConsoleID) VALUES (@ConsoleID)";
                // step 3
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ConsoleID", id);
                // step 4
                conn.Open();
                // step 5
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ConsoleWishlistItem console = new Models.ConsoleWishlistItem();
                        console.ConsoleID = int.Parse(reader["ConsoleID"].ToString());
                        console.ConsoleName = reader["ConsoleName"].ToString();
                        console.ReleaseYear = reader["ReleaseYear"].ToString();
                        console.ConsoleImageURL = (string)reader["ConsoleImageURL"];
                        console.Price = reader["Price"].ToString();
                        console.ConsoleDescription = reader["ConsoleDescription"].ToString();
                        ConsoleWishlistItem.Add(console);
                    }
                }


            }
            return RedirectToPage("/Consoles/Index");
        }
    }
}
