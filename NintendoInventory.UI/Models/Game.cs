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
        public string GameImageURL { get; set; } = string.Empty;
        public string GamePrice { get; set; } = string.Empty;
        public string GameESRBRatingId { get; set; } = string.Empty;
        public string GameConsoleId { get; set; } = string.Empty;
        public string GameReleaseDate { get; set; } = string.Empty;
    }
}
