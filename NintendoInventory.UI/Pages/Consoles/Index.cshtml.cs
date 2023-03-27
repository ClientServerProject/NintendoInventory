using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NintendoInventory.UI.Models;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;

namespace NintendoInventory.UI.Pages.Consoles
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Models.Console> ConsoleList { get; set; } = new List<Models.Console>();
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
                string sql = "SELECT * FROM Console Order By ConsoleName";
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
                        Models.Console console = new Models.Console();
                        console.ConsoleName = reader["ConsoleName"].ToString();
                        //console.ReleaseDate = SqlDateTime.Parse(reader["ReleaseDate"].ToString());
                        console.ConsoleImageURL = reader["ConsoleImageURL"].ToString();
                        console.Price = decimal.Parse(reader["Price"].ToString());
                        console.ConsoleID = (int)reader["ConsoleID"];
                        ConsoleList.Add(console);
                    }
                }
            }
        }
    }
}

