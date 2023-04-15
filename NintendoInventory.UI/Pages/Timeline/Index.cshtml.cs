using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;
using Game = NintendoInventory.UI.Models.Game;
using Console = NintendoInventory.UI.Models.Console;

namespace NintendoInventory.UI.Pages.Timeline
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Game> GameTimeline { get; set; } = new List<Game>();
        [BindProperty]
        public List<Console> ConsoleTimeline { get; set; } = new List<Console>();

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
            using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                // step 2
                string sql = "SELECT * FROM Game Order By ReleaseYear";
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
                        Game game = new Game();
                        game.GameTitle = reader["GameTitle"].ToString();
                        game.ReleaseYear = reader["ReleaseYear"].ToString();
                        //game.ConsoleID = (int)reader["ConsoleID"];
                        game.GameImageURL = (string)reader["GameImageURL"];
                        game.Price = reader["Price"].ToString();
                        //game.GameDescription = (string)reader["GameDescription"];
                        //game.ESBRRatingID = (int)reader["ESBRRatingID"];
                        game.GameID = int.Parse(reader["GameId"].ToString());
                        GameTimeline.Add(game);
                        //WishlistItem.Add(game);
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                // step 2
                string sql = "SELECT * FROM Console Order By ReleaseYear";
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
                        Console console = new Console();
                        console.ConsoleName = reader["ConsoleName"].ToString();
                        console.ReleaseYear = reader["ReleaseYear"].ToString();
                        console.ConsoleImageURL = reader["ConsoleImageURL"].ToString();
                        console.Price = reader["Price"].ToString();
                        console.ConsoleID = (int)reader["ConsoleID"];
                        console.ConsoleDescription = reader["ConsoleDescription"].ToString();
                        ConsoleTimeline.Add(console);
                    }
                }
            }
        }
    }
}