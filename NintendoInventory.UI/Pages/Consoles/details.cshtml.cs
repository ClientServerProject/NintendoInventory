using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using NintendoInventory.UI.Models;
using Console = NintendoInventory.UI.Models.Console;

namespace NintendoInventory.UI.Pages.Consoles
{
    public class detailsModel : PageModel   
    {
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
                string sql = "SELECT * FROM Console Order By ConsoleID";
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
                        //console.ReleaseDate = (DateTime)reader["ReleaseDate"];
                        console.ConsoleImageURL = reader["ConsoleImageURL"].ToString();
                        console.Price = reader["Price"].ToString();
                        console.ConsoleID = (int)reader["ConsoleID"];
                    }
                }
            }
            return RedirectToPage("Index");
        }
    }
}

