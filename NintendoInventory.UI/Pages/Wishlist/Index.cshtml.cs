using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;
using System.Data.SqlClient;

namespace NintendoInventory.UI.Pages.Wishlist
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Models.GameWishlistItem> WishlistList { get; set; } = new List<Models.GameWishlistItem>();
        [BindProperty]
        public List<Models.ConsoleWishlistItem> ConsoleWishlistList { get; set; } = new List<Models.ConsoleWishlistItem>();
        public void OnGet(int id, string itemType)
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
                if(itemType == "game")
                {
                    // step 2
                    string sql = "SELECT * FROM (Game Inner Join GameWishlist on Game.GameID = GameWishlist.GameID) Order by GameTitle"; //INSERT INTO GameWishlist(GameID) VALUES (@GameID); 
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
                            //game.ConsoleID = int.Parse(reader["ConsoleId"].ToString());
                            WishlistList.Add(game);
                        }
                    }
                }
                else if(itemType == "console")
                {
                    // step 2
                    string sql = "SELECT * FROM (Console Inner Join ConsoleWishlist on Console.ConsoleID = ConsoleWishlist.ConsoleID) Order by ConsoleName"; //INSERT INTO GameWishlist(GameID) VALUES (@GameID); 
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
                            //game.ESBRRatingID = (int)reader["ESBRRatingID"];
                            ConsoleWishlistList.Add(console);
                        }
                    }
                }
                else
                {
                    string sql = "SELECT * FROM (Game Inner Join GameWishlist on Game.GameID = GameWishlist.GameID) Order by GameTitle"; //INSERT INTO GameWishlist(GameID) VALUES (@GameID); 
                    string sql2 = "SELECT * FROM (Console Inner Join ConsoleWishlist on Console.ConsoleID = ConsoleWishlist.ConsoleID) Order by ConsoleName";
                    // step 3
                    SqlCommand cmd = new SqlCommand($"{sql}; {sql2}", conn);
                    cmd.Parameters.AddWithValue("@GameID", id);
                    cmd.Parameters.AddWithValue("@ConsoleID", id);
                    // step 4
                    conn.Open();
                    // step 5
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            if (reader["GameID"] != DBNull.Value)
                            {
                                GameWishlistItem game = new Models.GameWishlistItem();
                                game.GameID = int.Parse(reader["GameID"].ToString());
                                game.GameTitle = reader["GameTitle"].ToString();
                                game.ReleaseYear = reader["ReleaseYear"].ToString();
                                game.GameImageURL = (string)reader["GameImageURL"];
                                game.Price = reader["Price"].ToString();
                                game.GameDescription = reader["GameDescription"].ToString();
                                WishlistList.Add(game);

                            }  
                        }
                    }

                    reader.NextResult();
                    
                    if (reader.HasRows)  // Check if it is a console wishlist item
                    {
                        while (reader.Read())
                        {
                            if (reader["ConsoleID"] != DBNull.Value)
                            {
                                ConsoleWishlistItem console = new Models.ConsoleWishlistItem();
                                console.ConsoleID = int.Parse(reader["ConsoleID"].ToString());
                                console.ConsoleName = reader["ConsoleName"].ToString();
                                console.ReleaseYear = reader["ReleaseYear"].ToString();
                                console.ConsoleImageURL = (string)reader["ConsoleImageURL"];
                                console.Price = reader["Price"].ToString();
                                console.ConsoleDescription = reader["ConsoleDescription"].ToString();
                                ConsoleWishlistList.Add(console);

                            } 
                        }
                    }
                    conn.Close();
                }
               
            }
        }
    }
}
