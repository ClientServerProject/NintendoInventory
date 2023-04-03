﻿using System.ComponentModel.DataAnnotations;


namespace NintendoInventory.UI.Models
{
    public class Login
    {
        [Required(ErrorMessage = "This field is required.")]
        public string UserID { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required.")]
        public string PasswordHash { get; set; } = string.Empty;
        
    }
}
   