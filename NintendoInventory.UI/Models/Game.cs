using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class Game
    {
        public int GameID { get; set; }
        [StringLength(80, MinimumLength = 2)]
        [Required(ErrorMessage = "This field is required.")]
        public string GameTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required.")]
        public string GameDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required.")]
        public string GameImageURL { get; set; } = string.Empty;
        //public int ESBRRatingID { get; set; }
        //public int ConsoleID { get; set; }
        [StringLength(4, MinimumLength = 4)]
        public string ReleaseYear { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required.")]
        public string Price { get; set; } = string.Empty;


    }
}
