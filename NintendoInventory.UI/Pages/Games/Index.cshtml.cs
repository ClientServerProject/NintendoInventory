using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NintendoInventory.UI.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data.SqlTypes;
using Game = NintendoInventory.UI.Models.Game;

namespace NintendoInventory.UI.Pages.Games
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string SearchText { get; set; }
        public List<Models.Game> GameList { get; set; } = new List<Models.Game>();
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
                string sql = "SELECT * FROM Game Order By GameTitle";
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
                        game.GameDescription = reader["GameDescription"].ToString();
                        //game.ESBRRatingID = (int)reader["ESBRRatingID"];
                        game.GameID = int.Parse(reader["GameId"].ToString());
                        GameList.Add(game);
                        //WishlistItem.Add(game);
                    }
                }
            }
        }


        public IActionResult onPost(int id)
        {
            using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                string sql = "DELETE FROM Game WHERE GameID = @gameID";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gameID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return RedirectToPage("Index");
            }
        }

        public void OnPost()
        {
            string sql = "";
            if (SearchText == "")
            {
                sql = "SELECT * FROM Game Order By GameTitle";
            }
            else
            {
                sql = "SELECT * FROM Game WHERE GameTitle LIKE @gameTitle Order By GameTitle";
            }
                using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gameTitle", "%" + SearchText + "%");
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
                        game.GameDescription = reader["GameDescription"].ToString();
                        //game.ESBRRatingID = (int)reader["ESBRRatingID"];
                        game.GameID = int.Parse(reader["GameId"].ToString());
                        GameList.Add(game);
                        //WishlistItem.Add(game);
                    }
                }
            }
        }
    }
}

