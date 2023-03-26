using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NintendoInventory.UI.Models;

namespace NintendoInventory.UI.Pages.Wishlist
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<WishlistItem> Wishlist { get; set; } = new List<WishlistItem>();
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
                        Author author = new Author();
                        author.AuthorName = reader["AuthorName"].ToString();
                        author.AuthorBio = reader["AuthorBio"].ToString();
                        author.AuthorImageURL = reader["AuthorImageURL"].ToString();
                        author.AuthorWebsite = reader["AuthorWebsite"].ToString();
                        author.AuthorId = int.Parse(reader["AuthorId"].ToString());
                        AuthorList.Add(author);
                    }
                }
            }
        }
    }
}
