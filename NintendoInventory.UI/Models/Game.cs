using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class Game
    {
        public int GameId { get; set; }
        [Required]
        public string GameTitle { get; set; } = "Game Title";
        [Required]
        public string GameDescription { get; set; } = "Game Description";
        [Required]
        public string GameImageURL { get; set; } = "Game Image URL";
        [Required]
        public int ESBRRatingID { get; set; }
        [Required]
        public int ConsoleID { get; set; }
        [Required]
        public DateOnly ReleaseDate { get; set; }
        public string Price { get; set; } = "Game Price";

    }
}
