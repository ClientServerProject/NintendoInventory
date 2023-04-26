using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class GameWishlistItem
    {
        //public int WishlistID = { get; set; }

    [Required]
        public int GameID { get; set; }

        public string GameTitle { get; set; } = string.Empty;

        public string GameDescription { get; set; } = string.Empty;

        public string GameImageURL { get; set; } = string.Empty;
        //public int ESBRRatingID { get; set; }
        //public int ConsoleID { get; set; }
        public string ReleaseYear { get; set; } = string.Empty;

        public string Price { get; set; } = string.Empty;

    }
}
