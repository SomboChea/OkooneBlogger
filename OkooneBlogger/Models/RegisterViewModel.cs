﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OkooneBlogger.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, DataType(DataType.EmailAddress), MaxLength(50)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password), MinLength(5)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
