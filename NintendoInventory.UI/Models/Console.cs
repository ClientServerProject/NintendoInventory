using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace NintendoInventory.UI.Models
{
    public class Console
    {
        public int ConsoleID { get; set; }

        [StringLength(50, MinimumLength = 2)]
        [Required(ErrorMessage = "This field is required.")]
        public string ConsoleName { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required.")]
        public string ConsoleImageURL { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required.")]
        //public DateTime ReleaseDate { get; set; } 
        public string Price { get; set; } = string.Empty;
    }
}
