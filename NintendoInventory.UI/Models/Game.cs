using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class Game
    {
        //public int GameId { get; set; }
        //[Required(ErrorMessage = "This field is required.")]
        public string GameTitle { get; set; } = string.Empty;
        [Required(ErrorMessage = "This field is required.")]
        //public string GameDescription { get; set; } = string.Empty;
        //[Required(ErrorMessage = "This field is required.")]
        public string GameImageURL { get; set; } = string.Empty;
        //public int ESBRRatingID { get; set; }
        //public int ConsoleID { get; set; }
        public string ReleaseDate { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        

    }
}
