using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using NintendoInventory.UI.Models;
using Game = NintendoInventory.UI.Models.Game;

namespace NintendoInventory.UI.Pages.Games
{
    public class AddGameModel : PageModel
    {
        [BindProperty]
        public Game NewGame { get; set; } = new Game();

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
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
                    /*string sql = "INSERT INTO Game(GameTitle, GameDescription, GameImageURL, ReleaseDate, Price) " +
                        "VALUES (@GameTitle, @GameDescription, @GameImageURL, @ReleaseDate, @Price)";*/
                    string sql = "INSERT INTO Game(GameTitle, GameDescription, GameImageURL, ReleaseYear, Price) " +
                        "VALUES (@GameTitle, @GameDescription, @GameImageURL, @ReleaseYear, @Price)";
                    //DateTime releaseDate = DateTime.Parse(NewGame.ReleaseDate);
                    // step 3
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@GameTitle", NewGame.GameTitle);
                    cmd.Parameters.AddWithValue("@GameDescription", NewGame.GameDescription);
                    cmd.Parameters.AddWithValue("@GameImageURL", NewGame.GameImageURL);
                    cmd.Parameters.AddWithValue("@ReleaseYear", NewGame.ReleaseYear);
                    cmd.Parameters.AddWithValue("@Price", NewGame.Price);
                    // step 4s
                    conn.Open();
                    // step 5
                    cmd.ExecuteNonQuery();
                }
                return RedirectToPage("Index");
            }
            else
            {
                return Page();
            }


        }
    }
}
