using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;

namespace NintendoInventory.UI.Pages.Wishlist
{
    public class addModel : PageModel
    {
        
        //Adds games/consoles to the wishlist. Probably will be used in games and consoles page and not wishlist.
        [BindProperty]
        public List<Models.GameWishlistItem> WishlistList { get; set; } = new List<Models.GameWishlistItem>();

        public void OnGet(int id)
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
                string sql = "INSERT INTO GameWishlist(GameID) VALUES (@GameID)"; 
                // step 3
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@GameID", id);
                // step 4
                conn.Open();
                // step 5
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GameWishlistItem game = new Models.GameWishlistItem();
                        game.GameID = int.Parse(reader["GameId"].ToString());
                        game.GameTitle = reader["GameTitle"].ToString();
                        game.ReleaseYear = reader["ReleaseYear"].ToString();
                        game.GameImageURL = (string)reader["GameImageURL"];
                        game.Price = reader["Price"].ToString();
                        game.GameDescription = reader["GameDescription"].ToString();
                        //game.ESBRRatingID = (int)reader["ESBRRatingID"];
                        game.GameID = int.Parse(reader["GameId"].ToString());
                        WishlistList.Add(game);
                    }
                }
            }
        }
    }
}
