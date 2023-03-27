using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class Console
    {
        public int ConsoleID { get; set; }
        [Required]
        public string ConsoleName { get; set; } = string.Empty;
        [Required]
        public string ConsoleImageURL { get; set; } = string.Empty;
        [Required] 
        public DateOnly ReleaseDate { get; set; }
        public string Price { get; set; }
    }
}
