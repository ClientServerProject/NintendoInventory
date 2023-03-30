using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace NintendoInventory.UI.Models
{
    public class Console
    {
        public int ConsoleID { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string ConsoleName { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required.")]
        public string ConsoleImageURL { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required.")]
        //public SqlDateTime ReleaseDate { get; set; } 
        public decimal Price { get; set; }
    }
}
