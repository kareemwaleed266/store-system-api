﻿using System.ComponentModel.DataAnnotations;

namespace Store.Service.Services.UserService.Dto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
