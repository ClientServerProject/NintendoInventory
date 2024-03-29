﻿using System.ComponentModel.DataAnnotations;


namespace NintendoInventory.UI.Models
{
    public class Login
    {
        [Required(ErrorMessage = "This field is required.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; } = string.Empty;

    }
}
   