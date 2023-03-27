using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using NintendoInventory.UI.Models;
using Console = NintendoInventory.UI.Models.Console;

namespace NintendoInventory.UI.Pages.Consoles
{
    public class addModel : PageModel
    {
        [BindProperty]
        public Console NewConsole { get; set; } = new Console();
        public void OnGet()
        {

        }
        //Adds games/consoles to the wishlist. Probably will be used in games and consoles page and not wishlist.

        public IActionResult OnPost()
        {
            //if (LikeButton is selected) //pseudocode
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
                    //string sql = "INSERT INTO Console(ConsoleImageURL, ConsoleName, ReleaseDate, Price) " +
                    //    "VALUES (@ConsoleImageURL, @ConsoleName, @ReleaseDate, @Price)";
                    string sql = "INSERT INTO Console(ConsoleImageURL, ConsoleName, Price) " +
                        "VALUES (@ConsoleImageURL, @ConsoleName, @Price)";
                    // step 3
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ConsoleImageURL", NewConsole.ConsoleImageURL);
                    cmd.Parameters.AddWithValue("@ConsoleName", NewConsole.ConsoleName);
                    //cmd.Parameters.AddWithValue("@ReleaseDate", NewConsole.ReleaseDate); 
                    cmd.Parameters.AddWithValue("@Price", NewConsole.Price);
                    // step 4
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