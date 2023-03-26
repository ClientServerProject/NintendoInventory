using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NintendoInventory.UI.Models;

namespace NintendoInventory.UI.Pages.Games
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Game> GameList { get; set; } = new List<Game>();
        public void OnGet()
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
            using (SqlConnection conn = new SqlConnection(DBHelper.GetConnectionString()))
            {
                // step 2
                string sql = "SELECT * FROM Game Order By GameName";
                // step 3
                SqlCommand cmd = new SqlCommand(sql, conn);
                // step 4
                conn.Open();
                // step 5
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Game game = new game();
                        game.GameName = reader["GameName"].ToString();
                        game.ReleaseDate = reader["ReleaseDate"].ToString();
                        game.ConsoleId = reader["ConsoleId"].ToString();
                        game.GameImageURL = reader["GameImageURL"].ToString();
                        game.Price = reader["Price"].ToString();
                        game.Description = reader["Description"].ToString();
                        game.ESRBRatingId = reader["ESRBRatingId"].ToString();
                        game.GameId = int.Parse(reader["GameId"].ToString());
                        Wishlist.Add(game);
                    }
                }
            }
        }
    }
}
}
