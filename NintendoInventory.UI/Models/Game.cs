using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string GameTitle { get; set; } = string.Empty;
        [Required]
        public string GameDescription { get; set; } = string.Empty;
        [Required]
        public string GameImageURL { get; set; } = string.Empty;
        [Required]
        public int ESBRRatingID { get; set; }
        public int ConsoleID { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Price { get; set; } = string.Empty;

    }
}
