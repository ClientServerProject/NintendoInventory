using System.ComponentModel.DataAnnotations;

namespace NintendoInventory.UI.Models
{
    public class ProfileImage
    {
        public int ProfileImageID { get; set; }
        [Required]
        public string ProfileImageURL { get; set; } = string.Empty;

    }
}
