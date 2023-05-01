using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class ConsoleWishlistItem
    {
        [Required]
        public int ConsoleID { get; set; }
        public string ConsoleName { get; set; } = string.Empty;
        public string ConsoleImageURL { get; set; } = string.Empty;
        public string ReleaseYear { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string ConsoleDescription { get; set; } = string.Empty;
    }
}
