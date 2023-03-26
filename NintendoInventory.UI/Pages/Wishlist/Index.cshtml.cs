using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NintendoInventory.UI.Models;
using System.Data.SqlClient;

namespace NintendoInventory.UI.Pages.Wishlist
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<Models.Wishlist> WishlistList { get; set; } = new List<Models.Wishlist>();
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
                string sql = "SELECT * FROM Author Order By AuthorName";
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
                        /*Models.Wishlist author = new Models.Wishlist();
                        game.AuthorName = reader["AuthorName"].ToString();
                        author.AuthorBio = reader["AuthorBio"].ToString();
                        author.AuthorImageURL = reader["AuthorImageURL"].ToString();
                        author.AuthorWebsite = reader["AuthorWebsite"].ToString();
                        author.AuthorId = int.Parse(reader["AuthorId"].ToString());
                        AuthorList.Add(author);*/
                    }
                }
            }
        }
    }
}
