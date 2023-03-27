using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class Game
    {
        public int GameId { get; set; }
        [Required]
        public string GameTitle { get; set; } = string.Empty;
        [Required]
        public string GameDescription { get; set; } = string.Empty;
        [Required]
        public string GameImageURL { get; set; } = string.Empty;
        [Required]
        public int ESBRRatingID { get; set; }
        [Required]
        public int ConsoleID { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }

    }
}
