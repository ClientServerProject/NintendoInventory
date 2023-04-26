using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;
using Console = NintendoInventory.UI.Models.Console;

namespace NintendoInventoryUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public List<Game> GamePreview { get; set; } = new List<Game>();
        [BindProperty]
        public List<Console> ConsolePreview { get; set; } = new List<Console>();

        public void OnGet()
        {

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
                    while (reader.Read() && GamePreview.Count < 5)
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
                        GamePreview.Add(game);
                        //WishlistItem.Add(game);
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(DBhelper.GetConnectionString()))
            {
                // step 2
                string sql = "SELECT * FROM Console Order By ConsoleName";
                // step 3
                SqlCommand cmd = new SqlCommand(sql, conn);
                // step 4
                conn.Open();
                // step 5
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read() && ConsolePreview.Count < 5)
                    {
                        Console console = new Console();
                        console.ConsoleName = reader["ConsoleName"].ToString();
                        console.ReleaseYear = reader["ReleaseYear"].ToString();
                        console.ConsoleImageURL = reader["ConsoleImageURL"].ToString();
                        console.Price = reader["Price"].ToString();
                        console.ConsoleID = (int)reader["ConsoleID"];
                        console.ConsoleDescription = reader["ConsoleDescription"].ToString();
                        ConsolePreview.Add(console);
                    }
                }
            }
        }
    }
}