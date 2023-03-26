using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class WishlistItem
    {
        public int WishlistId { 
            
            get; set; 
        
        
        }
        [Required]
        public int GameId { get; set; }
        
        public int ConsoleId { get; set; }
        
    }
}
