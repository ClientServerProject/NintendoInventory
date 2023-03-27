using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace NintendoInventory.UI.Models
{
    public class Console
    {
        public int ConsoleID { get; set; }
        public string ConsoleName { get; set; } = string.Empty;
        [Required]
        public string ConsoleImageURL { get; set; } = string.Empty;
        [Required]
        //public SqlDateTime ReleaseDate { get; set; } 
        public decimal Price { get; set; }
    }
}
