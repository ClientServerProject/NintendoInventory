using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class Console
    {
        [Required]
        public int ConsoleId { get; set; }
        [Required]
        public string ConsoleName { get; set; } = string.Empty;
        [Required]
        public string ConsoleReleaseDate { get; set; } = string.Empty;
        [Required]
        public string ConsoleImageURL { get; set; } = string.Empty;
        [Required]
        public string ConsolePrice { get; set; } = string.Empty;
        
    }
}
